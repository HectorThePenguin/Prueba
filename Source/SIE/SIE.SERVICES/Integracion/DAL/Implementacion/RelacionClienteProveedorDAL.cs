using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class RelacionClienteProveedorDAL :  DALBase
    {
        internal IList<RelacionClienteProveedorInfo> ObtenerPorProveedorID(int proveedorID, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRelacionClienteProveedorDAL.ObtenerPorProveedorID(proveedorID, organizacionID);
                DataSet ds = Retrieve("CreditoProveedor_ObtenerPorProveedorID", parameters);
                var result = new List<RelacionClienteProveedorInfo>();
                if(ValidateDataSet(ds))
                {
                    result = MapRelacionClienteProveedorDAL.ObtenerPorProveedorID(ds);
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

        internal bool Guardar(List<RelacionClienteProveedorInfo> creditos)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from credito in creditos
                                 select new XElement("Creditos",
                                        new XElement("CreditoID", credito.CreditoID),
                                        new XElement("OrganizacionID", credito.Centro.OrganizacionID),
                                        new XElement("ProveedorID", credito.Proveedor.ProveedorID),
                                        new XElement("UsuarioCreacionID", credito.UsuarioCreacionID)));
                
                var parametros = new Dictionary<string, object>
                                 {
                                     {"@Xml", xml.ToString()},
                                 };

                Create("RelacionClienteProveedor_Guardar", parametros);
                
                return true;            
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
