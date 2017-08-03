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
    internal class MapConfiguracionParametroChequeDAL
    {
        internal static List<CatParametroConfiguracionBancoInfo> ObtenerConfiguracionParametroBanco(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                List<CatParametroConfiguracionBancoInfo> lista =
                    (from info in dt.AsEnumerable()
                     select
                         new CatParametroConfiguracionBancoInfo
                         {
                             CatParametroConfiguracionBancoID = info.Field<int>("CatParametroConfigBancoID"),
                             BancoID  = new BancoInfo{BancoID = info.Field<int>("BancoID")},
                             ParametroID = new CatParametroBancoInfo
                             {
                                 ParametroID = info.Field<int>("CatParametroBancoID"),
                                 Descripcion = info.Field<string>("Descripcion"),
                                 Valor = info.Field<string>("Valor")
                             },
                             X = info.Field<int>("X"),
                             Y = info.Field<int>("Y"),
                             Width = info.Field<int>("Width"),
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();


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
