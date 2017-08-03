using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using System.Linq;

namespace SIE.WinForm.Administracion
{
    /// <summary>
    /// Lógica de interacción para ImpresionCheckListRolado.xaml
    /// </summary>
    public partial class ImpresionCheckListRolado
    {
        private readonly List<int> renglonesOptimos = new List<int> {8, 11, 14, 17, 19, 21, 24, 27};

        private void CargarTurnos()
        {
            var listaTurnos = new List<TurnoInfo>
                                  {
                                      new TurnoInfo
                                          {
                                              TurnoID = 1,
                                              Descripcion = "Turno Uno"
                                          },
                                      new TurnoInfo
                                          {
                                              TurnoID = 2,
                                              Descripcion = "Turno Dos"
                                          }
                                      ,
                                      new TurnoInfo
                                          {
                                              TurnoID = 3,
                                              Descripcion = "Turno Tres"
                                          }
                                  };
            cmbTurno.ItemsSource = listaTurnos;
            cmbTurno.SelectedIndex = 0;
        }

        private void BtnLimpiar_OnClick(object sender, RoutedEventArgs e)
        {
            dtpFecha.SelectedDate = null;
            cmbTurno.SelectedIndex = 0;
        }

        private void BtnBuscar_OnClick(object sender, RoutedEventArgs e)
        {
            ObtenerCheckListRolado();
        }

