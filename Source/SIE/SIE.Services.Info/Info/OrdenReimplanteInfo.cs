using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;
using System.Text;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{


    public class PropertyChangedBase : INotifyPropertyChanged
    {
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }

    public class OrdenReimplanteInfo : PropertyChangedBase
    {
        private ProductoInfo productoSeleccionado;
        private CorralInfo corralSeleccionado;

        public OrdenReimplanteInfo()
        {
            Seleccionado = false;
            EsEditable = true;
        }
        /// <summary>
        /// Organizacion Id
        /// </summary>
        public int OrganizacionId { get; set; }
        /// <summary>
        /// Numero de identificacion del corral
        /// </summary>
        public int IdCorralOrigen{ get; set; }
        /// <summary>
        /// Codigo del corral de origen
        /// </summary>
        public string CodigoCorralOrigen { get; set; }
        /// <summary>
        /// Corral Origen
        /// </summary>
        public CorralInfo CorralOrigen { get; set; }
        /// <summary>
        /// Especifica el tipo de proceso del lote origen de la orden
        /// </summary>
        public int TipoProcesoIdLote { get; set; }

        /// <summary>
        /// Numero de identificacion del corral destino
        /// </summary>
        public int IdCorralDestino { get; set; }
        /// <summary>
        /// Codigo del corral destino
        /// </summary>
        public string CodigoCorralDestino { get; set; }
        /// <summary>
        /// Lote del corral
        /// </summary>
        public int LoteId { get; set; }
        /// <summary>
        /// Tipo de ganado
        /// </summary>
        public TipoGanadoInfo TipoGanado { get; set; }
        /// <summary>
        /// Kilos proyectados del corral
        /// </summary>
        public int KilosProyectados { get; set; }
        /// <summary>
        /// Numero de cabezas del corral
        /// </summary>
        public int Cabezas { get; set; }

        /// <summary>
        /// Establece la fecha de la programacion del reimplante
        /// </summary>
        public DateTime FechaReimplanteSeleccionado { get; set; }

        /// <summary>
        /// Fecha de reimplante del corral
        /// </summary>
        public DateTime FechaReimplante { get; set; }
        /// <summary>
        /// Numero de reimplantes del corral
        /// </summary>
        public int NumeroReimplante { get; set; }
        /// <summary>
        /// Tipo de reimplante a aplicar
        /// </summary>
        public IList<ProductoInfo> Productos {get; set; }
        /// <summary>
        /// Lista de corrales destino disponibles
        /// </summary>
        public IList<CorralInfo> CorralesDestino { get; set; }
        /// <summary>
        /// Marca como seleccionado un elemento de la Orden
        /// </summary>
        public bool Seleccionado { get; set; }
        /// <summary>
        /// Permite especificar si el corral es proximo a reimplantar y no se puede modificar
        /// </summary>
        public bool EsEditable { get; set; }

        /// <summary>
        /// Usuario de Creacion de la orden de reimplante
        /// </summary>
        public int UsuarioCreacion { get; set; }

        /// <summary>
        /// Especifica el producto para reimplante seleccionado
        /// </summary>
        public ProductoInfo ProductoSeleccionado
        {
            get
            {
                return productoSeleccionado;
            }
            set
            {
                if (productoSeleccionado != value)
                {
                    productoSeleccionado = value;
                    RaisePropertyChanged("ProductoSeleccionado");
                }
            }
        }
        /// <summary>
        /// Especifica el corral destino seleccionado para reimplante
        /// </summary>
        public CorralInfo CorralDestinoSeleccionado
        {
            get
            {
                return corralSeleccionado;
            }
            set
            {
                if (corralSeleccionado != value)
                {
                    corralSeleccionado = value;
                    RaisePropertyChanged("CorralSeleccionado");
                }
            }
        }
    }
}
