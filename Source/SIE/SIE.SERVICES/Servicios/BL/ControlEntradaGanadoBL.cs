using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    class ControlEntradaGanadoBL
    {
        /// <summary>
        /// Metodo para validar si el animal Tiene AnimalCosto
        /// </summary>
        /// <param name="animalInactivo"></param>
        /// <param name="costoGanado"></param>
        /// <returns></returns>
        public List<ControlEntradaGanadoInfo> ObtenerControlEntradaGanadoPorID(int animalID, int entradaGanadoID)
        {
            var controlEntradaGanadoInfo = new List<ControlEntradaGanadoInfo>();
            try
            {
                Logger.Info();
                var controlEntradaGanadoDAl = new ControlEntradaGanadoDAL();
                controlEntradaGanadoInfo = controlEntradaGanadoDAl.ObtenerControlEntradaGanadoPorID(animalID, entradaGanadoID);
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
            return controlEntradaGanadoInfo;
        }

        /// <summary>
        /// Metodo para validar si el animal Tiene AnimalCosto
        /// </summary>
        /// <param name="animalInactivo"></param>
        /// <param name="costoGanado"></param>
        /// <returns></returns>
        public bool GuardarControlEntradaGanado(ControlEntradaGanadoInfo controlEntradaGanado)
        {
            bool result;
            try
            {
                Logger.Info();
                var controlEntradaGanadoDAl = new ControlEntradaGanadoDAL();
                result = controlEntradaGanadoDAl.GuardarControlEntradaGanado(controlEntradaGanado);
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

        /// <summary>
        /// Metodo para validar si el animal Tiene AnimalCosto
        /// </summary>
        /// <param name="animalInactivo"></param>
        /// <param name="costoGanado"></param>
        /// <returns></returns>
        public bool EliminaControlEntradaGanado(int EntradaGanadoID)
        {
            bool result;
            try
            {
                Logger.Info();
                var controlEntradaGanadoDAl = new ControlEntradaGanadoDAL();
                result = controlEntradaGanadoDAl.EliminaControlEntradaGanado(EntradaGanadoID);
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
