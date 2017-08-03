using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Modelos
{
    public class AlimentacionCorralPorFormulaModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AlimentacionCorralPorFormulaModel()
        {
            FormulaDescripcion = string.Empty;
        }

        void notificarCambioColeccion(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        int formulaID;
        [SIE.Services.Info.Atributos.AtributoIgnorarColumnaExcel]
        public int FormulaID {
            get
            {
                return formulaID;
            }
            set
            {
                formulaID = value;
                notificarCambioColeccion("FormulaID");
            }
        }

        string formulaDescripcion;
        public string FormulaDescripcion
        {
            get
            {
                return formulaDescripcion;
            }
            set
            {
                formulaDescripcion = value;
                notificarCambioColeccion("FormulaDescripcion");
            }
        }

        int totalCorrales;
        public int TotalCorrales
        {
            get
            {
                return totalCorrales;
            }
            set
            {
                totalCorrales = value;
                notificarCambioColeccion("TotalCorrales");
            }
        }

    }
}
