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

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class ParametroGeneralDAL : BaseDAL
    {
        ParametroGeneralAccessor parametroGeneralAccessor;

        protected override void inicializar()
        {
            parametroGeneralAccessor = da.inicializarAccessor<ParametroGeneralAccessor>();
        }

        protected override void destruir()
        {
            parametroGeneralAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de ParametroGeneral
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ParametroGeneralInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroGeneralInfo filtro)
        {
            try
            {
                Logger.Info();
                ResultadoInfo<ParametroGeneralInfo> result = new ResultadoInfo<ParametroGeneralInfo>();
                var condicion = da.Tabla<ParametroGeneralInfo>().Where(e=> e.Activo == filtro.Activo);
                if (filtro.ParametroGeneralID > 0)
                {
                    condicion = condicion.Where(e=> e.ParametroGeneralID == filtro.ParametroGeneralID);
                }
                //if (!string.IsNullOrEmpty(filtro.ParametroGeneralID))
                //{
                //    condicion = condicion.Where(e=> e.ParametroGeneralID.Contains(filtro.ParametroGeneralID));
                //}
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
                                .OrderBy(e => e.ParametroGeneralID)
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
        /// Obtiene una lista de ParametroGeneral
        /// </summary>
        /// <returns></returns>
        public IQueryable<ParametroGeneralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<ParametroGeneralInfo>();
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
        /// Obtiene una lista de ParametroGeneral filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<ParametroGeneralInfo> ObtenerTodos(EstatusEnum estatus)
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
        /// Obtiene una entidad de ParametroGeneral por su Id
        /// </summary>
        /// <param name="parametroGeneralId">Obtiene una entidad ParametroGeneral por su Id</param>
        /// <returns></returns>
        public ParametroGeneralInfo ObtenerPorID(int parametroGeneralId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.ParametroGeneralID == parametroGeneralId).FirstOrDefault();
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
        /// Obtiene una entidad de ParametroGeneral por su Id
        /// </summary>
        /// <param name="claveParametro">Clave del Parametro</param>
        /// <returns></returns>
        public ParametroGeneralInfo ObtenerPorClaveParametro(string claveParametro)
        {
            try
            {
                Logger.Info();
                return parametroGeneralAccessor.ObtenerParametroPorClave(claveParametro);
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
        /// Metodo para Guardar/Modificar una entidad ParametroGeneral
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(ParametroGeneralInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.ParametroGeneralID > 0)
                {
                    id = da.Actualizar<ParametroGeneralInfo>(info);
                    parametroGeneralAccessor.ActualizarFechaModificacion(info.ParametroGeneralID);
                }
                else
                {
                    id = da.Insertar<ParametroGeneralInfo>(info);
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
