using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapProgramacionSacrificioDAL
    {

        internal static List<OrdenSacrificioDetalleInfo> ObtenerCorralesSacrificio(DataSet ds)
        {
            List<OrdenSacrificioDetalleInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new OrdenSacrificioDetalleInfo()
                         {
                            Corral = new CorralInfo()
                            {
                                Codigo = info.Field<string>("Codigo"),
                            },
                            Lote = new LoteInfo()
                            {
                                Lote = info.Field<string>("Lote"),
                            },
                            Cabezas = info.Field<int>("CabezasLote"),
                            CabezasASacrificar = info.Field<int>("CabezasSacrificio"),
                            FechaCreacion = info.Field<DateTime>("FechaCreacion")
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
        ///     Metodo que obtiene si existen La info del animal 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static AnimalInfo ObtenerExistenciaAnimal(DataSet ds)
        {
            AnimalInfo animalInfo = null;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                animalInfo = new AnimalInfo();
                var listaMovimiento = new List<AnimalMovimientoInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var animmalMovimientoInfo = new AnimalMovimientoInfo();
                    animmalMovimientoInfo.TipoMovimientoID = Convert.ToInt32(dr["TipoMovimientoID"]);
                    listaMovimiento.Add(animmalMovimientoInfo);

                    animalInfo.AnimalID = Convert.ToInt32(dr["AnimalID"]);
                    animalInfo.Arete = Convert.ToString(dr["Arete"]);
                    animalInfo.AreteMetalico = Convert.ToString(dr["AreteMetalico"]);
                    animalInfo.FechaCompra = Convert.ToDateTime(dr["FechaCompra"]);
                    animalInfo.TipoGanadoID = Convert.ToInt32(dr["TipoGanadoID"]);
                    animalInfo.CalidadGanadoID = Convert.ToInt32(dr["CalidadGanadoID"]);
                    animalInfo.ClasificacionGanadoID = Convert.ToInt32(dr["ClasificacionGanadoID"]);
                    animalInfo.PesoCompra = Convert.ToInt32(dr["PesoCompra"]);
                    animalInfo.OrganizacionIDEntrada = Convert.ToInt32(dr["OrganizacionIDEntrada"]);
                    animalInfo.FolioEntrada = Convert.ToInt32(dr["FolioEntrada"]);
                    animalInfo.PesoLlegada = Convert.ToInt32(dr["PesoLlegada"]);
                    animalInfo.Paletas = Convert.ToInt32(dr["Paletas"]);

                    if (dr["CausaRechadoID"] is DBNull)
                    {
                        animalInfo.CausaRechadoID = 0;
                    }
                    else
                    {
                        animalInfo.CausaRechadoID = Convert.ToInt32(dr["CausaRechadoID"]);
                    }

                    animalInfo.Venta = Convert.ToBoolean(dr["Venta"]);
                    animalInfo.Cronico = Convert.ToBoolean(dr["Cronico"]);
                    animalInfo.Activo = Convert.ToBoolean(dr["Activo"]);
                    animalInfo.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
                    animalInfo.UsuarioCreacionID = Convert.ToInt32(dr["UsuarioCreacionID"]);
                    //animalInfo.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"]);
                    //animalInfo.UsuarioModificacionID = Convert.ToInt32(dr["UsuarioModificacionID"]);
                    animalInfo.CorralID = Convert.ToInt32(dr["CorralID"]);
                    animalInfo.CodigoCorral = Convert.ToString(dr["Codigo"]);

                }
                animalInfo.ListaAnimalesMovimiento = listaMovimiento;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return animalInfo;
        }
    }
}
