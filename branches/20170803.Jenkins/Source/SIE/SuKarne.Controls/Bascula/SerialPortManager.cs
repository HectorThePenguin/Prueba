using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SuKarne.Controls.Bascula
{
    /// <summary>
    /// Administra los datos desde el puerto serial
    /// </summary>
    public class SerialPortManager : IDisposable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SerialPortManager()
        {
            // Buscamos los puertos serial instalados en el hardware
            currentSerialSettings.PortNameCollection = SerialPort.GetPortNames();
            currentSerialSettings.PropertyChanged += new PropertyChangedEventHandler(CurrentSerialSettings_PropertyChanged);

            // Si el puerto serial es encontrado, seleccionamos el primero de la lista
            //if (currentSerialSettings.PortNameCollection.Length > 0)
                //currentSerialSettings.PortName = currentSerialSettings.PortNameCollection[0];
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~SerialPortManager()
        {
            Dispose(false);
        }


        #region Fields
        private SerialPort serialPort;
        private SerialSettings currentSerialSettings = new SerialSettings();
        private string latestRecieved = String.Empty;
        public event EventHandler<SerialDataEventArgs> NewSerialDataRecieved; 

        #endregion

        #region Properties
        /// <summary>
        /// Obtiene o establece la configuracion actual del puerto serial
        /// </summary>
        public SerialSettings CurrentSerialSettings
        {
            get { return currentSerialSettings; }
            set { currentSerialSettings = value; }
        }

        #endregion

        #region Event handlers
        /// <summary>
        /// Handler del evento para cuando se detecta un cambio de configuracion para el puerto serial, esto cuando esta en ejecucion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentSerialSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Si el puerto serial es cambiado, una nueva consulta de velocidad de transmision es requerida
            if (e.PropertyName.Equals("PortName"))
                UpdateBaudRateCollection();
        }

        /// <summary>
        /// Handler que permite notificar a traves del NewSerialDataRecieved Event Handler la llegada de informacion desde el puerto serial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int dataLength = serialPort.BytesToRead;
                byte[] data = new byte[dataLength];

                Thread.Sleep(100);
                int nbrDataRead = serialPort.Read(data, 0, dataLength);
                if (nbrDataRead == 0)
                    return;
            
                // Enviar datos a los interesados
                if (NewSerialDataRecieved != null)
                    NewSerialDataRecieved(this, new SerialDataEventArgs(data));
            }
            catch (Exception)
            {
                ;
            }
        }

        #endregion

        #region Methods

        public string ObtenerLetura(byte[] Datos)
        {
            string datos = "";
            try
            {
                string str = Encoding.ASCII.GetString(Datos);
                str = str.Replace((char)13, ';');
                string[] arreglo = str.Split(';');

                if (arreglo.Length > 1)
                {
                    datos = arreglo[arreglo.Length - 2].Trim();
                    //datos = Regex.Replace(datos, "[^0-9.a-zA-Z]+", "");
                    datos = Regex.Replace(datos, "[^0-9.]+", "");
                }
                if (str.Trim() != String.Empty)
                {
                    //string tmp = Regex.Replace(str, "[^0-9a-zA-Z]+", "");
                    //lblPeso.Text = tmp;
                }
            }
            catch (Exception)
            {
                ;
            }
            return datos;
        }

        public string ObtenerLeturaRFID(byte[] Datos)
        {
            string str = Encoding.ASCII.GetString(Datos);
            string datos = str;

            try
            {
                datos = Regex.Replace(datos, "[^0-9.a-zA-Z]+", "");
                datos = datos.Substring(0, 15); // This throws ArgumentOutOfRangeException.
            }
            catch (ArgumentOutOfRangeException )
            {
                ;
            }  
            catch (Exception)
            {
                ;
            }

            return datos;
        }

        /// <summary>
        /// Connectar a un puerto serial definido a traves de la configuracion del puerto actual
        /// </summary>
        public void StartListening()
        {
          
            StartListening(currentSerialSettings.PortName, currentSerialSettings.BaudRate, currentSerialSettings.Parity,
                currentSerialSettings.DataBits, currentSerialSettings.StopBits);

        }

        public void StartListening(string portName, int baudRate, Parity paridad, int databits, StopBits stopbit)
        {
            bool found=false;
            //validamos que el puerto no este vacio
            if (portName == String.Empty)
                throw new InvalidPortNameException("El nombre del puerto especificado no es válido o no existe en la lista");

            //revisamos dentro de los puertos configurados en el pc el puerto solicitado, para evitar registro de puertos no existentes
            for (int i = 0; i < currentSerialSettings.PortNameCollection.Length; i++)
            {
                if (portName == currentSerialSettings.PortNameCollection[i])
                {
                    found = true;
                    break;
                }
            }
            // Si no encontramos el puerto en la lista generamos la excepcion
            if (!found)
                throw new InvalidPortNameException("El nombre del puerto especificado no es válido o no existe en la lista");

                // Cerramos el puerto si este ya esta abierto
                if (serialPort != null && serialPort.IsOpen)
                    serialPort.Close();

            // Establecemos la configuracion del puerto serial
            serialPort = new SerialPort(
                portName,
                baudRate,
                paridad,
                databits,
                stopbit);

            // Subscribimos el evento y realizamos la apertura del puerto serial
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            serialPort.Open();
        }
         

        /// <summary>
        /// Cierra el puerto serial
        /// </summary>
        public void StopListening()
        {
            // Cerramos el puerto si esta abierto
            if (serialPort != null && serialPort.IsOpen)
                serialPort.Close();
        }

        /// <summary>
        /// Regresa el la estructura COMMPROP y extra los valores permitidos de velocidad
        /// </summary>
        private void UpdateBaudRateCollection()
        {
            serialPort = new SerialPort(currentSerialSettings.PortName);
            serialPort.Open();
            object p = serialPort.BaseStream.GetType().GetField("commProp", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(serialPort.BaseStream);
            Int32 dwSettableBaud = (Int32)p.GetType().GetField("dwSettableBaud", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(p);

            serialPort.Close();
            currentSerialSettings.UpdateBaudRateCollection(dwSettableBaud);
        }

        // Llamada para liberacion del puerto
        public void Dispose()
        {
            Dispose(true);
        }

        // Parte de la base del patron de diseño para implementar Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                serialPort.DataReceived -= new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            }
           
            // Liberando el puerto serial y otros objetos no administrados
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                    serialPort.Close();

                serialPort.Dispose();
            }
        }


        #endregion

    }

    /// <summary>
    /// EventArgs usados para enviar bytes recibidos desde el puerto serial
    /// </summary>
    public class SerialDataEventArgs : EventArgs
    {
        public SerialDataEventArgs(byte[] dataInByteArray)
        {
            Data = dataInByteArray;
        }

        /// <summary>
        /// Arreglo de Byte que contiene los datos desde el puerto serial
        /// </summary>
        public byte[] Data;
    }

    [Serializable()]
    public class InvalidPortNameException : Exception
    {
         public InvalidPortNameException() : base() { }
        public InvalidPortNameException(string message) : base(message) { }
        public InvalidPortNameException(string message, System.Exception inner) : base(message, inner) { }

        // Constructor necesario para la realizacion de serializacion
        // Excepcion que puede ser propagada desde un servidor remotoa al cliente 
        protected InvalidPortNameException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

}
