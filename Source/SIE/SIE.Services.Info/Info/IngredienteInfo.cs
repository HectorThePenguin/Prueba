using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class IngredienteInfo : BitacoraInfo, INotifyPropertyChanged
    {

        
        /// <summary>
        /// Identificador del Ingrediente
        /// </summary>
        public int IngredienteId { get; set; }

        /// <summary>
        /// Organizacion del Ingrediente
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Formula a la que pertenece el Ingrediente
        /// </summary>
        public FormulaInfo Formula { get; set; }

        /// <summary>
        /// Producto ingrediente
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Porcentaje programado del Ingrediente
        /// </summary>
        public decimal PorcentajeProgramado { get; set; }
        //public decimal PorcentajeProgramado
        //{
        //    get
        //    {
        //        return porcentajeProgramado;
        //    }
        //    set
        //    {
        //        if (value != porcentajeProgramado)
        //        {
        //            porcentajeProgramado = value;
        //            OnPropertyChanged();
        //            //NotifyPropertyChanged("PorcentajeProgramado");
        //        }
        //    }
        //}

        /// <summary>
        /// Fecha en que se creo el registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha en que se modifico el registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// indica si se puede modificar el producto de la formula
        /// </summary>
        public bool HabilitaEdicion { get; set; }

        /// <summary>
        /// Indica si el ingrediente tiene cambios pendientes por guardar
        /// </summary>
        public bool TieneCambios { get; set; }

        public IngredienteInfo Clone()
        {
            var tratamientoProductoClone = new IngredienteInfo
            {
                IngredienteId = IngredienteId,
                Organizacion = Organizacion,
                Formula = Formula,
                Producto = Producto,
                PorcentajeProgramado = PorcentajeProgramado,
                HabilitaEdicion = HabilitaEdicion,
                TieneCambios = TieneCambios,
                Activo = Activo,
                UsuarioCreacionID = UsuarioCreacionID,
                UsuarioModificacionID = UsuarioModificacionID
            };
            return tratamientoProductoClone;
        }

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
