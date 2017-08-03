using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroDisponilidadInfo
    {
        /// <summary>
        /// Clave de Lote
        /// </summary>
        public int LoteId { get; set; }
        /// <summary>
        /// Clave de Organizacion
        /// </summary>
        public int OrganizacionId { get; set; }
        /// <summary>
        /// Clave de Usuario
        /// </summary>
        public int UsuarioId { get; set; }
        /// <summary>
        /// Fecha a consultar
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Fecha Fin a consultar
        /// </summary>
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Fecha para modificar la tabla Lote
        /// </summary>
        public DateTime FechaDisponibilidad { get; set; }
        /// <summary>
        /// Cantidad de semanas
        /// </summary>
        public int Semanas { get; set; }
        /// <summary>
        /// Lista para guardar la informacion de Disponibilidad
        /// </summary>
        public List<DisponibilidadLoteInfo> ListaLoteDisponibilidad { get; set; }
    }
}
