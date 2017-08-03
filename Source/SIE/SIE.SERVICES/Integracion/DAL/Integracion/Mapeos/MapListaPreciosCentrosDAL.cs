using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using System.Reflection;
using SIE.Base.Exepciones;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapListaPreciosCentrosDAL
    {
        internal static List<ListaPreciosCentrosInfo> ObtenerListaPreciosCentros(DataSet ds)
        {
            List<ListaPreciosCentrosInfo> precios;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                precios = (from info in dt.AsEnumerable()
                           select new ListaPreciosCentrosInfo
                              {
                                  OrganizacionId = info.Field<int>("OrganizacionID"),
                                  Organizacion = info.Field<string>("Organizacion"),
                                  SociedadId = info.Field<int>("SociedadId"),
                                  ZonaId = info.Field<int>("ZonaId"),
                                  
                                  MachoPesadoId = info.Field<int>("MachoPesadoId"),
                                  MachoPesado = info.Field<double>("MachoPesado"),
                                  PesoPromedioMachoPesado = info.Field<double>("PesoPromedioMachoPesado"),
                                  
                                  ToreteId = info.Field<int>("ToreteId"),
                                  Torete = info.Field<double>("Torete"),
                                  PesoPromedioTorete = info.Field<double>("PesoPromedioTorete"),
                                  
                                  BecerroLigeroId = info.Field<int>("BecerroLigeroId"),
                                  BecerroLigero = info.Field<double>("BecerroLigero"),
                                  PesoPromedioBecerroLigero = info.Field<double>("PesoPromedioBecerroLigero"),
                                  
                                  BecerroId = info.Field<int>("BecerroId"),
                                  Becerro = info.Field<double>("Becerro"),
                                  PesoPromedioBecerro = info.Field<double>("PesoPromedioBecerro"),
                                  
                                  VaquillaTipo2Id = info.Field<int>("VaquillaTipo2Id"),
                                  VaquillaTipo2 = info.Field<double>("VaquillaTipo2"),
                                  PesoPromedioVaquillaTipo2 = info.Field<double>("PesoPromedioVaquillaTipo2"),
                                  
                                  HembraPesadaId = info.Field<int>("HembraPesadaId"),
                                  HembraPesada = info.Field<double>("HembraPesada"),
                                  PesoPromedioHembraPesada = info.Field<double>("PesoPromedioHembraPesada"),
                                  
                                  VaquillaId = info.Field<int>("VaquillaId"),
                                  Vaquilla = info.Field<double>("Vaquilla"),
                                  PesoPromedioVaquilla = info.Field<double>("PesoPromedioVaquilla"),
                                  
                                  BecerraId = info.Field<int>("BecerraId"),
                                  Becerra = info.Field<double>("Becerra"),
                                  PesoPromedioBecerra = info.Field<double>("PesoPromedioBecerra"),
                                  
                                  BecerraLigeraId = info.Field<int>("BecerraLigeraId"),
                                  BecerraLigera = info.Field<double>("BecerraLigera"),
                                  PesoPromedioBecerraLigera = info.Field<double>("PesoPromedioBecerraLigera"),

                                  ToretePesadoId = info.Field<int>("ToretePesadoId"),
                                  ToretePesado = info.Field<double>("ToretePesado"),
                                  PesoPromedioToretePesado = info.Field<double>("PesoPromedioToretePesado")
                              }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return precios;

        }

        internal static bool Guardar(DataSet ds)
        {
            var result = false;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                if (Convert.ToInt32(dt.Rows[0][0]) == 1)
                {
                    result = true;
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
