using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace Pruebas.BL
{
    [TestClass]
    public class SolicitudProductoBL
    {
        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void ObtenerPorID()
        {
            //var bl = new SIE.Services.Servicios.BL.SolicitudProductoBL();
            //SolicitudProductoInfo solicitudProducto = null;
            //try
            //{
            //    solicitudProducto = bl.ObtenerPorID(new SolicitudProductoInfo {SolicitudProductoID = 3});
            //    Assert.AreNotEqual(solicitudProducto, null);
            //}
            //catch (Exception)
            //{
            //    Assert.AreEqual(solicitudProducto.SolicitudProductoID, 0);
            //}
        }

        /// <summary>
        /// Probar guardar una entidad con una descricpión existente
        /// </summary>
        [TestMethod]
        public void ObtenerPorPagina()
        {
            var bl = new SIE.Services.Servicios.BL.SolicitudProductoBL();

            var filtro = new FolioSolicitudInfo
                             {
                                 FolioSolicitud = 0,
                                 Descripcion = string.Empty,
                                 Usuario = new UsuarioInfo {UsuarioID = 4, OrganizacionID = 1},
                                 Activo = EstatusEnum.Activo
                             };

            var solicitudes = bl.ObtenerPorPagina(new PaginacionInfo {Inicio = 1, Limite = 15}, filtro);
            Assert.IsNotNull(solicitudes, null);
        }
    }
}
