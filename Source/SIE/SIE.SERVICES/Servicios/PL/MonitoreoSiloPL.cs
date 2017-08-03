using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class MonitoreoSiloPL
    {
        ///  Obtiene el estatus del almacen movimiento
        public int ObtenerTemperaturaMax(int organizacionID, string descripcion)
        {
            try
            {
                Logger.Info();
                var tempBL = new MonitoreoSiloBL();
                int info = tempBL.ObtenerTemperaturaMax(organizacionID, descripcion);
                
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

        ///  Obtiene el estatus del almacen movimiento
        public List<MonitoreoSiloInfo> ObtenerSilosParaMonitoreo(int organizacionID)
        {
            List<MonitoreoSiloInfo> info;
            try
            {
                Logger.Info();
                var siloBL = new MonitoreoSiloBL();
                info = siloBL.ObtenerSilosParaMonitoreo(organizacionID);
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

        ///  Obtiene altura y ubicacion de los sensores del grid
        public List<MonitoreoSiloInfo> ObtenerOrdenAlturaMonitoreoGrid(int organizacionID)
        {
            List<MonitoreoSiloInfo> info;
            try
            {
                Logger.Info();
                var siloBL = new MonitoreoSiloBL();
                info = siloBL.ObtenerOrdenAlturaMonitoreoGrid(organizacionID);
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

        //Guarda los datos del monitoreo de silos
        public bool Guardar(List<MonitoreoSiloInfo> listaMonitoreoSilo, MonitoreoSiloInfo silosInfo, int organizacionID)
        {
            try
            {
                Logger.Info();
                var siloBL = new MonitoreoSiloBL();
                bool result = siloBL.Guardar(listaMonitoreoSilo, silosInfo, organizacionID);
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
