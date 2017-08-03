using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class EmbarqueBL
    {
        /// <summary>
        /// Obtiene una lista de embarques pendiente de recibir 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaEmbarqueInfo> ObtenerEmbarquesPedientesPorPagina(PaginacionInfo pagina, FiltroEmbarqueInfo filtro)
        {
            ResultadoInfo<EntradaEmbarqueInfo> result;
            try
            {
                Logger.Info();
                var embarqueDAL = new EmbarqueDAL();
                result = embarqueDAL.ObtenerEmbarquesPedientesPorPagina(pagina, filtro);
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
        ///     Metodo que guarda un Embarque
        /// </summary>
        /// <param name="embarqueInfo"></param>
        internal int GuardarEmbarque(EmbarqueInfo embarqueInfo)
        {
            try
            {
                Logger.Info();
                var embarqueDAL = new EmbarqueDAL();
                using (var transaction = new TransactionScope())
                {
                    if (embarqueInfo.EmbarqueID != 0)
                    {
                        embarqueDAL.Actualizar(embarqueInfo);
                    }
                    else
                    {
                        embarqueInfo.EmbarqueID = embarqueDAL.Crear(embarqueInfo);
                    }

                    embarqueDAL.GuardarEscala(embarqueInfo.ListaEscala,
                                                          embarqueInfo.EmbarqueID);

                    //Obtener por Programacion por ID
                    IList<EmbarqueDetalleInfo> listaEscalasGuardado =
                        embarqueDAL.ObtenerEscalasPorEmbarqueID(
                            embarqueInfo.EmbarqueID);

                    //relacionar el identity EmbarqueDetalleID a la lista de costos  
                    IEnumerable<CostoEmbarqueDetalleInfo> listaCostos =
                         embarqueInfo.ListaEscala.SelectMany(x => x.ListaCostoEmbarqueDetalle).ToList();

                    foreach (var costo in listaCostos)
                    {
                        if (costo.EmbarqueDetalleID != 0)
                        {
                            continue;
                        }
                        var escalaInfo =
                            listaEscalasGuardado.FirstOrDefault(x => x.Orden == costo.Orden);
                        if (escalaInfo != null)
                        {
                            costo.EmbarqueDetalleID = escalaInfo.EmbarqueDetalleID;
                        }
                    }

                    if (listaCostos.Any())
                    {
                        embarqueDAL.GuardarCosto(listaCostos);
                    }

                    var embarqueGuardado = embarqueDAL.ObtenerPorID(embarqueInfo.EmbarqueID);
                    transaction.Complete();

                    return embarqueGuardado.FolioEmbarque;
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
        ///     Obtiene registro de la programación de embarque por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="embarqueId"> </param>
        /// <returns></returns>
        internal EmbarqueInfo ObtenerPorID(int embarqueId)
        {
            EmbarqueInfo result;
            try
            {
                Logger.Info();
                var embarqueDAL = new EmbarqueDAL();
                result = embarqueDAL.ObtenerPorID(embarqueId);
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
        ///     Obtiene registro de la programación de embarque por su folio
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="folioEmbarque"> </param>
        /// <returns></returns>
        internal EmbarqueInfo ObtenerPorFolioEmbarque(int folioEmbarque)
        {
            EmbarqueInfo result;
            try
            {
                Logger.Info();
                var embarqueDAL = new EmbarqueDAL();
                result = embarqueDAL.ObtenerPorFolioEmbarque(folioEmbarque);
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
        ///     Obtiene registro de la programación de embarque por su folio
        /// </summary>
        /// <param name="embarqueId"> </param>
        /// <returns></returns>
        internal IList<EmbarqueDetalleInfo> ObtenerEscalasPorEmbarqueID(int embarqueId)
        {
            IList<EmbarqueDetalleInfo> result;
            try
            {
                Logger.Info();
                var embarqueDAL = new EmbarqueDAL();
                result = embarqueDAL.ObtenerEscalasPorEmbarqueID(embarqueId);
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
        /// Obtiene un listado de entradas Activas Paginadas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EmbarqueInfo> ObtenerEntradasActivasPorPagina(PaginacionInfo pagina, FiltroEmbarqueInfo filtro)
        {
            ResultadoInfo<EmbarqueInfo> result;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EmbarqueDAL();
                result = entradaGanadoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Metodo que actualiza el estatus a recibido de la programacion de embarque
        /// </summary>
        /// <param name="entradaGanado"></param>
        internal void ActualizarEstatusDetalle(EntradaGanadoInfo entradaGanado)
        {
            try
            {
                Logger.Info();
                var embarqueDAL = new EmbarqueDAL();
                embarqueDAL.ActualizarEstatusDetalle(entradaGanado);
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
        /// Metodo que actualiza el estatus a recibido de la programacion de embarque (Cabecero) 
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <param name="estatus"></param>
        internal void ActualizarEstatus(EntradaGanadoInfo entradaGanado, Estatus estatus)
        {
            try
            {
                Logger.Info();
                var embarqueDAL = new EmbarqueDAL();
                embarqueDAL.ActualizarEstatus(entradaGanado, estatus);
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
        /// Metodo que Obtiene los embarques que estan pendientes de recibir 
        /// </summary>
        /// <param name="entradaGanado"></param>
        internal int PendientesRecibir(EntradaGanadoInfo entradaGanado)
        {
            int totalPendientes;
            try
            {
                Logger.Info();
                var embarqueDAL = new EmbarqueDAL();
                totalPendientes = embarqueDAL.PendientesRecibir(entradaGanado);
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
            return totalPendientes;
        }

        /// <summary>
        ///     Obtiene registro de la programación de embarque por su Id
        /// </summary>
        /// <returns></returns>
        internal EmbarqueInfo ObtenerPorFolioEmbarqueOrganizacion(int folioEmbarque, int organizacionId)
        {
            EmbarqueInfo info;
            try
            {
                Logger.Info();
                var embarqueDAL = new EmbarqueDAL();
                info = embarqueDAL.ObtenerPorFolioEmbarqueOrganizacion(folioEmbarque, organizacionId);
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
    }
}