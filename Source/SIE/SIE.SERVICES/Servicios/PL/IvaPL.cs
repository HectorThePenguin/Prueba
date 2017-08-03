using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class IvaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Iva
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public void Guardar(IvaInfo info)
        {
            try
            {
                Logger.Info();
                var ivaBL = new IvaBL();
                ivaBL.Guardar(info);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<IvaInfo> ObtenerPorPagina(PaginacionInfo pagina, IvaInfo filtro)
        {
            try
            {
                Logger.Info();
                var ivaBL = new IvaBL();
                ResultadoInfo<IvaInfo> result = ivaBL.ObtenerPorPagina(pagina, filtro);

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
        ///     Obtiene una lista de Ivas
        /// </summary>
        /// <returns></returns>
        public IList<IvaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var ivaBL = new IvaBL();
                IList<IvaInfo> result = ivaBL.ObtenerTodos();
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
        ///     Obtiene una lista de Ivas
        /// </summary>
        /// <returns></returns>
        public IList<IvaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var ivaBL = new IvaBL();
                IList<IvaInfo> result = ivaBL.ObtenerTodos(estatus);
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
        ///     Obtiene una entidad Iva por su Id
        /// </summary>
        /// <param name="ivaID"></param>
        /// <returns></returns>
        public IvaInfo ObtenerPorID(int ivaID)
        {
            try
            {
                Logger.Info();
                var ivaBL = new IvaBL();
                IvaInfo result = ivaBL.ObtenerPorID(ivaID);

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

