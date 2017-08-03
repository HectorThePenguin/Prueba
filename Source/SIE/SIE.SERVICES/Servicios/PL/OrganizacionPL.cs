using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;
using SuKarne.Controls.MessageBox;

namespace SIE.Services.Servicios.PL
{
    public class OrganizacionPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Organizacion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(OrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                int result = organizacionBL.Guardar(info);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public OrganizacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                OrganizacionInfo result = organizacionBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene todas las organizaciones de tipo ganaderas
        /// </summary>
        /// <returns></returns>
        public IList<OrganizacionInfo> ObtenerTipoGanaderas()
        {
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                IList<OrganizacionInfo> lista = organizacionBL.ObtenerTipoGanaderas();

                return lista;
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
        /// Obtiene todas las organizaciones de tipo ganaderas
        /// </summary>
        /// <returns></returns>
        public IList<OrganizacionInfo> ObtenerTipoCentros()
        {
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                IList<OrganizacionInfo> lista = organizacionBL.ObtenerTipoCentros();

                return lista;

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<OrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorPagina(pagina, filtro);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene un listado que retorna la lista de organizaciones que manejan premezclas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaPremezcla(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorPaginaPremezcla(pagina, filtro);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaTipoOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorPaginaTipoOrganizacion(pagina, filtro);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones tipo centros, cadis, descansos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaCentrosCadisDescansos(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorPaginaCentrosCadisDescansos(pagina, filtro);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        /// Obtiene una lista de Lote filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<OrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                IList<OrganizacionInfo> lista = organizacionBL.ObtenerTodos(estatus);

                return lista;
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
        ///      Obtiene un organizacion por su Id
        /// </summary>
        /// <returns> </returns>
        public OrganizacionInfo ObtenerPorID(int organizacionId)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                info = organizacionBL.ObtenerPorID(organizacionId);
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
            return info;
        }

        public OrganizacionInfo ObtenerPorIDFiltroTipoOrganizacion(int organizacionId)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                info = organizacionBL.ObtenerPorIDFiltroTipoOrganizacion(organizacionId);
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
            return info;
        }



        public OrganizacionInfo ObtenerPorID(OrganizacionInfo organizacionInfo)
        {
            OrganizacionInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorID(organizacionInfo.OrganizacionID);
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
            return resultadoOrganizacion;
        }
        /// <summary>
        /// Obtiene por id filtro tipo organizacion
        /// </summary>
        /// <param name="organizacionInfo"></param>
        /// <returns></returns>
        public OrganizacionInfo ObtenerPorIDFiltroTipoOrganizacion(OrganizacionInfo organizacionInfo)
        {
            OrganizacionInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorIDFiltroTipoOrganizacion(organizacionInfo.OrganizacionID);
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
            return resultadoOrganizacion;
        }

        public ResultadoInfo<OrganizacionInfo> ObtenerPorDescripcion(PaginacionInfo pagina, OrganizacionInfo organizacionInfo)
        {
            try
            {
                Logger.Info();
                var resultadoOrganizacion = new ResultadoInfo<OrganizacionInfo>();

                return resultadoOrganizacion;
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

        public OrganizacionInfo ObtenerPorDependencia(OrganizacionInfo organizacionInfo, IList<IDictionary<IList<string>, object>> dependencias)
        {
            OrganizacionInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorDependencias(organizacionInfo, dependencias);
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
            return resultadoOrganizacion;
        }

        public ResultadoInfo<OrganizacionInfo> ObtenerPorDependencia(PaginacionInfo pagina
                                                                    , OrganizacionInfo organizacionInfo
                                                                    , IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorDependencias(pagina, organizacionInfo, dependencias);
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
            return resultadoOrganizacion;
        }

        public ResultadoInfo<OrganizacionInfo> ObtenerPorEmbarqueTipoOrganizacionPaginado(PaginacionInfo pagina
                                                                    , OrganizacionInfo organizacionInfo
                                                                    , IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorEmbarqueTipoOrganizacionPaginado(pagina, organizacionInfo, dependencias);
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
            return resultadoOrganizacion;
        }

        public OrganizacionInfo ObtenerPorEmbarqueTipoOrganizacion(OrganizacionInfo organizacionInfo
                                                                 , IList<IDictionary<IList<string>, object>> dependencias)
        {
            OrganizacionInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorEmbarqueTipoOrganizacion(organizacionInfo, dependencias);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        public ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaOrigenID(PaginacionInfo pagina, OrganizacionInfo filtro
                                                                    , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorPaginaOrigenID(pagina, filtro, dependencias);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        public OrganizacionInfo ObtenerPorDependenciasOrigenID(OrganizacionInfo filtro
                                                     , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            OrganizacionInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorDependenciasOrigenID(filtro, dependencias);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        ///     Obtiene una lista de organizaciones que tengan embarques pendientes por recibir
        /// </summary>
        /// <param name="organizacionId">Identificador de la organzación</param>
        /// <param name="estatus">Estatus del embarque </param>
        /// <returns></returns>
        public IList<OrganizacionInfo> ObtenerPendientesRecibir(int organizacionId, int estatus)
        {
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                IList<OrganizacionInfo> result = organizacionBL.ObtenerPendientesRecibir(organizacionId, estatus);

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
        ///      Obtiene un organizacion por su Id
        /// </summary>
        /// <returns> </returns>
        public OrganizacionInfo ObtenerPorIdConIva(int organizacionId)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                info = organizacionBL.ObtenerPorIdConIva(organizacionId);
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
            return info;
        }

        /// <summary>
        /// Obtiene un organizacion por su Id que solo pertencezca a ganadera.
        /// </summary>
        /// <returns> </returns>
        public OrganizacionInfo ObtenerSoloGanaderaPorID(OrganizacionInfo filtro)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                info = organizacionBL.ObtenerSoloGanaderaPorID(filtro);
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
            return info;
        }


        /// <summary>
        /// Obtener organizaciones por tipos de organizacion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaPorTiposOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorPaginaPorTiposOrganizacion(pagina,filtro);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        /// Obtener organizaciones por tipos de organizacion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaPorFiltroTipoOrganizacion(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorPaginaFiltroTipoOrganizacion(pagina, filtro);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        /// Obtiene una organizacion por id y por tipos de organizacion
        /// </summary>
        /// <param name="organizacionInfo"></param>
        /// <returns></returns>
        public OrganizacionInfo ObtenerPorIdFiltroTiposOrganizacion(OrganizacionInfo organizacionInfo)
        {
            OrganizacionInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorIdFiltroTiposOrganizacion(organizacionInfo);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        /// Obtiene el listado de organizaciones del producto seleccionado
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>
        public List<OrganizacionInfo> ObtenerOrganizacionesProductoPremezcla(int productoId)
        {
            List<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBl = new OrganizacionBL();
                resultadoOrganizacion = organizacionBl.ObtenerOrganizacionesProductoPremezcla(productoId);
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
            return resultadoOrganizacion;
        }

       

        /// <summary>
        /// Obtiene todas las organizaciones de tipo centros, cadis, descansos
        /// </summary>
        /// <returns></returns>
        public OrganizacionInfo ObtenerTipoCentrosCadisDescansos(OrganizacionInfo organizacion)
        {
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                var res = organizacionBL.ObtenerTipoCentrosCadisDescansos(organizacion);
                
                return res;

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

        public OrganizacionInfo ObtenerPorIDSoloCentros(OrganizacionInfo organizacionInfo)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                info = organizacionBL.ObtenerPorIDSoloCentros(organizacionInfo.OrganizacionID);
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
            return info;
        }

        public OrganizacionInfo ObtenerPorIdSoloCentrosCadisDescansos(OrganizacionInfo organizacionInfo)
        {
            OrganizacionInfo info;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                info = organizacionBL.ObtenerPorIDSoloCentrosCadisDescansos(organizacionInfo.OrganizacionID);
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
            return info;
        }

        public OrganizacionInfo ObtenerPorIDOmitiendoTipos(OrganizacionInfo organizacionInfo)
        {
            OrganizacionInfo resultadoOrganizacion;
            List<TipoOrganizacion> TiposOmitidos = new List<TipoOrganizacion>();
            
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                TiposOmitidos.Add(TipoOrganizacion.CompraDirecta);
                resultadoOrganizacion = organizacionBL.ObtenerPorID(organizacionInfo.OrganizacionID);

                if (resultadoOrganizacion != null)
                {
                    foreach (var tipo in TiposOmitidos)
                    {
                        if (resultadoOrganizacion.TipoOrganizacion.TipoOrganizacionID == (int)tipo || resultadoOrganizacion.Activo == EstatusEnum.Inactivo)
                        {
                            resultadoOrganizacion = null;
                            break;
                        }
                    }
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
            return resultadoOrganizacion;
        }

        public ResultadoInfo<OrganizacionInfo> ObtenerPorPaginaOmitiendoTipos(PaginacionInfo pagina, OrganizacionInfo filtro)
        {
            ResultadoInfo<OrganizacionInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorPaginaAdministranIngredientes(pagina, filtro);
               
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
            return resultadoOrganizacion;
        }

        /// <summary>
        /// Obtiene todas las organizaciones de tipo ganaderas, Cadis y Descanso
        /// </summary>
        /// <returns></returns>
        public IList<OrganizacionInfo> ObtenerTipoGanaderasCadisDescanso()
        {
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                IList<OrganizacionInfo> lista = organizacionBL.ObtenerTipoGanaderasCadisDescanso();

                return lista;
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
        /// Obtiene todas las organizaciones origen
        /// </summary>
        /// <returns></returns>
        public IList<OrganizacionInfo> ObtenerOrganizacionesOrigen()
        {
            try
            {
                Logger.Info();
                var organizacionBL = new OrganizacionBL();
                IList<OrganizacionInfo> lista = organizacionBL.ObtenerOrganizacionesOrigen();

                return lista;
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
