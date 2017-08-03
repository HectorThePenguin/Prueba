using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroClonarParametroTrampa
    {
        /// <summary>
        /// Trampa origen a clonar
        /// </summary>
        public TrampaInfo TrampaOrigen { get; set; }
        /// <summary>
        /// Trampa destino a clonar
        /// </summary>
        public TrampaInfo TrampaDestino { get; set; }

        /// <summary>
        /// Id del Usuario que hace la Clonacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }

    }
}
