using System.Collections.ObjectModel;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class EntradaGanadoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int entradaGanadoID;
        private int folioEntrada;
        private string certificadoZoosanitario;
        private string pruebasTB;
        private string pruebasTR;
        private decimal pesoBruto;
        private decimal pesoTara;
        private int pesoLlegada;
        private string organizacionOrigen;

        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadOcultaEntradaGanado")]
        [AtributoAyuda(Nombre = "PropiedadOcultaCostroEntradaGanado")]
        [AtributoAyuda(Nombre = "PropiedadOcultaCancelacionCosteoEntradaGanado")]
        [AtributoInicializaPropiedad]
        public int EntradaGanadoID
        {
            get { return entradaGanadoID; }
            set
            {
                entradaGanadoID = value;
                NotifyPropertyChanged("EntradaGanadoID");
            }
        }

        /// <summary>
        /// Folio de entrada 
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveEntradaGanado", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerPorDependencias", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCosteoEntrada", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerEntradasGanadoRecibidasPorDependencias", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCancelacionCosteoEntrada", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerEntradasGanadoCosteadoPorDependencias", PopUp = false)]
        public int FolioEntrada
        {
            get { return folioEntrada; }
            set
            {
                folioEntrada = value;
                NotifyPropertyChanged("FolioEntrada");
            }
        }

        /// <summary>
        /// Organizacion de la que se esta realizando la captura
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Organizacion Origen del ganado
        /// </summary>
        public int OrganizacionOrigenID { get; set; }

        /// <summary>
        /// Fecha de entrega
        /// </summary>
        public DateTime FechaEntrada { get; set; }

        /// <summary>
        /// Numero de embarque
        /// </summary>
        public int EmbarqueID { get; set; }

        /// <summary>
        /// Tipo de Origen
        /// </summary>
        public int TipoOrigen { get; set; }

        /// <summary>
        /// Tipo de Origen
        /// </summary>
        public string TipoOrigenAgrupado { get; set; }

        /// <summary>
        /// campo referente a la entrada
        /// </summary>
        public int FolioOrigen { get; set; }

        /// <summary>
        /// Organizacion origen ID separada por pipes agrupadas en la pantalla programacion ganado
        /// </summary>
        public string FolioOrigenAgrupado { get; set; }

        /// <summary>
        /// Fecha de salida del embarque
        /// </summary>
        public DateTime FechaSalida { get; set; }

        /// <summary>
        /// Chofer que trae el camion 
        /// </summary>
        public int ChoferID { get; set; }

        /// <summary>
        /// Numero de la jaula 
        /// </summary>
        public int JaulaID { get; set; }

        /// <summary>
        /// Numero de la jaula 
        /// </summary>
        public int CamionID { get; set; }

        /// <summary>
        /// Nmero de cabezas Origen
        /// </summary>
        public int CabezasOrigen { get; set; }

        /// <summary>
        /// Cabezas recibidas
        /// </summary>
        public int CabezasRecibidas { get; set; }

        /// <summary>
        /// Operador 
        /// </summary>
        public int OperadorID { get; set; }

        /// <summary>
        /// Peso Bruto
        /// </summary>
        public decimal PesoBruto
        {
            get { return pesoBruto; }
            set
            {
                pesoBruto = value;
                NotifyPropertyChanged("PesoBruto");
            }
        }

        /// <summary>
        /// Peso Tara
        /// </summary>
        public decimal PesoTara
        {
            get { return pesoTara; }
            set
            {
                pesoTara = value;
                NotifyPropertyChanged("PesoTara");
            }
        }

        /// <summary>
        /// bandera que indica si es ruteo
        /// </summary>
        public bool EsRuteo { get; set; }

        /// <summary>
        /// Bandera que indica el sello de seguridad
        /// </summary>
        public bool Fleje { get; set; }

        /// <summary>
        /// Numero de checklist 
        /// </summary>
        public string CheckList { get; set; }

        /// <summary>
        /// Numero de corral
        /// </summary>
        public int CorralID { get; set; }

        /// <summary>
        /// Numero de lote
        /// </summary>
        public LoteInfo Lote { get; set; }

        /// <summary>
        /// Observacion
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", EncabezadoGrid = "Origen",
            MetodoInvocacion = "ObtenerPorDependencias", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerPorDependencias", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCosteoEntrada", EncabezadoGrid = "Origen",
            MetodoInvocacion = "ObtenerEntradaGanadoRecibidasPaginaPorDependencias", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCosteoEntrada", EncabezadoGrid = "Origen",
            MetodoInvocacion = "ObtenerEntradasGanadoRecibidasPorDependencias", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCancelacionCosteoEntrada", EncabezadoGrid = "Origen",
            MetodoInvocacion = "ObtenerEntradaGanadoCosteadoPaginaPorDependencias", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCancelacionCosteoEntrada", EncabezadoGrid = "Origen",
            MetodoInvocacion = "ObtenerEntradasGanadoCosteadoPorDependencias", PopUp = false)]
        public string Observacion { get; set; }

        /// <summary>
        /// Bandera que indica si ya se imprimio el ticket
        /// </summary>
        public bool ImpresionTicket { get; set; }

        /// <summary>
        /// Bandera que indica si el registro ya fue costeado
        /// </summary>
        public bool Costeado { get; set; }

        /// <summary>
        /// bandera que infica si el registro ya fué Manejado por el departamento de enfermeria
        /// </summary>
        public bool Manejado { get; set; }

        /// <summary>
        /// Lista con las condiciones del ganado
        /// </summary>
        public IList<EntradaCondicionInfo> ListaCondicionGanado { get; set; }

        /// <summary>
        /// Muestra la Descripcion del Organizacion de Origen
        /// </summary>
        public string OrganizacionOrigen
        {
            get { return organizacionOrigen; }
            set
            {
                organizacionOrigen = value;
                NotifyPropertyChanged("OrganizacionOrigen");
            }
        }

        /// <summary>
        /// Muestra el Codigo del Corral
        /// </summary>
        public string CodigoCorral { get; set; }

        /// <summary>
        /// Muestra el Codigo del Lote
        /// </summary>
        public string CodigoLote { get; set; }

        /// <summary>
        /// Muestra la descripcion del Tipo de Organizacion de Origen
        /// </summary>
        public string TipoOrganizacionOrigen { get; set; }

        /// <summary>
        /// Descripcion del Operador
        /// </summary>
        public string Operador { get; set; }

        /// <summary>
        /// Descripcion del Proveedor
        /// </summary>
        public string Proveedor { get; set; }

        /// <summary>
        /// Clave del Proveedor
        /// </summary>
        public int ProveedorID { get; set; }
 
    /// <summary>
        /// Muestra la descripcion
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Muestra el Nombre del Evaluador
        /// </summary>
        public string Evaluador { get; set; }

        /// <summary>
        /// Muestra la Fecha de Evaluacion
        /// </summary>
        public DateTime FechaEvaluacion { get; set; }
		
		 /// <summary>
        /// Acceso Hembras  
        /// </summary>
        public int Hembras { get; set; }

        /// <summary>
        /// Acceso Machos  
        /// </summary>
        public int Machos { get; set; }

        /// <summary>
        /// Acceso Rechazos  
        /// </summary>
        public string Rechazos { get; set; }
		/// <summary>
        /// Horas de transito

        /// </summary>
        public double Horas { get; set; }

        /// <summary>
        /// Tipo de TipoOrganizacion descripcion

        /// </summary>
        public string TipoOrganizacion { get; set; }

        /// <summary>
        /// LoteID

        /// </summary>
        public int LoteID { get; set; }

        /// <summary>
        /// Si la cabeza es clasificada con EsMetaFilaxia
        /// </summary>
        public bool EsMetaFilaxia { get; set; }

        /// <summary>
        /// Peso Origen del animal
        /// </summary>
        public int PesoOrigen { get; set; }

        /// <summary>
        /// Peso Llegada del animal
        /// </summary>
        public int PesoLlegada
        {
            get { return pesoLlegada; }
            set
            {
                pesoLlegada = value;
                NotifyPropertyChanged("PesoLlegada");
            }
        }

        /// <summary>
        /// Folio entrada separada por coma agrupadas en la pantalla programacion ganado
        /// </summary>
        public string FolioEntradaAgrupado { get; set; }

        /// <summary>
        /// Organizacion origen separada por coma agrupadas en la pantalla programacion ganado
        /// </summary>
        public string OrganizacionOrigenAgrupado { get; set; }

        /// <summary>
        /// Organizacion origen ID separada por pipes agrupadas en la pantalla programacion ganado
        /// </summary>
        public string OrganizacionOrigenIDAgrupado { get; set; }

        /// <summary>
        /// Cabezas recibidas agrupadas para cuando son partidas que bienen en ruteos
        /// </summary>
        public int CabezasRecibidasAgrupadas { get; set; }

        /// <summary>
        /// Indica si tiene datos agrupados
        /// </summary>
        public bool EsAgrupado { get; set; }

        /// <summary>
        /// Indica si se entrego documentacion de Guia
        /// </summary>
        public bool Guia { get; set; }
        /// <summary>
        /// Indica si se entrego documentacion de Factura
        /// </summary>
        public bool Factura { get; set; }
        /// <summary>
        /// Indica si se entrego documentacion de Poliza
        /// </summary>
        public bool Poliza { get; set; }
        /// <summary>
        /// Indica si se entrego documentacion de Hoja de Embarque
        /// </summary>
        public bool HojaEmbarque { get; set; }

        /// <summary>
        /// Indica si se el ganado fue manejado con estres
        /// </summary>
        public bool ManejoSinEstres { get; set; }

        /// <summary>
        /// Indica el número de Cabezas muertas
        /// </summary>
        public int CabezasMuertas { get; set; }

        /// <summary>
        /// Indica el mensaje que se va a mostrar en la pantalla Calificacion Ganado, en caso de que no cumpla con alguna regla de negocio
        /// </summary>
        public int MensajeRetornoCalificacion { get; set; }

        /// <summary>
        /// Indica el Estatus que tiene actualmente el Embarque
        /// </summary>
        public int EstatusEmbarque { get; set; }

        /// <summary>
        /// Indica el ID del Tipo De Organizacion
        /// </summary>
        public int TipoOrganizacionOrigenId { get; set; }

        /// <summary>
        /// Lista De animales q pertenecen al FolioEntrada
        /// </summary>
        public IList<AnimalInfo> ListaAnimal { get; set; }

        /// <summary>
        /// Indica el ID del Tipo De Organizacion
        /// </summary>
        public NivelGarrapata NivelGarrapata { get; set; }

        public override string ToString()
        {
            return CodigoCorral;
        }

        /// <summary>
        /// Indica el nivel de garrapata
        /// </summary>
        public string LeyendaNivelGarrapata { get; set; }

        /// <summary>
        /// Indica la lista obtenida de las interfaz salida obtenidos en base al EmbarqueID
        /// </summary>
        public List<InterfaceSalidaInfo> ListaInterfaceSalida { get; set; }
        /// <summary>
        /// Evaluacion del corral de entrada
        /// </summary>
        public string Evaluacion {
            get { return EsMetaFilaxia ? "Metafilaxia" : "Normal"; }
        }

        /// <summary>
        /// Certificado Zoosanitario
        /// </summary>
        public string CertificadoZoosanitario
        {
            get { return certificadoZoosanitario; }
            set
            {
                certificadoZoosanitario = value;
                NotifyPropertyChanged("CertificadoZoosanitario");
            }
        }

        /// <summary>
        /// Certificado de Pruebas TB
        /// </summary>
        public string PruebasTB
        {
            get { return pruebasTB; }
            set
            {
                pruebasTB = value;
                NotifyPropertyChanged("PruebasTB");
            }
        }

        /// <summary>
        /// Certificado de Pruebas TR
        /// </summary>
        public string PruebasTR
        {
            get { return pruebasTR; }
            set
            {
                pruebasTR = value;
                NotifyPropertyChanged("PruebasTR");
            }
        }

        /// <summary>
        /// Condicion en la cual llega la jaula
        /// </summary>
        public CondicionJaulaInfo CondicionJaula { get; set; }

        /// <summary>
        /// Obtiene una lista con los pesos unificados
        /// </summary>
        public ObservableCollection<PesoUnificadoInfo> PesosUnificados { get; set; }

        /// <summary>
        /// Indica si se habilitara el campo de peso para captura
        /// </summary>
        public bool HabilitarOrigen { get; set; }

        /// <summary>
        /// Indica el número de Cabezas muertas en Condicion
        /// </summary>
        public int CabezasMuertasCondicion { get; set; }

        /// <summary>
        /// InterfaceSalidaTraspaso
        /// </summary>
        public InterfaceSalidaTraspasoInfo InterfaceSalidaTraspaso { get; set; }

        /// <summary>
        /// Codigos de entrada de ganado agrupados
        /// </summary>
        public List<EntradaGanadoCodigoAgrupado> EntradaGanadoCodigoAgrupados { get; set; }

        /// <summary>
        /// Costos de Embarque
        /// </summary>
        public List<CostoInfo> CostosEmbarque { get; set; }

		
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
