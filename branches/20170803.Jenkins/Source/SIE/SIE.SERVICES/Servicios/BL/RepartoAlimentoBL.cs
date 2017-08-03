using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Transactions;
using SIE.Base.Infos;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class RepartoAlimentoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad RepartoAlimento
        /// </summary>
        /// <param name="reparto"></param>
        /// <param name="repartoDetalle"></param>
        /// <param name="organizacionID"></param>
        public int Guardar(RepartoAlimentoInfo reparto, List<GridRepartosModel> repartoDetalle, int organizacionID)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDAL = new RepartoAlimentoDAL();
                int result = reparto.RepartoAlimentoID;
                var listaRepartosAlimentoDetalle = new List<RepartoAlimentoDetalleInfo>();
                GenerarRepartoAlimentoDetalle(repartoDetalle, listaRepartosAlimentoDetalle, organizacionID, reparto.RepartoAlimentoID);

                using (var transaction = new TransactionScope())
                {
                    if (reparto.RepartoAlimentoID == 0)
                    {
                        result = repartoAlimentoDAL.Crear(reparto);
                    }
                    else
                    {
                        repartoAlimentoDAL.Actualizar(reparto);
                    }
                    listaRepartosAlimentoDetalle.ForEach(detalle =>
                        {
                            detalle.RepartoAlimentoID = result;
                            detalle.UsuarioCreacionID = reparto.UsuarioCreacionID;
                        });
                    var repartoAlimentoDetalleDAL = new RepartoAlimentoDetalleDAL();
                    repartoAlimentoDetalleDAL.Guardar(listaRepartosAlimentoDetalle);

                    if (reparto.ListaTiempoMuerto != null && reparto.ListaTiempoMuerto.Any())
                    {
                        reparto.ListaTiempoMuerto.ForEach(tiempo => tiempo.UsuarioCreacionID = reparto.UsuarioCreacionID);
                        var tiempoMuertoDAL = new TiempoMuertoDAL();
                        tiempoMuertoDAL.GuardarTiempoMuertoReparto(reparto.ListaTiempoMuerto.Where(tiempo => tiempo.TiempoMuertoID == 0).ToList(), result);
                    }

                    transaction.Complete();
                }
                return result;
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
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<RepartoAlimentoInfo> ObtenerPorPagina(PaginacionInfo pagina, RepartoAlimentoInfo filtro)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDAL = new RepartoAlimentoDAL();
                ResultadoInfo<RepartoAlimentoInfo> result = repartoAlimentoDAL.ObtenerPorPagina(pagina, filtro);
                return result;
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
        }

        /// <summary>
        /// Obtiene un lista de RepartoAlimento
        /// </summary>
        /// <returns></returns>
        public IList<RepartoAlimentoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDAL = new RepartoAlimentoDAL();
                IList<RepartoAlimentoInfo> result = repartoAlimentoDAL.ObtenerTodos();
                return result;
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
        }

        /// <summary>
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<RepartoAlimentoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDAL = new RepartoAlimentoDAL();
                IList<RepartoAlimentoInfo> result = repartoAlimentoDAL.ObtenerTodos(estatus);
                return result;
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
        }

        /// <summary>
        /// Obtiene una entidad RepartoAlimento por su Id
        /// </summary>
        /// <param name="repartoAlimentoID">Obtiene una entidad RepartoAlimento por su Id</param>
        /// <returns></returns>
        public RepartoAlimentoInfo ObtenerPorID(int repartoAlimentoID)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDAL = new RepartoAlimentoDAL();
                RepartoAlimentoInfo result = repartoAlimentoDAL.ObtenerPorID(repartoAlimentoID);
                return result;
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
        }

        /// <summary>
        /// Obtiene una entidad RepartoAlimento por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public RepartoAlimentoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDAL = new RepartoAlimentoDAL();
                RepartoAlimentoInfo result = repartoAlimentoDAL.ObtenerPorDescripcion(descripcion);
                return result;
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
        }

        public RepartoAlimentoInfo ConsultarRepartos(FiltroCheckListReparto filtro)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDAL = new RepartoAlimentoDAL();
                RepartoAlimentoInfo result = repartoAlimentoDAL.ConsultarRepartos(filtro);
                if (result == null)
                {
                    return null;
                }
                result.ListaGridRepartos.ToList().ForEach(deta =>
                    {
                        deta.TiempoTotalViaje = ObtenerTiempoTotalViajeConsulta(result.FechaReparto, deta);
                    });

                result.TotalKilosEmbarcados = result.ListaGridRepartos.Sum(rep => rep.KilosEmbarcados);
                result.TotalKilosRepartidos = result.ListaGridRepartos.Sum(rep => rep.KilosRepartidos);
                result.TotalSobrante = result.TotalKilosEmbarcados - result.TotalKilosRepartidos;
                result.TotalTiempoViaje = ObtenerTiempoTotalSuma(result.ListaGridRepartos);
                result.MermaReparto = ObtenerMermaReparto(result.ListaGridRepartos);
                result.Observaciones = ObtenerObservacion(result.ListaGridRepartos);

                return result;
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
        }

        public FiltroGenerarArchivoDataLink GenerarRepartos(FiltroCheckListReparto filtro)
        {
            var repartos = new List<GridRepartosModel>();
            var respuesta = new FiltroGenerarArchivoDataLink();
            try
            {
                Logger.Info();
                var parametroOrganizacionDAL = new ParametroOrganizacionDAL();

                ParametroOrganizacionInfo parametroRutaArchivo =
                    parametroOrganizacionDAL.ObtenerPorOrganizacionIDClaveParametro(filtro.OrganizacionID,
                                                                                    ParametrosEnum.rutaArchivoDatalink.
                                                                                        ToString());
                ParametroOrganizacionInfo parametroNombreArchivo =
                    parametroOrganizacionDAL.ObtenerPorOrganizacionIDClaveParametro(filtro.OrganizacionID,
                                                                                    ParametrosEnum.nombreArchivoDatalink.
                                                                                        ToString());
                if (parametroRutaArchivo == null)
                {
                    respuesta.Mensaje = string.Format(ConstantesBL.MensajeSinParametro, ParametrosEnum.rutaArchivoDatalink.
                                                                                        ToString());
                    return respuesta;
                }

                if (parametroNombreArchivo == null)
                {
                    respuesta.Mensaje = string.Format(ConstantesBL.MensajeSinParametro, ParametrosEnum.nombreArchivoDatalink.
                                                                                        ToString());
                    return respuesta;
                }
                string archivoDataLink = string.Format("{0}{1}", parametroRutaArchivo.Valor,
                                                       parametroNombreArchivo.Valor);
                if (File.Exists(archivoDataLink))
                {
                    string[] valoresArchivo;
                    using (var sr = new StreamReader(archivoDataLink))
                    {
                        valoresArchivo = sr.ReadToEnd().Split('\n');
                    }
                    var datosDataLink = (from s in valoresArchivo where !string.IsNullOrWhiteSpace(s) select CargarDatosArchivo(s)).ToList();
                    GenerarValoresGridReparto(repartos, datosDataLink, filtro);
                    if (!repartos.Any())
                    {
                        respuesta.Mensaje = ConstantesBL.MensajeSinDatosDataLink;
                    }
                    else
                    {
                        respuesta.DatosDataLink = repartos;
                        respuesta.TotalKilosEmbarcados = repartos.Sum(rep => rep.KilosEmbarcados);
                        respuesta.TotalKilosRepartidos = repartos.Sum(rep => rep.KilosRepartidos);
                        respuesta.TotalSobrante = respuesta.TotalKilosEmbarcados - respuesta.TotalKilosRepartidos;
                        respuesta.TotalTiempoViaje = ObtenerTiempoTotalSuma(repartos);
                        respuesta.MermaReparto = ObtenerMermaReparto(repartos);



                        //respuesta.DatosDataLink.ToList().ForEach(deta =>
                        //{
                        //    deta.TiempoTotalViaje = ObtenerTiempoTotalViajeConsulta(filtro.Fecha, deta);
                        //});
                    }
                }
                else
                {
                    respuesta.Mensaje = ConstantesBL.MensajeArchivoNoExiste;
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
            return respuesta;
        }

        private void GenerarValoresGridReparto(List<GridRepartosModel> repartos, List<DatosRepartoDataLinkModel> datosDataLink, FiltroCheckListReparto filtro)
        {
            int orden = 0;
            datosDataLink.ForEach(data =>
            {
                orden++;
                data.OrdenRegistro = orden;
            });



            var datosFiltrados = new List<DatosRepartoDataLinkModel>();
            foreach (var datosRepartoDataLinkModel in datosDataLink)
            {
                if (datosRepartoDataLinkModel.Operador == filtro.OperadorID && datosRepartoDataLinkModel.NumeroCamion.ToUpper().Trim().Equals(filtro.NumeroEconomico.ToUpper().Trim()) && datosRepartoDataLinkModel.TipoServicio == filtro.TipoServicioID)
                {
                    var fechaSplit = datosRepartoDataLinkModel.Fecha.Split('-');
                    var fechaReparto = new DateTime();
                    if (datosRepartoDataLinkModel.FormatoFecha == 0)
                    {
                        fechaReparto = new DateTime(2000 + Convert.ToInt32(fechaSplit[2]), Convert.ToInt32(fechaSplit[0]), Convert.ToInt32(fechaSplit[1]));
                    }
                    if (fechaReparto.Date != filtro.Fecha.Date)
                    {
                        continue;
                    }
                    datosRepartoDataLinkModel.FechaReparto = fechaReparto;
                    datosFiltrados.Add(datosRepartoDataLinkModel);
                }
            }
            if (!datosFiltrados.Any())
            {
                return;
            }
            repartos.AddRange((from data in datosFiltrados.OrderBy(data => data.OrdenRegistro)
                               group data by data.NumeroReparto into agrupado
                               let elementoDatos = agrupado.FirstOrDefault()
                               let kilosEmbarcados = ObtenerKilosEmbarcados(agrupado)
                               let kilosRepartidos = ObtenerKilosRepartidos(agrupado)
                               select new GridRepartosModel
                               {
                                   NumeroTolva = string.Empty,
                                   Servicio = elementoDatos == null ? 0 : elementoDatos.TipoServicio,
                                   Reparto = elementoDatos.NumeroReparto,
                                   RacionFormula = ObtenerRacionFormula(agrupado),
                                   KilosEmbarcados = kilosEmbarcados,
                                   KilosRepartidos = kilosRepartidos,
                                   Sobrante = kilosEmbarcados - kilosRepartidos,
                                   CorralInicio = ObtenerCorralInicio(agrupado),
                                   CorralFinal = ObtenerCorralFinal(agrupado),
                                   HoraInicioReparto = ObtenerHoraInicio(agrupado),
                                   HoraFinalReparto = ObtenerHoraFinal(agrupado),
                                   TiempoTotalViaje = ObtenerTiempoTotalViaje(agrupado.ToList()),
                                   PesoFinal = ObtenerPesoFinal(agrupado.ToList(), kilosRepartidos)
                               }
                                  ).ToList());

            var repartoAlimentoBL = new RepartoAlimentoDAL();
            RepartoAlimentoInfo repartoAlimentoExiste = repartoAlimentoBL.ConsultarRepartos(filtro);
            if (repartoAlimentoExiste != null && (repartoAlimentoExiste.ListaGridRepartos != null && repartoAlimentoExiste.ListaGridRepartos.Any()))
            {
                foreach (var datosReparto in repartos)
                {
                    var reparto =
                        repartoAlimentoExiste.ListaGridRepartos.FirstOrDefault(
                            rep => rep.Reparto == datosReparto.Reparto &&
                                   rep.Servicio == datosReparto.Servicio &&
                                   rep.RacionFormula == datosReparto.RacionFormula);
                    if (reparto == null)
                    {
                        continue;
                    }
                    datosReparto.NumeroTolva = reparto.NumeroTolva;
                }
            }
        }

        private string ObtenerTiempoTotalViaje(List<DatosRepartoDataLinkModel> agrupado)
        {
            var elementoHoraInicio = agrupado.FirstOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("P"));
            var elementoHoraFinal = agrupado.LastOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("P"));
            if (elementoHoraInicio == null || elementoHoraFinal == null)
            {
                return string.Empty;
            }
            var horasInicial = elementoHoraInicio.Hora.Split(':')[0];
            var minutosInicial = elementoHoraInicio.Hora.Split(':')[1];

            var horasFinal = elementoHoraFinal.Hora.Split(':')[0];
            var minutosFinal = elementoHoraFinal.Hora.Split(':')[1];

            var primerFecha =
                elementoHoraInicio.FechaReparto.AddHours(Convert.ToDouble(horasInicial)).AddMinutes(
                    Convert.ToDouble(minutosInicial));

            var ultimaFecha =
                elementoHoraFinal.FechaReparto.AddHours(Convert.ToDouble(horasFinal)).AddMinutes(
                    Convert.ToDouble(minutosFinal));

            TimeSpan duracion = ultimaFecha - primerFecha;
            long durationTicks = Math.Abs(duracion.Ticks / TimeSpan.TicksPerMillisecond);
            long hours = durationTicks / (1000 * 60 * 60);
            long minutes = (durationTicks - (hours * 60 * 60 * 1000)) / (1000 * 60);
            return string.Format("{0}:{1}", hours.ToString("00"), minutes.ToString("00"));
        }

        private string ObtenerHoraInicio(IEnumerable<DatosRepartoDataLinkModel> agrupado)
        {
            var elementoHoraInicio = agrupado.FirstOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("P"));
            if (elementoHoraInicio == null)
            {
                return string.Empty;
            }
            return elementoHoraInicio.Hora;
        }

        private string ObtenerHoraFinal(IEnumerable<DatosRepartoDataLinkModel> agrupado)
        {
            var elementoHoraFinal = agrupado.LastOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("P"));
            if (elementoHoraFinal == null)
            {
                return string.Empty;
            }
            return elementoHoraFinal.Hora;
        }

        private string ObtenerCorralInicio(IEnumerable<DatosRepartoDataLinkModel> agrupado)
        {
            var elementoCorralInicio = agrupado.FirstOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("P"));
            if (elementoCorralInicio == null)
            {
                return string.Empty;
            }
            int codigoValido;
            int.TryParse(elementoCorralInicio.CodigoCorral, out codigoValido);
            if (codigoValido == 0)
            {
                return elementoCorralInicio.CodigoCorral;
            }
            return Math.Abs(codigoValido).ToString(CultureInfo.InvariantCulture);
        }

        private string ObtenerCorralFinal(IEnumerable<DatosRepartoDataLinkModel> agrupado)
        {
            var elementoCorralFinal = agrupado.LastOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("P"));
            if (elementoCorralFinal == null)
            {
                return string.Empty;
            }
            int codigoValido;
            int.TryParse(elementoCorralFinal.CodigoCorral, out codigoValido);
            if (codigoValido == 0)
            {
                return elementoCorralFinal.CodigoCorral;
            }
            return Math.Abs(codigoValido).ToString(CultureInfo.InvariantCulture);
        }

        private int ObtenerKilosRepartidos(IEnumerable<DatosRepartoDataLinkModel> agrupado)
        {
            return
                agrupado.Where(data => !data.IngredienteCorral.ToUpper().Trim().Equals("I")).Sum(
                    data => data.KilosServidos);
        }

        private int ObtenerKilosEmbarcados(IEnumerable<DatosRepartoDataLinkModel> agrupado)
        {
            var elementoKilosEmbarcados =
                agrupado.FirstOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("I"));
            if (elementoKilosEmbarcados == null)
            {
                return 0;
            }
            return elementoKilosEmbarcados.KilosEmbarcados;
        }

        private int ObtenerRacionFormula(IEnumerable<DatosRepartoDataLinkModel> agrupado)
        {
            var elementoRacionFormula =
                agrupado.FirstOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("I"));
            if (elementoRacionFormula == null)
            {
                return 0;
            }
            return elementoRacionFormula.RacionFormula;
        }

        private int ObtenerPesoFinal(List<DatosRepartoDataLinkModel> agrupado, int kilosRepartidos)
        {
            int pesoFinal = 0;
            var elementoCabecero =
                agrupado.FirstOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("I"));
            var elementoRepartoInicio = agrupado.FirstOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("P"));
            var elementoRepartoFinal = agrupado.LastOrDefault(data => data.IngredienteCorral.ToUpper().Trim().Equals("P"));
            if (elementoRepartoInicio == null || elementoRepartoFinal == null || elementoCabecero == null)
            {
                return 0;
            }

            var fechaReparto = elementoCabecero.FechaReparto;

            var horasInicial = elementoCabecero.Hora.Split(':')[0];
            var minutosInicial = elementoCabecero.Hora.Split(':')[1];

            var horasFinal = elementoRepartoInicio.Hora.Split(':')[0];
            var minutosFinal = elementoRepartoInicio.Hora.Split(':')[1];

            var primerFecha =
                fechaReparto.AddHours(Convert.ToDouble(horasInicial)).AddMinutes(
                    Convert.ToDouble(minutosInicial));

            var ultimaFecha =
                fechaReparto.AddHours(Convert.ToDouble(horasFinal)).AddMinutes(
                    Convert.ToDouble(minutosFinal));

            if (primerFecha > ultimaFecha)
            {

                pesoFinal = ((elementoRepartoInicio.PesoBruto + elementoRepartoInicio.KilosServidos) - kilosRepartidos) - elementoRepartoFinal.PesoBruto;
            }
            else
            {
                pesoFinal = (elementoCabecero.PesoBruto - kilosRepartidos) - elementoRepartoFinal.PesoBruto;
            }

            return Math.Abs(pesoFinal);
        }

        private DatosRepartoDataLinkModel CargarDatosArchivo(string lineaDatos)
        {
            var elemento = new DatosRepartoDataLinkModel();
            try
            {
                if (string.IsNullOrWhiteSpace(lineaDatos))
                {
                    return null;
                }

                var propiedades = elemento.GetType().GetProperties();
                foreach (var propInfo in propiedades)
                {
                    dynamic customAttributes = elemento.GetType().GetProperty(propInfo.Name).GetCustomAttributes(typeof(AtributoDataLink), true);
                    if (customAttributes.Length > 0)
                    {
                        for (var indexAtributos = 0; indexAtributos < customAttributes.Length; indexAtributos++)
                        {
                            var atributos = (AtributoDataLink)customAttributes[indexAtributos];
                            var valor = lineaDatos.Substring(atributos.IndiceInicio - 1, atributos.Longitud);
                            int valorDefaultint;
                            int.TryParse(valor, out valorDefaultint);
                            switch (atributos.TipoDato)
                            {
                                case TypeCode.String: propInfo.SetValue(elemento, valor, null);
                                    break;
                                case TypeCode.Int32: propInfo.SetValue(elemento, valorDefaultint, null);
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return elemento;
        }

        private void GenerarRepartoAlimentoDetalle(List<GridRepartosModel> repartoDetalle, List<RepartoAlimentoDetalleInfo> listaRepartosAlimentoDetalle, int organizacionID, int repartoAlimentoID)
        {
            IList<RepartoAlimentoDetalleInfo> listaDetallesGuardadas = new List<RepartoAlimentoDetalleInfo>();
            if (repartoAlimentoID > 0)
            {
                var repartoAlimentoDetalleDAL = new RepartoAlimentoDetalleDAL();
                listaDetallesGuardadas =
                    repartoAlimentoDetalleDAL.ObtenerPorRepartoAlimentoID(repartoAlimentoID);
            }
            var codigosCorral = new List<string>();
            repartoDetalle.ForEach(rep =>
                {
                    codigosCorral.Add(rep.CorralInicio.Trim().PadLeft(3, '0'));
                    codigosCorral.Add(rep.CorralFinal.Trim().PadLeft(3, '0'));
                });
            var corralDAL = new CorralDAL();
            List<CorralInfo> corrales = corralDAL.ObtenerCorralesPorCodigosCorral(codigosCorral, organizacionID);
            if (corrales == null)
            {
                return;
            }
            foreach (var detalle in repartoDetalle)
            {
                if (listaDetallesGuardadas != null && listaDetallesGuardadas.Any())
                {
                    RepartoAlimentoDetalleInfo detalleExiste =
                        listaDetallesGuardadas.FirstOrDefault(
                            deta =>
                            deta.FolioReparto == detalle.Reparto && deta.FormulaIDRacion == detalle.RacionFormula);
                    if (detalleExiste != null)
                    {
                        continue;
                    }
                }
                var repartoAlimentoDetalle = new RepartoAlimentoDetalleInfo();
                repartoAlimentoDetalle.FolioReparto = detalle.Reparto;
                repartoAlimentoDetalle.FormulaIDRacion = detalle.RacionFormula;
                repartoAlimentoDetalle.Tolva = detalle.NumeroTolva.ToString(CultureInfo.InvariantCulture);
                repartoAlimentoDetalle.KilosEmbarcados = detalle.KilosEmbarcados;
                repartoAlimentoDetalle.KilosRepartidos = detalle.KilosRepartidos;
                repartoAlimentoDetalle.Sobrante = detalle.Sobrante;
                repartoAlimentoDetalle.PesoFinal = detalle.PesoFinal;

                CorralInfo corralInicio =
                    corrales.FirstOrDefault(
                        cor => cor.Codigo.ToUpper().Trim().Equals(detalle.CorralInicio.ToUpper().Trim().PadLeft(3, '0')));
                if (corralInicio == null)
                {
                    continue;
                }
                repartoAlimentoDetalle.CorralIDInicio = corralInicio.CorralID;
                CorralInfo corralFinal =
                   corrales.FirstOrDefault(
                       cor => cor.Codigo.ToUpper().Trim().Equals(detalle.CorralFinal.ToUpper().Trim().PadLeft(3, '0')));
                if (corralFinal == null)
                {
                    continue;
                }
                repartoAlimentoDetalle.CorralIDFinal = corralFinal.CorralID;
                repartoAlimentoDetalle.HoraRepartoInicio = detalle.HoraInicioReparto;
                repartoAlimentoDetalle.HoraRepartoFinal = detalle.HoraFinalReparto;
                repartoAlimentoDetalle.Observaciones = detalle.Observaciones;

                listaRepartosAlimentoDetalle.Add(repartoAlimentoDetalle);
            }
        }

        private string ObtenerTiempoTotalViajeConsulta(DateTime fechaReparto, GridRepartosModel reparto)
        {
            var horasInicial = reparto.HoraInicioReparto.Split(':')[0];
            var minutosInicial = reparto.HoraInicioReparto.Split(':')[1];

            var horasFinal = reparto.HoraFinalReparto.Split(':')[0];
            var minutosFinal = reparto.HoraFinalReparto.Split(':')[1];

            var primerFecha =
                fechaReparto.AddHours(Convert.ToDouble(horasInicial)).AddMinutes(
                    Convert.ToDouble(minutosInicial));

            var ultimaFecha =
                fechaReparto.AddHours(Convert.ToDouble(horasFinal)).AddMinutes(
                    Convert.ToDouble(minutosFinal));

            TimeSpan duracion = ultimaFecha - primerFecha;
            long durationTicks = Math.Abs(duracion.Ticks / TimeSpan.TicksPerMillisecond);
            long hours = durationTicks / (1000 * 60 * 60);
            long minutes = (durationTicks - (hours * 60 * 60 * 1000)) / (1000 * 60);
            return string.Format("{0}:{1}", hours.ToString("00"), minutes.ToString("00"));
        }

        private string ObtenerTiempoTotalSuma(IEnumerable<GridRepartosModel> repartos)
        {
            int horas = 0;
            int minutos = 0;
            foreach (var gridRepartosModel in repartos)
            {
                int horasTiempo = Convert.ToInt32(gridRepartosModel.TiempoTotalViaje.Split(':')[0]);
                horas += horasTiempo;

                int minutosTiempo = Convert.ToInt32(gridRepartosModel.TiempoTotalViaje.Split(':')[1]);
                minutos += minutosTiempo;
            }

            decimal minutosFinal = Convert.ToDecimal(minutos) / 60;

            horas += Convert.ToInt32(Math.Abs(minutosFinal));

            decimal minutosSobrantes = minutos % 60;
            return string.Format("{0}:{1}", horas.ToString("00"), minutosSobrantes.ToString("00"));
        }

        public List<RepartoAlimentoInfo> ImprimirRepartos(FiltroCheckListReparto filtro)
        {
            try
            {
                Logger.Info();
                var repartoAlimentoDAL = new RepartoAlimentoDAL();
                List<RepartoAlimentoInfo> result = repartoAlimentoDAL.ImprimirRepartos(filtro);
                if (result == null)
                {
                    return null;
                }
                foreach (var repartoAlimentoInfo in result)
                {
                    repartoAlimentoInfo.ListaGridRepartos.ToList().ForEach(deta =>
                    {
                        deta.TiempoTotalViaje = ObtenerTiempoTotalViajeConsulta(repartoAlimentoInfo.FechaReparto, deta);
                    });
                    repartoAlimentoInfo.TotalKilosEmbarcados = repartoAlimentoInfo.ListaGridRepartos.Sum(rep => rep.KilosEmbarcados);
                    repartoAlimentoInfo.TotalKilosRepartidos = repartoAlimentoInfo.ListaGridRepartos.Sum(rep => rep.KilosRepartidos);
                    repartoAlimentoInfo.TotalSobrante = repartoAlimentoInfo.TotalKilosEmbarcados - repartoAlimentoInfo.TotalKilosRepartidos;
                    repartoAlimentoInfo.TotalTiempoViaje = ObtenerTiempoTotalSuma(repartoAlimentoInfo.ListaGridRepartos);
                    repartoAlimentoInfo.MermaReparto = ObtenerMermaReparto(repartoAlimentoInfo.ListaGridRepartos);
                    repartoAlimentoInfo.Observaciones = ObtenerObservacion(repartoAlimentoInfo.ListaGridRepartos);
                }
                return result;
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
        }

        private int ObtenerMermaReparto(IEnumerable<GridRepartosModel> repartos)
        {
            int mermaFinal = 0;
            foreach (var gridRepartosModel in repartos)
            {
                int sobrante = gridRepartosModel.KilosEmbarcados - gridRepartosModel.KilosRepartidos;

                int merma = sobrante - gridRepartosModel.PesoFinal;
                if (merma > 0)
                {
                    mermaFinal += merma;
                }
            }
            return Math.Abs(mermaFinal);
        }

        private string ObtenerObservacion(IEnumerable<GridRepartosModel> repartos)
        {
            var datos = (from repa in repartos
                         group repa by repa.Observaciones
                             into agrupado
                             select agrupado.Key).ToArray();
            return string.Join("; ", datos);
        }

    }
}

