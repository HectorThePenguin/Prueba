using System;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class AlmacenDAL : BaseDAL
    {
        AlmacenAccessor almacenAccessor;

        protected override void inicializar()
        {
            almacenAccessor = da.inicializarAccessor<AlmacenAccessor>();
        }

        protected override void destruir()
        {
            almacenAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de Almacen
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenInfo> ObtenerPorPagina(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<AlmacenInfo>();
                var condicion = da.Tabla<AlmacenInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.AlmacenID > 0)
                {
                    condicion = condicion.Where(e=> e.AlmacenID == filtro.AlmacenID);
                }
                if (!string.IsNullOrEmpty(filtro.Descripcion))
                {
                    condicion = condicion.Where(e=> e.Descripcion.Contains(filtro.Descripcion));
                }
                result.TotalRegistros = condicion.Count();
                
                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = condicion
                                .OrderBy(e => e.Descripcion)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);

                result.Lista = paginado.ToList();

                return result;
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Almacen
        /// </summary>
        /// <returns></returns>
        public IQueryable<AlmacenInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<AlmacenInfo>();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de Almacen filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<AlmacenInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Activo == estatus);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de Almacen por su Id
        /// </summary>
        /// <param name="almacenId">Obtiene una entidad Almacen por su Id</param>
        /// <returns></returns>
        public AlmacenInfo ObtenerPorID(int almacenId)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.AlmacenID == almacenId);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de Almacen por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Almacen por su descripcion</param>
        /// <returns></returns>
        public AlmacenInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.Descripcion.ToLower() == descripcion.ToLower());
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Almacen
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.AlmacenID > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    id = da.Actualizar<AlmacenInfo>(info);
                }
                else
                {
                    id = da.Insertar<AlmacenInfo>(info);
                }
                return id;
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Consultar el Almacen General de una Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public AlmacenInfo ObtenerAlmacenGeneralOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                AlmacenInfo almacenGeneral = da.Tabla<AlmacenInfo>().FirstOrDefault(a => a.OrganizacionID == organizacionID
                                                                                              && a.TipoAlmacenID == (int)TipoAlmacenEnum.GeneralGanadera);
                return almacenGeneral;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Consultar el Almacen General de una Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoAlmacen"></param>
        /// <returns></returns>
        public AlmacenInfo ObtenerAlmacenOrganizacionTipo(int organizacionID, TipoAlmacenEnum tipoAlmacen)
        {
            try
            {
                Logger.Info();
                AlmacenInfo almacenGeneral = da.Tabla<AlmacenInfo>().FirstOrDefault(a => a.OrganizacionID == organizacionID
                                                                                              && a.TipoAlmacenID == (int)tipoAlmacen);
                return almacenGeneral;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
