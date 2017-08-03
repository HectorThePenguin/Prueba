using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class RecibirProductoAlmacenReplicaPL
    {
        public List<string> GuardarAretes(List<string> aretes, int organizacionId, int usuarioId )
        {
            try
            {
                Logger.Info();
                var bl = new RecibirProductoAlmacenReplicaBL();
                var lista = bl.GuardarAretes(aretes, organizacionId, usuarioId);

                return lista;
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

        public List<string> ConsultarAretes(List<string> aretes, int organizacionId)
        {
            try
            {
                Logger.Info();
                var bl = new RecibirProductoAlmacenReplicaBL();
                var lista = bl.ConsultarAretes(aretes, organizacionId);

                return lista;
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
