using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class RecibirProductoAlmacenReplicaBL
    {
        internal List<string> GuardarAretes(List<string> aretes, int organizacionId, int usuarioId )
        {
            List<String> lista;
            try
            {
                Logger.Info();
                var dal = new RecibirProductoAlmacenReplicaDAL();
                lista = dal.GuardarAretes(aretes, organizacionId, usuarioId);

                if(lista.Count > 0)
                {
                    foreach (var a in lista)
                    {
                        aretes.Remove(a);
                    }
                    lista = aretes;
                }
                
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

        internal List<string> ConsultarAretes(List<string> aretes, int organizacionId)
        {
            List<String> lista;
            try
            {
                Logger.Info();
                var dal = new RecibirProductoAlmacenReplicaDAL();
                lista = dal.ConsultarAretes(aretes, organizacionId);

                if (lista.Count > 0)
                {
                    foreach (var a in lista)
                    {
                        aretes.Remove(a);
                    }
                    lista = aretes;
                }

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
    }
}
