using SIE.Services.Info.Atributos;
using System;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Lote")]
    public class LoteInfo : BitacoraInfo, System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void notificarCambioPropiedad(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propiedad));
            }
        }
        /// <summary>
        /// Clave del Lote
        /// </summary>
        int loteID;
        [AtributoAyuda(Nombre = "ClaveLotesPorCorral", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerLoteDeCorralPorLoteID", PopUp = false, EstaEnContenedor = true, NombreContenedor = "Lote")]
        [AtributoAyuda(Nombre = "PropiedadClaveLote", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorId", PopUp = false, EstaEnContenedor = true, NombreContenedor = "Lote")]
        public int LoteID
        {
            get
            {
                return loteID;
            }
            set
            {
                loteID = value;
                notificarCambioPropiedad("LoteID");
            }
        }


        /// <summary>
        /// Clave de la Organizacion a la Cual
        /// Pertenece el Lote
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Clave del Corral al Cual Pertenece
        /// el Lote
        /// </summary>
        private int corralID;
        public int CorralID
        {
            get
            {
                return corralID;
            }
            set
            {
                corralID = value;
                notificarCambioPropiedad("CorralID");
            }
        }

        /// <summary>
        /// Codigo del Lote
        /// </summary>
        string lote;
        [AtributoAyuda(Nombre = "DescripcionLotesPorCorral", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerLotesCorralPorPagina", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Lote")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionLote", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Lote")]
        public string Lote
        {
            get
            {
                return lote;
            }
            set
            {
                lote = value;
                notificarCambioPropiedad("Lote");
            }
        }

        /// <summary>
        /// Clave de Tipo de Corral
        /// </summary>
        public int TipoCorralID { get; set; }

        /// <summary>
        /// Clave del Tipo de Proceso
        /// </summary>
        public int TipoProcesoID { get; set; }

        /// <summary>
        /// Fecha de Inicio del Corral
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Cabezas con las que Iniciar el Lote
        /// </summary>
        public int CabezasInicio { get; set; }

        /// <summary>
        /// Fecha de Cierre de Lote
        /// </summary>
        public DateTime FechaCierre { get; set; }

        /// <summary>
        /// Numero de Cabezas en el Lote
        /// </summary>
        public int Cabezas { get; set; }

        /// <summary>
        /// Fecha en la que estara Disponible el Lote
        /// </summary>
        public DateTime FechaDisponibilidad { get; set; }

        /// <summary>
        /// Bandera que Indica si Tendra Disponibilidad Manual
        /// </summary>
        public bool DisponibilidadManual { get; set; }

        /// <summary>
        /// Fecha de Creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Fecha de Salida del Ganado
        /// </summary>
        public DateTime FechaSalida { get; set; }

        /// <summary>
        /// Referencia a Corral
        /// </summary>
        public CorralInfo Corral { get; set; }

        /// <summary>
        /// Fecha Entrada Zilmax
        /// </summary>
        public DateTime? FechaEntradaZilmax { get; set; }

        /// <summary>
        /// Fecha Sallida Zilmax
        /// </summary>
        public DateTime? FechaSalidaZilmax { get; set; }

        /// <summary>
        /// Bandera que Indica si se actualiza el Cierre del Corral
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public bool AplicaCierreLote { get; set; }

        /// <summary>
        /// Valor que indica el Peso Promedio de Compra de los Animales
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int PesoCompra { get; set; }
    }
}
