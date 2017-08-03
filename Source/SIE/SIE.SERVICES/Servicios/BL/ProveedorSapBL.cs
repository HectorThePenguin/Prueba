using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SAP.Middleware.Connector;
using System.Configuration;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    public class ProveedorSapBL
    {
        private readonly RfcDestination _destino;

        public ProveedorSapBL()
        {
            var parametros = new RfcConfigParameters();
            parametros.Add(RfcConfigParameters.Name, ConfigurationManager.AppSettings["Name"]);
            parametros.Add(RfcConfigParameters.User, ConfigurationManager.AppSettings["User"]);
            parametros.Add(RfcConfigParameters.Password, ConfigurationManager.AppSettings["Password"]);

            parametros.Add(RfcConfigParameters.Client, ConfigurationManager.AppSettings["Client"]);
            parametros.Add(RfcConfigParameters.Language, ConfigurationManager.AppSettings["Language"]);
            parametros.Add(RfcConfigParameters.AppServerHost, ConfigurationManager.AppSettings["AppServerHost"]);
            parametros.Add(RfcConfigParameters.SystemNumber, ConfigurationManager.AppSettings["SystemNumber"]);
            parametros.Add(RfcConfigParameters.PoolSize, ConfigurationManager.AppSettings["PoolSize"]);

            _destino = RfcDestinationManager.GetDestination(parametros);
        }

        public bool Ping()
        {
            try
            {
                _destino.Ping();
                return true;
            }
            catch (Exception)
            {
                // Si el ping falla es porque no hubo conexión con el destino
                return false;
            }
        }

        public DataTable ConsultarProveedorSAP(string IdProveedor, string Sociedad)
        {
            DataTable resultado = new DataTable();
            try
            {
                // Objecto del RFC en SAP
                IRfcFunction func = _destino.Repository.CreateFunction("Z_FI_PROVEEDORES_SIAP");
                // Estructura definida en SAP
                //IRfcStructure structInputs = _destino.Repository.GetStructureMetadata("ZMXEA_S_UUID").CreateStructure();
                //parametros de entrada
                func.SetValue("COD_PROV", IdProveedor);
                func.SetValue("SOCIEDAD", Sociedad);

                //resultado
                var x = func.GetTable("ITAB");

                //Ejecutar
                func.Invoke(_destino);


                resultado.Columns.Add("ProveedorID", Type.GetType("System.String"));
                resultado.Columns.Add("Descripcion", Type.GetType("System.String"));
                resultado.Columns.Add("GrupoID", Type.GetType("System.String"));
                resultado.Columns.Add("Bloqueado", Type.GetType("System.String"));
                    
                foreach (IRfcStructure row in x)
                {
                    DataRow ac = resultado.NewRow();
                    ac["ProveedorID"] = row.GetValue("LIFNR");
                    ac["Descripcion"] = row.GetValue("NAME1").ToString() + row.GetValue("NAME2").ToString();
                    ac["GrupoID"] = row.GetValue("KTOKK");
                    ac["Bloqueado"] = row.GetValue("SPERR");

                    resultado.Rows.Add(ac);
                    
                }

            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                //throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        //public static DataTable CreateDataTable(Type prov)
        //{
        //    DataTable return_Datatable = new DataTable();
        //    foreach (PropertyInfo info in ProveedorInfo.GetProperties())
        //    {
        //        return_Datatable.Columns.Add(new DataColumn(info.Name, info.PropertyType));
        //    }
        //    return return_Datatable;
        //}
    }
}
