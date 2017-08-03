using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    public class ColorObjetivoBL
    {
        public string ObtenerSemaforo()
        {
            try
            {
                Logger.Info();
                var colorObjetivoDAL = new ColorObjetivoDAL();
                List<ColorObjetivoInfo> result = colorObjetivoDAL.ObtenerSemaforo();
                string configuracionSemaforo = string.Empty;
                if (result != null)
                {
                    configuracionSemaforo = ObtenerConfiguracionSemaforo(result);
                }
                return configuracionSemaforo;
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

        private string ObtenerConfiguracionSemaforo(List<ColorObjetivoInfo> colores)
        {
            var SB = new StringBuilder();
            for (int indexColores = 0; indexColores < colores.Count; indexColores++)
            {
                SB.Append(".");
                 switch ((TipoObjetivoCalidad) colores[indexColores].TipoObjetivoCalidad.TipoObjetivoCalidadID)
                 {
                     case TipoObjetivoCalidad.Optimo:
                         SB.Append(TipoObjetivoCalidad.Optimo.ToString());
                         break;
                     case TipoObjetivoCalidad.Maximo:
                         SB.Append(TipoObjetivoCalidad.Maximo.ToString());
                         break;
                     case TipoObjetivoCalidad.Minimo:
                         SB.Append(TipoObjetivoCalidad.Minimo.ToString());
                         break;
                     case TipoObjetivoCalidad.Rango:
                         SB.Append(TipoObjetivoCalidad.Rango.ToString());
                         break;
                 }
                var tendencia = colores[indexColores].Tendencia;
                switch (tendencia)
                {
                    case ">":
                        SB.Append("Mayor");
                        break;
                    case "<":
                        SB.Append("Menor");
                        break;
                    case "=":
                        SB.Append("Igual");
                        break;
                    case ">=":
                        SB.Append("MayorIgual");
                        break;
                    case "<=":
                        SB.Append("MenorIgual");
                        break;
                }
                SB.Append("{").AppendLine();
                SB.Append("background: none repeat scroll 0 0 ").Append(colores[indexColores].CodigoColor).
                    Append(" !important;").AppendLine();
                SB.Append("border-radius: 50%  !important;").AppendLine();
                SB.Append("width: 20px  !important;").AppendLine();
                SB.Append("height: 20px  !important; ").AppendLine();
                SB.Append("margin-top: 7px !important; ").AppendLine();
                SB.Append("margin-left: 5px !important; ").AppendLine().Append("}");
                SB.AppendLine();

                SB.Append(".deshabilitado {").AppendLine();
                SB.Append("background: none repeat scroll 0 0 #B4B4B4 !important;").AppendLine();
                SB.Append("border-radius: 50%  !important;").AppendLine();
                SB.Append("width: 20px  !important;").AppendLine();
                SB.Append("height: 20px  !important; ").AppendLine();
                SB.Append("margin-top: 7px !important; ").AppendLine();
                SB.Append("margin-left: 5px !important; ").AppendLine().Append("}");
                SB.AppendLine();
            }
            return SB.ToString();
        }
    }
}
