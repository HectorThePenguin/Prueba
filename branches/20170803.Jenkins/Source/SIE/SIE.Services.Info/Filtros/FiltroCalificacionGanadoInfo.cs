using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroCalificacionGanadoInfo
    {
        /// <summary>
        /// Identificador de la Organización para busqueda
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Identificador de la Entrada Ganado
        /// </summary>
        public int EntradaGanadoID { get; set; }

        /// <summary>
        /// Identificador del Folio de Entrada para busqueda
        /// </summary>
        public int FolioEntrada { get; set; }

        /// <summary>
        /// Identificador del Usuario
        /// </summary>
        public int UsuarioID { get; set; }

        /// <summary>
        /// Identificador el número de Cabezas Muertas
        /// </summary>
        public int CabezasMuertas { get; set; }

        /// <summary>
        /// Identificador el número de Cabezas Muertas
        /// </summary>
        public List<CalidadGanadoCapturaInfo> ListaCalidades { get; set; }
    }
}
