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
    internal class TipoCorralBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad TipoCorral
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(TipoCorralInfo info)
        {
            try
            {
                Logger.Info();
                var tipoCorralDAL = new TipoCorralDAL();
                int result = info.TipoCorralID;
                if (info.TipoCorralID == 0)
                {
                    result = tipoCorralDAL.Crear(info);
                }
                else
                {
                    tipoCorralDAL.Actualizar(info);
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
        internal ResultadoInfo<TipoCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoCorralDAL = new TipoCorralDAL();
                ResultadoInfo<TipoCorralInfo> result = tipoCorralDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de TipoCorral
        /// </summary>
        /// <returns></returns>
        internal IList<TipoCorralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoCorralDAL = new TipoCorralDAL();
                IList<TipoCorralInfo> result = tipoCorralDAL.ObtenerTodos();
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
        internal IList<TipoCorralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoCorralDAL = new TipoCorralDAL();
                IList<TipoCorralInfo> result = tipoCorralDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad TipoCorral por su Id
        /// </summary>
        /// <param name="tipoCorralID">Obtiene una entidad TipoCorral por su Id</param>
        /// <returns></returns>
        internal TipoCorralInfo ObtenerPorID(int tipoCorralID)
        {
            try
            {
                Logger.Info();
                var tipoCorralDAL = new TipoCorralDAL();
                TipoCorralInfo result = tipoCorralDAL.ObtenerPorID(tipoCorralID);
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
        /// Obtiene una entidad TipoCorral por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoCorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoCorralDAL = new TipoCorralDAL();
                TipoCorralInfo result = tipoCorralDAL.ObtenerPorDescripcion(descripcion);
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
        /// Valida si el Grupo Corral Pertenece a un Tipo Corral
        /// </summary>
        /// <param name="grupoCorralID"></param>
        /// <returns></returns>
        internal bool TieneAsignadoGruposCorral(int grupoCorralID)
        {
            try
            {
                Logger.Info();
                using (var tipoCorralDAL = new Integracion.DAL.ORM.TipoCorralDAL())
                {
                    bool result = tipoCorralDAL.TieneAsignadoGruposCorral(grupoCorralID);
                    return result;   
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
        }
    }
}

