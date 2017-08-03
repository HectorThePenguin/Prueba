using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.MateriaPrima
{
    /// <summary>
    /// Lógica de interacción para EntradaMateriaPrimaComprasParciales.xaml
    /// </summary>
    public partial class EntradaMateriaPrimaComprasParciales
    {
        private ContenedorEntradaMateriaPrimaInfo contenedor;
        public ContenedorEntradaMateriaPrimaInfo contenedorRetorno;
        public bool Grabo;
        public EntradaMateriaPrimaComprasParciales()
        {
            InitializeComponent();
        }

        public EntradaMateriaPrimaComprasParciales(ContenedorEntradaMateriaPrimaInfo contenedorEnviado)
        {
            InitializeComponent();
            contenedor = contenedorEnviado;
            contenedorRetorno = contenedorEnviado;
            MostrarDatosGrid();
            if (contenedorRetorno.Contrato.ListaContratoParcial == null)
            {
                contenedorRetorno.Contrato.ListaContratoParcial = new List<ContratoParcialInfo>();
            }
        }

        private void MostrarDatosGrid()
        {
            var contratoParcialPl = new ContratoParcialPL();
            if (contenedor.Contrato.TipoContrato.TipoContratoId != TipoContratoEnum.BodegaNormal.GetHashCode())
            {
                dgComprasParciales.Columns[0].Visibility = Visibility.Hidden;
                dgComprasParciales.Columns[4].Visibility = Visibility.Visible;
                dgComprasParciales.Columns[5].Visibility = Visibility.Hidden;
                dgComprasParciales.Columns[6].Visibility = Visibility.Hidden;
            }
            else
            {
                dgComprasParciales.Columns[0].Visibility = Visibility.Visible;
                dgComprasParciales.Columns[4].Visibility = Visibility.Hidden;
                dgComprasParciales.Columns[5].Visibility = Visibility.Visible;
                dgComprasParciales.Columns[6].Visibility = Visibility.Visible;
            }

            if (contenedor.Contrato.ListaContratoParcial != null)
            {
                dgComprasParciales.ItemsSource = contenedor.Contrato.ListaContratoParcial;
            }
            else
            {
                contenedor.Contrato.ListaContratoParcial = contratoParcialPl.ObtenerFaltantePorContratoId(contenedor.Contrato);
                if (contenedor.Contrato.TipoCambio.Descripcion == TipoCambioEnum.Dolar.ToString().ToUpper())
                {
                    TipoCambioPL tipoCambioPl = new TipoCambioPL();
                    var listaTipoCambio = tipoCambioPl.ObtenerPorFechaActual();
                    if (listaTipoCambio != null)
                    {
                        var tipoCambio =
                            listaTipoCambio.First(
                                registro =>
                                    registro.Descripcion.ToUpper() ==
                                    TipoCambioEnum.Dolar.ToString().ToUpper());
                        if (tipoCambio != null)
                        {
                            foreach (var contratoParcialInfo in contenedor.Contrato.ListaContratoParcial)
                            {
                                if (contratoParcialInfo.TipoCambio.Descripcion ==
                                    TipoCambioEnum.Dolar.ToString().ToUpper())
                                {
                                    contratoParcialInfo.Importe = contratoParcialInfo.ImporteConvertido*
                                                                  tipoCambio.Cambio;
                                }
                            }
                        }
                    }
                    dgComprasParciales.Columns[3].Visibility = Visibility.Visible;
                }
                else
                {
                    dgComprasParciales.Columns[3].Visibility = Visibility.Hidden;
                }
                dgComprasParciales.ItemsSource = contenedor.Contrato.ListaContratoParcial;
            }
        }

        private void BtnSalir_OnClick(object sender, RoutedEventArgs e)
        {
            Grabo = false;
            this.Close();
        }

        private void BtnAceptar_OnClick(object sender, RoutedEventArgs e)
        {
            if (contenedor.Contrato.TipoContrato.TipoContratoId == TipoContratoEnum.BodegaNormal.GetHashCode())
            {
                if (contenedor.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado).ToList().Count > 0)
                {
                    if (
                        contenedor.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado).ToList().Count <=
                        2)
                    {
                        if (contenedor.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString())
                        {

                            if (contenedor.EntradaProducto.PesoOrigen >
                                contenedor.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado)
                                    .ToList()
                                    .Sum(registro => registro.CantidadEntrante))
                            {
                                //Mensaje entrada mayor
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources
                                        .EntradaMateriaPrimaComprasParciales_ParcialidadesMenorEntradaOrigen,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                return;
                            }
                            else if (contenedor.EntradaProducto.PesoOrigen <
                                     contenedor.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado)
                                         .ToList()
                                         .Sum(registro => registro.CantidadEntrante))
                            {
                                //Mensaje entrada menor
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources
                                        .EntradaMateriaPrimaComprasParciales_ParcialidadesMayorEntradaOrigen,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                return;
                            }
                            else
                            {
                                Grabo = true;
                                contenedorRetorno.Contrato.ListaContratoParcial =
                                    contenedor.Contrato.ListaContratoParcial;
                                this.Close();
                            }
                        }
                        else if (contenedor.Contrato.PesoNegociar == PesoNegociarEnum.Destino.ToString())
                        {
                            if ((contenedor.EntradaProducto.PesoBruto - contenedor.EntradaProducto.PesoTara) >
                                contenedor.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado)
                                    .ToList()
                                    .Sum(registro => registro.CantidadEntrante))
                            {
                                //Mensaje entrada mayor  
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources
                                        .EntradaMateriaPrimaComprasParciales_ParcialidadesMenorEntradaDestino,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                return;
                            }
                            else if ((contenedor.EntradaProducto.PesoBruto - contenedor.EntradaProducto.PesoTara) <
                                     contenedor.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado)
                                         .ToList()
                                         .Sum(registro => registro.CantidadEntrante))
                            {
                                //Mensaje entrada menor
                                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                                    Properties.Resources
                                        .EntradaMateriaPrimaComprasParciales_ParcialidadesMayorEntradaDestino,
                                    MessageBoxButton.OK,
                                    MessageImage.Stop);
                                return;
                            }
                            else
                            {
                                Grabo = true;
                                contenedorRetorno.Contrato.ListaContratoParcial =
                                    contenedor.Contrato.ListaContratoParcial;
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrimaComprasParciales_ParcialidadesMayor,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        return;
                    }
                }
                else
                {
                    //Cambiar mensaje
                    SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                        Properties.Resources.EntradaMateriaPrimaComprasParciales_ParcialidadesSeleccionar,
                        MessageBoxButton.OK,
                        MessageImage.Stop);
                    return;
                }
            }
            else
            {
                Grabo = false;
                contenedorRetorno.Contrato.ListaContratoParcial =
                    contenedor.Contrato.ListaContratoParcial;
                this.Close();
            }
        }

        private void ChkParcialidad_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var checkBox = (CheckBox)sender;

                if (!checkBox.IsChecked.HasValue)
                {
                    return;
                }
                else
                {
                    var contratoParcial = (ContratoParcialInfo) checkBox.CommandParameter;
                    if (
                        contenedor.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado)
                            .ToList()
                            .Count() == 1)
                    {
                        foreach (var contratoParcialInfo in contenedor.Contrato.ListaContratoParcial)
                        {
                            if (contratoParcial == contratoParcialInfo)
                            {
                                if (contratoParcial.CantidadEntrante == 0)
                                {
                                    if (contenedor.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString())
                                    {
                                        if (contratoParcialInfo.CantidadRestante > contenedor.EntradaProducto.PesoOrigen)
                                        {
                                            contratoParcialInfo.CantidadEntrante = contenedor.EntradaProducto.PesoOrigen;
                                        }
                                        else
                                        {
                                            contratoParcialInfo.CantidadEntrante = contratoParcial.CantidadRestante;
                                        }
                                    }
                                    else
                                    {
                                        if (contratoParcialInfo.CantidadRestante >
                                            contenedor.EntradaProducto.PesoBruto - contenedor.EntradaProducto.PesoTara)
                                        {
                                            contratoParcialInfo.CantidadEntrante =
                                                contenedor.EntradaProducto.PesoBruto -
                                                contenedor.EntradaProducto.PesoTara;
                                        }
                                        else
                                        {
                                            contratoParcialInfo.CantidadEntrante = contratoParcial.CantidadRestante;
                                        }
                                    }
                                    dgComprasParciales.ItemsSource = new List<ContratoParcialInfo>();
                                    dgComprasParciales.ItemsSource = contenedor.Contrato.ListaContratoParcial;
                                }
                            }
                        }
                    }
                    else if (contenedor.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado)
                        .ToList()
                        .Count() == 2)
                    {
                        foreach (var contratoParcialInfo in contenedor.Contrato.ListaContratoParcial)
                        {
                            if (contratoParcial == contratoParcialInfo)
                            {
                                if (contratoParcial.CantidadEntrante == 0)
                                {
                                    var pesoEntrante =
                                        contenedor.Contrato.ListaContratoParcial.Sum(
                                            registro => registro.CantidadEntrante);
                                    if (contenedor.Contrato.PesoNegociar == PesoNegociarEnum.Origen.ToString())
                                    {
                                        if (pesoEntrante < contenedor.EntradaProducto.PesoOrigen)
                                        {
                                            if (contratoParcialInfo.CantidadRestante >
                                                (contenedor.EntradaProducto.PesoOrigen - pesoEntrante))
                                            {
                                                contratoParcialInfo.CantidadEntrante =
                                                    (contenedor.EntradaProducto.PesoOrigen - pesoEntrante);
                                            }
                                            else
                                            {
                                                contratoParcialInfo.CantidadEntrante = contratoParcial.CantidadRestante;
                                            }
                                            dgComprasParciales.ItemsSource = new List<ContratoParcialInfo>();
                                            dgComprasParciales.ItemsSource = contenedor.Contrato.ListaContratoParcial;
                                        }
                                    }
                                    else
                                    {
                                        if (pesoEntrante <
                                            (contenedor.EntradaProducto.PesoBruto - contenedor.EntradaProducto.PesoTara))
                                        {
                                            if (contratoParcialInfo.CantidadRestante >
                                                (contenedor.EntradaProducto.PesoBruto -
                                                 contenedor.EntradaProducto.PesoTara) -
                                                pesoEntrante)
                                            {
                                                contratoParcialInfo.CantidadEntrante =
                                                    (contenedor.EntradaProducto.PesoBruto -
                                                     contenedor.EntradaProducto.PesoTara) - pesoEntrante;
                                            }
                                            else
                                            {
                                                contratoParcialInfo.CantidadEntrante = contratoParcial.CantidadRestante;
                                            }
                                            dgComprasParciales.ItemsSource = new List<ContratoParcialInfo>();
                                            dgComprasParciales.ItemsSource = contenedor.Contrato.ListaContratoParcial;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (contenedor.Contrato.ListaContratoParcial.Where(registro => registro.Seleccionado)
                        .ToList()
                        .Count() > 2)
                    {
                        foreach (var contratoParcialInfo in contenedor.Contrato.ListaContratoParcial)
                        {
                            if (contratoParcial == contratoParcialInfo)
                            {
                                contratoParcialInfo.Seleccionado = false;
                            }
                        }
                        SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal],
                            Properties.Resources.EntradaMateriaPrimaComprasParciales_ParcialidadesMayor,
                            MessageBoxButton.OK,
                            MessageImage.Stop);
                        dgComprasParciales.ItemsSource = new List<ContratoParcialInfo>();
                        dgComprasParciales.ItemsSource = contenedor.Contrato.ListaContratoParcial;
                    }
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void ChkParcialidad_OnUnchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var checkBox = (CheckBox)sender;

                if (!checkBox.IsChecked.HasValue)
                {
                    return;
                }
                else
                {
                    var contratoParcial = (ContratoParcialInfo)checkBox.CommandParameter;
                    foreach (var contratoParcialInfo in contenedor.Contrato.ListaContratoParcial)
                    {
                        if (contratoParcial == contratoParcialInfo)
                        {
                            contratoParcialInfo.CantidadEntrante = 0;
                        }
                    }
                    dgComprasParciales.ItemsSource = new List<ContratoParcialInfo>();
                    dgComprasParciales.ItemsSource = contenedor.Contrato.ListaContratoParcial;
                }
            }
            catch (Exception ex)
            {
                SkMessageBox.Show(Application.Current.Windows[ConstantesVista.WindowPrincipal], ex.Message, MessageBoxButton.OK, MessageImage.Error);
            }
        }
    }
}
