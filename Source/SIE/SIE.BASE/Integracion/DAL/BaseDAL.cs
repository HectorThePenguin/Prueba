using System;

namespace SIE.Base.Integracion.DAL
{
    public abstract class BaseDAL : IDisposable
    {
        public event EventHandler Disposed;
        protected DataAccess da;

        public BaseDAL()
        {
            da = new DataAccess();
            inicializar();
        }

        protected abstract void inicializar();
        protected abstract void destruir();

        public void Dispose()
        {
            da.Disposed += (s, e) =>
            {
                da = null;
                destruir();
                if (Disposed != null)
                {
                    Disposed(this, EventArgs.Empty);
                }
            };
            da.Dispose();
        }

        public void SetConnection(BaseDAL baseDAL)
        {
            da = baseDAL.da;
        }

        protected DateTime FechaServidor()
        {
            return da.FechaServidor();
        }
    }
}
