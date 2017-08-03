using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAP.Consola
{
    class Program
    {
        static void Main(string[] args)
        {

            var crypto = System.Security.Cryptography.Rijndael.Create();
            var key = "porloqueesta,porloquevenga,artil";
            var vkey = "leriapresadapres";
            crypto.Key = System.Text.Encoding.Default.GetBytes(key);
            crypto.IV = System.Text.Encoding.Default.GetBytes(vkey);
            var decrypt = crypto.CreateEncryptor();
            var buffer = System.Text.Encoding.Default.GetBytes("data source=srv-siapdq;initial catalog=siap;integrated security=sspi;multipleactiveresultsets=false");
            buffer = decrypt.TransformFinalBlock(buffer, 0, buffer.Length);
            var result = Convert.ToBase64String(buffer);

            var parametros = new[]  {
                new { Org = 4, Fecha = new DateTime(2015,10,28) },
            };

            foreach (var parametro in parametros)
            {
                Console.WriteLine();
                Console.WriteLine();
                Stopwatch sw = new Stopwatch();
                try
                {
                    sw.Start();
                    new AjusteAretes((m) => Console.WriteLine(m), 4).Ejecutar(parametro.Org, parametro.Fecha);
                    sw.Stop();
                    Console.Write(string.Format("Cierre {0} - {1}, terminado en : {2}", parametro.Org, parametro.Fecha.ToString("yyyy-MM-dd"), sw.Elapsed.ToString()));
                    Console.WriteLine();
                    Console.WriteLine("Presione Enter para continuar...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    Console.Write(string.Format("Organizacion: {0} || Fecha: {1} || Error: {2}", parametro.Org, parametro.Fecha.ToString("yyyy-MM-dd"), ex.Message));
                }
                Console.WriteLine();
            }
            Console.WriteLine("Presione Enter para terminar...");
            Console.ReadKey();
        }
    }
}
