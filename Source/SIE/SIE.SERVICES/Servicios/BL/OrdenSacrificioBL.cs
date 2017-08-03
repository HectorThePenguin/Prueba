using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class OrdenSacrificioBL
    {
        /// <summary>
        /// Método Para Guardar en en la Tabla Animal
        /// </summary>
        internal OrdenSacrificioInfo GuardarOrdenSacrificio(OrdenSacrificioInfo ordenInfo, IList<OrdenSacrificioDetalleInfo> detalleOrden, int organizacionId)
        {
            OrdenSacrificioInfo result = null;
            try
            {
                Logger.Info();
                IList<SalidaSacrificioInfo> salidaSacrificio = null;
                var paramOrgPl = new ParametroOrganizacionBL();
                var salidaSacrificioDal = new SalidaSacrificioDAL();
                var ordenSacrificioDal = new OrdenSacrificioDAL();
                var salidaSacrificioNuevos = new List<SalidaSacrificioInfo>();
                var osId = 0;
                var aplicaMarel = false;
                string conexion = salidaSacrificioDal.ObtenerCadenaConexionSPI(organizacionId);
                var paramOrg = paramOrgPl.ObtenerPorOrganizacionIDClaveParametro(organizacionId, ParametrosEnum.AplicaSalidaSacrificioMarel.ToString());
                if (paramOrg != null)
                {
                    if (paramOrg.Valor.Trim() == "1")
                    {
                        aplicaMarel = true;
                        IList<SalidaSacrificioDetalleInfo> detalleSacrificio = null;
                        using (var transaction = new TransactionScope())
                        {
                            result = ordenSacrificioDal.GuardarOrdenSacrificio(ordenInfo);
                            if (result != null)
                            {
                                result.UsuarioCreacion = ordenInfo.UsuarioCreacion;
                                ordenSacrificioDal.GuardarDetalleOrdenSacrificio(result, detalleOrden);
                                salidaSacrificio = salidaSacrificioDal.ObtenerPorOrdenSacrificioID(result.OrdenSacrificioID);
                            }

                            //Obtenemos registros de la orden de sacrificio del SIAP para enviarlos al SCP
                            osId = Convert.ToInt32(result.OrdenSacrificioID);
                            detalleSacrificio = salidaSacrificioDal.ObtenerPorOrdenSacrificioId(osId, organizacionId, 0, ordenInfo.UsuarioCreacion);

                            transaction.Complete();
                        }

                        if (salidaSacrificio != null && salidaSacrificio.Count > 0)
                        {
                            foreach (var corralSacrificio in salidaSacrificio)
                            {
                                OrdenSacrificioDetalleInfo detalleValidar =
                                    detalleOrden.FirstOrDefault(orden => corralSacrificio.NUM_CORR.Equals(orden.Corral.Codigo.Trim().PadLeft(3, '0'), StringComparison.InvariantCultureIgnoreCase));

                                if (detalleValidar != null)
                                {
                                    if (detalleValidar.OrdenSacrificioDetalleID == 0)
                                    {
                                        salidaSacrificioNuevos.Add(corralSacrificio);
                                    }
                                }
                            }

                            var guardoSCP = false;
                            using (var transaction = new TransactionScope())
                            {
                                if (!salidaSacrificioDal.CrearLista(salidaSacrificioNuevos, conexion))
                                {
                                    Logger.Error(new Exception("Ocurrió un error al enviar información al SCP."));
                                    result = null;
                                }
                                else
                                {
                                    //Obtenemos salida de sacrificio del SCP para insertarla en Marel
                                    var sacrificio = salidaSacrificioDal.ObtenerSalidaSacrificio(detalleSacrificio.ToList(), organizacionId, conexion);
                                    if (sacrificio.Count > 0)
                                    {
                                        //Guardar información en Marel
                                        if (!salidaSacrificioDal.GuardarSalidaSacrificioMarel(sacrificio, organizacionId, conexion))
                                        {
                                            Logger.Error(new Exception("Ocurrió un error al enviar información al SCP-Marel."));
                                            result = null;
                                        }
                                        else
                                        {
                                            guardoSCP = true;
                                            transaction.Complete(); 
                                        }
                                    }
                                    else
                                    {
                                        Logger.Error(new Exception("Ocurrió un error al obtener información del SCP para enviarla a Marel"));
                                        result = null;   
                                    }
                                }
                            }

                            if (!guardoSCP)
                            {
                                salidaSacrificioDal.EliminarDetalleOrdenSacrificio(salidaSacrificioNuevos, organizacionId, osId, 1, ordenInfo.UsuarioCreacion);
                            }
                        }
                    }
                }

                if (!aplicaMarel)
                {
                    using (var transaction = new TransactionScope())
                    {
                        result = ordenSacrificioDal.GuardarOrdenSacrificio(ordenInfo);
                        if (result != null)
                        {
                            result.UsuarioCreacion = ordenInfo.UsuarioCreacion;
                            ordenSacrificioDal.GuardarDetalleOrdenSacrificio(result, detalleOrden);
                            salidaSacrificio = salidaSacrificioDal.ObtenerPorOrdenSacrificioID(result.OrdenSacrificioID);
                        }
                        osId = Convert.ToInt32(result.OrdenSacrificioID);
                        transaction.Complete();
                    }

                    if (salidaSacrificio != null && salidaSacrificio.Count > 0)
                    {
                        foreach (var corralSacrificio in salidaSacrificio)
                        {
                            OrdenSacrificioDetalleInfo detalleValidar =
                                detalleOrden.FirstOrDefault(orden => corralSacrificio.NUM_CORR.Equals(orden.Corral.Codigo.Trim().PadLeft(3, '0'), StringComparison.InvariantCultureIgnoreCase));

                            if (detalleValidar != null)
                            {
                                if (detalleValidar.OrdenSacrificioDetalleID == 0)
                                {
                                    salidaSacrificioNuevos.Add(corralSacrificio);
                                }
                            }
                        }

                        var guardoSCP = false;
                        using (var transaction = new TransactionScope())
                        {
                            if (!salidaSacrificioDal.CrearLista(salidaSacrificioNuevos, conexion))
                            {
                                Logger.Error(new Exception("Ocurrió un error al enviar información al SCP."));
                                result = null;
                            }
                            else
                            {
                                guardoSCP = true;
                                transaction.Complete();                                
                            }
                        }

                        if(!guardoSCP)
                        {
                            salidaSacrificioDal.EliminarDetalleOrdenSacrificio(salidaSacrificioNuevos, organizacionId, osId, 0,ordenInfo.UsuarioCreacion);
                        }
                    }
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        /// <summary>
        /// Obtener la orden de sacrificio del dia
        /// </summary>
        /// <param name="ordenInfo"></param>
        /// <returns></returns>
        internal OrdenSacrificioInfo ObtenerOrdenSacrificioDelDia(OrdenSacrificioInfo ordenInfo)
        {
            OrdenSacrificioInfo result;
            try
            {
                Logger.Info();
                var ordenSacrificioDal = new OrdenSacrificioDAL();
                result = ordenSacrificioDal.ObtenerOrdenSacrificioDelDia(ordenInfo);

                if (result != null)
                {
                    result.DetalleOrden = ordenSacrificioDal.ObtenerDetalleOrdenSacrificio(result, ordenInfo.Activo);
                }
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obteneri dias de engorda 70
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal int ObtnerDiasEngorda70(LoteInfo lote)
        {
            try
            {
                Logger.Info();

                var loteBL = new LoteBL();

                LoteInfo loteAnterior = loteBL.ObtenerLoteAnteriorAnimal(lote.LoteID);

                var animalBL = new AnimalBL();
                var animalesLote = animalBL.ObtenerAnimalesPorLote(lote.OrganizacionID,
                                                                 lote.LoteID);

                if (animalesLote == null)
                {
                    return 0;
                }

                var count = animalesLote.Count;
                double temp = 0D;
                for (int i = 0; i < count; i++)
                {
                    temp += animalesLote[i].FechaEntrada.Ticks / (double)count;
                }
                var average = new DateTime((long)temp);

                var diferenciaFechas = DateTime.Now - average;

                int diasEngordaLote = Convert.ToInt32(diferenciaFechas.TotalDays);
                //Si los dias de engorda son mayor de 115, automaticamente se dejan 101 dias de Engorda en Grano
                if(diasEngordaLote > 115)
                {
                    return 101;
                }

                var loteFiltro = new LoteInfo
                    {
                        LoteID = 0,
                        OrganizacionID = lote.OrganizacionID
                    };

                List<DiasEngordaGranoModel> diasEngorda = loteBL.ObtenerDiasEngordaGrano(loteFiltro); //Obtiene todos los Lotes



                //var repartoDal = new RepartoDAL();
                //var repartoDetalle = repartoDal.ObtenerDetalleRepartoPorLote(lote);
                int diasEngorda70 = 0;

                if(loteAnterior != null && loteAnterior.LoteID > 0)
                {
                    DiasEngordaGranoModel diasEngordaLoteAnterior =
                        diasEngorda.FirstOrDefault(dias => dias.LoteID == loteAnterior.LoteID);
                    if(diasEngordaLoteAnterior != null)
                    {
                        diasEngorda70 += diasEngordaLoteAnterior.DiasGrano;
                    }
                }

                DiasEngordaGranoModel diasEngordaLoteActual =
                    diasEngorda.FirstOrDefault(dias => dias.LoteID == lote.LoteID);
                if(diasEngordaLoteActual != null)
                {
                    diasEngorda70 += diasEngordaLoteActual.DiasGrano;
                }

                //if (repartoDetalle != null)
                //{
                //    var fechaActual = DateTime.Now;

                        
                //    var listaVespertino = repartoDetalle.Where(repartoDetalleInfo => repartoDetalleInfo.TipoServicioID == (int)TipoServicioEnum.Vespertino).ToList();
                //    var listaMatutino = repartoDetalle.Where(repartoDetalleInfo => repartoDetalleInfo.TipoServicioID == (int) TipoServicioEnum.Matutino).ToList();
                //    var diasFormula = listaMatutino.Sum(detalleMatutino => listaVespertino.Count(detalleVespertino => detalleMatutino.RepartoID == detalleVespertino.RepartoID && detalleMatutino.FormulaIDServida == detalleVespertino.FormulaIDServida));

                //    var diferenciaFechasDiasEngorda = fechaActual - lote.FechaInicio;
                //    var diasEngorda = diferenciaFechasDiasEngorda.Days;

                //    if (diasEngorda > 0 && diasFormula > 0)
                //    {
                //        diasEngorda70 = (int) (((float)diasFormula / (float)diasEngorda) * 100);
                //    }
                    
                //}
                return diasEngorda70;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
       
        }

        /// <summary>
        /// Eliminar detalle de la orden de sacrificio
        /// </summary>
        /// <param name="detalleOrdenInfo"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        internal int EliminarDetalleOrdenSacrificio(IList<OrdenSacrificioDetalleInfo> detalleOrdenInfo, int idUsuario)
        {
            int result;
            try
            {
                Logger.Info();
                var ordenSacrificioDal = new OrdenSacrificioDAL();
                result = ordenSacrificioDal.EliminarDetalleOrdenSacrificio(detalleOrdenInfo, idUsuario);

            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Realiza la impresion de la orden de sacrificio
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="detalleOrden"></param>
        internal void ImprimirOrdenDeSacrificio(ImpresionOrdenSacrificioInfo etiquetas, IList<OrdenSacrificioDetalleInfo> detalleOrden )
        {
      
            var reporte = new Document(PageSize.A4.Rotate(), 10, 10, 35, 75);
            try
            {
                const string nombreArchivo = "ordenSacrifio.pdf";
                PdfWriter.GetInstance(reporte, new FileStream(nombreArchivo, FileMode.OpenOrCreate));

                reporte.Open();
                var dirLogo = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\skLogo.png";
                var fuenteDatos = new Font {Size = 6, Color = Color.BLACK};


                var imgSuperior = Image.GetInstance(dirLogo);

                imgSuperior.ScaleAbsolute(90f, 25f);

                float[] medidaCeldas = { 0.75f, 0.75f, 0.75f, 0.75f, 0.75f, 1.20f, 1.0f, 2.25f, 0.85f, 0.85f, 0.85f };
                var table = new PdfPTable(11);
                table.SetWidths(medidaCeldas);
                //

                var cell = new PdfPCell(new Phrase(etiquetas.EtiquetaCabezaEmpresa + "\n" + etiquetas.EtiquetaTitulo, fuenteDatos))
                {
                    Colspan = 12,
                    HorizontalAlignment = 1,
                    Border = 0
                };
                table.AddCell(cell);
                //
                cell = new PdfPCell(imgSuperior) {Padding = 2, Colspan = 5, Border = 0};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 3, Border = 0};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fuenteDatos)) {VerticalAlignment = 0, Border = 0};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("" , fuenteDatos)) {Colspan = 3, VerticalAlignment = 0, Border = 0};
                table.AddCell(cell);
                //

                cell = new PdfPCell(new Phrase(" ", fuenteDatos)) {VerticalAlignment = 0, Colspan = 9, Border = 0};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaFolioIso, fuenteDatos))
                {
                    VerticalAlignment = 0,
                    Colspan = 3,
                    Border = 0
                };
                table.AddCell(cell);

                //

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaFecha, fuenteDatos)) {HorizontalAlignment = 0};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.FechaReporte.ToLongDateString(), fuenteDatos)) {Colspan = 3};
                table.AddCell(cell);

                table.AddCell("");

                cell = new PdfPCell(new Phrase("")) {Colspan = 3, Border = 0};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaPlanta, fuenteDatos)) {Border = 0};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaCabezaEmpresa, fuenteDatos))
                {
                    Border = 0,
                    PaddingBottom = 3,
                    Colspan = 3
                };
                table.AddCell(cell);

                //Titulos

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaNoSalida, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaCorraletas, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaCorral, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaLote, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaCabezas, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaDiasEngorda, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaTipo, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaProveedor, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaEstatus, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaDiasRetiro, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.Etiquetaczas, fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                //
                var total = 0;
                var totalSacrificados = 0;
                var contadorRegistro = 0;
                foreach (var turno in Enum.GetValues(typeof (TurnoEnum)).Cast<TurnoEnum>().ToList())
                {

                    if (contadorRegistro > 0)
                    {
                        cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 12, Padding = 5};
                        table.AddCell(cell);
                        contadorRegistro = 0;
                    }

                    foreach (var orden in detalleOrden)
                    {
                        if (orden.Turno == turno)
                        {
                            cell = new PdfPCell(new Phrase(orden.Corral.Codigo, fuenteDatos)) {HorizontalAlignment = 1};
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(orden.Corraleta.Codigo, fuenteDatos))
                            {
                                HorizontalAlignment = 1
                            };
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(orden.Corral.Codigo, fuenteDatos)) {HorizontalAlignment = 1};
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(orden.Lote.Lote, fuenteDatos)) {HorizontalAlignment = 1};
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(orden.Cabezas.ToString(CultureInfo.InvariantCulture), fuenteDatos))
                            {
                                HorizontalAlignment = 1
                            };
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(orden.DiasEngordaGrano.ToString(CultureInfo.InvariantCulture), fuenteDatos))
                            {
                                HorizontalAlignment = 1
                            };
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(orden.Clasificacion, fuenteDatos)) {HorizontalAlignment = 1};
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(orden.Proveedor.Descripcion, fuenteDatos))
                            {
                                HorizontalAlignment = 1
                            };
                            table.AddCell(cell);

                            
                            cell = new PdfPCell(new Phrase(orden.Estatus, fuenteDatos)) {HorizontalAlignment = 1};
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(orden.DiasRetiro.ToString(CultureInfo.InvariantCulture), fuenteDatos))
                            {
                                HorizontalAlignment = 1
                            };
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(orden.CabezasASacrificar.ToString(CultureInfo.InvariantCulture), fuenteDatos))
                            {
                                HorizontalAlignment = 1
                            };
                            table.AddCell(cell);
                            totalSacrificados += orden.CabezasASacrificar;
                            contadorRegistro++;
                        }
                    }


                }

                var detalleOrdenTmp = new List<OrdenSacrificioDetalleInfo>();
                foreach (var orden in detalleOrden)
                {
                    var contar = detalleOrdenTmp.All(detalleTmp => detalleTmp.Corral.CorralID != orden.Corral.CorralID);
                    detalleOrdenTmp.Add(orden);
                    if (contar)
                    {
                        total += orden.Cabezas;
                    }
                    
                }


                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Border = 0};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaTotal, fuenteDatos));

                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(total.ToString(CultureInfo.InvariantCulture), fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 5, Border = 0};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(totalSacrificados.ToString(CultureInfo.InvariantCulture), fuenteDatos)) {HorizontalAlignment = 1};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 12, Border = 0, Padding = 15};
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaObservaciones + "\n\n" + etiquetas.Observaciones+"\n", fuenteDatos))
                {
                    Colspan = 12,
                    PaddingBottom = 10
                };
                table.AddCell(cell);

                reporte.Add(table);

                reporte.Close();

                SendToPrinter(AppDomain.CurrentDomain.BaseDirectory + nombreArchivo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                reporte.Close();
            }
        }

       /// <summary>
       /// Inviar la pantalla de impresion
       /// </summary>
       /// <param name="archivo"></param>
        private void SendToPrinter(string archivo)
        {
            try
            {
                Process.Start(archivo);
            }
            catch (Exception ex)
            {
                
               Logger.Error(ex);
            }
            
        }

        /// <summary>
        /// Obteneri dias de engorda 70
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        public int ObtnerCabezasDiferentesOrdenes(LoteInfo lote, int ordenSacrificioId)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioDal = new OrdenSacrificioDAL();
                return ordenSacrificioDal.ObtnerCabezasDiferentesOrdenes(lote, ordenSacrificioId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }
        /// <summary>
        /// Metodo para decrementar el animal muerto de la orden se sacrificio.
        /// </summary>
        /// <param name="animalID"></param>
        internal void DecrementarAnimalMuerto(long animalID)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioDal = new OrdenSacrificioDAL();
                ordenSacrificioDal.DecrementarAnimalMuerto(animalID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el numero de cabezas programadas en otras ordenes para el lote determinado
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal bool ValidarLoteOrdenSacrificio(int loteID)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioDal = new OrdenSacrificioDAL();
                return ordenSacrificioDal.ValidarLoteOrdenSacrificio(loteID);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal List<OrdenSacrificioDetalleInfo> DetalleOrden(string fecha, int organizacionId)
        {
            List<OrdenSacrificioDetalleInfo> detalle;
            try
            {
                Logger.Info();
                var ordenSacrificioDal = new OrdenSacrificioDAL();
                detalle = ordenSacrificioDal.DetalleOrden(fecha, organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return detalle;
        }
        
        internal OrdenSacrificioInfo OrdenSacrificioFecha(string fecha, int organizacionId)
        {

            OrdenSacrificioInfo ordenFecha;
            try
            {
                Logger.Info();
                var ordenSacrificioDal = new OrdenSacrificioDAL();
                ordenFecha = ordenSacrificioDal.OrdenSacrificioFecha(fecha, organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return ordenFecha;
        }

        internal static IList<OrdenSacrificioDetalleInfo> ObtenerOrdenSacrificioDetalle(int ordenSacrificioId)
        {
            try
            {
                Logger.Info();
                var ordenSacrificioDal = new OrdenSacrificioDAL();
                return ordenSacrificioDal.ObtenerDetalleOrdenSacrificio(new OrdenSacrificioInfo() { OrdenSacrificioID = ordenSacrificioId }, true);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<string> ConexionOrganizacion(int organizacionId)
        {
            List<string> conexion;
            try
            {
                OrdenSacrificioDAL ordenSacrificioDal = new OrdenSacrificioDAL();
                conexion = ordenSacrificioDal.ConexionOrganizacion(organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return conexion;
        }

        internal List<string> ValidaLoteNoqueado(string con, string xml)
        {
            List<string> listLoteNoqueado;
            try
            {
                OrdenSacrificioDAL ordenDAL = new OrdenSacrificioDAL();
                listLoteNoqueado = ordenDAL.ValidaLoteNoqueado(con, xml);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listLoteNoqueado;
        }

        internal bool ValidaCancelacionCabezasSIAP(string xml, int ordenSacrificioId, int usuarioId)
        {
            bool result;
            try
            {
                OrdenSacrificioDAL ordenDAL = new OrdenSacrificioDAL();
                result = ordenDAL.ValidaCancelacionCabezasSIAP(xml, ordenSacrificioId, usuarioId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        internal bool CancelacionOrdenSacrificioMarel(string xml, int organizacionId)
        {
            bool result;
            try
            {
                var dal = new OrdenSacrificioDAL();
                result = dal.CancelacionOrdenSacrificioMarel(xml,organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        internal bool CancelacionOrdenSacrificioSCP(string xmlOrdenesDetalle, string fechaSacrificio, int organizacionId)
        {
            bool result;
            try
            {
                var dal = new OrdenSacrificioDAL();
                result = dal.CancelacionOrdenSacrificioSCP(xmlOrdenesDetalle, fechaSacrificio, organizacionId);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        internal List<SalidaSacrificioInfo> ConsultarEstatusOrdenSacrificioEnMarel(int organizacionId, string fechaSacrificio, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                var dal = new OrdenSacrificioDAL();
                return dal.ConsultarEstatusOrdenSacrificioEnMarel(organizacionId, fechaSacrificio, detalle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal List<OrdenSacrificioDetalleInfo> ValidarCabezasActualesVsCabezasSacrificar(int organizacionId, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                var dal = new OrdenSacrificioDAL();
                return dal.ValidarCabezasActualesVsCabezasSacrificar(organizacionId, detalle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal List<string> ValidarCorralConLotesActivos(int organizacionId, List<OrdenSacrificioDetalleInfo> detalle)
        {
            try
            {
                var dal = new OrdenSacrificioDAL();
                return dal.ValidarCorralConLotesActivos(organizacionId, detalle);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal List<ControlSacrificioInfo> ObtenerResumenSacrificioScp(string fechaSacrificio, int organizacionId)
        {
            try
            {
                var dal = new OrdenSacrificioDAL();
                return dal.ObtenerResumenSacrificioScp(fechaSacrificio, organizacionId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal List<ControlSacrificioInfo> ObtenerResumenSacrificioSiap(List<ControlSacrificioInfo> resumenSacrificio)
        {
            try
            {
                var dal = new OrdenSacrificioDAL();
                return dal.ObtenerResumenSacrificioSiap(resumenSacrificio);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
