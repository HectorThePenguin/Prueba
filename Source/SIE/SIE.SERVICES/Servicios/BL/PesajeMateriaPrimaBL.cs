using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.ORM;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Servicios.BL
{
    public class PesajeMateriaPrimaBL : IDisposable
    {
        PesajeMateriaPrimaDAL pesajeMateriaPrimaDAL;

        public PesajeMateriaPrimaBL()
        {
            pesajeMateriaPrimaDAL = new PesajeMateriaPrimaDAL();
        }

        public void Dispose()
        {
            pesajeMateriaPrimaDAL.Disposed += (s, e) =>
            {
                pesajeMateriaPrimaDAL = null;
            };
            pesajeMateriaPrimaDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de PesajeMateriaPrima
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<PesajeMateriaPrimaInfo> ObtenerPorPagina(PaginacionInfo pagina, PesajeMateriaPrimaInfo filtro)
        {
            try
            {
                Logger.Info();
                return pesajeMateriaPrimaDAL.ObtenerPorPagina(pagina, filtro);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de PesajeMateriaPrima
        /// </summary>
        /// <returns></returns>
        public IList<PesajeMateriaPrimaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return pesajeMateriaPrimaDAL.ObtenerTodos().ToList();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de PesajeMateriaPrima filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<PesajeMateriaPrimaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return pesajeMateriaPrimaDAL.ObtenerTodos().ToList();//.Where(e=> e.Activo == estatus).ToList();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de PesajeMateriaPrima por su Id
        /// </summary>
        /// <param name="pesajeMateriaPrimaId">Obtiene una entidad PesajeMateriaPrima por su Id</param>
        /// <returns></returns>
        public PesajeMateriaPrimaInfo ObtenerPorID(int pesajeMateriaPrimaId)
        {
            try
            {
                Logger.Info();
                return pesajeMateriaPrimaDAL.ObtenerTodos().Where(e=> e.PesajeMateriaPrimaID == pesajeMateriaPrimaId).FirstOrDefault();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad PesajeMateriaPrima
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(PesajeMateriaPrimaInfo info)
        {
            try
            {
                Logger.Info();
                return pesajeMateriaPrimaDAL.Guardar(info);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad PesajeMateriaPrima
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<FiltroTicketInfo> ObtenerPorFiltro(FiltroTicketInfo filtro)
        {
            try
            {
                Logger.Info();
                return pesajeMateriaPrimaDAL.ObtenerPorFiltro(filtro);
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
        /// Metodo para Guardar/Modificar una entidad PesajeMateriaPrima
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<FiltroTicketProduccionMolino> ObtenerValoresTicketProduccion(FiltroTicketInfo filtro)
        {
            try
            {
                Logger.Info();
                List<FiltroTicketProduccionMolino> lista = pesajeMateriaPrimaDAL.ObtenerValoresTicketProduccion(filtro);
                if(lista == null)
                {
                    return null;
                }
                lista.ForEach(ticket=>
                    {
                        ticket.HoraTicketInicial = DateTime.Now.ToString("HH:mm");
                    });
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
        /// Metodo que crea un registro en PesajeMateriaPrima
        /// </summary>
        /// <returns></returns>
        public PesajeMateriaPrimaInfo Crear(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            PesajeMateriaPrimaInfo result;
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new Integracion.DAL.Implementacion.PesajeMateriaPrimaDAL();
                result = pesajeMateriaPrimaDal.Crear(pesajeMateriaPrimaInfo);
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
        /// Metodo que obtiene un registro por ticket y pedido
        /// </summary>
        /// <returns></returns>
        public PesajeMateriaPrimaInfo ObtenerPorTicketPedido(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new Integracion.DAL.Implementacion.PesajeMateriaPrimaDAL();
                return pesajeMateriaPrimaDal.ObtenerPorTicketPedido(pesajeMateriaPrimaInfo);
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
        /// Obtiene los pesajes de materia prima por programacion
        /// </summary>
        /// <param name="programacionMateriaPrimaId"></param>
        /// <returns></returns>
        internal List<PesajeMateriaPrimaInfo> ObtenerPesajesPorProgramacionMateriaPrimaId(int programacionMateriaPrimaId)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new Integracion.DAL.Implementacion.PesajeMateriaPrimaDAL();
                var pesajeMateriaPrima = pesajeMateriaPrimaDal.ObtenerPesajesPorProgramacionMateriaPrimaId(programacionMateriaPrimaId);

                if (pesajeMateriaPrima != null)
                {
                    var proveedorChoferBl = new ProveedorChoferBL();
                    var camionBl = new CamionBL();
                    foreach (PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo in pesajeMateriaPrima)
                    {
                        pesajeMateriaPrimaInfo.ProveedorChofer =
                            proveedorChoferBl.ObtenerProveedorChoferPorId(pesajeMateriaPrimaInfo.ProveedorChoferID);
                        pesajeMateriaPrimaInfo.Camion = camionBl.ObtenerPorID(pesajeMateriaPrimaInfo.CamionID);

                    }
                }
                return pesajeMateriaPrima;
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
        /// Obtiene los pesajes de materia prima por programacion
        /// </summary>
        /// <param name="programacionMateriaPrimaId"></param>
        /// <returns></returns>
        internal List<PesajeMateriaPrimaInfo> ObtenerPesajesPorProgramacionMateriaPrimaIdSinActivo(int programacionMateriaPrimaId)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new Integracion.DAL.Implementacion.PesajeMateriaPrimaDAL();
                var pesajeMateriaPrima = pesajeMateriaPrimaDal.ObtenerPesajesPorProgramacionMateriaPrimaIdSinActivo(programacionMateriaPrimaId);

                if (pesajeMateriaPrima != null)
                {
                    var proveedorChoferBl = new ProveedorChoferBL();
                    var camionBl = new CamionBL();
                    foreach (PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo in pesajeMateriaPrima)
                    {
                        pesajeMateriaPrimaInfo.ProveedorChofer =
                            proveedorChoferBl.ObtenerProveedorChoferPorId(pesajeMateriaPrimaInfo.ProveedorChoferID);
                        pesajeMateriaPrimaInfo.Camion = camionBl.ObtenerPorID(pesajeMateriaPrimaInfo.CamionID);

                    }
                }
                return pesajeMateriaPrima;
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
        /// Actualiza todos los campos del pesaje ( se consulta primero en base al Id y se sobre Escribe)
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        internal void ActualizarPesajePorId(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new Integracion.DAL.Implementacion.PesajeMateriaPrimaDAL();
                pesajeMateriaPrimaDal.ActualizarPesajePorId(pesajeMateriaPrimaInfo);
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
        /// Obtiene los valores para generar la poliza
        /// de Pase a Proceso
        /// </summary>
        internal List<PolizaPaseProcesoModel> ObtenerValoresPolizaPaseProceso(int folioPedido, int organizacionID, string xmlLote)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new Integracion.DAL.Implementacion.PesajeMateriaPrimaDAL();
                List<PolizaPaseProcesoModel> polizaPaseProceso =
                    pesajeMateriaPrimaDal.ObtenerValoresPolizaPaseProceso(folioPedido, organizacionID, xmlLote);
                return polizaPaseProceso;
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
        /// Obtiene los datos de pase a proceso para 
        /// su reimpresion
        /// </summary>
        /// <param name="folioPedido"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public List<PolizaPaseProcesoModel> ObtenerValoresPolizaPaseProcesoReimpresion(int folioPedido, int organizacionId)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new Integracion.DAL.Implementacion.PesajeMateriaPrimaDAL();
                List<PolizaPaseProcesoModel> polizaPaseProceso =
                    pesajeMateriaPrimaDal.ObtenerValoresPolizaPaseProcesoReimpresion(folioPedido, organizacionId);
                return polizaPaseProceso;
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
        /// Obtiene una entidad de PesajeMateriaPrima por su Id
        /// </summary>
        /// <param name="pesaje">Obtiene una entidad PesajeMateriaPrima por su Id</param>
        /// <returns></returns>
        internal PesajeMateriaPrimaInfo ObtenerPorId(PesajeMateriaPrimaInfo pesaje)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new Integracion.DAL.Implementacion.PesajeMateriaPrimaDAL();
                return pesajeMateriaPrimaDal.ObtenerPorId(pesaje);
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
        /// Obtiene los Folios y ticket de los pase a proceso
        /// </summary>
        /// <param name="movimientos"></param>
        /// <returns></returns>
        internal List<ReporteInventarioPaseProcesoModel> ObtenerFoliosPaseProceso(List<long> movimientos)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaDal = new Integracion.DAL.Implementacion.PesajeMateriaPrimaDAL();
                return pesajeMateriaPrimaDal.ObtenerFoliosPaseProceso(movimientos);
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
