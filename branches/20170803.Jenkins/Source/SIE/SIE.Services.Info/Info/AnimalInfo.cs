using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class AnimalInfo : INotifyPropertyChanged
    {
        private long animalID;
        private string arete;
        private string areteMetalico;
        private ProveedorInfo proveedor;
        private List<AnimalMovimientoInfo> listaAnimalesMovimiento;
        private List<AnimalCostoInfo> listaCostosAnimal;
        private List<AnimalConsumoInfo> listaConsumosAnimal;
        private List<AnimalCostoInfo> listaCostoAbastoAnimal;
        private List<AnimalConsumoInfo> listaConsumoAbastoAnimal;

        /// <summary>
        ///     Identificador AnimalID .
        /// </summary>
        public long AnimalID
        {
            get { return animalID; }
            set
            {
                animalID = value;
                NotifyPropertyChanged("AnimalID");
            }
        }

        /// <summary>
        ///     Identificador Arete .
        /// </summary>
        public string Arete
        {
            get { return arete == null ? string.Empty : arete; }
            set
            {
                arete = value;
                NotifyPropertyChanged("Arete");
            }
        }

        /// <summary>
        ///     Identificador AreteMetalico .
        /// </summary>
        public string AreteMetalico
        {
            get { return areteMetalico == null ? string.Empty : areteMetalico; }
            set
            {
                areteMetalico = value;
                NotifyPropertyChanged("AreteMetalico");
            }
        }

        /// <summary>
        ///     Identificador FechaCompra .
        /// </summary>
        public DateTime FechaCompra { get; set; }

        /// <summary>
        ///     Identificador TipoGanadoID .
        /// </summary>
        public int TipoGanadoID { get; set; }

        /// <summary>
        ///     Identificador CalidadGanadoID .
        /// </summary>
        public int CalidadGanadoID { get; set; }

        /// <summary>
        ///     Identificador ClasificacionGanadoID .
        /// </summary>
        public int ClasificacionGanadoID 
        {
            get;
            set; 
        }

        /// <summary>
        ///     Identificador PesoCompra .
        /// </summary>
        public int PesoCompra { get; set; }

        /// <summary>
        ///     Identificador OrganizacionIDEntrada .
        /// </summary>
        public int OrganizacionIDEntrada { get; set; }

        /// <summary>
        ///     Identificador FolioEntrada .
        /// </summary>
        public int FolioEntrada { get; set; }

        /// <summary>
        ///     Identificador PesoLlegada .
        /// </summary>
        public int PesoLlegada { get; set; }

        /// <summary>
        ///     Identificador Paletas .
        /// </summary>
        public int Paletas { get; set; }

        /// <summary>
        ///     Identificador CausaRechadoID .
        /// </summary>
        public int CausaRechadoID { get; set; }

        /// <summary>
        ///     Identificador Venta .
        /// </summary>
        public bool Venta { get; set; }

        /// <summary>
        ///     Identificador Cronico .
        /// </summary>
        public bool Cronico { get; set; }

        /// <summary>
        ///     Identificador Activo .
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        ///     Identificador CargaInicial .
        /// </summary>
        public bool CargaInicial { get; set; }

        /// <summary>
        ///     Identificador FechaCreacion .
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        ///     Identificador UsuarioCreacionID .
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        ///     Identificador FechaModificacion .
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        ///     Identificador UsuarioModificacionID .
        /// </summary>
        public int UsuarioModificacionID { get; set; }

        /// <summary>
        ///     Codigo del corral.
        /// </summary>
        public String Corral { get; set; }

        /// <summary>
        ///     Peso al corte.
        /// </summary>
        public int PesoAlCorte { get; set; }

        /// <summary>
        ///  Entidad de Tipo de Ganado
        /// </summary>
        public TipoGanadoInfo TipoGanado { get; set; }

        /// <summary>
        ///  Entidad de la Clasificación de Ganado
        /// </summary>
        public ClasificacionGanadoInfo ClasificacionGanado { get; set; }

        public ProveedorInfo Proveedor
        {
            get { return proveedor; }
            set
            {
                proveedor = value;
                NotifyPropertyChanged("Proveedor");
            }
        }

        /// <summary>
        ///  Entidad de la Clasificación de Ganado
        /// </summary>
        public List<AnimalMovimientoInfo> ListaAnimalesMovimiento
        {
            get { return listaAnimalesMovimiento ?? new List<AnimalMovimientoInfo>(); }
            set
            {
                listaAnimalesMovimiento = value;
                NotifyPropertyChanged("ListaAnimalesMovimiento");
            }
        }

        /// <summary>
        ///  Lista de costos del animal
        /// </summary>
        public List<AnimalCostoInfo> ListaCostosAnimal
        {
            get { return listaCostosAnimal ?? new List<AnimalCostoInfo>(); }
            set
            {
                listaCostosAnimal = value;
                NotifyPropertyChanged("ListaCostosAnimal");
            }
        }

        /// <summary>
        ///    CorralID.
        /// </summary>
        public int CorralID { get; set; }

        /// <summary>
        ///     Codigo Corral.
        /// </summary>
        public string CodigoCorral { get; set; }

        /// <summary>
        ///     Codigo Corral.
        /// </summary>
        public bool CambioSexo { get; set; }
        /// <summary>
        ///  Override para obtener Arete
        /// </summary>
        public override string ToString()
        {
            return Arete;
        }
        /// <summary>
        /// Dias engorda del animal
        /// </summary>
        public int DiasEngorda { get; set; }

        /// <summary>
        /// Id del Lote donde se encuentra actualmente el Animal
        /// </summary>
        public int LoteID { get; set; }

        /// <summary>
        /// Fecha en que entro el Ganado
        /// </summary>
        public DateTime FechaEntrada { get; set; }

        /// <summary>
        /// Este es un flag para saber si el animal se encuentra en el historico o en el inventario normal
        /// </summary>
        public bool Historico { get; set; }

        /// <summary>
        /// Descripción de la calida de ganado
        /// </summary>
        public string DescripcionHistorico
        {
            get { return Historico ? "Historico" : "Normal"; }
        }

        /// <summary>
        /// Representa la calidad del ganado
        /// </summary>
        public CalidadGanadoInfo CalidadGanado { get; set; }

        /// <summary>
        /// listaConsumosAnimal
        /// </summary>
        public List<AnimalConsumoInfo> ListaConsumosAnimal
        {
            get { return listaConsumosAnimal ?? new List<AnimalConsumoInfo>(); }
            set
            {
                listaConsumosAnimal = value;
                NotifyPropertyChanged("ListaConsumosAnimal");
            }
        }

        /// <summary>
        ///  Lista de costos de abasto del animal
        /// </summary>
        public List<AnimalCostoInfo> ListaCostosAbastoAnimal
        {
            get { return listaCostoAbastoAnimal ?? new List<AnimalCostoInfo>(); }
            set
            {
                listaCostoAbastoAnimal = value;
                NotifyPropertyChanged("ListaCostosAbastoAnimal");
            }
        }

        /// <summary>
        ///  Lista de consumos de abasto del animal
        /// </summary>
        public List<AnimalConsumoInfo> ListaConsumoAbastoAnimal
        {
            get { return listaConsumoAbastoAnimal ?? new List<AnimalConsumoInfo>(); }
            set
            {
                listaConsumoAbastoAnimal = value;
                NotifyPropertyChanged("ListaConsumoAbastoAnimal");
            }
        }

        /// <summary>
        /// Representa la calidad del ganado
        /// </summary>
        public AnimalMovimientoInfo UltimoMovimiento { get; set; }

        /// <summary>
        /// Este es un flag para saber si el animal aplica guardarlo en la Bitacora
        /// </summary>
        public bool AplicaBitacora { get; set; }

        public string Origen { get; set; }

        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
