using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class InterfaceSalidaCostoBL
    {
        /// <summary>
        /// Obtener olos animales y sus costos en la interface salida
        /// </summary>
        /// <param name="folioOrigen"></param>
        /// <param name="organizacionOrigenID"></param>
        /// <returns></returns>
        internal IList<InterfaceSalidaCostoInfo> ObtenerCostoAnimales(int folioOrigen, int organizacionOrigenID)
        {

            IList<InterfaceSalidaCostoInfo> resultado;
            try
            {
                Logger.Info();
                var interfaceSalidaCostoDAL = new InterfaceSalidaCostoDAL();
                resultado = interfaceSalidaCostoDAL.ObtenerCostoAnimales(folioOrigen, organizacionOrigenID);
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
