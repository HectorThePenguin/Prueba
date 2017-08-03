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
    public class CuentaValorBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CuentaValor
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(CuentaValorInfo info)
        {
            try
            {
                Logger.Info();
                var cuentaValorDAL = new CuentaValorDAL();
                int result = info.CuentaValorID;
                if (info.CuentaValorID == 0)
                {
                    result = cuentaValorDAL.Crear(info);
                }
                else
                {
                    cuentaValorDAL.Actualizar(info);
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
        public ResultadoInfo<CuentaValorInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaValorInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaValorDAL = new CuentaValorDAL();
                ResultadoInfo<CuentaValorInfo> result = cuentaValorDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de CuentaValor
        /// </summary>
        /// <returns></returns>
        public IList<CuentaValorInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var cuentaValorDAL = new CuentaValorDAL();
                IList<CuentaValorInfo> result = cuentaValorDAL.ObtenerTodos();
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
        public IList<CuentaValorInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var cuentaValorDAL = new CuentaValorDAL();
                IList<CuentaValorInfo> result = cuentaValorDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad CuentaValor por su Id
        /// </summary>
        /// <param name="cuentaValorID">Obtiene una entidad CuentaValor por su Id</param>
        /// <returns></returns>
        public CuentaValorInfo ObtenerPorID(int cuentaValorID)
        {
            try
            {
                Logger.Info();
                var cuentaValorDAL = new CuentaValorDAL();
                CuentaValorInfo result = cuentaValorDAL.ObtenerPorID(cuentaValorID);
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
        /// Obtiene una entidad CuentaValor por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public CuentaValorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var cuentaValorDAL = new CuentaValorDAL();
                CuentaValorInfo result = cuentaValorDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad CuentaValor por su descripción
        /// </summary>
        /// <param name="cuentaValor"></param>
        /// <returns></returns>
        public CuentaValorInfo ObtenerPorFiltros(CuentaValorInfo cuentaValor)
        {
            try
            {
                Logger.Info();
                var cuentaValorDAL = new CuentaValorDAL();
                CuentaValorInfo result = cuentaValorDAL.ObtenerPorFiltros(cuentaValor);
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

