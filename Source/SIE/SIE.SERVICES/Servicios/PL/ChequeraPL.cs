using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class ChequeraPL
    {
        public IList<ChequeraInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var chequeraBL = new ChequeraBL();
                var lista = chequeraBL.ObtenerTodos();

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

        public IList<ChequeraInfo> ObtenerPorFiltro(ChequeraInfo info)
        {
            try
            {
                Logger.Info();
                var chequeraBL = new ChequeraBL();
                var lista = chequeraBL.ObtenerPorFiltro(info);

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

        public int Guardar(ChequeraInfo info)
        {
            try
            {
                Logger.Info();
                var chequeraBL = new ChequeraBL();
                return chequeraBL.Guardar(info);
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

        public ChequeraInfo ObtenerDetalleChequera(int chequera, int organizacion)
        {
            try
            {
                Logger.Info();
                var chequeraBL = new ChequeraBL();
                var lista = chequeraBL.ObtenerDetalleChequera(chequera, organizacion);

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

        public int ObtenerConsecutivo(int OrganizacionId)
        {
            try
            {
                Logger.Info();
                var chequeraBL = new ChequeraBL();
                var lista = chequeraBL.ObtenerConsecutivo(OrganizacionId);

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

        public bool ValidarSiExisteChequeraActiva(int organizacionId, int estatusId)
        {
            try
            {
                Logger.Info();
                var chequeraBL = new ChequeraBL();
                var result = chequeraBL.ValidarSiExisteChequeraActiva(organizacionId, estatusId);

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

        public bool ValidarChequesGirados(int organizacionId, int chequeraId)
        {
            try
            {
                Logger.Info();
                var chequeraBL = new ChequeraBL();
                var result = chequeraBL.ValidarChequesGirados(organizacionId, chequeraId);

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
    }
}
