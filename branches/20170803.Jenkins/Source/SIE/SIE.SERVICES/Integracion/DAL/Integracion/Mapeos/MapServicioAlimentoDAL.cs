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
    internal class MapServicioAlimentoDAL
    {
        /// <summary>
        ///     Metodo que obtiene corrales por ID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static ServicioAlimentoInfo ObtenerPorCorralID(DataSet ds)
        {
            ServicioAlimentoInfo servicioAlimentoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                servicioAlimentoInfo = (from servicioAlimento in dt.AsEnumerable()
                                        select new ServicioAlimentoInfo
                                {
                                    CodigoCorral = servicioAlimento.Field<string>("Codigo"),
                                    KilosProgramados = servicioAlimento.Field<int>("KilosProgramados"),
                                    DescripcionFormula = servicioAlimento.Field<string>("Formula"),
                                    Comentarios = servicioAlimento.Field<string>("Comentarios"),
                                    FechaRegistro = servicioAlimento.Field<DateTime>("Fecha").ToShortDateString(),
                                    Fecha = servicioAlimento.Field<DateTime>("Fecha"),
                                    CorralID = servicioAlimento.Field<int>("CorralID"),
                                    FormulaID = servicioAlimento.Field<int>("FormulaID"),
                                    OrganizacionID = servicioAlimento.Field<int>("OrganizacionID"),
                                    ServicioID = servicioAlimento.Field<int>("ServicioID"),

                                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return servicioAlimentoInfo;
        }

        /// <summary>
        ///     Metodo que obtiene corrales por ID
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<ServicioAlimentoInfo> ObtenerPorInformacionDiariaAlimento(DataSet ds)
        {
            List<ServicioAlimentoInfo> servicioAlimentoInfo;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                servicioAlimentoInfo = (from servicioAlimento in dt.AsEnumerable()
                                        select new ServicioAlimentoInfo
                                        {
                                            CodigoCorral = servicioAlimento.Field<string>("Codigo"),
                                            KilosProgramados = servicioAlimento.Field<int>("KilosProgramados"),
                                            DescripcionFormula = servicioAlimento.Field<string>("Formula"),
                                            Comentarios = servicioAlimento.Field<string>("Comentarios"),
                                            FechaRegistro = servicioAlimento.Field<DateTime>("Fecha").ToShortDateString(),
                                            Fecha = servicioAlimento.Field<DateTime>("Fecha"),
                                            CorralID = servicioAlimento.Field<int>("CorralID"),
                                            FormulaID = servicioAlimento.Field<int>("FormulaID"),
                                            OrganizacionID = servicioAlimento.Field<int>("OrganizacionID"),
                                            ServicioID = servicioAlimento.Field<int>("ServicioID"),

                                        }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return servicioAlimentoInfo;
        }

    }
}
