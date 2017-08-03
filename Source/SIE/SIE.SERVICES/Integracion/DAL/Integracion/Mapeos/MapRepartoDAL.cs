using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapRepartoDAL
    {
        /// <summary>
        /// Obtiene un reparto por id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static RepartoInfo ObtenerPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var result =
                    (from info in dt.AsEnumerable()
                        select
                            new RepartoInfo
                            {
                                RepartoID = info.Field<long>("RepartoID"),
                                OrganizacionID = info.Field<int>("OrganizacionID"),
                                //LoteID = info.Field<int>("LoteID"),
                                LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                                Fecha = info.Field<DateTime>("Fecha"),
                                PesoInicio = info.Field<int>("PesoInicio"),
                                PesoProyectado = info.Field<int>("PesoProyectado"),
                                DiasEngorda = info.Field<int>("DiasEngorda"),
                                PesoRepeso = info.Field<int>("PesoRepeso")
                            }).First();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los datos del reparto por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static RepartoInfo ObtenerPorLote(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var repartoDal = new RepartoDAL();
                var result =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoInfo
                         {
                             RepartoID = info.Field<long>("RepartoID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Corral = new CorralInfo { CorralID = info.Field<int>("CorralID") },
                             LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                             Fecha = info.Field<DateTime>("Fecha"),
                             PesoInicio = info.Field<int>("PesoInicio"),
                             PesoProyectado = info.Field<int>("PesoProyectado"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             PesoRepeso = info.Field<int>("PesoRepeso"),
                             DetalleReparto = repartoDal.ObtenerDetalle(new RepartoInfo
                                                                         {
                                                                             RepartoID = info.Field<long>("RepartoID"), 
                                                                             OrganizacionID = info.Field<int>("OrganizacionID")
                                                                         }),
                         }).First();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene los datos del detalle de un reparto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<RepartoDetalleInfo> ObtenerDetalle(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<RepartoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new RepartoDetalleInfo
                     {

                         RepartoDetalleID = info.Field<long>("RepartoDetalleID"),
                         RepartoID = info.Field<long>("RepartoID"),
                         TipoServicioID = info.Field<int>("TipoServicioID"),
                         FormulaIDProgramada = info.Field<int>("FormulaIDProgramada"),
                         FormulaIDServida = info["FormulaIDServida"] == DBNull.Value?0 : info.Field<int>("FormulaIDServida"),
                         TipoFormula = info["TipoFormulaID"]==DBNull.Value ? 0 : info.Field<int>("TipoFormulaID"),
                         CantidadProgramada = info.Field<int>("CantidadProgramada"),
                         CantidadServida = info.Field<int>("CantidadServida"),
                         HoraReparto = info.Field<string>("HoraReparto"),
                         CostoPromedio = info.Field<decimal>("CostoPromedio"),
                         Importe = info.Field<decimal>("Importe"),
                         Servido = info.Field<bool>("Servido"),
                         Cabezas = info.Field<int>("Cabezas"),
                         Ajuste = info.Field<bool>("Ajuste"),
                         EstadoComederoID = info.Field<int>("EstadoComederoID"),
                         CamionRepartoID = info["CamionRepartoID"] == DBNull.Value ? 0: info.Field<int>("CamionRepartoID"),
                         Observaciones = info.Field<string>("Observaciones"),
                     }).ToList();


                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene los dias de retiro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerDiasRetiro(DataSet ds)
        {
            var diasRetiro = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    diasRetiro = Convert.ToInt32(dr["DiasRetiro"]);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return diasRetiro;

        }
        /// <summary>
        /// Obtiene el listado de repartos del operador logueado.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static RepartoInfo ObtenerRepartoPorOperadorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var repartoDal = new RepartoDAL();

                var result =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoInfo
                         {
                             RepartoID = info.Field<long>("RepartoID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             //LoteID = info.Field<int>("LoteID"),
                             LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                             Fecha = info.Field<DateTime>("Fecha"),
                             PesoInicio = info.Field<int>("PesoInicio"),
                             PesoProyectado = info.Field<int>("PesoProyectado"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             PesoRepeso = info.Field<int>("PesoRepeso"),
                             DetalleReparto = repartoDal.ObtenerDetalle(new RepartoInfo
                             {
                                 RepartoID = info.Field<long>("RepartoID"),
                                 OrganizacionID = info.Field<int>("OrganizacionID")
                             })
                         }).First();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene el total de repartos por operador id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerTotalRepartosPorOperadorId(DataSet ds)
        {
            var totalRepartos = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    totalRepartos = Convert.ToInt32(dr["Total"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return totalRepartos;

        }
        /// <summary>
        /// Obtiene la informacion de los tipos de servicios
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoServicioInfo> ObtenerTiposDeServicios(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista =
                    (from info in dt.AsEnumerable()
                     select new TipoServicioInfo
                     {
                         TipoServicioId = info.Field<int>("TipoServicioID"),
                         HoraInicio = info.Field<string>("HoraInicio"),
                         HoraFin = info.Field<string>("HoraFin"),
                         Descripcion = info.Field<string>("Descripcion")
                     }).ToList();


                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene el reparto de la fecha actual
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<RepartoInfo> ObtenerRepartoActual(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista =
                    (from info in dt.AsEnumerable()
                     select new RepartoInfo
                     {
                         RepartoID = info.Field<long>("RepartoID"),
                         OrganizacionID = info.Field<int>("OrganizacionID"),
                         //LoteID = info.Field<int>("LoteID"),
                         LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                         Fecha = info.Field<DateTime>("Fecha"),
                         PesoInicio = info.Field<int>("PesoInicio"),
                         PesoProyectado = info.Field<int>("PesoProyectado"),
                         DiasEngorda = info.Field<int>("DiasEngorda"),
                         PesoRepeso = info.Field<int>("PesoRepeso"),
                     }).ToList();


                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene el consumo total del dia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerConsumoTotalDia(DataSet ds)
        {
            var totalConsumo = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    totalConsumo = dr["ConsumoTotal"] == DBNull.Value ? 0 :Convert.ToInt32(dr["ConsumoTotal"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return totalConsumo;

        }
        /// <summary>
        /// Obtiene el peso de llegada por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OrdenRepartoAlimentacionInfo ObtenerPesoLlegada(DataSet ds)
        {
  
            var ordenReparto = new OrdenRepartoAlimentacionInfo();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    ordenReparto.PesoLlegada = dr["PesoLlegada"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PesoLlegada"]);
                    ordenReparto.TotalAnimalesPeso = dr["Total"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Total"]);
                    ordenReparto.PesoActual = dr["Peso"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Peso"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return ordenReparto;

        }
        /// <summary>
        /// Obtiene los id resultado de los insert a orden reparto y orden reparto detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OrdenRepartoAlimentacionInfo ObtenerGenerarOrdenReparto(DataSet ds)
        {

            var ordenReparto = new OrdenRepartoAlimentacionInfo();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    ordenReparto.Reparto = new RepartoInfo
                    {
                        RepartoID = dr["RepartoID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["RepartoID"])
                    };
                    ordenReparto.DetalleOrdenReparto = new RepartoDetalleInfo
                    {
                        RepartoDetalleID = dr["RepartoDetalleID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["RepartoDetalleID"])
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return ordenReparto;

        }
       
        /// <summary>
        /// obtiene una orden de reparto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static RepartoInfo ObtenerRepartoPorFecha(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var resultado =
                    (from info in dt.AsEnumerable()
                     select new RepartoInfo
                     {
                         RepartoID = info.Field<long>("RepartoID"),
                         OrganizacionID = info.Field<int>("OrganizacionID"),
                         //LoteID = info.Field<int>("LoteID"),
                         LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                         Fecha = info.Field<DateTime>("Fecha"),
                         PesoInicio = info.Field<int>("PesoInicio"),
                         PesoProyectado = info.Field<int>("PesoProyectado"),
                         DiasEngorda = info.Field<int>("DiasEngorda"),
                         PesoRepeso = info.Field<int>("PesoRepeso"),
                     }).First();

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene los datos del avance
        /// </summary>
        /// <param name="ds">Data set con los datos</param>
        /// <returns></returns>
        internal static RepartoAvanceInfo ObtenerAvanceReparto(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var resultado =
                    (from info in dt.AsEnumerable()
                     select new RepartoAvanceInfo
                     {
                         UsuarioID = info.Field<int>("UsuarioID"),
                         Seccion = info.Field<string>("Seccion"),
                         TotalCorrales = info.Field<int>("TotalCorrales"),
                         TotalCorralesSeccion = info.Field<int>("TotalCorralesSeccion"),
                         TotalCorralesProcesados = info.Field<int>("TotalCorralesProcesados"),
                         TotalCorralesProcesadosSeccion = info.Field<int>("TotalCorralesProcesadosSeccion"),
                         PorcentajeSeccion = info.Field<int>("PorcentajeSeccion"),
                         PorcentajeTotal = info.Field<int>("PorcentajeTotal"),
                     }).First();


                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene el id generado del lote de distribucion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerIdLoteDistribucion(DataSet ds)
        {
            var loteDistribucion = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    loteDistribucion = Convert.ToInt32(dr["LoteDistribucionID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return loteDistribucion;

        }
        /// <summary>
        /// Obtener detalle por dia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<RepartoDetalleInfo> ObtenerDetallePorDia(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<RepartoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new RepartoDetalleInfo
                     {

                         RepartoDetalleID = info.Field<long>("RepartoDetalleID"),
                         RepartoID = info.Field<long>("RepartoID"),
                         TipoServicioID = info.Field<int>("TipoServicioID"),
                         FormulaIDProgramada = info.Field<int>("FormulaIDProgramada"),
                         FormulaIDServida = info["FormulaIDServida"] == DBNull.Value ? 0 : info.Field<int>("FormulaIDServida"),
                         CantidadProgramada = info.Field<int>("CantidadProgramada"),
                         CantidadServida = info.Field<int>("CantidadServida"),
                         HoraReparto = info.Field<string>("HoraReparto"),
                         CostoPromedio = info.Field<decimal>("CostoPromedio"),
                         Importe = info.Field<decimal>("Importe"),
                         Servido = info.Field<bool>("Servido"),
                         Cabezas = info.Field<int>("Cabezas"),
                         EstadoComederoID = info.Field<int>("EstadoComederoID"),
                         CamionRepartoID = info["CamionRepartoID"] == DBNull.Value ? 0 : info.Field<int>("CamionRepartoID"),
                         Observaciones = info.Field<string>("Observaciones"),
                     }).ToList();


                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<RepartoDetalleInfo> ObtenerRepartoDetallePorOrganizacionID(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                List<RepartoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new RepartoDetalleInfo
                     {

                         RepartoDetalleID = info.Field<long>("RepartoDetalleID"),
                         RepartoID = info.Field<long>("RepartoID"),
                         TipoServicioID = info.Field<int>("TipoServicioID"),
                         FormulaIDProgramada = info.Field<int>("FormulaIDProgramada"),
                         FormulaIDServida = info["FormulaIDServida"] == DBNull.Value ? 0 : info.Field<int>("FormulaIDServida"),
                         CantidadProgramada = info.Field<int>("CantidadProgramada"),
                         CantidadServida = info.Field<int>("CantidadServida"),
                         HoraReparto = info.Field<string>("HoraReparto"),
                         CostoPromedio = info.Field<decimal>("CostoPromedio"),
                         Importe = info.Field<decimal>("Importe"),
                         Servido = info.Field<bool>("Servido"),
                         Ajuste = info.Field<bool>("Ajuste"),
                         Prorrateo = info.Field<bool>("Prorrateo"),
                         Cabezas = info.Field<int>("Cabezas"),
                         EstadoComederoID = info.Field<int>("EstadoComederoID"),
                         CamionRepartoID = info["CamionRepartoID"] == DBNull.Value ? 0 : info.Field<int>("CamionRepartoID"),
                         Observaciones = info.Field<string>("Observaciones"),
                         AlmacenMovimientoID = info.Field<long?>("AlmacenMovimientoID") == null ? 0 : info.Field<long>("AlmacenMovimientoID"),
                         FechaReparto = info.Field<DateTime>("Fecha")
                     }).ToList();


                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Mapear el resultado de El reparto por tipo de servicio de forraje
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<RepartoDetalleInfo> ObtenerRepartoPorTipoServicioFecha(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                List<RepartoDetalleInfo> lista =
                    (from info in dt.AsEnumerable()
                     select new RepartoDetalleInfo
                     {

                         RepartoID = info.Field<long>("RepartoID"),
                         FormulaIDServida = info["FormulaIDServida"] == DBNull.Value ? 0 : info.Field<int>("FormulaIDServida"),
                         CantidadServida = info.Field<int>("CantidadServida"),
                         //Fec = info.Field<int>("CantidadServida")
                     }).ToList();


                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los datos para generar
        /// la poliza de consumo de alimento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PolizaConsumoAlimentoModel ObtenerDatosPolizaConsumo(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dtReparto = ds.Tables[ConstantesDAL.DtDatos];
                var dtProduccion = ds.Tables[ConstantesDAL.DtDetalle];

                var polizaConsumo = new PolizaConsumoAlimentoModel
                                        {
                                            Reparto = new RepartoInfo
                                                          {
                                                              DetalleReparto = (from info in dtReparto.AsEnumerable()
                                                                                select new RepartoDetalleInfo
                                                                                           {
                                                                                               Importe =
                                                                                                   info.Field<decimal>(
                                                                                                       "Importe"),
                                                                                               FormulaIDServida =
                                                                                                   info.Field<int>(
                                                                                                       "FormulaIDServida"),
                                                                                               OrganizacionID =
                                                                                                   info.Field<int>(
                                                                                                       "OrganizacionID"),
                                                                                           }).ToList()
                                                          },
                                            ProduccionFormula = new ProduccionFormulaInfo
                                                                    {
                                                                        ProduccionFormulaDetalle =
                                                                            (from produccion in
                                                                                 dtProduccion.AsEnumerable()
                                                                             select new ProduccionFormulaDetalleInfo
                                                                                        {
                                                                                            ProduccionFormulaId =
                                                                                                produccion.Field<int>(
                                                                                                    "FormulaID"),
                                                                                            Producto = new ProductoInfo
                                                                                                           {
                                                                                                               ProductoId
                                                                                                                   =
                                                                                                                   produccion
                                                                                                                   .
                                                                                                                   Field
                                                                                                                   <int>
                                                                                                                   ("ProductoID"),
                                                                                                           },
                                                                                            OrganizacionID =
                                                                                                produccion.Field<int>(
                                                                                                    "OrganizacionID"),
                                                                                            AlmacenID =
                                                                                                produccion.Field<int>(
                                                                                                    "AlmacenID")
                                                                                        }).ToList()
                                                                    }
                                        };
                return polizaConsumo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un reparto con su detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<RepartoInfo> ObtenerRepartoPorFechaCompleto(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var resultado =
                    (from info in dt.AsEnumerable()
                     select new RepartoInfo
                                {
                                    RepartoID = info.Field<long>("RepartoID"),
                                    OrganizacionID = info.Field<int>("OrganizacionID"),
                                    //LoteID = info.Field<int>("LoteID"),
                                    LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                                    Fecha = info.Field<DateTime>("Fecha"),
                                    PesoInicio = info.Field<int>("PesoInicio"),
                                    PesoProyectado = info.Field<int>("PesoProyectado"),
                                    DiasEngorda = info.Field<int>("DiasEngorda"),
                                    PesoRepeso = info.Field<int>("PesoRepeso"),
                                }).ToList();
                if (resultado != null)
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    resultado.ForEach(detalle =>
                                          {
                                              detalle.DetalleReparto = (from info in dt.AsEnumerable()
                                                                        where
                                                                            info.Field<long>("RepartoID") ==
                                                                            detalle.RepartoID
                                                                        select new RepartoDetalleInfo
                                                                                   {
                                                                                       RepartoDetalleID =
                                                                                           info.Field<long>(
                                                                                               "RepartoDetalleID"),
                                                                                       RepartoID =
                                                                                           info.Field<long>("RepartoID"),
                                                                                       TipoServicioID =
                                                                                           info.Field<int>(
                                                                                               "TipoServicioID"),
                                                                                       FormulaIDProgramada =
                                                                                           info.Field<int>(
                                                                                               "FormulaIDProgramada"),
                                                                                       FormulaIDServida =
                                                                                           info["FormulaIDServida"] ==
                                                                                           DBNull.Value
                                                                                               ? 0
                                                                                               : info.Field<int>(
                                                                                                   "FormulaIDServida"),
                                                                                       TipoFormula =
                                                                                           info["TipoFormulaID"] ==
                                                                                           DBNull.Value
                                                                                               ? 0
                                                                                               : info.Field<int>(
                                                                                                   "TipoFormulaID"),
                                                                                       CantidadProgramada =
                                                                                           info.Field<int>(
                                                                                               "CantidadProgramada"),
                                                                                       CantidadServida =
                                                                                           info.Field<int>(
                                                                                               "CantidadServida"),
                                                                                       HoraReparto =
                                                                                           info.Field<string>(
                                                                                               "HoraReparto"),
                                                                                       CostoPromedio =
                                                                                           info.Field<decimal>(
                                                                                               "CostoPromedio"),
                                                                                       Importe =
                                                                                           info.Field<decimal>("Importe"),
                                                                                       Servido =
                                                                                           info.Field<bool>("Servido"),
                                                                                       Cabezas =
                                                                                           info.Field<int>("Cabezas"),
                                                                                       EstadoComederoID =
                                                                                           info.Field<int>(
                                                                                               "EstadoComederoID"),
                                                                                       CamionRepartoID =
                                                                                           info["CamionRepartoID"] ==
                                                                                           DBNull.Value
                                                                                               ? 0
                                                                                               : info.Field<int>(
                                                                                                   "CamionRepartoID"),
                                                                                       Observaciones =
                                                                                           info.Field<string>(
                                                                                               "Observaciones"),
                                                                                   }).ToList();
                                          });
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// obtiene una orden de reparto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static RepartoInfo ObtenerRepartoPorFechaCorral(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var resultado =
                    (from info in dt.AsEnumerable()
                     select new RepartoInfo
                     {
                         RepartoID = info.Field<long>("RepartoID"),
                         OrganizacionID = info.Field<int>("OrganizacionID"),
                         //LoteID = info.Field<int>("LoteID"),
                         LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                         Corral = new CorralInfo()
                             {
                                 CorralID = info.Field<int>("CorralID")
                             },
                         Fecha = info.Field<DateTime>("Fecha"),
                         PesoInicio = info.Field<int>("PesoInicio"),
                         PesoProyectado = info.Field<int>("PesoProyectado"),
                         DiasEngorda = info.Field<int>("DiasEngorda"),
                         PesoRepeso = info.Field<int>("PesoRepeso"),
                     }).First();


                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los datos del reparto por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<RepartoInfo> ObtenerPorRepartosID(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var repartoDal = new RepartoDAL();
                var result =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoInfo
                         {
                             RepartoID = info.Field<long>("RepartoID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Corral = new CorralInfo { CorralID = info.Field<int>("CorralID") },
                             LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                             Fecha = info.Field<DateTime>("Fecha"),
                             PesoInicio = info.Field<int>("PesoInicio"),
                             PesoProyectado = info.Field<int>("PesoProyectado"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             PesoRepeso = info.Field<int>("PesoRepeso"),
                             DetalleReparto = repartoDal.ObtenerDetalle(new RepartoInfo
                             {
                                 RepartoID = info.Field<long>("RepartoID"),
                                 OrganizacionID = info.Field<int>("OrganizacionID")
                             }),
                         }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los datos del reparto por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<RepartoInfo> ObtenerRepartosAjustados(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];
                
                var result =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoInfo
                         {
                             RepartoID = info.Field<long>("RepartoID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Corral = new CorralInfo { CorralID = info.Field<int>("CorralID") },
                             LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                             Fecha = info.Field<DateTime>("Fecha"),
                             PesoInicio = info.Field<int>("PesoInicio"),
                             PesoProyectado = info.Field<int>("PesoProyectado"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             PesoRepeso = info.Field<int>("PesoRepeso"),
                             DetalleReparto = (from detalle in dtDetalle.AsEnumerable()
                                                   where detalle.Field<long>("RepartoID") == info.Field<long>("RepartoID")
                                                   select new RepartoDetalleInfo
                                                       {
                                                           RepartoDetalleID = detalle.Field<long>("RepartoDetalleID"),
                                                           RepartoID = detalle.Field<long>("RepartoID"),
                                                           TipoServicioID = detalle.Field<int>("TipoServicioID"),
                                                           FormulaIDProgramada = detalle.Field<int>("FormulaIDProgramada"),
                                                           FormulaIDServida = detalle["FormulaIDServida"] == DBNull.Value ? 0 : detalle.Field<int>("FormulaIDServida"),
                                                           CantidadProgramada = detalle.Field<int>("CantidadProgramada"),
                                                           CantidadServida = detalle["CantidadServida"] == DBNull.Value ? 0 : detalle.Field<int>("CantidadServida"),
                                                           HoraReparto = detalle.Field<string>("HoraReparto"),
                                                           CostoPromedio = detalle.Field<decimal>("CostoPromedio"),
                                                           Importe = detalle.Field<decimal>("Importe"),
                                                           Servido = detalle.Field<bool>("Servido"),
                                                           Cabezas = detalle.Field<int>("Cabezas"),
                                                           Ajuste = detalle.Field<bool>("Ajuste"),
                                                           EstadoComederoID = detalle.Field<int>("EstadoComederoID"),
                                                           CamionRepartoID = detalle["CamionRepartoID"] == DBNull.Value ? 0 : detalle.Field<int>("CamionRepartoID"),
                                                           Observaciones = detalle.Field<string>("Observaciones"),
                                                       }).ToList()
                         }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de repartos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<RepartoInfo> ObtenerRepartosPorLoteOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var result =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoInfo
                         {
                             RepartoID = info.Field<long>("RepartoID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             LoteID = info.Field<int>("LoteID"),
                             Fecha = info.Field<DateTime>("Fecha"),
                             PesoInicio = info.Field<int>("PesoInicio"),
                             PesoProyectado = info.Field<int>("PesoProyectado"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             PesoRepeso = info.Field<int>("PesoRepeso"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             DetalleReparto = new List<RepartoDetalleInfo>
                                                      {
                                                          new RepartoDetalleInfo
                                                              {
                                                                  RepartoDetalleID =
                                                                      info.Field<long>("RepartoDetalleID"),
                                                                  TipoServicioID = info.Field<int>("TipoServicioID"),
                                                                  FormulaIDProgramada =
                                                                      info.Field<int>("FormulaIDProgramada"),
                                                                  FormulaIDServida = info.Field<int>("FormulaIDServida"),
                                                                  TipoFormula = info.Field<int>("TipoFormulaID"),
                                                                  CantidadProgramada =
                                                                      info.Field<int>("CantidadProgramada"),
                                                                  CantidadServida = info.Field<int>("CantidadServida"),
                                                                  HoraReparto = info.Field<string>("HoraReparto"),
                                                                  CostoPromedio = info.Field<decimal>("CostoPromedio"),
                                                                  Importe = info.Field<decimal>("Importe"),
                                                                  Cabezas = info.Field<int>("Cabezas"),
                                                                  EstadoComederoID = info.Field<int>("EstadoComederoID"),
                                                                  CamionRepartoID = info.Field<int?>("CamionRepartoID") != null ? info.Field<int>("CamionRepartoID") : 0,
                                                                  Ajuste = info.Field<bool>("Ajuste"),
                                                                  Prorrateo = info.Field<bool>("Prorrateo"),
                                                                  AlmacenMovimientoID =
                                                                      info.Field<long?>("AlmacenMovimientoID") ?? 0,
                                                              }
                                                      }
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el peso de llegada por lote
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<OrdenRepartoAlimentacionInfo> ObtenerPesoLlegadaXML(DataSet ds)
        {

            var ordenRepartoLista = new List<OrdenRepartoAlimentacionInfo>();

            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    var ordenReparto = new OrdenRepartoAlimentacionInfo();
                    ordenReparto.Lote = new LoteInfo
                                            {LoteID = dr["LoteID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["LoteID"])};
                    ordenReparto.PesoLlegada = dr["PesoLlegada"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PesoLlegada"]);
                    ordenReparto.TotalAnimalesPeso = dr["Total"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Total"]);
                    ordenReparto.PesoActual = dr["Peso"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Peso"]);
                    ordenRepartoLista.Add(ordenReparto);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return ordenRepartoLista;

        }

        /// <summary>
        /// obtiene una orden de reparto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<RepartoInfo> ObtenerRepartoPorFechaXML(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var dtDetalle = ds.Tables[ConstantesDAL.DtDetalle];

                var resultado =
                    (from info in dt.AsEnumerable()
                     select new RepartoInfo
                     {
                         RepartoID = info.Field<long>("RepartoID"),
                         OrganizacionID = info.Field<int>("OrganizacionID"),
                         LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                         Fecha = info.Field<DateTime>("Fecha"),
                         PesoInicio = info.Field<int>("PesoInicio"),
                         PesoProyectado = info.Field<int>("PesoProyectado"),
                         DiasEngorda = info.Field<int>("DiasEngorda"),
                         PesoRepeso = info.Field<int>("PesoRepeso"),
                     }).ToList();

                var detalle =
                   (from info in dtDetalle.AsEnumerable()
                    select new RepartoDetalleInfo
                    {
                        RepartoDetalleID = info.Field<long>("RepartoDetalleID"),
                        RepartoID = info.Field<long>("RepartoID"),
                        TipoServicioID = info.Field<int>("TipoServicioID"),
                        FormulaIDProgramada = info.Field<int>("FormulaIDProgramada"),
                        FormulaIDServida = info["FormulaIDServida"] == DBNull.Value ? 0 : info.Field<int>("FormulaIDServida"),
                        TipoFormula = info["TipoFormulaID"] == DBNull.Value ? 0 : info.Field<int>("TipoFormulaID"),
                        CantidadProgramada = info.Field<int>("CantidadProgramada"),
                        CantidadServida = info.Field<int>("CantidadServida"),
                        HoraReparto = info.Field<string>("HoraReparto"),
                        CostoPromedio = info.Field<decimal>("CostoPromedio"),
                        Importe = info.Field<decimal>("Importe"),
                        Servido = info.Field<bool>("Servido"),
                        Cabezas = info.Field<int>("Cabezas"),
                        Ajuste = info.Field<bool>("Ajuste"),
                        EstadoComederoID = info.Field<int>("EstadoComederoID"),
                        CamionRepartoID = info["CamionRepartoID"] == DBNull.Value ? 0 : info.Field<int>("CamionRepartoID"),
                        Observaciones = info.Field<string>("Observaciones"),
                    }).ToList();

                if (!resultado.Any())
                {
                    return null;
                }
                foreach (var reparto in resultado)
                {
                    List<RepartoDetalleInfo> detallesReparto =
                        detalle.Where(det => det.RepartoID == reparto.RepartoID).ToList();
                    if(detallesReparto.Any())
                    {
                        reparto.DetalleReparto = detallesReparto;
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene el consumo total del dia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<ConsumoTotalCorralModel> ObtenerConsumoTotalDiaXML(DataSet ds)
        {
            var listaConsumo = new List<ConsumoTotalCorralModel>();
            ConsumoTotalCorralModel totalConsumo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    totalConsumo = new ConsumoTotalCorralModel();
                    totalConsumo.TotalConsumo = dr["ConsumoTotal"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ConsumoTotal"]);
                    totalConsumo.CorralID = dr["CorralID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CorralID"]);
                    listaConsumo.Add(totalConsumo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaConsumo;

        }

        /// <summary>
        /// Obtiene una lista de repartos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<RepartoInfo> ObtenerRepartosPorCorralOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var result =
                    (from info in dt.AsEnumerable()
                     select
                         new RepartoInfo
                         {
                             RepartoID = info.Field<long>("RepartoID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                             Fecha = info.Field<DateTime>("Fecha"),
                             PesoInicio = info.Field<int>("PesoInicio"),
                             PesoProyectado = info.Field<int>("PesoProyectado"),
                             DiasEngorda = info.Field<int>("DiasEngorda"),
                             PesoRepeso = info.Field<int>("PesoRepeso"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             DetalleReparto = new List<RepartoDetalleInfo>
                                                      {
                                                          new RepartoDetalleInfo
                                                              {
                                                                  RepartoDetalleID =
                                                                      info.Field<long>("RepartoDetalleID"),
                                                                  TipoServicioID = info.Field<int>("TipoServicioID"),
                                                                  FormulaIDProgramada =
                                                                      info.Field<int>("FormulaIDProgramada"),
                                                                  FormulaIDServida = info.Field<int>("FormulaIDServida"),
                                                                  TipoFormula = info.Field<int>("TipoFormulaID"),
                                                                  CantidadProgramada =
                                                                      info.Field<int>("CantidadProgramada"),
                                                                  CantidadServida = info.Field<int>("CantidadServida"),
                                                                  HoraReparto = info.Field<string>("HoraReparto"),
                                                                  CostoPromedio = info.Field<decimal>("CostoPromedio"),
                                                                  Importe = info.Field<decimal>("Importe"),
                                                                  Cabezas = info.Field<int>("Cabezas"),
                                                                  EstadoComederoID = info.Field<int>("EstadoComederoID"),
                                                                  CamionRepartoID = info.Field<int?>("CamionRepartoID") != null ? info.Field<int>("CamionRepartoID") : 0,
                                                                  Ajuste = info.Field<bool>("Ajuste"),
                                                                  Prorrateo = info.Field<bool>("Prorrateo"),
                                                                  AlmacenMovimientoID = info.Field<long?>("AlmacenMovimientoID") != null ? info.Field<long>("AlmacenMovimientoID") : 0,
                                                              }
                                                      }
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de repartos
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<AplicacionConsumoDetalleModel> ObtenerConsumoPendiente(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var result =
                    (from info in dt.AsEnumerable()
                     select
                         new AplicacionConsumoDetalleModel
                         {
                           CantidadRegistros = info.Field<int>("TotalRegistros"),
                           CantidadInventario = info.Field<decimal>("CantidadInventario"),
                           CantidadReparto = info.Field<int>("CantidadReparto"),
                           CantidadDiferencia = info.Field<decimal>("Diferencia"),
                           Producto = new ProductoInfo
                                          {
                                              ProductoId = info.Field<int>("ProductoID"),
                                              ProductoDescripcion = info.Field<string>("Producto")
                                          }
                         }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un reparto con su detalle
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<RepartoInfo> ObtenerRepartosPorFechaCorrales(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var resultado =
                    (from info in dt.AsEnumerable()
                     select new RepartoInfo
                     {
                         RepartoID = info.Field<long>("RepartoID"),
                         OrganizacionID = info.Field<int>("OrganizacionID"),
                         Corral = new CorralInfo
                                      {
                                          CorralID = info.Field<int>("CorralID")
                                      },
                         LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                         Fecha = info.Field<DateTime>("Fecha"),
                         PesoInicio = info.Field<int>("PesoInicio"),
                         PesoProyectado = info.Field<int>("PesoProyectado"),
                         DiasEngorda = info.Field<int>("DiasEngorda"),
                         PesoRepeso = info.Field<int>("PesoRepeso"),
                     }).ToList();
                if (resultado.Any())
                {
                    dt = ds.Tables[ConstantesDAL.DtDetalle];
                    resultado.ForEach(detalle =>
                    {
                        detalle.DetalleReparto = (from info in dt.AsEnumerable()
                                                  where info.Field<long>("RepartoID") ==detalle.RepartoID
                                                  select new RepartoDetalleInfo
                                                  {
                                                      RepartoDetalleID = info.Field<long>("RepartoDetalleID"),
                                                      RepartoID = info.Field<long>("RepartoID"),
                                                      TipoServicioID = info.Field<int>("TipoServicioID"),
                                                      FormulaIDProgramada = info.Field<int>("FormulaIDProgramada"),
                                                      FormulaIDServida = info["FormulaIDServida"] == DBNull.Value ? 0 : info.Field<int>("FormulaIDServida"),
                                                      CantidadProgramada = info.Field<int>("CantidadProgramada"),
                                                      CantidadServida = info.Field<int>("CantidadServida"),
                                                      HoraReparto = info.Field<string>("HoraReparto"),
                                                      CostoPromedio = info.Field<decimal>("CostoPromedio"),
                                                      Importe = info.Field<decimal>("Importe"),
                                                      Servido = info.Field<bool>("Servido"),
                                                      Cabezas = info.Field<int>("Cabezas"),
                                                      EstadoComederoID = info.Field<int>("EstadoComederoID"),
                                                      CamionRepartoID = info["CamionRepartoID"] == DBNull.Value ? 0 : info.Field<int>("CamionRepartoID"),
                                                      Observaciones = info.Field<string>("Observaciones"),
                                                  }).ToList();
                    });
                }
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// obtiene una orden de reparto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static RepartoInfo ObtenerRepartoPorFechaCorralServicio(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var resultado =
                    (from info in dt.AsEnumerable()
                     select new RepartoInfo
                     {
                         RepartoID = info.Field<long>("RepartoID"),
                         OrganizacionID = info.Field<int>("OrganizacionID"),
                         //LoteID = info.Field<int>("LoteID"),
                         LoteID = info.Field<int?>("LoteID") != null ? info.Field<int>("LoteID") : 0,
                         Corral = new CorralInfo()
                         {
                             CorralID = info.Field<int>("CorralID")
                         },
                         Fecha = info.Field<DateTime>("Fecha"),
                         PesoInicio = info.Field<int>("PesoInicio"),
                         PesoProyectado = info.Field<int>("PesoProyectado"),
                         DiasEngorda = info.Field<int>("DiasEngorda"),
                         PesoRepeso = info.Field<int>("PesoRepeso"),
                     }).First();


                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
