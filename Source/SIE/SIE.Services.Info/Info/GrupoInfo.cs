namespace SIE.Services.Info.Info
{
    public class GrupoInfo : BitacoraInfo
    {
        /// <summary>
        ///     Identificador Grupo .
        /// </summary>
        public int GrupoID { get; set; }

        /// <summary>
        ///     Group Description.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Grupo Formulario
        /// </summary>
        public GrupoFormularioInfo GrupoFormularioInfo { get; set; }
    }
}