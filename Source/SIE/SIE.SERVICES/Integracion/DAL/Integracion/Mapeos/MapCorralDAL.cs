using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using iTextSharp.text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapCorralDAL
    {
        /// <summary>
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<CorralInfo> ObtenerPorPagina(DataSet ds)
        {
            ResultadoInfo<CorralInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 Codigo = info.Field<String>("Codigo").Trim(),
                                 TipoCorral =
                                     new TipoCorralInfo
                                         {
                                             TipoCorralID = info.Field<int>("TipoCorralID"),
                                             Descripcion = info.Field<string>("TipoCorral")
                                         },
                                 Capacidad = info.Field<int>("Capacidad"),
                                 FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                 MetrosAncho = info.Field<long>("MetrosAncho"),
                                 MetrosLargo = info.Field<int>("MetrosLargo"),
                                 Seccion = info.Field<int>("Seccion"),
                                 Orden = info.Field<int>("Orden"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),

                             }).ToList();

                resultado = new ResultadoInfo<CorralInfo>
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerPorCodigo(DataSet ds)
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
                                         Activo = info.Field<bool>("Activo").BoolAEnum(),
                                         Capacidad = info.Field<int>("Capacidad"),
                                         Codigo = info.Field<String>("Codigo").Trim(),
                                         CorralID = info.Field<int>("CorralID"),
                                         MetrosAncho = info.Field<long>("MetrosAncho"),
                                         MetrosLargo = info.Field<int>("MetrosLargo"),
                                         Orden = info.Field<int>("Orden"),
                                         Organizacion =
                                             new OrganizacionInfo
                                                 {
                                                     OrganizacionID = info.Field<int>("OrganizacionID"),
                                                 },
                                         Seccion = info.Field<int>("Seccion"),
                                         TipoCorral =
                                             new TipoCorralInfo
                                                 {
                                                     TipoCorralID = info.Field<int>("TipoCorralID")
                                                 },
                                     }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal static CorralInfo ObtenerPorId(DataSet ds)
        {
            CorralInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new CorralInfo
                                        {
                                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                                            Capacidad = info.Field<int>("Capacidad"),
                                            Codigo = info.Field<String>("Codigo").Trim(),
                                            CorralID = info.Field<int>("CorralID"),
                                            FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                            MetrosAncho = info.Field<long>("MetrosAncho"),
                                            MetrosLargo = info.Field<int>("MetrosLargo"),
                                            Orden = info.Field<int>("Orden"),
                                            Organizacion =
                                                new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                                            Seccion = info.Field<int>("Seccion"),
                                            TipoCorral =
                                                new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                                            UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                            GrupoCorral = info.Field<int>("GrupoCorralID")
                                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerId(DataSet ds)
        {
            CorralInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new CorralInfo
                                        {
                                            CorralID = info.Field<int>("CorralID"),
                                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obteiene la lista de corrales
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<CorralInfo> ObtenerLista(DataSet ds)
        {
            IList<CorralInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista = (from info in dt.AsEnumerable()
                             select new CorralInfo
                                        {
                                            Activo = info.Field<bool>("Activo").BoolAEnum(),
                                            Capacidad = info.Field<int>("Capacidad"),
                                            Codigo = info.Field<String>("Codigo").Trim(),
                                            CorralID = info.Field<int>("CorralID"),
                                            FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                            MetrosAncho = info.Field<long>("MetrosAncho"),
                                            MetrosLargo = info.Field<int>("MetrosLargo"),
                                            Orden = info.Field<int>("Orden"),
                                            Organizacion = new OrganizacionInfo
                                                               {
                                                                   OrganizacionID = info.Field<int>("OrganizacionID"),
                                                               },
                                            Seccion = info.Field<int>("Seccion"),
                                            TipoCorral = new TipoCorralInfo
                                                             {
                                                                 TipoCorralID = info.Field<int>("TipoCorralID"),
                                                             },
                                            UsuarioCreacionID = info.Field<int>("UsuarioCreacionID"),
                                            UsuarioModificacionID =
                                                (info["UsuarioModificacionID"] == DBNull.Value
                                                     ? 0
                                                     : info.Field<int>("UsuarioModificacionID"))
                                        }).ToList();

                resultado = lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene los datos del corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerCorralPorEmbarqueRuteo(DataSet ds)
        {
            CorralInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                resultado = (from info in dt.AsEnumerable()
                             select new CorralInfo
                                        {
                                            Codigo = info.Field<String>("Codigo"),
                                            CorralID = info.Field<int>("CorralID"),
                                            Capacidad = info.Field<int>("Capacidad"),
                                        }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Organizacion =
                                     new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                                 Codigo = info.Field<string>("Codigo"),
                                 TipoCorral = new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                                 Capacidad = info.Field<int>("Capacidad"),
                                 MetrosLargo = info.Field<int>("MetrosLargo"),
                                 MetrosAncho = info.Field<long>("MetrosAncho"),
                                 Seccion = info.Field<int>("Seccion"),
                                 Orden = info.Field<int>("Orden"),
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<CorralInfo> ObtenerPorOrganizacion(DataSet ds)
        {
            ResultadoInfo<CorralInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                         },
                                 Codigo = info.Field<String>("Codigo").Trim(),
                                 TipoCorral =
                                     new TipoCorralInfo
                                         {
                                             TipoCorralID = info.Field<int>("TipoCorralID"),
                                         },
                                 Capacidad = info.Field<int>("Capacidad"),
                                 FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                 MetrosAncho = info.Field<long>("MetrosAncho"),
                                 MetrosLargo = info.Field<int>("MetrosLargo"),
                                 Seccion = info.Field<int>("Seccion"),
                                 Orden = info.Field<int>("Orden"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),

                             }).ToList();

                resultado = new ResultadoInfo<CorralInfo>
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
        ///  Método que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerCorralPorCodigo(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Codigo = info.Field<string>("Codigo"),
                                 TipoCorral = new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                                 GrupoCorral = info.Field<int>("GrupoCorralID"),
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
        internal static CorralInfo ObtenerCorralPorCodigoExiste(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Codigo = info.Field<string>("Codigo"),
                                 TipoCorral = new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
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
        internal static string ObtenerDetectorCorral(DataSet ds)
        {
            try
            {
                var entidad = "";
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        entidad += ",";
                    }
                    entidad += dt.Rows[0][0].ToString();
                }
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene si existe el corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static bool ObtenerValidarCodigoCorralPorEnfermeria(DataSet ds)
        {

            var resp = false;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resp = (int.Parse(dt.Rows[0][0].ToString()) == 1 ? true : false);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }

        ///// <summary>
        ///// Obtiene la Información que regresa el procedimiento almacenado
        ///// </summary>
        ///// <param name="ds"></param>
        ///// <returns></returns>
        //internal static List<ContenedorReporteProyectorInfo> ObtenerReporteProyectorComportamiento(DataSet ds)
        //{
        //    List<ContenedorReporteProyectorInfo> lista;
        //    try
        //    {
        //        Logger.Info();
        //        var dt = ds.Tables[ConstantesDAL.DtDatos];
        //        var dtAnimal = ds.Tables[ConstantesDAL.DtReporteProyectoAnimal];
        //        var dtLoteProyeccion = ds.Tables[ConstantesDAL.DtReporteProyectoLoteProyeccion];
        //        var dtLoteReimplante = ds.Tables[ConstantesDAL.DtReporteProyectoLoteReimplante];


        //        var listaCorrales = (from corral in dt.AsEnumerable()
        //                             select new ContenedorReporteProyectorInfo
        //                                 {
        //                                     CorralID = corral.Field<int>("CorralID"),
        //                                     CodigoCorral = corral.Field<string>("CodigoCorral"),
        //                                     LoteID = corral.Field<int>("LoteID"),
        //                                     CodigoLote = corral.Field<string>("CodigoLote"),
        //                                     Cabezas = corral.Field<int>("Cabezas"),
        //                                     TipoGanado = corral.Field<string>("TipoGanado"),
        //                                     FechaInicioLote = corral.Field<DateTime>("FechaInicio"),
        //                                     FechaDisponibilidad =
        //                                         corral.Field<DateTime?>("FechaDisponibilidad") != null
        //                                             ? corral.Field<DateTime>("FechaDisponibilidad")
        //                                             : DateTime.MinValue,
        //                                     DisponibilidadManual = corral.Field<bool>("DisponibilidadManual"),
        //                                     DiasF4 = corral.Field<int>("DiasF4"),
        //                                     DiasZilmax = corral.Field<int>("DiasZilmax"),
        //                                     DiasSacrificio = corral.Field<int>("DiasSacrificio")
        //                                 }).ToList();

        //        var listaAnimales = (from animales in dtAnimal.AsEnumerable()
        //                             select new AnimalInfo
        //                                 {
        //                                     CorralID = animales.Field<int>("CorralID"),
        //                                     AnimalID = animales.Field<long>("AnimalID"),
        //                                     Arete = animales.Field<string>("Arete"),
        //                                     AreteMetalico = animales.Field<string>("AreteMetalico"),
        //                                     FechaCompra = animales.Field<DateTime>("FechaCompra"),
        //                                     TipoGanado = new TipoGanadoInfo
        //                                         {
        //                                             TipoGanadoID = animales.Field<int>("TipoGanadoID"),
        //                                             Descripcion = animales.Field<string>("TipoGanado"),
        //                                             Sexo = animales.Field<string>("Sexo") == "H" ? Sexo.Hembra : Sexo.Macho
        //                                         },
        //                                     CalidadGanadoID = animales.Field<int>("CalidadGanadoID"),
        //                                     ClasificacionGanado = new ClasificacionGanadoInfo
        //                                             {
        //                                                 ClasificacionGanadoID = animales.Field<int>("ClasificacionGanadoID"),
        //                                                 Descripcion = animales.Field<string>("ClasificacionGanado")
        //                                             },
        //                                     PesoCompra = animales.Field<int>("PesoCompra"),
        //                                     OrganizacionIDEntrada = animales.Field<int>("OrganizacionIDEntrada"),
        //                                     FolioEntrada = Convert.ToInt32(animales.Field<long>("FolioEntrada")),
        //                                     PesoLlegada = animales.Field<int>("PesoLlegada"),
        //                                     Paletas = animales.Field<int>("Paletas"),
        //                                     CausaRechadoID = animales.Field<int?>("CausaRechadoID") != null ? animales.Field<int>("CausaRechadoID") : 0,
        //                                     Venta = animales.Field<bool>("Venta"),
        //                                     Cronico = animales.Field<bool>("Cronico"),
        //                                     DiasEngorda = animales.Field<int>("DiasEngorda")
        //                                 }).ToList();

        //        var listaProyeccion = (from lote in dtLoteProyeccion.AsEnumerable()
        //                               select new LoteProyeccionInfo
        //                                   {
        //                                       LoteProyeccionID = lote.Field<int>("LoteProyeccionID"),
        //                                       LoteID = lote.Field<int>("LoteID"),
        //                                       OrganizacionID = lote.Field<int>("OrganizacionID"),
        //                                       Frame = lote.Field<decimal>("Frame"),
        //                                       GananciaDiaria = lote.Field<decimal>("GananciaDiaria"),
        //                                       ConsumoBaseHumeda = lote.Field<decimal>("ConsumoBaseHumeda"),
        //                                       Conversion = lote.Field<decimal>("Conversion"),
        //                                       PesoMaduro = lote.Field<int>("PesoMaduro"),
        //                                       PesoSacrificio = lote.Field<int>("PesoSacrificio"),
        //                                       DiasEngorda = lote.Field<int>("DiasEngorda"),
        //                                       FechaEntradaZilmax = lote.Field<DateTime>("FechaEntradaZilmax")
        //                                   }).ToList();

        //        var listaReimplante = (from reimplante in dtLoteReimplante.AsEnumerable()
        //                               select new LoteReimplanteInfo
        //                                   {
        //                                       LoteReimplanteID = reimplante.Field<int>("LoteReimplanteID"),
        //                                       LoteProyeccionID = reimplante.Field<int>("LoteProyeccionID"),
        //                                       NumeroReimplante = reimplante.Field<int>("NumeroReimplante"),
        //                                       FechaProyectada = reimplante.Field<DateTime>("FechaProyectada"),
        //                                       PesoProyectado = reimplante.Field<int>("PesoProyectado"),
        //                                       FechaReal = reimplante.Field<DateTime?>("FechaReal") != null ? reimplante.Field<DateTime>("FechaReal") : DateTime.MinValue,
        //                                       PesoReal = reimplante.Field<int?>("PesoReal") != null ? reimplante.Field<int>("PesoReal") : 0
        //                                   }).ToList();

        //        foreach (var corral in listaCorrales)
        //        {
        //            corral.ListaAnimales = listaAnimales.Where(anim => anim.CorralID == corral.CorralID).ToList();
        //            corral.LoteProyeccion = listaProyeccion.FirstOrDefault(pro => pro.LoteID == corral.LoteID);
        //            if (corral.LoteProyeccion != null)
        //            {
        //                corral.LoteProyeccion.ListaReimplantes =
        //                    listaReimplante.Where(
        //                        reim => reim.LoteProyeccionID == corral.LoteProyeccion.LoteProyeccionID).ToList();
        //            }
        //        }
        //        lista = listaCorrales;

        //        #region datos

        //        //lista = (from corral in dt.AsEnumerable()
        //        //         select new ContenedorReporteProyectorInfo
        //        //                    {
        //        //                        CorralID = corral.Field<int>("CorralID"),
        //        //                        CodigoCorral = corral.Field<string>("CodigoCorral"),
        //        //                        LoteID = corral.Field<int>("LoteID"),
        //        //                        CodigoLote = corral.Field<string>("CodigoLote"),
        //        //                        Cabezas = corral.Field<int>("Cabezas"),
        //        //                        TipoGanado = corral.Field<string>("TipoGanado"),
        //        //                        FechaInicioLote = corral.Field<DateTime>("FechaInicio"),
        //        //                        FechaDisponibilidad =
        //        //                            corral.Field<DateTime?>("FechaDisponibilidad") != null
        //        //                                ? corral.Field<DateTime>("FechaDisponibilidad")
        //        //                                : DateTime.MinValue,
        //        //                        DisponibilidadManual = corral.Field<bool>("DisponibilidadManual"),
        //        //                        DiasF4 = corral.Field<int>("DiasF4"),
        //        //                        DiasZilmax = corral.Field<int>("DiasZilmax"),
        //        //                        DiasSacrificio = corral.Field<int>("DiasSacrificio"),
        //        //                        ListaAnimales = (from animales in dtAnimal.AsEnumerable()
        //        //                                         where
        //        //                                             animales.Field<int>("CorralID") ==
        //        //                                             corral.Field<int>("CorralID")
        //        //                                         select new AnimalInfo
        //        //                                                    {
        //        //                                                        CorralID = animales.Field<int>("CorralID"),
        //        //                                                        AnimalID = animales.Field<long>("AnimalID"),
        //        //                                                        Arete = animales.Field<string>("Arete"),
        //        //                                                        AreteMetalico =
        //        //                                                            animales.Field<string>("AreteMetalico"),
        //        //                                                        FechaCompra =
        //        //                                                            animales.Field<DateTime>("FechaCompra"),
        //        //                                                        TipoGanado = new TipoGanadoInfo
        //        //                                                                         {
        //        //                                                                             TipoGanadoID =
        //        //                                                                                 animales.Field<int>(
        //        //                                                                                     "TipoGanadoID"),
        //        //                                                                             Descripcion =
        //        //                                                                                 animales.Field<string>(
        //        //                                                                                     "TipoGanado"),
        //        //                                                                             Sexo =
        //        //                                                                                 animales.Field<string>(
        //        //                                                                                     "Sexo") == "H"
        //        //                                                                                     ? Sexo.Hembra
        //        //                                                                                     : Sexo.Macho
        //        //                                                                         },
        //        //                                                        CalidadGanadoID =
        //        //                                                            animales.Field<int>("CalidadGanadoID"),
        //        //                                                        ClasificacionGanado =
        //        //                                                            new ClasificacionGanadoInfo
        //        //                                                                {
        //        //                                                                    ClasificacionGanadoID =
        //        //                                                                        animales.Field<int>(
        //        //                                                                            "ClasificacionGanadoID"),
        //        //                                                                    Descripcion =
        //        //                                                                        animales.Field<string>(
        //        //                                                                            "ClasificacionGanado")
        //        //                                                                },
        //        //                                                        PesoCompra = animales.Field<int>("PesoCompra"),
        //        //                                                        OrganizacionIDEntrada =
        //        //                                                            animales.Field<int>("OrganizacionIDEntrada"),
        //        //                                                        FolioEntrada =
        //        //                                                            Convert.ToInt32(
        //        //                                                                animales.Field<long>("FolioEntrada")),
        //        //                                                        PesoLlegada = animales.Field<int>("PesoLlegada"),
        //        //                                                        Paletas = animales.Field<int>("Paletas"),
        //        //                                                        CausaRechadoID =
        //        //                                                            animales.Field<int?>("CausaRechadoID") !=
        //        //                                                            null
        //        //                                                                ? animales.Field<int>("CausaRechadoID")
        //        //                                                                : 0,
        //        //                                                        Venta = animales.Field<bool>("Venta"),
        //        //                                                        Cronico = animales.Field<bool>("Cronico"),
        //        //                                                        ListaAnimalesMovimiento =
        //        //                                                            (from animalDetalle in
        //        //                                                                 dtAnimalMovimiento.AsEnumerable()
        //        //                                                             where
        //        //                                                                 animalDetalle.Field<long>("AnimalID") ==
        //        //                                                                 animales.Field<long>("AnimalID")
        //        //                                                             select new AnimalMovimientoInfo
        //        //                                                                        {
        //        //                                                                            AnimalID =
        //        //                                                                                animalDetalle.Field
        //        //                                                                                <long>("AnimalID"),
        //        //                                                                            AnimalMovimientoID =
        //        //                                                                                animalDetalle.Field<long>
        //        //                                                                                ("AnimalMovimientoID"),
        //        //                                                                            OrganizacionID =
        //        //                                                                                animalDetalle.Field<int>
        //        //                                                                                ("OrganizacionID"),
        //        //                                                                            CorralID =
        //        //                                                                                animalDetalle.Field<int>
        //        //                                                                                ("CorralID"),
        //        //                                                                            LoteID =
        //        //                                                                                animalDetalle.Field<int>
        //        //                                                                                ("LoteID"),
        //        //                                                                            FechaMovimiento =
        //        //                                                                                animalDetalle.Field
        //        //                                                                                <DateTime>(
        //        //                                                                                    "FechaMovimiento"),
        //        //                                                                            Peso =
        //        //                                                                                animalDetalle.Field<int>
        //        //                                                                                ("Peso"),
        //        //                                                                            Temperatura =
        //        //                                                                                Convert.ToDouble(
        //        //                                                                                    animalDetalle.Field
        //        //                                                                                        <decimal>(
        //        //                                                                                            "Temperatura")),
        //        //                                                                            TipoMovimientoID =
        //        //                                                                                animalDetalle.Field<int>
        //        //                                                                                ("TipoMovimientoID"),
        //        //                                                                            TrampaID =
        //        //                                                                                animalDetalle.Field<int>
        //        //                                                                                ("TrampaID"),
        //        //                                                                            OperadorID =
        //        //                                                                                animalDetalle.Field<int>
        //        //                                                                                ("OperadorID"),
        //        //                                                                            Observaciones =
        //        //                                                                                animalDetalle.Field
        //        //                                                                                <string>("Observaciones")
        //        //                                                                        }
        //        //                                                            ).ToList()
        //        //                                                    }).ToList(),
        //        //                        LoteProyeccion = (from lote in dtLoteProyeccion.AsEnumerable()
        //        //                                          where lote.Field<int>("LoteID") == corral.Field<int>("LoteID")
        //        //                                          select new LoteProyeccionInfo
        //        //                                                     {
        //        //                                                         LoteProyeccionID =
        //        //                                                             lote.Field<int>("LoteProyeccionID"),
        //        //                                                         LoteID = lote.Field<int>("LoteID"),
        //        //                                                         OrganizacionID =
        //        //                                                             lote.Field<int>("OrganizacionID"),
        //        //                                                         Frame = lote.Field<decimal>("Frame"),
        //        //                                                         GananciaDiaria =
        //        //                                                             lote.Field<decimal>("GananciaDiaria"),
        //        //                                                         ConsumoBaseHumeda =
        //        //                                                             lote.Field<decimal>("ConsumoBaseHumeda"),
        //        //                                                         Conversion = lote.Field<decimal>("Conversion"),
        //        //                                                         PesoMaduro = lote.Field<int>("PesoMaduro"),
        //        //                                                         PesoSacrificio =
        //        //                                                             lote.Field<int>("PesoSacrificio"),
        //        //                                                         DiasEngorda = lote.Field<int>("DiasEngorda"),
        //        //                                                         FechaEntradaZilmax =
        //        //                                                             lote.Field<DateTime>("FechaEntradaZilmax"),
        //        //                                                         ListaReimplantes =
        //        //                                                             (from reimplante in
        //        //                                                                  dtLoteReimplante.AsEnumerable()
        //        //                                                              where
        //        //                                                                  lote.Field<int>("LoteProyeccionID") ==
        //        //                                                                  reimplante.Field<int>(
        //        //                                                                      "LoteProyeccionID")
        //        //                                                              select new LoteReimplanteInfo
        //        //                                                                         {
        //        //                                                                             LoteReimplanteID =
        //        //                                                                                 reimplante.Field<int>(
        //        //                                                                                     "LoteReimplanteID"),
        //        //                                                                             LoteProyeccionID =
        //        //                                                                                 reimplante.Field<int>(
        //        //                                                                                     "LoteProyeccionID"),
        //        //                                                                             NumeroReimplante =
        //        //                                                                                 reimplante.Field<int>(
        //        //                                                                                     "NumeroReimplante"),
        //        //                                                                             FechaProyectada =
        //        //                                                                                 reimplante.Field
        //        //                                                                                 <DateTime>(
        //        //                                                                                     "FechaProyectada"),
        //        //                                                                             PesoProyectado =
        //        //                                                                                 reimplante.Field<int>(
        //        //                                                                                     "PesoProyectado"),
        //        //                                                                             FechaReal =
        //        //                                                                                 reimplante.Field
        //        //                                                                                     <DateTime?>(
        //        //                                                                                         "FechaReal") !=
        //        //                                                                                 null
        //        //                                                                                     ? reimplante.Field
        //        //                                                                                           <DateTime>(
        //        //                                                                                               "FechaReal")
        //        //                                                                                     : DateTime.MinValue,
        //        //                                                                             PesoReal =
        //        //                                                                                 reimplante.Field<int?>(
        //        //                                                                                     "PesoReal") != null
        //        //                                                                                     ? reimplante.Field
        //        //                                                                                           <int>(
        //        //                                                                                               "PesoReal")
        //        //                                                                                     : 0
        //        //                                                                         }).ToList()
        //        //                                                     }).FirstOrDefault()

        //        //                    }).ToList();

        //        #endregion datos
        //    }

        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }

        //    return lista;
        //}

        /// <summary>
        /// Obtiene el numero de cabezas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerContarCabezas(DataSet ds)
        {

            int resp;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

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
        /// Obtiene los datos del corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerPorCodigoGrupo(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Organizacion =
                                     new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                                 Codigo = info.Field<string>("Codigo"),
                                 GrupoCorral = info.Field<int>("GrupoCorralID"),
                                 TipoCorral = new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                                 Capacidad = info.Field<int>("Capacidad"),
                                 MetrosLargo = info.Field<int>("MetrosLargo"),
                                 MetrosAncho = info.Field<long>("MetrosAncho"),
                                 Seccion = info.Field<int>("Seccion"),
                                 Orden = info.Field<int>("Orden"),
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
        /// Obtiene un corral por su codigo y organizacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerPorCodigoOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Codigo = info.Field<string>("Codigo"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 Capacidad = info.Field<int>("Capacidad"),
                                 Organizacion = new OrganizacionInfo
                                                    {
                                                        OrganizacionID = info.Field<int>("OrganizacionID")
                                                    },
                                 TipoCorral = new TipoCorralInfo
                                                  {
                                                      TipoCorralID = info.Field<int>("TipoCorralID")
                                                  },
                                 TipoCorralId = info.Field<int>("TipoCorralID")
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
        /// Obtiene los datos del corral por tipo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<CorralInfo> ObtenerCorralesPorTipo(DataSet ds)
        {
            ResultadoInfo<CorralInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                         },
                                 Codigo = info.Field<String>("Codigo").Trim(),
                                 TipoCorral =
                                     new TipoCorralInfo
                                         {
                                             TipoCorralID = info.Field<int>("TipoCorralID"),
                                         },
                                 Capacidad = info.Field<int>("Capacidad"),
                                 FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                 MetrosAncho = info.Field<long>("MetrosAncho"),
                                 MetrosLargo = info.Field<int>("MetrosLargo"),
                                 Seccion = info.Field<int>("Seccion"),
                                 Orden = info.Field<int>("Orden"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),

                             }).ToList();

                resultado = new ResultadoInfo<CorralInfo>
                                {
                                    Lista = lista,
                                    TotalRegistros = lista.Count

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
        /// Obtiene los datos de un corral 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerCorralEnfermeria(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Codigo = info.Field<string>("Codigo"),
                                 TipoCorral = new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 Capacidad = info.Field<int>("Capacidad"),
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
        /// Obtiene el numero de interfaz salida
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static int ObtenerExisteInterfaceSalida(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var retorno =
                    (from info in dt.AsEnumerable()
                     select info.Field<int>("SalidaID")
                    ).First();
                return retorno;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static CorralInfo ObtenerCorralPorLoteID(DataSet ds)
        {
            CorralInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new CorralInfo
                                        {
                                            Codigo = info.Field<String>("Codigo").Trim(),
                                            CorralID = info.Field<int>("CorralID"),
                                            Organizacion =
                                                new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                                            TipoCorral =
                                                new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                                            GrupoCorral = info.Field<int>("GrupoCorralID"),
                                            Seccion = info.Field<int>("Seccion"),
                                            Orden = info.Field<int>("Orden")
                                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene la informacion de los corrales que se les generara orden de reparto
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<CorralInfo> ObtenerCorralesParaReparto(DataSet ds)
        {
            ResultadoInfo<CorralInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                         },
                                 Codigo = info.Field<String>("Codigo").Trim(),
                                 TipoCorral =
                                     new TipoCorralInfo
                                         {
                                             TipoCorralID = info.Field<int>("TipoCorralID"),
                                             GrupoCorralID = info.Field<int>("GrupoCorralID"),
                                         },
                                 Capacidad = info.Field<int>("Capacidad"),
                                 FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                 MetrosAncho = info.Field<long>("MetrosAncho"),
                                 MetrosLargo = info.Field<int>("MetrosLargo"),
                                 Seccion = info.Field<int>("Seccion"),
                                 Orden = info.Field<int>("Orden"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),

                             }).ToList();

                resultado = new ResultadoInfo<CorralInfo>
                                {
                                    Lista = lista,
                                    TotalRegistros = lista.Count

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
        /// Obtiene el listado de corrales dependiendo del Grupo Corral al que pertenecen
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<CorralInfo> ObtenerPorCorralesPorGrupo(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                         {
                             CorralID = info.Field<int>("CorralID"),
                             Organizacion =
                                 new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                             Codigo = info.Field<string>("Codigo"),
                             GrupoCorral = info.Field<int>("GrupoCorralID"),
                             TipoCorral = new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                             Capacidad = info.Field<int>("Capacidad"),
                             MetrosLargo = info.Field<int>("MetrosLargo"),
                             MetrosAncho = info.Field<long>("MetrosAncho"),
                             Seccion = info.Field<int>("Seccion"),
                             Orden = info.Field<int>("Orden"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
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
        /// Obtiene un corral por su grupo corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerPorGrupoCorral(DataSet ds)
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
                                     Activo = info.Field<bool>("Activo").BoolAEnum(),
                                     Capacidad = info.Field<int>("Capacidad"),
                                     Codigo = info.Field<String>("Codigo").Trim(),
                                     CorralID = info.Field<int>("CorralID"),
                                     MetrosAncho = info.Field<long>("MetrosAncho"),
                                     MetrosLargo = info.Field<int>("MetrosLargo"),
                                     Orden = info.Field<int>("Orden"),
                                     Organizacion =
                                         new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                         },
                                     Seccion = info.Field<int>("Seccion"),
                                     TipoCorral =
                                         new TipoCorralInfo
                                         {
                                             TipoCorralID = info.Field<int>("TipoCorralID"),
                                             Descripcion = info.Field<string>("TipoCorral"),
                                             GrupoCorral = new GrupoCorralInfo
                                                               {
                                                                   GrupoCorralID = info.Field<int>("GrupoCorralID"),
                                                                   Descripcion = info.Field<string>("GrupoCorral")
                                                               }
                                         },
                                 }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una Lista Paginada de Corrales por su Grupo Corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ResultadoInfo<CorralInfo> ObtenerPorPaginaGrupoCorral(DataSet ds)
        {
            ResultadoInfo<CorralInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CorralInfo> corrales = (from info in dt.AsEnumerable()
                                             select
                                                 new CorralInfo
                                                 {
                                                     Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                     Capacidad = info.Field<int>("Capacidad"),
                                                     Codigo = info.Field<String>("Codigo").Trim(),
                                                     CorralID = info.Field<int>("CorralID"),
                                                     MetrosAncho = info.Field<long>("MetrosAncho"),
                                                     MetrosLargo = info.Field<int>("MetrosLargo"),
                                                     Orden = info.Field<int>("Orden"),
                                                     Organizacion =
                                                         new OrganizacionInfo
                                                         {
                                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                                         },
                                                     Seccion = info.Field<int>("Seccion"),
                                                     TipoCorral =
                                                         new TipoCorralInfo
                                                         {
                                                             TipoCorralID = info.Field<int>("TipoCorralID"),
                                                             Descripcion = info.Field<string>("TipoCorral"),
                                                             GrupoCorral = new GrupoCorralInfo
                                                             {
                                                                 GrupoCorralID = info.Field<int>("GrupoCorralID"),
                                                                 Descripcion = info.Field<string>("GrupoCorral")
                                                             }
                                                         },
                                                 }).ToList();

                resultado = new ResultadoInfo<CorralInfo>
                                {
                                    Lista = corrales,
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
        /// Obtiene una entidad de corral por su descripcion y organizacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerPorDescripcionOrganizacion(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                         {
                             CorralID = info.Field<int>("CorralID"),
                             Organizacion =
                                 new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                             Codigo = info.Field<string>("Codigo"),
                             TipoCorral = new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                             Capacidad = info.Field<int>("Capacidad"),
                             MetrosLargo = info.Field<int>("MetrosLargo"),
                             MetrosAncho = info.Field<long>("MetrosAncho"),
                             Seccion = info.Field<int>("Seccion"),
                             Orden = info.Field<int>("Orden"),
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

        internal static List<CorralInfo> ObtenerCorralesPorTipoCorral(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                         {
                             CorralID = info.Field<int>("CorralID"),
                             Organizacion =
                                 new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                             Codigo = info.Field<string>("Codigo"),
                             TipoCorral = new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                             Capacidad = info.Field<int>("Capacidad"),
                             MetrosLargo = info.Field<int>("MetrosLargo"),
                             MetrosAncho = info.Field<long>("MetrosAncho"),
                             Seccion = info.Field<int>("Seccion"),
                             Orden = info.Field<int>("Orden"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static List<CorralInfo> ObtenerCorralesPorCodigosCorral(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                         {
                             CorralID = info.Field<int>("CorralID"),
                             Organizacion =
                                 new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                             Codigo = info.Field<string>("Codigo"),
                             TipoCorral = new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                             Capacidad = info.Field<int>("Capacidad"),
                             MetrosLargo = info.Field<int>("MetrosLargo"),
                             MetrosAncho = info.Field<long>("MetrosAncho"),
                             Seccion = info.Field<int>("Seccion"),
                             Orden = info.Field<int>("Orden"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static ResultadoInfo<CorralInfo> ObtenerPorPaginaGruposCorrales(DataSet ds)
        {
            ResultadoInfo<CorralInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                List<CorralInfo> corrales = (from info in dt.AsEnumerable()
                                             select
                                                 new CorralInfo
                                                 {
                                                     Activo = info.Field<bool>("Activo").BoolAEnum(),
                                                     Capacidad = info.Field<int>("Capacidad"),
                                                     Codigo = info.Field<String>("Codigo").Trim(),
                                                     CorralID = info.Field<int>("CorralID"),
                                                     MetrosAncho = info.Field<long>("MetrosAncho"),
                                                     MetrosLargo = info.Field<int>("MetrosLargo"),
                                                     Orden = info.Field<int>("Orden"),
                                                     Organizacion =
                                                         new OrganizacionInfo
                                                         {
                                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                                         },
                                                     Seccion = info.Field<int>("Seccion"),
                                                     TipoCorral =
                                                         new TipoCorralInfo
                                                         {
                                                             TipoCorralID = info.Field<int>("TipoCorralID"),
                                                             GrupoCorral = new GrupoCorralInfo
                                                             {
                                                                 GrupoCorralID = info.Field<int>("GrupoCorralID"),
                                                             }
                                                         },
                                                 }).ToList();

                resultado = new ResultadoInfo<CorralInfo>
                {
                    Lista = corrales,
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
        ///     Metodo que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerPorCodigoCorral(DataSet ds)
        {
            CorralInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                         {
                             CorralID = info.Field<int>("CorralID"),
                             Organizacion =
                                 new OrganizacionInfo
                                 {
                                     OrganizacionID = info.Field<int>("OrganizacionID"),
                                     Descripcion = info.Field<string>("Organizacion")
                                 },
                             Codigo = info.Field<String>("Codigo").Trim(),
                             TipoCorral =
                                 new TipoCorralInfo
                                 {
                                     TipoCorralID = info.Field<int>("TipoCorralID"),
                                     Descripcion = info.Field<string>("TipoCorral")
                                 },
                             Capacidad = info.Field<int>("Capacidad"),
                             FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                             MetrosAncho = info.Field<long>("MetrosAncho"),
                             MetrosLargo = info.Field<int>("MetrosLargo"),
                             Seccion = info.Field<int>("Seccion"),
                             Orden = info.Field<int>("Orden"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),

                         }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una lista con los corrales
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<CorralInfo> ObtenerCorralesPorCorralIdXML(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 Codigo = info.Field<string>("Codigo"),
                                 TipoCorral = new TipoCorralInfo
                                                  {
                                                      TipoCorralID = info.Field<int>("TipoCorralID"),
                                                      Descripcion = info.Field<string>("TipoCorral")
                                                  },
                                 Capacidad = info.Field<int>("Capacidad"),
                                 MetrosLargo = info.Field<int>("MetrosLargo"),
                                 MetrosAncho = info.Field<long>("MetrosAncho"),
                                 Seccion = info.Field<int>("Seccion"),
                                 Orden = info.Field<int>("Orden"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 GrupoCorral = info.Field<int>("GrupoCorralID")
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
        /// Obtiene un corral que tenga existencia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static CorralInfo ObtenerValidaCorralConLoteConExistenciaActivo(DataSet ds)
        {
            CorralInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                             {
                                 CorralID = info.Field<int>("CorralID"),
                                 Organizacion =
                                     new OrganizacionInfo
                                         {
                                             OrganizacionID = info.Field<int>("OrganizacionID"),
                                             Descripcion = info.Field<string>("Organizacion")
                                         },
                                 Codigo = info.Field<String>("Codigo").Trim(),
                                 TipoCorral =
                                     new TipoCorralInfo
                                         {
                                             TipoCorralID = info.Field<int>("TipoCorralID"),
                                             Descripcion = info.Field<string>("TipoCorral")
                                         },
                                 Capacidad = info.Field<int>("Capacidad"),
                                 FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                 MetrosAncho = info.Field<long>("MetrosAncho"),
                                 MetrosLargo = info.Field<int>("MetrosLargo"),
                                 Seccion = info.Field<int>("Seccion"),
                                 Orden = info.Field<int>("Orden"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
        /// <summary>
        /// Se agrega consulta para el reporte de proyector y comportamiento
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ReporteProyectorInfo> ObtenerReporteProyectorComportamiento(DataSet ds)
        {
            List<ReporteProyectorInfo> lista;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                lista =
                    (from info in dt.AsEnumerable()
                     select new ReporteProyectorInfo
                     {
                         Corral = info.Field<string>("CodigoCorral"),
                         Lote = info.Field<string>("CodigoLote"),
                         Cabezas = info.Field<int>("Cabezas"),
                         TipoGanado = info.Field<string>("TipoGanado"),
                         Clasificacion = info.Field<string>("Clasificacion"),
                         PesoOrigen = info.Field<int>("PesoOrigen"),
                         Merma = info.Field<decimal>("Merma"),
                         PesoProyectado = info.Field<int>("PesoProyectado"),
                         GananciaDiaria = info.Field<decimal>("GananciaDiaria"),
                         DiasEngorda = info.Field<int>("DiasEngorda"),
                         Fecha1Reimplante = info["FechaReimplante1"] == DBNull.Value ? String.Empty : info.Field<DateTime>("FechaReimplante1").ToString("dd/MM/yyyy"),
                         Peso1Reimplante = info.Field<int>("PesoReimplante1"),
                         Ganancia1Diaria = info.Field<decimal>("GananciaReimplante1"),
                         Fecha2Reimplante = info["FechaReimplante2"] == DBNull.Value ? String.Empty : info.Field<DateTime>("FechaReimplante2").ToString("dd/MM/yyyy"),
                         Peso2Reimplante = info.Field<int>("PesoReimplante2"),
                         Ganancia2Diaria = info.Field<decimal>("GananciaReimplante2"),
                         Fecha3Reimplante = info["FechaReimplante3"] == DBNull.Value ? String.Empty : info.Field<DateTime>("FechaReimplante3").ToString("dd/MM/yyyy"),
                         Peso3Reimplante = info.Field<int>("PesoReimplante3"),
                         Ganancia3Diaria = info.Field<decimal>("GananciaReimplante3"),
                         DiasF4 = info.Field<int>("DiasF4"),
                         DiasZilmax = info.Field<int>("DiasZilmax"),
                         FechaSacrificio = info["FechaSacrificio"] == DBNull.Value ? String.Empty : info.Field<DateTime>("FechaSacrificio").ToString("dd/MM/yyyy"),
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
        /// Obtiene un corral que tenga existencia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<SeccionModel> ObtenerSeccionesCorral(DataSet ds)
        {
            List<SeccionModel> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new SeccionModel
                         {
                             Seccion = info.Field<int>("Seccion"),
                             Descripcion = info.Field<int>("Seccion").ToString(CultureInfo.InvariantCulture)
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene la lista de los corrales improductivos para la pantalla Corte por Transferencia
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<CorralInfo> ObtenerCorralesImproductivos(DataSet ds)
        {
            List<CorralInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                         {
                             CorralID = info.Field<int>("CorralID"),
                             Organizacion =
                                 new OrganizacionInfo
                                 {
                                     OrganizacionID = info.Field<int>("OrganizacionID"),
                                 },
                             Codigo = info.Field<String>("Codigo").Trim(),
                             TipoCorral =
                                 new TipoCorralInfo
                                 {
                                     TipoCorralID = info.Field<int>("TipoCorralID"),
                                 },
                             Capacidad = info.Field<int>("Capacidad"),
                             MetrosAncho = info.Field<long>("MetrosAncho"),
                             MetrosLargo = info.Field<int>("MetrosLargo"),
                             Seccion = info.Field<int>("Seccion"),
                             Orden = info.Field<int>("Orden"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                         }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal static CorralInfo ObtenerCorralFormulaPorId(DataSet ds)
        {
            CorralInfo resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new CorralInfo
                             {
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 Capacidad = info.Field<int>("Capacidad"),
                                 Codigo = info.Field<String>("Codigo").Trim(),
                                 CorralID = info.Field<int>("CorralID"),
                                 FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                 MetrosAncho = info.Field<long>("MetrosAncho"),
                                 MetrosLargo = info.Field<int>("MetrosLargo"),
                                 Orden = info.Field<int>("Orden"),
                                 Organizacion =
                                     new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                                 Seccion = info.Field<int>("Seccion"),
                                 TipoCorral =
                                     new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                                 UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
                             }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal static ResultadoInfo<CorralInfo> ObtenerInformacionCorraletasDisponiblesSacrificio(DataSet ds)
        {
            ResultadoInfo<CorralInfo> resultado = null;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var corrales = (from info in dt.AsEnumerable()
                                select new CorralInfo
                                {
                                    Activo = info.Field<bool>("Activo").BoolAEnum(),
                                    Capacidad = info.Field<int>("Capacidad"),
                                    Codigo = info.Field<String>("Codigo").Trim(),
                                    CorralID = info.Field<int>("CorralID"),
                                    FechaCreacion = info.Field<DateTime>("FechaCreacion"),
                                    MetrosAncho = info.Field<long>("MetrosAncho"),
                                    MetrosLargo = info.Field<int>("MetrosLargo"),
                                    Orden = info.Field<int>("Orden"),
                                    Organizacion =
                                        new OrganizacionInfo { OrganizacionID = info.Field<int>("OrganizacionID"), },
                                    Seccion = info.Field<int>("Seccion"),
                                    TipoCorral =
                                        new TipoCorralInfo { TipoCorralID = info.Field<int>("TipoCorralID"), },
                                    UsuarioCreacionID = info.Field<int>("UsuarioCreacionID")
                                    //,GrupoCorral = info.Field<int>("GrupoCorralID")
                                }).ToList();

                resultado = new ResultadoInfo<CorralInfo> { TotalRegistros = corrales.Count(), Lista = corrales };

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal static IList<DiasEngordaLoteModel> ObtenerDiasEngordaPorLoteXML(DataSet ds)
        {
            IList<DiasEngordaLoteModel> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from info in dt.AsEnumerable()
                             select new DiasEngordaLoteModel
                             {
                                 LoteID = info.Field<int>("LoteID"),
                                 DiasEngorda = info.Field<int?>("DiasEngorda") != null ? info.Field<int>("DiasEngorda") : 0
                             }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene corral por codigo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static CorralInfo ObtenerCorralesPorTipos(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralInfo
                         {
                             CorralID = info.Field<int>("CorralID"),
                             Codigo = info.Field<string>("Codigo"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             Capacidad = info.Field<int>("Capacidad"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID")
                             },
                             TipoCorral = new TipoCorralInfo
                             {
                                 TipoCorralID = info.Field<int>("TipoCorralID")
                             },
                             TipoCorralId = info.Field<int>("TipoCorralID")
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
        /// Obtiene la Entrada de Ganado del corral
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static EntradaGanadoInfo ObtenerPartidaCorral(DataSet ds)
        {
            EntradaGanadoInfo resultado;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resultado = (from entradaInfo in dt.AsEnumerable()
                             select new EntradaGanadoInfo
                             {
                                 FolioEntrada = entradaInfo.Field<int>("FolioEntrada"),
                                 OrganizacionOrigenID = entradaInfo.Field<int>("OrganizacionID"),
                                 OrganizacionOrigen = entradaInfo.Field<string>("Organizacion"),
                                 TipoOrganizacionOrigenId = entradaInfo.Field<int>("TipoOrganizacionID"),
                             }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        public static IList<CorralesPorOrganizacionInfo> ObtenerPorUsuarioId(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CorralesPorOrganizacionInfo
                         {
                             ParametroID = info.Field<int>("ParametroID"),
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             EntradaGanadoTransitoID = info.Field<int>("EntradaGanadoTransitoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Valor = info.Field<string>("Valor"),
                             CorralID = info.Field<int>("CorralID"),
                             LoteID = info.Field<int>("LoteID"),
                             Cabezas = info.Field<int>("Cabezas"),
                             PesoPromedio = info.Field<int>("Peso_Promedio"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static IList<CostoCorralInfo> ObtenerPorEntradaGanadoTransito(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new CostoCorralInfo()
                         {
                             CostoID = info.Field<int>("CostoID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             Importe = info.Field<decimal>("Importe")
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