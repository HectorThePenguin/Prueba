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
    internal class MapSociedadDAL
    {
        internal static List<SociedadInfo> ObtenerTodas(DataSet ds)
        {
            List<SociedadInfo> result;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                result = (from info in dt.AsEnumerable()
                          select new SociedadInfo 
                          {
                              SociedadID = info.Field<int>("SociedadID"),
                              Descripcion = info.Field<string>("Descripcion"),
                              Pais = info.Field<int>("PaisID"),
                              Activo = info.Field<bool>("Activo")
                          }).ToList();
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
