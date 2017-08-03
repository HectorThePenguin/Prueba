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
    internal class CuentaSAPDAL : BaseDAL
    {
        CuentaSAPAccessor cuentaSAPAccessor;

        protected override void inicializar()
        {
            cuentaSAPAccessor = da.inicializarAccessor<CuentaSAPAccessor>();
        }

        protected override void destruir()
        {
            cuentaSAPAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de CuentaSAP
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CuentaSAPInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<CuentaSAPInfo>();
                var condicion = da.Tabla<CuentaSAPInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.CuentaSAPID > 0)
                {
                    condicion = condicion.Where(e=> e.CuentaSAPID == filtro.CuentaSAPID);
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
        /// Obtiene una lista de CuentaSAP
        /// </summary>
        /// <returns></returns>
        public IQueryable<CuentaSAPInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<CuentaSAPInfo>();
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
        /// Obtiene una lista de CuentaSAP filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<CuentaSAPInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de CuentaSAP por su Id
        /// </summary>
        /// <param name="cuentaSAPId">Obtiene una entidad CuentaSAP por su Id</param>
        /// <returns></returns>
        public CuentaSAPInfo ObtenerPorID(int cuentaSAPId)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.CuentaSAPID == cuentaSAPId);
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
        /// Obtiene una entidad de CuentaSAP por su cuenta contable
        /// </summary>
        /// <param name="cuentaSAP">Obtiene una entidad CuentaSAP por su cuenta contable</param>
        /// <returns></returns>
        public CuentaSAPInfo ObtenerPorCuentaSAP(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                return ObtenerTodos().FirstOrDefault(e => e.CuentaSAP == cuentaSAP.CuentaSAP);
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
        /// Obtiene una entidad de CuentaSAP por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad CuentaSAP por su descripcion</param>
        /// <returns></returns>
        public CuentaSAPInfo ObtenerPorDescripcion(string descripcion)
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
        /// Metodo para Guardar/Modificar una entidad CuentaSAP
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CuentaSAPInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.CuentaSAPID > 0)
                {
                    id = da.Actualizar<CuentaSAPInfo>(info);
                }
                else
                {
                    id = da.Insertar<CuentaSAPInfo>(info);
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
