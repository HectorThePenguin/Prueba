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
    internal  class MapCorteGanadoDAL
    {
        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static int ObtenerCabezasEnEnfermeria(DataSet ds)
        {
            var resp = 0;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resp = int.Parse(dt.Rows[0][0].ToString());
               
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }
        /// <summary>
        ///     Metodo que obtiene el nombre del proveedor
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static String ObtenerNombreProveedor(DataSet ds)
        {
            var resp = "";
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resp = dt.Rows[0][0].ToString();

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }

        /// <summary>
        ///     Metodo que obtiene si existen programacion corte
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static bool ObtenerExisteProgramacionCorteGanado(DataSet ds)
        {
            var resp = false;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                resp = (int.Parse(dt.Rows[0][0].ToString()) > 0 ? true : false);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }
        
        /// <summary>
        ///     Metodo que obtiene si existen La info del animal 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static AnimalInfo ObtenerExisteExisteAreteEnPartida(DataSet ds)
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

        /// <summary>
        ///     Metodo que obtiene un registro
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal  static int ObtenerCabezasCortadas(DataSet ds)
        {
            var resp = 0;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                resp = int.Parse(dt.Rows[0][0].ToString());

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resp;
        }

        /// <summary>
        /// Obtener las partidas cortadas y sus sobrantes que lleva cortados
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static IList<CabezasSobrantesPorEntradaInfo> ObtenerCabezasSobrantesCortadas(DataSet ds)
        {
            var listaPartidas = new List<CabezasSobrantesPorEntradaInfo>();
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                listaPartidas.AddRange(
                    from DataRow dr in ds.Tables[0].Rows
                    select new CabezasSobrantesPorEntradaInfo
                    {
                        EntradaGanado = new EntradaGanadoInfo()
                        {
                            EntradaGanadoID = Convert.ToInt32(dr["EntradaGanadoID"]),
                            CabezasRecibidas = Convert.ToInt32(dr["CabezasRecibidas"]),
                            CabezasOrigen = Convert.ToInt32(dr["CabezasOrigen"]),
                        },
                        FolioEntrada = Convert.ToInt32(dr["FolioEntrada"]),
                        CabezasSobrantes = Convert.ToInt32(dr["CabezasSobrantes"]),
                        CabezasSobrantesCortadas = Convert.ToInt32(dr["CabezasSobrantesCortadas"]),
                        CabezasCortadas = Convert.ToInt32(dr["CabezasCortadas"]),
                        OrganizacionOrigen = new OrganizacionInfo()
                        {
                            OrganizacionID = Convert.ToInt32(dr["OrganizacionID"]), 
                            TipoOrganizacion = new TipoOrganizacionInfo()
                            {
                                TipoOrganizacionID = Convert.ToInt32(dr["TipoOrganizacionID"]), 
                                Descripcion = Convert.ToString(dr["Descripcion"])
                            }
                        }
                    });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaPartidas;
        }
    }
}
