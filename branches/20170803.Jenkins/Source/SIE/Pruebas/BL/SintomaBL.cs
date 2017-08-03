using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace Pruebas.BL
{
    [TestClass]
    public class SintomaBL
    {
        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void CuentaGuardarDescripcionExistente()
        {
            var bl = new SIE.Services.Servicios.BL.SintomaBL();

            SintomaInfo sintoma = null;

            try
            {
                sintoma = bl.ObtenerPorDescripcion("Dificultadad para Respirar");
                var lista = bl.ObtenerPorProblema(1);
            }
            catch (Exception)
            {
                Assert.AreEqual(sintoma.SintomaID, 0);
            }
        }
    }
}
