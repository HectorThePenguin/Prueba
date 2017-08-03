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
    public class CuentaSAPPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CuentaSAP
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public void Guardar(CuentaSAPInfo info)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                cuentaSAPBL.Guardar(info);
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
        public ResultadoInfo<CuentaSAPInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                ResultadoInfo<CuentaSAPInfo> result = cuentaSAPBL.ObtenerPorPagina(pagina, filtro);

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
        public ResultadoInfo<CuentaSAPInfo> ObtenerPorPaginaSinId(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                ResultadoInfo<CuentaSAPInfo> result = cuentaSAPBL.ObtenerPorPaginaSinId(pagina, filtro);

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
        ///     Obtiene una lista de CuentaSAPs
        /// </summary>
        /// <returns></returns>
        public IList<CuentaSAPInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                IList<CuentaSAPInfo> result = cuentaSAPBL.ObtenerTodos();

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
        ///  Obtiene una lista de CuentaSAP filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<CuentaSAPInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                IList<CuentaSAPInfo> result = cuentaSAPBL.ObtenerTodos(estatus);

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
        /// <param name="cuentaSAPID"></param>
        /// <returns></returns>
        public CuentaSAPInfo ObtenerPorID(int cuentaSAPID)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                CuentaSAPInfo result = cuentaSAPBL.ObtenerPorID(cuentaSAPID);

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
        /// <param name="cuentaSAP"></param>
        /// <returns></returns>
        public CuentaSAPInfo ObtenerPorFiltro(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                 CuentaSAPInfo result = cuentaSAPBL.ObtenerPorFiltro(cuentaSAP);

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
        /// <param name="cuentaSAP"></param>
        /// <returns></returns>
        public CuentaSAPInfo ObtenerPorCuentaSAP(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                CuentaSAPInfo result = cuentaSAPBL.ObtenerPorCuentaSAP(cuentaSAP);

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
        /// Obtiene las cuentas sap por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CuentaSAPInfo> ObtenerPorPaginaCuentasSap(PaginacionInfo pagina, CuentaSAPInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                ResultadoInfo<CuentaSAPInfo> result = cuentaSAPBL.ObtenerPorPaginaCuentasSap(pagina, filtro);

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
        /// Obtiene la cuenta sap por filtro sin tipo cuenta
        /// </summary>
        /// <param name="cuentaSAP"></param>
        /// <returns></returns>
        public CuentaSAPInfo ObtenerPorFiltroSinTipo(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                CuentaSAPInfo result = cuentaSAPBL.ObtenerPorFiltroSinTipo(cuentaSAP);

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
        /// Obtiene una cuenta mediante la interfaz con SAP
        /// </summary>
        /// <param name="cuentaSAP"></param>
        /// <returns></returns>
        public CuentaSAPInfo ObtenerCuentaSAPInterfaz(CuentaSAPInfo cuentaSAP)
        {
            try
            {
                Logger.Info();
                var cuentaSAPBL = new CuentaSAPBL();
                CuentaSAPInfo result = cuentaSAPBL.ObtenerCuentaSAPInterfaz(cuentaSAP);

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

