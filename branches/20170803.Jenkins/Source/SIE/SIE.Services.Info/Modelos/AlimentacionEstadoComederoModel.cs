using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class AlimentacionEstadoComederoModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AlimentacionEstadoComederoModel()
        {
            Fecha = DateTime.Today;
            EstaTrabajando = false;

            CorralesPorFormula = new ObservableCollection<AlimentacionCorralPorFormulaModel>();
            CorralesPorEstadoComedero = new ObservableCollection<AlimentacionCorralPorEstadoComederoModel>();
            CorralesPorFormula.CollectionChanged += CorralesPorFormula_CollectionChanged;
            CorralesPorEstadoComedero.CollectionChanged += CorralesPorEstadoComedero_CollectionChanged;
        }

        void notificarCambioColeccion(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        void CorralesPorEstadoComedero_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            notificarCambioColeccion("CorralesPorEstadoComedero");
        }
        void CorralesPorFormula_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            notificarCambioColeccion("CorralesPorFormula");
        }

        DateTime fecha;
        public DateTime Fecha
        {
            get
            {
                return fecha;
            }
            set
            {
                fecha = value;
                notificarCambioColeccion("Fecha");
            }
        }

        bool estaTrabajado;
        public bool EstaTrabajando
        {
            get
            {
                return estaTrabajado;
            }
            set
            {
                estaTrabajado = value;
                notificarCambioColeccion("EstaTrabajando");
            }
        }

        int organizacionID;
        public int OrganizacionID
        {
            get
            {
                return organizacionID;
            }
            set
            {
                organizacionID = value;
                notificarCambioColeccion("OrganizacionID");
            }
        }

        string division;
        public string Division
        {
            get
            {
                return division;
            }
            set
            {
                division = value;
                notificarCambioColeccion("Division");
            }
        }

        public ObservableCollection<AlimentacionCorralPorFormulaModel> CorralesPorFormula { get; set; }
        public ObservableCollection<AlimentacionCorralPorEstadoComederoModel> CorralesPorEstadoComedero { get; set; }
        /// <summary>
        /// Se utiliza para guardar la fecha que tiene el servidor BD en cada consulta
        /// </summary>
        public DateTime FechaServidorBD { get; set; }
        /// <summary>
        /// En este Info se guardan todos los datos caputarados con el sp para poder generar el reporte -estado comedero-
        /// </summary>
        public ObservableCollection<AlimentacionEstadoComederoInfo> DatosReporteComederoInfo { get; set; }
    }
}
