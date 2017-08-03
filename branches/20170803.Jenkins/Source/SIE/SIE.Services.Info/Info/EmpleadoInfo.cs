using System.Collections.Generic;
using SIE.Services.Info.Atributos;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class EmpleadoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int empleadoID;
        private string empleado;
        private string nombre;
        private string paterno;
        private string materno;

        public EmpleadoInfo()
        {

        }

        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyuda",
                EncabezadoGrid = "Clave",
                MetodoInvocacion = "ObtenerPorID",
                PopUp = false,
                EstaEnContenedor = true,
                NombreContenedor = "EmpleadoID")]

        public int EmpleadoID
        {
            get { return empleadoID; }
            set
            {
                empleadoID = value;
                NotifyPropertyChanged("EmpleadoID");
            }
        }

        [AtributoAyuda(Nombre = "PropiedadNombreCatalogoAyuda",
            EncabezadoGrid = "Nombre",
            MetodoInvocacion = "ObtenerPorPagina",
            PopUp = true,
            EstaEnContenedor = true,
            NombreContenedor = "Empleado")]
        
        public string Empleado
        {
            get { return empleado; }
            set
            {
                empleado = value;
                NotifyPropertyChanged("Empleado");
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                NotifyPropertyChanged("Nombre");
            }
        }

        public string Paterno
        {
            get { return paterno; }
            set
            {
                paterno = value;
                NotifyPropertyChanged("Paterno");
            }
        }

        public string Materno
        {
            get { return materno; }
            set
            {
                materno = value;
                NotifyPropertyChanged("Materno");
            }
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