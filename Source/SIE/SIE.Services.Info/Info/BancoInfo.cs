using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Banco")]
    public class BancoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int bancoID;
        private string descripcion;
        private string telefono;
        private PaisInfo pais;
        //private int paisID;

        /// <summary>
        /// Identificador BancoID.
        /// </summary>
        /// <summary>
        /// Id de banco
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int BancoID
        {
            get { return bancoID; }
            set
            {
                bancoID = value;
                NotifyPropertyChanged("BancoID");
            }
        }
        /// <summary>
        /// Identificador BancoID.
        /// </summary>
        /// <summary>
        /// Id de banco
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public PaisInfo Pais
        {
            get { return pais; }
            set
            {
                pais = value;
                NotifyPropertyChanged("Pais");
            }
        }
        /// <summary>
        /// Descripción.
        /// </summary>
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                string valor = value;
                descripcion = valor == null ? string.Empty : valor.Trim();
                NotifyPropertyChanged("Descripcion");
            }
        }

        /// <summary>
        /// Descripción.
        /// </summary>
        public string Telefono
        {
            get { return telefono == null ? null : telefono.Trim(); }
            set
            {
                string valor = value;
                telefono = valor == null ? string.Empty : valor.Trim();
                NotifyPropertyChanged("Telefono");
            }
        }

        public BancoInfo()
        {
            Pais = new PaisInfo();
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
