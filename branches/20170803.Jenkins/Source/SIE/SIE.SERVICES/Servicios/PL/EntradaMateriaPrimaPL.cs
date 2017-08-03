
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class EntradaMateriaPrimaPL
    {
        /// <summary>
        /// Obtiene los costos de los fletes por contrato
        /// </summary>
        /// <param name="contenedorMateriaPrima"></param>
        /// <param name="pesosCostos">Kilos de la entrada</param>
        /// <returns></returns>
        public List<CostoEntradaMateriaPrimaInfo> ObtenerCostosFletes(ContenedorEntradaMateriaPrimaInfo contenedorMateriaPrima, CostoEntradaMateriaPrimaInfo pesosCostos)
        {
            List<CostoEntradaMateriaPrimaInfo> resultado;
            try
            {
                Logger.Info();
                var EntradaMateriaPrimaBl = new EntradaMateriaPrimaBL();
                resultado = EntradaMateriaPrimaBl.ObtenerCostosFletes(contenedorMateriaPrima, pesosCostos);
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
            return resultado;
        }

        /// <summary>
        /// Guarda los costos de la entrada de materia prima
        /// </summary>
        /// <param name="entradaMateriaPrima">Contiene todos los datos necesarios para guardar los costos</param>
        /// <returns></returns>
        public MemoryStream GuardarEntradaMateriaPrima(ContenedorEntradaMateriaPrimaInfo entradaMateriaPrima)
        {
            MemoryStream resultado;
            try
            {
                Logger.Info();
                var EntradaMateriaPrimaBl = new EntradaMateriaPrimaBL();
                resultado = EntradaMateriaPrimaBl.GuardarEntradaMateriaPrima(entradaMateriaPrima);
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
            return resultado;
        }
        /// <summary>
        /// Obtener precio origen
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        public decimal ObtenerPrecioOrigen(ContratoInfo contrato)
        {
            decimal resultado;
            try
            {
                Logger.Info();
                var EntradaMateriaPrimaBl = new EntradaMateriaPrimaBL();
                resultado = EntradaMateriaPrimaBl.ObtenerPrecioOrigen(contrato);
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
            return resultado;
        }


        /// <summary>
        /// Obtiene la merma permitida
        /// </summary>
        /// <param name="contenedorMateriaPrima"></param>
        /// <returns></returns>
        public CostoEntradaMateriaPrimaInfo ObtenerMermaPermitida(EntradaProductoInfo entradaProducto)
        {
            CostoEntradaMateriaPrimaInfo resultado;
            try
            {
                Logger.Info();
                var EntradaMateriaPrimaBl = new EntradaMateriaPrimaBL();
                resultado = EntradaMateriaPrimaBl.ObtenerMermaPermitida(entradaProducto);
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
            return resultado;
        }
    }
}
