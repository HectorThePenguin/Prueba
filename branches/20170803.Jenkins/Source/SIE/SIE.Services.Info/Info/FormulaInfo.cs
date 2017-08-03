using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class FormulaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion = string.Empty;
        private int formulaId = 0;
        
        
        /// <summary> 
        ///	FormulaID  
        /// </summary> 
        [AtributoAyuda(Nombre = "PropiedadFormulaId", EncabezadoGrid = "Id", MetodoInvocacion = "ObtenerActivoPorId", PopUp = false)]
        public int FormulaId //{ get; set; }
        {
            get { return formulaId; }
            set
            {
                int valor = value;
                formulaId = valor;
                NotifyPropertyChanged("FormulaId");
            }
        }

        /// <summary> 
        ///	Descripcion  
        /// </summary> 
        [AtributoAyuda(Nombre = "PropiedadDescripcion", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                string valor = value;
                descripcion = valor == null ? valor : valor.Trim();
                NotifyPropertyChanged("Descripcion");
            }
        }

        /// <summary>
        /// Clave del tipo de formula
        /// </summary>
        public int TipoFormulaId { get; set; }

        /// <summary>
        /// Acceso ProductoId
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary> 
        ///	TipoFormulaID  
        /// </summary> 
        public TipoFormulaInfo TipoFormula { get; set; }

        /// <summary> 
        ///	ProductoID  
        /// </summary> 
        public ProductoInfo Producto { get; set; }

        /// <summary> 
        ///	Contiene la Lista de ingredientes de la Formula
        /// </summary> 
        public List<IngredienteInfo> ListaIngredientes { get; set; }

        /// <summary>
        /// Obtiene la organizacion a la que pertenece la formula
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadOrganizacion", EncabezadoGrid = "Organizacion", PopUp = false)]
        public OrganizacionInfo Organizacion { get; set; }

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
