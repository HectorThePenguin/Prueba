using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Base.Auxiliar;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using System.Data.SqlClient;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapSalidaSacrificioDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<SalidaSacrificioInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SalidaSacrificioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaSacrificioInfo
                         {
                             FEC_SACR = info.Field<string>("FEC_SACR"),
                             NUM_SALI = info.Field<string>("NUM_SALI"),
                             NUM_CORR = info.Field<string>("NUM_CORR"),
                             NUM_PRO = info.Field<string>("NUM_PRO"),
                             FEC_SALC = info.Field<string>("FEC_SALC"),
                             HORA_SAL = info.Field<string>("HORA_SAL"),
                             EDO_COME = info.Field<string>("EDO_COME"),
                             NUM_CAB = info.Field<int>("NUM_CAB"),
                             TIP_ANI = info.Field<string>("TIP_ANI"),
                             KGS_SAL = info.Field<int>("KGS_SAL"),
                             PRECIO = info.Field<int>("PRECIO"),
                             ORIGEN = info.Field<string>("ORIGEN"),
                             CTA_PROVIN = info.Field<string>("CTA_PROVIN"),
                             PRE_EST = info.Field<int>("PRE_EST"),
                             ID_SalidaSacrificio = info.Field<int>("ID_SALIDA_SACRIFICIO"),
                             VENTA_PARA = info.Field<string>("VENTA_PARA"),
                             COD_PROVEEDOR = info.Field<string>("COD_PROVEEDOR"),
                             NOTAS = info.Field<string>("NOTAS"),
                             COSTO_CABEZA = info.Field<string>("COSTO_CABEZA"),
                             CABEZAS_PROCESADAS = info.Field<int>("CABEZAS_PROCESADAS"),
                             FICHA_INICIO = info.Field<int>("FICHA_INICIO"),
                             COSTO_CORRAL = info.Field<string>("COSTO_CORRAL"),
                             UNI_ENT = info.Field<string>("UNI_ENT"),
                             UNI_SAL = info.Field<string>("UNI_SAL"),
                             SYNC = info.Field<string>("SYNC"),
                             ID_S = info.Field<int>("ID_S"),
                             SEXO = info.Field<int>("SEXO"),
                             DIAS_ENG = info.Field<string>("DIAS_ENG"),
                             FOLIO_ENTRADA_I = info.Field<string>("FOLIO_ENTRADA_I"),
                             ORIGEN_GANADO = info.Field<string>("ORIGEN_GANADO"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             CorralID = info.Field<int>("CorralID"),
                             LoteID = info.Field<int>("LoteID"),
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<SalidaSacrificioInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<SalidaSacrificioInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<SalidaSacrificioInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaSacrificioInfo
                         {
                             FEC_SACR = info.Field<string>("FEC_SACR"),
                             NUM_SALI = info.Field<string>("NUM_SALI"),
                             NUM_CORR = info.Field<string>("NUM_CORR"),
                             NUM_PRO = info.Field<string>("NUM_PRO"),
                             FEC_SALC = info.Field<string>("FEC_SALC"),
                             HORA_SAL = info.Field<string>("HORA_SAL"),
                             EDO_COME = info.Field<string>("EDO_COME"),
                             NUM_CAB = info.Field<int>("NUM_CAB"),
                             TIP_ANI = info.Field<string>("TIP_ANI"),
                             KGS_SAL = info.Field<int>("KGS_SAL"),
                             PRECIO = info.Field<int>("PRECIO"),
                             ORIGEN = info.Field<string>("ORIGEN"),
                             CTA_PROVIN = info.Field<string>("CTA_PROVIN"),
                             PRE_EST = info.Field<int>("PRE_EST"),
                             ID_SalidaSacrificio = info.Field<int>("ID_SalidaSacrificio"),
                             VENTA_PARA = info.Field<string>("VENTA_PARA"),
                             COD_PROVEEDOR = info.Field<string>("COD_PROVEEDOR"),
                             NOTAS = info.Field<string>("NOTAS"),
                             COSTO_CABEZA = info.Field<string>("COSTO_CABEZA"),
                             CABEZAS_PROCESADAS = info.Field<int>("CABEZAS_PROCESADAS"),
                             FICHA_INICIO = info.Field<int>("FICHA_INICIO"),
                             COSTO_CORRAL = info.Field<string>("COSTO_CORRAL"),
                             UNI_ENT = info.Field<string>("UNI_ENT"),
                             UNI_SAL = info.Field<string>("UNI_SAL"),
                             SYNC = info.Field<string>("SYNC"),
                             ID_S = info.Field<int>("ID_S"),
                             SEXO = info.Field<int>("SEXO"),
                             DIAS_ENG = info.Field<string>("DIAS_ENG"),
                             FOLIO_ENTRADA_I = info.Field<string>("FOLIO_ENTRADA_I"),
                             ORIGEN_GANADO = info.Field<string>("ORIGEN_GANADO"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static SalidaSacrificioInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SalidaSacrificioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaSacrificioInfo
                         {
                             FEC_SACR = info.Field<string>("FEC_SACR"),
                             NUM_SALI = info.Field<string>("NUM_SALI"),
                             NUM_CORR = info.Field<string>("NUM_CORR"),
                             NUM_PRO = info.Field<string>("NUM_PRO"),
                             FEC_SALC = info.Field<string>("FEC_SALC"),
                             HORA_SAL = info.Field<string>("HORA_SAL"),
                             EDO_COME = info.Field<string>("EDO_COME"),
                             NUM_CAB = info.Field<int>("NUM_CAB"),
                             TIP_ANI = info.Field<string>("TIP_ANI"),
                             KGS_SAL = info.Field<int>("KGS_SAL"),
                             PRECIO = info.Field<int>("PRECIO"),
                             ORIGEN = info.Field<string>("ORIGEN"),
                             CTA_PROVIN = info.Field<string>("CTA_PROVIN"),
                             PRE_EST = info.Field<int>("PRE_EST"),
                             ID_SalidaSacrificio = info.Field<int>("ID_SalidaSacrificio"),
                             VENTA_PARA = info.Field<string>("VENTA_PARA"),
                             COD_PROVEEDOR = info.Field<string>("COD_PROVEEDOR"),
                             NOTAS = info.Field<string>("NOTAS"),
                             COSTO_CABEZA = info.Field<string>("COSTO_CABEZA"),
                             CABEZAS_PROCESADAS = info.Field<int>("CABEZAS_PROCESADAS"),
                             FICHA_INICIO = info.Field<int>("FICHA_INICIO"),
                             COSTO_CORRAL = info.Field<string>("COSTO_CORRAL"),
                             UNI_ENT = info.Field<string>("UNI_ENT"),
                             UNI_SAL = info.Field<string>("UNI_SAL"),
                             SYNC = info.Field<string>("SYNC"),
                             ID_S = info.Field<int>("ID_S"),
                             SEXO = info.Field<int>("SEXO"),
                             DIAS_ENG = info.Field<string>("DIAS_ENG"),
                             FOLIO_ENTRADA_I = info.Field<string>("FOLIO_ENTRADA_I"),
                             ORIGEN_GANADO = info.Field<string>("ORIGEN_GANADO"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static SalidaSacrificioInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                SalidaSacrificioInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaSacrificioInfo
                         {
                             FEC_SACR = info.Field<string>("FEC_SACR"),
                             NUM_SALI = info.Field<string>("NUM_SALI"),
                             NUM_CORR = info.Field<string>("NUM_CORR"),
                             NUM_PRO = info.Field<string>("NUM_PRO"),
                             FEC_SALC = info.Field<string>("FEC_SALC"),
                             HORA_SAL = info.Field<string>("HORA_SAL"),
                             EDO_COME = info.Field<string>("EDO_COME"),
                             NUM_CAB = info.Field<int>("NUM_CAB"),
                             TIP_ANI = info.Field<string>("TIP_ANI"),
                             KGS_SAL = info.Field<int>("KGS_SAL"),
                             PRECIO = info.Field<int>("PRECIO"),
                             ORIGEN = info.Field<string>("ORIGEN"),
                             CTA_PROVIN = info.Field<string>("CTA_PROVIN"),
                             PRE_EST = info.Field<int>("PRE_EST"),
                             ID_SalidaSacrificio = info.Field<int>("ID_SalidaSacrificio"),
                             VENTA_PARA = info.Field<string>("VENTA_PARA"),
                             COD_PROVEEDOR = info.Field<string>("COD_PROVEEDOR"),
                             NOTAS = info.Field<string>("NOTAS"),
                             COSTO_CABEZA = info.Field<string>("COSTO_CABEZA"),
                             CABEZAS_PROCESADAS = info.Field<int>("CABEZAS_PROCESADAS"),
                             FICHA_INICIO = info.Field<int>("FICHA_INICIO"),
                             COSTO_CORRAL = info.Field<string>("COSTO_CORRAL"),
                             UNI_ENT = info.Field<string>("UNI_ENT"),
                             UNI_SAL = info.Field<string>("UNI_SAL"),
                             SYNC = info.Field<string>("SYNC"),
                             ID_S = info.Field<int>("ID_S"),
                             SEXO = info.Field<int>("SEXO"),
                             DIAS_ENG = info.Field<string>("DIAS_ENG"),
                             FOLIO_ENTRADA_I = info.Field<string>("FOLIO_ENTRADA_I"),
                             ORIGEN_GANADO = info.Field<string>("ORIGEN_GANADO"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IList<SalidaSacrificioInfo> ObtenerPorOrdenSacrificioID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<SalidaSacrificioInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaSacrificioInfo
                         {
                             FEC_SACR = info["FEC_SACR"].FromDB<string>(), //info .Field<string>("FEC_SACR").FromDB<string>(),
                             NUM_SALI = info["NUM_SALI"].FromDB<string>(),
                             NUM_CORR = info["NUM_CORR"].FromDB<string>(),
                             NUM_PRO = info["NUM_PRO"].FromDB<string>(),
                             FEC_SALC = info["FEC_SALC"].FromDB<string>(),
                             HORA_SAL = info["HORA_SAL"].FromDB<string>(),
                             EDO_COME = info["EDO_COME"].FromDB<string>(),
                             NUM_CAB = info["NUM_CAB"].FromDB<int>(),
                             TIP_ANI = info["TIP_ANI"].FromDB<string>(),
                             KGS_SAL = info["KGS_SAL"].FromDB<int>(),
                             PRECIO = info["PRECIO"].FromDB<int>(),
                             ORIGEN = info["ORIGEN"].FromDB<string>(),
                             CTA_PROVIN = info["CTA_PROVIN"].FromDB<string>(),
                             PRE_EST = info["PRE_EST"].FromDB<int>(),
                             ID_SalidaSacrificio = info["ID_SalidaSacrificio"].FromDB<int>(),
                             VENTA_PARA = info["VENTA_PARA"].FromDB<string>(),
                             COD_PROVEEDOR = info["COD_PROVEEDOR"].FromDB<string>(),
                             NOTAS = info.Field<string>("NOTAS").FromDB<string>(),
                             COSTO_CABEZA = info["COSTO_CABEZA"].FromDB<string>(),
                             CABEZAS_PROCESADAS = info["CABEZAS_PROCESADAS"].FromDB<int>(),
                             FICHA_INICIO = info["FICHA_INICIO"].FromDB<int>(),
                             COSTO_CORRAL = info["COSTO_CORRAL"].FromDB<string>(),
                             UNI_ENT = info["UNI_ENT"].FromDB<string>(),
                             UNI_SAL = info["UNI_SAL"].FromDB<string>(),
                             SYNC = info["SYNC"].FromDB<string>(),
                             ID_S = info["ID_S"].FromDB<int>(),
                             SEXO = info["SEXO"].FromDB<int>(),
                             DIAS_ENG = info["DIAS_ENG"].FromDB<string>(),
                             FOLIO_ENTRADA_I = info["FOLIO_ENTRADA_I"].FromDB<string>(),
                             ORIGEN_GANADO = info["ORIGEN_GANADO"].FromDB<string>(),
                             //Activo = info.Field<bool>("Activo").BoolAEnum(),
                             OrganizacionID = info["OrganizacionID"].FromDB<int>(),
                             CorralID = info["CorralID"].FromDB<int>(),
                             LoteID = info["LoteID"].FromDB<int>(),
                             OrdenSacrificioID = info["OrdenSacrificioID"].FromDB<int>(),

                         }).ToList();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        ///  Método que lee los registros de una salida de sacrificio
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<SalidaSacrificioInfo> ObtenerPorOrdenSacrificio(SqlDataReader reader)
        {
            try
            {
                Logger.Info();
                var list = new List<SalidaSacrificioInfo>();
                while (reader.Read())
                {
                    var info = new SalidaSacrificioInfo
                    {
                        LoteID = Convert.ToInt32(reader.GetValue(0)),
                        AuxiliarId = Convert.ToInt64(reader.GetValue(1)),
                        Clasificacion = reader.GetValue(2).ToString(),
                        FEC_SACR = reader.GetValue(3).ToString(),
                        NUM_SALI = reader.GetValue(4).ToString(),
                        NUM_CORR = reader.GetValue(5).ToString(),
                        NUM_PRO = reader.GetValue(6).ToString(),
                        NUM_CAB = Convert.ToInt32(reader.GetValue(7)),
                        TIP_ANI = reader.GetValue(8).ToString(),
                        ORIGEN = reader.GetValue(9).ToString(),
                        NOTAS = reader.GetValue(10).ToString(),
                        DIAS_ENG = reader.GetValue(11).ToString()
                    };

                    list.Add(info);
                }
                return list;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static string GuardarSalidaSacrificioMarel(SqlDataReader reader)
        {
            try
            {
                Logger.Info();
                var result = string.Empty;
                if (reader.Read())
                {
                    result = reader.GetValue(0).ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static IList<SalidaSacrificioDetalleInfo> ObtenerPorOrdenSacrificioDetalleId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                IList<SalidaSacrificioDetalleInfo> entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new SalidaSacrificioDetalleInfo
                         {
                             LoteId = info["LoteId"].FromDB<int>(),
                             FolioSalida = info["FolioSalida"].FromDB<int>(),
                             AnimalId = info["AnimalId"].FromDB<long>(),
                             Arete = info["Arete"].FromDB<string>(),
                         }).ToList();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

