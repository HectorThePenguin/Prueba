using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class CheckListRoladoraHorometroBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CheckListRoladoraHorometro
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(CheckListRoladoraHorometroInfo info)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraHorometroDAL = new CheckListRoladoraHorometroDAL();
                int result = info.CheckListRoladoraHorometroID;
                if (info.CheckListRoladoraHorometroID == 0)
                {
                    result = checkListRoladoraHorometroDAL.Crear(info);
                }
                else
                {
                    checkListRoladoraHorometroDAL.Actualizar(info);
                }
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

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraHorometroInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraHorometroInfo filtro)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraHorometroDAL = new CheckListRoladoraHorometroDAL();
                ResultadoInfo<CheckListRoladoraHorometroInfo> result = checkListRoladoraHorometroDAL.ObtenerPorPagina(pagina, filtro);
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

        /// <summary>
        /// Obtiene un lista de CheckListRoladoraHorometro
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraHorometroInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var checkListRoladoraHorometroDAL = new CheckListRoladoraHorometroDAL();
                IList<CheckListRoladoraHorometroInfo> result = checkListRoladoraHorometroDAL.ObtenerTodos();
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

        /// <summary>
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraHorometroInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraHorometroDAL = new CheckListRoladoraHorometroDAL();
                IList<CheckListRoladoraHorometroInfo> result = checkListRoladoraHorometroDAL.ObtenerTodos(estatus);
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

        /// <summary>
        /// Obtiene una entidad CheckListRoladoraHorometro por su Id
        /// </summary>
        /// <param name="checkListRoladoraHorometroID">Obtiene una entidad CheckListRoladoraHorometro por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraHorometroInfo ObtenerPorID(int checkListRoladoraHorometroID)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraHorometroDAL = new CheckListRoladoraHorometroDAL();
                CheckListRoladoraHorometroInfo result = checkListRoladoraHorometroDAL.ObtenerPorID(checkListRoladoraHorometroID);
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

        /// <summary>
        /// Obtiene una entidad CheckListRoladoraHorometro por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public CheckListRoladoraHorometroInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var checkListRoladoraHorometroDAL = new CheckListRoladoraHorometroDAL();
                CheckListRoladoraHorometroInfo result = checkListRoladoraHorometroDAL.ObtenerPorDescripcion(descripcion);
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
    }
}

