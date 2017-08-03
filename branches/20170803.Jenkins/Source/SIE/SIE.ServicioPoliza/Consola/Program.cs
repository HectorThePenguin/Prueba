using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new SIE.PolizaServicio.PolizaServicio();
            p.Start(args);

            Console.WriteLine("Presione enter para terminar");
            Console.ReadLine();

            p.Stop();
        }
    }
}
