using System.Text;
using System.Collections.Generic;

namespace Entidad
{
    public class Response<T>
    {
        public Response()
        {
            this.esValido = false;
            this.mensajes = new List<string>();
            this.cadenaMensajes = string.Empty;
        }
        internal bool esValido;
        private List<string> mensajes;
        private T datos;
        public bool EsValido
        {
            get
            {
                return this.esValido;
            }
        }
        public string Mensaje
        {
            get
            {
                return this.cadenaMensajes;
            }
        }
        private string cadenaMensajes;
        private bool tieneMensajes;
        public T Datos
        {
            get
            {
                return this.datos;
            }
        }
        public bool TieneMensajes
        {
            get
            {
                return this.tieneMensajes;
            }
        }
        public void ObtenMensajes()
        {
            StringBuilder msg = new StringBuilder();
            foreach (string texto in this.mensajes)
            {
                msg.AppendLine(texto);
                this.tieneMensajes = true;
            }
            this.cadenaMensajes = msg.ToString();
        }
        internal void AgregarDatos(T datos)
        {
            this.datos = datos;
        }
        internal void AgregarMensaje(params string[] Mensajes)
        {
            this.mensajes.AddRange(Mensajes);
        }
        public static Response<T> CrearResponse<T>(bool esValido, T datos, params string[] mensajes)
        {
            Response<T> response = new Response<T>();
            response.esValido = esValido;
            response.AgregarMensaje(mensajes);
            response.AgregarDatos(datos);
            response.ObtenMensajes();
            return response;
        }
        public static Response<T> CrearResponseVacio<T>(bool esValido, params string[] mensajes)
        {
            return CrearResponse<T>(esValido, default(T), mensajes);
        }
    }

}
