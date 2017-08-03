using System;
using System.ComponentModel;
using System.IO.Ports;

namespace SuKarne.Controls.Bascula
{
    /// <summary>
    /// Clase que contiene propiedades relacionadas al puerto serial
    /// </summary>
    public class SerialSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string portName = "";
        string[] portNameCollection;
        int baudRate = 4800;       //velocidad por defecto
        readonly BindingList<int> baudRateCollection = new BindingList<int>(); //colecion para almacenar las velocidades
        Parity parity = Parity.None;
        int dataBits = 8;                  //tamaño del paquete por defecto
        int[] dataBitsCollection = new int[] { 5, 6, 7, 8 }; //posibles valores de tamaños de paquete
        StopBits stopBits = StopBits.One;          // StopBits por defecto

        #region Properties
        /// <summary>
        /// El puerto a usar (Por ejemplo, COM1).
        /// </summary>
        public string PortName
        {
            get { return portName; }
            set
            {
                if (!portName.Equals(value))
                {
                    portName = value;
                    SendPropertyChangedEvent("PortName");
                }
            }
        }
        /// <summary>
        /// Velocidad de transmisión del puerto (baud rate)
        /// </summary>
        public int BaudRate
        {
            get { return baudRate; }
            set 
            {
                if (baudRate != value)
                {
                    baudRate = value;
                    SendPropertyChangedEvent("BaudRate");
                }
            }
        }

        /// <summary>
        /// Uno de los valores de paridad
        /// </summary>
        public Parity Parity
        {
            get { return parity; }
            set 
            {
                if (parity != value)
                {
                    parity = value;
                    SendPropertyChangedEvent("Parity");
                }
            }
        }
        /// <summary>
        /// Valor de datbits del puerto
        /// </summary>
        public int DataBits
        {
            get { return dataBits; }
            set
            {
                if (dataBits != value)
                {
                    dataBits = value;
                    SendPropertyChangedEvent("DataBits");
                }
            }
        }
        /// <summary>
        /// Valor de bits de detención
        /// </summary>
        public StopBits StopBits
        {
            get { return stopBits; }
            set
            {
                if (stopBits != value)
                {
                    stopBits = value;
                    SendPropertyChangedEvent("StopBits");
                }
            }
        }

        /// <summary>
        /// Puertos disponibles en la computadora
        /// </summary>
        public string[] PortNameCollection
        {
            get { return portNameCollection; }
            set { portNameCollection = value; }
        }

        /// <summary>
        /// Velocidades de transmisión disponibles del puerto serial actual
        /// </summary>
        public BindingList<int> BaudRateCollection
        {
            get { return baudRateCollection; }
        }

        /// <summary>
        /// Número de databits disponibles del puerto
        /// </summary>
        public int[] DataBitsCollection
        {
            get { return dataBitsCollection; }
            set { dataBitsCollection = value; }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Actualiza el rango de posibles velocidades de transmisión para el dispositivo
        /// </summary>
        /// <param name="possibleBaudRates">Parametro dwSettableBaud desde el la estructura COMMPROP</param>
        /// <returns>Una lista de valores posibles</returns>
        public void UpdateBaudRateCollection(int possibleBaudRates)
        {
            const int BAUD_075 = 0x00000001;
            const int BAUD_110 = 0x00000002;
            const int BAUD_150 = 0x00000008;
            const int BAUD_300 = 0x00000010;
            const int BAUD_600 = 0x00000020;
            const int BAUD_1200 = 0x00000040;
            const int BAUD_1800 = 0x00000080;
            const int BAUD_2400 = 0x00000100;
            const int BAUD_4800 = 0x00000200;
            const int BAUD_7200 = 0x00000400;
            const int BAUD_9600 = 0x00000800;
            const int BAUD_14400 = 0x00001000;
            const int BAUD_19200 = 0x00002000;
            const int BAUD_38400 = 0x00004000;
            const int BAUD_56K = 0x00008000;
            const int BAUD_57600 = 0x00040000;
            const int BAUD_115200 = 0x00020000;
            const int BAUD_128K = 0x00010000;

            baudRateCollection.Clear();

            if ((possibleBaudRates & BAUD_075) > 0)
                baudRateCollection.Add(75);
            if ((possibleBaudRates & BAUD_110) > 0)
                baudRateCollection.Add(110);
            if ((possibleBaudRates & BAUD_150) > 0)
                baudRateCollection.Add(150);
            if ((possibleBaudRates & BAUD_300) > 0)
                baudRateCollection.Add(300);
            if ((possibleBaudRates & BAUD_600) > 0)
                baudRateCollection.Add(600);
            if ((possibleBaudRates & BAUD_1200) > 0)
                baudRateCollection.Add(1200);
            if ((possibleBaudRates & BAUD_1800) > 0)
                baudRateCollection.Add(1800);
            if ((possibleBaudRates & BAUD_2400) > 0)
                baudRateCollection.Add(2400);
            if ((possibleBaudRates & BAUD_4800) > 0)
                baudRateCollection.Add(4800);
            if ((possibleBaudRates & BAUD_7200) > 0)
                baudRateCollection.Add(7200);
            if ((possibleBaudRates & BAUD_9600) > 0)
                baudRateCollection.Add(9600);
            if ((possibleBaudRates & BAUD_14400) > 0)
                baudRateCollection.Add(14400);
            if ((possibleBaudRates & BAUD_19200) > 0)
                baudRateCollection.Add(19200);
            if ((possibleBaudRates & BAUD_38400) > 0)
                baudRateCollection.Add(38400);
            if ((possibleBaudRates & BAUD_56K) > 0)
                baudRateCollection.Add(56000);
            if ((possibleBaudRates & BAUD_57600) > 0)
                baudRateCollection.Add(57600);
            if ((possibleBaudRates & BAUD_115200) > 0)
                baudRateCollection.Add(115200);
            if ((possibleBaudRates & BAUD_128K) > 0)
                baudRateCollection.Add(128000);

            SendPropertyChangedEvent("BaudRateCollection");
        }

        /// <summary>
        /// Envia un evento PropertyChanged
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad cambiada</param>
        private void SendPropertyChangedEvent(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
