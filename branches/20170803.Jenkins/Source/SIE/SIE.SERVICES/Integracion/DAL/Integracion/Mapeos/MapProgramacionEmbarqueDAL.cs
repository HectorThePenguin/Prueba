using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapProgramacionEmbarqueDAL
    {
        /// <summary>
        /// Método para leer los datos obtenidos desde la base de datos.
        /// </summary>
        /// <param name="ds"> DataSet con la información ingresada por la base de datos. </param>
        /// <returns> Lista con los embarques encontrados en la base de datos. </returns>
        internal static List<EmbarqueInfo> ObtenerProgramacionPorOrganizacionId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EmbarqueInfo
                         {
                             Organizacion = new OrganizacionInfo()
                                            {
                                                OrganizacionID = info.Field<int>("OrganizacionID"),
                                            },
                             DescripcionEstatus = info.Field<string>("Estatus"),
                             FolioEmbarque = info.Field<int>("FolioEmbarque"),
                             ListaEscala = new List<EmbarqueDetalleInfo>()
                                            {
                                                new EmbarqueDetalleInfo()
                                                {
                                                    OrganizacionOrigen = new OrganizacionInfo()
                                                    {
                                                        OrganizacionID = info.Field<int>("OrganizacionOrigenID"),
                                                        Descripcion = info.Field<string>("OrganizacionOrigen")
                                                    },

                                                    OrganizacionDestino = new OrganizacionInfo()
                                                    {
                                                        OrganizacionID = info.Field<int>("OrganizacionDestinoID"),
                                                        Descripcion = info.Field<string>("OrganizacionDestino")
                                                    }
                                                }
                                            },
                             ResponsableEmbarque = info.Field<string>("ResponsableEmbarque"),
                             TipoEmbarque = new TipoEmbarqueInfo()
                                             {
                                                 Descripcion = info.Field<string>("TipoEmbarque"),
                                                 TipoEmbarqueID = info.Field<int>("TipoEmbarqueID")
                                             },
                             Observacion = new ObservacionInfo()
                             {
                                 Descripcion = info["Observaciones"] == DBNull.Value ? String.Empty : info.Field<string>("Observaciones").Replace("\\n","\n")
                             },
                             CitaCarga =  info.Field<DateTime>("CitaCarga"),
                             CitaDescarga = info.Field<DateTime>("CitaDescarga"),
                             HorasTransito = info.Field<int?>("HorasTransito"),
                             EmbarqueID = info.Field<int>("EmbarqueID"),
                             DatosCapturados = info.Field<bool?>("DatosCapturados") == null ? false : info.Field<bool>("DatosCapturados")

                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método para mapear los ruteos obtenidos de la base de datos.
        /// </summary>
        /// <param name="ds"> DataSet con las rutas de la base de datos. </param>
        /// <returns> Lista de los ruteos obtenidos </returns>
        internal static List<RuteoInfo> ObtenerRuteosPorOrganizacionId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RuteoInfo
                         {
                             RuteoID = info.Field<int>("RuteoID"),
                             NombreRuteo = info.Field<string>("NombreRuteo")
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que mapea los datos de la base de datos por el procedimiento.
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida por la base de datos </param>
        /// <returns> Lista con los detalles del ruteo seleccionado </returns>
        internal static List<RuteoDetalleInfo> ObtenerRuteoDetallesPorRuteoId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RuteoDetalleInfo()
                         {
                             RuteoDetalleID = info.Field<int>("RuteoDetalleID"),
                             OrganizacionOrigen = new OrganizacionInfo()
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                             },
                             Fecha = info.Field<DateTime>("Fecha"),
                             Horas = info.Field<double>("Horas")
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que lee los datos obtenidos por el método ObtenerTiposEmbarque
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida </param>
        /// <returns> Objeto con los datos obtenidos </returns>
        internal static List<TipoEmbarqueInfo> ObtenerTiposEmbarque(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<TipoEmbarqueInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new TipoEmbarqueInfo
                         {
                             TipoEmbarqueID = info.Field<int>("TipoEmbarqueID"),
                             Descripcion = info.Field<string>("Descripcion")
                         }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método para leer los datos obtenidos desde la base de datos.
        /// </summary>
        /// <param name="ds"> DataSet con la información ingresada por la base de datos. </param>
        /// <returns> Lista con las jaulas programadas encontradas en la base de datos. </returns>
        internal static List<EmbarqueInfo> ObtenerJaulasProgramadas(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EmbarqueInfo
                         {
                             CitaCarga = info.Field<DateTime>("Fecha"),
                             JaulasProgramadas = info.Field<int>("JaulasProgramadas"),

                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que lee el EmbarqueID del registro mas reciente
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida </param>
        /// <returns> Objeto con los datos obtenidos </returns>
        internal static List<EmbarqueInfo> ObtenerEmbarqueID(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[0];
                var lista =
                    (from info in dt.AsEnumerable()
                        select
                            new EmbarqueInfo
                            {
                                EmbarqueID = info.Field<int>("EmbarqueID")
                            }).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que mapea los ruteos del embarque a editar.
        /// </summary>
        /// <param name="ds"> Data set con la información obtenida desde la base de datos. </param>
        /// <returns> Listado con los ruteos encontrados por el embarque </returns>
        internal static List<EmbarqueRuteoInfo> ObtenerDetallesEmbarqueRuteo(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EmbarqueRuteoInfo()
                         {
                             EmbarqueRuteoID = info.Field<int>("EmbarqueRuteoID"),
                             OrganizacionOrigen = new OrganizacionInfo()
                             {
                                 Descripcion = info.Field<string>("Descripcion")
                             },
                             Fecha = info.Field<DateTime>("FechaProgramada"),
                             Horas = info.Field<TimeSpan>("Horas"),
                             Ruteo = new RuteoInfo()
                             {
                                 RuteoID = info.Field<int?>("RuteoID") == null ? 0 : info.Field<int>("RuteoID")
                             }
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que mapea el flio de embarque.
        /// </summary>
        /// <param name="ds"> Data set con la información obtenida desde la base de datos. </param>
        /// <returns> Listado con los ruteos encontrados por el embarque </returns>
        internal static EmbarqueInfo ObtenerFolioEmbarque(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EmbarqueInfo()
                         {
                             FolioEmbarque = info.Field<int>("FolioEmbarque"),
                             DescripcionEstatus = info.Field<string>("Descripcion")

                         }).First();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Procedimiento que valida que exista tarifa para un origen y un destino
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ConfiguracionEmbarqueInfo> ObtenerFleteOrigenDestino(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new ConfiguracionEmbarqueInfo
                         {
                             ConfiguracionEmbarqueID = info.Field<int>("ConfiguracionEmbarqueID")
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene la información para la consulta general de la pestaña de transporte
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida desde la base de datos </param>
        /// <returns> Listado con los embarques obtenidos </returns>
        internal static List<EmbarqueInfo> ObtenerProgramacionTransporte(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EmbarqueInfo()
                         {
                             EmbarqueID = info.Field<int>("EmbarqueID"),
                             FolioEmbarque = info.Field<int>("FolioEmbarque"),
                             Proveedor = new ProveedorInfo()
                             {
                                 ProveedorID = info.Field<int?>("ProveedorID") == null ? 0 : info.Field<int>("ProveedorID"),
                                 CodigoSAP = info.Field<string>("CodigoSAP") == null ? "0" : info.Field<string>("CodigoSAP"),
                                 Descripcion = info.Field<string>("Proveedor") == null ? "" : info.Field<string>("Proveedor"),
                                 Correo = info.Field<string>("Correo") == null ? "" : info.Field<string>("Correo")
                             },
                             ConfiguracionEmbarque = new ConfiguracionEmbarqueInfo()
                             {
                                 ConfiguracionEmbarqueDetalle = new ConfiguracionEmbarqueDetalleInfo()
                                 {
                                     ConfiguracionEmbarqueID = info.Field<int?>("RutaID"),
                                     Descripcion = info.Field<string>("DescripcionRuta") == null ? "" : info.Field<string>("DescripcionRuta"),
                                     Kilometros = info.Field<decimal?>("Kilometros") == null ? 0 : info.Field<decimal?>("Kilometros"),
                                 },
                                 OrganizacionOrigen = new OrganizacionInfo()
                                 {
                                     OrganizacionID = info.Field<int>("Origen")
                                 },
                                 OrganizacionDestino = new OrganizacionInfo()
                                 {
                                     OrganizacionID = info.Field<int>("Destino")
                                 }
                             },
                             PendienteTransporte = info.Field<bool>("Pendiente"),
                             CitaCarga = info.Field<DateTime>("CitaCarga"),
                             CitaDescarga = info.Field<DateTime>("CitaDescarga"),
                             Costos = new List<EmbarqueCostoInfo>()
                             {
                                 new EmbarqueCostoInfo()
                                 {
                                     Importe = info.Field<decimal?>("Flete") == null ? 0 : info.Field<decimal?>("Flete")
                                 },
                                 new EmbarqueCostoInfo()
                                 {
                                     Importe = info.Field<decimal?>("GastoVariable") == null ? 0 : info.Field<decimal?>("GastoVariable")
                                 },
                                 new EmbarqueCostoInfo()
                                 {
                                     Importe = info.Field<decimal?>("Demora") == null ? 0 : info.Field<decimal?>("Demora")
                                 }
                             },
                             GastosFijos = new List<EmbarqueGastosFijosInfo>()
                             {
                                 new EmbarqueGastosFijosInfo()
                                 {
                                     Importe = info.Field<decimal?>("GastoFijo") == null ? 0 : info.Field<decimal?>("GastoFijo")
                                 }
                             },
                             Observaciones = info.Field<string>("Observaciones") == null ? "" : info.Field<string>("Observaciones").Replace("\\n","\n"),
                             DobleTransportista = info.Field<bool>("DobleTransportista"),
                             DatosCapturados = info.Field<bool?>("DatosCapturados") == null ? false : info.Field<bool>("DatosCapturados")
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene la información para la consulta general de la pestaña de datos
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida desde la base de datos </param>
        /// <returns> Listado con los embarques obtenidos </returns>
        internal static List<EmbarqueInfo> ObtenerProgramacionDatos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EmbarqueInfo()
                         {
                             EmbarqueID = info.Field<int>("EmbarqueID"),
                             FolioEmbarque = info.Field<int>("FolioEmbarque"),
                             DobleTransportista = info.Field<bool>("DobleTransportista"),
                             Proveedor = new ProveedorInfo()
                             {
                                 ProveedorID = info.Field<int?>("ProveedorID") == null ? 0 : info.Field<int>("ProveedorID")
                             },
                             Operador1 = new ChoferInfo()
                             {
                                 ChoferID = info.Field<int?>("Operador1ID") == null ? 0 : info.Field<int>("Operador1ID"),
                                 Nombre = info.Field<string>("Operador1") == null ? "" : info.Field<string>("Operador1")
                             },
                             Operador2 = new ChoferInfo()
                             {
                                 ChoferID = info.Field<int?>("Operador2ID") == null ? 0 : info.Field<int>("Operador2ID"),
                                 Nombre = info.Field<string>("Operador2") == null ? "" : info.Field<string>("Operador2")
                             },
                             Tracto = new CamionInfo()
                             {
                                 CamionID = info.Field<int?>("CamionID") == null ? 0 : info.Field<int>("CamionID"),
                                 PlacaCamion = info.Field<string>("PlacaTracto") == null ? "" : info.Field<string>("PlacaTracto"),
                                 Economico = info.Field<string>("EcoTracto") == null ? "" : info.Field<string>("EcoTracto")
                             },
                             Jaula = new JaulaInfo()
                             {
                                 JaulaID = info.Field<int?>("JaulaID") == null ? 0 : info.Field<int>("JaulaID"),
                                 PlacaJaula = info.Field<string>("PlacaJaula") == null ? "" : info.Field<string>("PlacaJaula"),
                                 NumEconomico = info.Field<string>("EcoJaula") == null ? "" : info.Field<string>("EcoJaula"),
                             },
                             Observaciones = info.Field<string>("Observaciones") == null ? "" : info.Field<string>("Observaciones").Replace("\\n", "\n"),
                             DatosCapturados = info.Field<bool>("Pendiente")
                         }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para consultar el costo del flete 
        /// </summary>
        /// <param name="ds"> Data set con la información obtenida desde la base de datos. </param>
        /// <returns> Costo del Flete </returns>
        internal static EmbarqueTarifaInfo ObtenerCostoFlete(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EmbarqueTarifaInfo()
                         {
                             EmbarqueTarifaID = info.Field<int>("EmbarqueTarifaID"),
                             Importe = info.Field<decimal>("ImporteTarifa")

                         }).First();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que obtiene la información para los gastos fijos de acuerdo a la ruta seleccionada.
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida desde la base de datos </param>
        /// <returns> Objeto que contiene la suma de los gastos fijos de acuerdo a la ruta. </returns>
        internal static AdministracionDeGastosFijosInfo ObtenerGastosFijos(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new AdministracionDeGastosFijosInfo()
                         {

                             Importe = info.Field<decimal?>("Importe") == null ? 0 : info.Field<decimal>("Importe")

                         }).First();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que mapea la información obtenidad desde la base de datos con la información para el correo
        /// </summary>
        /// <param name="ds"> DataSet con la información obtenida desde la base de datos </param>
        /// <returns> Objeto con la información del correo a enviar. </returns>
        internal static EmbarqueInfo ObtenerInformacionCorreo(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                EmbarqueInfo embarque =
                    (from info in dt.AsEnumerable()
                     select
                         new EmbarqueInfo
                         {
                             EmbarqueID = info.Field<int>("EmbarqueID"),
                             TipoEmbarque = new TipoEmbarqueInfo()
                             {
                                 TipoEmbarqueID = info.Field<int>("TipoEmbarqueID"),
                                 Descripcion = info.Field<string>("TipoEmbarque")
                             },
                             Ruteo = new RuteoInfo()
                             {
                                 OrganizacionOrigen = new OrganizacionInfo()
                                 {
                                     OrganizacionID = info.Field<int>("OrganizacionOrigenID"),
                                     Descripcion = info.Field<string>("OrganizacionOrigen")
                                 },
                                 OrganizacionDestino = new OrganizacionInfo()
                                 {
                                     OrganizacionID = info.Field<int>("OrganizacionDestinoID"),
                                     Descripcion = info.Field<string>("OrganizacionDestino")
                                 },
                             },
                             CitaCarga = info.Field<DateTime>("CitaCarga"),
                             ResponsableEmbarque = info.Field<string>("ResponsableEmbarque")
                         }).First();
                return embarque;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Método que mapea el estatus del embarque.
        /// </summary>
        /// <param name="ds"> Data set con la información obtenida desde la base de datos. </param>
        /// <returns> Estatus Validado </returns>
        internal static EmbarqueInfo ObtenerEstatusValido(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new EmbarqueInfo()
                         {
                             DescripcionEstatus = info.Field<string>("Descripcion")

                         }).First();

                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
