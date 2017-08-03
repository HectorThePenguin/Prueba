

namespace SIE.Services.Info.Info
{
    public class ResultadoOperacion
    {
        /// <summary>
        /// Endica si el proceso se realizo correctamente
        /// </summary>
        public bool Resultado { get; set; }
        /// <summary>
        /// Enum para obtner la descripcion del mensaje
        /// </summary>
        public string Mensaje { get; set; }
        /// <summary>
        /// Descripcion del mensaje obtenida del archivo de recurso
        /// </summary>
        public  string DescripcionMensaje { get; set; }
        /// <summary>
        /// total de registros afectados
        /// </summary>
        public int RegistrosAfectados { get; set; }

        /// <summary>
        /// total de registros afectados
        /// </summary>
        public int CodigoMensaje { get; set; }
        
    }
}
