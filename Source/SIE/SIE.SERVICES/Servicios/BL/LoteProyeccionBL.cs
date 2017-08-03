using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class LoteProyeccionBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad LoteProyeccion
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(LoteProyeccionInfo info)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                int result = info.LoteProyeccionID;
                if (info.LoteProyeccionID == 0)
                {
                    result = loteProyeccionDAL.Crear(info);
                }
                else
                {
                    loteProyeccionDAL.Actualizar(info);
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
        internal ResultadoInfo<LoteProyeccionInfo> ObtenerPorPagina(PaginacionInfo pagina, LoteProyeccionInfo filtro)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                ResultadoInfo<LoteProyeccionInfo> result = loteProyeccionDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de LoteProyeccion
        /// </summary>
        /// <returns></returns>
        internal IList<LoteProyeccionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                IList<LoteProyeccionInfo> result = loteProyeccionDAL.ObtenerTodos();
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
        internal IList<LoteProyeccionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                IList<LoteProyeccionInfo> result = loteProyeccionDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad LoteProyeccion por su Id
        /// </summary>
        /// <param name="loteProyeccionID">Obtiene una entidad LoteProyeccion por su Id</param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerPorID(int loteProyeccionID)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                LoteProyeccionInfo result = loteProyeccionDAL.ObtenerPorID(loteProyeccionID);
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
        /// Obtiene una entidad LoteProyeccion por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad LoteProyeccion por su Id</param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                LoteProyeccionInfo result = loteProyeccionDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene la proyeccion por lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDal = new LoteProyeccionDAL();
                var result = loteProyeccionDal.ObtenerPorLote(lote);
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
        /// Obtiene una lista de lotes proyeccion
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal IList<LoteProyeccionInfo> ObtenerPorLoteXML(int organizacionId, IList<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                IList<LoteProyeccionInfo> result = loteProyeccionDAL.ObtenerPorLoteXML(organizacionId, lotes);
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
        /// Metodo para obtener las proyecciones de los corrales origenes para un corral
        /// </summary>
        /// <param name="loteCorral"></param>
        /// <returns></returns>
        internal List<LoteProyeccionInfo> ObtenerProyeccionDeLotesOrigen(LoteCorralReimplanteInfo loteCorral)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                return loteProyeccionDAL.ObtenerProyeccionDeLotesOrigen(loteCorral);
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
        /// Obtiene la proyeccion por lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerPorLoteCompleto(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDal = new LoteProyeccionDAL();
                var result = loteProyeccionDal.ObtenerPorLoteCompleto(lote);
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
        /// Metodo para guardar la configuracion de reimplantes
        /// </summary>
        /// <param name="configuracionReimplante">Representa el Contexto de lo que se va a guardar</param>
        public int GuardarConfiguracionReimplante(ConfiguracionReimplanteModel configuracionReimplante)
        {
            try
            {
                Logger.Info();
                var loteReimplanteBL = new LoteReimplanteBL();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                var loteDAL = new LoteDAL();
                int result = configuracionReimplante.LoteProyeccion.LoteProyeccionID;
                var opciones = new TransactionOptions();
                var filtroDisponilidadInfo = new FiltroDisponilidadInfo
                                                                    {
                                                                        UsuarioId = configuracionReimplante.LoteProyeccion.UsuarioCreacionID,
                                                                        ListaLoteDisponibilidad =
                                                                            new List<DisponibilidadLoteInfo>
                                                                                {
                                                                                    new DisponibilidadLoteInfo
                                                                                        {
                                                                                            LoteId = configuracionReimplante.Lote.LoteID,
                                                                                            FechaDisponibilidad = configuracionReimplante.FechaDisponible,
                                                                                            DisponibilidadManual = false
                                                                                        }
                                                                                }

                                                                    };
                opciones.IsolationLevel = IsolationLevel.ReadUncommitted;
                opciones.Timeout = new TimeSpan(2, 0, 0);
                using (var scope = new TransactionScope(TransactionScopeOption.Required, opciones))
                {
                    if (configuracionReimplante.LoteProyeccion.LoteProyeccionID == 0)
                    {
                        configuracionReimplante.LoteProyeccion.OrganizacionID = configuracionReimplante.Corral.Organizacion.OrganizacionID;
                        configuracionReimplante.LoteProyeccion.LoteID = configuracionReimplante.Lote.LoteID;
                        result = loteProyeccionDAL.Crear(configuracionReimplante.LoteProyeccion);
                        configuracionReimplante.LoteProyeccion.LoteProyeccionID = result;
                        loteProyeccionDAL.CrearBitacora(configuracionReimplante.LoteProyeccion);
                    }
                    else
                    {
                        if (configuracionReimplante.LoteProyeccion.AplicaBitacora)
                        {
                            loteProyeccionDAL.Actualizar(configuracionReimplante.LoteProyeccion);
                            loteProyeccionDAL.CrearBitacora(configuracionReimplante.LoteProyeccion);
                        }
                    }
                    if (configuracionReimplante.LoteProyeccion.ListaReimplantes != null &&
                        configuracionReimplante.LoteProyeccion.ListaReimplantes.Any())
                    {
                        configuracionReimplante.LoteProyeccion.ListaReimplantes.ToList().ForEach(
                            reimp => reimp.LoteProyeccionID = result);
                        loteReimplanteBL.GuardarListaReimplantes(
                            configuracionReimplante.LoteProyeccion.ListaReimplantes.ToList());
                    }
                    loteDAL.ActualizarLoteDisponibilidad(filtroDisponilidadInfo);
                    if(configuracionReimplante.Lote.AplicaCierreLote)
                    {
                        loteDAL.ActualizaFechaCierre(configuracionReimplante.Lote.LoteID, configuracionReimplante.UsuarioCreacionID);
                    }
                    scope.Complete();
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
        /// Obtiene una entidad LoteProyeccion por su Id
        /// </summary>
        /// <param name="loteProyeccionID">Obtiene una entidad LoteProyeccion por su Id</param>
        /// <returns></returns>
        internal LoteProyeccionInfo ObtenerBitacoraPorLoteProyeccionID(int loteProyeccionID)
        {
            try
            {
                Logger.Info();
                var loteProyeccionDAL = new LoteProyeccionDAL();
                LoteProyeccionInfo result = loteProyeccionDAL.ObtenerBitacoraPorLoteProyeccionID(loteProyeccionID);
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

