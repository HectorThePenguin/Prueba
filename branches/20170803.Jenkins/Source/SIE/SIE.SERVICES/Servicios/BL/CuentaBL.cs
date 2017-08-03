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
    internal class CuentaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Cuenta
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(CuentaInfo info)
        {
            try
            {
                Logger.Info();
                var cuentaDAL = new CuentaDAL();
                if (info.CuentaID == 0)
                {
                    cuentaDAL.Crear(info);
                }
                else
                {
                    cuentaDAL.Actualizar(info);
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

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CuentaInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaDAL = new CuentaDAL();
                ResultadoInfo<CuentaInfo> result = cuentaDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista de Cuentas
        /// </summary>
        /// <returns></returns>
        internal IList<CuentaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var cuentaDAL = new CuentaDAL();
                IList<CuentaInfo> result = cuentaDAL.ObtenerTodos();
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
        /// Obtiene una lista de Cuenta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<CuentaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var cuentaDAL = new CuentaDAL();
                IList<CuentaInfo> result = cuentaDAL.ObtenerTodos(estatus);

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
        /// <param name="cuentaID">Obtiene uan entidad Cuenta por su Id</param>
        /// <returns></returns>
        internal CuentaInfo ObtenerPorID(int cuentaID)
        {
            try
            {
                Logger.Info();
                var cuentaDAL = new CuentaDAL();
                CuentaInfo result = cuentaDAL.ObtenerPorID(cuentaID);
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
        /// <param name="cuentaID">Obtiene uan entidad Cuenta por su Id</param>
        /// <returns></returns>
        internal CuentaInfo ObtenerPorIDGastosMateriasPrimas(CuentaInfo cuenta)
        {
            try
            {
                Logger.Info();
                var cuentaDAL = new CuentaDAL();
                CuentaInfo result = cuentaDAL.ObtenerPorIDGastosMateriasPrimas(cuenta);
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
        internal ClaveContableInfo ObtenerPorClaveCuentaOrganizacion(string claveCuenta, int organizacionID)
        {
            try
            {
                Logger.Info();
                var cuentaDAL = new CuentaDAL();
                ClaveContableInfo result = cuentaDAL.ObtenerPorClaveCuentaOrganizacion(claveCuenta, organizacionID);
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
        /// Obtiene una entidad Cuenta por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public CuentaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var cuentaDAL = new CuentaDAL();
                CuentaInfo result = cuentaDAL.ObtenerPorDescripcion(descripcion);
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

