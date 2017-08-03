using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using System.IO;

namespace SIE.Services.Servicios.PL
{
    public class AjusteDeInventarioPL
    {
        /// <summary>
        /// Obtiene el Status de AlmacenMovimiento
        /// </summary>
        /// <returns> </returns>
        public EstatusInfo ObtenerEstatusInfo(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            EstatusInfo info;
            try
            {
                Logger.Info();
                var ajusteDeInventarioBL = new AjusteDeInventarioBL();
                info = ajusteDeInventarioBL.ObtenerEstatusInfo(almacenMovimientoInfo);
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

        /// <summary>
        /// Obtiene los productos que presentan diferencia entre inventario teorico e inventario fisico
        /// </summary>
        /// <returns> </returns>
        public List<AjusteDeInventarioDiferenciasInventarioInfo> ObtenerDiferenciaInventarioFisicoTeorico(AlmacenMovimientoInfo almacenMovimientoInfo, int organizacionID)
        {
            List<AjusteDeInventarioDiferenciasInventarioInfo> info;
            try
            {
                Logger.Info();
                var ajusteDeInventarioBL = new AjusteDeInventarioBL();
                info = ajusteDeInventarioBL.ObtenerDiferenciaInventarioFisicoTeorico(almacenMovimientoInfo, organizacionID);
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

        /// <summary>
        /// Metodo para actualizar los ajustes de inventario contenidos en el grid
        /// </summary>
        /// <param name="articulos"></param>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        public IList<ResultadoPolizaModel> GuardarAjusteDeInventario(List<AjusteDeInventarioDiferenciasInventarioInfo> articulos, AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var ajusteDeInventarioBL = new AjusteDeInventarioBL();
                return ajusteDeInventarioBL.GuardarAjusteDeInventario(articulos, almacenMovimientoInfo);
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
        }
    }
}
