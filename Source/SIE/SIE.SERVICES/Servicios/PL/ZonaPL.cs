using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ZonaPL
    {
        /// <summary>
        ///     Obtiene un zona por Id
        /// </summary>
        /// <param name="zonaId"></param>
        /// <returns></returns>
        public ZonaInfo ObtenerPorID(int zonaId)
        {
            ZonaInfo info;
            try
            {
                Logger.Info();
                var zonaBL = new ZonaBL();
                info = zonaBL.ObtenerPorID(zonaId);
            }
            catch( ExcepcionGenerica)
            {
                throw;    
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return info;
        }

        /// <summary>
        ///     Obtiene un zona por Id
        /// </summary>
        /// <param name="zonaInfo"></param>
        /// <returns></returns>
        public ZonaInfo ObtenerPorID(ZonaInfo zonaInfo)
        {
            ZonaInfo Info;
            try
            {
                Logger.Info();
                var zonaBL = new ZonaBL();
                Info = zonaBL.ObtenerPorID(zonaInfo);
                if (Info != null && Info.Pais != null && Info.Pais.PaisID != zonaInfo.Pais.PaisID)
                {
                    Info = null;
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
            return Info;      
        }

        /// <summary>
        ///     Obtiene un zona por Id
        /// </summary>
        /// <param name="zonaInfo"></param>
        /// <returns></returns>
        public ZonaInfo ObtenerPorIdSinValidarPais(ZonaInfo zonaInfo)
        {
            try
            {
                Logger.Info();
                var zonaBl = new ZonaBL();
                return zonaBl.ObtenerPorID(zonaInfo);
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
        /// Obtiene una entidad zona por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Zona por su Id</param>
        /// <returns></returns>
        public ZonaInfo ObtenerPorDescripcion(string descripcion)
        {
            ZonaInfo Info;
            try
            {
                Logger.Info();
                var zonaBL = new ZonaBL();
                Info = zonaBL.ObtenerPorDescripcion(descripcion);
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
            return Info;         
        }

        /// <summary>
        ///  Obtiene un lista paginada de zonas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ZonaInfo> ObtenerPorPagina(PaginacionInfo pagina, ZonaInfo filtro)
        {
            ResultadoInfo<ZonaInfo> Info;
            try
            {
                var zonaBL = new ZonaBL();
                Info = zonaBL.ObtenerPorPagina(pagina, filtro);
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
            return Info; 
        
        }
 
    }
}
