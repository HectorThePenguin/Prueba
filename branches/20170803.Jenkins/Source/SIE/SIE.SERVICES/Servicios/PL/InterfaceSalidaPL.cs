using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class InterfaceSalidaPL
    {
        /// <summary>
        ///     Metodo que crear una interfaceSalida
        /// </summary>
        /// <param name="interfaceSalidaInfo"></param>
        public void GuardarInterfaceSalida(InterfaceSalidaInfo interfaceSalidaInfo)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaBL = new InterfaceSalidaBL();
                interfaceSalidaBL.GuardarInterfaceSalida(interfaceSalidaInfo);
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
        ///     Obtiene una Interface de Salida por Id
        /// </summary>
        /// <param name="salidaID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public InterfaceSalidaInfo ObtenerPorID(int salidaID, int organizacionID)
        {
            InterfaceSalidaInfo interfaceSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaBL = new InterfaceSalidaBL();
                interfaceSalidaInfo = interfaceSalidaBL.ObtenerPorID(salidaID, organizacionID);
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
            return interfaceSalidaInfo;
        }

        /// <summary>
        ///     Obtiene una Lista de Interface de Salida
        /// </summary>
        /// <returns></returns>
        public IList<InterfaceSalidaInfo> ObtenerTodos()
        {
            IList<InterfaceSalidaInfo> interfacesSalidaInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaBL = new InterfaceSalidaBL();
                interfacesSalidaInfo = interfaceSalidaBL.ObtenerTodos();
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
            return interfacesSalidaInfo;
        }

        /// <summary>
        ///     Obtiene una Interface de Salida por SalidaID y OrganizacionID
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        public EntradaGanadoCosteoInfo ObtenerPorSalidaOrganizacion(EntradaGanadoInfo entradaGanado)
        {
            EntradaGanadoCosteoInfo entradaGanadoCosteoInfo;
            try
            {
                Logger.Info();
                var interfaceSalidaBL = new InterfaceSalidaBL();
                entradaGanadoCosteoInfo = interfaceSalidaBL.ObtenerPorSalidaOrganizacion(entradaGanado);
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
            return entradaGanadoCosteoInfo;
        }

        public Dictionary<int, decimal> ObtenerCostoFleteProrrateado(EntradaGanadoInfo entradaGanado, List<EntradaGanadoCostoInfo> listaCostosEmbarque, List<EntradaGanadoInfo> listaCompraDirecta)
        {
            Dictionary<int, decimal> importesProrrateados;
            try
            {
                Logger.Info();
                var interfaceSalidaBL = new InterfaceSalidaBL();
                importesProrrateados = interfaceSalidaBL.ObtenerCostoFleteProrrateado(entradaGanado, listaCostosEmbarque, listaCompraDirecta);
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
            return importesProrrateados;
        }

        /// <summary>
        ///     Obtiene una Interface de Salida por Id
        /// </summary>
        /// <param name="salidaID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="organizacioOrigenID"></param>
        /// <returns></returns>
        public int ObtenerPorEmbarque(int salidaID, int organizacionID, int organizacioOrigenID)
        {
            int resultado;
            try
            {
                Logger.Info();
                var interfaceSalidaBL = new InterfaceSalidaBL();
                resultado = interfaceSalidaBL.ObtenerPorEmbarque(salidaID, organizacionID, organizacioOrigenID);
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
