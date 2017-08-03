using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using CrystalDecisions.Shared.Json;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapIncidenciasDAL
    {
        internal static List<AlertaInfo> ObtenerConfiguracionAlertas(DataSet ds)
        {
            List<AlertaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new AlertaInfo
                         {
                             AlertaID = info.Field<int>("AlertaID"),
                             Descripcion = info.Field<string>("Descripcion"),
                             HorasRespuesta = info.Field<int>("HorasRespuesta"),
                             TerminadoAutomatico = info.Field<bool>("TerminadoAutomatico").BoolAEnum(),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             ConfiguracionAlerta = new ConfiguracionAlertasInfo
                             {
                                 AlertaConfiguracionID = info.Field<int>("AlertaConfiguracionID"),
                                 Datos = info.Field<string>("Datos"),
                                 Fuentes = info.Field<string>("Fuentes"),
                                 Condiciones = info.Field<string>("Condiciones"),
                                 Agrupador = info.Field<string>("Agrupador"),
                                 Activo = info.Field<bool>("Activo").BoolAEnum(),
                                 NivelAlerta = new NivelAlertaInfo
                                 {
                                     NivelAlertaId = info.Field<int>("NivelAlertaID")
                                 }
                             }
                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static List<IncidenciasInfo> ObtenerIncidenciasPorOrganizacionID(DataSet ds)
        {
            List<IncidenciasInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new IncidenciasInfo
                         {
                             Fecha = info.Field<DateTime>("Fecha"),
                             FechaVencimiento = info["FechaVencimiento"] == DBNull.Value ? new DateTime() : info.Field<DateTime>("FechaVencimiento"),
                             Alerta = new AlertaInfo()
                             {
                                 AlertaID = info.Field<int>("AlertaID"),
                                 Descripcion = info.Field<string>("AlertaDescripcion"),
                                 Modulo = new ModuloInfo()
                                 {
                                     ModuloID = info.Field<int>("ModuloID"),
                                     Descripcion =  info.Field<string>("ModuloDescripcion")
                                 },
                                 ConfiguracionAlerta = new ConfiguracionAlertasInfo()
                                 {
                                     NivelAlerta = new NivelAlertaInfo()
                                     {
                                         NivelAlertaId = info.Field<int>("NivelInicial")
                                     }
                                 }
                             },
                             Accion = new AccionInfo()
                             {
                                 AccionID = info.Field<int>("AccionID"),
                                 Descripcion = (info.Field<string>("AccionDescripcion") ?? "")
                             },
                             Folio = info.Field<int>("Folio"),
                             XmlConsulta = XDocument.Parse(info.Field<string>("XmlConsulta")),
                             NivelAlerta = new NivelAlertaInfo()
                             {
                                 NivelAlertaId = info.Field<int>("NivelAlertaID")
                             },
                             IncidenciasID = info.Field<int>("IncidenciaID"),
                             Comentarios = info.Field<string>("Comentarios"),
                             Organizacion = new OrganizacionInfo()
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Descripcion = info.Field<string>("OrganizacionDescripcion")
                             },
                             UsuarioResponsable = new UsuarioInfo()
                             {
                                UsuarioID = info.Field<int>("UsuarioResponsableID"),   
                             },
                             Estatus = new EstatusInfo()
                             {
                                 EstatusId = info.Field<int>("EstatusID"),
                                 Descripcion = info.Field<string>("EstatusDescripcion")
                             }
                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static List<IncidenciaSeguimientoInfo> ObtenerSeguimientoPorIncidenciaID(DataSet ds)
        {
            List<IncidenciaSeguimientoInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new IncidenciaSeguimientoInfo
                         {
                             IncidenciaSeguimientoID = info.Field<int>("IncidenciaSeguimientoID"),
                             FechaVencimientoAnterior = info.Field<DateTime>("FechaVencimiento"),
                             Accion = new AccionInfo()
                             {
                                 AccionID = info.Field<int>("AccionID"),
                                 Descripcion = info.Field<string>("AccionDescripcion")
                             },
                             AccionAnterior = new AccionInfo()
                             {
                                 AccionID = info.Field<int>("AccionAnteriorID") 
                             },
                             Comentarios = info.Field<string>("Comentarios"),
                             EstatusAnterior = new EstatusInfo()
                             {
                                 EstatusId = info.Field<int>("EstatusAnteriorID")
                             },
                             UsuarioResponsable = new UsuarioInfo()
                             {
                                 UsuarioID = info.Field<int>("UsuarioResponsableID")
                             },
                             UsuarioResponsableAnterior = new UsuarioInfo()
                             {
                                 UsuarioID = info.Field<int>("UsuarioResponsableAnteriorID")
                             },
                             NivelAlertaAnterior = new NivelAlertaInfo()
                             {
                                 NivelAlertaId = info.Field<int>("NivelAlertaAnteriorID")
                             },
                             NivelAlertaActual = new NivelAlertaInfo()
                             {
                                 NivelAlertaId = info.Field<int>("NivelAlertaID")
                             }
                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        public static void ObtenerDatosQuery(DataSet ds)
        {
            int count = ds.Tables["Dtos"].Columns.Count;
            if (count > 0)
            {
                return;
            }
            //List<T> values = new List<T>();
            //for (int i = 0; i < count; i++)
            //{
            //    values.Add((T)ds.Tables["Dtos"].Rows[i][""]);
            //}
            //return values;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<IncidenciasInfo> ObtenerIncidenciasActivas(DataSet ds)
        {
            List<IncidenciasInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new IncidenciasInfo
                         {
                             IncidenciasID = info.Field<int>("IncidenciaID"),
                             Organizacion = new OrganizacionInfo
                             {
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                             },
                             Alerta = new AlertaInfo
                             {
                                 AlertaID = info.Field<int>("AlertaID"),
                                 HorasRespuesta = info.Field<int>("HorasRespuesta"),
                                 TerminadoAutomatico = info.Field<bool>("TerminadoAutomatico").BoolAEnum()
                             },
                             Folio = info.Field<int>("Folio"),
                             XmlConsulta = XDocument.Parse(info.Field<string>("XmlConsulta")),
                             Estatus = new EstatusInfo()
                             {
                                 EstatusId = info.Field<int>("EstatusID"),
                             },
                             Fecha = info.Field<DateTime>("Fecha"),
                             FechaVencimiento = (info.Field<DateTime?>("FechaVencimiento")),
                             NivelAlerta = new NivelAlertaInfo()
                             {
                                 NivelAlertaId = info.Field<int>("NivelAlertaID")
                             },
                             Accion = new AccionInfo
                             {
                                 AccionID = info.Field<int>("AccionID")
                             },
                             UsuarioResponsable = new UsuarioInfo
                             {
                                 UsuarioID = info.Field<int>("UsuarioResponsableID")
                             } ,
                             Comentarios = info.Field<string>("Comentarios"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),

                         }).ToList();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }
        /// <summary>
        /// Obtiene el xml del dateset
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static XDocument ObtenerIncidenciasXML(DataSet ds)
        {
            try{
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                XDocument datos = new XDocument
               (
                   new XElement("NuevaTabla",
                       from row in dt.AsEnumerable()
                       select new XElement("Table",
                                  from column in dt.Columns.Cast<DataColumn>()
                                  select new XElement(column.ColumnName,row[column]))
                       )
               );
                IEnumerable<XElement> hijos = datos.Root.Elements("Table").FirstOrDefault().Elements();
                var contador = 0;
                List<int> hijosFecha = new List<int>();
                string[] formats = new[] { "yyyy-MM-ddThh:mm:ss", "yyyy-MM-dd hh:mm:ss", "yyyy-MM-dd", "yyyy-MM-ddTH:mm:ss" };
                foreach (var hijo in hijos)
                {
                    DateTime fechaFormateada;
                    
                    if (DateTime.TryParseExact(hijo.Value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaFormateada) )
                    {
                        hijosFecha.Add(contador);
                        var x = fechaFormateada.ToShortDateString();
                    }
                    contador++;
                }
                foreach (var index in hijosFecha)
                {
                    var hijos2 = datos.Root.Elements("Table");
                    foreach (var hijo in hijos2)
                    {
                        var fechaFormateada = DateTime.Parse(hijo.Elements().ElementAt(index).Value).ToString("dd/MM/yyyy HH:mm");
                        hijo.Elements().ElementAt(index).SetValue(fechaFormateada);
                    }
                }
                

                return datos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
