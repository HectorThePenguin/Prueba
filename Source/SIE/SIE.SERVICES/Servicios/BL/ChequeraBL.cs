using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ChequeraBL
    {
        internal IList<ChequeraInfo> ObtenerTodos()
        {
            IList<ChequeraInfo> lista;
            try
            {
                Logger.Info();
                var chequeraDAL = new ChequeraDAL();
                lista = chequeraDAL.ObtenerTodos();
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

        internal IList<ChequeraInfo> ObtenerPorFiltro(ChequeraInfo info)
        {
            IList<ChequeraInfo> lista;
            try
            {
                Logger.Info();
                var chequeraDAL = new ChequeraDAL();
                lista = chequeraDAL.ObtenerPorFiltro(info);
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

        internal int Guardar(ChequeraInfo info)
        {
            try
            {
                Logger.Info();
                var chequeraDAL = new ChequeraDAL();
                return chequeraDAL.Guardar(info);
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

        internal ChequeraInfo ObtenerDetalleChequera(int chequera, int organizacion)
        {
            ChequeraInfo lista;
            try
            {
                Logger.Info();
                var chequeraDAL = new ChequeraDAL();
                lista = chequeraDAL.ObtenerDetalleChequera(chequera, organizacion);
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

        internal int ObtenerConsecutivo(int OrganizacionId)
        {
            int chequera;
            try
            {
                Logger.Info();
                var chequeraDAL = new ChequeraDAL();
                chequera = chequeraDAL.ObtenerConsecutivo(OrganizacionId);
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

            return chequera;
        }

        internal bool ValidarSiExisteChequeraActiva(int organizacionId, int estatusId)
        {
            try
            {
                Logger.Info();
                var chequeraDAL = new ChequeraDAL();
                return chequeraDAL.ValidarSiExisteChequeraActiva(organizacionId, estatusId);
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

        internal bool ValidarChequesGirados(int organizacionId, int chequeraId)
        {
            try
            {
                Logger.Info();
                var chequeraDAL = new ChequeraDAL();
                return chequeraDAL.ValidarChequesGirados(organizacionId, chequeraId);
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
