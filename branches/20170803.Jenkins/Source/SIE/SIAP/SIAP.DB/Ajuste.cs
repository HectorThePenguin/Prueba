using System;
using System.Collections.Generic;
using System.Text;

public class Ajuste
{
    public int Indice { get; set; }
    public string Arete { get; set; }
    public long AnimalID { get; set; }
    public int Corral { get; set; }
    public int Destino { get; set; }
    public bool Cambio { get; set; }

    public string ID
    {
        get
        {
            return Arete.ToString().PadLeft(20, '0')
                + ID_01;
        }
    }
    public string ID_01
    {
        get
        {
            return (Corral.ToString() ?? string.Empty).PadLeft(10, '0')
                + (Destino.ToString() ?? string.Empty).PadLeft(10, '0');
        }
    }

    public override bool Equals(object obj)
    {
        return this.ID == (obj as Ajuste).ID;
    }
    public override int GetHashCode()
    {
        return this.ID.GetHashCode();
    }
    public override string ToString()
    {
        return this.Arete;
    }
}
