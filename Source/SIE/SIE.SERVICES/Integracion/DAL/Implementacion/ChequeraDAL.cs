using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Xml.Linq;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ChequeraDAL : DALBase
    {
        internal List<ChequeraInfo> ObtenerTodos()
        {
            List<ChequeraInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Chequera_ObtenerTodos");
                if (ValidateDataSet(ds))
                {
                    result = MapChequeraDAL.ObtenerTodos(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        internal List<ChequeraInfo> ObtenerPorFiltro(ChequeraInfo info)
        {
            List<ChequeraInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxChequeraDAL.ObtenerParametros(info);

                DataSet ds = Retrieve("Chequera_ObtenerPorFiltro", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapChequeraDAL.ObtenerTodos(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        internal int Guardar(ChequeraInfo info)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 new XElement("Chequera",
                                        new XElement("ChequeraId", info.ChequeraId),
                                        new XElement("NumeroChequera", info.NumeroChequera),
                                        new XElement("ChequeIDInicial", info.ChequeInicial),
                                        new XElement("ChequeIDFinal", info.ChequeFinal),
                                        new XElement("OrganizacionId", info.CentroAcopio.OrganizacionID),
                                        new XElement("BancoId", info.Banco.BancoID),
                                        new XElement("Activo", info.ChequeraEtapas.EtapaId),
                                        new XElement("UsuarioCreacionID", info.UsuarioCreacionID)));
          
                var parametros = new Dictionary<string, object>
                                 {
                                     {"@XmlChequera", xml.ToString()},
                                 };

                DataSet ds = Retrieve("Chequera_Guardar", parametros);
                int result = 0;
                if (ValidateDataSet(ds))
                {
                    var folio = MapChequeraDAL.ObtenerFolio(ds);
                    if( folio > 0)
                    {
                        result = folio;
                    }
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal ChequeraInfo ObtenerDetalleChequera(int chequera, int organizacion)
        {
            ChequeraInfo result = null;
            try
            {
                Logger.Info();
                var parameters = AuxChequeraEtapasDAL.ObtenerParametros(chequera, organizacion);
                DataSet ds = Retrieve("Chequera_ObtenerDetalle", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapChequeraDAL.ObtenerPorChequera(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        internal int ObtenerConsecutivo(int OrganizacionId)
        {
            try
            {
                int result = 0;
                Logger.Info();
                var parameters = AuxChequeraDAL.ObtenerParametrosConsecutivo(OrganizacionId);
                var ds = Retrieve("Chequera_ObtenerConsecutivo", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapChequeraDAL.ObtenerConsecutivo(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal bool ValidarSiExisteChequeraActiva(int organizacion, int estatusId)
        {
            try
            {
                var result = false;
                Logger.Info();
                var parameters = AuxChequeraEtapasDAL.ObtenerParametrosExisteChequera(organizacion, estatusId);
                var ds = Retrieve("Chequera_ExisteChequera", parameters);
                if (ValidateDataSet(ds))
                {
                    result = true;
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal bool ValidarChequesGirados(int organizacion, int chequeraId)
        {
            try
            {
                var result = false;
                Logger.Info();
                var parameters = AuxChequeraEtapasDAL.ObtenerParametrosValidarChequesGirados(organizacion, chequeraId);
                var ds = Retrieve("Chequera_ValidarChequesGirados", parameters);
                if (ValidateDataSet(ds))
                {
                    result = true;
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
