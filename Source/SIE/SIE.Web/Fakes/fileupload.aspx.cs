using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Entidad;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Fakes
{
    public partial class fileupload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public override void ProcessRequest(HttpContext context )
        {
            var respuesta = new Response<int>();
            
            string fileName = context.Request.Form["filename"];
            int tipo = int.Parse(context.Request.Form["tipo"]);
            string ruta = "";
            string tipoFoto = "";
            try
            {
                
                var seguridad = (SeguridadInfo) HttpContext.Current.Session["Seguridad"];
                //informacion del la organzacion y usuario

                if (seguridad != null)
                {
                    ruta = ObtenerParametrosRuta(seguridad.Usuario.Organizacion.OrganizacionID).Valor;

                    int carpetaFoto = int.Parse(context.Request.Form["carpetaFoto"]);

                    if (carpetaFoto > 0)
                    {
                        switch (carpetaFoto)
                        {
                            case 1:
                                fileName = TipoFoto.Enfermo.ToString() + '/' + fileName;
                                tipoFoto = TipoFoto.Enfermo.ToString();
                                break;
                            case 2:
                                fileName = TipoFoto.Muerte.ToString() + '/' + fileName;
                                tipoFoto = TipoFoto.Muerte.ToString();
                                break;
                            case 3:
                                fileName = TipoFoto.Supervision.ToString() + '/' + fileName;
                                tipoFoto = TipoFoto.Supervision.ToString();
                                break;
                            case 4:
                                fileName = TipoFoto.Necropsia.ToString() + '/' + fileName;
                                tipoFoto = TipoFoto.Necropsia.ToString();
                                break;
                            case 5:
                                fileName = TipoFoto.Venta.ToString() + '/' + fileName;
                                tipoFoto = TipoFoto.Venta.ToString();
                                break;
                            default:
                                fileName = fileName;
                                break;
                        }
                        if (!(Directory.Exists(ruta + tipoFoto)))
                        {
                            Directory.CreateDirectory(ruta + tipoFoto);
                        }
                    }
                    if (tipo == 2) //Recibe el archivo en base64
                    {
                        

                        string file = context.Request.Form["fileupload"];
                        string camino;
                        camino = Path.Combine(ruta, fileName);

                        //Un arreglo de Bytes para descodificar la imagen
                        int indice = file.IndexOf(',');
                        file = file.Substring(indice + 1);
                        byte[] bytes;
                        bytes = Convert.FromBase64String(file);

                        var ms = new MemoryStream(bytes);

                        Image originalImage = Image.FromStream(ms);
                        var bitMap = ResizeImage(originalImage, 200, 140);
                        bitMap.Save(camino);

                        ms.Dispose();
                        originalImage.Dispose();
                        bitMap.Dispose();
                    }
                    else
                    {
                        HttpPostedFile file = context.Request.Files["fileupload"];
                        if (file != null)
                        {
                            string archivo = string.Format("{0}{1}", ruta, fileName);
                            Image originalImage = Image.FromStream(file.InputStream, true, true);
                            var bitMap = ResizeImage(originalImage, 200, 140);
                            bitMap.Save(archivo);
                        }
                    }
                }

                respuesta.esValido = true;
                respuesta.AgregarDatos(1);
                respuesta.AgregarMensaje("OK");

            }
            catch (Exception ex)
            {
              
                respuesta.esValido = false;
                respuesta.AgregarDatos(0);
                respuesta.AgregarMensaje(ex.Message);
            }

            context.Response.Write(respuesta);

        }

        private ConfiguracionParametrosInfo ObtenerParametrosRuta(int organizacionid)
        {
            ConfiguracionParametrosInfo retVal = null;
            var pl = new ConfiguracionParametrosPL();
            try
            {
                retVal = pl.ObtenerPorOrganizacionTipoParametroClave(new ConfiguracionParametrosInfo()
                {
                    Clave = ParametrosEnum.ubicacionFotosGuardar.ToString(),
                    TipoParametro = (int)TiposParametrosEnum.Imagenes,
                    OrganizacionID = organizacionid
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }

            return retVal;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
       
    }
}