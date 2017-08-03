using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapPedidoGanadoDAL
    {
        /// <summary>
        ///     Método asigna el registro del pedido de ganado obtenido
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PedidoGanadoInfo ObtenerPedidoGanado(DataSet ds)
        {
            PedidoGanadoInfo pedidoGanadoInfo;
            try
            {

                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                pedidoGanadoInfo = new PedidoGanadoInfo();
                foreach (DataRow dr in dt.Rows)
                {
                    pedidoGanadoInfo.PedidoGanadoID = Convert.ToInt32(dr["PedidoGanadoID"]);
                    pedidoGanadoInfo.CabezasPromedio = Convert.ToInt32(dr["CabezasPromedio"]);
                    pedidoGanadoInfo.FechaInicio = Convert.ToDateTime(dr["FechaInicio"]);
                    pedidoGanadoInfo.Organizacion= new OrganizacionInfo();
                    pedidoGanadoInfo.Organizacion.OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]);
                    pedidoGanadoInfo.Lunes = Convert.ToInt32(dr["Lunes"]);
                    pedidoGanadoInfo.Martes = Convert.ToInt32(dr["Martes"]);
                    pedidoGanadoInfo.Miercoles = Convert.ToInt32(dr["Miercoles"]);
                    pedidoGanadoInfo.Jueves = Convert.ToInt32(dr["Jueves"]);
                    pedidoGanadoInfo.Viernes = Convert.ToInt32(dr["Viernes"]);
                    pedidoGanadoInfo.Sabado = Convert.ToInt32(dr["Sabado"]);
                    pedidoGanadoInfo.Domingo = Convert.ToInt32(dr["Domingo"]);
                    pedidoGanadoInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pedidoGanadoInfo;
        }

        /// <summary>
        /// Obtiene una lista de PedidoGanadoEspejo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PedidoGanadoEspejoInfo> ObtenerPedidoGanadoEspejoPorPedidoGanadoID(DataSet ds)
        {
            List<PedidoGanadoEspejoInfo> pedidoGanadoEspejoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                pedidoGanadoEspejoInfo = (from info in dt.AsEnumerable()
                              select new PedidoGanadoEspejoInfo
                              {
                                  PedidoGanadoEspejoID = info.Field<int>("PedidoGanadoEspejoID"),
                                  PedidoGanado = new PedidoGanadoInfo()
                                  {
                                      PedidoGanadoID = info.Field<int>("PedidoGanadoID")
                                  },
                                  Activo = info.Field<bool>("Activo").BoolAEnum(),
                                  CabezasPromedio = info.Field<int>("CabezasPromedio"),
                                  Lunes = info.Field<int>("Lunes"),
                                  Martes = info.Field<int>("Martes"),
                                  Miercoles = info.Field<int>("Miercoles"),
                                  Jueves = info.Field<int>("Jueves"),
                                  Viernes = info.Field<int>("Viernes"),
                                  Sabado = info.Field<int>("Sabado"),
                                  Domingo = info.Field<int>("Domingo"),
                                  Estatus = info.Field<bool?>("Estatus"),
                                  FechaInicio = info.Field<DateTime>("FechaInicio"),
                                  Justificacion = info.Field<string>("Justificacion"),
                                  Organizacion = new OrganizacionInfo()
                                  {
                                      OrganizacionID = info.Field<int>("OrganizacionID")
                                  },
                                  UsuarioAproboID = info.Field<int?>("UsuarioAproboID")?? 0,
                                  UsuarioSolicitanteID = info.Field<int>("UsuarioSolicitanteID")
                              }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pedidoGanadoEspejoInfo;
        }

        /// <summary>
        /// Obtiene una lista de PedidoGanadoEspejo
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PedidoGanadoEspejoInfo ObtenerPedidoGanadoEspejo(DataSet ds)
        {
            PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                pedidoGanadoEspejoInfo = (from info in dt.AsEnumerable()
                                          select new PedidoGanadoEspejoInfo
                                          {
                                              PedidoGanadoEspejoID = info.Field<int>("PedidoGanadoEspejoID"),
                                              PedidoGanado = new PedidoGanadoInfo()
                                              {
                                                  PedidoGanadoID = info.Field<int>("PedidoGanadoID")
                                              },
                                              Activo = info.Field<bool>("Activo").BoolAEnum(),
                                              CabezasPromedio = info.Field<int>("CabezasPromedio"),
                                              Lunes = info.Field<int>("Lunes"),
                                              Martes = info.Field<int>("Martes"),
                                              Miercoles = info.Field<int>("Miercoles"),
                                              Jueves = info.Field<int>("Jueves"),
                                              Viernes = info.Field<int>("Viernes"),
                                              Sabado = info.Field<int>("Sabado"),
                                              Domingo = info.Field<int>("Domingo"),
                                              Estatus = info.Field<bool?>("Estatus"),
                                              FechaInicio = info.Field<DateTime>("FechaInicio"),
                                              Justificacion = info.Field<string>("Justificacion"),
                                              Organizacion = new OrganizacionInfo()
                                              {
                                                  OrganizacionID = info.Field<int>("OrganizacionID")
                                              },
                                              UsuarioAproboID = info.Field<int?>("UsuarioAproboID")?? 0,
                                              UsuarioSolicitanteID = info.Field<int>("UsuarioSolicitanteID")
                                          }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return pedidoGanadoEspejoInfo;
        }
    }
}
