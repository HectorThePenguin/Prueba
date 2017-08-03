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
    public class MapMarcasDAL
    {
        /// <summary>
        /// Método que lee los resultados de la consulta de guardar marca.
        /// </summary>
        /// <param name="ds"> DataSet con la información consultada </param>
        /// <returns> Objeto con los datos obtenidos </returns>
        internal static MarcasInfo GuardaMarca(DataSet ds)
        {
            MarcasInfo marcasInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                marcasInfo = (from info in dt.AsEnumerable()
                                        select new MarcasInfo()
                                        {
                                            MarcaId = info.Field<int>("MarcaID"),
                                            Descripcion = info.Field<string>("Descripcion"),
                                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                                            EsTracto = info.Field<bool>("Tracto").TractoAEnum(),
                                            UsuarioCreacionID = info.Field<int>("UsuarioCreacionId"),
                                            FechaCreacion = info.Field<DateTime>("FechaCreacion")
                                        }).First();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return marcasInfo;

        }

        /// <summary>
        /// Lee los datos obtenidos al actualizar marca
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida </param>
        /// <returns> Objeto con la información leida </returns>
        internal static MarcasInfo ActulizarMarca(DataSet ds)
        {
            MarcasInfo marcasInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                marcasInfo = (from info in dt.AsEnumerable()
                              select new MarcasInfo()
                              {
                                  MarcaId = info.Field<int>("MarcaID"),
                                  Descripcion = info.Field<string>("Descripcion"),
                                  Activo = info.Field<bool>("Activo").BoolAEnum(),
                                  EsTracto = info.Field<bool>("Tracto").TractoAEnum(),
                                  UsuarioCreacionID = info.Field<int>("UsuarioCreacionId"),
                                  FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                  UsuarioModificacionID = info.Field<int>("UsuarioModificacionId"),
                                  FechaModificacion = info.Field<DateTime>("FechaModificacion")
                              }).First();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return marcasInfo;

        }

        /// <summary>
        /// Método que lee los datos obtenidos por el metodo obtener por página
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida </param>
        /// <returns> Objeto con los datos obtenidos. </returns>
        internal static ResultadoInfo<MarcasInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<MarcasInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MarcasInfo
                         {
                             MarcaId = info.Field<int>("MarcaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             EsTracto = info.Field<bool>("Tracto").TractoAEnum()
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<MarcasInfo>
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
        /// Método que lee los datos obtenidos por el método ObtenerMarcas
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida </param>
        /// <returns> Objeto con los datos obtenidos </returns>
        internal static List<MarcasInfo> ObtenerMarcas(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<MarcasInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new MarcasInfo
                         {
                             MarcaId = info.Field<int>("MarcaID"),
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
        /// Lee los datos obtenidos por el método VerificaExistenciaMarca
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida </param>
        /// <returns> Objeto creado con los datos leidos </returns>
        internal static MarcasInfo VerificaExistenciaMarca(DataSet ds)
        {
            MarcasInfo marcasInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                marcasInfo = (from info in dt.AsEnumerable()
                              select new MarcasInfo()
                              {
                                  MarcaId = info.Field<int>("MarcaID"),
                                  Descripcion = info.Field<string>("Descripcion"),
                                  Activo = info.Field<bool>("Activo").BoolAEnum(),
                                  EsTracto = info.Field<bool>("Tracto").TractoAEnum(),
                                  UsuarioCreacionID = info.Field<int>("UsuarioCreacionId"),
                                  FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                  UsuarioModificacionID = info.Field<int>("UsuarioModificacionId"),
                                  FechaModificacion = info.Field<DateTime>("FechaModificacion")
                              }).First();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return marcasInfo;

        }
    }
}
