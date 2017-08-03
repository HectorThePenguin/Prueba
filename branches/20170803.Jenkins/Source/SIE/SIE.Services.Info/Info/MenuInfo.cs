namespace SIE.Services.Info.Info
{
    public class MenuInfo
    {
        /// <summary>
        /// Identificador del modulo
        /// </summary>
        public int ModuloID { get; set; }

        /// <summary>
        /// Descripcion del Modulo
        /// </summary>
        public string Modulo { get; set; }

        /// <summary>
        /// Identificador del Formulario
        /// </summary>
        public int FormularioID { get; set; }

        /// <summary>
        /// Descripcion del Formulario
        /// </summary>
        public string Formulario { get; set; }

        /// <summary>
        /// Formulario windows
        /// </summary>
        public string WinForm { get; set; }

        /// <summary>
        /// Imagen que mostrara
        /// </summary>
        public string Imagen { get; set; }

        /// <summary>
        /// Imagen que mostrara
        /// </summary>
        public string Control { get; set; }

        /// <summary>
        /// Imagen que mostrara
        /// </summary>
        public int? PadreID { get; set; }

        /// <summary>
        /// Imagen que mostrara
        /// </summary>
        public string Padre { get; set; }

        /// <summary>
        /// Imagen que mostrara
        /// </summary>
        public string ImagenPadre { get; set; }

        /// <summary>
        /// Imagen que mostrara
        /// </summary>
        public int OrdenFormulario { get; set; }

        /// <summary>
        /// Imagen que mostrara
        /// </summary>
        public int OrdenModulo { get; set; }

        /// <summary>
        /// Clase con la cual se representara el elemento del menu
        /// </summary>
        public string Clase { get; set; }
    }
}