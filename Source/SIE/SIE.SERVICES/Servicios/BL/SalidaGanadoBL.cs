using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class SalidaGanadoBL
    {
        /// <summary>
        /// Metodo para Guardar la Salida Ganado
        /// </summary>
        /// <param name="animalInactivo"></param>
        internal bool GuardarSalidaGanado(AnimalInfo animalInactivo)
        {
            var envioAnimal = false;
            try
            {
                Logger.Info();
                var animalBL = new AnimalBL();

                //Se obtiene el ultimo movimiento
                var ultimoMovimiento = animalBL.ObtenerUltimoMovimientoAnimal(animalInactivo);

                var salidaGanadoInfo = new SalidaGanadoInfo
                {
                    Organizacion = new OrganizacionInfo
                    {
                        OrganizacionID = animalInactivo.OrganizacionIDEntrada
                    },
                    TipoMovimiento = (TipoMovimiento)ultimoMovimiento.TipoMovimientoID,
                    Activo = EstatusEnum.Activo,
                    //VentaGanado = new VentaGanadoInfo(){VentaGanadoID = null},
                    UsuarioCreacionID = animalInactivo.UsuarioCreacionID
                };

                if (ultimoMovimiento.TipoMovimientoID == (int) TipoMovimiento.SalidaPorVenta)
                {
                    //Si el ultimo movimiento es venta obtener el ID de la VentaGanado
                    var ventaDetalleBL = new VentaGanadoDetalleBL();
                    VentaGanadoDetalleInfo ventaDetalleInfo =
                        ventaDetalleBL.ExisteAnimal(animalInactivo);

                    if (ventaDetalleInfo != null)
                    {
                        salidaGanadoInfo.VentaGanado = new VentaGanadoInfo
                        {
                            VentaGanadoID = ventaDetalleInfo.VentaGanadoID
                        };
                    }

                }
            
                var salidaGanadoDAL = new SalidaGanadoDAL();
                SalidaGanadoInfo salidaGanado = null;

                //Se valida si la venta es
                if (salidaGanadoInfo.VentaGanado != null)
                {
                    //Se busca la venta en la salida Ganado
                    salidaGanado = salidaGanadoDAL.ExisteVentaEnSalidaGanado(salidaGanadoInfo);
                }

                if (salidaGanado == null)
                {
                    //Se guarda en SalidaGanado
                    salidaGanado = 
                        salidaGanadoDAL.GuardarSalidaGanado(salidaGanadoInfo, (int)TipoFolio.SalidaGanado);
                }
                
                //Se almacena SalidaAnimal
                var salidaAnimalBL = new SalidaAnimalBL();

                var salidaAnimalInfo = new SalidaAnimalInfo()
                {
                    SalidaGanado = salidaGanado,
                    Animal = animalInactivo,
                    Lote = new LoteInfo{ LoteID = ultimoMovimiento.LoteID},
                    Activo = EstatusEnum.Activo,
                    UsuarioCreacionID = animalInactivo.UsuarioCreacionID

                };

                envioAnimal = salidaAnimalBL.Guardar(salidaAnimalInfo);

                envioAnimal = true;
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
            return envioAnimal;
        }
    }
}
