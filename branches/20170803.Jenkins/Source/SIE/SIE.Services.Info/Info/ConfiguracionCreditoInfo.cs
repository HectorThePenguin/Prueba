﻿using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ConfiguracionCreditoInfo : INotifyPropertyChanged
    {
        private int configuracionCreditoID;
        private TipoCreditoInfo tipoCredito;
        private PlazoCreditoInfo plazoCredito;
        private EstatusEnum activo;
        private int usuarioCreacionID;
        private int usuarioModificacionID;

        private List<ConfiguracionCreditoRetencionesInfo> retenciones;

        public int ConfiguracionCreditoID
        {
            get { return configuracionCreditoID; }
            set
            {
                configuracionCreditoID = value;
                NotifyPropertyChanged("ConfiguracionCreditoID");
            }
        }

        public TipoCreditoInfo TipoCredito
        {
            get { return tipoCredito; }
            set
            {
                tipoCredito = value;
                NotifyPropertyChanged("TipoCredito");
            }
        }

        public PlazoCreditoInfo PlazoCredito
        {
            get { return plazoCredito; }
            set
            {
                plazoCredito = value;
                NotifyPropertyChanged("PlazoCredito");
            }
        }

        public EstatusEnum Activo
        {
            get { return activo; }
            set
            {
                activo = value;
                NotifyPropertyChanged("Activo");
            }
        }

        public int UsuarioCreacionID
        {
            get { return usuarioCreacionID; }
            set
            {
                usuarioCreacionID = value;
                NotifyPropertyChanged("UsuarioCreacionID");
            }
        }

        public int UsuarioModificacionID
        {
            get { return usuarioModificacionID; }
            set
            {
                usuarioModificacionID = value;
                NotifyPropertyChanged("UsuarioModificacionID");
            }
        }

        public List<ConfiguracionCreditoRetencionesInfo> Retenciones
        {
            get { return retenciones; }
            set
            {
                retenciones = value;
                NotifyPropertyChanged("Retenciones");
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