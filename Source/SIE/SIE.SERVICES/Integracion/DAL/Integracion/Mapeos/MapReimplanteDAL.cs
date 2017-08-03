using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapReimplanteDAL
    {
        /// <summary>
        ///     Método para obtener los datos de la compra
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static DatosCompra ObtenerDatosCompra(DataSet ds)
        {
            DatosCompra datoscompra = null;
            try
            {

                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                datoscompra = new DatosCompra();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    datoscompra.FechaInicio = Convert.ToDateTime(dr["FechaEntrada"]);
                    datoscompra.Origen = Convert.ToString(dr["TipoOrganizacion"]);
                    datoscompra.Proveedor = Convert.ToString(dr["Proveedor"]);
                    datoscompra.TipoAnimal = Convert.ToString(dr["TipoAnimal"]);
                    datoscompra.TipoOrigen = Convert.ToInt32(dr["TipoOrganizacionID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return datoscompra;
        }
        /// <summary>
        ///     Método para obtener los datos de la compra
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AnimalInfo ObtenerDatosReimplante(DataSet ds)
        {
            AnimalInfo resultado = null;
            try
            {

                Logger.Info();
                
                resultado = MapAnimalDAL.ObtenerAnimal(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    resultado.Corral = Convert.ToString(dr["CodigoCorral"]);
                    if (String.IsNullOrEmpty(Convert.ToString(dr["Peso"])))
                    {
                        resultado.PesoAlCorte = 0;
                    }
                    else
                    {
                        resultado.PesoAlCorte = Convert.ToInt32(dr["Peso"]); 
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Metodo que regresa un Mapa de los Reimplantes Disponibles
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static bool ObtenerExisteProgramacionReimplante(DataSet ds)
        {
            var resp = false;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resp = (int.Parse(dt.Rows[0][0].ToString()) > 0 ? true : false);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }
        

        /// <summary>
        ///     Método para obtener los datos del arete
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ReimplanteInfo ObtenerAreteIndividual(DataSet ds)
        {
            ReimplanteInfo resultado = null;
            try
            {

                Logger.Info();
                resultado = new ReimplanteInfo();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Se llena el objeto Animal
                    resultado.Animal = new AnimalInfo
                    {
                        AnimalID = Convert.ToInt32(dr["AnimalID"]),
                        Arete = Convert.ToString(dr["Arete"]),
                        AreteMetalico = Convert.ToString(dr["AreteMetalico"]),
                        TipoGanadoID = Convert.ToInt32(dr["TipoGanadoID"]),
                        CalidadGanadoID = Convert.ToInt32(dr["CalidadGanadoID"]),
                        PesoCompra = Convert.ToInt32(dr["PesoCompra"]),
                        OrganizacionIDEntrada = Convert.ToInt32(dr["OrganizacionIDEntrada"]),
                        FolioEntrada = Convert.ToInt32(dr["FolioEntrada"]),
                        PesoLlegada = Convert.ToInt32(dr["PesoLlegada"]),
                        Venta = Convert.ToBoolean(dr["Venta"]),
                        FechaCompra = Convert.ToDateTime(dr["FechaCompra"])
                    };

                    //Se llena el objeto Corral
                    resultado.Corral = new CorralInfo
                    {
                        CorralID = Convert.ToInt32(dr["CorralID"]),
                        Codigo = Convert.ToString(dr["Codigo"]),
                        TipoCorral = new TipoCorralInfo{ TipoCorralID = Convert.ToInt32(dr["TipoCorralID"]) },
                        Capacidad = Convert.ToInt32(dr["Capacidad"])
                    };

                    //Se obtiene numero reimplante
                    if (dr["NumeroReimplante"] != DBNull.Value)
                        resultado.NumeroReimplante = dr["NumeroReimplante"] != DBNull.Value
                            ? Convert.ToInt32(dr["NumeroReimplante"])
                            : -1;

                    //Se llena el objeto Lote
                    resultado.Lote = new LoteInfo
                    {
                        LoteID = Convert.ToInt32(dr["LoteID"]),
                        Lote = Convert.ToString(dr["Lote"]),
                        TipoCorralID = Convert.ToInt32(dr["TipoCorralID"]),
                        CabezasInicio = Convert.ToInt32(dr["CabezasInicio"]),
                        FechaCierre = dr["FechaCierre"] != DBNull.Value
                                        ? Convert.ToDateTime(dr["FechaCierre"])
                                        : new DateTime(1, 1, 1),
                        FechaDisponibilidad = dr["FechaDisponibilidad"] != DBNull.Value
                                        ? Convert.ToDateTime(dr["FechaDisponibilidad"])
                                        : new DateTime(1,1,1),
                        Cabezas = Convert.ToInt32(dr["Cabezas"]),
                    };

                    //Se llena el objeto PesoCorte
                    resultado.PesoCorte = Convert.ToInt32(dr["PesoCorte"]);
                    resultado.FolioProgramacionReimplanteID = dr["FolioProgramacionID"] != DBNull.Value
                        ? Convert.ToInt32(dr["FolioProgramacionID"])
                        : -1;
                    resultado.FechaReimplante = dr["FechaReimplante"] != DBNull.Value
                        ? Convert.ToDateTime(dr["FechaReimplante"])
                        : new DateTime();
                    resultado.LoteReimplanteID = dr["LoteReimplanteID"] != DBNull.Value 
                        ? Convert.ToInt32(dr["LoteReimplanteID"]) 
                        : -1;

                    resultado.TipoOrigen = Convert.ToInt32(dr["TipoOrigen"]);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que regresa un Mapa de la validacion de corral destino
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerExisteCorralDestino(DataSet ds)
        {
            var resp = 0;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resp = String.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 0 : int.Parse(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }

        internal static bool ObtenerValidarReimplante(DataSet ds)
        {

            var resp = false;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resp = (int.Parse(dt.Rows[0][0].ToString()) == 0 ? true : false);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }
        /// <summary>
        /// Obtiene el valor regresado de la base de datos con el total de cabezas en enfermeria
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerCabezasEnEnfermeria(DataSet ds)
        {
            var resp = 0;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resp = int.Parse(dt.Rows[0][0].ToString());

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }

        /// <summary>
        /// Obtiene el valor de la base de datos para cabezas reimplantadas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<CabezasCortadas> ObtenerCabezasReimplantadas(DataSet ds)
        {
            List<CabezasCortadas> resp;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                resp = (from info in dt.AsEnumerable()
                             select
                                 new CabezasCortadas
                                 {
                                     LoteID = info.Field<int>("LoteID"),
                                     Cabezas = info.Field<int>("Cabezas")
                                 }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }

        internal static int ObtenerCabezasMuertas(DataSet ds)
        {
            var resp = 0;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resp = int.Parse(dt.Rows[0][0].ToString());

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }

        internal static List<ProgramacionReinplanteInfo> ObtenerObtenerCorralesParaAjuste(DataSet ds)
        {
            var listaProgramacionReinplante = new List<ProgramacionReinplanteInfo>();
            try
            {

                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var programacionReinplanteInfo = new ProgramacionReinplanteInfo();
                    programacionReinplanteInfo.LoteID = Convert.ToInt32(dr["LoteID"]);
                    programacionReinplanteInfo.FolioProgramacionID = Convert.ToInt32(dr["FolioProgramacionID"]);

                    listaProgramacionReinplante.Add(programacionReinplanteInfo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaProgramacionReinplante;
        }

        /// <summary>
        /// Metodo para obtener una lista de corrales que fueron reimpantados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<LoteCorralReimplanteInfo> ObtenerCorralesReimplantados(DataSet ds)
        {
            List<LoteCorralReimplanteInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new LoteCorralReimplanteInfo
                         {
                             Lote = new LoteInfo(){LoteID = info.Field<int>("LoteID")},
                             Corral = new CorralInfo(){CorralID = info.Field<int>("CorralID")},
                             PesoOrigen = info.Field<int>("PesoOrigen"),
                             PesoReimplante = info.Field<int>("PesoReimplante"),
                             TotalCabezas = info.Field<int>("TotalCabezas")
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        public static CorralInfo ObtenerExisteCorralDestinoPuntaChica(DataSet ds)
        {
            CorralInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select
                                 new CorralInfo
                                 {

                                     CorralID = info.Field<int>("CorralDestinoID"),
                                     PuntaChica = info.Field<bool>("PuntaChica"),
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
