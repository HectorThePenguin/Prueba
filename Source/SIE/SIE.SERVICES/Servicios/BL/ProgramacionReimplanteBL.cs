using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Info.Enums;

namespace SIE.Services.Servicios.BL
{
    internal class ProgramacionReimplanteBL
    {
        /// <summary>
        /// Obtiene la lista de tratamientos para los los corrales disponibles para programar reimplante
        /// </summary>
        /// <param name="organizacionId">Organizacionid</param>
        /// <returns></returns>
        internal ResultadoInfo<OrdenReimplanteInfo> ObtenerLotesDisponiblesReimplante(int organizacionId)
        {
            ResultadoInfo<OrdenReimplanteInfo> lista = null;
            try
            {
                Logger.Info();
                var dal = new ProgramacionReimplanteDAL();
                lista = dal.ObtenerLotesDisponiblesReimplante(organizacionId);
                var tratamientoBl = new TratamientoBL();
                var dalCorrales = new CorralDAL();

                if(lista.Lista != null)
                {
                    foreach (var orden in lista.Lista)
                    {
                        orden.CorralOrigen = dalCorrales.ObtenerPorId(orden.IdCorralOrigen);

                        var trat = new TratamientoInfo
                        {
                            OrganizacionId = organizacionId,
                            Sexo = orden.TipoGanado.Sexo,
                            Peso = orden.KilosProyectados
                        };

                        //obtenemos los tratamientos
                        var tratamientos = tratamientoBl.ObtenerTratamientosPorTipoReimplante(trat);
                        //de los tratamientos recuperamos sus productos en igual de buscarlos en la bd
                        var subFamilia = (int)SubFamiliasEnum.ImplantesYReimplantes;//int.Parse(ConfigurationManager.AppSettings["subFamiliaProductosReimplante"]);
                        var listaAuxiliarProductos = ( from tratamiento in tratamientos
                                                         from producto in tratamiento.Productos
                                                       where producto.SubfamiliaId == subFamilia
                                                       select producto
                                                       ).ToList();
                        orden.Productos = new List<ProductoInfo>();
                        foreach (ProductoInfo producto in listaAuxiliarProductos)
                        {
                            if(!orden.Productos.Contains(producto)){
                                orden.Productos.Add(producto);
                            }
                        }
                        /* Se quito para no ir a buscar de nuevo a la bd
                        if (tratamientos != null)
                        {
                            var listaAuxiliarProductos = tratamientoBl.ObtenerProductosPorTratamiento(tratamientos);
                            var subFamilia = int.Parse(ConfigurationManager.AppSettings["subFamiliaProductosReimplante"]);
                            orden.Productos = listaAuxiliarProductos.Where(prod => prod.SubfamiliaId == subFamilia).ToList();
                        }
                        /*
                        IList<CorralInfo> corralesDestino = dalCorrales.ObtenerParaProgramacionReimplanteDestino(organizacionId);

                        orden.CorralesDestino = new List<CorralInfo> {orden.CorralOrigen};
                        foreach (var corralInfo in corralesDestino)
                        {
                            orden.CorralesDestino.Add(corralInfo);    
                        }
                        */
                        //REgla del siguiente dia
                        var reimplante = new DateTime(orden.FechaReimplante.Year, orden.FechaReimplante.Month, orden.FechaReimplante.Day);
                        var fechax = DateTime.Now.AddDays(1d);
                        var siguiente = new DateTime(fechax.Year, fechax.Month, fechax.Day);
                        if(reimplante.CompareTo(siguiente) == 0) //son la misma fecha
                        {
                            orden.Seleccionado = true;
                            orden.EsEditable = false;

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

            return lista;
        }

        /// <summary>
        /// Guarda la lista definicida para ordenes de reimplante
        /// </summary>
        /// <param name="orden">Lista de ordenes</param>
        /// <returns></returns>
        internal bool GuardarProgramacionReimplante(IList<OrdenReimplanteInfo> orden)
        {
            var retValue = false;
            try
            {
                Logger.Info();
                var dal = new ProgramacionReimplanteDAL();
                // Se inicia la transaccion por Entrada validada
                using (var transaction = new TransactionScope())
                {
                    foreach (var item in orden.Where(item => item.Seleccionado))
                    {
                        dal.GuardarOrdenReimplante(item);
                        retValue = true;
                    }
                    transaction.Complete();
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

            return retValue;
        }

        internal bool EliminarProgramacionReimplante(int folioProgReimplante)
        {
            try
            {
                Logger.Info();
                
                var programacionReimplanteDAL = new ProgramacionReimplanteDAL();
                var respuesta = programacionReimplanteDAL.EliminarProgramacionReimplante(folioProgReimplante);
                return respuesta;
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

        internal bool GuardarFechaReal(String fechaReal, LoteReimplanteInfo loteReimplante)
        {
            try
            {
                Logger.Info();

                var programacionReimplanteDAL = new ProgramacionReimplanteDAL();
                var regresa = programacionReimplanteDAL.GuardarFechaReal(fechaReal, loteReimplante);
                return regresa;
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

        internal bool ExisteProgramacionReimplante(int organizacionId, DateTime selectedDate)
        {
            try
            {
                Logger.Info();

                var programacionReimplanteDAL = new ProgramacionReimplanteDAL();
                var respuesta = programacionReimplanteDAL.ExisteProgramacionReimplante(organizacionId, selectedDate);
                return respuesta;
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

        internal List<ProgramacionReinplanteInfo> ObtenerProgramacionReimplantePorLoteID(LoteInfo lote)
        {
            List<ProgramacionReinplanteInfo> lista = null;
            try
            {
                Logger.Info();
                var dal = new ProgramacionReimplanteDAL();
                lista = dal.ObtenerProgramacionReimplantePorLoteID(lote);
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
        /// Validar que el reimplante sea de corral a corral
        /// </summary>
        /// <param name="loteIDOrigen"></param>
        /// <param name="corralIDDestino"></param>
        /// <returns></returns>
        internal bool ValidarReimplanteCorralACorral(int loteIDOrigen, int corralIDDestino)
        {
            try
            {
                Logger.Info();

                var programacionReimplanteDAL = new ProgramacionReimplanteDAL();
                var respuesta = programacionReimplanteDAL.ValidarReimplanteCorralACorral(loteIDOrigen, corralIDDestino);
                return respuesta;
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
        /// Elimina la programacion de reimplante
        /// </summary>
        /// <param name="corralesProgramados"></param>
        internal void EliminarProgramacionReimplanteXML(List<ProgramacionReinplanteInfo> corralesProgramados)
        {
            try
            {
                Logger.Info();
                var programacionReimplanteDAL = new ProgramacionReimplanteDAL();
                programacionReimplanteDAL.EliminarProgramacionReimplanteXML(corralesProgramados);
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
        /// Cierra las programaciones de Reimplante pendientes
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="usuarioID"></param>
        internal void CerrarProgramacionReimplante(int organizacionID, int usuarioID)
        {
            try
            {
                Logger.Info();
                var programacionReimplanteDAL = new ProgramacionReimplanteDAL();
                programacionReimplanteDAL.CerrarProgramacionReimplante(organizacionID, usuarioID);
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