        private void ObtenerCheckListRolado()
        {
            try
            {
                var checkListRoladoraBL = new CheckListRoladoraBL();
                if (!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                              Properties.Resources.ImpresionCheckListRolado_FechaInvalida, MessageBoxButton.OK,
                              MessageImage.Warning);
                    return;
                }
                var fecha = dtpFecha.SelectedDate.Value;
                int organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();

                var turnoInfo = (TurnoInfo)cmbTurno.SelectedItem;
                int turno = 0;
                if (turnoInfo != null)
                {
                    turno = turnoInfo.TurnoID;
                }
                var checkList =
                    checkListRoladoraBL.ObtenerDatosImpresionCheckListRoladora(fecha, turno, organizacionID);
                if (checkList == null)
                {
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                             Properties.Resources.ImpresionCheckListRolado_SinCheckList, MessageBoxButton.OK,
                             MessageImage.Warning);
                    return;
                }
                GenerarReporteExcel(checkList);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionCheckListRolado_ErrorConsultarCheckList, MessageBoxButton.OK,
                                  MessageImage.Error);
            }

        }

        private void DtpFecha_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!dtpFecha.SelectedDate.HasValue || dtpFecha.SelectedDate.Value == DateTime.MinValue)
            {
                return;
            }
            if (dtpFecha.SelectedDate.Value > DateTime.Now.Date)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                Properties.Resources.ImpresionCheckListRolado_FechaMayor, MessageBoxButton.OK,
                                MessageImage.Warning);
                dtpFecha.SelectedDate = null;
            }
        }

        //readonly byte[] rgbGrisClaro = new byte[] { 240, 240, 240 };
        //private readonly XSSFColor grisClaro;

        readonly byte[] rgbGrisObscuro = new byte[] { 210, 210, 210 };

        readonly byte[] rgbRojoTitulo = new byte[] { 230, 0, 0 };

        
        private readonly XSSFColor grisObscuro;
        private readonly XSSFColor rojoTitulo;

        public ImpresionCheckListRolado()
        {
            InitializeComponent();
            //grisClaro = new XSSFColor(rgbGrisClaro);
            grisObscuro = new XSSFColor(rgbGrisObscuro);
            rojoTitulo = new XSSFColor(rgbRojoTitulo);
            CargarTurnos();
        }

        /// <summary>
        /// Genera el archivo excel
        /// </summary>

        /// <returns></returns>
        private void GenerarReporteExcel(List<ImpresionCheckListRoladoModel> checkList)
        {
            var hssfworkbook = new XSSFWorkbook();

            var valoresGeneral = checkList.FirstOrDefault();

            if (valoresGeneral == null)
            {
                return;
            }
            CheckListRoladoraGeneralInfo checkListGeneral = valoresGeneral.CheckListRoladoraGeneral;



            var celdaGrisBorde = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
            celdaGrisBorde.SetFillForegroundColor(grisObscuro);
            celdaGrisBorde.FillPattern = FillPattern.SolidForeground;
            celdaGrisBorde.FillBackgroundColor = IndexedColors.White.Index;
            celdaGrisBorde.BorderBottom = BorderStyle.Medium;
            celdaGrisBorde.BorderTop = BorderStyle.Medium;
            celdaGrisBorde.BorderLeft = BorderStyle.Medium;
            celdaGrisBorde.BorderRight = BorderStyle.Medium;
            celdaGrisBorde.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            var celdaRojoTitulo = (XSSFCellStyle)hssfworkbook.CreateCellStyle();
            celdaRojoTitulo.SetFillForegroundColor(rojoTitulo);
            celdaRojoTitulo.FillPattern = FillPattern.SolidForeground;
            celdaRojoTitulo.FillBackgroundColor = IndexedColors.White.Index;
            celdaRojoTitulo.BorderBottom = BorderStyle.Medium;
            celdaRojoTitulo.BorderTop = BorderStyle.Medium;
            celdaRojoTitulo.BorderLeft = BorderStyle.Medium;
            celdaRojoTitulo.BorderRight = BorderStyle.Medium;
            celdaRojoTitulo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            //Creamos la hoja del archivo
            ISheet hojaExcel = hssfworkbook.CreateSheet("CheckList");

            //indices
            const int indexColumn = 0;

            //fuente para todos los encabezados y totales
            IFont fontEncabezado = hssfworkbook.CreateFont();
            fontEncabezado.FontHeightInPoints = 14;
            fontEncabezado.Boldweight = (short)FontBoldWeight.Bold;

            IFont fontTitulos = hssfworkbook.CreateFont();
            fontTitulos.FontHeightInPoints = 10;
            fontTitulos.Boldweight = (short)FontBoldWeight.Bold;


            //Estilo con bordeado
            var estiloCeldaBorde = hssfworkbook.CreateCellStyle();
            estiloCeldaBorde.BorderRight = BorderStyle.Medium;
            estiloCeldaBorde.BorderBottom = BorderStyle.Medium;
            estiloCeldaBorde.BorderTop = BorderStyle.Medium;
            estiloCeldaBorde.BorderLeft = BorderStyle.Medium;
            estiloCeldaBorde.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            const int indexAux = indexColumn;

            #region Encabezados
            IRow renglonCabecero = hojaExcel.CreateRow(2);

            var lblFecha = new XSSFRichTextString(string.Format("Fecha: {0}", checkListGeneral.FechaInicio.ToString("dd/MM/yyyy")));
            lblFecha.ApplyFont(0, lblFecha.Length, fontEncabezado);
            ICell cellLblFecha = renglonCabecero.CreateCell(indexAux + 1);
            var celdaFecha = new CellRangeAddress(2, 2, 1, 2);
            hojaExcel.AddMergedRegion(celdaFecha);
            cellLblFecha.SetCellValue(lblFecha);

            var lblTurno = new XSSFRichTextString(string.Format("Turno: {0}", checkListGeneral.Turno));
            lblTurno.ApplyFont(0, lblTurno.Length, fontEncabezado);
            ICell cellLblTurno = renglonCabecero.CreateCell(indexAux + 4);
            var celdaTurno = new CellRangeAddress(2, 2, 4, 5);
            hojaExcel.AddMergedRegion(celdaTurno);
            cellLblTurno.SetCellValue(lblTurno);

            var lblResponsable = new XSSFRichTextString(string.Format("Responsable: {0}", checkListGeneral.NombreUsuario));
            lblResponsable.ApplyFont(0, lblResponsable.Length, fontEncabezado);
            ICell cellLblResponsable = renglonCabecero.CreateCell(indexAux + 7);
            var celdaResponsable = new CellRangeAddress(2, 2, 7, 12);
            hojaExcel.AddMergedRegion(celdaResponsable);
            cellLblResponsable.SetCellValue(lblResponsable);

            var lblRoladoras = new XSSFRichTextString(string.Format("Roladoras: {0}", valoresGeneral.Horometros.Count));
            lblRoladoras.ApplyFont(0, lblRoladoras.Length, fontEncabezado);
            ICell cellLblRoladoras = renglonCabecero.CreateCell(indexAux + 13);
            var celdaRoladoras = new CellRangeAddress(2, 2, 13, 14);
            hojaExcel.AddMergedRegion(celdaRoladoras);
            cellLblRoladoras.SetCellValue(lblRoladoras);

            var lblHoraInicio = new XSSFRichTextString(string.Format("Hora Inicio: {0}", checkListGeneral.FechaInicio.ToString("HH:mm")));
            lblHoraInicio.ApplyFont(0, lblHoraInicio.Length, fontEncabezado);
            ICell cellLblHoraInicio = renglonCabecero.CreateCell(indexAux + 15);
            var celdaHoraInicio = new CellRangeAddress(2, 2, 15, 16);
            hojaExcel.AddMergedRegion(celdaHoraInicio);
            cellLblHoraInicio.SetCellValue(lblHoraInicio);


            CheckListRoladoraHorometroInfo horaFinal =
                       valoresGeneral.Horometros.OrderBy(id => id.HorometroFinal)
                           .Last();

            var lblHoraFin = new XSSFRichTextString(string.Format("Hora Fin: {0}", horaFinal.HorometroFinal));
            lblHoraFin.ApplyFont(0, lblHoraFin.Length, fontEncabezado);
            ICell cellLblHoraFin = renglonCabecero.CreateCell(indexAux + 18);
            var celdaHoraFin = new CellRangeAddress(2, 2, 18, 19);
            hojaExcel.AddMergedRegion(celdaHoraFin);
            cellLblHoraFin.SetCellValue(lblHoraFin);

            #endregion Encabezados
            hojaExcel.SetColumnWidth(1, 4000);
            hojaExcel.SetColumnWidth(2, 5000);
            hojaExcel.SetColumnWidth(3, 3500);

            #region ColumnasDescripcion
            IRow renglon = hojaExcel.CreateRow(4);
            //IRow renglon5 = sheet1.CreateRow(5);

            var lblDescripcionParametros = new XSSFRichTextString("DESCRIPCIÓN PARÁMETRO");
            lblDescripcionParametros.ApplyFont(fontTitulos);
            ICell cellLblDescripcionParametros = renglon.CreateCell(indexAux + 1);
            cellLblDescripcionParametros.SetCellValue(lblDescripcionParametros);
            cellLblDescripcionParametros.CellStyle = estiloCeldaBorde;
            cellLblDescripcionParametros.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblDescripcionParametros.CellStyle.WrapText = true;

            var celdaParametros = new CellRangeAddress(4, 5, 1, 2);
            hojaExcel.AddMergedRegion(celdaParametros);

            var lblRango = new XSSFRichTextString("RANGO");
            lblRango.ApplyFont(fontTitulos);
            ICell cellLblRango = renglon.CreateCell(indexAux + 3);
            cellLblRango.SetCellValue(lblRango);
            cellLblRango.CellStyle = estiloCeldaBorde;
            cellLblRango.CellStyle.WrapText = true;

            var celdaRango = new CellRangeAddress(4, 5, 3, 3);
            hojaExcel.AddMergedRegion(celdaRango);

            renglon = hojaExcel.CreateRow(6);

            var lblHoraReal = new XSSFRichTextString("Hora real de inicio o paro");
            lblHoraReal.ApplyFont(fontTitulos);
            ICell cellLblHoraReal = renglon.CreateCell(indexAux + 1);
            cellLblHoraReal.SetCellValue(lblHoraReal);
            cellLblHoraReal.CellStyle = estiloCeldaBorde;
            cellLblDescripcionParametros.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblHoraReal.CellStyle.WrapText = true;

            var celdaHoraReal = new CellRangeAddress(6, 6, 1, 2);
            hojaExcel.AddMergedRegion(celdaHoraReal);

            var lblRol = new XSSFRichTextString("ROL");
            lblRol.ApplyFont(fontTitulos);
            ICell cellLblRol = renglon.CreateCell(indexAux + 3);
            cellLblRol.SetCellValue(lblRol);
            cellLblRol.CellStyle = estiloCeldaBorde;
            cellLblRol.CellStyle.WrapText = true;


            #region COCEDOR


            renglon = hojaExcel.CreateRow(7);

            var lblCocedor = new XSSFRichTextString("COCEDOR");
            lblCocedor.ApplyFont(fontTitulos);
            ICell cellLblCocedor = renglon.CreateCell(indexAux + 1);
            cellLblCocedor.SetCellValue(lblCocedor);
            cellLblCocedor.CellStyle = estiloCeldaBorde;
            cellLblCocedor.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblCocedor.CellStyle.WrapText = true;
            var celdaCocedor = new CellRangeAddress(7, 12, 1, 1);
            hojaExcel.AddMergedRegion(celdaCocedor);


            var lblPresion = new XSSFRichTextString("Presion de Vapor en cabezal de servicio");
            lblPresion.ApplyFont(fontTitulos);
            ICell cellLblPresion = renglon.CreateCell(indexAux + 2);
            cellLblPresion.SetCellValue(lblPresion);
            cellLblPresion.CellStyle = estiloCeldaBorde;
            cellLblPresion.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblPresion.CellStyle.WrapText = true;
            var celdaPresion = new CellRangeAddress(7, 9, 2, 2);
            hojaExcel.AddMergedRegion(celdaPresion);



            var lblMayor6 = new XSSFRichTextString("Mayor a 6 Kg");
            lblMayor6.ApplyFont(fontTitulos);
            ICell cellLblMayor6 = renglon.CreateCell(indexAux + 3);
            cellLblMayor6.SetCellValue(lblMayor6);
            cellLblMayor6.CellStyle = estiloCeldaBorde;
            cellLblMayor6.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMayor6.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(8);

            var lblEntre45 = new XSSFRichTextString("Entre 4-5 kg");
            lblEntre45.ApplyFont(fontTitulos);
            ICell cellLblEntre45 = renglon.CreateCell(indexAux + 3);
            cellLblEntre45.SetCellValue(lblEntre45);
            //esRowGeneral.SetFillForegroundColor(grisClaro);

            cellLblEntre45.CellStyle = celdaGrisBorde;
            //cellLblEntre45.CellStyle = celdaBorde;
            cellLblEntre45.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblEntre45.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(9);

            var lblMenor4 = new XSSFRichTextString("Menor a 5 Kg");
            lblMenor4.ApplyFont(fontTitulos);
            ICell cellLblMenor4 = renglon.CreateCell(indexAux + 3);
            cellLblMenor4.SetCellValue(lblMenor4);
            cellLblMenor4.CellStyle = estiloCeldaBorde;
            cellLblMenor4.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMenor4.CellStyle.WrapText = true;


            renglon = hojaExcel.CreateRow(10);

            var lblTemperatura = new XSSFRichTextString("Temperatura del termometro inferior");
            lblTemperatura.ApplyFont(fontTitulos);
            ICell cellLblTemperatura = renglon.CreateCell(indexAux + 2);
            cellLblTemperatura.SetCellValue(lblTemperatura);
            cellLblTemperatura.CellStyle = estiloCeldaBorde;
            cellLblTemperatura.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblTemperatura.CellStyle.WrapText = true;
            var celdaPresioTemperatura = new CellRangeAddress(10, 12, 2, 2);
            hojaExcel.AddMergedRegion(celdaPresioTemperatura);



            var lblMayor105 = new XSSFRichTextString("Mayor a 105 °C");
            lblMayor105.ApplyFont(fontTitulos);
            ICell cellLblMayor105 = renglon.CreateCell(indexAux + 3);
            cellLblMayor105.SetCellValue(lblMayor105);
            cellLblMayor105.CellStyle = estiloCeldaBorde;
            cellLblMayor105.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMayor105.CellStyle.WrapText = true;


            renglon = hojaExcel.CreateRow(11);

            var lblEntre95 = new XSSFRichTextString("Entre 95 a 105 °C");
            lblEntre95.ApplyFont(fontTitulos);
            ICell cellLblEntre95 = renglon.CreateCell(indexAux + 3);
            cellLblEntre95.SetCellValue(lblEntre95);
            cellLblEntre95.CellStyle = estiloCeldaBorde;
            cellLblEntre95.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblEntre95.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(12);

            var lblMenor95 = new XSSFRichTextString("Menor a 95 °C");
            lblMenor95.ApplyFont(fontTitulos);
            ICell cellLblMenor95 = renglon.CreateCell(indexAux + 3);
            cellLblMenor95.SetCellValue(lblMenor95);
            cellLblMenor95.CellStyle = estiloCeldaBorde;
            cellLblMenor95.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMenor95.CellStyle.WrapText = true;

            #endregion COCEDOR

            #region AMPERAJE

            renglon = hojaExcel.CreateRow(13);

            var lblAmperaje = new XSSFRichTextString("Amperaje (motor derecho)");
            lblAmperaje.ApplyFont(fontTitulos);
            ICell cellLblAmperaje = renglon.CreateCell(indexAux + 1);
            cellLblAmperaje.SetCellValue(lblAmperaje);
            cellLblAmperaje.CellStyle = estiloCeldaBorde;
            cellLblAmperaje.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblAmperaje.CellStyle.WrapText = true;
            var celdaAmperaje = new CellRangeAddress(13, 15, 1, 1);
            hojaExcel.AddMergedRegion(celdaAmperaje);


            var lblRoladoraAmperaje = new XSSFRichTextString("Roladora 1 entre 25-30 Roladora 2 entre 25-30 Roladora 3 entre 85-90 Roladora 4 entre 40-45");
            lblRoladoraAmperaje.ApplyFont(fontTitulos);
            ICell cellLblRoladoraAmperaje = renglon.CreateCell(indexAux + 2);
            cellLblRoladoraAmperaje.SetCellValue(lblRoladoraAmperaje);
            cellLblRoladoraAmperaje.CellStyle = estiloCeldaBorde;
            cellLblRoladoraAmperaje.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblRoladoraAmperaje.CellStyle.WrapText = true;


            var celdaRoladoraAmperaje = new CellRangeAddress(13, 15, 2, 2);
            hojaExcel.AddMergedRegion(celdaRoladoraAmperaje);

            var lblMayor = new XSSFRichTextString("Mayor");
            lblMayor.ApplyFont(fontTitulos);
            ICell cellLblMayor = renglon.CreateCell(indexAux + 3);
            cellLblMayor.SetCellValue(lblMayor);
            cellLblMayor.CellStyle = estiloCeldaBorde;
            cellLblMayor.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMayor.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(14);

            var lblOptimo = new XSSFRichTextString("Optimo");
            lblOptimo.ApplyFont(fontTitulos);
            ICell cellLblOptimo = renglon.CreateCell(indexAux + 3);
            cellLblOptimo.SetCellValue(lblOptimo);
            cellLblOptimo.CellStyle = estiloCeldaBorde;
            cellLblOptimo.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblOptimo.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(15);

            var lblMenor = new XSSFRichTextString("Menor");
            lblMenor.ApplyFont(fontTitulos);
            ICell cellLblMenor = renglon.CreateCell(indexAux + 3);
            cellLblMenor.SetCellValue(lblMenor);
            cellLblMenor.CellStyle = estiloCeldaBorde;
            cellLblMenor.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMenor.CellStyle.WrapText = true;

            cellLblMenor.Row.Height = 600;
            #endregion AMPERAJE

            #region CALIDADROLADO

            renglon = hojaExcel.CreateRow(16);

            var lblCalidadRolado = new XSSFRichTextString("CALIDAD DE ROLADO");
            lblCalidadRolado.ApplyFont(fontTitulos);
            ICell cellLblCalidadRolado = renglon.CreateCell(indexAux + 1);
            cellLblCalidadRolado.SetCellValue(lblCalidadRolado);
            cellLblCalidadRolado.CellStyle = estiloCeldaBorde;
            cellLblCalidadRolado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblCalidadRolado.CellStyle.WrapText = true;
            var celdaCalidadRolado = new CellRangeAddress(16, 28, 1, 1);
            hojaExcel.AddMergedRegion(celdaCalidadRolado);


            var lblPresenciaFinos = new XSSFRichTextString("presencia de finos");
            lblPresenciaFinos.ApplyFont(fontTitulos);
            ICell cellLblPresenciaFinos = renglon.CreateCell(indexAux + 2);
            cellLblPresenciaFinos.SetCellValue(lblPresenciaFinos);
            cellLblPresenciaFinos.CellStyle = estiloCeldaBorde;
            cellLblPresenciaFinos.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblPresenciaFinos.CellStyle.WrapText = true;
            var celdaPresenciaFinos = new CellRangeAddress(16, 17, 2, 2);
            hojaExcel.AddMergedRegion(celdaPresenciaFinos);

            var lblMayor8 = new XSSFRichTextString("Mayor a 8");
            lblMayor8.ApplyFont(fontTitulos);
            ICell cellLblMayor8 = renglon.CreateCell(indexAux + 3);
            cellLblMayor8.SetCellValue(lblMayor8);
            cellLblMayor8.CellStyle = estiloCeldaBorde;
            cellLblMayor8.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMayor8.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(17);

            var lblMenor8 = new XSSFRichTextString("Menor o igual a 8.0");
            lblMenor8.ApplyFont(fontTitulos);
            ICell cellLblMenor8 = renglon.CreateCell(indexAux + 3);
            cellLblMenor8.SetCellValue(lblMenor8);
            cellLblMenor8.CellStyle = estiloCeldaBorde;
            cellLblMenor8.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMenor8.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(18);

            var lblGranoEntero = new XSSFRichTextString("% grano entero");
            lblGranoEntero.ApplyFont(fontTitulos);
            ICell cellLblGranoEntero = renglon.CreateCell(indexAux + 2);
            cellLblGranoEntero.SetCellValue(lblGranoEntero);
            cellLblGranoEntero.CellStyle = estiloCeldaBorde;
            cellLblGranoEntero.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblGranoEntero.CellStyle.WrapText = true;
            var celdaGranoEntero = new CellRangeAddress(18, 19, 2, 2);
            hojaExcel.AddMergedRegion(celdaGranoEntero);

            var lblMayor1 = new XSSFRichTextString("Mayor o igual a 1 %");
            lblMayor1.ApplyFont(fontTitulos);
            ICell cellLblMayor1 = renglon.CreateCell(indexAux + 3);
            cellLblMayor1.SetCellValue(lblMayor1);
            cellLblMayor1.CellStyle = estiloCeldaBorde;
            cellLblMayor1.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMayor1.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(19);

            var lblEntre0 = new XSSFRichTextString("Igual a 0 %");
            lblEntre0.ApplyFont(fontTitulos);
            ICell cellLblEntre0 = renglon.CreateCell(indexAux + 3);
            cellLblEntre0.SetCellValue(lblEntre0);
            cellLblEntre0.CellStyle = estiloCeldaBorde;
            cellLblEntre0.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblEntre0.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(20);

            var lblHumedad = new XSSFRichTextString("Humedad");
            lblHumedad.ApplyFont(fontTitulos);
            ICell cellLblHumedad = renglon.CreateCell(indexAux + 2);
            cellLblHumedad.SetCellValue(lblHumedad);
            cellLblHumedad.CellStyle = estiloCeldaBorde;
            cellLblHumedad.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblHumedad.CellStyle.WrapText = true;
            var celdaHumedad = new CellRangeAddress(20, 22, 2, 2);
            hojaExcel.AddMergedRegion(celdaHumedad);

            var lblMayor24 = new XSSFRichTextString("Mayor a  24.5 %");
            lblMayor24.ApplyFont(fontTitulos);
            ICell cellLblMayor24 = renglon.CreateCell(indexAux + 3);
            cellLblMayor24.SetCellValue(lblMayor24);
            cellLblMayor24.CellStyle = estiloCeldaBorde;
            cellLblMayor24.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMayor24.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(21);

            var lblEntre21 = new XSSFRichTextString("Entre 21 y  24.5%");
            lblEntre21.ApplyFont(fontTitulos);
            ICell cellLblEntre21 = renglon.CreateCell(indexAux + 3);
            cellLblEntre21.SetCellValue(lblEntre21);
            cellLblEntre21.CellStyle = estiloCeldaBorde;
            cellLblEntre21.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblEntre21.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(22);

            var lblMenor21 = new XSSFRichTextString("Menor a 21 %");
            lblMenor21.ApplyFont(fontTitulos);
            ICell cellLblMenor21 = renglon.CreateCell(indexAux + 3);
            cellLblMenor21.SetCellValue(lblMenor21);
            cellLblMenor21.CellStyle = estiloCeldaBorde;
            cellLblMenor21.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMenor21.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(23);

            var lblDensidadCaliente = new XSSFRichTextString("Densidad Lbs/bushel en Caliente");
            lblDensidadCaliente.ApplyFont(fontTitulos);
            ICell cellLblDensidadCaliente = renglon.CreateCell(indexAux + 2);
            cellLblDensidadCaliente.SetCellValue(lblDensidadCaliente);
            cellLblDensidadCaliente.CellStyle = estiloCeldaBorde;
            cellLblDensidadCaliente.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblDensidadCaliente.CellStyle.WrapText = true;
            var celdaDensidadCaliente = new CellRangeAddress(23, 25, 2, 2);
            hojaExcel.AddMergedRegion(celdaDensidadCaliente);

            var lblMayorA = new XSSFRichTextString("Mayor a   ");
            lblMayorA.ApplyFont(fontTitulos);
            ICell cellLblMayorA = renglon.CreateCell(indexAux + 3);
            cellLblMayorA.SetCellValue(lblMayorA);
            cellLblMayorA.CellStyle = estiloCeldaBorde;
            cellLblMayorA.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMayorA.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(24);

            var lblEntreY = new XSSFRichTextString("Entre       y    ");
            lblEntreY.ApplyFont(fontTitulos);
            ICell cellLblEntreY = renglon.CreateCell(indexAux + 3);
            cellLblEntreY.SetCellValue(lblEntreY);
            cellLblEntreY.CellStyle = estiloCeldaBorde;
            cellLblEntreY.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblEntreY.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(25);

            var lblMenorQue = new XSSFRichTextString("Menor que");
            lblMenorQue.ApplyFont(fontTitulos);
            ICell cellLblMenorQue = renglon.CreateCell(indexAux + 3);
            cellLblMenorQue.SetCellValue(lblMenorQue);
            cellLblMenorQue.CellStyle = estiloCeldaBorde;
            cellLblMenorQue.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMenorQue.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(26);

            var lblDensidadTemporizado = new XSSFRichTextString("Densidad Lbs/bushel a Temporizado");
            lblDensidadTemporizado.ApplyFont(fontTitulos);
            ICell cellLblDensidadTemporizado = renglon.CreateCell(indexAux + 2);
            cellLblDensidadTemporizado.SetCellValue(lblDensidadTemporizado);
            cellLblDensidadTemporizado.CellStyle = estiloCeldaBorde;
            cellLblDensidadTemporizado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblDensidadTemporizado.CellStyle.WrapText = true;
            var celdaDensidadTemporizado = new CellRangeAddress(26, 28, 2, 2);
            hojaExcel.AddMergedRegion(celdaDensidadTemporizado);

            var lblMayorATemporizado = new XSSFRichTextString(string.Empty);
            lblMayorATemporizado.ApplyFont(fontTitulos);
            ICell cellLblMayorATemporizado = renglon.CreateCell(indexAux + 3);
            cellLblMayorATemporizado.SetCellValue(lblMayorATemporizado);
            cellLblMayorATemporizado.CellStyle = estiloCeldaBorde;
            cellLblMayorATemporizado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMayorATemporizado.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(27);

            var lblEntreYTemporizado = new XSSFRichTextString(string.Empty);
            lblEntreYTemporizado.ApplyFont(fontTitulos);
            ICell cellLblEntreYTemporizado = renglon.CreateCell(indexAux + 3);
            cellLblEntreYTemporizado.SetCellValue(lblEntreYTemporizado);
            cellLblEntreYTemporizado.CellStyle = estiloCeldaBorde;
            cellLblEntreYTemporizado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblEntreYTemporizado.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(28);

            var lblMenorQueTemporizado = new XSSFRichTextString(string.Empty);
            lblMenorQueTemporizado.ApplyFont(fontTitulos);
            ICell cellLblMenorQueTemporizado = renglon.CreateCell(indexAux + 3);
            cellLblMenorQueTemporizado.SetCellValue(lblMenorQueTemporizado);
            cellLblMenorQueTemporizado.CellStyle = estiloCeldaBorde;
            cellLblMenorQueTemporizado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblMenorQueTemporizado.CellStyle.WrapText = true;

            #endregion CALIDADROLADO

            renglon = hojaExcel.CreateRow(29);


            var lblVisitaJefe = new XSSFRichTextString("Visita Jefe PA");
            lblVisitaJefe.ApplyFont(fontTitulos);
            ICell cellLblVisitaJefe = renglon.CreateCell(indexAux + 1);
            cellLblVisitaJefe.SetCellValue(lblVisitaJefe);
            cellLblVisitaJefe.CellStyle = estiloCeldaBorde;
            cellLblVisitaJefe.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblVisitaJefe.CellStyle.WrapText = true;
            var celdaVisitaJefe = new CellRangeAddress(29, 29, 1, 3);
            hojaExcel.AddMergedRegion(celdaVisitaJefe);


            #region Renglon Horometros

            #region Table Horometro

            renglon = hojaExcel.CreateRow(30);

            var lblInicioRoladora = new XSSFRichTextString("Inicio");
            lblInicioRoladora.ApplyFont(fontTitulos);
            ICell cellLblInicioRoladora = renglon.CreateCell(indexAux + 2);
            cellLblInicioRoladora.SetCellValue(lblInicioRoladora);
            //cellLblInicioRoladora.CellStyle = celdaBorde;
            cellLblInicioRoladora.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            //cellLblInicioRoladora.CellStyle.WrapText = true;

            var lblFinalRoladora = new XSSFRichTextString("Final");
            lblFinalRoladora.ApplyFont(fontTitulos);
            ICell cellLblFinalRoladora = renglon.CreateCell(indexAux + 3);
            cellLblFinalRoladora.SetCellValue(lblFinalRoladora);
            //cellLblFinalRoladora.CellStyle = celdaBorde;
            cellLblFinalRoladora.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            //cellLblFinalRoladora.CellStyle.WrapText = true;

            var lblHorasOperacion = new XSSFRichTextString("Horas Operación");
            lblHorasOperacion.ApplyFont(fontTitulos);
            ICell cellLblHorasOperacion = renglon.CreateCell(indexAux + 4);
            cellLblHorasOperacion.SetCellValue(lblHorasOperacion);
            //cellLblHorasOperacion.CellStyle = celdaBorde;
            cellLblHorasOperacion.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            cellLblHorasOperacion.CellStyle.WrapText = true;

            int indexRow = 31;
            int auxiliarRoladoras = 0;
            for (int j = indexRow; j < indexRow + 5; j++)
            {
                renglon = hojaExcel.CreateRow(j);
                var horometroRoladora = valoresGeneral.Horometros[auxiliarRoladoras];
                auxiliarRoladoras++;
                var lblHorometroRoladora = new XSSFRichTextString(string.Format("Horometro Roladora {0}", auxiliarRoladoras));
                lblHorometroRoladora.ApplyFont(fontTitulos);
                ICell cellLblHorometroRoladora = renglon.CreateCell(indexAux + 1);
                cellLblHorometroRoladora.SetCellValue(lblHorometroRoladora);
                cellLblHorometroRoladora.CellStyle = estiloCeldaBorde;
                cellLblHorometroRoladora.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

                var lblHorometroInicioRoladora = new XSSFRichTextString(horometroRoladora.HorometroInicial);//Hora Inicio Roladora 1
                lblHorometroInicioRoladora.ApplyFont(fontTitulos);
                ICell cellLblHorometroInicioRoladora = renglon.CreateCell(indexAux + 2);
                cellLblHorometroInicioRoladora.SetCellValue(lblHorometroInicioRoladora);
                cellLblHorometroInicioRoladora.CellStyle = estiloCeldaBorde;
                cellLblHorometroInicioRoladora.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

                var lblHorometroFinalRoladora = new XSSFRichTextString(horometroRoladora.HorometroFinal); //Hora Final Roladora 1
                lblHorometroFinalRoladora.ApplyFont(fontTitulos);
                ICell cellLblHorometroFinalRoladora = renglon.CreateCell(indexAux + 3);
                cellLblHorometroFinalRoladora.SetCellValue(lblHorometroFinalRoladora);
                cellLblHorometroFinalRoladora.CellStyle = estiloCeldaBorde;
                cellLblHorometroFinalRoladora.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

                var lblHorometroOperando = new XSSFRichTextString(horometroRoladora.HorasOperacion); //Valor Operando Hora Final Roladora 1
                lblHorometroOperando.ApplyFont(fontTitulos);
                ICell cellLblHorometroOperando = renglon.CreateCell(indexAux + 4);
                cellLblHorometroOperando.SetCellValue(lblHorometroOperando);
                cellLblHorometroOperando.CellStyle = estiloCeldaBorde;
                cellLblHorometroOperando.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            }

            #endregion Table Horometro

            #region TablaSurfactante

            renglon = hojaExcel.GetRow(30);

            var lblInicioReporteLab = new XSSFRichTextString("Reporte de Lab.");
            lblInicioReporteLab.ApplyFont(fontTitulos);
            ICell cellLblReporteLab = renglon.CreateCell(indexAux + 9);
            cellLblReporteLab.SetCellValue(lblInicioReporteLab);
            cellLblReporteLab.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(31);

            var lblHumedadGranoRolado = new XSSFRichTextString("Humedad Grano Rolado en Bodega");
            lblHumedadGranoRolado.ApplyFont(fontTitulos);
            ICell cellLblHumedadGranoRolado = renglon.CreateCell(indexAux + 6);
            cellLblHumedadGranoRolado.SetCellValue(lblHumedadGranoRolado);
            cellLblHumedadGranoRolado.CellStyle = estiloCeldaBorde;
            cellLblHumedadGranoRolado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaHumedadGranoRolado = new CellRangeAddress(31, 31, 6, 8);
            hojaExcel.AddMergedRegion(celdaHumedadGranoRolado);

            var lblValorHumedadGranoRolado = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.HumedadGranoRoladoBodega));//Valor Humedad Grano Rolado
            lblValorHumedadGranoRolado.ApplyFont(fontTitulos);
            ICell cellLblValorHumedadGranoRolado = renglon.CreateCell(indexAux + 9);
            cellLblValorHumedadGranoRolado.SetCellValue(lblValorHumedadGranoRolado);
            cellLblValorHumedadGranoRolado.CellStyle = estiloCeldaBorde;
            cellLblValorHumedadGranoRolado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(32);

            var lblHumedadGranoEntero = new XSSFRichTextString("Humedad Grano Entero en Bodega");
            lblHumedadGranoEntero.ApplyFont(fontTitulos);
            ICell cellLblHumedadGranoEntero = renglon.CreateCell(indexAux + 6);
            cellLblHumedadGranoEntero.SetCellValue(lblHumedadGranoEntero);
            cellLblHumedadGranoEntero.CellStyle = estiloCeldaBorde;
            cellLblHumedadGranoEntero.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaHumedadGranoEntero = new CellRangeAddress(32, 32, 6, 8);
            hojaExcel.AddMergedRegion(celdaHumedadGranoEntero);

            var lblValorHumedadGranoEntero = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.HumedadGranoEnteroBodega));//Valor Humedad Grano Entero
            lblValorHumedadGranoEntero.ApplyFont(fontTitulos);
            ICell cellLblValorHumedadGranoEntero = renglon.CreateCell(indexAux + 9);
            cellLblValorHumedadGranoEntero.SetCellValue(lblValorHumedadGranoEntero);
            cellLblValorHumedadGranoEntero.CellStyle = estiloCeldaBorde;
            cellLblValorHumedadGranoEntero.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(33);

            var lblSuperavitAdicion = new XSSFRichTextString("Superavit Adición de Agua/Surfactante");
            lblSuperavitAdicion.ApplyFont(fontTitulos);
            ICell cellLblSuperavitAdicion = renglon.CreateCell(indexAux + 6);
            cellLblSuperavitAdicion.SetCellValue(lblSuperavitAdicion);
            cellLblSuperavitAdicion.CellStyle = estiloCeldaBorde;
            cellLblSuperavitAdicion.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaSuperavitAdicion = new CellRangeAddress(33, 33, 6, 8);
            hojaExcel.AddMergedRegion(celdaSuperavitAdicion);

            var lblValorSuperavitAdicion = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.SuperavitAdicionAguaSurfactante));//Valor Superavit Adicion
            lblValorSuperavitAdicion.ApplyFont(fontTitulos);
            ICell cellLblValorSuperavitAdicion = renglon.CreateCell(indexAux + 9);
            cellLblValorSuperavitAdicion.SetCellValue(lblValorSuperavitAdicion);
            cellLblValorSuperavitAdicion.CellStyle = estiloCeldaBorde;
            cellLblValorSuperavitAdicion.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(34);

            var lblSurfactanteInicio = new XSSFRichTextString("Inicio");
            lblSurfactanteInicio.ApplyFont(fontTitulos);
            ICell cellLblSurfactanteInicio = renglon.CreateCell(indexAux + 8);
            cellLblSurfactanteInicio.SetCellValue(lblSurfactanteInicio);
            cellLblSurfactanteInicio.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblSurfactanteFinal = new XSSFRichTextString("Final");
            lblSurfactanteFinal.ApplyFont(fontTitulos);
            ICell cellLblSurfactanteFinal = renglon.CreateCell(indexAux + 9);
            cellLblSurfactanteFinal.SetCellValue(lblSurfactanteFinal);
            cellLblSurfactanteFinal.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            GenerarBordes(hojaExcel, 31, 33, 6, 9, estiloCeldaBorde);

            renglon = hojaExcel.GetRow(35);

            var lblSurfactante = new XSSFRichTextString("Surfactante");
            lblSurfactante.ApplyFont(fontTitulos);
            ICell cellLblSurfactante = renglon.CreateCell(indexAux + 6);
            cellLblSurfactante.SetCellValue(lblSurfactante);
            cellLblSurfactante.CellStyle = estiloCeldaBorde;
            cellLblSurfactante.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaSurfactanteo = new CellRangeAddress(35, 35, 6, 7);
            hojaExcel.AddMergedRegion(celdaSurfactanteo);

            var lblValorSurfactanteInicio = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.SurfactanteInicio));//Valor de inicio del surfactante
            lblValorSurfactanteInicio.ApplyFont(fontTitulos);
            ICell cellLblValorSurfactanteInicio = renglon.CreateCell(indexAux + 8);
            cellLblValorSurfactanteInicio.SetCellValue(lblValorSurfactanteInicio);
            cellLblValorSurfactanteInicio.CellStyle = estiloCeldaBorde;
            cellLblValorSurfactanteInicio.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblValorSurfactanteFinal = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.SurfactanteFinal));//Valor de Fin del surfactante
            lblValorSurfactanteFinal.ApplyFont(fontTitulos);
            ICell cellLblValorSurfactanteFinal = renglon.CreateCell(indexAux + 9);
            cellLblValorSurfactanteFinal.SetCellValue(lblValorSurfactanteFinal);
            cellLblValorSurfactanteFinal.CellStyle = estiloCeldaBorde;
            cellLblValorSurfactanteFinal.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            GenerarBordes(hojaExcel, 35, 35, 6, 8, estiloCeldaBorde);

            #endregion TablaSurfactante

            #region TablaConsumoAgua
            renglon = hojaExcel.GetRow(31);


            var lblContadorInicialAgua = new XSSFRichTextString("Contador Inicial de Agua");
            lblContadorInicialAgua.ApplyFont(fontTitulos);
            ICell cellLblContadorInicialAgua = renglon.CreateCell(indexAux + 11);
            cellLblContadorInicialAgua.SetCellValue(lblContadorInicialAgua);
            cellLblContadorInicialAgua.CellStyle = estiloCeldaBorde;
            cellLblContadorInicialAgua.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaContadorInicialAgua = new CellRangeAddress(31, 31, 11, 12);
            hojaExcel.AddMergedRegion(celdaContadorInicialAgua);


            var lblValorContadorInicialAgua = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.ContadorAguaInicial));//Valor Contador Final Agua
            lblValorContadorInicialAgua.ApplyFont(fontTitulos);
            ICell cellLblValorContadorInicialAgua = renglon.CreateCell(indexAux + 13);
            cellLblValorContadorInicialAgua.SetCellValue(lblValorContadorInicialAgua);
            cellLblValorContadorInicialAgua.CellStyle = estiloCeldaBorde;
            cellLblValorContadorInicialAgua.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(32);


            var lblContadorFinalAgua = new XSSFRichTextString("Contador Final de Agua");
            lblContadorFinalAgua.ApplyFont(fontTitulos);
            ICell cellLblContadorFinalAgua = renglon.CreateCell(indexAux + 11);
            cellLblContadorFinalAgua.SetCellValue(lblContadorFinalAgua);
            cellLblContadorFinalAgua.CellStyle = estiloCeldaBorde;
            cellLblContadorFinalAgua.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaContadorFinalAgua = new CellRangeAddress(32, 32, 11, 12);
            hojaExcel.AddMergedRegion(celdaContadorFinalAgua);


            var lblValorContadorFinalAgua = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.ContadorAguaFinal));//Valor Contador Final Agua
            lblValorContadorFinalAgua.ApplyFont(fontTitulos);
            ICell cellLblValorContadorFinalAgua = renglon.CreateCell(indexAux + 13);
            cellLblValorContadorFinalAgua.SetCellValue(lblValorContadorFinalAgua);
            cellLblValorContadorFinalAgua.CellStyle = estiloCeldaBorde;
            cellLblValorContadorFinalAgua.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;


            renglon = hojaExcel.GetRow(33);


            var lblConsumoAguaGalon = new XSSFRichTextString("Consumo de Agua Galon");
            lblConsumoAguaGalon.ApplyFont(fontTitulos);
            ICell cellLblConsumoAguaGalon = renglon.CreateCell(indexAux + 11);
            cellLblConsumoAguaGalon.SetCellValue(lblConsumoAguaGalon);
            cellLblConsumoAguaGalon.CellStyle = estiloCeldaBorde;
            cellLblConsumoAguaGalon.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaConsumoAguaGalon = new CellRangeAddress(33, 33, 11, 12);
            hojaExcel.AddMergedRegion(celdaConsumoAguaGalon);


            var lblValorConsumoAguaGalon = new XSSFRichTextString(string.Format("{0}", Math.Round(checkListGeneral.ParametrosCheckListRolado.ConsumoAguaLitro / (decimal)3.7854),2));//Valor Contador Final Agua
            lblValorConsumoAguaGalon.ApplyFont(fontTitulos);
            ICell cellLblValorConsumoAguaGalon = renglon.CreateCell(indexAux + 13);
            cellLblValorConsumoAguaGalon.SetCellValue(lblValorConsumoAguaGalon);
            cellLblValorConsumoAguaGalon.CellStyle = estiloCeldaBorde;
            cellLblValorConsumoAguaGalon.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(34);


            var lblConsumoAguaLitro = new XSSFRichTextString("Consumo de Agua Litro");
            lblConsumoAguaLitro.ApplyFont(fontTitulos);
            ICell cellLblConsumoAguaLitro = renglon.CreateCell(indexAux + 11);
            cellLblConsumoAguaLitro.SetCellValue(lblConsumoAguaLitro);
            cellLblConsumoAguaLitro.CellStyle = estiloCeldaBorde;
            cellLblConsumoAguaLitro.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaConsumoAguaLitro = new CellRangeAddress(34, 34, 11, 12);
            hojaExcel.AddMergedRegion(celdaConsumoAguaLitro);


            var lblValorConsumoAguaLitro = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.ConsumoAguaLitro));//Valor Contador Final Agua
            lblValorConsumoAguaLitro.ApplyFont(fontTitulos);
            ICell cellLblValorConsumoAguaLitro = renglon.CreateCell(indexAux + 13);
            cellLblValorConsumoAguaLitro.SetCellValue(lblValorConsumoAguaLitro);
            cellLblValorConsumoAguaLitro.CellStyle = estiloCeldaBorde;
            cellLblValorConsumoAguaLitro.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            GenerarBordes(hojaExcel, 31, 34, 11, 13, estiloCeldaBorde);

            #endregion TablaConsumoAgua

            #region TablaGrano

            renglon = hojaExcel.GetRow(31);


            var lblTotalGranoEntero = new XSSFRichTextString("Total Grano Entero PP");
            lblTotalGranoEntero.ApplyFont(fontTitulos);
            ICell cellLblTotalGranoEntero = renglon.CreateCell(indexAux + 15);
            cellLblTotalGranoEntero.SetCellValue(lblTotalGranoEntero);
            cellLblTotalGranoEntero.CellStyle = estiloCeldaBorde;
            cellLblTotalGranoEntero.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaTotalGranoEntero = new CellRangeAddress(31, 31, 15, 16);
            hojaExcel.AddMergedRegion(celdaTotalGranoEntero);



            var lblValorTotalGranoEntero = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.TotalGranoEntreroPP));//Valor Contador Final Agua
            lblValorTotalGranoEntero.ApplyFont(fontTitulos);
            ICell cellLblValorTotalGranoEntero = renglon.CreateCell(indexAux + 17);
            cellLblValorTotalGranoEntero.SetCellValue(lblValorTotalGranoEntero);
            cellLblValorTotalGranoEntero.CellStyle = estiloCeldaBorde;
            cellLblValorTotalGranoEntero.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(32);


            var lblTotalGranoEnteroBodega = new XSSFRichTextString("Total Final Grano Ent. Bodega");
            lblTotalGranoEnteroBodega.ApplyFont(fontTitulos);
            ICell cellLblTotalGranoEnteroBodega = renglon.CreateCell(indexAux + 15);
            cellLblTotalGranoEnteroBodega.SetCellValue(lblTotalGranoEnteroBodega);
            cellLblTotalGranoEnteroBodega.CellStyle = estiloCeldaBorde;
            cellLblTotalGranoEnteroBodega.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaTotalGranoEnteroBodega = new CellRangeAddress(32, 32, 15, 16);
            hojaExcel.AddMergedRegion(celdaTotalGranoEnteroBodega);



            var lblValorTotalGranoEnteroBodega = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.TotalGranoEnteroBodega));//Valor Contador Final Agua
            lblValorTotalGranoEnteroBodega.ApplyFont(fontTitulos);
            ICell cellLblValorTotalGranoEnteroBodega = renglon.CreateCell(indexAux + 17);
            cellLblValorTotalGranoEnteroBodega.SetCellValue(lblValorTotalGranoEnteroBodega);
            cellLblValorTotalGranoEnteroBodega.CellStyle = estiloCeldaBorde;
            cellLblValorTotalGranoEnteroBodega.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(33);


            var lblTotalGranoProcesado = new XSSFRichTextString("Total Grano Procesado");
            lblTotalGranoProcesado.ApplyFont(fontTitulos);
            ICell cellLblTotalGranoProcesado = renglon.CreateCell(indexAux + 15);
            cellLblTotalGranoProcesado.SetCellValue(lblTotalGranoProcesado);
            cellLblTotalGranoProcesado.CellStyle = estiloCeldaBorde;
            cellLblTotalGranoProcesado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaTotalGranoProcesado = new CellRangeAddress(33, 33, 15, 16);
            hojaExcel.AddMergedRegion(celdaTotalGranoProcesado);



            var lblValorTotalGranoProcesado = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.TotalGranoProcesado));//Valor Contador Final Agua
            lblValorTotalGranoProcesado.ApplyFont(fontTitulos);
            ICell cellLblValorTotalGranoProcesado = renglon.CreateCell(indexAux + 17);
            cellLblValorTotalGranoProcesado.SetCellValue(lblValorTotalGranoProcesado);
            cellLblValorTotalGranoProcesado.CellStyle = estiloCeldaBorde;
            cellLblValorTotalGranoProcesado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(34);


            var lblSuperavitGranoRolado = new XSSFRichTextString("Superavit Grano Rolado");
            lblSuperavitGranoRolado.ApplyFont(fontTitulos);
            ICell cellLblSuperavitGranoRolado = renglon.CreateCell(indexAux + 15);
            cellLblSuperavitGranoRolado.SetCellValue(lblSuperavitGranoRolado);
            cellLblSuperavitGranoRolado.CellStyle = estiloCeldaBorde;
            cellLblSuperavitGranoRolado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaSuperavitGranoRolado = new CellRangeAddress(34, 34, 15, 16);
            hojaExcel.AddMergedRegion(celdaSuperavitGranoRolado);



            var lblValorSuperavitGranoRolado = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.SuperavitGranoRolado));//Valor Contador Final Agua
            lblValorSuperavitGranoRolado.ApplyFont(fontTitulos);
            ICell cellLblValorSuperavitGranoRolado = renglon.CreateCell(indexAux + 17);
            cellLblValorSuperavitGranoRolado.SetCellValue(lblValorSuperavitGranoRolado);
            cellLblValorSuperavitGranoRolado.CellStyle = estiloCeldaBorde;
            cellLblValorSuperavitGranoRolado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(35);


            var lblTotalGranoRolado = new XSSFRichTextString("Total Grano Rolado");
            lblTotalGranoRolado.ApplyFont(fontTitulos);
            ICell cellLblTotalGranoRolado = renglon.CreateCell(indexAux + 15);
            cellLblTotalGranoRolado.SetCellValue(lblTotalGranoRolado);
            cellLblTotalGranoRolado.CellStyle = estiloCeldaBorde;
            cellLblTotalGranoRolado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaTotalGranoRolado = new CellRangeAddress(35, 35, 15, 16);
            hojaExcel.AddMergedRegion(celdaTotalGranoRolado);



            var lblValorTotalGranoRolado = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.TotalGranoRolado));//Valor Contador Final Agua
            lblValorTotalGranoRolado.ApplyFont(fontTitulos);
            ICell cellLblValorTotalGranoRolado = renglon.CreateCell(indexAux + 17);
            cellLblValorTotalGranoRolado.SetCellValue(lblValorTotalGranoRolado);
            cellLblValorTotalGranoRolado.CellStyle = estiloCeldaBorde;
            cellLblValorTotalGranoRolado.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;


            GenerarBordes(hojaExcel, 31, 35, 15, 17, estiloCeldaBorde);

            #endregion TablaGrano

            #region TablaDiesel

            renglon = hojaExcel.GetRow(31);


            var lblConsumoDieselCalderas = new XSSFRichTextString("Consumo Diesel Calderas");
            lblConsumoDieselCalderas.ApplyFont(fontTitulos);
            ICell cellLblConsumoDieselCalderas = renglon.CreateCell(indexAux + 19);
            cellLblConsumoDieselCalderas.SetCellValue(lblConsumoDieselCalderas);
            cellLblConsumoDieselCalderas.CellStyle = estiloCeldaBorde;
            cellLblConsumoDieselCalderas.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaConsumoDieselCalderas = new CellRangeAddress(31, 31, 19, 20);
            hojaExcel.AddMergedRegion(celdaConsumoDieselCalderas);

            var lblValorConsumoDieselCalderas = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.ConsumoDieselCalderas));//Valor Contador Final Agua
            lblValorConsumoDieselCalderas.ApplyFont(fontTitulos);
            ICell cellLblValorConsumoDieselCalderas = renglon.CreateCell(indexAux + 21);
            cellLblValorConsumoDieselCalderas.SetCellValue(lblValorConsumoDieselCalderas);
            cellLblValorConsumoDieselCalderas.CellStyle = estiloCeldaBorde;
            cellLblValorConsumoDieselCalderas.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.GetRow(32);


            var lblConsumoDieselTonelada = new XSSFRichTextString("Diesel/Tonelada de Grano Rolado");
            lblConsumoDieselTonelada.ApplyFont(fontTitulos);
            ICell cellLblConsumoDieselTonelada = renglon.CreateCell(indexAux + 19);
            cellLblConsumoDieselTonelada.SetCellValue(lblConsumoDieselTonelada);
            cellLblConsumoDieselTonelada.CellStyle = estiloCeldaBorde;
            cellLblConsumoDieselTonelada.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaConsumoDieselTonelada = new CellRangeAddress(32, 32, 19, 20);
            hojaExcel.AddMergedRegion(celdaConsumoDieselTonelada);

            var lblValorConsumoDieselTonelada = new XSSFRichTextString(string.Format("{0}", checkListGeneral.ParametrosCheckListRolado.DieseToneladaGranoRolado));//Valor Contador Final Agua
            lblValorConsumoDieselTonelada.ApplyFont(fontTitulos);
            ICell cellLblValorConsumoDieselTonelada = renglon.CreateCell(indexAux + 21);
            cellLblValorConsumoDieselTonelada.SetCellValue(lblValorConsumoDieselTonelada);
            cellLblValorConsumoDieselTonelada.CellStyle = estiloCeldaBorde;
            cellLblValorConsumoDieselTonelada.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            GenerarBordes(hojaExcel, 31, 32, 19, 21, estiloCeldaBorde);

            #endregion TablaDiesel


            #endregion Renglon Horometros

            #region TablaBodega


            renglon = hojaExcel.CreateRow(36);

            var lblInicioBodega = new XSSFRichTextString("Inicio");
            lblInicioBodega.ApplyFont(fontTitulos);
            ICell cellLblInicioBodega = renglon.CreateCell(indexAux + 2);
            cellLblInicioBodega.SetCellValue(lblInicioBodega);
            //cellLblInicioRoladora.CellStyle = celdaBorde;
            cellLblInicioBodega.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            //cellLblInicioRoladora.CellStyle.WrapText = true;

            var lblFinalBodega = new XSSFRichTextString("Final");
            lblFinalBodega.ApplyFont(fontTitulos);
            ICell cellLblFinalBodega = renglon.CreateCell(indexAux + 3);
            cellLblFinalBodega.SetCellValue(lblFinalBodega);
            //cellLblFinalRoladora.CellStyle = celdaBorde;
            cellLblFinalBodega.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            //cellLblFinalRoladora.CellStyle.WrapText = true;

            renglon = hojaExcel.CreateRow(37);
            var lblBodega5 = new XSSFRichTextString("TM Bodega 5 GR");
            lblBodega5.ApplyFont(fontTitulos);
            ICell cellLblBodega5 = renglon.CreateCell(indexAux + 1);
            cellLblBodega5.SetCellValue(lblBodega5);
            cellLblBodega5.CellStyle = estiloCeldaBorde;
            cellLblBodega5.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblInicioBodega5 = new XSSFRichTextString(string.Empty);//Bodega 5 Inicial
            lblInicioBodega5.ApplyFont(fontTitulos);
            ICell cellLblInicioBodega5 = renglon.CreateCell(indexAux + 2);
            cellLblInicioBodega5.SetCellValue(lblInicioBodega5);
            cellLblInicioBodega5.CellStyle = estiloCeldaBorde;
            cellLblInicioBodega5.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblFinalBodega5 = new XSSFRichTextString(string.Empty); //Bodega 5 Final
            lblFinalBodega5.ApplyFont(fontTitulos);
            ICell cellLblFinalBodega5 = renglon.CreateCell(indexAux + 3);
            cellLblFinalBodega5.SetCellValue(lblFinalBodega5);
            cellLblFinalBodega5.CellStyle = estiloCeldaBorde;
            cellLblFinalBodega5.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.CreateRow(38);
            var lblBodega6 = new XSSFRichTextString("TM Bodega 6 GR");
            lblBodega6.ApplyFont(fontTitulos);
            ICell cellLblBodega6 = renglon.CreateCell(indexAux + 1);
            cellLblBodega6.SetCellValue(lblBodega6);
            cellLblBodega6.CellStyle = estiloCeldaBorde;
            cellLblBodega6.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblInicioBodega6 = new XSSFRichTextString(string.Empty);//Bodega 6 Inicial
            lblInicioBodega6.ApplyFont(fontTitulos);
            ICell cellLblInicioBodega6 = renglon.CreateCell(indexAux + 2);
            cellLblInicioBodega6.SetCellValue(lblInicioBodega6);
            cellLblInicioBodega6.CellStyle = estiloCeldaBorde;
            cellLblInicioBodega6.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblFinalBodega6 = new XSSFRichTextString(string.Empty); //Bodega 6 Final
            lblFinalBodega6.ApplyFont(fontTitulos);
            ICell cellLblFinalBodega6 = renglon.CreateCell(indexAux + 3);
            cellLblFinalBodega6.SetCellValue(lblFinalBodega6);
            cellLblFinalBodega6.CellStyle = estiloCeldaBorde;
            cellLblFinalBodega6.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.CreateRow(39);
            var lblBodega7 = new XSSFRichTextString("TM Bodega 7 GR");
            lblBodega7.ApplyFont(fontTitulos);
            ICell cellLblBodega7 = renglon.CreateCell(indexAux + 1);
            cellLblBodega7.SetCellValue(lblBodega7);
            cellLblBodega7.CellStyle = estiloCeldaBorde;
            cellLblBodega7.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblInicioBodega7 = new XSSFRichTextString(string.Empty);//Bodega 7 Inicial
            lblInicioBodega7.ApplyFont(fontTitulos);
            ICell cellLblInicioBodega7 = renglon.CreateCell(indexAux + 2);
            cellLblInicioBodega7.SetCellValue(lblInicioBodega7);
            cellLblInicioBodega7.CellStyle = estiloCeldaBorde;
            cellLblInicioBodega7.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblFinalBodega7 = new XSSFRichTextString(string.Empty); //Bodega 7 Final
            lblFinalBodega7.ApplyFont(fontTitulos);
            ICell cellLblFinalBodega7 = renglon.CreateCell(indexAux + 3);
            cellLblFinalBodega7.SetCellValue(lblFinalBodega7);
            cellLblFinalBodega7.CellStyle = estiloCeldaBorde;
            cellLblFinalBodega7.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            renglon = hojaExcel.CreateRow(40);
            var lblBodega3 = new XSSFRichTextString("TM Bodega 3 GE");
            lblBodega3.ApplyFont(fontTitulos);
            ICell cellLblBodega3 = renglon.CreateCell(indexAux + 1);
            cellLblBodega3.SetCellValue(lblBodega3);
            cellLblBodega3.CellStyle = estiloCeldaBorde;
            cellLblBodega3.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblInicioBodega3 = new XSSFRichTextString(string.Empty);//Bodega 3 Inicial
            lblInicioBodega3.ApplyFont(fontTitulos);
            ICell cellLblInicioBodega3 = renglon.CreateCell(indexAux + 2);
            cellLblInicioBodega3.SetCellValue(lblInicioBodega3);
            cellLblInicioBodega3.CellStyle = estiloCeldaBorde;
            cellLblInicioBodega3.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var lblFinalBodega3 = new XSSFRichTextString(string.Empty); //Bodega 3 Final
            lblFinalBodega3.ApplyFont(fontTitulos);
            ICell cellLblFinalBodega3 = renglon.CreateCell(indexAux + 3);
            cellLblFinalBodega3.SetCellValue(lblFinalBodega3);
            cellLblFinalBodega3.CellStyle = estiloCeldaBorde;
            cellLblFinalBodega3.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;


            renglon = hojaExcel.GetRow(38);

            var lblObservaciones = new XSSFRichTextString("Observaciones:");
            lblObservaciones.ApplyFont(fontTitulos);
            ICell cellLblObservaciones = renglon.CreateCell(indexAux + 6);
            cellLblObservaciones.SetCellValue(lblObservaciones);
            cellLblObservaciones.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaObservaciones = new CellRangeAddress(38, 38, 6, 7);
            hojaExcel.AddMergedRegion(celdaObservaciones);

            var lblValorObservaciones = new XSSFRichTextString(string.Empty);//Valor de las observaciones
            lblValorObservaciones.ApplyFont(fontTitulos);
            ICell cellLblValorObservaciones = renglon.CreateCell(indexAux + 8);
            cellLblValorObservaciones.SetCellValue(lblValorObservaciones);
            cellLblValorObservaciones.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            #endregion TablaBodega


            #endregion ColumnasDescripcion

            #region ArmarDatosRoladoras

            int indiceColumna = 4;
            int indiceRenglon;
            int columnaDescripcionValor = 3;
            foreach (var roladoras in checkList)
            {
                indiceRenglon = 4;
                renglon = hojaExcel.GetRow(indiceRenglon);

                var lblRolDatos = new XSSFRichTextString("ROL");
                lblRolDatos.ApplyFont(fontTitulos);
                ICell cellLblRolDatos = renglon.CreateCell(indiceColumna);
                cellLblRolDatos.SetCellValue(lblRolDatos);
                cellLblRolDatos.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                cellLblRolDatos.CellStyle.WrapText = true;
                var celdaRol = new CellRangeAddress(indiceRenglon, indiceRenglon, indiceColumna, indiceColumna + 1);
                hojaExcel.AddMergedRegion(celdaRol);

                var celdaRolEspacio = new CellRangeAddress(indiceRenglon + 1, indiceRenglon + 1, indiceColumna, indiceColumna + 1);
                hojaExcel.AddMergedRegion(celdaRolEspacio);

                var lblHoraDatos = new XSSFRichTextString("HORA");
                lblHoraDatos.ApplyFont(fontTitulos);
                ICell cellLblHoraDatos = renglon.CreateCell(indiceColumna + 2);
                cellLblHoraDatos.SetCellValue(lblHoraDatos);
                cellLblHoraDatos.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                cellLblHoraDatos.CellStyle.WrapText = true;
                var celdaHora = new CellRangeAddress(indiceRenglon, indiceRenglon, indiceColumna + 2, indiceColumna + 4);
                hojaExcel.AddMergedRegion(celdaHora);

                renglon = hojaExcel.GetRow(indiceRenglon + 1);
                if (renglon == null)
                {
                    renglon = hojaExcel.CreateRow(indiceRenglon + 1);
                }

                var lblValorHoraDatos = new XSSFRichTextString(string.Format("{0}:00", roladoras.Hora));
                lblValorHoraDatos.ApplyFont(fontTitulos);
                ICell cellLblValorHoraDatos = renglon.CreateCell(indiceColumna + 2);
                cellLblValorHoraDatos.SetCellValue(lblValorHoraDatos);
                cellLblValorHoraDatos.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                cellLblValorHoraDatos.CellStyle.WrapText = true;
                var celdaValorHora = new CellRangeAddress(indiceRenglon + 1, indiceRenglon + 1, indiceColumna + 2, indiceColumna + 4);
                hojaExcel.AddMergedRegion(celdaValorHora);

                List<CheckListRoladoraDetalleInfo> detallesRoladora1 =
                    roladoras.Detalles.Where(rol =>
                        rol.CheckListRoladora.Roladora.Descripcion.Equals("ROLADORA 1",
                                                                          StringComparison.CurrentCultureIgnoreCase)).ToList();

                List<CheckListRoladoraDetalleInfo> detallesRoladora2 =
                   roladoras.Detalles.Where(rol =>
                       rol.CheckListRoladora.Roladora.Descripcion.Equals("ROLADORA 2",
                                                                         StringComparison.CurrentCultureIgnoreCase)).ToList();

                List<CheckListRoladoraDetalleInfo> detallesRoladora3 =
                   roladoras.Detalles.Where(rol =>
                       rol.CheckListRoladora.Roladora.Descripcion.Equals("ROLADORA 3",
                                                                         StringComparison.CurrentCultureIgnoreCase)).ToList();

                List<CheckListRoladoraDetalleInfo> detallesRoladora4 =
                   roladoras.Detalles.Where(rol =>
                       rol.CheckListRoladora.Roladora.Descripcion.Equals("ROLADORA 4",
                                                                         StringComparison.CurrentCultureIgnoreCase)).ToList();

                List<CheckListRoladoraDetalleInfo> detallesRoladora5 =
                   roladoras.Detalles.Where(rol =>
                       rol.CheckListRoladora.Roladora.Descripcion.Equals("ROLADORA 5",
                                                                         StringComparison.CurrentCultureIgnoreCase)).ToList();

                renglon = hojaExcel.GetRow(6);

                var lblNumeroRoladora1 = new XSSFRichTextString("1");
                lblNumeroRoladora1.ApplyFont(fontTitulos);
                ICell cellLblNumeroRoladora1 = renglon.CreateCell(indiceColumna);
                cellLblNumeroRoladora1.SetCellValue(lblNumeroRoladora1);
                cellLblNumeroRoladora1.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                cellLblNumeroRoladora1.CellStyle.WrapText = true;

                var lblNumeroRoladora2 = new XSSFRichTextString("2");
                lblNumeroRoladora2.ApplyFont(fontTitulos);
                ICell cellLblNumeroRoladora2 = renglon.CreateCell(indiceColumna + 1);
                cellLblNumeroRoladora2.SetCellValue(lblNumeroRoladora2);
                cellLblNumeroRoladora2.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                cellLblNumeroRoladora2.CellStyle.WrapText = true;

                var lblNumeroRoladora3 = new XSSFRichTextString("3");
                lblNumeroRoladora3.ApplyFont(fontTitulos);
                ICell cellLblNumeroRoladora3 = renglon.CreateCell(indiceColumna + 2);
                cellLblNumeroRoladora3.SetCellValue(lblNumeroRoladora3);
                cellLblNumeroRoladora3.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                cellLblNumeroRoladora3.CellStyle.WrapText = true;

                var lblNumeroRoladora4 = new XSSFRichTextString("4");
                lblNumeroRoladora4.ApplyFont(fontTitulos);
                ICell cellLblNumeroRoladora4 = renglon.CreateCell(indiceColumna + 3);
                cellLblNumeroRoladora4.SetCellValue(lblNumeroRoladora4);
                cellLblNumeroRoladora4.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                cellLblNumeroRoladora4.CellStyle.WrapText = true;

                var lblNumeroRoladora5 = new XSSFRichTextString("5");
                lblNumeroRoladora5.ApplyFont(fontTitulos);
                ICell cellLblNumeroRoladora5 = renglon.CreateCell(indiceColumna + 4);
                cellLblNumeroRoladora5.SetCellValue(lblNumeroRoladora5);
                cellLblNumeroRoladora5.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                cellLblNumeroRoladora5.CellStyle.WrapText = true;

                indiceRenglon = 7;


                for (int i = indiceRenglon; i < 30; i++)
                {
                    renglon = hojaExcel.GetRow(i);
                    if (renglon == null)
                    {
                        renglon = hojaExcel.CreateRow(i);
                    }
                    ICell celdaValor = renglon.GetCell(columnaDescripcionValor);
                    if (celdaValor == null)
                    {
                        continue;
                    }

                    CheckListRoladoraDetalleInfo valorRoladora1 =
                        detallesRoladora1.FirstOrDefault(
                            chk => chk.CheckListRoladoraRango.Descripcion.Equals(celdaValor.ToString()));

                    var lblValorDetalle1 = new XSSFRichTextString(valorRoladora1 != null ? "X" : string.Empty);
                    lblValorDetalle1.ApplyFont(fontTitulos);
                    ICell cellLblValorDetalle1 = renglon.CreateCell(indiceColumna);
                    cellLblValorDetalle1.SetCellValue(lblValorDetalle1);
                    cellLblValorDetalle1.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    cellLblValorDetalle1.CellStyle.WrapText = true;

                    CheckListRoladoraDetalleInfo valorRoladora2 =
                       detallesRoladora2.FirstOrDefault(
                           chk => chk.CheckListRoladoraRango.Descripcion.Equals(celdaValor.ToString()));

                    var lblValorDetalle2 = new XSSFRichTextString(valorRoladora2 != null ? "X" : string.Empty);
                    lblValorDetalle2.ApplyFont(fontTitulos);
                    ICell cellLblValorDetalle2 = renglon.CreateCell(indiceColumna + 1);
                    cellLblValorDetalle2.SetCellValue(lblValorDetalle2);
                    cellLblValorDetalle2.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    cellLblValorDetalle2.CellStyle.WrapText = true;

                    CheckListRoladoraDetalleInfo valorRoladora3 =
                     detallesRoladora3.FirstOrDefault(
                         chk => chk.CheckListRoladoraRango.Descripcion.Equals(celdaValor.ToString()));

                    var lblValorDetalle3 = new XSSFRichTextString(valorRoladora3 != null ? "X" : string.Empty);
                    lblValorDetalle3.ApplyFont(fontTitulos);
                    ICell cellLblValorDetalle3 = renglon.CreateCell(indiceColumna + 2);
                    cellLblValorDetalle3.SetCellValue(lblValorDetalle3);
                    cellLblValorDetalle3.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    cellLblValorDetalle3.CellStyle.WrapText = true;

                    CheckListRoladoraDetalleInfo valorRoladora4 =
                   detallesRoladora4.FirstOrDefault(
                       chk => chk.CheckListRoladoraRango.Descripcion.Equals(celdaValor.ToString()));

                    var lblValorDetalle4 = new XSSFRichTextString(valorRoladora4 != null ? "X" : string.Empty);
                    lblValorDetalle4.ApplyFont(fontTitulos);
                    ICell cellLblValorDetalle4 = renglon.CreateCell(indiceColumna + 3);
                    cellLblValorDetalle4.SetCellValue(lblValorDetalle4);
                    cellLblValorDetalle4.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    cellLblValorDetalle4.CellStyle.WrapText = true;

                    CheckListRoladoraDetalleInfo valorRoladora5 =
                detallesRoladora5.FirstOrDefault(
                    chk => chk.CheckListRoladoraRango.Descripcion.Equals(celdaValor.ToString()));

                    var lblValorDetalle5 = new XSSFRichTextString(valorRoladora5 != null ? "X" : string.Empty);
                    lblValorDetalle5.ApplyFont(fontTitulos);
                    ICell cellLblValorDetalle5 = renglon.CreateCell(indiceColumna + 4);
                    cellLblValorDetalle5.SetCellValue(lblValorDetalle5);
                    cellLblValorDetalle5.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    cellLblValorDetalle5.CellStyle.WrapText = true;

                }

                indiceColumna = indiceColumna + 5;

            }
          
            #endregion ArmarDatosRoladoras

            GenerarBordes(hojaExcel, 4, 29, 1, indiceColumna, estiloCeldaBorde);
            foreach (var indice in renglonesOptimos)
            {
                GenerarBordes(hojaExcel, indice, indice, 3, indiceColumna, celdaGrisBorde);
            }

            renglon = hojaExcel.GetRow(0);
            if(renglon == null)
            {
                renglon = hojaExcel.CreateRow(0);
            }

            var lblTitulo = new XSSFRichTextString("CHECK LIST DE ROLADO");
            lblTitulo.ApplyFont(fontEncabezado);
            ICell cellLblTitulo = renglon.CreateCell(1);
            cellLblTitulo.SetCellValue(lblTitulo);
            cellLblTitulo.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            var celdaTitulo = new CellRangeAddress(0, 0, 1, indiceColumna);
            hojaExcel.AddMergedRegion(celdaTitulo);
            GenerarBordes(hojaExcel, 0, 0, 1, indiceColumna, celdaRojoTitulo);

            EscribirArchivo(hssfworkbook);

            //return retValue;
        }

        private void GenerarBordes(ISheet hoja, int renglonInicio, int renglonFinal, int columnaInicio, int columnaFin, ICellStyle estilo)
        {

            for (int i = renglonInicio; i <= renglonFinal; i++)
            {
                for (int j = columnaInicio; j < columnaFin; j++)
                {
                    var renglonCombinado = hoja.GetRow(i);
                    if (renglonCombinado != null)
                    {
                        var celdaCombinada = renglonCombinado.GetCell(j);
                        if (celdaCombinada != null)
                        {
                            celdaCombinada.CellStyle = estilo;
                        }
                        else
                        {
                            ICell celda = renglonCombinado.CreateCell(j);
                            celda.CellStyle = estilo;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Escribir archivo excel
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private void EscribirArchivo(XSSFWorkbook workbook)
        {
            try
            {
                var dialogo = new SaveFileDialog
                    {
                        Filter = "Excel File (*.xls)|*.xlsx",
                        FilterIndex = 1,
                        RestoreDirectory = true,
                        FileName = "CheckList"
                    };

                dialogo.ShowDialog();
                if (dialogo.FileName != string.Empty)
                {
                    //Write the stream data of workbook to the root directory
                    var file = new FileStream(dialogo.FileName, FileMode.Create);
                    workbook.Write(file);
                    file.Close();

                     SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionCheckListRolado_CheckListImpreso, MessageBoxButton.OK,
                                  MessageImage.Correct);
                    
                }
            }
            catch (IOException ioex)
            {
                Logger.Error(ioex);
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                  Properties.Resources.ImpresionCheckListRolado_FalloGuardarArchivo, MessageBoxButton.OK,
                                  MessageImage.Error);
            }
        }


    }
}

