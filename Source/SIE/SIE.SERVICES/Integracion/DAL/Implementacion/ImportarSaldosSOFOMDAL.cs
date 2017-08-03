using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ImportarSaldosSOFOMDAL : DALBase
    {
        public List<ImportarSaldosSOFOMInfo> Guardar(List<ImportarSaldosSOFOMInfo> datos)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var result = new List<ImportarSaldosSOFOMInfo>();
                var xml =
                    new XElement("ROOT",
                                 from saldos in datos
                                 select new XElement("Saldos",
                                        new XElement("UsuarioCreacionID", saldos.UsuarioCreacionID),
                                        new XElement("CreditoID", saldos.CreditoID),
                                        new XElement("Nombre", saldos.Nombre),
                                        new XElement("TipoCredito", saldos.TipoCredito.Descripcion),
                                        new XElement("FechaAlta", Convert.ToDateTime(saldos.FechaAlta)),
                                        new XElement("FechaVencimiento", Convert.ToDateTime(saldos.FechaVencimiento)),
                                        new XElement("Saldo", saldos.Saldo)));
                
                parametros = new Dictionary<string, object>
                                 {
                                     {"@XmlSaldos", xml.ToString()},
                                 };

                var ds = Retrieve("ImportarSaldosSOFOM_Guardar", parametros);
                if (ValidateDataSet(ds))
                {
                    var map = new MapImportarSaldosSOFOMDAL();
                    result = map.ObtenerProveedor(ds);
                    if(result.Count <= 0)
                    {
                        throw new ExcepcionServicio("Error al guardar información.");
                    }
                }
                else
                {
                    throw new ExcepcionServicio("Error al guardar información.");
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

        public ImportarSaldosSOFOMInfo CreditoSOFOM_ObtenerPorID(int creditoID)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                                 {
                                     {"@CreditoID", creditoID},
                                 };

                var ds = Retrieve("CreditoSOFOM_ObtenerPorID", parametros);
                if (ValidateDataSet(ds))
                {
                    var map = new MapImportarSaldosSOFOMDAL();
                    return map.CreditoSOFOM_ObtenerPorID(ds);
                }
                else
                {
                    throw new ExcepcionServicio("Ocurrió un error al obtener el saldo del crédito.");
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
        }
    }
}
