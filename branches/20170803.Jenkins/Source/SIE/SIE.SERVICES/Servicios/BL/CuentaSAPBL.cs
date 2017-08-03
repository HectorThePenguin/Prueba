using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class CuentaSAPBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CuentaSAP
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(CuentaSAPInfo info)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                if (info.CuentaSAPID == 0)
                {
                    cuentaSAPDAL.Crear(info);
                }
                else
                {
                    cuentaSAPDAL.Actualizar(info);
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
        internal ResultadoInfo<CuentaSAPInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                ResultadoInfo<CuentaSAPInfo> result = cuentaSAPDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CuentaSAPInfo> ObtenerPorPaginaSinId(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                ResultadoInfo<CuentaSAPInfo> result = cuentaSAPDAL.ObtenerPorPaginaSinId(pagina, filtro);
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
        ///     Obtiene un lista de CuentaSAPs
        /// </summary>
        /// <returns></returns>
        internal IList<CuentaSAPInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                IList<CuentaSAPInfo> result = cuentaSAPDAL.ObtenerTodos();
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
        /// Obtiene una lista de CuentaSAP filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<CuentaSAPInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                IList<CuentaSAPInfo> result = cuentaSAPDAL.ObtenerTodos(estatus);

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
        ///     Obtiene una entidad CuentaSAP por su Id
        /// </summary>
        /// <param name="cuentaSAPID">Obtiene uan entidad CuentaSAP por su Id</param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerPorID(int cuentaSAPID)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                CuentaSAPInfo result = cuentaSAPDAL.ObtenerPorID(cuentaSAPID);
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
        ///     Obtiene una entidad CuentaSAP por su Id
        /// </summary>
        /// <param name="cuentaSAP">Obtiene uan entidad CuentaSAP por su Id</param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerPorCuentaSAP(string cuentaSAP)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                CuentaSAPInfo result = cuentaSAPDAL.ObtenerPorCuentaSAP(cuentaSAP);
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
        ///     Obtiene una entidad CuentaSAP por su Id
        /// </summary>
        /// <param name="cuentaSAP">Obtiene uan entidad CuentaSAP por su Id</param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerPorFiltro(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                CuentaSAPInfo result = cuentaSAPDAL.ObtenerPorFiltro(cuentaSAP);
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
        ///     Obtiene una entidad CuentaSAP por su Id
        /// </summary>
        /// <param name="cuentaSAP">Obtiene uan entidad CuentaSAP por su Id</param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerPorCuentaSAP(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new Integracion.DAL.ORM.CuentaSAPDAL();
                CuentaSAPInfo result = cuentaSAPDAL.ObtenerPorCuentaSAP(cuentaSAP);
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
        /// Obtiene todas las cuentas sap por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CuentaSAPInfo> ObtenerPorPaginaCuentasSap(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                ResultadoInfo<CuentaSAPInfo> result = cuentaSAPDAL.ObtenerPorPaginaCuentasSap(pagina, filtro);
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
        /// Obtiene una cuenta sap por filtro sin tipo de cuenta
        /// </summary>
        /// <param name="cuentaSAP"></param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerPorFiltroSinTipo(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new CuentaSAPDAL();
                CuentaSAPInfo result = cuentaSAPDAL.ObtenerPorFiltroSinTipo(cuentaSAP);
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
        /// Obtiene las cuentas SAP por tipo
        /// </summary>
        /// <returns></returns>
        internal List<CuentaSAPInfo> ObtenerCuentasSAPPorTipoCuenta(TipoCuenta tipoCuenta)
        {
            try
            {
                Logger.Info();
                var cuentaSAPDAL = new Integracion.DAL.ORM.CuentaSAPDAL();
                IQueryable<CuentaSAPInfo> cuentas = cuentaSAPDAL.ObtenerTodos();
                return cuentas.Where(tipo => tipo.TipoCuentaID == tipoCuenta.GetHashCode()).ToList();
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
        /// Obtiene una cuenta mediante la interfaz con SAP
        /// </summary>
        /// <param name="cuentaSAP"></param>
        /// <returns></returns>
        internal CuentaSAPInfo ObtenerCuentaSAPInterfaz(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                var interfaceSAPBL = new InterfaceSAPBL();
                CuentaSAPInfo result = interfaceSAPBL.ObtenerCuentaSAP(cuentaSAP);
                return result;
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
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
