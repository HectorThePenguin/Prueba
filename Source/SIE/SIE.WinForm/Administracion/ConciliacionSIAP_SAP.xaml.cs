using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using SIE.Base.Log;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;
using OfficeOpenXml;



namespace SIE.WinForm.Administracion
{
 
    /// <summary>
    /// Lógica de interacción para ConciliacionSIAP_SAP.xaml
    /// </summary>
    public partial class ConciliacionSIAP_SAP //: UserControl
    {

        public ConciliacionSIAP_SAP()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObtenerPoliza();
            cboTipoPoliza.SelectedValue = 0;

        }



        private void ObtenerPoliza()
        {
            var tiposPlizaPL = new TipoPolizaPL();
            IList<TipoPolizaInfo> tiposPoliza = tiposPlizaPL.ObtenerTodos(EstatusEnum.Activo);
            if (tiposPoliza == null)
            {
                tiposPoliza = new List<TipoPolizaInfo>();
            }
            var tipoPoliza = new TipoPolizaInfo
                                {
                                    TipoPolizaID = 0,
                                    Descripcion =  Properties.Resources.cbo_Seleccione                                    
                                };
            tiposPoliza.Insert(0, tipoPoliza);

            cboTipoPoliza.ItemsSource = tiposPoliza;
            cboTipoPoliza.SelectedValuePath = "TipoPolizaID";
            cboTipoPoliza.DisplayMemberPath = "Descripcion";
        }

