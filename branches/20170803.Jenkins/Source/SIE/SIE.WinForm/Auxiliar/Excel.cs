using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SIE.Services.Info.Reportes;


namespace SIE.WinForm.Auxiliar
{
    public class Excel
    {
        public void Generar(List<ReporteMedicamentosAplicadosModel> datos
            ,string[] encabezados)
        {
            using (ExcelPackage p = new ExcelPackage())
            {
                //Here setting some document properties

                p.Workbook.Properties.Author = "Sukarne";

                p.Workbook.Properties.Title = "Reportes";



                //Create a sheet

                p.Workbook.Worksheets.Add("Hoja Prueba");

                ExcelWorksheet ws = p.Workbook.Worksheets[1];

                ws.Name = "Prueba"; //Setting Sheet's name

                ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet

                ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

                ws.Cells[1, 1].Value = "Sample DataTable Export";

                ws.Cells[1, 1, 1, datos.Count].Merge = true;

                ws.Cells[1, 1, 1, datos.Count].Style.Font.Bold = true;

                ws.Cells[1, 1, 1, datos.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int colIndex = 1;

                int rowIndex = 2;

                foreach (string encabezado in encabezados) //Creating Headings
                {
                    var cell = ws.Cells[rowIndex, colIndex];
                    //Setting the background color of header cells to Gray

                    var fill = cell.Style.Fill;

                    fill.PatternType = ExcelFillStyle.Solid;

                    fill.BackgroundColor.SetColor(Color.Gray);
                    //Setting Top/left,right/bottom borders.

                    var border = cell.Style.Border;

                    border.Bottom.Style =

                        border.Top.Style =

                        border.Left.Style =

                        border.Right.Style = ExcelBorderStyle.Thin;
                    //Setting Value in cell

                    cell.Value = "Heading " + encabezado;
                    colIndex++;
                }
                var propiedades = ObtenerPropiedades();
                //var valores = new object[datos.Count, propiedades.Length];
                for (int j = 0; j < datos.Count; j++)
                {
                    colIndex = 1;
                    rowIndex++;
                    ReporteMedicamentosAplicadosModel item = datos[j];
                    for (int i = 0; i < propiedades.Length; i++)
                    {
                        object y = typeof(ReporteMedicamentosAplicadosModel).InvokeMember(propiedades[i].ToString(),
                                                           BindingFlags.GetProperty, null, item, null);


                        var cell = ws.Cells[rowIndex, colIndex];

                        //Setting Value in cell

                        cell.Value = y;



                        //Setting borders of cell

                        var border = cell.Style.Border;

                        border.Left.Style =

                            border.Right.Style = ExcelBorderStyle.Thin;

                        colIndex++;

                        //valores[j, i] = (y == null) ? "" : y;
                    }
                }

                Byte[] bin = p.GetAsByteArray();

                string file = "C:\\" + Guid.NewGuid().ToString() + ".xlsx";

                File.WriteAllBytes(file, bin);
            }

     
        }

        protected object[] ObtenerPropiedades()
        {
            Type ignorable = typeof(SIE.Services.Info.Atributos.AtributoIgnorarColumnaExcel);
            PropertyInfo[] propiedades = typeof(ReporteMedicamentosAplicadosModel).GetProperties().Where(e => e.GetCustomAttributes(ignorable, true).Count() == 0).ToArray();
            return propiedades.Select(t => t.Name).Cast<object>().ToArray();
        }
     
    }
}
