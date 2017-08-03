using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace Pruebas.PL
{
    [TestClass]
    public class GrupoFormulario
    {
        [TestMethod]
        public void FormulaObtenerPorId()
        {
            var pl = new GrupoFormularioPL();
            GrupoFormularioInfo info;

            try
            {
                info = pl.ObtenerPorID(1);
            }
            catch (Exception)
            {
                info = null;
            }
            Assert.AreNotEqual(info, null);
        }
    }
}
