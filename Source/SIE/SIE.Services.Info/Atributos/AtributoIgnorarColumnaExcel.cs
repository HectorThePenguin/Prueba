using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AtributoIgnorarColumnaExcel : Attribute
    {

    }
}
