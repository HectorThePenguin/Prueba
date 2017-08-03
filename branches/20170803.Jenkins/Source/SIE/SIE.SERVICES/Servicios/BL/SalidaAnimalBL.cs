using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class SalidaAnimalBL
    {
        /// <summary>
        /// Metodo para almacenar el animal en Salida Animal
        /// </summary>
        /// <param name="salidaAnimalInfo"></param>
        /// <returns></returns>
        internal bool Guardar(SalidaAnimalInfo salidaAnimalInfo)
        {
            bool result;
            try
            {
                Logger.Info();
                var salidaAnimaDAL = new SalidaAnimaDAL();
                result = salidaAnimaDAL.GuardarAnimal(salidaAnimalInfo);

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
