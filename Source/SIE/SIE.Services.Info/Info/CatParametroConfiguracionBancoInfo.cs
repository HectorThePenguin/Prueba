using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    
    public class CatParametroConfiguracionBancoInfo : BitacoraInfo, INotifyPropertyChanged, ICloneable
    {

        private int catParametroConfiguracionBancoID;
        private BancoInfo bancoID;        
        private CatParametroBancoInfo parametroID;
        private int x;
        private int y;
        private int width;

        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int CatParametroConfiguracionBancoID
        {
            get { return catParametroConfiguracionBancoID; }
            set
            {
                catParametroConfiguracionBancoID = value;
                NotifyPropertyChanged("CatParametroConfiguracionBancoID");
            }
        }

        public BancoInfo BancoID
        {
            get { return bancoID; }
            set
            {
                bancoID = value;
                NotifyPropertyChanged("BancoID");
            }
        }                

        public CatParametroBancoInfo ParametroID
        {
            get { return parametroID; }
            set
            {
                parametroID = value;
                NotifyPropertyChanged("ParametroID");
            }
        }

        public int X
        {
            get { return x; }
            set
            {
                x = value;
                NotifyPropertyChanged("X");
            }
        }

        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                NotifyPropertyChanged("Y");
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                NotifyPropertyChanged("Width");
            }
        }

        public override string ToString()
        {
            try
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}", CatParametroConfiguracionBancoID, parametroID.ParametroID, bancoID.BancoID, X, Y,Width,Activo);
            }
            catch (Exception)
            {
                return string.Empty;
            }
           
        }

        public object Clone()
        {
            return this.MemberwiseClone();
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
