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
    public class CuentaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Cuenta
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public void Guardar(CuentaInfo info)
        {
            try
            {
                Logger.Info();
                var cuentaBL = new CuentaBL();
                cuentaBL.Guardar(info);
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
        public ResultadoInfo<CuentaInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaBL = new CuentaBL();
                ResultadoInfo<CuentaInfo> result = cuentaBL.ObtenerPorPagina(pagina, filtro);

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
        ///     Obtiene una lista de Cuentas
        /// </summary>
        /// <returns></returns>
        public IList<CuentaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var cuentaBL = new CuentaBL();
                IList<CuentaInfo> result = cuentaBL.ObtenerTodos();

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
        ///  Obtiene una lista de Cuenta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<CuentaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var cuentaBL = new CuentaBL();
                IList<CuentaInfo> result = cuentaBL.ObtenerTodos(estatus);

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
        ///     Obtiene una entidad Cuenta por su Id
        /// </summary>
        /// <param name="cuentaID"></param>
        /// <returns></returns>
        public CuentaInfo ObtenerPorID(int cuentaID)
        {
            try
            {
                Logger.Info();
                var cuentaBL = new CuentaBL();
                CuentaInfo result = cuentaBL.ObtenerPorID(cuentaID);

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
        ///     Obtiene una entidad Cuenta por su Id
        /// </summary>
        /// <param name="cuenta"></param>
        /// <returns></returns>
        public CuentaInfo ObtenerPorIDGastosMateriasPrimas(CuentaInfo cuenta)
        {
            try
            {
                Logger.Info();
                var cuentaBL = new CuentaBL();
                CuentaInfo result = cuentaBL.ObtenerPorIDGastosMateriasPrimas(cuenta);

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
        ///     Obtiene una entidad Clave Contable por su Clave de Cuenta y su Organizacion
        /// </summary>
        /// <param name="claveCuenta">Clave de la Cuenta a buscar</param>
        /// /// <param name="organizacionID">Organizacion de la Cuenta a buscar</param>
        /// <returns></returns>
        public ClaveContableInfo ObtenerPorClaveCuentaOrganizacion(string claveCuenta, int organizacionID)
        {
            try
            {
                Logger.Info();
                var cuentaBL = new CuentaBL();
                ClaveContableInfo result = cuentaBL.ObtenerPorClaveCuentaOrganizacion(claveCuenta, organizacionID);
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
        ///     Obtiene una entidad Clave Contable por su Clave de Cuenta y su Organizacion
        /// </summary>
        /// <param name="descripcion">Clave de la Cuenta a buscar</param>
        /// <returns></returns>
        public CuentaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var cuentaBL = new CuentaBL();
                CuentaInfo result = cuentaBL.ObtenerPorDescripcion(descripcion);
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

