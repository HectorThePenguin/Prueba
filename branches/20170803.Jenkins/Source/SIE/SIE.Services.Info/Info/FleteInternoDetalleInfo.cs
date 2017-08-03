using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class FleteInternoDetalleInfo
    {

        private string mermaPermitidaDescripcion = string.Empty;
        /// <summary>
        /// Id correspondiente al registro en bd
        /// </summary>
        public int FleteInternoDetalleId { get; set; }

        /// <summary>
        /// Flete interno al que corresponde el detalle
        /// </summary>
        public int FleteInternoId { get; set; }

        /// <summary>
        /// Proveedor correspondiente al detalle
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Merma permitida para el flete detalle
        /// </summary>
        public decimal MermaPermitida { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MermaPermitidaDescripcion
        {
            set
            {
                if (value != mermaPermitidaDescripcion)
                {
                    string valor = value;
                    mermaPermitidaDescripcion = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("Descripcion");
                }
            }
            get { return mermaPermitidaDescripcion == null ? null : mermaPermitidaDescripcion.Trim(); }
        }

        /// <summary>
        /// Observaciones del flete detalle
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Indica si el registro esta activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifico el registro
        /// </summary>
        public int UsuarioModificacionId { get; set; }

        /// <summary>
        /// Listado de costos
        /// </summary>
        public List<FleteInternoCostoInfo> ListadoFleteInternoCosto { get; set; } 

        /// <summary>
        /// Indica si el registro fue guardado
        /// </summary>
        public bool Guardado { get; set; }

        /// <summary>
        /// Indica si el registro fue eliminado
        /// </summary>
        public bool Eliminado { get; set; }

        /// <summary>
        /// Indica si el registro fue modificado
        /// </summary>
        public bool Modificado { get; set; }

        /// <summary>
        /// Indica si la merma permitida se mostrara en el grid
        /// </summary>
        public bool MermaPermitidaVisible { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        /// <summary>
        /// Tipo de Tarifa del Flete Interno
        /// </summary>
        public int TipoTarifaID { get; set; }

        /// <summary>
        /// Tipo de Tarifa del Flete Interno
        /// </summary>
        public string TipoTarifa { get; set; }
    }
}
