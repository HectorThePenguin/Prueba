using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class CorteTransferenciaGanadoGuardarInfo
    {
        /// <summary>
        /// Animal info de CorteTransferencia
        /// </summary>
        public AnimalInfo AnimalCorteTransferenciaInfo { get; set; }

        /// <summary>
        /// Animal movimiento de corte por transfencia
        /// </summary>
        public AnimalMovimientoInfo AnimalMovimientoCorteTransferenciaInfo { get; set; }

        /// <summary>
        /// CorralInfo Origen
        /// </summary>
        public CorralInfo CorralInfoGen { get; set; }

        /// <summary>
        /// Organizacion del corte por transferencia de ganado
        /// </summary>
        public int OrganizacionId { get; set; }

        /// <summary>
        /// Lote oriten info
        /// </summary>
        public AnimalMovimientoInfo AnimalMovimientoOrigenInfo { get; set; }

        /// <summary>
        /// Codigo de corral origen
        /// </summary>
        public String TxtCorralDestino { get; set; }


        /// <summary>
        /// AnimalInfo de animal Actual
        /// </summary>
        public AnimalInfo AnimalActualInfo { get; set; }

        /// <summary>
        /// AnimalSalidaInfo 
        /// </summary>
        public AnimalSalidaInfo AnimalSalidaGuardarInfo { get; set; }

        /// <summary>
        /// Lote  destino
        /// </summary>
        public LoteInfo LoteDestinoInfo { get; set; }

        /// <summary>
        /// Corral global de
        /// </summary>
        public int CorralGlobal { get; set; }

        /// <summary>
        /// bandera para eliminar animales de animalSalida
        /// </summary>
        public  bool BanderaEliminarAnimalSalida { get; set; }

        /// <summary>
        /// ID de usuario
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// ID de almacen
        /// </summary>
        public int AlmacenID { get; set; }

        /// <summary>
        /// ID del corral de destino
        /// </summary>
        public int CorralDestinoID { get; set; }
    }
}
