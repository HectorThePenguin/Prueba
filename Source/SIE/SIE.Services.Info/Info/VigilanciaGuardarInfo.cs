using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class VigilanciaGuardarInfo
    {
        /// <summary>
        /// Son los campos que se guardan en la tabla Registro Vigilancia, asi como los datos para validar si el usuario
        /// tiene permisos para guardar estos datos
        /// </summary>

        /// <summary>
        /// Es el ID del producto
        /// </summary>
        public int ProductoID { set; get; }

        /// <summary>
        /// es el ID del proveedor
        /// </summary>
        public int ProveedorID { set; get; }

        /// <summary>
        /// Es el ID del transportista de fletes
        /// </summary>
        public int TransportistaID { set; get; }

        /// <summary>
        /// es el ID del chofer
        /// </summary>
        public int ChoferID { set; get; }

        /// <summary>
        /// Es el ID de las placas del camión que entra
        /// </summary>
        public int PlacasID { set; get; }

        /// <summary>
        /// Marca del camion que entra
        /// </summary>
        public string Marca { set; get; }

        /// <summary>
        /// Tipo de color del Camion que entre
        /// </summary>
        public string Color { set; get; }

        /// <summary>
        /// A donde va el camion que entra
        /// </summary>
        public string Accede { set; get; }

        /// <summary>
        /// es el ID de la organizacion al que pertenece el vigilante que ingresa los datos
        /// </summary>
        public int OrganizacionID { set; get; }

        /// <summary>
        /// Hace referencia al fol io que se genera para dicha entrada “Folio”
        /// </summary>
        public int FolioID { set; get; }

        /// <summary>
        /// Es la fecha a la que el camion entro
        /// </summary>
        public DateTime  FechaLlegada { set; get; }

        /// <summary>
        /// Es la fecha a la que el camion sale
        /// </summary>
        public DateTime FechaSalida { set; get; }

        /// <summary>
        /// usuario
        /// </summary>
        public int Usuario { set; get; }
    }
}
