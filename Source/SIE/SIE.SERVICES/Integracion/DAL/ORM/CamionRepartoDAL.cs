using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using System.Data.SqlClient;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class CamionRepartoDAL : BaseDAL
    {
        CamionRepartoAccessor camionRepartoAccessor;

        protected override void inicializar()
        {
            camionRepartoAccessor = da.inicializarAccessor<CamionRepartoAccessor>();
        }

        protected override void destruir()
        {
            camionRepartoAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CamionReparto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CamionRepartoInfo> ObtenerPorPagina(PaginacionInfo pagina, CamionRepartoInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<CamionRepartoInfo>();
                var condicion = da.Tabla<CamionRepartoInfo>().Where(e => e.Activo == filtro.Activo);
                if (filtro.CamionRepartoID > 0)
                {
                    condicion = condicion.Where(e => e.CamionRepartoID == filtro.CamionRepartoID);
                }
                if (!string.IsNullOrEmpty(filtro.NumeroEconomico))
                {
                    condicion = condicion.Where(e => e.NumeroEconomico.Contains(filtro.NumeroEconomico));
                }
                if (filtro.Organizacion != null && filtro.Organizacion.OrganizacionID > 0)
                {
                    condicion = condicion.Where(e => e.OrganizacionID == filtro.Organizacion.OrganizacionID);
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
                                .OrderBy(e => e.CamionRepartoID)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);

                result.Lista = paginado.ToList();
                CargarOrganizaciones(result.Lista);
                CargarCentrosCosto(result.Lista);
                return result;
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

        private void CargarOrganizaciones(IList<CamionRepartoInfo> lista)
        {
            var organizacionDAL = new OrganizacionDAL();
            IList<OrganizacionInfo> listaOrganizaciones = organizacionDAL.ObtenerTodos(EstatusEnum.Activo);
            foreach (var camionReparto in lista)
            {
                OrganizacionInfo organizacion =
                    listaOrganizaciones.FirstOrDefault(org => org.OrganizacionID == camionReparto.OrganizacionID);
                if (organizacion == null)
                {
                    continue;
                }
                camionReparto.Organizacion = organizacion;
            }
        }

        private void CargarCentrosCosto(IList<CamionRepartoInfo> lista)
        {
            var centroCostoDAL = new CentroCostoDAL();
            IList<CentroCostoInfo> listaCentroCostos = centroCostoDAL.ObtenerTodos(EstatusEnum.Activo).ToList();
            foreach (var camionReparto in lista)
            {
                CentroCostoInfo centroCosto =
                    listaCentroCostos.FirstOrDefault(centro => centro.CentroCostoID == camionReparto.CentroCostoID);
                if (centroCosto == null)
                {
                    continue;
                }
                camionReparto.CentroCosto = centroCosto;
            }
        }

        /// <summary>
        /// Obtiene una lista de CamionReparto
        /// </summary>
        /// <returns></returns>
        public IQueryable<CamionRepartoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CamionRepartoInfo>();
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
        /// Obtiene una lista de CamionReparto filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CamionRepartoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e => e.Activo == estatus);
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
        /// Obtiene una entidad de CamionReparto por su Id
        /// </summary>
        /// <param name="camionRepartoId">Obtiene una entidad CamionReparto por su Id</param>
        /// <returns></returns>
        public CamionRepartoInfo ObtenerPorID(int camionRepartoId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e => e.CamionRepartoID == camionRepartoId).FirstOrDefault();
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
        /// Obtiene una entidad de CamionReparto por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad CamionReparto por su descripcion</param>
        /// <returns></returns>
        public CamionRepartoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e => e.NumeroEconomico.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad CamionReparto
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CamionRepartoInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CamionRepartoID > 0)
                {
                    id = da.Actualizar<CamionRepartoInfo>(info);
                    camionRepartoAccessor.ActualizaFechaModificacion(info.CamionRepartoID);
                }
                else
                {
                    id = da.Insertar<CamionRepartoInfo>(info);
                }
                return id;
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
        /// Obtiene una lista de CamionReparto
        /// </summary>
        /// <returns></returns>
        public CamionRepartoInfo ObtenerPorNumeroEconomico(string numeroEconomico, int organizacionID)
        {
            try
            {
                Logger.Info();
                return da.Tabla<CamionRepartoInfo>().FirstOrDefault(camion => camion.OrganizacionID == organizacionID && 
                    camion.NumeroEconomico.ToUpper().Trim().Equals(numeroEconomico.ToUpper().Trim()) &&
                    camion.Activo == EstatusEnum.Activo);
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
        /// Obtiene una lista de CamionReparto
        /// </summary>
        /// <returns></returns>
        public IQueryable<CamionRepartoInfo> ObtenerPorNumeroEconomicoBusqueda(string numeroEconomico, int organizacionID)
        {
            try
            {
                Logger.Info();
                return da.Tabla<CamionRepartoInfo>().Where(camion => camion.OrganizacionID == organizacionID &&
                    camion.NumeroEconomico.ToUpper().Trim().Contains(numeroEconomico.ToUpper().Trim()) &&
                    camion.Activo == EstatusEnum.Activo);
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
        /// Obtiene una lista de CamionReparto
        /// </summary>
        /// <returns></returns>
        public IQueryable<CamionRepartoInfo> ObtenerPorOrganizacionID(int organizacionID)
        {
            try
            {
                Logger.Info();
                return da.Tabla<CamionRepartoInfo>().Where(camion=> camion.OrganizacionID == organizacionID);
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

        public CamionRepartoInfo ObtenerCamionRepartoPorID(CamionRepartoInfo camionReparto)
        {
            try
            {
                Logger.Info();
                return da.Tabla<CamionRepartoInfo>().FirstOrDefault(camion => camion.OrganizacionID == camionReparto.Organizacion.OrganizacionID &&
                    camion.Activo == EstatusEnum.Activo && camion.CamionRepartoID==camionReparto.CamionRepartoID);
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
