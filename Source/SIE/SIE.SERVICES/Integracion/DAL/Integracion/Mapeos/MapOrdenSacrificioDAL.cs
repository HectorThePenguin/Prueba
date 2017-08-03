using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Data.SqlClient;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapOrdenSacrificioDAL
    {

        /// <summary>
        ///     Método asigna el registro del animal obtenido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static OrdenSacrificioInfo ObtenerOrdenSacrificio(DataSet ds)
        {
            OrdenSacrificioInfo ordenSacrificio = null;
            try
            {

                Logger.Info();
                
                ordenSacrificio = new OrdenSacrificioInfo();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   
                    ordenSacrificio.OrdenSacrificioID = Convert.ToInt32(dr["OrdenSacrificioID"]);
                    ordenSacrificio.FolioOrdenSacrificio = Convert.ToInt32(dr["FolioOrdenSacrificio"]);
                    ordenSacrificio.OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]);
                    ordenSacrificio.Observacion = Convert.ToString(dr["Observaciones"]);
                    ordenSacrificio.EstatusID = Convert.ToInt32(dr["EstatusID"]);
                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return ordenSacrificio;
        }
        /// <summary>
        /// Obtiene el detalle de la orden sacrificio
        /// </summary>
        /// <param name="ds">Data set con los datos</param>
        /// <param name="activo">Indica si el registro del detalle se puede editar</param>
        /// <returns></returns>
		internal static IList<OrdenSacrificioDetalleInfo> ObtenerDetalleOrdenSacrificio(DataSet ds,bool activo)        {
            IList<OrdenSacrificioDetalleInfo> listaOrdenSacrificio = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var usuarioDAL = new UsuarioDAL();
                var corralDal = new CorralDAL();
                var loteDal = new LoteDAL();
                listaOrdenSacrificio = (from detalle in dt.AsEnumerable()
                                        select new OrdenSacrificioDetalleInfo
                                         {  
                                             OrdenSacrificioDetalleID = detalle.Field<int>("OrdenSacrificioDetalleID"),
                                             OrdenSacrificioID = detalle.Field<int>("OrdenSacrificioID"),
                                             FolioOrdenSacrificio = detalle.Field<int>("FolioSalida"),
                                             Corraleta = corralDal.ObtenerPorId(detalle.Field<int>("CorraletaID")),
                                             Lote = loteDal.ObtenerPorID(detalle.Field<int>("LoteID")),
                                             CorraletaCodigo = detalle.Field<string>("CorraletaCodigo"),
                                             Cabezas = detalle.Field<int>("CabezasLote"),
                                             DiasEngordaGrano = detalle.Field<int>("DiasEngordaGrano"),
                                             DiasRetiro = detalle.Field<int>("DiasRetiro"),
                                             CabezasASacrificar = detalle.Field<int>("CabezasSacrificio"),
                                             Turno = (TurnoEnum)detalle.Field<int>("Turno"),
                                             Proveedor = new ProveedorInfo{Descripcion = detalle.Field<string>("Proveedor")},
                                             Clasificacion = detalle.Field<string>("Clasificacion"),
                                             Orden = detalle.Field<int>("Orden"),
                                             Turnos = Enum.GetValues(typeof(TurnoEnum)).Cast<TurnoEnum>().ToList(),
                                             Usuario = usuarioDAL.ObtenerPorID(detalle.Field<int>("UsuarioCreacion")),
                                             Activo = activo,
                                             CabezasActuales = detalle.Field<int>("CabezasActuales"),
                                             Seleccionable = true
                                         }).ToList();

                if (listaOrdenSacrificio!=null)
                {
                    foreach (var ordenDetalle in listaOrdenSacrificio)
                    {
                        ordenDetalle.Corral = corralDal.ObtenerPorId(ordenDetalle.Lote.CorralID);
                        //if(ordenDetalle.Corral.TipoCorral.TipoCorralID != TipoCorral.Intensivo.GetHashCode() && ordenDetalle.CabezasActuales == 0)
                        //{
                        //    ordenDetalle.Seleccionable = false;
                        //}
                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaOrdenSacrificio;
        }

        /// <summary>
        /// Obtiene los dias de engorda 70
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerDiasEngorda70(DataSet ds)
        {
            int diasRetiro = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    diasRetiro = Convert.ToInt32(dr["DiasEngorda70"]);

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
        /// Obtiene el numero de cabezas en ordenes de sacrificio distintas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtnerCabezasDiferentesOrdenes(DataSet ds)
        {
            var cabezas = 0;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    cabezas = dr["Cabezas"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Cabezas"]);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return cabezas;

        }

        internal static OrdenSacrificioInfo ObtenerOrdenSacrificioFecha(DataSet ds)
        {
            OrdenSacrificioInfo ordenSacrificio = null;
            try
            {
                Logger.Info();
                ordenSacrificio = new OrdenSacrificioInfo();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ordenSacrificio.OrdenSacrificioID = Convert.ToInt32(dr["ordenSacrificioID"]);
                    ordenSacrificio.FolioOrdenSacrificio = Convert.ToInt32(dr["FolioOrdenSacrificio"]);
                    ordenSacrificio.OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]);
                    ordenSacrificio.Observacion = Convert.ToString(dr["Observaciones"]);
                    ordenSacrificio.FechaOrden = Convert.ToDateTime(dr["FechaOrden"]);
                    ordenSacrificio.EstatusID = Convert.ToInt32(dr["EstatusID"]);
                    ordenSacrificio.Activo = Convert.ToBoolean(dr["Activo"]);
                    ordenSacrificio.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    ordenSacrificio.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacionID"]);
                    ordenSacrificio.FechaModificacion = string.IsNullOrEmpty(dr["FechaModificacion"].ToString()) ? new Nullable<DateTime>() : Convert.ToDateTime(dr["FechaModificacion"]);
                    ordenSacrificio.UsuarioModificacionID = string.IsNullOrEmpty(dr["UsuarioModificacionID"].ToString()) ? new Nullable<int>() : Convert.ToInt32(dr["UsuarioModificacionID"]);
                }
                return ordenSacrificio;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);

            }
        }

        internal static List<OrdenSacrificioDetalleInfo> ObtenerDetalleOrdenSacrificioFecha(DataSet ds)
        {
            List<OrdenSacrificioDetalleInfo> listaDetalleOrdenSacrificio = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                listaDetalleOrdenSacrificio = (from detalle in dt.AsEnumerable()
                                               select new OrdenSacrificioDetalleInfo
                                               {
                                                   OrdenSacrificioDetalleID = detalle.Field<int>("OrdenSacrificioDetalleID"),
                                                   FolioOrdenSacrificio = detalle.Field<int>("OrdenSacrificioID"),
                                                   FolioSalida = detalle.Field<int>("FolioSalida"),
                                                   CorraletaCodigo = detalle.Field<string>("CorraletaCodigo"),
                                                   Cabezas = detalle.Field<int>("CabezasLote"),
                                                   DiasEngordaGrano = detalle.Field<int>("DiasEngordaGrano"),
                                                   DiasRetiro = detalle.Field<int>("DiasRetiro"),
                                                   CabezasASacrificar = detalle.Field<int>("CabezasSacrificio"),
                                                   ProveedorID = detalle.Field<string>("Proveedor"),
                                                   Clasificacion = detalle.Field<string>("Clasificacion"),
                                                   Orden = detalle.Field<int>("Orden"),
                                                   Activo = detalle.Field<bool>("Activo"),
                                                   FechaCreacion = detalle.Field<DateTime>("FechaCreacion"),
                                                   UsuarioCreacion = detalle.Field<int>("UsuarioCreacion"),
                                                   FechaModificacion = string.IsNullOrEmpty(detalle["FechaModificacion"].ToString()) ? new Nullable<DateTime>() : detalle.Field<DateTime>("FechaModificacion"),
                                                   UsuarioModificacion = string.IsNullOrEmpty(detalle["UsuarioModificacion"].ToString()) ? new Nullable<int>() : detalle.Field<int>("UsuarioModificacion")
                                               }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaDetalleOrdenSacrificio;
        }

        internal static bool ObtenerCancelacionOrden(DataSet ds)
        {
            var result = false;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    result = Convert.ToInt32(dr[0]) != 0 ? true : false;
                    if(dr[1].ToString().Trim() != string.Empty)
                    {
                        Logger.Error(new Exception(dr[1].ToString().Trim()));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }

        internal static bool ObtenerCancelacionMarel(SqlDataReader reader)
        {
            var result = false;
            try
            {
                Logger.Info();
                if(reader.Read())
                {
                    result = Convert.ToInt32(reader.GetValue(0).ToString()) != 0 ? true : false;
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<ControlSacrificioInfo> ObtenerResumenSacrificioScp(SqlDataReader reader)
        {
            var result = new List<ControlSacrificioInfo>();
            try
            {
                Func<IDataReader, string, Nullable<int>> fInt = (r, i) =>
                {
                    if (r[i] is DBNull)
                        return new Nullable<int>();
                    return int.Parse(r[i].ToString());
                };
                Func<IDataReader, string, Nullable<long>> fLong = (r, i) =>
                {
                    if (r[i] is DBNull)
                        return new Nullable<long>();
                    return long.Parse(r[i].ToString());
                };

                Logger.Info();
                while (reader.Read())
                {
                    var info = new ControlSacrificioInfo
                                   {
                                       FechaSacrificio = reader["FEC_SACR"].ToString(),
                                       NumeroCorral = reader["NUM_CORR"].ToString(),
                                       NumeroProceso = reader["NUM_PRO"].ToString(),
                                       Arete = reader["TAG_ARETE"].ToString(),
                                       TipoGanadoId = fInt(reader, "TIPO_DE_GANADO"),
                                       LoteId = fInt(reader,"LOTEID"),
                                       AnimalId = fLong(reader, "ANIMALID"),
                                       CorralInnova = reader["NUM_CORR_INNOVA"].ToString(),
                                       Po = reader["PO_INNOVA"].ToString(),
                                       Consecutivo = Convert.ToInt32(reader["Consecutivo_Sacrificio"].ToString()),
                                       Noqueo =         Convert.ToBoolean(reader["Indicador_Noqueo"].ToString()),
                                       PielSangre =     Convert.ToBoolean(reader["Indicador_Piel_Sangre"].ToString()),
                                       PielDescarnada = Convert.ToBoolean(reader["Indicador_Piel_Descarnada"].ToString()),
                                       Viscera =        Convert.ToBoolean(reader["Indicador_Viscera"].ToString()),
                                       Inspeccion =     Convert.ToBoolean(reader["Indicador_Inspeccion"].ToString()),
                                       CanalCompleta =  Convert.ToBoolean(reader["Indicador_Canal_Completa"].ToString()),
                                       CanalCaliente =  Convert.ToBoolean(reader["Indicador_Canal_Caliente"].ToString())
                                   };
                    result.Add(info);
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los datos de conexion a control de piso por organización.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<string> ObtenerConexionOrganizacion(DataSet ds)
        {
            var result = new List<string>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                foreach (DataRow dr in dt.Rows)
                {
                    var dato = Convert.ToString(dr["Valor"]);

                    result.Add(dato);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        internal static List<SalidaSacrificioInfo> ObtenerEstatusOrdenSacrificioMarel(SqlDataReader reader)
        {
            try
            {
                Logger.Info();
                var result = new List<SalidaSacrificioInfo>();
                while(reader.Read())
                {
                    var salida = new SalidaSacrificioInfo
                                     {
                                         Estatus = Convert.ToInt32(reader.GetValue(0).ToString()),
                                         NUM_SALI = reader.GetValue(1).ToString(),
                                         FEC_SACR = reader.GetValue(2).ToString(),
                                         NUM_CORR = reader.GetValue(3).ToString(),
                                         NUM_PRO = reader.GetValue(4).ToString(),
                                         Lote = reader.GetValue(5).ToString()
                                     };
                    result.Add(salida);
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<OrdenSacrificioDetalleInfo> ObtenerCabezasActualesVsCabezasSacrificar(DataSet ds)
        {
            List<OrdenSacrificioDetalleInfo> listaDetalleOrdenSacrificio = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                listaDetalleOrdenSacrificio = (from detalle in dt.AsEnumerable()
                                               select new OrdenSacrificioDetalleInfo
                                               {
                                                   Lote = new LoteInfo() { Lote = detalle.Field<string>("Lote") },
                                                   Cabezas = detalle.Field<int>("Cabezas"),
                                                   CabezasASacrificar = detalle.Field<int>("CabezasSacrificar")
                                                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaDetalleOrdenSacrificio;
        }

        internal static List<string> ObtenerCorralesConLotesActivos(DataSet ds)
        {
            List<string> corrales = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                corrales = (from detalle in dt.AsEnumerable()
                                               select detalle.Field<string>("Codigo")).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return corrales;
        }

        internal static List<ControlSacrificioInfo> ObtenerResumenSacrificioSiap(DataSet ds)
        {
            var resumen = new List<ControlSacrificioInfo>();
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                resumen = (from detalle in dt.AsEnumerable()
                           select new ControlSacrificioInfo
                                               {
                                                   CodigoCorral = detalle.Field<string>("Codigo"),
                                                   LoteIdSiap = detalle.Field<int?>("LoteIdSiap"),
                                                   Lote = detalle.Field<string>("Lote"),
                                                   Arete = detalle.Field<long>("TAG_ARETE").ToString(),
                                                   FechaSacrificio = detalle.Field<string>("FEC_SACR"),
                                                   NumeroCorral = detalle.Field<string>("NUM_CORR"),
                                                   NumeroProceso = detalle.Field<string>("NUM_PRO"),
                                                   TipoGanado = detalle.Field<string>("TIPO_DE_GANADO"),
                                                   TipoGanadoId = detalle.Field<int>("TipoGanadoID"),
                                                   LoteId = detalle.Field<int>("LOTEID"),
                                                   AnimalId = detalle.Field<long>("ANIMALID"),
                                                   AnimalIdSiap = detalle.Field<long?>("AnimalIdSiap"),
                                                   CorralInnova = detalle.Field<string>("NUM_CORR_INNOVA"),
                                                   Po = detalle.Field<string>("PO_INNOVA"),
                                                   Consecutivo = detalle.Field<int>("Consecutivo_Sacrificio"),
                                                   Noqueo = detalle.Field<bool>("Indicador_Noqueo"),
                                                   PielSangre = detalle.Field<bool>("Indicador_Piel_Sangre"),
                                                   PielDescarnada = detalle.Field<bool>("Indicador_Piel_Descarnada"),
                                                   Viscera = detalle.Field<bool>("Indicador_Viscera"),
                                                   Inspeccion = detalle.Field<bool>("Indicador_Inspeccion"),
                                                   CanalCompleta = detalle.Field<bool>("Indicador_Canal_Completa"),
                                                   CanalCaliente = detalle.Field<bool>("Indicador_Canal_Caliente")
                                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resumen;
        }
    }
}
