using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SAP.Middleware.Connector;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    public class InterfaceSAPBL
    {
         private readonly RfcDestination destino;

         public InterfaceSAPBL()
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

            destino = RfcDestinationManager.GetDestination(parametros);
        }

        public bool Ping()
        {
            try
            {
                destino.Ping();
                return true;
            }
            catch (Exception)
            {
                // Si el ping falla es porque no hubo conexión con el destino
                return false;
            }
        }

        internal CuentaSAPInfo ObtenerCuentaSAP(CuentaSAPInfo cuentaSAPInfo)
        {
            var cuentaSAP = new CuentaSAPInfo();

            try
            {
                Logger.Info();
                // Nombre de la Función RFC
                IRfcFunction func = destino.Repository.CreateFunction("Z_FI_CUENTA_SIAP");

                //parametros de entrada (Import)
                func.SetValue("COD_CUENTA", cuentaSAPInfo.CuentaSAP);
                func.SetValue("SOCIEDAD", cuentaSAPInfo.Sociedad);
                func.SetValue("PLAN_CUENTA", cuentaSAPInfo.PlanCuenta);

                //resultado (Changin)
                var tablaResultado = func.GetTable("ITAB");

                //Ejecutar
                func.Invoke(destino);

                var listaCuentas = new List<CuentaSAPInfo>();
                    
                foreach (IRfcStructure row in tablaResultado)
                {
                    var cuenta = new CuentaSAPInfo();

                    cuenta.Sociedad = row.GetValue("BUKRS") as string ?? string.Empty;
                    cuenta.CuentaSAP = row.GetValue("SAKNR") as string ?? string.Empty;
                    cuenta.Descripcion = row.GetValue("TXT50") as string ?? string.Empty;
                    string bloqueo = row.GetValue("XSPEB") as string ?? string.Empty;
                    cuenta.Bloqueada = bloqueo.Trim().ToUpper().Equals("X");
                    cuenta.TipoCuenta = new TipoCuentaInfo();
                    cuenta.UsuarioCreacionID = cuentaSAPInfo.UsuarioCreacionID;
                    listaCuentas.Add(cuenta);
                }
                cuentaSAP = listaCuentas.FirstOrDefault();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return cuentaSAP;
        }

        internal ClienteInfo ObtenerClienteSAP(ClienteInfo clienteInfo)
        {
            var clienteSAP = new ClienteInfo();

            try
            {
                Logger.Info();
                // Nombre de la Función RFC
                IRfcFunction func = destino.Repository.CreateFunction("Z_FI_CLIENTE_SIAP");

                //parametros de entrada (Import)
                func.SetValue("COD_CLIENTE", clienteInfo.CodigoSAP);
                func.SetValue("SOCIEDAD", clienteInfo.Sociedad);

                //resultado (Changin)
                var tablaResultado = func.GetTable("ITAB");

                //Ejecutar
                func.Invoke(destino);

                var listaClientes = new List<ClienteInfo>();

                foreach (IRfcStructure row in tablaResultado)
                {
                    var cliente = new ClienteInfo();

                    cliente.Sociedad = row.GetValue("BUKRS") as string ?? string.Empty;
                    cliente.CodigoSAP = row.GetValue("KUNNR") as string ?? string.Empty;
                    cliente.Descripcion = row.GetValue("NAME1") as string ?? string.Empty;
                    cliente.Poblacion = row.GetValue("ORT01") as string ?? string.Empty;
                    cliente.Estado = row.GetValue("REGIO") as string ?? string.Empty;
                    cliente.Pais = row.GetValue("LAND1") as string ?? string.Empty;
                    cliente.Calle = row.GetValue("STRAS") as string ?? string.Empty;
                    cliente.CodigoPostal = row.GetValue("PSTLZ") as string ?? string.Empty;
                    cliente.RFC = row.GetValue("STCD1") as string ?? string.Empty;
                    cliente.CondicionPago = 1;//row.GetValue("ZTERM") as string ?? string.Empty;

                    string bloqueo = row.GetValue("SPERR") as string ?? string.Empty;
                    cliente.Bloqueado = bloqueo.Trim().ToUpper().Equals("X");
                    cliente.MetodoPago = new MetodoPagoInfo();
                    cliente.UsuarioCreacionID = clienteInfo.UsuarioCreacionID;
                    listaClientes.Add(cliente);
                }
                clienteSAP = listaClientes.FirstOrDefault();
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return clienteSAP;
        }
    }
}
