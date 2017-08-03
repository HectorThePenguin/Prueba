using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Servicios.PL
{
    public class ConfiguracionFormulaPL
    {
		/// <summary>
        /// Obtener la configuracion de las formulas para una organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public IList<ConfiguracionFormulaInfo> ObtenerConfiguracionFormula(int organizacionID)
        {
            IList<ConfiguracionFormulaInfo> lista;
            try
            {
                Logger.Info();
                var configuracionFormulaBL = new ConfiguracionFormulaBL();
                lista = configuracionFormulaBL.ObtenerConfiguracionFormula(organizacionID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
        /// <summary>
        /// Metodo para importar archivo 
        /// </summary>
        /// <param name="configuracionImportar"></param>
        /// <returns></returns>
        public bool ImportarArchivo(ConfiguracionFormulaInfo configuracionImportar)
        {
            bool importarArchivo;
            try
            {
                Logger.Info();
                var configuracionFormulaBL = new ConfiguracionFormulaBL();
                importarArchivo = configuracionFormulaBL.ImportarArchivo(configuracionImportar);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return importarArchivo;
        }

        /// <summary>
        /// Metodo para exportar la configuracion de formulas de una organizacion
        /// </summary>
        /// <param name="configuracionImportar"></param>
        /// <returns></returns>
        public bool ExportarArchivo(ConfiguracionFormulaInfo configuracionImportar)
        {
            bool exportarArchivo;
            try
            {
                Logger.Info();
                var configuracionFormulaBL = new ConfiguracionFormulaBL();
                exportarArchivo = configuracionFormulaBL.ExportarArchivo(configuracionImportar);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return exportarArchivo;
        }
        /// <summary>
        /// Consulta la configuracion de la formula del tipo ganado
        /// </summary>
        /// <param name="tipoGanadoIn"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<ConfiguracionFormulaInfo> ObtenerFormulaPorTipoGanado(TipoGanadoInfo tipoGanadoIn, int organizacionID)
        {
            List<ConfiguracionFormulaInfo> result;
            try
            {
                Logger.Info();
                var configuracionFormulaBL = new ConfiguracionFormulaBL();
                result = configuracionFormulaBL.ObtenerFormulaPorTipoGanado(tipoGanadoIn, organizacionID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
