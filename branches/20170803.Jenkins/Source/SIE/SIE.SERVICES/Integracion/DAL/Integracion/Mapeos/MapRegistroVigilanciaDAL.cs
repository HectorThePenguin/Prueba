using System;
using System.Data;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapRegistroVigilanciaDAL
    {
        /// <summary>
        /// Obtiene por un registro vigilancia por id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        internal static RegistroVigilanciaInfo ObtenerPorId(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var entidad =
                    (from info in dt.AsEnumerable()
                     select
                         new RegistroVigilanciaInfo
                         {
                             RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID"),
                             Organizacion = new OrganizacionInfo() {OrganizacionID = info.Field<int>("OrganizacionID")},
                             ProveedorMateriasPrimas = new ProveedorInfo() { ProveedorID = info.Field<int>("ProveedorIDMateriasPrimas") },
                             Contrato = new ContratoInfo() { ContratoId = info["ContratoID"] == DBNull.Value ? 0 : info.Field<int>("ContratoID") },
                             ProveedorChofer = new ProveedorChoferInfo() { ProveedorChoferID = info["ProveedorChoferID"] == DBNull.Value ? 0 : info.Field<int>("ProveedorChoferID") },
                             Transportista = info["Transportista"] == DBNull.Value ? "" : info.Field<string>("Transportista"),
                             Chofer = info["Chofer"] == DBNull.Value ? "" : info.Field<string>("Chofer"),
                             Producto = new ProductoInfo(){ProductoId = info.Field<int>("ProductoID")},
                             Camion = new CamionInfo() { CamionID = info["CamionID"] == DBNull.Value ? 0 : info.Field<int>("CamionID") },
                             CamionCadena = info["Camion"] == DBNull.Value ? "" : info.Field<string>("Camion"),
                             Marca = info.Field<string>("Marca"),
                             Color = info.Field<string>("Color"),
                             FolioTurno = info.Field<int>("FolioTurno"),
                             FechaLlegada = info["FechaLlegada"] == DBNull.Value ? new DateTime(1900, 01, 01) : info.Field<DateTime>("FechaLlegada"),
                             FechaSalida = info["FechaSalida"] == DBNull.Value ? new DateTime(1900, 01, 01) : info.Field<DateTime>("FechaSalida"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             IdNulo = 0
                         }).First();
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene por un registro vigilancia por id
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>RegistroVigilanciaInfo</returns>
        internal static ResultadoInfo<RegistroVigilanciaInfo> ObtenerPorPagina(DataSet ds)
        {
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                var lista =
                    (from info in dt.AsEnumerable()
                     select
                         new RegistroVigilanciaInfo
                         {
                             RegistroVigilanciaId = info.Field<int>("RegistroVigilanciaID"),
                             Organizacion = new OrganizacionInfo
                                                {
                                                    OrganizacionID = info.Field<int>("OrganizacionID")
                                                },
                             ProveedorMateriasPrimas = new ProveedorInfo
                                                           {
                                                               ProveedorID = info.Field<int>("ProveedorIDMateriasPrimas"),
                                                               Descripcion = info.Field<string>("Proveedor")
                                                           },
                             Contrato = new ContratoInfo
                                            {
                                                ContratoId = info["ContratoID"] == DBNull.Value ? 0 : info.Field<int>("ContratoID"),
                                                Folio = info["Folio"] == DBNull.Value ? 0 : info.Field<int>("Folio")
                                            },
                             ProveedorChofer = new ProveedorChoferInfo
                                                   {
                                                       ProveedorChoferID = info["ProveedorChoferID"] == DBNull.Value ? 0 : info.Field<int>("ProveedorChoferID")
                                                   },
                             Transportista = info["Transportista"] == DBNull.Value ? "" : info.Field<string>("Transportista"),
                             Chofer = info["Chofer"] == DBNull.Value ? "" : info.Field<string>("Chofer"),
                             Producto = new ProductoInfo
                                            {
                                                ProductoId = info.Field<int>("ProductoID"),
                                                Descripcion = info.Field<string>("Producto"),
                                                ProductoDescripcion = info.Field<string>("Producto")
                                            },
                             Camion = new CamionInfo
                                          {
                                              CamionID = info["CamionID"] == DBNull.Value ? 0 : info.Field<int>("CamionID")
                                          },
                             CamionCadena = info["Camion"] == DBNull.Value ? "" : info.Field<string>("Camion"),
                             Marca = info.Field<string>("Marca"),
                             Color = info.Field<string>("Color"),
                             FolioTurno = info.Field<int>("FolioTurno"),
                             FechaLlegada = info["FechaLlegada"] == DBNull.Value ? new DateTime(1900, 01, 01) : info.Field<DateTime>("FechaLlegada"),
                             FechaSalida = info["FechaSalida"] == DBNull.Value ? new DateTime(1900, 01, 01) : info.Field<DateTime>("FechaSalida"),
                             Activo = info.Field<bool>("Activo").BoolAEnum(),
                             IdNulo = 0
                         }).ToList();

                int totalRegistros = Convert.ToInt32(ds.Tables[ConstantesDAL.DtContador].Rows[0]["TotalReg"]);
                var resultado =
                    new ResultadoInfo<RegistroVigilanciaInfo>
                    {
                        Lista = lista,
                        TotalRegistros = totalRegistros
                    };
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene los parametros para consultar Disponibilidad de Camion por placas
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static bool ObtenerDisponibilidadCamion(DataSet ds)
        {
            int registros = 0;
            var resultado = false;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                foreach (DataRow dr in dt.Rows)
                {
                    registros = Convert.ToInt32(dr["SalidaCamion"]);
                }

                if (registros > 0)
                {
                    resultado = true;
                }
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
