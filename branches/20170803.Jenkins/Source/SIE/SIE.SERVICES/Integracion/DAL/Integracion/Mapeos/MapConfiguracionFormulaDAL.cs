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
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    public class MapConfiguracionFormulaDAL
    {
        /// <summary>
        /// Obtener mapeo para los datos opbtenidos de la configuracion de las formulas para una organizacion
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<ConfiguracionFormulaInfo> ObtenerConfiguracionFormula(DataSet ds)
        {
            List<ConfiguracionFormulaInfo> lista;
            try
            {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];

                lista = (from info in dt.AsEnumerable()
                         select new ConfiguracionFormulaInfo
                         {
                             OrganizacionID = info.Field<int>("OrganizacionID"),
                             Formula = new FormulaInfo{FormulaId = info.Field<int>("FormulaID")},
                             PesoInicioMinimo = info.Field<int>("PesoInicioMinimo"),
                             PesoInicioMaximo = info.Field<int>("PesoInicioMaximo"),
                             TipoGanado = info.Field<string>("TipoGanado"),
                             PesoSalida = info.Field<int>("PesoSalida"),
                             FormulaSiguiente = new FormulaInfo { FormulaId = info.Field<int>("FormulaSiguienteID") },
                             DiasEstanciaMinimo = info.Field<int>("DiasEstanciaMinimo"),
                             DiasEstanciaMaximo = info.Field<int>("DiasEstanciaMaximo"),
                             DiasTransicionMinimo = info.Field<int>("DiasTransicionMinimo"),
                             DiasTransicionMaximo = info.Field<int>("DiasTransicionMaximo"),
                             Disponibilidad = info.Field<bool>("Disponibilidad")
                                                            ? Disponibilidad.Si
                                                            : Disponibilidad.No,
                             Activo = info.Field<bool>("Activo").BoolAEnum()
                         }).ToList();
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }

        internal static List<ConfiguracionFormulaInfo> ObtenerFormulaPorTipoGanado(DataSet ds)
        {
            List<ConfiguracionFormulaInfo> resultado;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];

                var lista = (from info in dt.AsEnumerable()
                             select new ConfiguracionFormulaInfo
                             {
                                 ConfiguracionFormulaID = info.Field<int>("ConfiguracionFormulaID"),
                                 OrganizacionID = info.Field<int>("OrganizacionID"),
                                 Formula = new FormulaInfo { FormulaId = info.Field<int>("FormulaID") },
                                 PesoInicioMinimo = info.Field<int>("PesoInicioMinimo"),
                                 PesoInicioMaximo = info.Field<int>("PesoInicioMaximo"),
                                 TipoGanado = info.Field<string>("TipoGanado"),
                                 PesoSalida = info.Field<int>("PesoSalida"),
                                 FormulaSiguiente = new FormulaInfo { FormulaId = info.Field<int>("FormulaSiguienteID") },
                                 DiasEstanciaMinimo = info.Field<int>("DiasEstanciaMinimo"),
                                 DiasEstanciaMaximo = info.Field<int>("DiasEstanciaMaximo"),
                                 DiasTransicionMinimo = info.Field<int>("DiasTransicionMinimo"),
                                 DiasTransicionMaximo = info.Field<int>("DiasTransicionMaximo"),
                                 Disponibilidad = info.Field<bool>("Disponibilidad") == true ? Disponibilidad.Si : Disponibilidad.No
                             }).ToList();

                resultado = lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
