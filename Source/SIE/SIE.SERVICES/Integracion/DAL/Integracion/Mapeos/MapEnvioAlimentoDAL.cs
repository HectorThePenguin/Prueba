using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapEnvioAlimentoDAL
    {
        internal static EnvioAlimentoInfo RegistrarEnvioAlimento(DataSet ds)
        {
            EnvioAlimentoInfo lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new EnvioAlimentoInfo
                         {
                             EnvioId = int.Parse(info.Field<object>("EnvioProductoID").ToString()),
                             FechaEnvio = info.Field<DateTime>("FechaEnvio")
                             
                         }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }
    }
}
