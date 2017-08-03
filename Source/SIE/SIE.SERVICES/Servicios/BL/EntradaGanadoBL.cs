using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using System.Linq;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    internal class EntradaGanadoBL
    {
        /// <summary>
        ///     Metodo que crear una entrada de ganado
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <param name="actualizarRecibido"></param>
        internal int GuardarEntradaGanado(EntradaGanadoInfo entradaGanado, bool actualizarRecibido)
        {
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                var embarqueBL = new EmbarqueBL();
                var loteBL = new LoteBL();

                ValidaCamposEntradaGanado(entradaGanado);
                ObservableCollection<PesoUnificadoInfo> pesosUnificados = entradaGanado.PesosUnificados;
                int entradaGanadoID = 0;
                using (var transaccion = new TransactionScope())
                {
                    if (pesosUnificados != null)
                    {
                        List<EntradaGanadoInfo> entradas =
                            pesosUnificados.Where(id => id.EntradaGanado.EntradaGanadoID > 0).Select(
                                entrada => entrada.EntradaGanado).ToList();
                        for (var indexPesos = 0; indexPesos < entradas.Count; indexPesos++)
                        {
                            entradaGanadoID = GuardarEntradaGanado(entradas[indexPesos],
                                                                   entradaGanado,
                                                                   actualizarRecibido, loteBL,
                                                                   entradaGanadoDAL, embarqueBL);
                        }
                    }
                    else
                    {
                        entradaGanadoID = GuardarEntradaGanado(entradaGanado, entradaGanado, actualizarRecibido, loteBL,
                                                               entradaGanadoDAL, embarqueBL);
                    }
                    transaccion.Complete();
                }
                return entradaGanadoID;
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

        private int GuardarEntradaGanado(EntradaGanadoInfo entradaGanado, EntradaGanadoInfo entradaGanadoPantalla
                                       , bool actualizarRecibido, LoteBL loteBL,
                                         EntradaGanadoDAL entradaGanadoDAL, EmbarqueBL embarqueBL)
        {
            if (entradaGanado.PesoTara > 0)
            {
                if (entradaGanado.Lote.LoteID != 0)
                {
                    if (!entradaGanado.ImpresionTicket && !actualizarRecibido)
                    {
                        loteBL.AcutalizaCabezasLote(entradaGanado.Lote);
                    }
                }
                else
                {
                    int loteID = loteBL.GuardaLote(entradaGanado.Lote);
                    entradaGanado.Lote.LoteID = loteID;
                    int? usuarioModificacionID = entradaGanado.UsuarioModificacionID.HasValue
                                                     ? entradaGanado.UsuarioModificacionID
                                                     : entradaGanado.UsuarioCreacionID;
                    loteBL.ActualizaFechaCierre(entradaGanado.Lote.LoteID, usuarioModificacionID);
                }
            }
            int entradaGanadoID = entradaGanado.EntradaGanadoID;
            if (entradaGanadoID != 0)
            {
                entradaGanado.ImpresionTicket = entradaGanado.PesoTara > 0;
                entradaGanadoDAL.ActualizaEntradaGanado(entradaGanado);
                entradaGanadoID = entradaGanado.EntradaGanadoID;
            }
            else
            {
                entradaGanadoID = entradaGanadoDAL.GuardarEntradaGanado(entradaGanado);
            }

            if (entradaGanado.TipoOrigen == TipoOrganizacion.Ganadera.GetHashCode() 
                || (entradaGanado.ListaCondicionGanado != null && entradaGanado.ListaCondicionGanado.Count > 0))
            {
                entradaGanadoDAL.GuardarEntradaCondicion(entradaGanado.ListaCondicionGanado, entradaGanadoID);
                if (actualizarRecibido && entradaGanado.EntradaGanadoID == entradaGanadoPantalla.EntradaGanadoID)
                {
                    embarqueBL.ActualizarEstatusDetalle(entradaGanado);
                    int totaPendientes = embarqueBL.PendientesRecibir(entradaGanado);

                    if (totaPendientes == 0)
                    {
                        embarqueBL.ActualizarEstatus(entradaGanado, Estatus.Recibido);
                    }
                }
            }
            return entradaGanadoID;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado por ID
        /// </summary>
        /// <param name="entradaGanadoID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorID(int entradaGanadoID)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPorID(entradaGanadoID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorFolioEntrada(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPorFolioEntrada(folioEntrada, organizacionID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene las entradas de ganado por Tipo de Corral
        /// </summary>
        /// <param name="organizacionID"></param>
        /// /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorTipoCorral(int organizacionID, int tipoCorralID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPorTipoCorral(organizacionID, tipoCorralID);
                if (entradaGanadoInfo != null)
                {
                    entradaGanadoInfo.Lista.ToList().ForEach(ent =>
                    {
                        if (ent.NivelGarrapata != NivelGarrapata.Ninguno)
                        {
                            ent.LeyendaNivelGarrapata = ConstantesBL.Banio;
                        }
                    });
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene un listado de entradas Activas Paginadas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradasActivasPorPagina(PaginacionInfo pagina,
                                                                                int organizacionID)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                listaEntradaGanado = entradaGanadoDAL.ObtenerEntradasActivasPorPagina(pagina, organizacionID);
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
            return listaEntradaGanado;
        }

        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorId(int id,
                                                             IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                var entradaGanadoInfo = entradaGanadoDAL.ObtenerPorFolioEntrada(id, dependencias);
                listaEntradaGanado = new ResultadoInfo<EntradaGanadoInfo> { Lista = new List<EntradaGanadoInfo> { entradaGanadoInfo } };
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
            return listaEntradaGanado;
        }

        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorDescripcion(PaginacionInfo pagina, string descripcion,
                                                                      IList<IDictionary<IList<string>, object>>
                                                                          dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();

                var folioEntrada = -1;
                int.TryParse(descripcion, out folioEntrada);

                var entradaGanadoDAL = new EntradaGanadoDAL();
                listaEntradaGanado = entradaGanadoDAL.ObtenerPorDescripcion(pagina, folioEntrada, dependencias);
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
            return listaEntradaGanado;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado por Programacion Embarque ID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <param name="organizacionOrigenID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorEmbarqueID(int embarqueID, int organizacionOrigenID)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPorEmbarqueID(embarqueID, organizacionOrigenID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado por Programacion Embarque ID
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorEmbarqueID(int embarqueID)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPorEmbarqueID(embarqueID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Valida que los campos de Tipo Cadena Contengan valor no Nulo
        /// </summary>
        /// <param name="entradaGanadoInfo"></param>
        private void ValidaCamposEntradaGanado(EntradaGanadoInfo entradaGanadoInfo)
        {
            if (String.IsNullOrWhiteSpace(entradaGanadoInfo.CheckList))
            {
                entradaGanadoInfo.CheckList = String.Empty;
            }
            if (String.IsNullOrWhiteSpace(entradaGanadoInfo.Observacion))
            {
                entradaGanadoInfo.Observacion = String.Empty;
            }
        }

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, EntradaGanadoInfo filtro)
        {
            ResultadoInfo<EntradaGanadoInfo> result;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
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
        /// Metodo que obtiene las entradas de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorIDOrganizacion(int organizacionID, int tipoCorralID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPorIDOrganizacion(organizacionID, tipoCorralID);
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
            return entradaGanadoInfo;
        }

        internal EntradaGanadoInfo ObtenerPorDependencias(EntradaGanadoInfo entradaGanadoInfo,
                                                        IList<IDictionary<IList<string>, object>> Dependencias)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                result = entradaGanadoDAL.ObtenerPorDependencias(entradaGanadoInfo, Dependencias);
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

        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPorDependencias(PaginacionInfo Pagina,
                                                                       EntradaGanadoInfo entradaGanadoInfo,
                                                                       IList<IDictionary<IList<string>, object>>
                                                                           Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                listaEntradaGanado = entradaGanadoDAL.ObtenerPorDependencias(Pagina, entradaGanadoInfo, Dependencias);
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
            return listaEntradaGanado;
        }

        internal EntradaGanadoInfo ObtenerEntradasGanadoRecibidasPorDependencias(EntradaGanadoInfo entradaGanadoInfo,
                                                                               IList<IDictionary<IList<string>, object>>
                                                                                   Dependencias)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                result = entradaGanadoDAL.ObtenerEntradasGanadoRecibidasPorDependencias(entradaGanadoInfo, Dependencias);
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

        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoRecibidasPaginaPorDependencias(
            PaginacionInfo Pagina, EntradaGanadoInfo entradaGanadoInfo,
            IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                listaEntradaGanado = entradaGanadoDAL.ObtenerEntradaGanadoRecibidasPaginaPorDependencias(Pagina,
                                                                                                         entradaGanadoInfo,
                                                                                                         Dependencias);
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
            return listaEntradaGanado;
        }

        internal EntradaGanadoInfo ObtenerEntradasGanadoRecibidas(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                result = entradaGanadoDAL.ObtenerEntradasGanadoRecibidas(folioEntrada, organizacionID);
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
        /// Metodo que obtiene entradas de ganado programadas.
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPartidasProgramadas(int folioEntrada, int organizacionID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPartidasProgramadas(folioEntrada, organizacionID);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene entradas de ganado programadas.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerPartidasProgramadasPorPaginas(PaginacionInfo pagina, int folioEntrada, int organizacionID)
        {
            ResultadoInfo<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPartidasProgramadasPorPaginacion(pagina, folioEntrada, organizacionID);
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
            return entradaGanadoInfo;
        }



        /// <summary>
        ///     Obtiene Calidad PorSexo
        /// </summary>
        /// <returns></returns>

        internal IList<CalidadGanadoInfo> ObtenerCalidadPorSexo(string sexo)
        {
            IList<CalidadGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerCalidadPorSexo(sexo);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        ///     Obtiene CalidadGanado Por CausaRechazo
        /// </summary>
        /// <returns></returns>

        internal IList<CausaRechazoInfo> ObtenerCalidadPorCausaRechazo()
        {
            IList<CausaRechazoInfo> causaRechazoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAl = new EntradaGanadoDAL();
                causaRechazoInfo = entradaGanadoDAl.ObtenerCalidadPorCausaRechazo();
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
            return causaRechazoInfo;
        }

        /// <summary>
        /// Metodo que obtiene el catalogo de clasificacion
        /// </summary>
        /// <returns></returns>

        internal IList<ClasificacionGanadoInfo> ObtenerCatClasificacion()
        {
            IList<ClasificacionGanadoInfo> catClasificacionInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAl = new EntradaGanadoDAL();
                catClasificacionInfo = entradaGanadoDAl.ObtenerCatClasificacion();
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
            return catClasificacionInfo;
        }

        internal EntradaGanadoInfo ObtenerEntradasGanadoCosteado(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                result = entradaGanadoDAL.ObtenerEntradasGanadoCosteado(folioEntrada, organizacionID);
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

        internal EntradaGanadoInfo ObtenerEntradasGanadoCosteadoPorDependencias(EntradaGanadoInfo entradaGanadoInfo,
                                                                              IList<IDictionary<IList<string>, object>>
                                                                                  Dependencias)
        {
            EntradaGanadoInfo result = null;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                result = entradaGanadoDAL.ObtenerEntradasGanadoCosteadoPorDependencias(entradaGanadoInfo, Dependencias);
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

        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoCosteadoPaginaPorDependencias(PaginacionInfo Pagina,
                                                                                                  EntradaGanadoInfo
                                                                                                      entradaGanadoInfo,
                                                                                                  IList
                                                                                                      <
                                                                                                      IDictionary
                                                                                                      <IList<string>,
                                                                                                      object>>
                                                                                                      Dependencias)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                listaEntradaGanado = entradaGanadoDAL.ObtenerEntradaGanadoCosteadoPaginaPorDependencias(Pagina,
                                                                                                        entradaGanadoInfo,
                                                                                                        Dependencias);
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
            return listaEntradaGanado;
        }

        /// <summary>
        /// Metodo para consultar los datos de la entrada para la captura de Calidad de Ganado
        /// </summary>
        /// <param name="filtroCalificacionGanado"></param>
        internal EntradaGanadoInfo ObtenerEntradaGanadoCapturaCalidad(FiltroCalificacionGanadoInfo filtroCalificacionGanado)
        {
            EntradaGanadoInfo result;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                result = entradaGanadoDAL.ObtenerEntradaGanadoCapturaCalidad(filtroCalificacionGanado);
                if (result == null)
                {
                    return null;
                }
                //if(result.ListaCondicionGanado != null && result.ListaCondicionGanado.Any())
                //{
                //    if(result.MensajeRetornoCalificacion == 0)
                //    {
                //        result.MensajeRetornoCalificacion = 4; //Mensaje que indica que ya se realizo el Conteo
                //    }

                //}
                var ganadoMuerto =
                    result.ListaCondicionGanado.FirstOrDefault(
                        cond => cond.CondicionID == ConidicionGanadoEnum.Muerto.GetHashCode());
                if (ganadoMuerto != null && ganadoMuerto.Cabezas > 0)
                {
                    result.CabezasRecibidas = result.CabezasRecibidas - ganadoMuerto.Cabezas;
                    result.CabezasMuertasCondicion = ganadoMuerto.Cabezas;
                    result.CabezasMuertas = ganadoMuerto.Cabezas;
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
            return result;
        }


        /// <summary>
        ///     Metodo que crear una entrada de ganado
        /// </summary>
        /// <param name="entradaGanado"></param>
        internal void GuardarEntradaCondicion(EntradaGanadoInfo entradaGanado)
        {
            try
            {
                Logger.Info();
                using (var transaccion = new TransactionScope())
                {
                    var entradaGanadoDAL = new EntradaGanadoDAL();
                    var loteBL = new LoteBL();
                    entradaGanadoDAL.GuardarEntradaCondicion(entradaGanado.ListaCondicionGanado,
                                                             entradaGanado.EntradaGanadoID);
                    if (entradaGanado.Lote != null && entradaGanado.Lote.LoteID == 0)
                    {
                        entradaGanado.Lote.LoteID = loteBL.GuardaLote(entradaGanado.Lote);
                        int? usuarioModificacionID = entradaGanado.UsuarioModificacionID.HasValue
                                                        ? entradaGanado.UsuarioModificacionID
                                                        : entradaGanado.UsuarioCreacionID;
                        loteBL.ActualizaFechaCierre(entradaGanado.Lote.LoteID, usuarioModificacionID);
                    }
                    else
                    {
                        loteBL.AcutalizaCabezasLote(entradaGanado.Lote);
                    }
                    entradaGanadoDAL.ActualizaEntradaGanado(entradaGanado);
                    transaccion.Complete();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que obtiene entradas de ganado programadas.
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralID"> </param>
        /// <param name="embarqueID"> </param>
        /// <returns></returns>
        internal int ObtenerPorCorralDisponible(int organizacionID, int corralID, int embarqueID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                int result = entradaGanadoDAL.ObtenerPorCorralDisponible(organizacionID, corralID, embarqueID);
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
        /// Obtiene la entrada en base a los datos del lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradaPorLote(LoteInfo lote)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerEntradaPorLote(lote);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtener las entradas de ganado costeadas sin prorratear
        /// </summary>
        /// <param name="tipoOrganizacion"></param>
        /// <returns></returns>
        public List<EntradaGanadoInfo> ObtenerEntradasCosteadasSinProrratear(int tipoOrganizacion)
        {
            List<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerEntradasCosteadasSinProrratear(tipoOrganizacion);
                //entradaGanadoInfo = (List<EntradaGanadoInfo>) AgruparEntradas(entradaGanadoInfo);

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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtener las entradas de ganado en base al corral lote
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        public List<EntradaGanadoInfo> ObtenerEntradasPorCorralLote(LoteInfo lote)
        {
            List<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerEntradasPorCorralLote(lote);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo para agrupar las entradas por lote cuando sean por ruteo
        /// </summary>
        /// <param name="listaEntrada"></param>
        /// <returns></returns>
        private IList<EntradaGanadoInfo> AgruparEntradas(IList<EntradaGanadoInfo> listaEntrada)
        {
            IList<EntradaGanadoInfo> listaDepurada = new List<EntradaGanadoInfo>();
            bool encontrado;
            foreach (var entradaGanadoInfo in listaEntrada)
            {
                encontrado = false;
                foreach (var tmpEntradaGanadoInfo in listaDepurada)
                {
                    if (entradaGanadoInfo.Lote.LoteID == tmpEntradaGanadoInfo.Lote.LoteID)
                    {
                        encontrado = true;

                        tmpEntradaGanadoInfo.FolioEntradaAgrupado = String.Format("{0}{1}{2}", tmpEntradaGanadoInfo.FolioEntradaAgrupado, " | ", entradaGanadoInfo.FolioEntrada);
                        tmpEntradaGanadoInfo.FolioOrigenAgrupado = String.Format("{0}{1}{2}", tmpEntradaGanadoInfo.FolioOrigenAgrupado, " | ", entradaGanadoInfo.FolioOrigen);
                        tmpEntradaGanadoInfo.OrganizacionOrigenIDAgrupado = String.Format("{0}{1}{2}", tmpEntradaGanadoInfo.OrganizacionOrigenIDAgrupado, " | ", entradaGanadoInfo.OrganizacionOrigenID);

                        tmpEntradaGanadoInfo.EsAgrupado = true;
                    }
                }
                if (encontrado) continue;

                entradaGanadoInfo.FolioEntradaAgrupado = entradaGanadoInfo.FolioEntrada.ToString();
                entradaGanadoInfo.FolioOrigenAgrupado = entradaGanadoInfo.FolioOrigen.ToString();
                entradaGanadoInfo.OrganizacionOrigenIDAgrupado = entradaGanadoInfo.OrganizacionOrigenID.ToString();

                listaDepurada.Add(entradaGanadoInfo);
            }

            foreach (var entradaGanadoInfo in listaEntrada)
            {
                var entradaAgrupada = listaDepurada.Where(
                                            o => o.Lote.LoteID == entradaGanadoInfo.Lote.LoteID
                                        ).Select(o => new { entrada = o }).First();

                if (entradaAgrupada == null) continue;

                entradaGanadoInfo.FolioEntradaAgrupado = entradaAgrupada.entrada.FolioEntradaAgrupado;
                entradaGanadoInfo.FolioOrigenAgrupado = entradaAgrupada.entrada.FolioOrigenAgrupado;
                entradaGanadoInfo.OrganizacionOrigenIDAgrupado = entradaAgrupada.entrada.OrganizacionOrigenIDAgrupado;
            }

            return listaEntrada;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorFolioEntradaOrganizacion(int folioEntrada, int organizacionID)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPorFolioEntradaPorOrganizacion(folioEntrada, organizacionID);
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
            return entradaGanadoInfo;
        }

        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaPaginado(PaginacionInfo pagina, EntradaGanadoInfo entradaInfo)
        {
            ResultadoInfo<EntradaGanadoInfo> entradasGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradasGanado = entradaGanadoDAL.ObtenerEntradaPaginado(pagina, entradaInfo);
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
            return entradasGanado;
        }

        internal EntradaGanadoInfo ObtenerEntradasGanadoRecibidas(EntradaGanadoInfo entradaInfo)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerEntradasGanadoRecibidas(entradaInfo);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene una lista de entradas de ganado
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal IList<EntradaGanadoInfo> ObtenerEntradaPorLoteXML(int organizacionId, IList<LoteInfo> lotes)
        {
            IList<EntradaGanadoInfo> entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerEntradasPorLoteXML(organizacionId, lotes);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Actualiza el lote a una entrada de ganado
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="corralInfoDestino"></param>
        /// <param name="usuarioInfo"></param>
        internal EntradaGanadoInfo ActualizarCorral(LoteInfo lote, CorralInfo corralInfoDestino, UsuarioInfo usuarioInfo)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ActualizarCorral(lote, corralInfoDestino, usuarioInfo);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Metodo para obtener una partida de compra directa de una lista de partidas para un lote
        /// </summary>
        /// <param name="entradaSeleccionada"></param>
        /// <returns></returns>
        internal int ObtenerPartidaCompraDirecta(EntradaGanadoInfo entradaSeleccionada)
        {
            int entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPartidaCompraDirecta(entradaSeleccionada);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene entradas de ganado por EntradaGanadoID
        /// </summary>
        /// <param name="entradas"></param>
        /// <returns></returns>
        internal List<EntradaGanadoInfo> ObtenerEntradasPorIDs(List<int> entradas)
        {
            List<EntradaGanadoInfo> entradasGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradasGanado = entradaGanadoDAL.ObtenerEntradasPorIDs(entradas);
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
            return entradasGanado;
        }

        internal List<EntradaGanadoInfo> ObtenerEntradasPorEmbarqueID(int embarqueID)
        {
            List<EntradaGanadoInfo> entradasGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradasGanado = entradaGanadoDAL.ObtenerEntradasPorEmbarqueID(embarqueID);
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
            return entradasGanado;
        }

        /// <summary>
        /// Obtiene entradas de ganado por folio de origen
        /// </summary>
        /// <returns></returns>
        internal List<EntradaGanadoInfo> ObtenerEntradasPorFolioOrigenXML(List<EntradaGanadoInfo> foliosOrigen)
        {
            List<EntradaGanadoInfo> entradasGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradasGanado = entradaGanadoDAL.ObtenerEntradasPorFolioOrigenXML(foliosOrigen);
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
            return entradasGanado;
        }

        /// <summary>
        /// Obtiene las entradas para su impresion de tarjeta de recepcion
        /// </summary>
        /// <param name="filtro">filtros para obtener las entradas a imprimir</param>
        /// <returns></returns>
        internal List<ImpresionTarjetaRecepcionModel> ObtenerEntradasImpresionTarjetaRecepcion(FiltroImpresionTarjetaRecepcion filtro)
        {
            List<ImpresionTarjetaRecepcionModel> entradasGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradasGanado = entradaGanadoDAL.ObtenerEntradasImpresionTarjetaRecepcion(filtro);
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
            return entradasGanado;
        }

        /// <summary>
        /// Obtiene las entradas para su impresion de tarjeta de recepcion
        /// </summary>
        /// <param name="filtro">filtros para obtener las entradas a imprimir</param>
        /// <returns></returns>
        internal List<ImpresionCalidadGanadoModel> ObtenerEntradasImpresionCalidadGanado(FiltroImpresionCalidadGanado filtro)
        {
            List<ImpresionCalidadGanadoModel> entradasGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradasGanado = entradaGanadoDAL.ObtenerEntradasImpresionCalidadGanado(filtro);

                foreach (var impresionCalidadGanadoModel in entradasGanado)
                {
                    var propiedades = impresionCalidadGanadoModel.GetType().GetProperties();
                    foreach (var propInfo in propiedades)
                    {
                        dynamic customAttributes =
                            impresionCalidadGanadoModel.GetType().GetProperty(propInfo.Name).GetCustomAttributes(
                                typeof (AtributoImpresionCalidad), true);
                        if (customAttributes.Length > 0)
                        {
                            for (var indexAtributos = 0; indexAtributos < customAttributes.Length; indexAtributos++)
                            {
                                var atributos = (AtributoImpresionCalidad) customAttributes[indexAtributos];
                                var calidaGanadoID = atributos.CalidadGanadoID;

                                EntradaGanadoCalidadInfo calidadGanado = impresionCalidadGanadoModel.ListaCalidadGanado.
                                    FirstOrDefault(
                                        cali => cali.CalidadGanado.CalidadGanadoID == calidaGanadoID);

                                if (calidadGanado != null)
                                {
                                    propInfo.SetValue(impresionCalidadGanadoModel, calidadGanado.Valor, null);
                                }
                            }
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
            return entradasGanado;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPorFolioEntradaCortadaIncompleta(EntradaGanadoInfo filtro)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerPorFolioEntradaCortadaIncompleta(filtro);
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
            return entradaGanadoInfo;
        }

        /// <summary>
        /// Obtiene las Entradas de Ganado por Pagina
        /// </summary>
        /// <param name="pagina">Indica la manera en que se realizara el Paginado</param>
        /// <param name="entradaGanadoInfo">Folio Por el Cual se Filtrara</param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoInfo> ObtenerEntradaGanadoPaginaCortadasIncompletas(PaginacionInfo pagina, EntradaGanadoInfo entradaGanadoInfo)
        {
            ResultadoInfo<EntradaGanadoInfo> listaEntradaGanado;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                listaEntradaGanado = entradaGanadoDAL.ObtenerEntradaGanadoPaginaCortadasIncompletas(pagina, entradaGanadoInfo);
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
            return listaEntradaGanado;
        }
        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<FiltroAnimalesReemplazoArete> ObtenerReemplazoAretes(EntradaGanadoInfo filtro)
        {
            List<FiltroAnimalesReemplazoArete> listaAretes;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                listaAretes = entradaGanadoDAL.ObtenerReemplazoAretes(filtro);
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
            return listaAretes;
        }

        /// <summary>
        /// Metodo que obtiene una entrada de ganado Folio de entrada y Organizacion
        /// </summary>
        /// <param name="listaAretes"></param>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal int GuardarReemplazoAretes(List<FiltroAnimalesReemplazoArete> listaAretes, EntradaGanadoInfo entradaGanado){
            
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                return entradaGanadoDAL.GuardarReemplazoAretes(listaAretes, entradaGanado);
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
        ///  Obtiene la entrada en base al Embarque
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <returns></returns>
        internal List<CabezasPartidasModel> ObtenerCabezasEntradasRuteo(int embarqueID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                return entradaGanadoDAL.ObtenerCabezasEntradasRuteo(embarqueID);
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
        /// Obtener entrada ganado por loteID y corralID
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerEntradaGanadoLoteCorral(LoteInfo lote)
        {
            EntradaGanadoInfo entradaGanadoInfo;
            try
            {
                Logger.Info();
                var entradaGanadoDAL = new EntradaGanadoDAL();
                entradaGanadoInfo = entradaGanadoDAL.ObtenerEntradaGanadoLoteCorral(lote);
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
            return entradaGanadoInfo;
        }
    }
}
