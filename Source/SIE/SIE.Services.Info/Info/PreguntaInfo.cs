using System;
using BLToolkit.Mapping;
using SIE.Services.Info.Enums;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Pregunta")]
    public class  PreguntaInfo : INotifyPropertyChanged
    {
        private string descripcion = string.Empty;
        private string valor = string.Empty;
        /// <summary>
        /// Acceso PreguntaID 
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int PreguntaID { get; set; }

        /// <summary> 
        ///	TipoPreguntaID  
        /// </summary> 
        [MapIgnore]
        public TipoPreguntaInfo TipoPregunta { get; set; }

        /// <summary> 
        ///	TipoPreguntaID  
        /// </summary> 
        public int TipoPreguntaID { get; set; }

        /// <summary>
        /// Acceso Descripcion 
        /// </summary>
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                string valor = value;
                descripcion = valor == null ? valor : valor.Trim();
                NotifyPropertyChanged("Descripcion");
            }
        }

        /// <summary>
        /// Acceso Valor
        /// </summary>
        public string Valor
        {
            get { return valor == null ? null : valor.Trim(); }
            set
            {
                string val = value;
                valor = val == null ? val : val.Trim();
                NotifyPropertyChanged("Valor");
            }
        }

        /// <summary>
        /// Acceso Valor
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore]
        [MapIgnore]
        public bool Activo
        {
            get { return Estatus == EstatusEnum.Activo; }
            set { Estatus = value ? EstatusEnum.Activo : EstatusEnum.Inactivo; }
        }

        /// <summary>
        /// Acceso Valor
        /// </summary>
        [BLToolkit.Mapping.MapField("Activo")]
        public EstatusEnum Estatus { get; set; }

        /// <summary> 
        ///	Id del Usuario que crea el Registro  
        /// </summary> 
        public int UsuarioCreacionID { get; set; }

        /// <summary> 
        ///	Id del Usuario que modifica el Registro  
        /// </summary> 
        public int UsuarioModificacionID { get; set; }

        /// <summary>
        /// Fecha de Creacion
        /// </summary>
        [BLToolkit.DataAccess.NonUpdatable]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de Modificacion
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

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
