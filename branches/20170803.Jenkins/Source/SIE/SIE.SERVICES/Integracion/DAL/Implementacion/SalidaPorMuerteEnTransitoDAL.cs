using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Facturas;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{         
    class SalidaGanadoEnTransitoDAL : DALBase
    {
        public bool GuardarSalida(ref SalidaGanadoEnTransitoInfo info)//regresa true si el registro y el proceso se completo exitosamente
        {
            try
            {
                //consulta el folio correspondiente a la salida de ganado
                var folio = (info.Muerte) ? TipoFolio.SalidaMuerteEnTransito.GetHashCode() : TipoFolio.SalidaVentaEnTransito.GetHashCode();
                info.Folio = Folio(info.OrganizacionID, folio);
                
                //registra la salida de ganado y sus detalles:
                GuardarSalidaEnTransito(info);

                if(info.Venta)       
                {
                    //generar serie de factura y asignar al registro:
                    AsignarSerieFactura(info);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// asigna la serie de factura al registro de salida de ganado
        /// </summary>
        /// <param name="salida">salida de ganado en transito al que e le asignara la serie de factura correspondiente</param>
        private void AsignarSerieFactura(SalidaGanadoEnTransitoInfo salida)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaGanadoEnTransitoDAL.ObtenerParametrosAsignarFolioFactura(salida);
                Update("SalidaGanadoTransitoVenta_GenerarFolioFactura", parameters);

            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// /registra la salida de ganado y sus detalles
        /// </summary>
        /// <param name="info">salida de ganado en transito que se registrara</param>
        internal void GuardarSalidaEnTransito(SalidaGanadoEnTransitoInfo info)
        {
            try
            {
                Logger.Info();
                info.FechaCreacion = DateTime.Now;
                Dictionary<string, object> parameters = AuxSalidaGanadoEnTransitoDAL.ObtenerParametrosCrear_Salida(info);
                Create("SalidaGanadoTransito_Guardar", parameters);

            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Actualiza los importes,cantidades de los registros de entrada de ganado en transito correspondientes a la salida
        /// </summary>
        /// <param name="info">salida de ganado en transito en base al cual se actualizaran las tablas de entradas de ganado en transito y del lote</param>
        /// <returns>Regresa un true si se logro actualizar exitosamente las entradas de ganado en transito y del lote</returns>
        internal bool ActualizarEntradas(SalidaGanadoEnTransitoInfo info)
        {
            try
            {
                Logger.Info();
                info.FechaCreacion = DateTime.Now;

               var importes=new List<CostoInfo>();
                for (int i=0;i< info.DetallesSalida.Count;i++)
                {
                    var temp = new CostoInfo
                    {
                        CostoID = info.DetallesSalida[i].CostoId,
                        ImporteCosto = info.Costos[i].Importe - info.DetallesSalida[i].ImporteCosto
                    };
                    importes.Add(temp);
                }

                Dictionary<string, object> parameters = AuxSalidaGanadoEnTransitoDAL.ObtenerParametrosActualizarEntradas(info,importes);
                Update("SalidaGanadoTransito_ActualizarEntradasGanadoTransito", parameters);

                return true;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene el numero de folio correspondiente para la salida por muerte en transito
        /// </summary>
        /// <param name="organizacionId">Id de la organizacion para la cual se genera el folio</param>
        /// <param name="tipoFolioId">ID del tipo de folio que se generara</param>
        /// <returns>Regresa el folio que le corresponde a la salida de ganado en transito</returns>
        internal int Folio(int organizacionId, int tipoFolioId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaGanadoEnTransitoDAL.Folio(organizacionId, tipoFolioId);
                var resultado = Create("Folio_ObtenerPorOrganizacionTipoFolio", parameters);

                return resultado;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// consulta los datos faltantes para generar la poliza(sociedad,Cuenta,etc)
        /// </summary>
        /// <param name="salida">salida de ganado en transito de la que se obtendran los datos faltantes para la generacion de la poliza</param>
        /// <returns>Regresa los datos faltantes para la generacion de la poliza</returns>
        internal DatosPolizaSalidaGanadoTransitoInfo ObtenerDatosPolizaSalidaPorMuerte(SalidaGanadoEnTransitoInfo salida)
        {
            try
            {
                Logger.Info();
                DatosPolizaSalidaGanadoTransitoInfo result;
                Dictionary<string, object> parameters = AuxSalidaGanadoEnTransitoDAL.ObtenerParametrosObtenerPoliza(salida);
                DataSet ds = Retrieve("SalidaGanadoTransito_PolizaSalida", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapPolizaSalidaGanadoTransitoDAL.ObtenerDatosPoliza(ds);
                    return result;
                }
                return new DatosPolizaSalidaGanadoTransitoInfo();

            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Asigna el numero de poliza generada al registro de salida de ganado
        /// </summary>
        /// <param name="input">salida de ganado en transito al que se asignara la poliza generada</param>
        public void AsignarPolizaRegistrada(SalidaGanadoEnTransitoInfo input)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaGanadoEnTransitoDAL.ObtenerParametrosAsignarPolizaSalida(input);
                Update("SalidaGanadoTransito_ActualizarPoliza", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Genera la factura para las salidas de ganado en transito venta
        /// </summary>
        /// <param name="folio">el folio que se acava de registrar actualmente</param>
        /// <param name="activo">El SP valida que activo (Venta) sea igual a true</param>
        /// <returns>Regresa la informacion de la factura de la salida de ganado en transito por venta</returns>
        internal FacturaInfo SalidaGanadoTransito_ObtenerDatosFactura(int folio, bool activo)
        {
            FacturaInfo factura = null;
            try
            {
                Dictionary<string, object> parameters = AuxSalidaGanadoEnTransitoDAL.SalidaGanadoTransito_ObtenerDatosFactura(folio, activo);
                DataSet ds = Retrieve("SalidaGanadoTransito_ObtenerDatosFactura", parameters);
                if (ValidateDataSet(ds))
                {
                    factura = MapSalidaGanadoTransito.SalidaGanadoTransito_ObtenerDatosFactura(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return factura;
        }
    }
}
