using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class InterfaceSalidaTraspasoDAL:DALBase
    {
        /// <summary>
        ///  Guarda una interface salida traspaso y regresa el item insertado.
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <returns></returns>
        internal InterfaceSalidaTraspasoInfo Crear(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            InterfaceSalidaTraspasoInfo result = null;
            try
            {
                Logger.Info();
                var parametros = AuxInterfaceSalidaTraspasoDAL.ObtenerParametrosCrear(interfaceSalidaTraspaso);
                DataSet ds = Retrieve("InterfaceSalidaTraspaso_Crear", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaTraspasoDAL.ObtenerPorId(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        ///  Guarda una interface salida traspaso y regresa el item insertado.
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <returns></returns>
        internal void Actualizar(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            try
            {
                Logger.Info();
                var parametros = AuxInterfaceSalidaTraspasoDAL.ObtenerParametrosActualizar(interfaceSalidaTraspaso);
                Update("InterfaceSalidaTraspaso_Actualizar", parametros);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Guarda una lista de interface salida traspaso.
        /// </summary>
        /// <param name="listaInterfaceSalidaTraspasoDetalle"></param>
        internal void CrearLista(List<InterfaceSalidaTraspasoDetalleInfo> listaInterfaceSalidaTraspasoDetalle)
        {
            try
            {
                Logger.Info();
                var parametros = AuxInterfaceSalidaTraspasoDAL.ObtenerParametrosCrearListado(listaInterfaceSalidaTraspasoDetalle);
                Create("InterfaceSalidaTraspasoDetalle_GuardarXML", parametros);
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
        internal List<InterfaceSalidaTraspasoInfo> ObtenerListaInterfaceSalidaTraspaso()
        {
            List<InterfaceSalidaTraspasoInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("InterfaceSalidaTraspaso_ConsultarListadoDelDia");
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaTraspasoDAL.ObtenerListaInterfaceSalidaTraspaso(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Obtiene el listado del traspaso por el folio y la organizacionID
        /// </summary>
        /// <returns></returns>
        internal InterfaceSalidaTraspasoInfo ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(InterfaceSalidaTraspasoInfo interfaceSalidaTraspasoInfo)
        {
            InterfaceSalidaTraspasoInfo result = null;
            try
            {
                Logger.Info();
                var parametros = AuxInterfaceSalidaTraspasoDAL.ObtenerParametrosInterfaceSalidaTraspasoPorFolioOrganizacion(interfaceSalidaTraspasoInfo);
                DataSet ds = Retrieve("InterfaceSalidaTraspaso_ObtenerPorFolioOrganizacion", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaTraspasoDAL.ObtenerInterfaceSalidaTraspasoPorFolioOrganizacion(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Obtiene 
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="folioOrigen"> </param>
        /// <returns></returns>
        internal InterfaceSalidaTraspasoInfo ObtenerDatosInterfaceSalidaTraspaso(int organizacionID, int folioOrigen)
        {
            InterfaceSalidaTraspasoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxInterfaceSalidaTraspasoDAL.ObtenerParametrosDatosInterfaceSalidaTraspaso(organizacionID,
                                                                                                folioOrigen);
                DataSet ds = Retrieve("InterfaceSalidaTraspaso_ObtenerPorFolioTraspaso", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaTraspasoDAL.ObtenerDatosInterfaceSalidaTraspaso(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene el listado del traspaso por el folio y la organizacionID
        /// </summary>
        /// <returns></returns>
        internal InterfaceSalidaTraspasoInfo ObtenerInterfaceSalidaTraspasoPorLote(LoteInfo loteInfo)
        {
            InterfaceSalidaTraspasoInfo result = null;
            try
            {
                Logger.Info();
                var parametros = AuxInterfaceSalidaTraspasoDAL.ObtenerParametrosInterfaceSalidaTraspasoPorLote(loteInfo);
                DataSet ds = Retrieve("InterfaceSalidaTraspaso_ObtenerPorLoteID", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaTraspasoDAL.ObtenerInterfaceSalidaTraspasoPorLote(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }

        /// <summary>
        /// Obtiene datos de Interface Salida Traspaso
        /// por Lotes
        /// </summary>
        /// <param name="lotes"></param>
        /// <param name="fecha"> </param>
        /// <param name="foliosTraspaso"> </param>
        /// <returns></returns>
        internal List<InterfaceSalidaTraspasoInfo> ObtenerInterfaceSalidaTraspasoLotes(List<LoteInfo> lotes, DateTime fecha, List<PolizaSacrificioModel> foliosTraspaso)
        {
            List<InterfaceSalidaTraspasoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxInterfaceSalidaTraspasoDAL.ObtenerParametrosInterfaceSalidaTraspasoPorLotes(lotes, fecha, foliosTraspaso);
                DataSet ds = Retrieve("InterfaceSalidaTraspaso_ObtenerPorLoteXML", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaTraspasoDAL.ObtenerInterfaceSalidaTraspasoPorLotes(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene una coleccion con los datos que han
        /// sido traspasados por medio de la interface
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaTraspasoInfo> ObtenerTraspasosPorFechaConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<InterfaceSalidaTraspasoInfo> result = null;
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                                     {
                                         {"@OrganizacionID", organizacionID},
                                         {"@FechaInicial", fechaInicial},
                                         {"@FechaFinal", fechaFinal}
                                     };
                DataSet ds = Retrieve("InterfaceSalidaTraspaso_ObtenerTraspasosConciliacion", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaTraspasoDAL.ObtenerInterfaceSalidaTraspasoConciliacion(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene datos de Interface Salida Traspaso
        /// por Lotes
        /// </summary>
        /// <param name="folioTraspaso"></param>
        internal List<InterfaceSalidaTraspasoInfo> ObtenerInterfaceSalidaTraspaso(PolizaSacrificioModel folioTraspaso)
        {
            List<InterfaceSalidaTraspasoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxInterfaceSalidaTraspasoDAL.ObtenerParametrosInterfaceSalidaTraspaso(folioTraspaso);
                DataSet ds = Retrieve("InterfaceSalidaTraspaso_ObtenerPorLote", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaTraspasoDAL.ObtenerInterfaceSalidaTraspasoPorLotes(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
    }
}
