using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using SuKarne.Controls.MessageBox;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Resources;
using Application = Microsoft.Office.Interop.Excel.Application;
using Clipboard = System.Windows.Forms.Clipboard;
using Font = Microsoft.Office.Interop.Excel.Font;
using SIE.Base.Exepciones;

namespace SIE.WinForm.Auxiliar
{
    public abstract class ExportarExcelBase
    {
        public ExportarExcelBase()
        {
            Encabezados = new string[0];
            TituloReporte = string.Empty;
            NombreReporte = string.Empty;
            NombreArchivo = string.Empty;
            libro = null;
            hoja = null;
            hojas = null;
            excelApp = null;
            libros = null;
            iniciarDiccionarioTipos();
        }

        protected readonly object valorOpcional = Missing.Value;
        protected Application excelApp;
        protected Font fuente;
        protected Worksheet hoja;
        protected Sheets hojas;
        protected Workbook libro;
        protected Workbooks libros;
        protected Range rango;
        public string[] Encabezados { private get; set; }
        public string TituloReporte { private get; set; }
        public string SubTitulo { private get; set; }
        public string NombreReporte { private get; set; }
        public bool MostrarLogo { private get; set; }
        public string NombreArchivo { private get; set; }
        private string RutaFinal { get; set; }
        private Dictionary<Type, string> dicFormatos;

        /// <summary>
        /// Inicializa el diccionario de formatos en base al tipo
        /// </summary>
        private void iniciarDiccionarioTipos()
        {
            dicFormatos = new Dictionary<Type,string>();
            dicFormatos.Add(typeof(int), "0");
            dicFormatos.Add(typeof(int?), "0");
            dicFormatos.Add(typeof(long), "0");
            dicFormatos.Add(typeof(long?), "0");
            dicFormatos.Add(typeof(short), "0");
            dicFormatos.Add(typeof(short?), "0");
            
            dicFormatos.Add(typeof(decimal), "0.00");
            dicFormatos.Add(typeof(decimal?), "0.00");
            dicFormatos.Add(typeof(float), "0.00");
            dicFormatos.Add(typeof(float?), "0.00");
            dicFormatos.Add(typeof(double), "0.00");
            dicFormatos.Add(typeof(double?), "0.00");

            //dicFormatos.Add(typeof(DateTime), "dd/mm/aaaa");
            //dicFormatos.Add(typeof(DateTime?), "dd/mm/aaaa");
            
            dicFormatos.Add(typeof(string), "@");
            dicFormatos.Add(typeof(bool), "@");
            dicFormatos.Add(typeof(bool?), "@");
            dicFormatos.Add(typeof(char), "@");
        }

        /// <summary>
        /// Valida si se puede generar el Reporte, practicamente verifica que tenga datos que pueda escribir
        /// </summary>
        /// <returns></returns>
        protected abstract bool PuedeGenerarReporte();

        /// <summary>
        /// Escribir la informacion en la hoja
        /// </summary>
        /// <param name="propiedades"></param>
        protected abstract void EscribirDatos(object[] propiedades);

        /// <summary>
        /// Escribir los totales de la hoja
        /// </summary>
        /// <param name="propiedades"></param>
        protected abstract void EscribirTotales(object[] propiedades);

        /// <summary>
        /// Establece el formato de las Columnas
        /// </summary>
        protected abstract void EstablecerFormato();

        /// <summary>
        /// Metodo que obtiene las propiedades de la coleccion para llenar los datos
        /// </summary>
        /// <returns></returns>
        protected abstract object[] ObtenerPropiedades();

        /// <summary>
        /// Establece el formato de una columna especifica
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="tipo"></param>
        protected void EstablecerFormatoColumna(string columna, Type tipo)
        {
            rango = hoja.Range[columna];
            rango = rango.Resize[65500, 1];
            if (dicFormatos.ContainsKey(tipo))
                rango.NumberFormat = dicFormatos[tipo];
            else
                rango.NumberFormat = "@";
        }

        /// <summary>
        /// Generate report and sub functions
        /// </summary>
        /// <param name="ejecutar"></param>
        /// <returns></returns>
        private string generarReporte(System.Action ejecutar = null)
        {
            try
            {
                if (PuedeGenerarReporte())
                {
                    Mouse.SetCursor(Cursors.Wait);
                    RutaFinal = string.Empty;
                    CrearExcel();
                    LlenarHoja();
                    if (ejecutar != null)
                    {
                        ejecutar();
                    }
                    OpenReport();
                    Mouse.SetCursor(Cursors.Arrow);
                }
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Properties.Resources.ErrorGenerarArchivoExcel, MessageBoxButton.OK);
            }
            finally
            {
                Liberar(hoja);
                Liberar(hojas);
                Liberar(libro);
                Liberar(libros);
                Liberar(excelApp);
            }
            return RutaFinal;
        }

