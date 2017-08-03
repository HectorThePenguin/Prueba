using System;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas;
using SIE.Services.Polizas.Fabrica;
using System.Linq;

namespace SIE.Services.Servicios.BL
{
    /// <summary>
    /// Clase de negocio para gastos al inventario
    /// </summary>
    internal class GastoInventarioBL
    {
        /// <summary>
        /// Guarda el gasto de inventario a la base de datos
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal long Guardar(GastoInventarioInfo info)
        {
            long folioGasto;
            try
            {
                PolizaAbstract poliza;
                IList<PolizaInfo> listaPolizas;
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    info.TipoFolio = TipoFolio.GastoInventario;
                    var dal = new GastoInventarioDAL();
                    int gastoInventarioID = dal.Guardar(info);

                    GastoInventarioInfo gastoInventario = dal.ObtenerPorID(gastoInventarioID);
                    if (gastoInventario == null)
                    {
                        return 0;
                    }
                    gastoInventario.CuentaGasto = info.CuentaGasto;
                    gastoInventario.CentroCosto = info.CentroCosto;
                    gastoInventario.Corral = info.Corral;
                    gastoInventario.TotalCorrales = info.TotalCorrales;
                    poliza = FabricaPoliza.ObtenerInstancia().ObtenerTipoPoliza(TipoPoliza.GastosInventario);
                    if (gastoInventario.Importe < 0)
                    {
                        poliza.Cancelacion = true;
                        gastoInventario.Importe = Math.Abs(gastoInventario.Importe);
                    }
                    listaPolizas = poliza.GeneraPoliza(gastoInventario);

                    var polizaDAL = new PolizaDAL();
                    listaPolizas.ToList().ForEach(datos =>
                    {
                        datos.OrganizacionID = info.Organizacion.OrganizacionID;
                        datos.UsuarioCreacionID = info.UsuarioId;
                        datos.Activo = EstatusEnum.Activo;
                        datos.ArchivoEnviadoServidor = 1;
                    });
                    polizaDAL.CrearServicioPI(listaPolizas, TipoPoliza.GastosInventario);
                    transaction.Complete();
                    folioGasto = gastoInventario.FolioGasto;
                }
            }
            catch (ExcepcionServicio)
            {
                throw;
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
            return folioGasto;
        }

        /// <summary>
        /// Obtiene una coleccion de gastos por inventario
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal IEnumerable<GastoInventarioInfo> ObtenerGastosInventarioPorFechaConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            IEnumerable<GastoInventarioInfo> gastosInventario;
            try
            {
                Logger.Info();
                var dal = new GastoInventarioDAL();
                gastosInventario = dal.ObtenerGastosInventarioPorFechaConciliacion(organizacionID, fechaInicial,
                                                                                   fechaFinal);
            }
            catch (ExcepcionServicio)
            {
                throw;
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
            return gastosInventario;
        }
    }
}
