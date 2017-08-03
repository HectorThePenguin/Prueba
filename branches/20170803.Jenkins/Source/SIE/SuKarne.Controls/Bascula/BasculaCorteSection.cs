using System;
using System.Configuration;
using System.IO.Ports;

namespace SuKarne.Controls.Bascula
{
    /// <summary>
    /// Clase que permite realizar el mapa de valors de configuracion de la seccion BasculaCorteSection
    /// que permita administrar los valores para la bascula de corte de ganado
    /// </summary>
    public class BasculaCorteSection : ConfigurationSection
    {
        /// <summary>
        /// Nombre de la seccion. Default basculaCorte
        /// </summary>
        [ConfigurationProperty("nombre", DefaultValue = "basculaCorte",
            IsRequired = true, IsKey = true)]
        public string Nombre
        {
            get
            {
                return (string)this["nombre"];
            }
            set
            {
                this["nombre"] = value;
            }
        }

        /// <summary>
        /// Nombre del puerto COM de la bascula. Default COM1
        /// </summary>
        [ConfigurationProperty("puerto", DefaultValue = "COM1",
            IsRequired = true)]
        public string Puerto
        {
            get
            {
                return (string)this["puerto"];
            }
            set
            {
                this["puerto"] = value;
            }
        }

        /// <summary>
        /// Valor para almacenar la velocidad del dispositivo. Default 9600
        /// </summary>
        [ConfigurationProperty("baudrate", DefaultValue = (int)9600, IsRequired = true)]
        public int Baudrate
        {
            get
            {
                return (int)this["baudrate"];
            }
            set
            {
                this["baudrate"] = value;
            }
        }

        /// <summary>
        /// Tamaño del paquete de datos obtenido del puerto serial. Default 7
        /// </summary>
        [ConfigurationProperty("databits", DefaultValue = (int)7, IsRequired = true)]
        public int Databits
        {
            get
            {
                return (int)this["databits"];
            }
            set
            {
                this["databits"] = value;
            }
        }

        /// <summary>
        /// Paridad del puerto serial. Default None. Valores posibles: None, Even, Mark, Odd, Space
        /// </summary>
        [ConfigurationProperty("paridad", DefaultValue = "None", IsRequired = true)]
        public Parity Paridad
        {
            get
            {
                Parity retVal = Parity.None;
                string input = this["paridad"].ToString();
                switch (input)
                {
                    case "None": retVal = Parity.None; break;
                    case "Even": retVal = Parity.Even; break;
                    case "Mark": retVal = Parity.Mark; break;
                    case "Odd": retVal = Parity.Odd; break;
                    case "Space": retVal = Parity.Space; break;
                    default: retVal = Parity.None; break;
                }
                return retVal;
            }
            set
            {
                this["paridad"] = value;
            }
        }

        public void cambiarParidad(String paridad)
        {
            this["paridad"] = paridad;
        }

        /// <summary>
        /// Final de caracter para resincronizar. Default One. Valores posibles: None, One, OnePointFive, Two
        /// </summary>
        [ConfigurationProperty("bitstop", DefaultValue = StopBits.One, IsRequired = true)]
        public StopBits BitStop
        {
            get
            {
                return (StopBits)this["bitstop"];
            }
            set
            {
                this["bitstop"] = value;
            }
        }
        public void cambiarBitStop(string bitstop)
        {
            switch (bitstop)
            {
                case "One":
                    this["bitstop"] = StopBits.One;
                    break;
                case "None":
                    this["bitstop"] = StopBits.None;
                    break;
                case "OnePointFive":
                    this["bitstop"] = StopBits.OnePointFive;
                    break;
                case "Two":
                    this["bitstop"] = StopBits.Two;
                    break;
            }

        }

        /// <summary>
        /// Valor de configuracion que permite determinar el numero de valores iguales para detener la lectura del puerto serial
        /// </summary>
        [ConfigurationProperty("espera", DefaultValue = 5, IsRequired = true)]
        public int Espera
        {
            get
            {
                return (int)this["espera"];
            }
            set
            {
                this["espera"] = value;
            }
        }

        /// <summary>
        /// Valor de configuracion que permite determinar si esta permitido capturar manualmente la temperatura y el peso
        /// </summary>
        [ConfigurationProperty("CapturaManual", DefaultValue = false, IsRequired = true)]
        public bool CapturaManual
        {
            get
            {
                return (bool)this["CapturaManual"];
            }
            set
            {
                this["CapturaManual"] = value;
            }
        }
    }
}