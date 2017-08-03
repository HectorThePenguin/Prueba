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
    internal class MapMigracionDAL
    {

        internal static ControlIndividualInfo ObtenerAnimalesControlIndividual(DataSet ds)
        {
            ControlIndividualInfo controlIndividualInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtCtrMani];
                controlIndividualInfo = new ControlIndividualInfo
                {
                    ListaCtrManiInfo = (from ctrManiInfo in dt.AsEnumerable()
                        select new CtrManiInfo
                        {
                            Arete = ctrManiInfo.Field<string>("Arete"),
                            FechaComp = ctrManiInfo.Field<DateTime>("Fecha_Comp"),
                            FechaCorte = ctrManiInfo.Field<DateTime>("Fecha_Corte"),
                            NumCorr = ctrManiInfo.Field<string>("Num_Corr"),
                            CalEng = ctrManiInfo.Field<string>("Cal_Eng"),
                            PesoCorte = ctrManiInfo.Field<int>("Peso_Corte"),
                            Paletas = ctrManiInfo.Field<int>("Paletas"),
                            TipoGan = ctrManiInfo.Field<string>("Tipo_Gan"),
                            Temperatura = ctrManiInfo.Field<decimal>("Temperatura")
                        }).ToList()
                };

                if (controlIndividualInfo.ListaCtrManiInfo != null && controlIndividualInfo.ListaCtrManiInfo.Any())
                {
                    dt = ds.Tables[ConstantesDAL.DtCtrReim];
                    controlIndividualInfo.ListaCtrReimInfo = (from ctrReimInfo in dt.AsEnumerable()
                                        select new CtrReimInfo
                                        {
                                            Arete = ctrReimInfo.Field<string>("Arete"),
                                            FechaReim = ctrReimInfo.Field<DateTime>("Fecha_Reim"),
                                            FechaComp = ctrReimInfo.Field<DateTime>("Fecha_Comp"),
                                            PesoReimp = ctrReimInfo.Field<int>("Peso_Reimp"),
                                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return controlIndividualInfo;
        }

        /// <summary>
        /// Metodo para 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="paso"></param>
        /// <returns></returns>
        internal static MigracionCifrasControlInfo ObtenerMigracionCifrasContro(DataSet ds, int paso)
        {
            MigracionCifrasControlInfo cifrasInfo;
            try
            {
                Logger.Info();
                var tabla = paso == 3 ? ConstantesDAL.DtDetalle : ConstantesDAL.DtDatos;
                var dt = ds.Tables[tabla];
                cifrasInfo = (from info in dt.AsEnumerable()
                              select new MigracionCifrasControlInfo
                              {
                                  Paso = paso,
                                  TotalCabezas = info["TotalCabezas"] == DBNull.Value? 0 : info.Field<long>("TotalCabezas"),
                                  TotalCostos = info["TotalCostos"] == DBNull.Value ? 0 : info.Field<double>("TotalCostos")
                              }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return cifrasInfo;
        }
    }
}
