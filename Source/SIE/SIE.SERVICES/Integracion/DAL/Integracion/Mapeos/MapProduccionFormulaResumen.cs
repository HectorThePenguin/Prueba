using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapProduccionFormulaResumen
    {
        /// <summary>
        /// Obtiene de la formula ingresada y para la organizacion del usuario, cuanto hay en inventario y cuanto esta programada para repartir
        /// </summary>
        internal static ProduccionFormulaInfo ConsultarFormulaId(DataSet ds)
        {
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                ProduccionFormulaInfo resultado =
                    (from info in dt.AsEnumerable()
                     select
                         new ProduccionFormulaInfo
                         {
                             DescripcionFormula = info.Field<string>("DescripcionFormula"),
                             CantidadProducida = info.Field<decimal>("CantidadProducida"),
                             CantidadReparto = Convert.ToDecimal(info.Field<int>("CantidadReparto")),
                         }).First();
                return resultado;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
