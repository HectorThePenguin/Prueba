using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapTipoProveedorDAL
    {

        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TipoProveedorInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<TipoProveedorInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<TipoProveedorInfo> lista = (from info in dt.AsEnumerable()
                                                 select new TipoProveedorInfo
                                                            {
                                                                TipoProveedorID = info.Field<int>("TipoProveedorId"),
                                                                Descripcion = info.Field<string>("Descripcion"),
                                                                CodigoGrupoSap = info.Field<string>("CodigoGrupoSap"),
                                                                Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                                UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                                                UsuarioModificacionID = info.Field<int>("UsuarioModificacionID")
                                                            }).ToList();

                resultado = new ResultadoInfo<TipoProveedorInfo>
                                {
                                    Lista = lista,
                                    TotalRegistros =
                                        Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
                                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoProveedorInfo> ObtenerTodos(DataSet ds)
        {
            List<TipoProveedorInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new TipoProveedorInfo
                                    {
                                        TipoProveedorID = info.Field<int>("TipoProveedorId"),
                                        Descripcion = info.Field<string>("Descripcion"),
                                        CodigoGrupoSap = info.Field<string>("CodigoGrupoSap"),
                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoProveedorInfo ObtenerPorID(DataSet ds)
        {
            TipoProveedorInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new TipoProveedorInfo
                                    {
                                        TipoProveedorID = info.Field<int>("TipoProveedorId"),
                                        Descripcion = info.Field<string>("Descripcion"),
                                        CodigoGrupoSap = info.Field<string>("CodigoGrupoSap"),
                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                                    }).First();
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