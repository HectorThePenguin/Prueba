using System;
using System.Reflection;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class AnalisisGrasaPL
    {
        //Guarda los datos para un lista con Analisis de Grasas
        public bool Guardar(List<AnalisisGrasaInfo> listaAnalisisGrasaInfo)
        {
            try
            {
                Logger.Info();
                var analisisGrasaBl = new AnalisisGrasaBL();
                analisisGrasaBl.Guardar(listaAnalisisGrasaInfo);
                return true;
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
