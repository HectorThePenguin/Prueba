using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using System.Linq;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class CalidadGanadoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una Calidad de ganado
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(CalidadGanadoInfo info)
        {
            try
            {
                Logger.Info();
                var calidadGanadoDAL = new CalidadGanadoDAL();
                if (info.CalidadGanadoID == 0)
                {
                    calidadGanadoDAL.Crear(info);
                }
                else
                {
                    calidadGanadoDAL.Actualizar(info);
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
        internal ResultadoInfo<CalidadGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, CalidadGanadoInfo filtro)
        {
            ResultadoInfo<CalidadGanadoInfo> result;
            try
            {
                Logger.Info();
                var calidadGanadoDAL = new CalidadGanadoDAL();
                result = calidadGanadoDAL.ObtenerPorPagina(pagina, filtro);
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
            return result;
        }

        /// <summary>
        ///     Obtiene un lista de Calidad de Ganado
        /// </summary>
        /// <returns></returns>
        internal List<CalidadGanadoInfo> ObtenerTodos()
        {
            List<CalidadGanadoInfo> lista;
            try
            {
                Logger.Info();
                var calidadGanadoDAL = new CalidadGanadoDAL();
                lista = calidadGanadoDAL.ObtenerTodos();
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
            return lista;
        }

        /// <summary>
        ///  Obtiene una lista de CalidadGanado filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<CalidadGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            List<CalidadGanadoInfo> lista;
            try
            {
                Logger.Info();
                var calidadGanadoDAL = new CalidadGanadoDAL();
                lista = calidadGanadoDAL.ObtenerTodos(estatus);
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
            return lista;
        }

        /// <summary>
        ///     Obtiene una Calidad de ganado por su Id
        /// </summary>
        /// <param name="calidadGanadoID"></param>
        /// <returns></returns>
        internal CalidadGanadoInfo ObtenerPorID(int calidadGanadoID)
        {
            try
            {
                Logger.Info();
                var calidadGanadoDAL = new CalidadGanadoDAL();
                CalidadGanadoInfo result = calidadGanadoDAL.ObtenerPorID(calidadGanadoID);
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
        /// Obtiene la Lista de Entrada Ganado Calidad
        /// </summary>
        /// <returns></returns>
        internal List<EntradaGanadoCalidadInfo> ObtenerListaCalidadGanado()
        {
            List<EntradaGanadoCalidadInfo> lista;
            try
            {
                Logger.Info();
                var calidadGanadoDAL = new CalidadGanadoDAL();
                lista = calidadGanadoDAL.ObtenerListaCalidadGanado();
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
            return lista;
        }

        /// <summary>
        /// Obtiene una entidad CalidadGanado por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="sexo"> </param>
        /// <returns></returns>
        internal CalidadGanadoInfo ObtenerPorDescripcion(string descripcion, Sexo sexo)
        {
            try
            {
                Logger.Info();
                var calidadGanadoDAL = new CalidadGanadoDAL();
                CalidadGanadoInfo result = calidadGanadoDAL.ObtenerPorDescripcion(descripcion, sexo);
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
        ///  Obtiene una lista de CalidadGanado filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<CalidadGanadoInfo> ObtenerTodosCapturaCalidad(EstatusEnum estatus)
        {
            List<CalidadGanadoInfo> calidadesAgrupadas;
            try
            {
                Logger.Info();
                var calidadGanadoDAL = new CalidadGanadoDAL();
                List<CalidadGanadoInfo> lista = calidadGanadoDAL.ObtenerTodos(estatus);
                calidadesAgrupadas = (from cali in lista
                                          group cali by new { cali.Calidad, cali.Descripcion } into agru
                                          let calidadGanadoInfo = agru.FirstOrDefault()
                                          let calidadGanadoHembraInfo = agru.FirstOrDefault(cali=> cali.Sexo == Sexo.Hembra)
                                          let calidadGanadoMachoInfo = agru.FirstOrDefault(cali=> cali.Sexo == Sexo.Macho)
                                          where calidadGanadoInfo != null
                                          select new CalidadGanadoInfo
                                          {
                                              CalidadGanadoID = calidadGanadoInfo.CalidadGanadoID,
                                              CalidadGanadoHembraID = calidadGanadoHembraInfo.CalidadGanadoID.HasValue ? calidadGanadoHembraInfo.CalidadGanadoID.Value : 0,
                                              CalidadGanadoMachoID = calidadGanadoMachoInfo.CalidadGanadoID.HasValue ? calidadGanadoMachoInfo.CalidadGanadoID.Value : 0,
                                              Calidad = agru.Key.Calidad,
                                              Descripcion = agru.Key.Descripcion
                                          }).ToList();
                calidadesAgrupadas.ForEach(calidad =>
                    {
                        var calidadEnLinea =
                            ConstantesBL.CalidadEnLinea.FirstOrDefault(linea => linea == calidad.Calidad);
                        if(calidadEnLinea != null)
                        {
                            calidad.ClasificacionCalidad = ConstantesBL.EnLinea;
                        }

                        var calidadProduccion =
                            ConstantesBL.CalidadProduccion.FirstOrDefault(linea => linea == calidad.Calidad);
                        if (calidadProduccion != null)
                        {
                            calidad.ClasificacionCalidad = ConstantesBL.Produccion;
                        }

                        var calidadVenta =
                            ConstantesBL.CalidadVenta.FirstOrDefault(linea => linea == calidad.Calidad);
                        if (calidadVenta != null)
                        {
                            calidad.ClasificacionCalidad = ConstantesBL.Venta;
                        }
                    });
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
            return calidadesAgrupadas;
        }
    }
}
