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
    internal class EntradaPremezclaBL
    {
        /// <summary>
        /// Metodo para almacenar los movimientos que genera la premezcla
        /// </summary>
        /// <param name="entradaPremezclaInfo"></param>
        internal int Guardar(EntradaPremezclaInfo entradaPremezclaInfo)
        {
            try
            {
                Logger.Info();
                var entradaPremezclaDAL = new EntradaPremezclaDAL();
                return entradaPremezclaDAL.Guardar(entradaPremezclaInfo);
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
        /// Metodo para obtener la lista de movimientos de la entrada
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal List<EntradaPremezclaInfo> ObtenerPorMovimientoEntrada(AlmacenMovimientoInfo almacenMovimiento)
        {
            try
            {
                Logger.Info();
                var entradaPremezclaDAL = new EntradaPremezclaDAL();
                return entradaPremezclaDAL.ObtenerPorMovimientoEntrada(almacenMovimiento);
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
