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
    public class TipoCuentaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar la entidad TipoCuenta
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public void Guardar(TipoCuentaInfo info)
        {
            try
            {
                Logger.Info();
                var tipoCuentaBL = new TipoCuentaBL();
                tipoCuentaBL.Guardar(info);
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
        public ResultadoInfo<TipoCuentaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCuentaInfo filtro)
        {
            try
            {
                Logger.Info();
                var tipoCuentaBL = new TipoCuentaBL();
                ResultadoInfo<TipoCuentaInfo> result = tipoCuentaBL.ObtenerPorPagina(pagina, filtro);

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
        ///     Obtiene una lista de TipoCuenta por su Id
        /// </summary>
        /// <returns></returns>
        public IList<TipoCuentaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var tipoCuentaBL = new TipoCuentaBL();
                IList<TipoCuentaInfo> result = tipoCuentaBL.ObtenerTodos();

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
        ///     Obtiene una lista de TipoCuenta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<TipoCuentaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoCuentaBL = new TipoCuentaBL();
                IList<TipoCuentaInfo> result = tipoCuentaBL.ObtenerTodos(estatus);

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
        ///     Obtiene una entidad TipoCuenta su Id
        /// </summary>
        /// <param name="tipoCuentaID"></param>
        /// <returns></returns>
        public TipoCuentaInfo ObtenerPorID(int tipoCuentaID)
        {
            try
            {
                Logger.Info();
                var tipoCuentaBL = new TipoCuentaBL();
                TipoCuentaInfo result = tipoCuentaBL.ObtenerPorID(tipoCuentaID);

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

