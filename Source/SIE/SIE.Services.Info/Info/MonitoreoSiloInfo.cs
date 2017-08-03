using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class MonitoreoSiloInfo
    {
        // 
        public int TemperaturaMax { get; set; }

        // Datos para llenar el combo
        public string SiloDescripcion { get; set; }

        // Datos para llenar Grid
        public int UbicacionSensor { get; set; } // Se para los datos del Detalle
        public int AlturaSilo { get; set; } // Se para los datos del Detalle
        public int OrdenSensor { get; set; }

        //Datos a guardar 
        public decimal TemperaturaAmbiente { get; set; }
        public int ProductoID{ get; set; }
        public decimal HR { get; set; }
        public string Observaciones { get; set; }
        public int UsuarioCreacionId { get; set; }

        //Datos del Detalle
        public int TemperaturaCelda { get; set; }

    }
}
