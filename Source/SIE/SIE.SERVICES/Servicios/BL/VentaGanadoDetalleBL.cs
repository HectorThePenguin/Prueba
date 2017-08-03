using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class VentaGanadoDetalleBL
    {
        internal List<VentaGanadoDetalleInfo> ObtenerVentaGanadoPorTicket(int ventaGanadoID)
        {
            try
            {
                Logger.Info();
                var venta = new VentaGanadoDetalleDAL();
                List<VentaGanadoDetalleInfo> result = venta.ObtenerVentaGanadoPorTicket(ventaGanadoID);
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


        internal int GuardarDetalle(GrupoVentaGanadoInfo venta)
        {
            var animalBL = new AnimalBL();
            var corralBl = new CorralBL();
            var loteBl = new LoteBL();
            try
            {
                Logger.Info();
                int retorno;
                using (var transaccion = new TransactionScope())
                {
                    var ventaGanadoDetalleDAL = new VentaGanadoDetalleDAL();
                    
                    //Obtener Corral y Lote
                    var corral = corralBl.ObtenerCorralPorCodigo(venta.OrganizacionId, venta.CodigoCorral);
                    var lote = loteBl.ObtenerPorCorralCerrado(venta.OrganizacionId, corral.CorralID);

                    if (venta.TipoVenta == Info.Enums.TipoVentaEnum.Propio)
                    {
                        //Validar si tenemos animales que pertenezcan a la carga inicial
                        var listaCargaInicial = venta.VentaGandadoDetalle.Where(animal => animal.Animal.CargaInicial).ToList();
                        if (listaCargaInicial != null && listaCargaInicial.Any())
                        {
                            
                            foreach (var animal in listaCargaInicial)
                            {
                                var animalInventario = animalBL.ObtenerAnimalPorArete(animal.Arete, venta.OrganizacionId);
                                var deteccionGrabar = new DeteccionInfo
                                {
                                    CorralID = corral.CorralID,
                                    LoteID = lote.LoteID,
                                    UsuarioCreacionID = venta.VentaGanado.UsuarioModificacionID
                                };

                                // Se intercambian aretes por encontrarse el animal en un corral distinto y ser carga inicial
                                animalBL.ReemplazarAretes(animalInventario, deteccionGrabar);
                            }
                        }
                    }
                    
                    retorno = ventaGanadoDetalleDAL.GuardarDetalle(venta);
                    
                    // Se cierral la transaccion
                    transaccion.Complete();
                }
                return retorno;
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
        /// Validar si existe el animal como vendido.
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal VentaGanadoDetalleInfo ExisteAnimal(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var ventaGanadoDetalleDAL = new VentaGanadoDetalleDAL();
                VentaGanadoDetalleInfo retorno = ventaGanadoDetalleDAL.ExisteAnimal(animal);
                return retorno;
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