        private void btnGeneral_Click(object sender, RoutedEventArgs e)
        {

            var TipoPoliza = cboTipoPoliza.SelectedValue;

            if ((int)TipoPoliza != 0)
            {
                var Conciliacion = new ConciliacionPL();
                IList<PolizasIncorrectasInfo> lista = Conciliacion.ConciliacionTipoPoliza((int)TipoPoliza);

                IList<ConciliacionInfo> ListaDetalle = Conciliacion.ConciliacionDetalle((int)TipoPoliza);

                if ((lista != null && lista.Count > 0) || (ListaDetalle != null && ListaDetalle.Count > 0))
                {
                    var dialogo = new SaveFileDialog();
                    dialogo.FileName = "Conciliacion SIAP - SAP";
                    dialogo.DefaultExt = "xslx";
                    dialogo.Filter = "Archivo de Excel (*.xlsx)|*.xlsx";
                    DialogResult resultado = dialogo.ShowDialog();

                    if (resultado == DialogResult.OK)
                    {
                        System.IO.File.WriteAllBytes(dialogo.FileName, SIE.WinForm.Properties.Resources.Conciliacion);

                        var archivoCarga = new FileInfo(dialogo.FileName);
                        using (var excel = new ExcelPackage(archivoCarga))
                        {
                            ExcelWorkbook libro = excel.Workbook;

                            string detalle = "DETALLE";
                            string pendiente = "PENDIENTES";
                            ExcelWorksheet hojaExcelPendiente = libro.Worksheets[pendiente];
                            ExcelWorksheet hojaExcel = libro.Worksheets[detalle];

                            if (lista != null && lista.Count > 0)
                            {
                                int rowList = 0;
                                int rowFor = 2;
                                int rowDef = rowFor;
                                hojaExcelPendiente.Tables[0].TableXml.DocumentElement.Attributes["ref"].Value = "A1:L" + (lista.Count + 1).ToString();
                                hojaExcelPendiente.Tables[0].TableXml.DocumentElement.ChildNodes[0].Attributes["ref"].Value = "A1:L" + (lista.Count + 1).ToString();
                                for (int row = 1; row <= lista.Count; row++)
                                {
                                    hojaExcelPendiente.Cells[rowFor, 1].Value = lista[rowList].OrganizacionID;
                                    hojaExcelPendiente.Cells[rowFor, 2].Value = lista[rowList].FolioMovto;
                                    hojaExcelPendiente.Cells[rowFor, 3].Value = lista[rowList].FechaDocto;
                                    hojaExcelPendiente.Cells[rowFor, 4].Value = lista[rowList].Concepto;
                                    hojaExcelPendiente.Cells[rowFor, 5].Value = lista[rowList].Ref3;
                                    hojaExcelPendiente.Cells[rowFor, 6].Value = lista[rowList].Cargos;
                                    hojaExcelPendiente.Cells[rowFor, 7].Value = lista[rowList].Abonos;
                                    hojaExcelPendiente.Cells[rowFor, 8].Value = lista[rowList].DocumentoSAP;
                                    hojaExcelPendiente.Cells[rowFor, 9].Value = lista[rowList].Procesada;
                                    hojaExcelPendiente.Cells[rowFor, 10].Value = lista[rowList].Mensaje;
                                    hojaExcelPendiente.Cells[rowFor, 11].Value = lista[rowList].PolizaID;
                                    hojaExcelPendiente.Cells[rowFor, 12].Value = lista[rowList].TipoPoliza;

                                    for (int cell = 1; cell < 13; cell++)
                                    {
                                        var estilo = hojaExcelPendiente.Cells[rowDef, cell];
                                        hojaExcelPendiente.Cells[rowFor, cell].StyleID = estilo.StyleID;
                                    }

                                    rowFor++;
                                    rowList++;
                                }
                            }

                            if (ListaDetalle != null && ListaDetalle.Count > 0)
                            {
                                int rowListDta = 0;
                                int rowForDta = 2;
                                int rowDef2 = rowForDta;
                                hojaExcel.Tables[0].TableXml.DocumentElement.Attributes["ref"].Value = "A1:AB" + (ListaDetalle.Count + 1).ToString();
                                hojaExcel.Tables[0].TableXml.DocumentElement.ChildNodes[0].Attributes["ref"].Value = "A1:AB" + (ListaDetalle.Count + 1).ToString();
                                for (int row = 1; row <= ListaDetalle.Count; row++)
                                {
                                    hojaExcel.Cells[rowForDta, 1].Value = ListaDetalle[rowListDta].NoRef;
                                    hojaExcel.Cells[rowForDta, 2].Value = ListaDetalle[rowListDta].FechaDocto;
                                    hojaExcel.Cells[rowForDta, 3].Value = ListaDetalle[rowListDta].FechaCont;
                                    hojaExcel.Cells[rowForDta, 4].Value = ListaDetalle[rowListDta].ClaseDoc;
                                    hojaExcel.Cells[rowForDta, 5].Value = ListaDetalle[rowListDta].Sociedad;
                                    hojaExcel.Cells[rowForDta, 6].Value = ListaDetalle[rowListDta].Moneda;
                                    hojaExcel.Cells[rowForDta, 7].Value = ListaDetalle[rowListDta].TipoCambio;
                                    hojaExcel.Cells[rowForDta, 8].Value = ListaDetalle[rowListDta].TextoDocto;
                                    hojaExcel.Cells[rowForDta, 9].Value = ListaDetalle[rowListDta].Mes;
                                    hojaExcel.Cells[rowForDta, 10].Value = ListaDetalle[rowListDta].Cuenta;
                                    hojaExcel.Cells[rowForDta, 11].Value = ListaDetalle[rowListDta].Proveedor;
                                    hojaExcel.Cells[rowForDta, 12].Value = ListaDetalle[rowListDta].Cliente;
                                    hojaExcel.Cells[rowForDta, 13].Value = ListaDetalle[rowListDta].Importe;
                                    hojaExcel.Cells[rowForDta, 14].Value = ListaDetalle[rowListDta].Concepto;
                                    hojaExcel.Cells[rowForDta, 15].Value = ListaDetalle[rowListDta].Division;
                                    hojaExcel.Cells[rowForDta, 16].Value = ListaDetalle[rowListDta].NoLinea;
                                    hojaExcel.Cells[rowForDta, 17].Value = ListaDetalle[rowListDta].Ref3;
                                    hojaExcel.Cells[rowForDta, 18].Value = ListaDetalle[rowListDta].ArchivoFolio;
                                    hojaExcel.Cells[rowForDta, 19].Value = ListaDetalle[rowListDta].DocumentoSAP;
                                    hojaExcel.Cells[rowForDta, 20].Value = ListaDetalle[rowListDta].DocumentoCancelacionSAP;
                                    hojaExcel.Cells[rowForDta, 21].Value = ListaDetalle[rowListDta].Segmento;
                                    hojaExcel.Cells[rowForDta, 22].Value = ListaDetalle[rowListDta].OrganizacionID;
                                    hojaExcel.Cells[rowForDta, 23].Value = ListaDetalle[rowListDta].Conciliada;
                                    hojaExcel.Cells[rowForDta, 24].Value = ListaDetalle[rowListDta].Procesada;
                                    hojaExcel.Cells[rowForDta, 25].Value = ListaDetalle[rowListDta].Cancelada;
                                    hojaExcel.Cells[rowForDta, 26].Value = ListaDetalle[rowListDta].Mensaje;
                                    hojaExcel.Cells[rowForDta, 27].Value = ListaDetalle[rowListDta].PolizaID;
                                    hojaExcel.Cells[rowForDta, 28].Value = ListaDetalle[rowListDta].TipoPoliza;
                                    for (int cell = 1; cell < 29; cell++)
                                    {
                                        var estilo = hojaExcel.Cells[rowDef2, cell];
                                        hojaExcel.Cells[rowForDta, cell].StyleID = estilo.StyleID;
                                    }
                                    rowListDta++;
                                    rowForDta++;
                                }
                            }

                            FileInfo ArchivoExcel = new FileInfo(dialogo.FileName);
                            excel.SaveAs(ArchivoExcel);

                            SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                              Properties.Resources.ConciliacionSIAP_SAP_Generado,
                                                              MessageBoxButton.OK, MessageImage.Correct);
                        }
                    }

                }
                else
                {
                    SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                              string.Format( Properties.Resources.ConciliacionSIAP_SAP_ListasVacias, cboTipoPoliza.Text),
                                                              MessageBoxButton.OK, MessageImage.Warning); 
                }

            }
            else
            {
                SkMessageBox.Show(System.Windows.Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                                              Properties.Resources.ConciliacionSIAP_SAP_TipoPolizaVacia,
                                                              MessageBoxButton.OK, MessageImage.Warning);                
            
            
            }       





            
        }

      
    }
}
