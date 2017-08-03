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
    public class IndicadorProductoBoletaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad IndicadorProductoBoleta
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(IndicadorProductoBoletaInfo info)
        {
            try
            {
                Logger.Info();
                var indicadorProductoBoletaDAL = new IndicadorProductoBoletaDAL();
                int result = info.IndicadorProductoBoletaID;
                if (info.IndicadorProductoBoletaID == 0)
                {
                    result = indicadorProductoBoletaDAL.Crear(info);
                }
                else
                {
                    indicadorProductoBoletaDAL.Actualizar(info);
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
        public ResultadoInfo<IndicadorProductoBoletaInfo> ObtenerPorPagina(PaginacionInfo pagina,
                                                                           IndicadorProductoBoletaInfo filtro)
        {
            try
            {
                Logger.Info();
                var indicadorProductoBoletaDAL = new IndicadorProductoBoletaDAL();
                ResultadoInfo<IndicadorProductoBoletaInfo> result = indicadorProductoBoletaDAL.ObtenerPorPagina(pagina,
                                                                                                                filtro);
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
        /// Obtiene un lista de IndicadorProductoBoleta
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoBoletaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var indicadorProductoBoletaDAL = new IndicadorProductoBoletaDAL();
                IList<IndicadorProductoBoletaInfo> result = indicadorProductoBoletaDAL.ObtenerTodos();
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
        public IList<IndicadorProductoBoletaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var indicadorProductoBoletaDAL = new IndicadorProductoBoletaDAL();
                IList<IndicadorProductoBoletaInfo> result = indicadorProductoBoletaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad IndicadorProductoBoleta por su Id
        /// </summary>
        /// <param name="indicadorProductoBoletaID">Obtiene una entidad IndicadorProductoBoleta por su Id</param>
        /// <returns></returns>
        public IndicadorProductoBoletaInfo ObtenerPorID(int indicadorProductoBoletaID)
        {
            try
            {
                Logger.Info();
                var indicadorProductoBoletaDAL = new IndicadorProductoBoletaDAL();
                IndicadorProductoBoletaInfo result = indicadorProductoBoletaDAL.ObtenerPorID(indicadorProductoBoletaID);
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
        /// Obtiene una indicador producto boleta por
        /// indicador producto y organizacion
        /// </summary>
        /// <param name="indicadorProductoID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public IndicadorProductoBoletaInfo ObtenerPorIndicadorProductoOrganizacion(int indicadorProductoID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var indicadorProductoBoletaDAL = new IndicadorProductoBoletaDAL();
                IndicadorProductoBoletaInfo result =
                    indicadorProductoBoletaDAL.ObtenerPorIndicadorProductoOrganizacion(indicadorProductoID,
                                                                                       organizacionID);
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
        /// Obtiene una indicador producto boleta por
        /// indicador producto y organizacion
        /// </summary>
        /// <param name="productoID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public List<IndicadorProductoBoletaInfo> ObtenerPorProductoOrganizacion(int productoID, int organizacionID, EstatusEnum activo)
        {
            try
            {
                Logger.Info();
                var indicadorProductoBoletaDAL = new IndicadorProductoBoletaDAL();
                List<IndicadorProductoBoletaInfo> result =
                    indicadorProductoBoletaDAL.ObtenerPorProductoOrganizacion(productoID,organizacionID, activo);
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

