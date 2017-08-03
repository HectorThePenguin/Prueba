namespace SIE.Services.Info.Info
{
    public class NombreDetectorEvaluadorInfo
    {
        /// <summary>
        /// Identificador del operador
        /// </summary>
        public int OperadorID { get; set; }

        /// <summary>
        /// Organizacion a la que pertenece el Operador
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Nombre del operador
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido paterno del operador
        /// </summary>
        public string ApellidoPaterno { get; set; }

        /// <summary>
        /// Apellido Materno del operador
        /// </summary>
        public string ApellidoMaterno { get; set; }

        /// <summary>
        /// Enfermeria Id
        /// </summary>
        public int EnfermeriaID { get; set; }

        /// <summary>
        /// Corral ID
        /// </summary>
        public int CorralID { get; set; }

    }
}
