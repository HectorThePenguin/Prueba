using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ConfiguracionParametrosPL
    {
        /// <summary>
        /// Obtine parametros por organizacion, tipo parametro y clave
        /// </summary>
        /// <param name="parametro">Es necesario los datos OrganizacionID,TipoParametro y Clave</param>
        public ConfiguracionParametrosInfo ObtenerPorOrganizacionTipoParametroClave(ConfiguracionParametrosInfo parametro)
        {
            ConfiguracionParametrosInfo result = null;
            try
            {
                Logger.Info();
                if (parametro != null)
                {
                    if (parametro.OrganizacionID > 0 && parametro.TipoParametro >0 && parametro.Clave != null)
                    {
                        var configuracionBL = new ConfiguracionParametrosBL();
                        result = configuracionBL.ObtenerPorOrganizacionTipoParametroClave(parametro);
                    }
                    else
                    {
                        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), new Exception());
                    }
                   
                }
                

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
            return result;
        }
        /// <summary>
        /// Obtiene la configuracion por organizacion y tipo de parametros
        /// </summary>
        /// <param name="parametro">Es necesario proporcionar OrganizacionID y TipoParametros</param>
        /// <returns>Configuracion</returns>
        public IList<ConfiguracionParametrosInfo> ObtenerPorOrganizacionTipoParametro(ConfiguracionParametrosInfo parametro)
        {
            IList<ConfiguracionParametrosInfo> result = null;
            try
            {
                Logger.Info();
                if (parametro != null)
                {
                    if (parametro.OrganizacionID >0 && parametro.TipoParametro> 0 )
                    {
                        var configuracionBL = new ConfiguracionParametrosBL();
                        result = configuracionBL.ObtenerPorOrganizacionTipoParametro(parametro);
                    }
                    else
                    {
                        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), new Exception());
                    }

                }

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
            return result;
        }
        /// <summary>
        /// Obtiene la lista de configuracion para una trampa, tipo de parametro y clave
        /// </summary>
        /// <param name="parametro">Es necesario proporcionar TipoParametro, clave</param>
        /// <param name="trampaID">Trampa</param>
        /// <returns>Configuracion</returns>

        public IList<ConfiguracionParametrosInfo> ParametroObtenerPorTrampaTipoParametroClave(ConfiguracionParametrosInfo parametro, int trampaID)
        {
            IList<ConfiguracionParametrosInfo> result = null;
            try
            {
                Logger.Info();
                if (parametro != null)
                {
                    if (parametro.TipoParametro >0 && parametro.Clave != null)
                    {
                        var configuracionBL = new ConfiguracionParametrosBL();
                        result = configuracionBL.ParametroObtenerPorTrampaTipoParametroClave(parametro, trampaID);
                    }
                    else
                    {
                        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), new Exception());
                    }

                }

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
            return result;
        }

        public IList<ConfiguracionParametrosInfo> ParametroObtenerPorTrampaTipoParametro(ConfiguracionParametrosInfo parametro, int trampaID)
        {
            IList<ConfiguracionParametrosInfo> result = null;
            try
            {
                Logger.Info();
                if (parametro != null)
                {
                    if (parametro.TipoParametro > 0 )
                    {
                        var configuracionBL = new ConfiguracionParametrosBL();
                        result = configuracionBL.ParametroObtenerPorTrampaTipoParametro(parametro, trampaID);
                    }
                    else
                    {
                        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), new Exception());
                    }

                }

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
            return result;
        }
    }
}
