using System;
using System.Collections.Generic;
using System.Windows;
using SIE.Base.Log;
using SIE.Services.Servicios.PL;
using SIE.Services.Info.Info;

namespace SIE.WinForm.Calidad
{
    /// <summary>
    /// Lógica de interacción para CalidadMezcladoFormulasResumen.xaml
    /// </summary>
    public partial class CalidadMezcladoFormulasResumen : Window
    {
        private int organizacionID;
        private int FormulasMuestrear;
        IList<CalidadMezcladoFormula_ResumenInfo> DatosGridUno = new List<CalidadMezcladoFormula_ResumenInfo>();
    
        #region Constructor

        public CalidadMezcladoFormulasResumen(int orgID, int ForMuestra)
            {
            InitializeComponent();
            organizacionID = orgID;
            FormulasMuestrear = ForMuestra;
            CargarPrimerGrid();
            }
       
        #endregion

        #region Metodos

        /// <summary>
        /// Algoritmo para cargar los datos necesarios para llenar el Grid de la ventana
        /// </summary>
        private void CargarPrimerGrid()
        {
            try
            {
                CalidadMezcladoFormulasAlimentoPL traerDatosTablaResumen = new CalidadMezcladoFormulasAlimentoPL();
                DatosGridUno = traerDatosTablaResumen.TraerDatosTablaResumen(organizacionID, FormulasMuestrear);
                if (DatosGridUno != null)
                {


                    //Se calcula la columna de Particulas Esperadas
                    foreach (var elementos in DatosGridUno)
                    {
                        decimal materiaSeca = (Convert.ToDecimal(elementos.PesoBS) / Convert.ToDecimal(elementos.PesoBH)) * 100;
                        if (materiaSeca == 0)
                            elementos.ParticulasEsperadas = 0;
                        else
                            elementos.ParticulasEsperadas = ((elementos.Factor * 100) / materiaSeca) * elementos.Peso;
                    }
                    //Se calcula % de eficiencia de recuperación
                    foreach (var elementos in DatosGridUno)
                    {
                        elementos.PorEficiencia = (elementos.PromParticulasEsperadas / elementos.ParticulasEsperadas) * 100;
                    }
                    // calcula el campo "Promedio" o "Media"
                    int a = 0;
                    decimal asum = 0;
                    foreach (var elementos in DatosGridUno)
                    {
                        a++;
                        asum += elementos.PorEficiencia;
                    }
                    Promedio.Text = Convert.ToString(decimal.Round(asum / a, 0));
                    //se calcula el campo Desviación Estándar
                    int cCont = 0;
                    double media = Convert.ToDouble(asum / a);
                    double preVarianza = 0;
                    double varianza = 0;
                    double desviacionEstandar = 0;
                    foreach (var elementos in DatosGridUno)
                    {
                        preVarianza += Math.Pow((Convert.ToDouble(elementos.PorEficiencia) - media), 2);
                        cCont++;

                    }
                    varianza = preVarianza / cCont;
                    desviacionEstandar = Math.Sqrt(varianza);

                    DesvEstandar.Text = Convert.ToString(Math.Round(desviacionEstandar, 2));
                    //Se calcula Coeficiente de Variacion
                    double coeficienteVar = ((desviacionEstandar * 100) / media);
                    CoefVariacion.Text = Convert.ToString(Math.Round(coeficienteVar, 2));
                    //Se calcula Eficiencia de Mezclado
                    EficMezclado.Text = Convert.ToString(Math.Round(100 - coeficienteVar, 2));
                    GridUno.ItemsSource = new List<CalidadMezcladoFormula_ResumenInfo>();
                    GridUno.ItemsSource = DatosGridUno;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        
        #endregion

        #region Eventos

        /// <summary>
        /// Método que cierra la ventana y regresa a la página desde donde se lanzó
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
