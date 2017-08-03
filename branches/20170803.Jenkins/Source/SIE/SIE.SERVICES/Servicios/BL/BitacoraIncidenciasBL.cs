using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class BitacoraIncidenciasBL
    {
        /// <summary>
        /// Guradar un error en la bitacora
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        internal int GuardarError(BitacoraErroresInfo errorInfo)
        {
            try
            {
                Logger.Info();
                var bitacoraErrores = new BitacoraErroresDAL();
                return bitacoraErrores.GuardarError(errorInfo);
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

        internal int GuardarErrorIncidencia(BitacoraIncidenciaInfo bitacora)
        {
            try
            {
                Logger.Info();
                var bitacoraErrores = new BitacoraErroresDAL();
                return bitacoraErrores.GuardarErrorIncidencia(bitacora);
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
