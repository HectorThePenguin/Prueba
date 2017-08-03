using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.PL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Reflection;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using Application = System.Windows.Application;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace SIE.WinForm.Reporte
{
    /// <summary>
    /// Lógica de interacción para ReporteTabularDisponibilidadSemana.xaml
    /// </summary>
    public partial class ReporteTabularDisponibilidadSemana
    {
        byte[] rgbGrisClaro = new byte[3] { 240, 240, 240 };
        private XSSFColor GrisClaro; 

        byte[] rgbGrisObscuro = new byte[3] { 210, 210, 210 };
        private XSSFColor GrisObscuro; 

        /// <summary>
        /// Contexto de controles
        /// </summary>
        private FiltroFechasInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (FiltroFechasInfo)DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ReporteTabularDisponibilidadSemana()
        {
            InitializeComponent();

            GrisClaro = new XSSFColor(rgbGrisClaro); 
            GrisObscuro = new XSSFColor(rgbGrisObscuro);

            InicializaContexto();
            
            InicializarDatosUsuario();
        }

        /// <summary>
        /// Inicializa datos de usuario
        /// </summary>
        private void InicializarDatosUsuario()
        {
            CargarComboOrganizaciones();
        }

        /// <summary>
        /// Carga las organizaciones
        /// </summary>
        private void CargarComboOrganizaciones()
        {
            try
            {
                bool usuarioCorporativo = AuxConfiguracion.ObtenerUsuarioCorporativo();
                //Obtener la organizacion del usuario
                int organizacionId = AuxConfiguracion.ObtenerOrganizacionUsuario();
                var organizacionPl = new OrganizacionPL();

                var organizacion = organizacionPl.ObtenerPorID(organizacionId);
                //int tipoOrganizacion = organizacion.TipoOrganizacion.TipoOrganizacionID;
                var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                var organizacionesPL = new OrganizacionPL();
                IList<OrganizacionInfo> listaorganizaciones = organizacionesPL.ObtenerTipoGanaderas();
                if (usuarioCorporativo)
                {

                    var organizacion0 =
                        new OrganizacionInfo
                        {
                            OrganizacionID = 0,
                            Descripcion = Properties.Resources.ReporteConsumoProgramadovsServido_cmbSeleccione,
                        };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    cmbOrganizacion.SelectedItem = organizacion0;
                    cmbOrganizacion.IsEnabled = true;

                }
                else
                {
                    var organizacion0 =
                       new OrganizacionInfo
                       {
                           OrganizacionID = organizacionId,
                           Descripcion = nombreOrganizacion
                       };
                    listaorganizaciones.Insert(0, organizacion0);
                    cmbOrganizacion.ItemsSource = listaorganizaciones;
                    cmbOrganizacion.SelectedItem = organizacion0;
                    cmbOrganizacion.IsEnabled = false;
                    btnGenerar.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ReporteTabularDisponibilidad_ErrorCargarOrganizaciones,
                                  MessageBoxButton.OK, MessageImage.Error);

            }
        }

        /// <summary>
        /// Inicializa contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new FiltroFechasInfo
            {
                FechaInicial = null,
                FechaFinal = null,
                Valido = false
            };
        }

        /// <summary>
        /// Handler del boton generar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            ObtenerReporte();
        }

        /// <summary>
        ///  Handler combo organizaciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbOrganizacion_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbOrganizacion.SelectedIndex == 0)
            {
                btnGenerar.IsEnabled = false;
            }
            else
            {
                btnGenerar.IsEnabled = true;
            }
        }

        /// <summary>
        /// Obtiene el reporte
        /// </summary>
        private void ObtenerReporte()
        {
            try
            {

                var organizacion = (OrganizacionInfo)cmbOrganizacion.SelectedItem;

                var resultadoInfo = ObtenerReporteTabularDisponibilidad(organizacion.OrganizacionID);

                if (resultadoInfo != null && resultadoInfo.Count > 0)
                {

                    //Cargar reporte de CR
                    var nombreOrganizacion = organizacion != null ? organizacion.Descripcion : String.Empty;

                    
                    var orgPl = new OrganizacionPL();

                    organizacion = orgPl.ObtenerPorIdConIva(organizacion.OrganizacionID);
                    string nombreOrg = String.Format(Properties.Resources.Reporte_NombreEmpresa, organizacion.Division);
                    
                    var regreso = GenerarReporteExcel(resultadoInfo, Properties.Resources.ReporteTabularCr_Titulo, nombreOrg, DtpFecha.SelectedDate.Value);
                    if (regreso)
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionReporteTabular_ReporteGeneradoExito,
                                      MessageBoxButton.OK, MessageImage.Correct);
                    }

                }
                else
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                      Properties.Resources.RecepcionReporteTabular_MsgSinInformacion,
                                      MessageBoxButton.OK, MessageImage.Warning);
                }

            }
            catch (ExcepcionGenerica)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteEjecutivo_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteEjecutivo_ErrorBuscar, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }

        /// <summary>
        /// Escribir archivo excel
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        private bool EscribirArchivo(XSSFWorkbook workbook, string nombre)
        {
            var retValue = false;

            try
            {
                SaveFileDialog dialogo = new SaveFileDialog();

                dialogo.Filter = "Excel File (*.xls)|*.xlsx";
                dialogo.FilterIndex = 1;
                dialogo.RestoreDirectory = true;
                bool? resultado = dialogo.ShowDialog() ;
                if (dialogo.FileName != string.Empty)
                {
                    //Write the stream data of workbook to the root directory
                    FileStream file = new FileStream(dialogo.FileName, FileMode.Create);
                    workbook.Write(file);
                    file.Close();

                    retValue = true;
                }

               
            }
            catch (IOException ioex)
            {
                Logger.Error(ioex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.RecepcionReporteTabular_FalloGuardarArchivo, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
            
            return retValue;
        }

        /// <summary>
        /// Genera el archivo excel
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="tituloReporte"></param>
        /// <param name="nombreOrganizacion"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private bool GenerarReporteExcel(List<ReporteTabularDisponibilidadSemanaInfo> datos, string tituloReporte, string nombreOrganizacion, DateTime fecha)
        {
            bool retValue = false;

            XSSFWorkbook hssfworkbook = new XSSFWorkbook();

            //Manejo del formato
            IDataFormat format = hssfworkbook.CreateDataFormat();
            //Estilo para renglon en general encabezado claro
            var esRowGeneral = (XSSFCellStyle) hssfworkbook.CreateCellStyle();
            esRowGeneral.SetFillForegroundColor(GrisClaro);
            esRowGeneral.FillPattern = FillPattern.SolidForeground;
            esRowGeneral.FillBackgroundColor = IndexedColors.White.Index;

            //Creamos la hora del archivo
            ISheet sheet1 = hssfworkbook.CreateSheet(Properties.Resources.RecepcionReporteTabular_RptNomHoja);
            
            //indices
            int indexColumn = 0;
            int indexRow = 6;
            int indexNumeroSemana = 0;
            int maxCorrales = 0;

            foreach (var semana in datos)
            {
                if (maxCorrales < semana.TotalCorrales)
                    maxCorrales = semana.TotalCorrales;
            }

            //Renglones de encabezado
            ICellStyle styleEncabezado = hssfworkbook.CreateCellStyle();

            var i = 0;
            //Creamos los renglones para alojar el numero de corrales
            for (; i < maxCorrales + indexRow + 5; i++)
            {
                IRow rengloi = sheet1.CreateRow(i);
            }

            //fuente para todos los encabezados y totales
            IFont fontEncabezado = hssfworkbook.CreateFont();
            fontEncabezado.FontHeightInPoints = 10;
            fontEncabezado.Boldweight = (short)FontBoldWeight.Bold;


            IRow renglonEncabezado1 = sheet1.GetRow(indexRow + 1);
            IRow renglonNsemana = sheet1.CreateRow(indexRow + 2);
            IRow renglonEncabezadoDetalle = sheet1.CreateRow(indexRow + 3);
            IRow renglonTotal = sheet1.CreateRow(i);

            //Creamos los registros de semanas
            foreach (var semana in datos)
            {

                var indexAux = indexColumn;

                //Total Cabezas
                XSSFRichTextString lblTotal = new XSSFRichTextString(Properties.Resources.RecepcionReporteTabular_RptEncTotal);
                lblTotal.ApplyFont(0, lblTotal.Length, fontEncabezado);
                ICell cellLblTotal = renglonTotal.CreateCell(indexAux);
                cellLblTotal.SetCellValue(lblTotal);

                //Total Cabezas
                ICellStyle esTotal = hssfworkbook.CreateCellStyle();
                esTotal.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;

                XSSFRichTextString txtTotal = new XSSFRichTextString(semana.TotalCabezas.ToString());
                txtTotal.ApplyFont(0, txtTotal.Length, fontEncabezado);
                ICell cellTxtTotal = renglonTotal.CreateCell(indexAux + 3);
                cellTxtTotal.CellStyle = esTotal;
                cellTxtTotal.SetCellValue(txtTotal);


                //LblSem 

                var esSem = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                esSem.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                esSem.SetFillForegroundColor(GrisClaro);
                esSem.FillPattern = FillPattern.SolidForeground;
                esSem.FillBackgroundColor = IndexedColors.White.Index;

                XSSFRichTextString lblSemana = new XSSFRichTextString(Properties.Resources.RecepcionReporteTabular_RptEncSemN);
                lblSemana.ApplyFont(0, lblSemana.Length, fontEncabezado);
                ICell cellLblSemana = renglonEncabezado1.CreateCell(indexAux);
                cellLblSemana.CellStyle = esSem;
                cellLblSemana.SetCellValue(lblSemana);

                //////merged cells on mutiple rows
                //CellRangeAddress regionSemana = new CellRangeAddress(indexRow, indexRow, indexColumn, indexColumn+=2);
                //sheet1.AddMergedRegion(regionSemana);

                //LblAc 
                var esAc = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                esAc.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                esAc.SetFillForegroundColor(GrisClaro);
                esAc.FillPattern = FillPattern.SolidForeground;
                esAc.FillBackgroundColor = IndexedColors.White.Index;

                XSSFRichTextString lblAc = new XSSFRichTextString(Properties.Resources.RecepcionReporteTabular_RptEncAC);
                lblAc.ApplyFont(0, lblAc.Length, fontEncabezado);
                ICell cellLblAc = renglonEncabezado1.CreateCell(indexAux + 3);
                cellLblAc.CellStyle = esAc;
                cellLblAc.SetCellValue(lblAc);

                //CellRangeAddress regionAc = new CellRangeAddress(indexRow, indexRow, indexColumn, indexColumn + 1);
                //sheet1.AddMergedRegion(regionAc);

                //Renglon 2 valor de semana y primer lunes de la semana
                indexAux = indexColumn;

                //Valor de numero de semana
                var estxtSemana = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                estxtSemana.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                estxtSemana.SetFillForegroundColor(GrisClaro);
                estxtSemana.FillPattern = FillPattern.SolidForeground;
                estxtSemana.FillBackgroundColor = IndexedColors.White.Index;

                XSSFRichTextString txtSemana = new XSSFRichTextString(indexNumeroSemana.ToString());
                txtSemana.ApplyFont(0, txtSemana.Length, fontEncabezado);
                ICell cellTxtSemana = renglonNsemana.CreateCell(indexAux);
                cellTxtSemana.CellStyle = estxtSemana;
                cellTxtSemana.SetCellValue(txtSemana);

                var cellx1 = renglonNsemana.CreateCell(indexAux + 1);
                cellx1.CellStyle = estxtSemana;
                var cellx2 = renglonNsemana.CreateCell(indexAux + 2);
                cellx2.CellStyle = estxtSemana;

                //Valor de numero de semana
                var esTxtLunes = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                esTxtLunes.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                esTxtLunes.SetFillForegroundColor(GrisClaro);
                esTxtLunes.FillPattern = FillPattern.SolidForeground;
                esTxtLunes.FillBackgroundColor = IndexedColors.White.Index;

                XSSFRichTextString txtPrimerLunes =
                    new XSSFRichTextString(semana.FechaInicioSemana.ToString("MMM dd").ToUpper());
                txtPrimerLunes.ApplyFont(0, txtPrimerLunes.Length, fontEncabezado);
                ICell cellTxtLunes = renglonNsemana.CreateCell(indexAux + 3);
                cellTxtLunes.CellStyle = esTxtLunes;
                cellTxtLunes.SetCellValue(txtPrimerLunes);

                var cellx3 = renglonNsemana.CreateCell(indexAux + 4);
                cellx3.CellStyle = estxtSemana;

                indexAux = indexColumn;

                //Valor de enc corral
                var esEncCorr = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                esEncCorr.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                esEncCorr.SetFillForegroundColor(GrisClaro);
                esEncCorr.FillPattern = FillPattern.SolidForeground;
                esEncCorr.FillBackgroundColor = IndexedColors.White.Index;

                sheet1.SetColumnWidth(indexAux, 5 * 256);
                XSSFRichTextString encCorral = new XSSFRichTextString(Properties.Resources.RecepcionReporteTabular_RptEncCorral);
                encCorral.ApplyFont(0, encCorral.Length, fontEncabezado);
                ICell cellEncCorral = renglonEncabezadoDetalle.CreateCell(indexAux++);
                cellEncCorral.CellStyle = esEncCorr;
                cellEncCorral.SetCellValue(encCorral);

                //Valor disp
                var esDis = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                esDis.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                esDis.SetFillForegroundColor(GrisClaro);
                esDis.FillPattern = FillPattern.SolidForeground;
                esDis.FillBackgroundColor = IndexedColors.White.Index;

                sheet1.SetColumnWidth(indexAux, 3 * 256);
                XSSFRichTextString encDispoManual = new XSSFRichTextString("");
                encDispoManual.ApplyFont(0, encDispoManual.Length, fontEncabezado);
                ICell cellDispManual = renglonEncabezadoDetalle.CreateCell(indexAux++);
                cellDispManual.CellStyle = esDis;
                cellDispManual.SetCellValue(encDispoManual);

                //Valor de enc tipo
                var esTipo = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                esTipo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                esTipo.SetFillForegroundColor(GrisClaro);
                esTipo.FillPattern = FillPattern.SolidForeground;
                esTipo.FillBackgroundColor = IndexedColors.White.Index;

                sheet1.SetColumnWidth(indexAux, 20 * 256);
                XSSFRichTextString encTipo = new XSSFRichTextString(Properties.Resources.RecepcionReporteTabular_RptEncTipo);
                encTipo.ApplyFont(0, encTipo.Length, fontEncabezado);
                ICell cellEncTipo = renglonEncabezadoDetalle.CreateCell(indexAux++);
                cellEncTipo.CellStyle = esTipo;
                cellEncTipo.SetCellValue(encTipo);

                //Valor de enc cabezas
                var esCab = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                esCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                esCab.SetFillForegroundColor(GrisClaro);
                esCab.FillPattern = FillPattern.SolidForeground;
                esCab.FillBackgroundColor = IndexedColors.White.Index;

                sheet1.SetColumnWidth(indexAux, 5 * 256);
                XSSFRichTextString encCabezas = new XSSFRichTextString(Properties.Resources.RecepcionReporteTabular_RptEncCab);
                encCabezas.ApplyFont(0, encCabezas.Length, fontEncabezado);
                ICell cellEncCabezas = renglonEncabezadoDetalle.CreateCell(indexAux++);
                cellEncCabezas.CellStyle = esCab;
                cellEncCabezas.SetCellValue(encCabezas);

                //encabezado formula
                var esForm = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
                esForm.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                esForm.SetFillForegroundColor(GrisClaro);
                esForm.FillPattern = FillPattern.SolidForeground;
                esForm.FillBackgroundColor = IndexedColors.White.Index;

                sheet1.SetColumnWidth(indexAux, 5 * 256);
                XSSFRichTextString encForm = new XSSFRichTextString(Properties.Resources.RecepcionReporteTabular_RptEncFor);
                encForm.ApplyFont(0, encForm.Length, fontEncabezado);
                ICell cellEncFormula = renglonEncabezadoDetalle.CreateCell(indexAux);
                cellEncFormula.CellStyle = esForm;
                cellEncFormula.SetCellValue(encForm);

                //colocamos los corrales
                var indexCorral = indexRow + 4;

                foreach (var corral in semana.Corrales)
                {
                    indexAux = indexColumn;

                    IRow rowCorral = sheet1.GetRow(indexCorral);

                    XSSFRichTextString rwCorral = new XSSFRichTextString(corral.Codigo.Trim());
                    ICell cellCorral = rowCorral.CreateCell(indexAux++);
                    cellCorral.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                    cellCorral.SetCellValue(rwCorral);

                    XSSFRichTextString rwDispo =
                        new XSSFRichTextString(corral.DisponibilidadManual == 1 ? "*" : "");
                    ICell cellDispo = rowCorral.CreateCell(indexAux++);
                    cellDispo.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                    cellDispo.SetCellValue(rwDispo);

                    XSSFRichTextString rwTipo = new XSSFRichTextString(corral.Descripcion);
                    ICell cellTipo = rowCorral.CreateCell(indexAux++);
                    cellTipo.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    cellTipo.SetCellValue(rwTipo);

                    XSSFRichTextString rwCabezas = new XSSFRichTextString(corral.Cabezas.ToString());
                    ICell cellCabezas = rowCorral.CreateCell(indexAux++);
                    ICellStyle esRwCabezas = hssfworkbook.CreateCellStyle();
                    esRwCabezas.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                    esRwCabezas.DataFormat = format.GetFormat("0.00");
                    cellCabezas.CellStyle = esRwCabezas;
                    cellCabezas.SetCellValue(rwCabezas);

                    XSSFRichTextString rwFormula = new XSSFRichTextString(corral.FormulaIDServida.ToString());
                    ICell cellFormula = rowCorral.CreateCell(indexAux);
                    ICellStyle esRwFormula = hssfworkbook.CreateCellStyle();
                    esRwFormula.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                    esRwFormula.DataFormat = format.GetFormat("0.00");

                    cellFormula.CellStyle = esRwFormula;
                    cellFormula.SetCellValue(rwFormula);

                    indexCorral++;
                }

                indexColumn += 6;
                sheet1.SetColumnWidth(indexColumn - 1, 2 * 256);
                for (int renglonR = indexRow; renglonR < maxCorrales + indexRow + 5; renglonR++)
                {
                    var renglon = sheet1.GetRow(renglonR);
                    ICell celda = renglon.GetCell(indexColumn - 1);

                    if (celda == null)
                        celda = renglon.CreateCell(indexColumn - 1);

                    celda.CellStyle = esRowGeneral;

                }


                indexNumeroSemana++;

                if (indexColumn > 10000)
                {
                    indexColumn = 0;
                    indexRow = maxCorrales + 10;
                }
            }



            //Agregamos el logo
            IDrawing patriarch = sheet1.CreateDrawingPatriarch();
            //create the anchor
            XSSFClientAnchor anchor;
            anchor = new XSSFClientAnchor(10, 10, 0, 0, 0, 0, 7, 5);
            anchor.AnchorType = 2;
            //load the picture and get the picture index in the workbook
            XSSFPicture picture = (XSSFPicture)patriarch.CreatePicture(anchor, LoadImage("Imagenes/skLogo.png", hssfworkbook));
            //Reset the image to the original size.
            //picture.Resize();   //Note: Resize will reset client anchor you set.
            picture.LineStyle = LineStyle.None;

            for (int indexRowTitulos = 0; indexRowTitulos <= 7; indexRowTitulos++)
            {
                IRow renglon = sheet1.GetRow(indexRowTitulos);

                if (renglon != null)
                {

                    for (int ix = 0; ix <= indexColumn - 1; ix++)
                    {
                        ICell celda = renglon.GetCell(ix);

                        if (celda == null)
                            celda = renglon.CreateCell(ix);

                        celda.CellStyle = esRowGeneral;
                    }
                }

            }

            for (int ix = 0; ix <= indexColumn - 1; ix++)
            {
                //ICell celda = renglonEncabezado1.GetCell(ix);
                //if (celda == null)
                //    celda = renglonEncabezado1.CreateCell(ix);

                ICell celdaTotal = renglonTotal.GetCell(ix);
                if (celdaTotal == null)
                    celdaTotal = renglonTotal.CreateCell(ix);

                //celda.CellStyle = esRowGeneral;
                celdaTotal.CellStyle = esRowGeneral;

            }

            //Encabezado de empresa
            var estiloOrganizacion = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
            estiloOrganizacion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            estiloOrganizacion.SetFillForegroundColor(GrisClaro);
            estiloOrganizacion.FillPattern = FillPattern.SolidForeground;

            XSSFRichTextString txtOrganizacion = new XSSFRichTextString(nombreOrganizacion);
            IFont fontOrg = hssfworkbook.CreateFont();
            fontOrg.FontHeightInPoints = 20;
            fontOrg.Boldweight = (short)FontBoldWeight.Bold;
            txtOrganizacion.ApplyFont(0, txtOrganizacion.Length, fontOrg);
            ICell cell = sheet1.GetRow(1).CreateCell(14);
            cell.CellStyle = estiloOrganizacion;
            cell.SetCellValue(txtOrganizacion);



            //Titulo reporte
            var estiloTituloReporte = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
            estiloTituloReporte.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            estiloTituloReporte.SetFillForegroundColor(GrisClaro);
            estiloTituloReporte.FillPattern = FillPattern.SolidForeground;

            XSSFRichTextString txtTitulo = new XSSFRichTextString(tituloReporte);
            IFont fontTitulo = hssfworkbook.CreateFont();
            fontTitulo.FontHeightInPoints = 14;
            fontTitulo.Boldweight = (short)FontBoldWeight.Bold;
            txtTitulo.ApplyFont(0, txtTitulo.Length, fontTitulo);
            ICell cellTitulo = sheet1.GetRow(3).CreateCell(14);
            cellTitulo.CellStyle = estiloTituloReporte;
            cellTitulo.SetCellValue(txtTitulo);

            //fecha 
            var estiloFecha = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
            estiloFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            estiloFecha.SetFillForegroundColor(GrisClaro);
            estiloFecha.FillPattern = FillPattern.SolidForeground;

            XSSFRichTextString txtFecha = new XSSFRichTextString(fecha.ToString("dd/MM/yyyy"));
            IFont fontFecha = hssfworkbook.CreateFont();
            fontFecha.FontHeightInPoints = 10;
            fontFecha.Boldweight = (short)FontBoldWeight.Bold;
            txtFecha.ApplyFont(0, txtFecha.Length, fontFecha);
            ICell cellFecha = sheet1.GetRow(4).CreateCell(14);
            cellFecha.CellStyle = estiloFecha;
            cellFecha.SetCellValue(txtFecha);

            retValue = EscribirArchivo(hssfworkbook, Properties.Resources.RecepcionReporteTabular_RptFileName);

            return retValue;
        }

        /// <summary>
        /// Cargar la imagen para el archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="wb"></param>
        /// <returns></returns>
        public static int LoadImage(string path, IWorkbook wb)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            return wb.AddPicture(buffer, PictureType.JPEG);

        }

        /// <summary>
        /// Handler de carga de la fecha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFecha_Loaded(object sender, RoutedEventArgs e)
        {
            DtpFecha.SelectedDate = DateTime.Now;
        }
        /// <summary>
        /// Método obtener el reporte ejecutivo
        /// </summary>
        /// <returns></returns>
        private List<ReporteTabularDisponibilidadSemanaInfo> ObtenerReporteTabularDisponibilidad(int organizacionId)
        {
            try
            {
                var reporteTabPL = new ReporteTabularDisponibilidadPL();

                List<ReporteTabularDisponibilidadSemanaInfo> resultadoInfo =
                    reporteTabPL.GenerarReporteTabularDisponibilidad(organizacionId,DtpFecha.SelectedDate.Value);

                return resultadoInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
