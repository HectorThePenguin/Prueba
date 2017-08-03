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
    public class DescripcionGanadoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad DescripcionGanado
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(DescripcionGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var descripcionGanadoDAL = new DescripcionGanadoDAL();
                int result = info.DescripcionGanadoID;
                if (info.DescripcionGanadoID == 0)
                {
                    result = descripcionGanadoDAL.Crear(info);
                }
                else
                {
                    descripcionGanadoDAL.Actualizar(info);
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
        public ResultadoInfo<DescripcionGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, DescripcionGanadoInfo filtro)
        {
            try
            {
                Logger.Info();
                var descripcionGanadoDAL = new DescripcionGanadoDAL();
                ResultadoInfo<DescripcionGanadoInfo> result = descripcionGanadoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de DescripcionGanado
        /// </summary>
        /// <returns></returns>
        public IList<DescripcionGanadoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var descripcionGanadoDAL = new DescripcionGanadoDAL();
                IList<DescripcionGanadoInfo> result = descripcionGanadoDAL.ObtenerTodos();
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
        public IList<DescripcionGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var descripcionGanadoDAL = new DescripcionGanadoDAL();
                IList<DescripcionGanadoInfo> result = descripcionGanadoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad DescripcionGanado por su Id
        /// </summary>
        /// <param name="descripcionGanadoID">Obtiene una entidad DescripcionGanado por su Id</param>
        /// <returns></returns>
        public DescripcionGanadoInfo ObtenerPorID(int descripcionGanadoID)
        {
            try
            {
                Logger.Info();
                var descripcionGanadoDAL = new DescripcionGanadoDAL();
                DescripcionGanadoInfo result = descripcionGanadoDAL.ObtenerPorID(descripcionGanadoID);
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
        /// Obtiene una entidad DescripcionGanado por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public DescripcionGanadoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var descripcionGanadoDAL = new DescripcionGanadoDAL();
                DescripcionGanadoInfo result = descripcionGanadoDAL.ObtenerPorDescripcion(descripcion);
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