        /// <summary>
        /// Generate report and sub functions
        /// </summary>
        public string GenerarReporte()
        {
            return generarReporte();
        }

        /// <summary>
        /// Generate report and sub functions with another sheets
        /// </summary>
        public string GenerarReporte(params ExportarExcelBase[] hojas)
        {
            return generarReporte(() =>
            {
                var i = 2;
                var anterior = this.hoja;
                foreach (var _hoja in hojas)
                {
                    Worksheet workSheet = this.hojas.get_Item(i++);
                    if (workSheet == null)
                    {
                        workSheet = this.hojas.Add(valorOpcional, anterior);
                    }
                    _hoja.GenerarHoja(workSheet);
                    anterior = workSheet;
                }
            });
        }

        /// <summary>
        /// Generar una Hoja con la informacion.
        /// </summary>
        /// <param name="hoja"></param>
        protected void GenerarHoja(Worksheet hoja)
        {
            try
            {
                if (PuedeGenerarReporte())
                {
                    this.hoja = hoja;
                    LlenarHoja();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Properties.Resources.ErrorGenerarArchivoExcel, MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Hacer que la aplicacion sea visible al usuario
        /// </summary>
        private void OpenReport()
        {
            if (NombreArchivo == string.Empty)
            {
                NombreArchivo = "Libro";
            }
            var dialogo = new SaveFileDialog
            {
                FileName = NombreArchivo,
                DefaultExt = ".xls",
                Filter = "Documentos Excel (*.xls)|*.xls"
            };
            bool? resultado = dialogo.ShowDialog();
            if (resultado == true)
            {
                string nombre = dialogo.FileName;
                if (File.Exists(nombre))
                {
                    try
                    {
                        var archivoExcel = new FileStream(nombre, FileMode.Open);
                        archivoExcel.Close();
                    }
                    catch (IOException)
                    {
                        throw new ExcepcionServicio(Properties.Resources.ExportarExcel_ArchivoAbierto);
                    }
                }
                excelApp.ActiveWorkbook.SaveCopyAs(nombre);
                excelApp.ActiveWorkbook.Saved = true;
                excelApp.Quit();
                RutaFinal = nombre;
            }
        }

        /// <summary>
        /// Llenar la hoja de Excel
        /// </summary>
        private void LlenarHoja()
        {
            CrearTitulo();
            CrearNombreReporte();
            CrearSubTitulo();
            CrearEncabezado();
            EstablecerFormato();
            object[] encabezado = ObtenerPropiedades();
            EscribirDatos(encabezado);
            EscribirTotales(encabezado);
        }

        /// <summary>
        /// Metodo que ajusta el ancho de las columnas de acuerdo a los datos       
        /// </summary>
        /// <param name="rangoInicial"></param>
        /// <param name="contadorrenglones"></param>
        /// <param name="contadorColumnas"></param>
        protected void AjustarColumnas(string rangoInicial, int contadorrenglones, int contadorColumnas)
        {
            rango = hoja.Range[rangoInicial, valorOpcional];
            rango = rango.Resize[contadorrenglones, contadorColumnas];
            rango.Columns.AutoFit();
        }

        /// <summary>
        /// Metodo que ajusta el ancho de las columnas
        /// </summary>
        /// <param name="rangoInicial"></param>
        /// <param name="contadorrenglones"></param>
        /// <param name="contadorColumnas"></param>
        protected void AjustarColumnas(int columnas)
        {
            var i = 65;
            for (int j = 0; j < columnas; j++)
            {
                rango = hoja.Range[string.Format("{0}:{0}", (char)i), valorOpcional];
                rango.Columns.AutoFit();
                i++;
            }
        }

        /// <summary>
        /// Metodo que crea el encabezado 
        /// </summary>
        /// <returns></returns>
        private void CrearEncabezado()
        {
            if (Encabezados.Length > 0)
            {
                string[] encabezadoAgregar = Encabezados.ToArray();
                AgregarRenglon("A4", 1, encabezadoAgregar.Length, encabezadoAgregar);
                EstiloEncabezado();
            }
        }

        /// <summary>
        /// Escribir el titulo del Reporte
        /// </summary>
        private void CrearTitulo()
        {
            if (TituloReporte != string.Empty)
            {
                object[] encabezado = ObtenerPropiedades();
                AgregarRenglonCentrado("A1", 1, encabezado.Length, TituloReporte);
                EstiloTitulo();
            }
        }

        /// <summary>
        /// Metodo que muestra muestra el logo del reporte
        /// </summary>
        private void CrearLogo()
        {
            if (MostrarLogo)
            {
                var ruta = new Uri(string.Format("/SIAP;component/Imagenes/ReporteLogo.png"),
                               UriKind.Relative);
                StreamResourceInfo streamLogo = System.Windows.Application.GetResourceStream(ruta);
                Image logoSK = null;
                if (streamLogo != null)
                {
                    Stream imagenStream = streamLogo.Stream;
                    logoSK = Image.FromStream(imagenStream);
                }
                if (logoSK != null)
                {
                    Clipboard.Clear();
                    Clipboard.SetImage(logoSK);
                    if (Clipboard.ContainsImage())
                    {
                        hoja.Paste();
                        var p = (Pictures)hoja.Pictures(Missing.Value);
                        var pic = (Picture)p.Item(p.Count);
                        pic.Height = 30;
                        pic.Width = 80;
                        pic.Top = 15;
                        pic.Left = rango.Left;
                    }
                    streamLogo = null;
                    logoSK.Dispose();
                    logoSK = null;
                }
            }
        }

        /// <summary>
        /// Metodo que crear el nombre del reporte
        /// </summary>
        private void CrearNombreReporte()
        {
            if (NombreReporte != string.Empty)
            {
                int columna = ObtenerPropiedades().Length;
                AgregarRenglonCentrado("A2", 1, columna, NombreReporte);
                EstiloNombreReporte();
                CrearLogo();
            }
        }

        /// <summary>
        /// Metodo que crear el nombre del reporte
        /// </summary>
        private void CrearSubTitulo()
        {
            if (SubTitulo != string.Empty)
            {
                int columna = ObtenerPropiedades().Length;
                Range rango = AgregarRenglonCentrado("A3", 1, columna, SubTitulo);
                EstiloSubTitulo();
            }
        }

        /// <summary>
        /// Metodo que establece el estilo del encabezado
        /// </summary>
        private void EstiloEncabezado()
        {
            int color = ColorTranslator.ToOle(Color.FromArgb(0xF2, 0xF2, 0xF2));
            EstablecerEstilo("Arial", true, 12, color);
        }

        /// <summary>
        /// Metodo que establece el estilo del Titulo del reporte
        /// </summary>
        private void EstiloTitulo()
        {
            int color = ColorTranslator.ToOle(Color.FromArgb(0xF2, 0xF2, 0xF2));
            EstablecerEstilo("Arial", true, 18, color);
        }

        /// <summary>
        /// Metodo que establece el estilo del nombre del reporte
        /// </summary>
        private void EstiloNombreReporte()
        {
            int color = ColorTranslator.ToOle(Color.FromArgb(0xF2, 0xF2, 0xF2));
            EstablecerEstilo("Arial", true, 14, color);
        }

        /// <summary>
        /// Establece el estilo de las celdas
        /// </summary>
        /// <param name="nombreFuente"></param>
        /// <param name="negritas"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        private void EstablecerEstilo(string nombreFuente, bool negritas, int size, int color)
        {
            rango.Font.Name = nombreFuente;
            fuente = rango.Font;
            fuente.Bold = negritas;
            fuente.Size = size;
            rango.Cells.Interior.Color = color;
        }

        /// <summary>
        /// Metodo que establece el estilo del Subtitulo
        /// </summary>
        private void EstiloSubTitulo()
        {
            int color = ColorTranslator.ToOle(Color.FromArgb(0xF2, 0xF2, 0xF2));
            EstablecerEstilo("Arial", true, 12, color);
        }

        /// <summary>
        /// Metodo que agrega un renglon 
        /// </summary>
        /// <param name="rangoInicial"></param>
        /// <param name="contadorRenglon"></param>
        /// <param name="contadorColumna"></param>
        /// <param name="valores"></param>
        protected void AgregarRenglon(string rangoInicial, int contadorRenglon, int contadorColumna, object valores, bool negrita = false)
        {
            rango = hoja.Range[rangoInicial, valorOpcional];
            rango = rango.Resize[contadorRenglon, contadorColumna];
            rango.Value = valores;
            if (negrita)
            {
                rango.Font.Bold = true;
            }
        }

        /// <summary>
        /// Metodo que agrega un renglon centrado en un rango de columnas
        /// </summary>
        /// <param name="rangoInicial"></param>
        /// <param name="contadorRenglon"></param>
        /// <param name="contadorColumna"></param>
        /// <param name="valor"></param>
        private Range AgregarRenglonCentrado(string rangoInicial, int contadorRenglon, int contadorColumna, object valor)
        {
            rango = hoja.Range[rangoInicial, valorOpcional];
            rango = rango.Resize[contadorRenglon, contadorColumna];
            rango.Merge();
            rango.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;            
            rango.Value = valor;

            return rango;
        }

        /// <summary>
        /// Metodo que crea una aplicacion de Excel 
        /// </summary>
        private void CrearExcel()
        {
            excelApp = new Application();
            libros = excelApp.Workbooks;
            libro = libros.Add(valorOpcional);
            hojas = libro.Worksheets;
            hoja = (Worksheet)(hojas.Item[1]);
        }

        /// <summary>
        /// Libera de memoria los objetos
        /// </summary>
        /// <param name="obj"></param>
        private void Liberar(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
