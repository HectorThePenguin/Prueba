using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class JaulaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string placaJaula = string.Empty;

        public JaulaInfo()
        {
            Activo = EstatusEnum.Activo;
        }
        /// <summary>
        ///     Identificador de la placa de la jaula que transporta el ganado 
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveEntradaGanado", EncabezadoGrid = "Clave")]
        [AtributoAyuda(Nombre = "PropiedadClaveRegistroProgramacionEmbarque", EncabezadoGrid = "Clave")]
        public int JaulaID { get; set; }
        
        /// <summary>
        ///     Id de Proveedor al que pertenece la Jaula 
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        ///     Número de placa de la jaula
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", EncabezadoGrid = "Descripcion",
            MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaGanado", MetodoInvocacion = "ObtenerJaula", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistroProgramacionEmbarque", EncabezadoGrid = "Descripcion",
            MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistroProgramacionEmbarque", MetodoInvocacion = "ObtenerJaula",
            PopUp = false)]
        public string PlacaJaula
        {
            get { return placaJaula == null ? string.Empty : placaJaula.Trim(); }
            set
            {
                string valor = value;
                placaJaula = valor == null ? valor : valor.Trim();
                NotifyPropertyChanged("PlacaJaula");
            }
        }

        /// <summary>
        ///     Numero de cabezas de ganado que puede transportar 
        /// </summary>
        public int Capacidad { get; set; }

        /// <summary>
        ///     Secciones en las que se puede dividir la Jaula para el transporte de Ganado 
        /// </summary>
        public int Secciones { get; set; }

        /// <summary>
        ///     Número Económico de la placa de la Jaula  
        /// </summary>
        public string NumEconomico { get; set; }

        /// <summary>
        ///     ID de la marca de la jaula   
        /// </summary>
        public MarcasInfo Marca { get; set; }

        /// <summary>
        ///     Descripción del modelo de la jaula   
        /// </summary>
        public int Modelo { get; set; }

        /// <summary>
        ///     Indicador de jaula con boletinado   
        /// </summary>
        public bool Boletinado { get; set; }

        /// <summary>
        ///     Observaciones referente al boletinado 
        /// </summary>
        public string Observaciones { get; set; }

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


