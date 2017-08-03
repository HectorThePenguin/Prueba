using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Servicios.BL
{
    public class ReporteEstadoComederoBL
    {
        public AlimentacionEstadoComederoModel GenerarSegundoReporte(int organizacionID)
        {
            try
            {
                var modelo = new SIE.Services.Info.Modelos.AlimentacionEstadoComederoModel();

                var dal = new SIE.Services.Integracion.DAL.Implementacion.ReporteEstadoComederoDAL();
                modelo.CorralesPorFormula = new System.Collections.ObjectModel.ObservableCollection<Info.Modelos.AlimentacionCorralPorFormulaModel>(dal.ObtenerCorralesPorFormula(organizacionID));
                modelo.CorralesPorEstadoComedero = new System.Collections.ObjectModel.ObservableCollection<Info.Modelos.AlimentacionCorralPorEstadoComederoModel>(dal.ObtenerCorralesPorEstadoComedero(organizacionID));
                
                return modelo;
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        public SIE.Services.Info.Modelos.AlimentacionEstadoComederoModel GenerarReporteDetallado(int organizacionID)
        {
            var modelo = new SIE.Services.Info.Modelos.AlimentacionEstadoComederoModel();
            try
            {
                
                var dal = new SIE.Services.Integracion.DAL.Implementacion.ReporteEstadoComederoDAL();
                modelo = dal.Generar(organizacionID);

            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return modelo;
        }
        class Agrupador
        {
            public int LoteID { get; set; }
            public int Dias { get; set; }
            public int Valor { get; set; }
        }
    }
}
