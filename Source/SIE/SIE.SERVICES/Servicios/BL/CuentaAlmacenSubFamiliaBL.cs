using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class CuentaAlmacenSubFamiliaBL
    {
        /// <summary>
        /// Obtiene una lista de CuentaAlmacenSubFamilia
        /// </summary>
        /// <returns></returns>
        public IList<CuentaAlmacenSubFamiliaInfo> ObtenerCostosSubFamilia(int almacenID)
        {
            try
            {
                Logger.Info();
                var cuentaAlmacenSubFamiliaDAL = new CuentaAlmacenSubFamiliaDAL();
                IList<CuentaAlmacenSubFamiliaInfo> cuentasAlmacenSubFammilia =
                    cuentaAlmacenSubFamiliaDAL.ObtenerCostosSubFamilia(almacenID);
                return cuentasAlmacenSubFammilia;
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
        /// Metodo para Guardar/Modificar una entidad CuentaAlmacenSubFamilia
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(CuentaAlmacenSubFamiliaInfo info)
        {
            try
            {
                Logger.Info();
                var cuentaAlmacenSubFamiliaDAL = new CuentaAlmacenSubFamiliaDAL();
                int result = info.CuentaAlmacenSubFamiliaID;
                if (info.CuentaAlmacenSubFamiliaID == 0)
                {
                    result = cuentaAlmacenSubFamiliaDAL.Crear(info);
                }
                else
                {
                    cuentaAlmacenSubFamiliaDAL.Actualizar(info);
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
        public ResultadoInfo<CuentaAlmacenSubFamiliaInfo> ObtenerPorPagina(PaginacionInfo pagina, CuentaAlmacenSubFamiliaInfo filtro)
        {
            try
            {
                Logger.Info();
                var cuentaAlmacenSubFamiliaDAL = new CuentaAlmacenSubFamiliaDAL();
                ResultadoInfo<CuentaAlmacenSubFamiliaInfo> result = cuentaAlmacenSubFamiliaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de CuentaAlmacenSubFamilia
        /// </summary>
        /// <returns></returns>
        public IList<CuentaAlmacenSubFamiliaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var cuentaAlmacenSubFamiliaDAL = new CuentaAlmacenSubFamiliaDAL();
                IList<CuentaAlmacenSubFamiliaInfo> result = cuentaAlmacenSubFamiliaDAL.ObtenerTodos();
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
        public IList<CuentaAlmacenSubFamiliaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var cuentaAlmacenSubFamiliaDAL = new CuentaAlmacenSubFamiliaDAL();
                IList<CuentaAlmacenSubFamiliaInfo> result = cuentaAlmacenSubFamiliaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad CuentaAlmacenSubFamilia por su Id
        /// </summary>
        /// <param name="cuentaAlmacenSubFamiliaID">Obtiene una entidad CuentaAlmacenSubFamilia por su Id</param>
        /// <returns></returns>
        public CuentaAlmacenSubFamiliaInfo ObtenerPorID(int cuentaAlmacenSubFamiliaID)
        {
            try
            {
                Logger.Info();
                var cuentaAlmacenSubFamiliaDAL = new CuentaAlmacenSubFamiliaDAL();
                CuentaAlmacenSubFamiliaInfo result = cuentaAlmacenSubFamiliaDAL.ObtenerPorID(cuentaAlmacenSubFamiliaID);
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
        /// Obtiene una entidad CuentaAlmacenSubFamilia por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public CuentaAlmacenSubFamiliaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var cuentaAlmacenSubFamiliaDAL = new CuentaAlmacenSubFamiliaDAL();
                CuentaAlmacenSubFamiliaInfo result = cuentaAlmacenSubFamiliaDAL.ObtenerPorDescripcion(descripcion);
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
