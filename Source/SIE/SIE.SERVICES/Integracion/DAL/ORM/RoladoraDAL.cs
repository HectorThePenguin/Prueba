using System;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Collections.Generic;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class RoladoraDAL : BaseDAL
    {
        RoladoraAccessor roladoraAccessor;

        protected override void inicializar()
        {
            roladoraAccessor = da.inicializarAccessor<RoladoraAccessor>();
        }

        protected override void destruir()
        {
            roladoraAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de Roladora
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<RoladoraInfo> ObtenerPorPagina(PaginacionInfo pagina, RoladoraInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<RoladoraInfo> result = new ResultadoInfo<RoladoraInfo>();
                var condicion = da.Tabla<RoladoraInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.RoladoraID > 0)
                {
                    condicion = condicion.Where(e=> e.RoladoraID == filtro.RoladoraID);
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

                CargarOrganizaciones(result.Lista);

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
        /// Carga los objetos de Organizacion
        /// </summary>
        /// <param name="lista"></param>
        private void CargarOrganizaciones(IList<RoladoraInfo> lista)
        {
            var organizacionDAL = new OrganizacionDAL();

            List<OrganizacionInfo> organizaciones = organizacionDAL.ObtenerTodos();

            foreach (var roladora in lista)
            {
                OrganizacionInfo organizacion = organizaciones.FirstOrDefault(or => or.OrganizacionID == roladora.OrganizacionID);

                if (organizacion == null)
                {
                    continue;
                }
                roladora.Organizacion = organizacion;
            }
        }

        /// <summary>
        /// Obtiene una lista de Roladora
        /// </summary>
        /// <returns></returns>
        public IQueryable<RoladoraInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<RoladoraInfo>();
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
        /// Obtiene una lista de Roladora filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<RoladoraInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de Roladora por su Id
        /// </summary>
        /// <param name="roladoraId">Obtiene una entidad Roladora por su Id</param>
        /// <returns></returns>
        public RoladoraInfo ObtenerPorID(int roladoraId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.RoladoraID == roladoraId).FirstOrDefault();
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
        /// Obtiene una entidad de Roladora por su descripcion
        /// </summary>
        /// <param name="descripción">Obtiene una entidad Roladora por su descripcion</param>
        /// <returns></returns>
        public RoladoraInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad Roladora
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(RoladoraInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.RoladoraID > 0)
                {
                    id = da.Actualizar<RoladoraInfo>(info);
                    roladoraAccessor.ActualizarFechaModificacion(info.RoladoraID);
                }
                else
                {
                    id = da.Insertar<RoladoraInfo>(info);
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
    }
}
