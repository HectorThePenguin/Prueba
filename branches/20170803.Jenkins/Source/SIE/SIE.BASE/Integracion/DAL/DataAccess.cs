using System;
using System.Linq;
using BLToolkit.Data.Linq;

namespace SIE.Base.Integracion.DAL
{
    public class DataAccess : IDisposable
    {
        #region variables
        public event EventHandler Disposed;
        BLToolkit.Data.DbManager db;
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor
        /// </summary>
        public DataAccess()
        {
            db = new BLToolkit.Data.DbManager();
        }

        /// <summary>
        /// Libera los recursos del objeto
        /// </summary>
        public void Dispose()
        {
            db.Disposed += (s, e) =>
            {
                db = null;
                if (Disposed != null)
                {
                    Disposed(this, EventArgs.Empty);
                }
            };
            db.Dispose();
        }
        #endregion

        /// <summary>
        /// Método para inicializar el objeto
        /// </summary>
        /// <typeparam name="T">Tipo de dato del elemento</typeparam>
        /// <returns></returns>
        public T inicializarAccessor<T>() where T : BLToolkit.DataAccess.DataAccessor
        {
            return BLToolkit.DataAccess.DataAccessor.CreateInstance<T>(db);
        }

        /// <summary>
        /// Obtiene los datos de la tabla
        /// </summary>
        /// <typeparam name="T">Tipo de dato del elemento</typeparam>
        /// <returns></returns>
        public IQueryable<T> Tabla<T>() where T : class
        {
            return db.GetTable<T>();
        }

        /// <summary>
        /// Inserta un elemento a la tabla
        /// </summary>
        /// <typeparam name="T">Tipo de dato del elemento</typeparam>
        /// <param name="elemento">Datos del elemento</param>
        /// <returns></returns>
        public int Insertar<T>(T elemento) where T : class
        {
            var result = db.InsertWithIdentity<T>(elemento);
            var id = 0;
            int.TryParse(result.ToString(), out id);
            var propertyID = (from p in elemento.GetType().GetProperties()
                              where p.GetCustomAttributes(typeof(BLToolkit.DataAccess.IdentityAttribute), true).Any()
                              select p).FirstOrDefault();
            if (propertyID != null) propertyID.SetValue(elemento, id, null);
            return id;
        }

        /// <summary>
        /// Inserta una lista de elementos a la tabla
        /// </summary>
        /// <typeparam name="T">Tipo de dato de la tabla</typeparam>
        /// <param name="elementos">Lista de registros a insertar</param>
        /// <returns></returns>
        public int Insertar<T>(T[] elementos) where T : class
        {
            return db.InsertBatch(elementos);
        }

        /// <summary>
        /// Actualiza un elemento de la tabla
        /// </summary>
        /// <typeparam name="T">Tipo de dato de la tabla</typeparam>
        /// <param name="elemento">Registro a actualizar</param>
        /// <returns></returns>
        public int Actualizar<T>(T elemento) where T : class
        {
            return db.Update(elemento);
        }

        /// <summary>
        /// Permite actualizar uno o mas campos segun la expresion Lambda especificada en sentence, afectando los elementos especificados en la expresion Lambda predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public int Actualizar<T>(Func<T, bool> predicate, Func<T, T> sentence) where T : class 
        {
            return db.GetTable<T>().Update(e => predicate(e), e => sentence(e));
        }

        /// <summary>
        /// Actualiza una lista de elementos en la tabla
        /// </summary>
        /// <typeparam name="T">Tipo de dato de la tabla</typeparam>
        /// <param name="elementos">Lista de registros a actualzar</param>
        /// <returns></returns>
        public int Actualiza<T>(T[] elementos) where T : class
        {
            return db.Update<T[]>(elementos);
        }
        
        /// <summary>
        /// Obtiene la fecha del servidor.
        /// </summary>
        /// <returns></returns>
        public DateTime FechaServidor()
        {
            return db.Select(() => Sql.CurrentTimestamp);
        }
    }
}
