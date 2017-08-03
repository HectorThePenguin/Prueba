using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class CheckListRoladoraGeneralBL : IDisposable
    {
        CheckListRoladoraGeneralDAL checkListRoladoraGeneralDAL;

        public CheckListRoladoraGeneralBL()
        {
            checkListRoladoraGeneralDAL = new CheckListRoladoraGeneralDAL();
        }

        public void Dispose()
        {
            checkListRoladoraGeneralDAL.Disposed += (s, e) =>
            {
                checkListRoladoraGeneralDAL = null;
            };
            checkListRoladoraGeneralDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladoraGeneral
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraGeneralInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraGeneralInfo filtro)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraGeneralDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de CheckListRoladoraGeneral
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraGeneralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return checkListRoladoraGeneralDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de CheckListRoladoraGeneral filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CheckListRoladoraGeneralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraGeneralDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de CheckListRoladoraGeneral por su Id
        /// </summary>
        /// <param name="checkListRoladoraGeneralId">Obtiene una entidad CheckListRoladoraGeneral por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraGeneralInfo ObtenerPorID(int checkListRoladoraGeneralId)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraGeneralDAL.ObtenerTodos().Where(e=> e.CheckListRoladoraGeneralID == checkListRoladoraGeneralId).FirstOrDefault();
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

        ///// <summary>
        ///// Obtiene una entidad de CheckListRoladoraGeneral por su descripcion
        ///// </summary>
        ///// <param name="checkListRoladoraGeneralId">Obtiene una entidad CheckListRoladoraGeneral por su descripcion</param>
        ///// <returns></returns>
        //public CheckListRoladoraGeneralInfo ObtenerPorDescripcion(string descripcion)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        return checkListRoladoraGeneralDAL.ObtenerTodos().Where(e=> e.CheckListRoladoraGeneralID.ToLower() == descripcion.ToLower()).FirstOrDefault();
        //    }
        //    catch(ExcepcionGenerica)
        //    {
        //        throw;
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //}

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CheckListRoladoraGeneral
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraGeneralInfo info)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraGeneralDAL.Guardar(info);
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
