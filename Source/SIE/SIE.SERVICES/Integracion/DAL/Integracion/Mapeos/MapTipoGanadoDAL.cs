using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal static class MapTipoGanadoDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TipoGanadoInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<TipoGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<TipoGanadoInfo> lista = (from info in dt.AsEnumerable()
                                              select new TipoGanadoInfo
                                                         {
                                                             TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                             Descripcion = info.Field<string>("Descripcion"),
                                                             Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                                                        ? Sexo.Macho
                                                                        : Sexo.Hembra,
                                                             PesoMinimo = info.Field<int>("PesoMinimo"),
                                                             PesoMaximo = info.Field<int>("PesoMaximo"),
                                                             PesoSalida = info.Field<int>("PesoSalida"),
                                                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                         }).ToList();
                resultado = new ResultadoInfo<TipoGanadoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<TipoGanadoInfo> Centros_ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<TipoGanadoInfo> resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                List<TipoGanadoInfo> lista = (from info in dt.AsEnumerable()
                                              select new TipoGanadoInfo
                                              {
                                                  TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                  Descripcion = info.Field<string>("Descripcion"),
                                                  Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                                             ? Sexo.Macho
                                                             : Sexo.Hembra,
                                                  PesoMinimo = info.Field<int>("PesoMinimo"),
                                                  PesoMaximo = info.Field<int>("PesoMaximo"),
                                                  PesoSalida = info.Field<int>("PesoSalida"),
                                                  Activo = info.Field<bool>("Activo").BoolAEnum(),
                                              }).ToList();
                resultado = new ResultadoInfo<TipoGanadoInfo>
                {
                    Lista = lista,
                    TotalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"])
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoGanadoInfo ObtenerPorID(DataSet ds)
        {
            TipoGanadoInfo tipoGanadoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                tipoGanadoInfo = (from info in dt.AsEnumerable()
                                  select new TipoGanadoInfo
                                             {
                                                 TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                 Descripcion = info.Field<string>("Descripcion").Trim(),
                                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                 PesoMaximo = info.Field<int>("PesoMaximo"),
                                                 PesoMinimo = info.Field<int>("PesoMinimo"),
                                                 Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                                            ? Sexo.Macho
                                                            : Sexo.Hembra,
                                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return tipoGanadoInfo;
        }

        /// <summary>
        ///     Metodo que obtiene una lista
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoGanadoInfo> ObtenerTodos(DataSet ds)
        {
            List<TipoGanadoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new TipoGanadoInfo
                                    {
                                        TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                        Descripcion = info.Field<string>("Descripcion").Trim(),
                                        Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                                   ? Sexo.Macho
                                                   : Sexo.Hembra,
                                        Activo = info.Field<bool>("Activo").BoolAEnum(),
                                        PesoSalida = info.Field<int>("PesoSalida")
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
        /// Metodo que regresa un Mapa de los rangos iniciales por el sexo del ganado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<TipoGanadoInfo> ObtenerPorSexo(DataSet ds)
        {
            List<TipoGanadoInfo> lista;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new TipoGanadoInfo
                         {
                             TipoGanadoID = info.Field<int>("TipoGanadoID"),
                             PesoMinimo = info.Field<int>("PesoMinimo")
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
        /// Metodo que regresa un TipoGanadoInfo con el rango final y tipo de ganado
        /// por el sexo del ganado y el rango inicial
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoGanadoInfo ObtenerPorSexoRangoInicial(DataSet ds)
        {
            TipoGanadoInfo tipoGanadoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                tipoGanadoInfo = (from info in dt.AsEnumerable()
                                  select new TipoGanadoInfo
                                  {
                                      TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                      Descripcion = info.Field<String>("Descripcion").Trim(),
                                      PesoMaximo = info.Field<int>("PesoMaximo")
                                  }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return tipoGanadoInfo;
        }

        /// <summary>
        /// Metodo que regresa un TipoGanadoInfo TipoGanadoID,Descripcion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoGanadoInfo ObtenerTipoGanadoSexoPeso(DataSet ds)
        {
            TipoGanadoInfo tipoGanadoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                tipoGanadoInfo = (from info in dt.AsEnumerable()
                                  select new TipoGanadoInfo
                                  {
                                      TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                      Descripcion = info.Field<string>("Descripcion").Trim(),
                                      Activo = info.Field<bool>("Activo").BoolAEnum(),
                                      PesoMaximo = info.Field<int>("PesoMaximo"),
                                      PesoMinimo = info.Field<int>("PesoMinimo"),
                                      Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                                 ? Sexo.Macho
                                                 : Sexo.Hembra
                                  }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return tipoGanadoInfo;
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoGanadoInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoGanadoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoGanadoInfo
                         {
                             TipoGanadoID = info.Field<int>("TipoGanadoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                               ? Sexo.Macho
                                               : Sexo.Hembra, 
                             PesoMinimo = info.Field<int>("PesoMinimo"),
                             PesoMaximo = info.Field<int>("PesoMaximo"),
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
        internal static TipoGanadoInfo Centros_ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoGanadoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoGanadoInfo
                         {
                             TipoGanadoID = info.Field<int>("TipoGanadoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                               ? Sexo.Macho
                                               : Sexo.Hembra,
                             PesoMinimo = info.Field<int>("PesoMinimo"),
                             PesoMaximo = info.Field<int>("PesoMaximo"),
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
        /// Obtiene una entidad tipoGanadoInfo mediante EntradaGanado
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoGanadoInfo ObtenerTipoGanadoIDPorEntradaGanado(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                TipoGanadoInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoGanadoInfo
                         {
                             TipoGanadoID = info.Field<int>("TipoGanadoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             //Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                             //                  ? Sexo.Macho
                             //                  : Sexo.Hembra,
                             //PesoMinimo = info.Field<int>("PesoMinimo"),
                             //PesoMaximo = info.Field<int>("PesoMaximo"),
                             //Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<TipoGanadoInfo> ObtenerDescripcionesPorIDs(DataSet ds)
        {

            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoGanadoInfo
                         {
                             TipoGanadoID = info.Field<int>("TipoGanadoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<ContenedorTipoGanadoPoliza> ObtenerTipoPorAnimal(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ContenedorTipoGanadoPoliza
                             {
                                 AnimalMovimiento = new AnimalMovimientoInfo
                                                        {
                                                            AnimalID = info.Field<long>("AnimalID"),
                                                            OrganizacionID = info.Field<int>("OrganizacionID")
                                                        },
                                 Animal = new AnimalInfo
                                              {
                                                  AnimalID = info.Field<long>("AnimalID"),
                                                  TipoGanado = new TipoGanadoInfo
                                                                   {
                                                                       Descripcion = info.Field<string>("TipoGanado"),
                                                                       TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                                   },
                                                  PesoLlegada = info.Field<int>("PesoLlegada"),
                                                  PesoCompra = info.Field<int>("PesoCompra")
                                              },
                                 Lote = new LoteInfo
                                            {
                                                Lote = info.Field<string>("Lote"),
                                                Corral = new CorralInfo
                                                             {
                                                                 Codigo = info.Field<string>("Corral")
                                                             }
                                            }
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static TipoGanadoInfo ObtenerTipoGanadoInfo(DataSet ds)
        {
            TipoGanadoInfo tipoGanado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                tipoGanado = (from info in dt.AsEnumerable()
                                              select new TipoGanadoInfo
                                              {
                                                  TipoGanadoID = info.Field<int>("TipoGanadoID"),
                                                  Descripcion = info.Field<string>("Descripcion"),
                                                  Sexo = Convert.ToChar(info.Field<string>("Sexo")) == 'M'
                                                             ? Sexo.Macho
                                                             : Sexo.Hembra,
                                                  PesoMinimo = info.Field<int>("PesoMinimo"),
                                                  PesoMaximo = info.Field<int>("PesoMaximo"),
                                                  PesoSalida = info.Field<int>("PesoSalida"),
                                                  Activo = info.Field<bool>("Activo").BoolAEnum(),
                                              }).FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return tipoGanado;
        }
    }
}