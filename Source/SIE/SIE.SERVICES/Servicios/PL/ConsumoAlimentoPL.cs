using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using System;
using System.Reflection;

namespace SIE.Services.Servicios.PL
{
    public class ConsumoAlimentoPL
    {

        /// <summary>
        /// Funcion principal que genera el consumo de alimento
        /// </summary>
        /// <returns>Booleano que regresa si se hizo correctamente</returns>
        public void GenerarConsumoAlimento(int organizacionID, DateTime fecha)
        {
            try
            {
                Logger.Info();
                var consumoAlimentoBL = new ConsumoAlimentoBL();
                consumoAlimentoBL.GenerarConsumoAlimento(organizacionID,fecha);
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
    }
}
