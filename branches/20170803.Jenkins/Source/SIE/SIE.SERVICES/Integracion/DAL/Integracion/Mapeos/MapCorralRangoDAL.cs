using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal  class MapCorralRangoDAL
    {
        /// <summary>
        /// Metodo que regresa un Mapa de los Corrales Disponibles
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static List<CorralRangoInfo> ObtenerPorOrganizacionID(DataSet ds)
        {
            List<CorralRangoInfo> lista = new List<CorralRangoInfo>();
            try
            {
                Logger.Info();
                if (ds.Tables.Count > ConstantesDAL.DtDatos)
                {
                    DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                    lista = (from info in dt.AsEnumerable()
                        select new CorralRangoInfo
                        {                                              
                            CorralID = info.Field<int>("CorralID"),
                            Codigo = info.Field<string>("Codigo")

                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }


        internal  static List<CorralRangoInfo> ObtenerCorralesConfiguradosPorOrganizacionID(DataSet ds)
        {
            List<CorralRangoInfo> lista = null;
            try
            {
                Logger.Info();
                if (ds.Tables.Count > ConstantesDAL.DtDatos)
                {
                    DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                    int i = 0;
                    lista = (from info in dt.AsEnumerable()
                             select new CorralRangoInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Codigo = info.Field<string>("Codigo"),
                                 Sexo = info.Field<string>("Sexo"),
                                 RangoInicial = info.Field<int>("RangoInicial"),
                                 RangoFinal = info.Field<int>("RangoFinal"),
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 DescripcionTipoGanado = info.Field<string>("Descripcion"),
                                 Modificado = false,
                                 Accion = AccionConfigurarCorrales.Almacenado,
                                 Id = ++i
                             }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
        /// <summary>
        /// Metodo que regresa un boolean true si el corral tiene o false si no tiene lote asignado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static Boolean ObtenerLoteAsignado(DataSet ds)
        {
            var tieneLoteAsignado = false;
            try
            {
                Logger.Info();
                if (ds.Tables.Count > ConstantesDAL.DtDatos)
                {
                    DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                    var count = (from info in dt.AsEnumerable()
                        select new CorralRangoInfo
                        {
                            CorralID = info.Field<int>("CorralID")
                        }).Count();

                    if (count > 0)
                    {
                        tieneLoteAsignado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return tieneLoteAsignado;
        }
        
        /// <summary>
        /// Metodo obtiene el corral destino
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static IList<CorralRangoInfo> ObtenerCorralDestino(DataSet ds)
        {
            List<CorralRangoInfo> lista = new List<CorralRangoInfo>();
            try
            {
                Logger.Info();
                if (ds.Tables.Count > ConstantesDAL.DtDatos)
                {
                    DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                    lista = (from info in dt.AsEnumerable()
                             select new CorralRangoInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Sexo = info.Field<string>("Sexo"),
                                 RangoInicial = info.Field<int>("RangoInicial"),
                                 RangoFinal = info.Field<int>("RangoFinal"),
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Codigo = info.Field<string>("Codigo")
                             }).ToList();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }

    }
}
