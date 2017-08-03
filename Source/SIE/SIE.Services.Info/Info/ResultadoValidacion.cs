
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ResultadoValidacion
    {
        public ResultadoValidacion()
        {
            Mensaje = "";
            TipoResultadoValidacion = TipoResultadoValidacion.Default;
            Resultado = false;
        }

        /// <summary>
        ///     indica el resultado de la validacion
        /// </summary>
        public bool Resultado { get; set; }

        /// <summary>
        ///     Identificador Acceso .
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>
        /// Tipo de Resultado
        /// </summary>
        public TipoResultadoValidacion TipoResultadoValidacion { get; set; }
        /// <summary>
        /// Clave del resultado
        /// </summary>
        public string ClaveTipoResultado
        {
            get { return TipoResultadoValidacion.ToString(); }
        }

        /// <summary>
        /// Control que causa la validacion
        /// </summary>
        public object Control { get; set; }

        /// <summary>
        /// Codigo para controlar los mensajes
        /// </summary>
        public int CodigoMensaje { get; set; }



    }
}
