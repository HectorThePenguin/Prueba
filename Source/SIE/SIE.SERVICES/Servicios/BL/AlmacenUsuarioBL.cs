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
    public class AlmacenUsuarioBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad AlmacenUsuario
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(AlmacenUsuarioInfo info)
        {
            try
            {
                Logger.Info();
                var almacenUsuarioDAL = new AlmacenUsuarioDAL();
                int result = info.AlmacenUsuarioID;
                if (info.AlmacenUsuarioID == 0)
                {
                    result = almacenUsuarioDAL.Crear(info);
                }
                else
                {
                    almacenUsuarioDAL.Actualizar(info);
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
        /// Metodo para Guardar/Modificar una entidad AlmacenUsuario
        /// </summary>
        /// <param name="info"></param>
        public int GuardarXML(List<AlmacenUsuarioInfo> info)
        {
            try
            {
                Logger.Info();
                var almacenUsuarioDAL = new AlmacenUsuarioDAL();
                int result = almacenUsuarioDAL.GuardarXML(info);
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
        public ResultadoInfo<AlmacenUsuarioInfo> ObtenerPorPagina(PaginacionInfo pagina, AlmacenUsuarioInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenUsuarioDAL = new AlmacenUsuarioDAL();
                ResultadoInfo<AlmacenUsuarioInfo> result = almacenUsuarioDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de AlmacenUsuario
        /// </summary>
        /// <returns></returns>
        public IList<AlmacenUsuarioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var almacenUsuarioDAL = new AlmacenUsuarioDAL();
                IList<AlmacenUsuarioInfo> result = almacenUsuarioDAL.ObtenerTodos();
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
        public IList<AlmacenUsuarioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var almacenUsuarioDAL = new AlmacenUsuarioDAL();
                IList<AlmacenUsuarioInfo> result = almacenUsuarioDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad AlmacenUsuario por su Id
        /// </summary>
        /// <param name="almacenUsuarioID">Obtiene una entidad AlmacenUsuario por su Id</param>
        /// <returns></returns>
        public AlmacenUsuarioInfo ObtenerPorID(int almacenUsuarioID)
        {
            try
            {
                Logger.Info();
                var almacenUsuarioDAL = new AlmacenUsuarioDAL();
                AlmacenUsuarioInfo result = almacenUsuarioDAL.ObtenerPorID(almacenUsuarioID);
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
        /// Obtiene una entidad AlmacenUsuario por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public AlmacenUsuarioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var almacenUsuarioDAL = new AlmacenUsuarioDAL();
                AlmacenUsuarioInfo result = almacenUsuarioDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad AlmacenUsuario por su descripción
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<AlmacenUsuarioInfo> ObtenerPorAlmacenID(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var almacenUsuarioDAL = new AlmacenUsuarioDAL();
                List<AlmacenUsuarioInfo> result = almacenUsuarioDAL.ObtenerPorAlmacenID(info);
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

