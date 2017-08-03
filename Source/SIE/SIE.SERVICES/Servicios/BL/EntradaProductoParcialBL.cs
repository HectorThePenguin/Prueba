
using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class EntradaProductoParcialBL
    {
        internal bool Crear(ContenedorEntradaMateriaPrimaInfo contenedorEntradaMateriaPrima)
        {
            bool regreso = true;
            try
            {
                var entradaProductoParcialDal = new EntradaProductoParcialDAL();
                regreso = entradaProductoParcialDal.Crear(contenedorEntradaMateriaPrima);
            }
            catch (ExcepcionGenerica)
            {
                regreso = false;
                throw;
            }
            catch (Exception ex)
            {
                regreso = false;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return regreso;
        }
    }
}
