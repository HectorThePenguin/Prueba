using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class EntradaGanadoMuerteBL
    {
        /// <summary>
        /// Obtiene una entidad EntradaGanadoTransito por su Id
        /// </summary>
        /// <param name="entradaGanadoID">Obtiene una entidad EntradaGanadoID por su Id</param>
        /// <returns></returns>
        public List<EntradaGanadoMuerteInfo> ObtenerMuertesEnTransitoPorEntradaGanadoID(int entradaGanadoID)
        {
            try
            {
                Logger.Info();
                var entradaGanadoMuerteDAL = new EntradaGanadoMuerteDAL();
                List<EntradaGanadoMuerteInfo> entradaGanadoMuerteInfo =
                    entradaGanadoMuerteDAL.ObtenerMuertesEnTransitoPorEntradaGanadoID(entradaGanadoID);
                return entradaGanadoMuerteInfo;
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
        /// Actualiza datos de entrada ganado muerte
        /// </summary>
        /// <param name="list"></param>
        internal void Actualizar(List<EntradaGanadoMuerteInfo> entradasGanadoMuertes)
        {
            try
            {
                Logger.Info();
                var entradaGanadoMuerteDAL = new EntradaGanadoMuerteDAL();
                entradaGanadoMuerteDAL.Actualizar(entradasGanadoMuertes);
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
