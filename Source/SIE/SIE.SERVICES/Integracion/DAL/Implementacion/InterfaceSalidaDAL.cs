using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Data;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class InterfaceSalidaDAL : DALBase
    {
        /// <summary>
        ///     Metodo que crear una interfaceSalida
        /// </summary>
        /// <param name="interfaceSalidaInfo"></param>
        internal void Crear(InterfaceSalidaInfo interfaceSalidaInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaDAL.ObtenerParametrosGuardado(interfaceSalidaInfo);
                Create("InterfaceSalida_Crear", parameters);
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

        /// <summary>
        ///     Obtiene una Interface de Salida por Id
        /// </summary>
        /// <param name="salidaID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal InterfaceSalidaInfo ObtenerPorID(int salidaID, int organizacionID)
        {
            InterfaceSalidaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaDAL.ObtenerParametrosPorID(salidaID, organizacionID);
                DataSet ds = Retrieve("[dbo].[InterfaceSalida_ObtenerPorID]",parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaDAL.ObtenerParametrosPorID(ds);
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

        /// <summary>
        ///     Obtiene una Lista de Interface de Salida
        /// </summary>
        /// <returns></returns>
        internal IList<InterfaceSalidaInfo> ObtenerTodos()
        {
            List<InterfaceSalidaInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("[dbo].[InterfaceSalida_ObtenerTodos]");
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaDAL.ObtenerTodos(ds);
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

        /// <summary>
        ///     Obtiene una Interface de Salida por SalidaID y OrganizacionID
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal InterfaceSalidaInfo ObtenerPorSalidaOrganizacion(EntradaGanadoInfo entradaGanado)
        {
            InterfaceSalidaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaDAL.ObtenerParametrosPorSalidaOrganizacion(entradaGanado);
                DataSet ds = Retrieve("[dbo].[InterfaceSalida_ObtenerPorSalidaOrganizacion]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaDAL.ObtenerPorSalidaOrganizacion(ds);
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

        /// <summary>
        ///     Obtiene una Interface de Salida por SalidaID y OrganizacionID
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaInfo> ObtenerPorEmbarqueID(EntradaGanadoInfo entradaGanado)
        {
            List<InterfaceSalidaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaDAL.ObtenerParametrosPorEmbarqueID(entradaGanado);
                DataSet ds = Retrieve("[dbo].[InsterfaceSalida_ObtenerPorEmbarqueID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaDAL.ObtenerPorEmbarqueID(ds);
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

        /// <summary>
        ///     Obtiene una Interface de Salida por SalidaID y OrganizacionID
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaInfo> ObtenerPorEmbarqueIDConCompraDirecta(EntradaGanadoInfo entradaGanado)
        {
            List<InterfaceSalidaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaDAL.ObtenerParametrosPorEmbarqueID(entradaGanado);
                DataSet ds = Retrieve("[dbo].[InterfaceSalida_ObtenerPorEmbarqueIDConCompraDirecta]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapInterfaceSalidaDAL.ObtenerPorEmbarqueID(ds);
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

        /// <summary>
        ///     Obtiene una Interface de Salida por Id
        /// </summary>
        /// <param name="salidaID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="organizacionOrigenID"></param>
        /// <returns></returns>
        internal int ObtenerPorEmbarque(int salidaID, int organizacionID, int organizacionOrigenID)
        {
            int result;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxInterfaceSalidaDAL.ObtenerParametrosPorEmbarque(salidaID, organizacionID, organizacionOrigenID);
                result = RetrieveValue<int>("[dbo].[InterfaceSalida_ObtenerPorEmbarque]", parameters);                
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
    }
}
