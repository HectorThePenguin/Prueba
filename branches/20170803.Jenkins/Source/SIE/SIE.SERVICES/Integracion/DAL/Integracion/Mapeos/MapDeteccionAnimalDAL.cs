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
    public class MapDeteccionAnimalDAL
    {
        /// <summary>
        /// Método que obtiene una lista paginada
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static ResultadoInfo<DeteccionAnimalInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<DeteccionAnimalInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new DeteccionAnimalInfo
                             {
								DeteccionAnimalID = info.Field<int>("DeteccionAnimalID"),
								AnimalMovimientoID = info.Field<long>("AnimalMovimientoID"),
								Arete = info.Field<string>("Arete"),
								AreteMetalico = info.Field<string>("AreteMetalico"),
								FotoDeteccion = info.Field<string>("FotoDeteccion"),
								Lote = new LoteInfo { LoteID = info.Field<int>("LoteID"), Lote = info.Field<string>("Lote") },
								Operador = new OperadorInfo { OperadorID = info.Field<int>("OperadorID"), Nombre = info.Field<string>("Operador") },
								TipoDeteccion = new TipoDeteccionInfo { TipoDeteccionID = info.Field<int>("TipoDeteccionID"), Descripcion = info.Field<string>("TipoDeteccion") },
								Grado = new GradoInfo { GradoID = info.Field<int>("GradoID"), Descripcion = info.Field<string>("Grado") },
								Observaciones = info.Field<string>("Observaciones"),
								NoFierro = info.Field<string>("NoFierro"),
								FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
								DeteccionAnalista = info.Field<bool>("DeteccionAnalista"),
								Activo = info.Field<bool>("Activo").BoolAEnum(),
                             }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<DeteccionAnimalInfo>
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
        public static List<DeteccionAnimalInfo> ObtenerTodos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<DeteccionAnimalInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new DeteccionAnimalInfo
                             {
								DeteccionAnimalID = info.Field<int>("DeteccionAnimalID"),
								AnimalMovimientoID = info.Field<long>("AnimalMovimientoID"),
								Arete = info.Field<string>("Arete"),
								AreteMetalico = info.Field<string>("AreteMetalico"),
								FotoDeteccion = info.Field<string>("FotoDeteccion"),
								Lote = new LoteInfo { LoteID = info.Field<int>("LoteID"), Lote = info.Field<string>("Lote") },
								Operador = new OperadorInfo { OperadorID = info.Field<int>("OperadorID"), Nombre = info.Field<string>("Operador") },
								TipoDeteccion = new TipoDeteccionInfo { TipoDeteccionID = info.Field<int>("TipoDeteccionID"), Descripcion = info.Field<string>("TipoDeteccion") },
								Grado = new GradoInfo { GradoID = info.Field<int>("GradoID"), Descripcion = info.Field<string>("Grado") },
								Observaciones = info.Field<string>("Observaciones"),
								NoFierro = info.Field<string>("NoFierro"),
								FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
								DeteccionAnalista = info.Field<bool>("DeteccionAnalista"),
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
        public static DeteccionAnimalInfo ObtenerPorID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DeteccionAnimalInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new DeteccionAnimalInfo
                             {
								DeteccionAnimalID = info.Field<int>("DeteccionAnimalID"),
								AnimalMovimientoID = info.Field<long>("AnimalMovimientoID"),
								Arete = info.Field<string>("Arete"),
								AreteMetalico = info.Field<string>("AreteMetalico"),
								FotoDeteccion = info.Field<string>("FotoDeteccion"),
                                Lote = new LoteInfo { LoteID = info.Field<int>("LoteID"), Lote = info.Field<string>("Lote") },
                                Operador = new OperadorInfo { OperadorID = info.Field<int>("OperadorID"), Nombre = info.Field<string>("Operador") },
								TipoDeteccion = new TipoDeteccionInfo { TipoDeteccionID = info.Field<int>("TipoDeteccionID"), Descripcion = info.Field<string>("TipoDeteccion") },
								Grado = new GradoInfo { GradoID = info.Field<int>("GradoID"), Descripcion = info.Field<string>("Grado") },
								Observaciones = info.Field<string>("Observaciones"),
								NoFierro = info.Field<string>("NoFierro"),
								FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
								DeteccionAnalista = info.Field<bool>("DeteccionAnalista"),
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
        public static DeteccionAnimalInfo ObtenerPorDescripcion(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DeteccionAnimalInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new DeteccionAnimalInfo
                             {
								DeteccionAnimalID = info.Field<int>("DeteccionAnimalID"),
								AnimalMovimientoID = info.Field<long>("AnimalMovimientoID"),
								Arete = info.Field<string>("Arete"),
								AreteMetalico = info.Field<string>("AreteMetalico"),
								FotoDeteccion = info.Field<string>("FotoDeteccion"),
                                Lote = new LoteInfo { LoteID = info.Field<int>("LoteID"), Lote = info.Field<string>("Lote") },
                                Operador = new OperadorInfo { OperadorID = info.Field<int>("OperadorID"), Nombre = info.Field<string>("Operador") },
								TipoDeteccion = new TipoDeteccionInfo { TipoDeteccionID = info.Field<int>("TipoDeteccionID"), Descripcion = info.Field<string>("TipoDeteccion") },
								Grado = new GradoInfo { GradoID = info.Field<int>("GradoID"), Descripcion = info.Field<string>("Grado") },
								Observaciones = info.Field<string>("Observaciones"),
								NoFierro = info.Field<string>("NoFierro"),
								FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
								DeteccionAnalista = info.Field<bool>("DeteccionAnalista"),
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
        public static DeteccionAnimalInfo ObtenerPorAnimalMovimientoID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                DeteccionAnimalInfo entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new DeteccionAnimalInfo
                         {
                             DeteccionAnimalID = info.Field<int>("DeteccionAnimalID"),
                             AnimalMovimientoID = info.Field<long>("AnimalMovimientoID"),
                             Arete = info.Field<string>("Arete"),
                             AreteMetalico = info.Field<string>("AreteMetalico"),
                             FotoDeteccion = info.Field<string>("FotoDeteccion"),
                             Lote = new LoteInfo { LoteID = info.Field<int>("LoteID"), Lote = info.Field<string>("Lote"), TipoCorralID = info.Field<int>("TipoCorralID")},
                             Operador = new OperadorInfo { OperadorID = info.Field<int>("OperadorID"), Nombre = info.Field<string>("Operador") },
                             TipoDeteccion = new TipoDeteccionInfo { TipoDeteccionID = info.Field<int>("TipoDeteccionID"), Descripcion = info.Field<string>("TipoDeteccion") },
                             Grado = new GradoInfo { GradoID = info.Field<int>("GradoID"), Descripcion = info.Field<string>("Grado") },
                             Observaciones = info.Field<string>("Observaciones"),
                             NoFierro = info.Field<string>("NoFierro"),
                             FechaDeteccion = info.Field<DateTime>("FechaDeteccion"),
                             DeteccionAnalista = info.Field<bool>("DeteccionAnalista"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             GrupoCorralID = info.Field<int>("GrupoCorralID")
                         }).First();
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

