using SIE.Services.Info.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ProductoTiempoEstandarInfo
    {
        public ProductoTiempoEstandarInfo()
        {
            Producto = new ProductoInfo();
            Tiempo = string.Empty;
            Estatus = EstatusEnum.Inactivo;
        }
        public int ProductoTiempoEstandarID { get; set; }

        public string Tiempo { get; set; }

        public EstatusEnum Estatus { get; set; }

        public ProductoInfo Producto { get; set; }

        public int UsuarioCreacionID { get; set; }
    }
}