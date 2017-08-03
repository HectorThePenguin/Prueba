using System;
using System.Collections.Generic;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("RepartoAlimento")]
    public class RepartoAlimentoInfo : BitacoraInfo
    {
        /// <summary> 
        ///	Reparto Alimento  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int RepartoAlimentoID { get; set; }

        /// <summary> 
        ///	Tipo Servicio  
        /// </summary> 
        public int TipoServicioID { get; set; }

        /// <summary> 
        ///	Entidad Tipo Servicio  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public TipoServicioInfo TipoServicio { get; set; }

        /// <summary> 
        ///	Camión Reparto  
        /// </summary> 
        public int CamionRepartoID { get; set; }

        /// <summary> 
        ///	Entidad Camión Reparto  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public CamionRepartoInfo CamionReparto { get; set; }

        /// <summary> 
        ///	Usuario Ieparto  
        /// </summary> 
        public int UsuarioIDReparto { get; set; }

        /// <summary> 
        ///	Entidad Usuario Ieparto  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public UsuarioInfo Usuario { get; set; }

        /// <summary> 
        ///	Horometro Inicial  
        /// </summary> 
        public int HorometroInicial { get; set; }

        /// <summary> 
        ///	Horometro Final  
        /// </summary> 
        public int HorometroFinal { get; set; }

        /// <summary> 
        ///	Odometro Inicial  
        /// </summary> 
        public int OdometroInicial { get; set; }

        /// <summary> 
        ///	Odometro Final  
        /// </summary> 
        public int OdometroFinal { get; set; }

        /// <summary> 
        ///	Litros Diesel  
        /// </summary> 
        public int LitrosDiesel { get; set; }

        /// <summary> 
        ///	Fecha Reparto  
        /// </summary> 
        public DateTime FechaReparto { get; set; }

        /// <summary> 
        ///	Fecha Creación  
        /// </summary> 
        [BLToolkit.DataAccess.NonUpdatable]
        public DateTime FechaCreacion { get; set; }

        /// <summary> 
        ///	Fecha Modificación  
        /// </summary> 
        public DateTime? FechaModificacion { get; set; }

        /// <summary> 
        ///	Lista de los Detalles del Grid
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public IList<GridRepartosModel> ListaGridRepartos { get; set; }

        /// <summary> 
        ///	Lista con los Tiempos Muertos que hubo en el Reparto de Alimento
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public List<TiempoMuertoInfo> ListaTiempoMuerto { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public int TotalKilosEmbarcados { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public int TotalKilosRepartidos { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public int TotalSobrante { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public int MermaReparto { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public string TotalTiempoViaje { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public string Observaciones { get; set; }
    }
}
