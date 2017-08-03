using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.IO;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Properties;

namespace SIE.Services.Servicios.BL
{
    internal class DescargarArchivoDataLinkBL
    {
        private const string Guion = "-";
        private const string PeMayuscula = "P";
        private const char Coma = ',';
        private const char Punto = '.';
        private const string TodosLosArchivos = "*.*";
        /// <summary>
        /// Valida si existe la ruta del archivo datalink
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        internal bool ValidarRuta(string ruta)
        {
            var resultado = false;
            try
            {
                Logger.Info();
                if (Directory.Exists(ruta))
                {
                    resultado = true;
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Lectura del archivo datalink
        /// </summary>
        /// <param name="validacion"></param>
        /// <returns></returns>
        internal ResultadoValidacion LeerArchivo(ValidacionDataLink validacion)
        {
            var resultado = new ResultadoValidacion { Resultado = true };
            validacion.ListaDataLink = new List<DataLinkInfo>();
            try
            {
                Logger.Info();
                var archivo = validacion.RutaArchivo + validacion.NombreArchivo;
                if (Directory.Exists(validacion.RutaArchivo))
                {
                    var linea = string.Empty;

                    if (File.Exists(archivo))
                    {
                        var readerArchivo = new StreamReader(archivo);
                        while (linea != null)
                        {
                            linea = readerArchivo.ReadLine();
                            if (linea != null)
                            {
                                var dataLink = ObtenerDatalink(linea);
                                if (dataLink != null)
                                {
                                    if (!dataLink.CodigoCorral.Contains(Guion))
                                    {
                                        if (dataLink.CodigoCorral.Substring(0, 1).ToUpper() != PeMayuscula)
                                        {
                                            validacion.ListaDataLink.Add(dataLink);
                                        }

                                    }

                                }
                            }
                        }
                        readerArchivo.Close();
                    }
                    else
                    {
                        resultado.Resultado = false;
                        resultado.Mensaje = ResourceServices.DescargarDataLink_ArchivoNoExiste;
                    }
                }
                else
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = ResourceServices.DescargarDataLink_RutaNoExiste;
                }

            }
            catch (ExcepcionGenerica)
            {
                resultado.Resultado = false;
                resultado.Mensaje = ResourceServices.DescargarDataLink_ErrorInesperado;
                throw;
            }
            catch (Exception ex)
            {
                resultado.Resultado = false;
                resultado.Mensaje = ResourceServices.DescargarDataLink_ErrorInesperado;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// obtiene la informcacion del lata link de las lineas del archivo
        /// </summary>
        /// <param name="linea"></param>
        /// <returns></returns>
        private DataLinkInfo ObtenerDatalink(string linea)
        {
            DataLinkInfo datalink;
            try
            {
                var valores = linea.Split(Coma);
                datalink = new DataLinkInfo();

                if (valores.Length > 0)
                {
                    if(valores[2].Trim().Equals("I", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return null;
                    }
                    datalink.TipoServicio = int.Parse(valores[4][0].ToString(CultureInfo.InvariantCulture));
                    datalink.CodigoCorral = valores[5];
                    datalink.ClaveFormula = valores[6].Trim();
                    /*var formulaTemporal = valores[6];
                    var posicion31 = formulaTemporal.Substring(5, 1).Trim();
                    if (posicion31 == cero || string.IsNullOrEmpty(posicion31))
                    {
                        datalink.ClaveFormula = formulaTemporal.Substring(0, 1);
                    }
                    else
                    {
                        datalink.ClaveFormula = posicion31;
                    }*/
                    datalink.KilosProgramados = int.Parse(valores[7]);
                    datalink.KilosServidos = int.Parse(valores[8]);
                    datalink.Hora = valores[10];
                    datalink.CadenaFechaReparto = valores[12];
                    datalink.FechaReparto = ObtenerFecha(datalink.CadenaFechaReparto);
                    datalink.NumeroCamion = valores[0];
                }

            }
            catch (Exception ex)
            {
                datalink = null;
                Logger.Error(ex);
            }
            return datalink;
        }
        /// <summary>
        /// Carga el archivo data link en la orden de reparto
        /// </summary>
        /// <param name="validacionDatalink"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        internal ResultadoOperacion CargarArchivoDatalink(ValidacionDataLink validacionDatalink, UsuarioInfo usuario)
        {
            var validar = new DataLinkInfo();
            var resultado = new ResultadoOperacion{Resultado = true};
            try
            {
                Logger.Info();
                var corralBl = new CorralBL();
                var loteBl = new LoteBL();
                var repartoBl = new RepartoBL();
                using (var camionRepartoBl = new CamionRepartoBL())
                {
                    var servicioAlimentoBL = new ServicioAlimentoBL();
                    var datalinks = new List<DataLinkInfo>();
                    var repartosServicioAlimentacion = new List<RepartoInfo>();
                    var repartosDetalles = new List<RepartoDetalleInfo>();
                    var corralesEliminar = new List<CorralInfo>();

                    var formulaBL = new FormulaBL();
                    IList<FormulaInfo> formulasExistentes = formulaBL.ObtenerTodos(EstatusEnum.Activo);

                    IList<CamionRepartoInfo> camionesRepartoOrganizacion =
                        camionRepartoBl.ObtenerPorOrganizacionID(usuario.Organizacion.OrganizacionID);

                    foreach (var dataLink in validacionDatalink.ListaDataLink)
                    {
                        validar = dataLink;

                        //CamionRepartoInfo camionRepartoInfo = camionRepartoBl.ObtenerPorNumeroEconomico(dataLink.NumeroCamion.Trim(), usuario.Organizacion.OrganizacionID);
                        CamionRepartoInfo camionRepartoInfo =
                            camionesRepartoOrganizacion.FirstOrDefault(
                                cam =>
                                cam.NumeroEconomico.Equals(dataLink.NumeroCamion.Trim(),
                                                           StringComparison.CurrentCultureIgnoreCase));
                        dataLink.CamionReparto = camionRepartoInfo;
                        CorralInfo corral = corralBl.ObtenerPorCodicoOrganizacionCorral(new CorralInfo
                            {
                                Codigo = dataLink.CodigoCorral,
                                Organizacion = new OrganizacionInfo { OrganizacionID = usuario.Organizacion.OrganizacionID },
                                Activo = EstatusEnum.Activo,
                            });
                        if (corral != null)
                        {
                            var lote = loteBl.ObtenerPorCorralCerrado(usuario.Organizacion.OrganizacionID, corral.CorralID);
                            if(lote == null)
                            {
                                lote = new LoteInfo();
                            }
                            //if (lote != null)
                            //{

                                var fecha = ObtenerFecha(dataLink.CadenaFechaReparto);
                                var reparto = repartoBl.ObtenerRepartoPorFechaCorralServicio(fecha, corral, dataLink.TipoServicio);
                                if (reparto != null)
                                {
                                    //var formulaBl = new FormulaBL();
                                    //int formulaId;
                                    //int.TryParse(dataLink.ClaveFormula, out formulaId);
                                    var formula = formulasExistentes.FirstOrDefault(fo => fo.Descripcion.Equals(dataLink.ClaveFormula.Trim(), StringComparison.CurrentCultureIgnoreCase));
                                    if (formula != null)
                                    {
                                        dataLink.FormulaServida = formula;
                                        dataLink.Reparto = reparto;
                                        dataLink.UsuarioID = usuario.UsuarioID;
                                        dataLink.OrganizacionID = usuario.Organizacion.OrganizacionID;
                                        datalinks.Add(dataLink);
                                    }
                                }
                                //Si el Lote no se encuentra en el Reparto, buscarlo en la tabla ServicioAlimento
                                else
                                {
                                    ServicioAlimentoInfo servicioAlimentoInfo =
                                        servicioAlimentoBL.ObtenerPorCorralID(usuario.Organizacion.OrganizacionID,
                                                                              corral.CorralID);

                                    if (servicioAlimentoInfo != null)
                                    {
                                        LoteDescargaDataLinkModel datosLote =
                                            loteBl.ObtenerLoteDataLink(usuario.Organizacion.OrganizacionID, lote.LoteID);

                                        if(datosLote == null)
                                        {
                                            datosLote = new LoteDescargaDataLinkModel
                                                {
                                                    PesoInicio = 0,
                                                    FechaInicio = fecha,
                                                    Cabezas = 0,
                                                };
                                        }


                                        //var formulaBl = new FormulaBL();
                                        //int formulaId;
                                        //int.TryParse(dataLink.ClaveFormula, out formulaId);
                                        //var formula = formulaBl.ObtenerPorID(formulaId);
                                        var formula = formulasExistentes.FirstOrDefault(fo => fo.Descripcion.Equals(dataLink.ClaveFormula.Trim(), StringComparison.CurrentCultureIgnoreCase));
                                        if (formula != null)
                                        {

                                            var repartoNuevo = new RepartoInfo
                                                {
                                                    OrganizacionID = usuario.Organizacion.OrganizacionID,
                                                    LoteID = lote.LoteID,
                                                    Corral = corral,
                                                    Fecha = fecha,
                                                    PesoInicio = datosLote.PesoInicio,
                                                    PesoProyectado = 0,
                                                    DiasEngorda = Convert.ToInt32((fecha - datosLote.FechaInicio).TotalDays),
                                                    PesoRepeso = 0,
                                                    DetalleReparto = new List<RepartoDetalleInfo>(),
                                                    UsuarioCreacionID = usuario.UsuarioID,
                                                    Activo = EstatusEnum.Activo
                                                };

                                            var detalleReparto = new RepartoDetalleInfo
                                                {
                                                    TipoServicioID = dataLink.TipoServicio,
                                                    FormulaIDProgramada = servicioAlimentoInfo.FormulaID,
                                                    FormulaIDServida = formula.FormulaId,
                                                    CantidadProgramada = servicioAlimentoInfo.KilosProgramados,
                                                    CantidadServida = dataLink.KilosServidos,
                                                    HoraReparto = dataLink.Hora,
                                                    CostoPromedio = 0,
                                                    Importe = 0,
                                                    Servido = true,
                                                    Cabezas = datosLote.Cabezas == 0 ? 1 : datosLote.Cabezas,
                                                    EstadoComederoID = EstadoComederoEnum.Normal.GetHashCode(),
                                                    CamionRepartoID = dataLink.CamionReparto.CamionRepartoID,
                                                    UsuarioCreacionID = usuario.UsuarioID,
                                                    Activo = EstatusEnum.Activo
                                                };
                                            repartoNuevo.DetalleReparto.Add(detalleReparto);

                                            repartosServicioAlimentacion.Add(repartoNuevo);

                                            var corralEliminar = new CorralInfo
                                                {
                                                    CorralID = corral.CorralID,
                                                    UsuarioModificacionID = usuario.UsuarioID
                                                };
                                            corralesEliminar.Add(corralEliminar);

                                        }
                                    }
                                }
                            //}
                            //else
                            //{
                            //    var bitacoraBL = new BitacoraIncidenciasBL();
                            //    var errorInfo = new BitacoraErroresInfo
                            //        {
                            //            AccionesSiapID = AccionesSIAPEnum.DeDataLink,
                            //            Mensaje = "No es posible aplicar el consumo para el corral: " + corral.Codigo,
                            //            UsuarioCreacionID = usuario.UsuarioID
                            //        };

                            //    //DescargarArchivoDataLink
                            //    bitacoraBL.GuardarError(errorInfo);
                            //}

                        }
                    }
                    if (datalinks.Count > 0)
                    {
                        if (datalinks.Any(x => x.CamionReparto == null))
                        {
                            resultado.Resultado = false;
                            resultado.DescripcionMensaje = ResourceServices.DescargaDataLink_msgCamionNoAsignado;
                        }
                        else
                        {
                            using (var transaccion = new TransactionScope())
                            {
                                if (repartosServicioAlimentacion.Any())
                                {

                                    foreach (var repartoInfo in repartosServicioAlimentacion)
                                    {
                                        int repartoID = repartoBl.Guardar(repartoInfo);
                                        repartoInfo.DetalleReparto.ToList().ForEach(rep => rep.RepartoID = repartoID);
                                        repartosDetalles.AddRange(repartoInfo.DetalleReparto);
                                    }
                                    repartoBl.GuardarRepartoDetalle(repartosDetalles);
                                    if (corralesEliminar.Any())
                                    {
                                        servicioAlimentoBL.EliminarXML(corralesEliminar);
                                    }
                                }

                                var res = repartoBl.CargarArchivoDatalink(datalinks);
                                resultado.RegistrosAfectados = res;
                                resultado.DescripcionMensaje = ResourceServices.DescargarDataLink_GuradadoOk;
                                transaccion.Complete();
                            }

                            RenombrarArchivos(validacionDatalink);
                        }
                    }
                    else
                    {
                        resultado.Resultado = false;
                        resultado.DescripcionMensaje = ResourceServices.DescargarDataLink_NoSecargo;
                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                if(validar != null)
                {
                    
                }
                resultado.Resultado = false;
                throw;
            }
            catch (Exception ex)
            {
                if (validar != null)
                {

                }
                resultado.Resultado = false;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Obtiene la fecha del reparto
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private DateTime ObtenerFecha(string fecha)
        {
            var fechaConvertida = new DateTime();
            var porcionAno = ResourceServices.DescargarDataLink_PorcionAno;
            try
            {
                var mes = fecha.Substring(0, 2);
                var dia = fecha.Substring(3, 2);
                var ano = porcionAno + fecha.Substring(6, 2);
                var iMes = int.Parse(mes);
                var iDia = int.Parse(dia);
                var iAno = int.Parse(ano);
                fechaConvertida = new DateTime(iAno, iMes, iDia);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return fechaConvertida;
        }
        /// <summary>
        /// Valida que los datos del archivo correspondan a la fecha
        /// </summary>
        /// <param name="validacion"></param>
        /// <returns></returns>
        internal ResultadoValidacion ValidarDatosArchivo(ValidacionDataLink validacion)
        {
            var resultado = new ResultadoValidacion { Resultado = true };
            try
            {
                Logger.Info();
                if (validacion.ListaDataLink.Count > 0)
                {
                    var listaValidos = new List<DataLinkInfo>();
                    listaValidos.AddRange(validacion.ListaDataLink.Where(dataLink => dataLink.FechaReparto == validacion.Fecha && dataLink.TipoServicio == (int)validacion.TipoServicio));
                    validacion.ListaDataLink = listaValidos;
                    if (listaValidos.Count == 0)
                    {
                        resultado.Resultado = false;
                        resultado.TipoResultadoValidacion = TipoResultadoValidacion.RepartoFechaNoValida;
                        resultado.Mensaje = ResourceServices.DescargarDataLink_FechaNoValida;
                    }
                }
                else
                {
                    resultado.Resultado = false;
                    resultado.TipoResultadoValidacion = TipoResultadoValidacion.RepartoArchivoSinDatos;
                    resultado.Mensaje = ResourceServices.DescargarDataLink_ArchivoSinDatos;
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Realiza los respaldo de los archivos
        /// </summary>
        /// <param name="validacion"></param>
        /// <returns></returns>
        internal ResultadoOperacion RenombrarArchivos(ValidacionDataLink validacion)
        {
            var resultado = new ResultadoOperacion { Resultado = true };
            try
            {
                Logger.Info();
                var extencionArchivoFinal = ResourceServices.DescargarDataLink_ExtencionArchivoFinal;
                var extencionArchivoPrincipal = ResourceServices.DescargarDataLink_ExtencionArchivoPrincipal;
                var extencionArchivoInicial = ResourceServices.DescargarDataLink_ExtencionArchivoInicial;
                var extencialParcial = ResourceServices.DescargarDataLink_ExtencionParcial;
                var dirInfo = new DirectoryInfo(validacion.RutaArchivo);
                var fileNames = dirInfo.GetFiles(TodosLosArchivos);
                var archivosValidos = 0;
                var nombreArchivo = validacion.NombreArchivo.Split(Punto)[0];
                foreach (var fi in fileNames.Where(fi => !string.IsNullOrEmpty(fi.Name)).Where(fi => fi.Name.Split(Punto)[0].ToUpper().Trim().Equals(nombreArchivo.ToUpper().Trim())))
                {
                    archivosValidos++;
                    if (fi.Extension.ToUpper() == extencionArchivoFinal)
                    {
                        fi.Delete();
                    }
                }
                if (archivosValidos > 0)
                {
                    fileNames = dirInfo.GetFiles(TodosLosArchivos);
                    foreach (var fi in fileNames)
                    {
                        if (!string.IsNullOrEmpty(fi.Name))
                        {
                            if (fi.Name.Split(Punto)[0].ToUpper().Trim().Equals(nombreArchivo.ToUpper().Trim()))
                            {
                                var extencion = fi.Extension.ToUpper();
                                if (extencion.ToUpper().Trim().Equals(extencionArchivoPrincipal.ToUpper().Trim()))
                                {
                                    fi.CopyTo(validacion.RutaArchivo + nombreArchivo + extencionArchivoInicial);
                                    if(!string.IsNullOrWhiteSpace(validacion.RutaRespaldo))
                                    {
                                        fi.CopyTo(validacion.RutaRespaldo + nombreArchivo + extencionArchivoPrincipal, true);
                                    }
                                    fi.Delete();
                                }
                                else
                                {
                                    var numero = extencion.Substring(3, 1);

                                    var respaldo = int.Parse(numero) - 1;

                                    fi.CopyTo(validacion.RutaArchivo + nombreArchivo + extencialParcial + respaldo);
                                    fi.Delete();
                                }

                            }
                        }

                    }
                }
            }
            catch (ExcepcionGenerica)
            {
                resultado.Resultado = false;
            }
            catch (Exception)
            {
                resultado.Resultado = false;
            }
            return resultado;
        }
    }
}
