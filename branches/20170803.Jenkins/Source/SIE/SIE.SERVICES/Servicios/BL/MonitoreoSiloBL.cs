using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System.Transactions;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class MonitoreoSiloBL
    {
        /// Obtiene la temperatura maxima
        public int ObtenerTemperaturaMax(int organizacionID, string descripcion)
        {
            try
            {
                Logger.Info();
                var tempDAL = new MonitoreoSiloDAL();
                int info = tempDAL.ObtenerTemperaturaMax(organizacionID, descripcion);
                
                return info;
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

        /// Obtiene los Estatus dados de alta
        public List<MonitoreoSiloInfo> ObtenerSilosParaMonitoreo(int organizacionID)
        {
            List<MonitoreoSiloInfo> info;
            try
            {
                Logger.Info();
                var siloDAL = new MonitoreoSiloDAL();
                info = siloDAL.ObtenerSilosParaMonitoreo(organizacionID);
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
            return info;
        }

        /// Obtiene los Estatus dados de alta
        public List<MonitoreoSiloInfo> ObtenerOrdenAlturaMonitoreoGrid(int organizacionID)
        {
            List<MonitoreoSiloInfo> info;
            try
            {
                Logger.Info();
                var siloDAL = new MonitoreoSiloDAL();
                info = siloDAL.ObtenerOrdenAlturaMonitoreoGrid(organizacionID);
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
            return info;
        }

        //Guarda los datos para una lista de Analisis Grasos
        internal bool Guardar(List<MonitoreoSiloInfo> listaMonitoreoSilo, MonitoreoSiloInfo silosInfo, int organizacionID)
        {
            try
            {
                //int result = 0;
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    var silosDAL = new MonitoreoSiloDAL();
                    bool resultado = silosDAL.Guardar(listaMonitoreoSilo, silosInfo, organizacionID);

                    transaction.Complete();
                }
                return true;
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
