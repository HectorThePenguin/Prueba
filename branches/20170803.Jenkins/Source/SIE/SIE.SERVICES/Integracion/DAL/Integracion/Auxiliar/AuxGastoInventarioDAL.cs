using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    /// <summary>
    /// Clase que representa metodos auxiliares para el gasto al inventario DAL
    /// </summary>
    internal class AuxGastoInventarioDAL
    {
        public static Dictionary<string, object> ObtenerParametrosGuardarGastoInventario(GastoInventarioInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@OrganizacionId", info.Organizacion.OrganizacionID},
                            {"@TipoGasto", info.TipoGasto.ToString()},
                            {"@FechaGasto", info.FechaGasto},
                            {"@CostoId", info.Costo.CostoID},
                            {"@TieneCuenta", info.TieneCuenta},
                            {"@CuentaSAPId", info.CuentaSAP !=null ? info.CuentaSAP.CuentaSAPID: 0},
                            {"@ProveedorId", info.Proveedor != null ? info.Proveedor.ProveedorID : 0},
                            {"@Importe", info.Importe},
                            {"@Observaciones", info.Observaciones},
                            {"@Factura", info.Factura},
                            {"@IVA", info.IVA},
                            {"@Retencion", info.Retencion},
                            {"@TipoFolio", info.TipoFolio.GetHashCode()},
                            {"@CorralId", info.Corral != null ? info.Corral.CorralID : 0},
                            {"@Activo", info.Activo == EstatusEnum.Activo ? 1 : 0},
                            {"@UsuarioId", info.UsuarioId},
                            {"@TotalCorrales", info.TotalCorrales},
                            {"@CentroCosto", info.CentroCosto},
                            {"@CuentaGasto", info.CuentaGasto},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosObtenerPorID(int gastoInventarioID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
							{"@GastoInventarioID", gastoInventarioID},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
