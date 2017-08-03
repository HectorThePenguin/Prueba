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
    internal class MapAnimalCostoDAL
    {
        internal static List<AnimalCostoInfo> ObtenerCostosAnimal(DataSet ds)
        {
            List<AnimalCostoInfo> animalCosto;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                animalCosto = (from info in dt.AsEnumerable()
                               select new AnimalCostoInfo
                               {
                                   AnimalID = info.Field<long>("AnimalID"),
                                   CostoID = info.Field<int>("CostoID"),
                                   Importe = info.Field<decimal>("Importe"),
                                   FolioReferencia = info.Field<long>("FolioReferencia"),
                                   FechaCosto = info.Field<DateTime>("FechaCosto"),
                                   Arete = info.Field<string>("Arete")
                               }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return animalCosto;
        }
    }
}
