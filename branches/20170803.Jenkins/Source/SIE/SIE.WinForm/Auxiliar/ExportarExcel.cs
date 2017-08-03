using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SIE.WinForm.Auxiliar
{
    /// <summary>
    /// Class for generator of Excel file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExportarExcel<T> : ExportarExcelBase where T : class
    {
        public ExportarExcel()
            : base()
        {

        }

        public IList<T> Datos { private get; set; }
        public IList<T> Totales { private get; set; }

        protected override object[] ObtenerPropiedades()
        {
            Type ignorable = typeof(SIE.Services.Info.Atributos.AtributoIgnorarColumnaExcel);
            PropertyInfo[] propiedades = typeof(T).GetProperties().Where(e => e.GetCustomAttributes(ignorable, true).Count() == 0).ToArray();
            return propiedades.Select(t => t.Name).Cast<object>().ToArray();
        }

        protected override void EscribirDatos(object[] propiedades)
        {
            if (Datos == null)
            {
                return;
            }
            var datos = new object[Datos.Count, propiedades.Length];
            for (int j = 0; j < Datos.Count; j++)
            {
                T item = Datos[j];
                for (int i = 0; i < propiedades.Length; i++)
                {
                    object y = typeof(T).InvokeMember(propiedades[i].ToString(),
                                                       BindingFlags.GetProperty, null, item, null);
                    datos[j, i] = (y == null) ? "" : y;
                }
            }
            AgregarRenglon("A5", Datos.Count, propiedades.Length, datos);
            AjustarColumnas("A4", Datos.Count + 1, propiedades.Length);
            rango.BorderAround(XlLineStyle.xlContinuous);
            //se establece el borde del encabezado
            rango = hoja.Range["A1", valorOpcional];
            rango = rango.Resize[3, propiedades.Length];
            rango.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium);
        }

        protected override void EscribirTotales(object[] propiedades)
        {
            if (Totales == null)
            {
                return;
            }
            var totales = new object[Totales.Count, propiedades.Length];
            for (int j = 0; j < Totales.Count; j++)
            {
                T item = Totales[j];
                for (int i = 0; i < propiedades.Length; i++)
                {
                    object y = typeof(T).InvokeMember(propiedades[i].ToString(),
                                                       BindingFlags.GetProperty, null, item, null);
                    totales[j, i] = (y == null) ? "" : y;
                }
            }
            
            var renglon = 5 + (Datos == null ? 0 : Datos.Count);
            AgregarRenglon(string.Format("A{0}", renglon), Totales.Count, propiedades.Length, totales, true);
            AjustarColumnas(propiedades.Length);
        }

        protected override bool PuedeGenerarReporte()
        {
            return (Datos != null && Datos.Count > 0) || (Totales != null && Totales.Count > 0);
        }

        protected override void EstablecerFormato()
        {
            Type ignorable = typeof(SIE.Services.Info.Atributos.AtributoIgnorarColumnaExcel);
            PropertyInfo[] propiedades = typeof(T).GetProperties().Where(e => e.GetCustomAttributes(ignorable, true).Count() == 0).ToArray();
            var i = 1;
            foreach (var propiedad in propiedades)
            {
                var col = GetColumn(i++);
                EstablecerFormatoColumna(string.Format("{0}5", col), propiedad.PropertyType);
            }
        }

        private string GetColumn(int valor)
        {
            var res = 26;
            var sum = 64;
            var lis = new List<string>();
            decimal val = valor;
            for (int i = 0; i < 3; i++)
            {
                if ((int)val == 0)
                    break;
                var mod = val % res;
                lis.Add((mod == 0) ? "Z" : ((char)(mod + sum)).ToString());
                val = (val - 1M) / (decimal)res;
                if (val < 0)
                    break;
            }
            lis.Reverse();
            return lis.Aggregate((a, b) => string.Format("{0}{1}", a, b));
        }
    }
}