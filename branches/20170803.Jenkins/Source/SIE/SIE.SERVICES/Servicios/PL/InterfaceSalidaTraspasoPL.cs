using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.Services.Servicios.PL
{
    public class InterfaceSalidaTraspasoPL
    {
        /// <summary>
        ///  Guarda una interface salida traspaso y regresa el item insertado.
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        public InterfaceSalidaTraspasoInfo Crear(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            try
            {
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoBL();
                return interfaceSalidaTraspaspBl.Crear(interfaceSalidaTraspaso);
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
        ///  Guarda una lista de interface salida traspaso.
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        public Dictionary<long, decimal> GuardarListadoInterfaceSalidaTraspaso(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            try
            {
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoBL();
                return interfaceSalidaTraspaspBl.CrearLista(interfaceSalidaTraspaso);
            }
            catch (ExcepcionServicio ex)
            {
                throw ex;
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
        /// Obtiene el listado de organizaciones que se han registrado del dia
        /// </summary>
        /// <returns></returns>
        public List<InterfaceSalidaTraspasoInfo> ObtenerListaInterfaceSalidaTraspaso()
        {
            try
            {
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoBL();
                return interfaceSalidaTraspaspBl.ObtenerListaInterfaceSalidaTraspaso();
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
        ///  Consulta la interface de salida 
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        public InterfaceSalidaTraspasoInfo ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            try
            {
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoBL();
                return interfaceSalidaTraspaspBl.ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(interfaceSalidaTraspaso);
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
        /// Obtiene 
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="folioOrigen"> </param>
        /// <returns></returns>
        public EntradaGanadoCosteoInfo ObtenerDatosInterfaceSalidaTraspaso(int organizacionID, int folioOrigen)
        {
            try
            {
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoBL();
                return interfaceSalidaTraspaspBl.ObtenerDatosInterfaceSalidaTraspaso(organizacionID, folioOrigen);
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
        ///  Consulta la interface de salida 
        /// </summary>
        /// <param name="loteInfo"></param>
        public InterfaceSalidaTraspasoInfo ObtenerInterfaceSalidaTraspasoPorLote(LoteInfo loteInfo)
        {
            try
            {
                var interfaceSalidaTraspaspBl = new InterfaceSalidaTraspasoBL();
                return interfaceSalidaTraspaspBl.ObtenerInterfaceSalidaTraspasoPorLote(loteInfo);
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
        /// Obtiene datos de Interface Salida Traspaso
        /// por Lotes
        /// </summary>
        /// <param name="lotes"></param>
        /// <param name="fecha"> </param>
        /// <param name="foliosTraspaso"> </param>
        /// <returns></returns>
        public List<InterfaceSalidaTraspasoInfo> ObtenerInterfaceSalidaTraspasoLotes(List<LoteInfo> lotes, DateTime fecha, List<PolizaSacrificioModel> foliosTraspaso)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspasoBL = new InterfaceSalidaTraspasoBL();
                return interfaceSalidaTraspasoBL.ObtenerInterfaceSalidaTraspasoLotes(lotes, fecha, foliosTraspaso);
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
        /// Obtiene una coleccion con los datos que han
        /// sido traspasados por medio de la interface
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public List<InterfaceSalidaTraspasoInfo> ObtenerTraspasosPorFechaConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspasoBL = new InterfaceSalidaTraspasoBL();
                return interfaceSalidaTraspasoBL.ObtenerTraspasosPorFechaConciliacion(organizacionID, fechaInicial,
                                                                                      fechaFinal);
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
        /// Obtiene datos de Interface Salida Traspaso
        /// por Lotes
        /// </summary>
        /// <param name="folioTraspaso"></param>
        public List<InterfaceSalidaTraspasoInfo> ObtenerInterfaceSalidaTraspaso(PolizaSacrificioModel folioTraspaso)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspasoBL = new InterfaceSalidaTraspasoBL();
                List<InterfaceSalidaTraspasoInfo> resultado = interfaceSalidaTraspasoBL
                                                                .ObtenerInterfaceSalidaTraspaso(folioTraspaso);
                return resultado;
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
