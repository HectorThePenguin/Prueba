using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Servicios.BL;
using SIE.Base.Exepciones;
using System.Reflection;
namespace SIE.Services.Servicios.PL
{
    public class DeteccionPL
    {

        public int GuardarDeteccion(DeteccionInfo deteccionGrabar, FlagCargaInicial esCargaInicial, AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var deteccionBl = new DeteccionBL();
                int result = deteccionBl.Guardar(deteccionGrabar, esCargaInicial, animal);
                return result;
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
        }

        /// <summary>
        /// Actualizar deteccion de ganado con el arete ingresado y la foto especificada
        /// </summary>
        /// <param name="animalDetectado"></param>
        public bool ActualizarDeteccionAreteConFoto(AnimalDeteccionInfo animalDetectado)
        {
            bool retValue = false;
            try
            {
                Logger.Info();
                var deteccionBl = new DeteccionBL();
                retValue = deteccionBl.ActualizarDeteccionConFoto(animalDetectado);

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

            return retValue;
        }
    }
}
