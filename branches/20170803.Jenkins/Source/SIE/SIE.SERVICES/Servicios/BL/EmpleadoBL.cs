using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    internal class EmpleadoBL
    {
        /// <summary>
        ///     Obtiene un Empleado por Id
        /// </summary>
        /// <param name="empleadoID"></param>
        /// <returns></returns>
        internal EmpleadoInfo ObtenerPorID(int empleadoID)
        {
            EmpleadoInfo info;
            try
            {
                Logger.Info();
                var dal = new EmpleadoDAL();
                info = dal.ObtenerPorID(empleadoID);
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
            return info;
        }

        /// <summary>
        ///     Obtiene un lista paginada de empleados
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EmpleadoInfo> ObtenerPorPagina(PaginacionInfo pagina, EmpleadoInfo filtro)
        {
            ResultadoInfo<EmpleadoInfo> result;
            try
            {
                Logger.Info();
                var dal = new EmpleadoDAL();
                result = dal.ObtenerPorPagina(pagina, filtro);
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
