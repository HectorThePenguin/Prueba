using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class BasculaMultipesajeInfo : INotifyPropertyChanged
    {
        private DateTime fechaCreacion;
        private OperadorInfo quienRecibe;
        private string chofer;
        private string placas;
        private int pesoBruto;
        private int pesoTara;
        private int pesoNeto;
        private string producto;
        private string observacion;
        private OrganizacionInfo organizacionInfo;
        private FolioMultipesajeInfo folioMultipesaje;

        private string pesoBrutoText;
        private string pesoTaraText;
        private bool envioSAP;
        private int usuarioCreacion;
        public string PesoBrutoText
        {
            get { return pesoBrutoText; }
            set
            {
                pesoBrutoText = value;
                NotifyPropertyChanged("PesoBrutoText");
            }
        }

        public string PesoTaraText 
        {
            get { return pesoTaraText; }
            set
            {
                pesoTaraText = value;
                NotifyPropertyChanged("PesoTaraText");
            }
        }

        public OrganizacionInfo OrganizacionInfo
        {
            get { return organizacionInfo; }
            set
            {
                organizacionInfo = value;
                NotifyPropertyChanged("OrganizacionID");
            }
        }


        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set
            {
                fechaCreacion = value;
                NotifyPropertyChanged("FechaCreacion");
            }
        }

        public OperadorInfo QuienRecibe
        {
            get { return quienRecibe; }
            set
            {
                quienRecibe = value;
                NotifyPropertyChanged("OperadorIndoId");
            }
        }

        public string Chofer
        {
            get { return chofer; }
            set
            {
                chofer = value;
                NotifyPropertyChanged("Chofer");
            }
        }

        public string Placas
        {
            get { return placas; }
            set
            {
                placas = value;
                NotifyPropertyChanged("Placas");
            }
        }

        public int PesoBruto
        {
            get
            {
                int val;
                if (int.TryParse(PesoBrutoText, out val))
                {
                    return val;
                }

                return 0;
            }
            set
            {
                pesoBruto = value;
                PesoBrutoText = pesoBruto.ToString();
                NotifyPropertyChanged("PesoBruto");
            }
        }

        public int PesoTara
        {
            get
            {
                int val;
                if (int.TryParse(PesoTaraText, out val))
                {
                    return val;
                }
                return 0;
            }
            set
            {
                pesoTara = value;
                pesoTaraText = pesoTara.ToString();
                NotifyPropertyChanged("PesoTara");
            }
        }

        public int PesoNeto
        {
            get { return pesoNeto; }
            set
            {
                pesoNeto = value;
                NotifyPropertyChanged("PesoNeto");
            }
        }

        public string Producto
        {
            get { return producto; }
            set
            {
                producto = value;
                NotifyPropertyChanged("Producto");
            }
        }

        public string Observacion
        {
            get { return observacion; }
            set
            {
                observacion = value;
                NotifyPropertyChanged("Observacion");
            }
        }

        public FolioMultipesajeInfo FolioMultipesaje
        {
            get { return folioMultipesaje; }
            set
            {
                folioMultipesaje = value;
                NotifyPropertyChanged("FolioMultipesaje");
            }
        }

        public bool EsPesoBruto { get; set; }
        public DateTime? FechaPesoBruto { get; set; }
        public DateTime? FechaPesoTara { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public bool EnvioSAP
        {
            get { return envioSAP; }
            set
            {
                envioSAP = value;
                NotifyPropertyChanged("EnvioSAP");
            }
        }

        public int UsuarioCreacion
        {
            get { return usuarioCreacion; }
            set
            {
                usuarioCreacion = value;
                NotifyPropertyChanged("UsuarioCreacion");
            }
        }

        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
