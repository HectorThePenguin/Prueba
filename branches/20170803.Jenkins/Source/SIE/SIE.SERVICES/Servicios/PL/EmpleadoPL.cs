using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class EmpleadoPL
    {
        /// <summary>
        ///     Obtiene un lista paginada de Empelados
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<EmpleadoInfo> ObtenerPorPagina(PaginacionInfo pagina, EmpleadoInfo filtro)
        {
            ResultadoInfo<EmpleadoInfo> resultado;
            try
            {
                Logger.Info();
                resultado = new ResultadoInfo<EmpleadoInfo>();
                var bl = new EmpleadoBL();
                resultado = bl.ObtenerPorPagina(pagina, filtro);
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
            return resultado;
        }

        /// <summary>
        /// Obtiene un Empleado por Id para implementar la ayuda.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public EmpleadoInfo ObtenerPorID(EmpleadoInfo info)
        {
            EmpleadoInfo empleadoInfo;
            try
            {
                Logger.Info();
                empleadoInfo = new EmpleadoInfo();
                var bl = new EmpleadoBL();
                empleadoInfo = bl.ObtenerPorID(info.EmpleadoID);
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
            return empleadoInfo;
        } 
    }
}

