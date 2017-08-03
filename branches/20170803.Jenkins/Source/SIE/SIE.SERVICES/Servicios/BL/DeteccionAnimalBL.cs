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
    public class DeteccionAnimalBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad DeteccionAnimal
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(DeteccionAnimalInfo info)
        {
            try
            {
                Logger.Info();
                var deteccionAnimalDAL = new DeteccionAnimalDAL();
                int result = info.DeteccionAnimalID;
                if (info.DeteccionAnimalID == 0)
                {
                    result = deteccionAnimalDAL.Crear(info);
                }
                else
                {
                    deteccionAnimalDAL.Actualizar(info);
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
        public ResultadoInfo<DeteccionAnimalInfo> ObtenerPorPagina(PaginacionInfo pagina, DeteccionAnimalInfo filtro)
        {
            try
            {
                Logger.Info();
                var deteccionAnimalDAL = new DeteccionAnimalDAL();
                ResultadoInfo<DeteccionAnimalInfo> result = deteccionAnimalDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de DeteccionAnimal
        /// </summary>
        /// <returns></returns>
        public IList<DeteccionAnimalInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var deteccionAnimalDAL = new DeteccionAnimalDAL();
                IList<DeteccionAnimalInfo> result = deteccionAnimalDAL.ObtenerTodos();
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
        public IList<DeteccionAnimalInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var deteccionAnimalDAL = new DeteccionAnimalDAL();
                IList<DeteccionAnimalInfo> result = deteccionAnimalDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad DeteccionAnimal por su Id
        /// </summary>
        /// <param name="deteccionAnimalID">Obtiene una entidad DeteccionAnimal por su Id</param>
        /// <returns></returns>
        public DeteccionAnimalInfo ObtenerPorID(int deteccionAnimalID)
        {
            try
            {
                Logger.Info();
                var deteccionAnimalDAL = new DeteccionAnimalDAL();
                DeteccionAnimalInfo result = deteccionAnimalDAL.ObtenerPorID(deteccionAnimalID);
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
        /// Obtiene una entidad DeteccionAnimal por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public DeteccionAnimalInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var deteccionAnimalDAL = new DeteccionAnimalDAL();
                DeteccionAnimalInfo result = deteccionAnimalDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene por Animal Movimiento ID
        /// </summary>
        /// <param name="animalMovimientoID">id del animal movimiento</param>
        /// <returns></returns>
        public DeteccionAnimalInfo ObtenerPorAnimalMovimientoID(long animalMovimientoID)
        {
            try
            {
                Logger.Info();
                var deteccionAnimalDAL = new DeteccionAnimalDAL();
                DeteccionAnimalInfo result = deteccionAnimalDAL.ObtenerPorAnimalMovimientoID(animalMovimientoID);
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

