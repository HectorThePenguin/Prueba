using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Services.Info.Enums;
namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxConfiguracionFormulaDAL
    {
		/// <summary>
        /// Obtener los parametros para la configuracion de las formulas para una organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametroOrganizacion(int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        public static Dictionary<string, object> ObtenerParametroInactivar(ConfiguracionFormulaInfo configuracionImportar)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", configuracionImportar.OrganizacionID},
                                     {"@UsuarioModificacionID", configuracionImportar.UsuarioCreacionID},
                                     {"@Activo", configuracionImportar.Activo}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametroGuardar(ConfiguracionFormulaInfo configuracion, 
                                                                           ConfiguracionFormulaInfo configuracionImportar)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", configuracionImportar.OrganizacionID},
                                     {"@FormulaID", configuracion.Formula.FormulaId},
			                         {"@PesoInicioMinimo", configuracion.PesoInicioMinimo},
			                         {"@PesoInicioMaximo", configuracion.PesoInicioMaximo},
			                         {"@TipoGanado", configuracion.TipoGanado},
			                         {"@PesoSalida", configuracion.PesoSalida},
			                         {"@FormulaSiguienteID", configuracion.FormulaSiguiente.FormulaId},
			                         {"@DiasEstanciaMinimo", configuracion.DiasEstanciaMinimo},
			                         {"@DiasEstanciaMaximo", configuracion.DiasEstanciaMaximo},
			                         {"@DiasTransicionMinimo", configuracion.DiasTransicionMinimo},
			                         {"@DiasTransicionMaximo", configuracion.DiasTransicionMaximo},
			                         {"@Disponibilidad", configuracion.Disponibilidad},
                                     {"@Activo", EstatusEnum.Activo},
                                     {"@UsuarioCreacionID", configuracionImportar.UsuarioCreacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosFormulaPorTipoGanado(TipoGanadoInfo tipoGanadoIn,int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionID", organizacionID},
                    {"@TipoGanadoID", tipoGanadoIn.TipoGanadoID}
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
