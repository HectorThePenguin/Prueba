using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.WinForm.Sanidad
{
    /// <summary>
    /// Lógica de interacción para DetalleHistorialClinico.xaml
    /// </summary>
    public partial class DetalleHistorialClinico : Window
    {

        private IList<HistorialClinicoDetalleInfo> Detalles { get; set; }
        public DetalleHistorialClinico()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void CargarHistorial(HistorialClinicoInfo historialSeleccionado)
        {
            IList<HistorialClinicoDetalleInfo> detalles = new List<HistorialClinicoDetalleInfo>();
            var tratamientoPl = new TratamientoPL();
            TratamientoInfo tratamientoInfo = null;
            var tratamientoConsultaPl = new TratamientoProductoPL();

            var animalMovimiento = new AnimalMovimientoInfo
            {
                OrganizacionID = historialSeleccionado.OrganizacionID,
                AnimalMovimientoID = historialSeleccionado.AnimalMovimientoId
            };
            if (historialSeleccionado.ListaProblemas != null && historialSeleccionado.ListaProblemas.Any())
            {
                foreach (var problema in historialSeleccionado.ListaProblemas)
                {
                    if (problema.Tratamientos != null)
                    {
                        if (!String.IsNullOrEmpty(historialSeleccionado.Tratamiento.Trim()))
                        {
                            //Se separan los tratamientos
                            string[] listaTratamiento = historialSeleccionado.Tratamiento.Trim().Split(new Char[] { ',' });
                            foreach (string tratamiento in listaTratamiento)
                            {
                                if (tratamiento.Trim() != "")
                                {
                                    tratamientoInfo = new TratamientoInfo { TratamientoID = int.Parse(tratamiento.Trim()) };
                                    tratamientoInfo = tratamientoPl.ObtenerPorID(tratamientoInfo.TratamientoID);

                                    var listaDetalles =
                                        tratamientoConsultaPl.ObtenerTratamientoAplicadoPorMovimientoTratamientoID(animalMovimiento, tratamientoInfo);

                                    if (listaDetalles!=null){
                                        foreach (var historialClinicoDetalleInfo in listaDetalles)
                                        {
                                            historialClinicoDetalleInfo.Problema = problema.Descripcion;
                                            detalles.Add(historialClinicoDetalleInfo);
                                        }
                                    }
                                }
                            }
                        }

                        /*
                        foreach (var tratamiento in problema.Tratamientos)
                        {
                            var listaDetalles =
                                tratamientoConsultaPl.ObtenerTratamientoAplicadoPorMovimientoTratamientoID(animalMovimiento, tratamiento);
                            if (listaDetalles != null)
                            {
                                foreach (var historialClinicoDetalleInfo in listaDetalles)
                                {
                                    historialClinicoDetalleInfo.Problema = problema.Descripcion;
                                    detalles.Add(historialClinicoDetalleInfo);
                                }
                            }
                        }*/
                    }
                }
            }
            else if (!String.IsNullOrEmpty(historialSeleccionado.Tratamiento.Trim()))
            {
                //Se separan los tratamientos
                string[] listaTratamiento = historialSeleccionado.Tratamiento.Trim().Split(new Char[] { ',' });
                foreach (string tratamiento in listaTratamiento)
                {
                    if (tratamiento.Trim() != "")
                    {
                        tratamientoInfo = new TratamientoInfo { TratamientoID = int.Parse(tratamiento.Trim()) };
                        tratamientoInfo = tratamientoPl.ObtenerPorID(tratamientoInfo.TratamientoID);

                        var listaDetalles =
                            tratamientoConsultaPl.ObtenerTratamientoAplicadoPorMovimientoTratamientoID(animalMovimiento, tratamientoInfo);

                        if (listaDetalles!=null){
                            foreach (var historialClinicoDetalleInfo in listaDetalles)
                            {
                                detalles.Add(historialClinicoDetalleInfo);
                            }
                        }
                    }
                }
            }

            GridDetalleHistorial.ItemsSource = detalles;
        }
    }
}
