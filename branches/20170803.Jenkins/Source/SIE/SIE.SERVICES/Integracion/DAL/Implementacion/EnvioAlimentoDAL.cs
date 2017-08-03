using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    class EnvioAlimentoDAL : DALBase
    {
        internal long RegistrarRecepcionProductoEnc(EnvioAlimentoInfo envio,TipoMovimiento tipo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEnvioAlimentoDAL.ObtenerParametrosRegistrarRecepcionProductoEnc(envio,tipo);
                int registrosAfectados = RetrieveValue<int>("EnvioAlimento_RegistrarRecepcionProductoEnc", parameters);
                return registrosAfectados;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal long RegistrarRecepcionProductoDet(EnvioAlimentoInfo envio, TipoMovimiento tipo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEnvioAlimentoDAL.ObtenerParametrosRegistrarRecepcionProductoDet(envio,tipo);
                int registrosAfectados = RetrieveValue<int>("EnvioAlimento_RegistrarRecepcionProductoDet", parameters);
                return registrosAfectados;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal EnvioAlimentoInfo ObtenerPorId(EnvioAlimentoInfo envio)
        {
            EnvioAlimentoInfo envioConfirmacion = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEnvioAlimentoDAL.ObtenerParametrosBuscarEnvioPorId(envio);
                DataSet datos = Retrieve("EnvioAlimento_ObtenerPorId", parameters);
                if (ValidateDataSet(datos))
                {
                    envioConfirmacion = MapEnvioAlimentoDAL.RegistrarEnvioAlimento(datos);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return envioConfirmacion;
        }
       
        internal EnvioAlimentoInfo RegistrarEnvioAlimento(EnvioAlimentoInfo envio)
        {
            EnvioAlimentoInfo envioConfirmacion = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEnvioAlimentoDAL.ObtenerParametrosRegistrarEnvioAlimento(envio);
                DataSet datos = Retrieve("EnvioAlimento_RegistrarEnvioAlimento", parameters);
                if (ValidateDataSet(datos))
                {
                    envioConfirmacion = MapEnvioAlimentoDAL.RegistrarEnvioAlimento(datos);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return envioConfirmacion;
        }
    }
}
