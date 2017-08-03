using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class Querys
{
#if local
    //public const string Conexion = "data source=srv-siapdq;initial catalog=siap;user id=usuariosie;password=Password01;multipleactiveresultsets=true;";
    public const string Conexion = "Data Source=SRV-SIAPDB;Initial Catalog=SIAP;User Id=usrsiap;password=5y5S1aPpprod;MultipleActiveResultSets=true;";
#else
    public const string Conexion = "context connection=true";
#endif
    public const string ObtenerConexion = "select valor from ParametroGeneral pg inner join Parametro p on pg.ParametroID = p.ParametroID where Clave = 'SQLCLRConnString'";
    public const string Error = "raiserror ('{0}', 16, 1)";
    public const string Fecha = "fec";
    public const string Organizacion = "org";
    public const string ObtenerAretes = "Select Arete, Corral, Lote from InterfazSPI where fechaSacrificio = @fec and organizacionID = @org and organizacionIDSacrificio = @org";
    public const string ObtenerLotesID = @"
        Select right('000' + rtrim(ltrim(c.Codigo)),3) + right('0000000000000' + rtrim(ltrim(l.Lote)),{1}) as CorralLote, l.LoteID 
        from Lote l inner join Corral c on l.CorralID = c.CorralID 
        inner join OrdenSacrificioDetalle osd on l.LoteID = osd.LoteID inner join OrdenSacrificio os on osd.OrdenSacrificioID = os.OrdenSacrificioID
        where right('000' + rtrim(ltrim(c.Codigo)),3) + right('0000000000000' + rtrim(ltrim(l.Lote)),{1}) in ({0})
        and os.activo = 1 and os.OrganizacionID = {2} and dateadd(d,0,datediff(d,0,os.FechaOrden)) = '{3}'
";
    public const string ObtenerAnimalesPorCorral = @"
        Select LoteID, count(distinct AnimalID) Cabezas from AnimalMovimiento 
        where Activo = 1 and TipoMovimientoID != 16 and LoteID in ({0}) and AnimalID not in (select AnimalID from LoteSacrificioDetalle)
        group by LoteID";
    public const string ValidarAretesDuplicados = @"
        select cast(a.Arete as bigint) Arete, COUNT(1) Coincidencias
        from Animal a inner join AnimalMovimiento am on	a.AnimalID = am.AnimalID and am.Activo = 1
        where OrganizacionID = @org group by cast(a.Arete as bigint)
        having COUNT(1) > 1";
    public const string ObtenerInventario = @"
        select distinct am.AnimalID, am.LoteID, a.Arete
        from Animal a inner join AnimalMovimiento am on a.AnimalID = am.AnimalID and am.Activo = 1 and am.TipoMovimientoID != 16
        where am.OrganizacionID = @org and am.AnimalID not in (select AnimalID from LoteSacrificioDetalle)";
    public const string ValidarMovimientosActivosDuplicados = @"
        select AnimalID from AnimalMovimiento where Activo = 1 and OrganizacionID = @org
        group by AnimalID having COUNT(1) > 1";
}

public class AjusteAretes
{
    Action<string> EnviarMensaje;
    int longitud;

    public AjusteAretes(Action<string> enviarMensaje, int longitud)
    {
        EnviarMensaje = enviarMensaje;
        this.longitud = longitud;
    }

    public void Ejecutar(int organizacionId, DateTime fechaSacrificio)
    {

        List<InterfazSPI> aretes = new List<InterfazSPI>();
        ValidarMovimientosActivosDuplicados_DB(organizacionId);
        aretes = ObtenerAretes_DB(organizacionId, fechaSacrificio);
        ValidarAretesDuplicados(aretes);
        aretes = ObtenerLotesId_DB(aretes, organizacionId, fechaSacrificio.ToString("yyyy/MM/dd"));
        var lotes = ObtenerLoteId(aretes);
        ValidarAretesDuplicados_DB(organizacionId);
        ValidarCorralesCompletos_DB(aretes, lotes);
        var inventario = ObtenerInventario_DB(organizacionId);
        var ajustados = AjustarAretes(aretes, inventario);

        var x = from a in aretes
                join b in ajustados on new { a.Arete, a.LoteID } equals new { b.Arete, LoteID = b.Corral }
                select a;

        x.ToList().ForEach(a => a.Ajustado = true);

        ValidarAjusteTerminado(aretes, inventario, lotes);
        Guardar_DB(ajustados);
    }

    #region funciones con Base de Datos
    void ValidarMovimientosActivosDuplicados_DB(int organizacionId)
    {

        using (SqlConnection conn = new SqlConnection(Querys.Conexion))
        {
            conn.Open();
            SqlCommand comm = new SqlCommand(Querys.ValidarMovimientosActivosDuplicados, conn);
            comm.Parameters.Add(new SqlParameter(Querys.Organizacion, organizacionId));
            SqlDataReader read = comm.ExecuteReader();
            using (read)
            {
                if (read.HasRows)
                    throw new Exception("Existen movimientos activos duplicados en SIAP, Favor de informar a sistemas.");
            }
            conn.Close();
        }
    }
    List<InterfazSPI> ObtenerAretes_DB(int organizacionId, DateTime fechaSacrificio)
    {

        List<InterfazSPI> interfaz = new List<InterfazSPI>();
        using (SqlConnection conn = new SqlConnection(Querys.Conexion))
        {
            conn.Open();
            SqlCommand comm = new SqlCommand(Querys.ObtenerAretes, conn);
            comm.Parameters.Add(new SqlParameter(Querys.Fecha, fechaSacrificio));
            comm.Parameters.Add(new SqlParameter(Querys.Organizacion, organizacionId));
            SqlDataReader read = comm.ExecuteReader();
            using (read)
            {
                while (read.Read())
                {
                    string arete = string.Empty;

                    try
                    {
                        arete = long.Parse(read.GetString(0)).ToString();
                    }
                    catch
                    {
                        throw new Exception("Existen aretes registrados no validos en Control de Piso. Favor de informar a sistemas");
                    }

                    interfaz.Add(new InterfazSPI()
                    {
                        Arete = arete,
                        OrganizacionID = organizacionId,
                        Corral = read.GetString(1).ConFormato(3),
                        Lote = read.GetString(2).ConFormato(this.longitud),
                        FechaSacrificio = fechaSacrificio
                    });
                }
            }
            conn.Close();
        }

        if (interfaz.Count == 0)
            throw new Exception(string.Format("No existen aretes sacrificados para la fecha {0}", fechaSacrificio.ToString("yyyy-MM-dd")));

        return interfaz.OrderBy(e => e.LoteID).ThenBy(e => long.Parse(e.Arete)).ToList();
    }
    List<InterfazSPI> ObtenerLotesId_DB(List<InterfazSPI> aretes, int organizacionId, string fecha)
    {

        var lotes = ObtenerCorralLote(aretes);
        var dicLotes = new Dictionary<string, int>();
        if (lotes.Count > 0)
        {
            string query = lotes.Count == 1 ? lotes.FirstOrDefault() : lotes.Aggregate((p1, p2) => string.Format("{0},{1}", p1, p2));
            query = string.Format(Querys.ObtenerLotesID, query, longitud, organizacionId, fecha);
            using (SqlConnection conn = new SqlConnection(Querys.Conexion))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataReader read = comm.ExecuteReader();
                using (read)
                {
                    while (read.Read())
                    {
                        var corralLote = read.GetString(0);
                        var loteid = read.GetInt32(1);
                        if (dicLotes.ContainsKey(corralLote))
                            dicLotes[corralLote] = loteid;
                        else
                            dicLotes.Add(corralLote, loteid);
                    }
                }
                conn.Close();
            }
        }

        aretes.ForEach(a =>
        {
            var key = string.Format("{0}{1}", a.Corral.ConFormato(3), a.Lote.ConFormato(this.longitud));
            if (dicLotes.ContainsKey(key))
            {
                a.LoteID = dicLotes[key];
            }
        });
        return aretes;
    }
    List<AnimalMovimiento> ObtenerInventario_DB(int organizacionID)
    {

        var animales = new List<AnimalMovimiento>();
        using (SqlConnection conn = new SqlConnection(Querys.Conexion))
        {
            conn.Open();
            SqlCommand comm = new SqlCommand(Querys.ObtenerInventario, conn);
            comm.Parameters.Add(new SqlParameter(Querys.Organizacion, organizacionID));
            SqlDataReader read = comm.ExecuteReader();
            using (read)
            {
                while (read.Read())
                {
                    var id = read.GetInt64(0);
                    var l = read.GetInt32(1);
                    string a = string.Empty;

                    try
                    {
                        a = long.Parse(read.GetString(2)).ToString();
                    }
                    catch
                    {
                        throw new Exception("Existen aretes registrados no validos en SIAP. Favor de informar a sistemas");
                    }

                    animales.Add(new AnimalMovimiento()
                    {
                        AnimalID = id,
                        Arete = a,
                        LoteID = l
                    });
                }
            }
            conn.Close();
        }
        return animales.OrderBy(e => e.LoteID).ThenBy(e => long.Parse(e.Arete)).ToList();
    }
    void ValidarCorralesCompletos_DB(List<InterfazSPI> aretes, List<int> lotesId)
    {

        var lotes = lotesId.Select(e => e.ToString()).ToList();
        if (lotes.Count > 0)
        {
            var dicLotes = new Dictionary<int, int>();

            string query = lotes.Count == 1 ? lotes.FirstOrDefault() : lotes.Aggregate((p1, p2) => string.Format("{0},{1}", p1.ToString(), p2.ToString()));
            query = string.Format(Querys.ObtenerAnimalesPorCorral, query);
            using (SqlConnection conn = new SqlConnection(Querys.Conexion))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataReader read = comm.ExecuteReader();
                using (read)
                {
                    while (read.Read())
                    {
                        var loteID = read.GetInt32(0);
                        var cabezas = read.GetInt32(1);
                        dicLotes.Add(loteID, cabezas);
                    }
                }
                conn.Close();
            }

            var dicSacrificados = ObtenerConteoSacrificadosPorCorral(aretes);

            ValidarCorralesCompletos(dicSacrificados, dicLotes, aretes);
        }
    }
    void ValidarAretesDuplicados_DB(int organizacionID)
    {

        using (SqlConnection conn = new SqlConnection(Querys.Conexion))
        {
            conn.Open();
            SqlCommand comm = new SqlCommand(Querys.ValidarAretesDuplicados, conn);
            comm.Parameters.Add(new SqlParameter(Querys.Organizacion, organizacionID));
            SqlDataReader read = comm.ExecuteReader();
            using (read)
            {
                if (read.HasRows)
                    throw new Exception("Existen aretes duplicados en SIAP, Favor de informar a sistemas.");
            }
            conn.Close();
        }
    }
    string ObtenerConexion_DB()
    {
        var str = string.Empty;
        using (SqlConnection conn = new SqlConnection(Querys.Conexion))
        {
            conn.Open();
            SqlCommand comm = new SqlCommand(Querys.ObtenerConexion, conn);
            SqlDataReader read = comm.ExecuteReader();
            using (read)
            {
                if (!read.HasRows)
                    throw new Exception("No se ha configurado el Parametro General 'SQLCLRConnString'.");
                while (read.Read())
                {
                    str = read.GetString(0);
                }
            }
            conn.Close();
        }
        var crypto = System.Security.Cryptography.Rijndael.Create();
        var key = "porloqueesta,porloquevenga,artil";
        var vkey = "leriapresadapres";
        crypto.Key = System.Text.Encoding.Default.GetBytes(key);
        crypto.IV = System.Text.Encoding.Default.GetBytes(vkey);
        var decrypt = crypto.CreateDecryptor();
        var buffer = Convert.FromBase64String(str);
        buffer = decrypt.TransformFinalBlock(buffer, 0, buffer.Length);
        return System.Text.Encoding.Default.GetString(buffer);
    }
    void Guardar_DB(List<Ajuste> movimientos)
    {
        using (SqlConnection conn = new SqlConnection(Querys.Conexion))
        {
            using (SqlCommand command = new SqlCommand("", conn))
            {
                SqlTransaction tr = null;
                try
                {
                    conn.Open();
                    tr = conn.BeginTransaction();
                    command.Transaction = tr;

                    EnviarMensaje("Desabilitar INDEX");

                    command.CommandText = "ALTER INDEX UX_Animal_Arete ON ANIMAL DISABLE;";
                    command.ExecuteNonQuery();

                    var listas = movimientos.Split(500);

                    EnviarMensaje("Actualizar Animal");
                    foreach (var lista in listas)
                    {
                        var t_sql = lista.Select(e => string.Format("Update Animal Set Arete = '{0}' Where AnimalId = {1}", e.Arete, e.AnimalID)).Aggregate((a, b) => string.Format("{0};{1};", a, b));
                        command.CommandTimeout = 15;
                        command.CommandText = t_sql;
                        command.ExecuteNonQuery();
                    }

                    EnviarMensaje("Habilitar INDEX");

                    command.CommandText = "ALTER INDEX UX_Animal_Arete ON ANIMAL REBUILD;";
                    command.ExecuteNonQuery();

                    tr.Commit();

                    EnviarMensaje("INDEX habilitado");
                }
                catch(Exception ex)
                {
                    EnviarMensaje("Error: " + ex.Message);

                    if (tr != null)
                        tr.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
    #endregion

    #region Ajuste de Aretes
    List<Ajuste> AjustarAretes(List<InterfazSPI> aretes, List<AnimalMovimiento> movimientos)
    {

        var ajustados = new List<Ajuste>();
        ajustados = DetectarAretesAjustados(aretes, movimientos);
        var iAjustados = ajustados.Count;
        var rAjustados = 0;
        while (iAjustados != rAjustados)
        {
            rAjustados = ajustados.Count();
            ajustados = DetectarAretesCruzados(aretes, movimientos, ajustados);
            ajustados = DetectarAretesRegados(aretes, movimientos, ajustados);
            iAjustados = ajustados.Count;
        }
        ajustados = DetectarAretesNoExistentes(aretes, movimientos, ajustados);

        return ajustados;
    }
    /***************************************************************************************************************************/
    /***************************************************************************************************************************/
    List<Ajuste> DetectarAretesAjustados(List<InterfazSPI> sacrificio, List<AnimalMovimiento> corrales)
    {

        var q1 = from a in sacrificio
                 join b in corrales on a.Arete equals b.Arete
                 select new Ajuste { Arete = a.Arete, Corral = a.LoteID, Destino = b.LoteID, AnimalID = b.AnimalID };

        var q2 = from b in corrales
                 join a in sacrificio on b.Arete equals a.Arete
                 select new Ajuste { Arete = b.Arete, Corral = b.LoteID, Destino = a.LoteID, AnimalID = b.AnimalID };

        var qListo = from a in q1
                     join b in q2 on new { a.Arete, a.Corral, a.Destino } equals new { b.Arete, b.Corral, b.Destino }
                     select a;
        var listo = qListo.ToList();

        return listo;
    }
    private List<Ajuste> DetectarAretesCruzados(List<InterfazSPI> sacrificio, List<AnimalMovimiento> corrales, List<Ajuste> listo)
    {

        var q1 = from a in sacrificio
                 join b in corrales on a.Arete equals b.Arete
                 select new Ajuste { Arete = a.Arete, Corral = b.LoteID, Destino = a.LoteID, AnimalID = b.AnimalID };

        Dictionary<string, int> dic = new Dictionary<string, int>();
        var x1 = listo.Select(a => a.Arete).ToList();
        var rq = q1.Where(e => !x1.Contains(e.Arete)).ToList();
        rq.ForEach(e =>
        {
            if (!dic.ContainsKey(e.ID_01))
                dic.Add(e.ID_01, 1);
            else
                dic[e.ID_01]++;
            e.Indice = dic[e.ID_01];
        });

        var q3 = from a in rq.Where(e => !x1.Contains(e.Arete))
                 join b in rq.Where(e => !x1.Contains(e.Arete)) on
                 new { l1 = a.Corral, l2 = a.Destino, a.Indice } equals new { l1 = b.Destino, l2 = b.Corral, b.Indice }
                 select new { a, b };

        var r3 = q3.ToList();

        r3.ForEach(e =>
        {
            if (!listo.Where(f => f.Arete == e.a.Arete).Any())
            {
                var ma = corrales.Where(x => x.Arete == e.a.Arete).FirstOrDefault();
                var mb = corrales.Where(x => x.Arete == e.b.Arete).FirstOrDefault();
                var tmp = e.b.Arete;
                mb.Arete = e.b.Arete = e.a.Arete;
                ma.Arete = e.a.Arete = tmp;
                listo.Add(e.a);
                listo.Add(e.b);
            }
        });

        return listo;
    }
    private List<Ajuste> DetectarAretesRegados(List<InterfazSPI> sacrificio, List<AnimalMovimiento> corrales, List<Ajuste> listo)
    {

        bool again = true;
        while (again)
        {
            var q1 = from a in sacrificio
                     join b in corrales on a.Arete equals b.Arete
                     select new Ajuste { Arete = a.Arete, Corral = a.LoteID, Destino = b.LoteID, AnimalID = b.AnimalID };

            var q2 = from b in corrales
                     join a in sacrificio on b.Arete equals a.Arete
                     select new Ajuste { Arete = b.Arete, Corral = b.LoteID, Destino = a.LoteID, AnimalID = b.AnimalID };

            var q3 = from b in corrales
                     select new Ajuste { Arete = b.Arete, Corral = b.LoteID, AnimalID = b.AnimalID };

            Dictionary<int, int> dic = new Dictionary<int, int>();
            var l1 = listo.Select(a => a.Arete).ToList();
            var r1 = q1.Where(e => !l1.Contains(e.Arete)).ToList();
            r1.ForEach(e =>
            {
                if (!dic.ContainsKey(e.Corral))
                    dic.Add(e.Corral, 1);
                else
                    dic[e.Corral]++;
                e.Indice = dic[e.Corral];
            });

            var x0 = r1.Select(e => e.Arete).ToList();

            var q4 = from b in q2
                     where x0.Contains(b.Arete)
                     select b;

            var s1 = sacrificio.Select(f => f.Arete).ToList();

            var x1 = r1.Union(listo.Where(e => s1.Contains(e.Arete))).Select(e => e.Arete).ToList();

            var q5 = from b in q3
                     where !x1.Contains(b.Arete)
                     select b;

            var r5 = q5.ToList();
            dic = new Dictionary<int, int>();
            r5.ForEach(e =>
            {
                if (!dic.ContainsKey(e.Corral))
                    dic.Add(e.Corral, 1);
                else
                    dic[e.Corral]++;
                e.Indice = dic[e.Corral];
            });

            var qr = from a in r1
                     join b in q4 on a.Arete equals b.Arete
                     join c in r5 on new { a.Corral, a.Indice } equals new { c.Corral, c.Indice }
                     select new { a, b, c };

            var rr = qr.ToList();

            again = rr.Any();

            rr.ForEach(e =>
            {
                var mb = corrales.Where(x => x.Arete == e.b.Arete).FirstOrDefault();
                var mc = corrales.Where(x => x.Arete == e.c.Arete).FirstOrDefault();
                var tmp = e.b.Arete;
                mb.Arete = e.b.Arete = e.c.Arete;
                mc.Arete = e.c.Arete = tmp;
                listo.RemoveAll(x => x.Arete == e.b.Arete);
                listo.RemoveAll(x => x.Arete == e.c.Arete);
                listo.Add(e.b);
                listo.Add(e.c);
            });
            listo = listo.OrderBy(e => e.Arete).ToList();

        }
        return listo;
    }
    private List<Ajuste> DetectarAretesNoExistentes(List<InterfazSPI> sacrificio, List<AnimalMovimiento> corrales, List<Ajuste> listo)
    {

        var q1 = from a in sacrificio
                 select new Ajuste { Arete = a.Arete, Corral = a.LoteID };

        var q3 = from b in corrales
                 select new Ajuste { Arete = b.Arete, Corral = b.LoteID, AnimalID = b.AnimalID };

        Dictionary<int, int> dic = new Dictionary<int, int>();
        var x1 = listo.Select(a => a.Arete).ToList();
        var r1 = q1.Where(e => !x1.Contains(e.Arete)).ToList();
        r1.ForEach(e =>
        {
            if (!dic.ContainsKey(e.Corral))
                dic.Add(e.Corral, 1);
            else
                dic[e.Corral]++;
            e.Indice = dic[e.Corral];
        });

        var s1 = sacrificio.Select(e => e.Arete).ToList();

        var q5 = from b in q3
                 where !s1.Contains(b.Arete)
                 select b;

        var r5 = q5.ToList();
        dic = new Dictionary<int, int>();
        r5.ForEach(e =>
        {
            if (!dic.ContainsKey(e.Corral))
                dic.Add(e.Corral, 1);
            else
                dic[e.Corral]++;
            e.Indice = dic[e.Corral];
        });

        var qr = from a in r1
                 join b in r5 on new { a.Corral, a.Indice } equals new { b.Corral, b.Indice }
                 select new { a, b };

        var rr = qr.ToList();

        rr.ForEach(e =>
        {
            var mb = corrales.Where(x => x.Arete == e.b.Arete).FirstOrDefault();
            listo.RemoveAll(x => x.Arete == e.b.Arete);
            mb.Arete = e.b.Arete = e.a.Arete;
            listo.Add(e.b);
        });

        listo = listo.OrderBy(e => e.Arete).ToList();

        return listo;
    }
    /***************************************************************************************************************************/
    /***************************************************************************************************************************/
    #endregion

    #region Otras funciones
    void ValidarCorralesCompletos(Dictionary<int, int> sacrificio, Dictionary<int, int> lotes, List<InterfazSPI> aretes)
    {

        var error = string.Empty;

        var corrales = aretes.Select(e => new { e.LoteID, Corral = e.Corral.ConFormato(3) }).Distinct().ToDictionary(e => e.LoteID, e => e.Corral);

        foreach (var sacrificado in sacrificio)
        {
            if (lotes.ContainsKey(sacrificado.Key))
            {
                var disponible = lotes[sacrificado.Key];

                if (sacrificado.Value > disponible)
                {
                    error += string.Format("Corral {0} faltan {1} cabezas, ", corrales[sacrificado.Key], sacrificado.Value - disponible);
                }
            }
            else
            {
                error += string.Format("Corral {0} faltan {1} cabezas, ", corrales[sacrificado.Key], sacrificado.Value);
            }
        }

        if (error.Length > 0)
        {
            error = "No se puede realizar el ajuste de Aretes ya que faltan cabezas en los siguientes Corrales: " + error + "favor de completar estos corrales.";
            throw new Exception(error);
        }
    }
    void ValidarAretesDuplicados(List<InterfazSPI> aretes)
    {

        var q = from a in aretes
                group a by a.Arete into g
                where g.Count() > 1
                select g.Key;
        if (q.Any())
            throw new Exception("Existen aretes duplicados en Control de Piso, Favor de informar a sistemas.");
    }
    List<string> ObtenerCorralLote(List<InterfazSPI> aretes)
    {

        return (from a in aretes
                select string.Format("'{0}{1}'", a.Corral.ConFormato(3), a.Lote.ConFormato(this.longitud))
                ).Distinct()
                .ToList();
    }
    Dictionary<int, int> ObtenerConteoSacrificadosPorCorral(List<InterfazSPI> aretes)
    {
        var q = from a in aretes
                group a by a.LoteID into g
                select new { LoteID = g.Key, Cabezas = g.Count() };

        return q.ToDictionary(e => e.LoteID, e => e.Cabezas);
    }
    List<int> ObtenerLoteId(List<InterfazSPI> aretes)
    {
        var q = from a in aretes
                select a.LoteID;
        return q.Distinct().ToList();
    }
    void ValidarAjusteTerminado(List<InterfazSPI> aretes, List<AnimalMovimiento> animales, List<int> lotesId)
    {

        if (aretes.Where(e => !e.Ajustado).Any())
            throw new Exception("No se han podido ajustar todos los aretes");

        var q = from a in animales
                group a by a.Arete into g
                where g.Count() > 1
                select g.Key;


        if (q.Any())
            throw new Exception("Existe posibilidad de aretes duplicados, Favor de informar a sistemas.");

        var q2 = from a in animales
                 group a by a.AnimalID into g
                 where g.Count() > 1
                 select g.Key;


        if (q2.Any())
            throw new Exception("Existe posibilidad de animales duplicados, Favor de informar a sistemas.");

        if (lotesId.Count > 0)
        {
            var dicLotes = animales.Where(e => lotesId.Contains(e.LoteID)).GroupBy(e => e.LoteID).ToDictionary(e => e.Key, e => e.Count());
            var dicSacrificados = ObtenerConteoSacrificadosPorCorral(aretes);
            ValidarCorralesCompletos(dicSacrificados, dicLotes, aretes);
        }
    }
    #endregion
}
public static class Extension
{
    public static string SinFormato(this string me)
    {
        return me.Trim().TrimStart('0');
    }
    public static string ConFormato(this string me, int size)
    {
        return me.SinFormato().PadLeft(size, '0');
    }

    public static List<AnimalMovimiento> ObtenerAnimal(this List<AnimalMovimiento> me, long id)
    {
        return me.Where(e => e.AnimalID == id).ToList();
    }
    public static List<Ajuste> ObtenerAnimal(this List<Ajuste> me, long id)
    {
        return me.Where(e => e.AnimalID == id).ToList();
    }
    public static List<AnimalMovimiento> ObtenerAnimal(this List<AnimalMovimiento> me, string arete)
    {
        return me.Where(e => e.Arete == arete).ToList();
    }
    public static List<Ajuste> ObtenerAnimal(this List<Ajuste> me, string arete)
    {
        return me.Where(e => e.Arete == arete).ToList();
    }

    public static List<Ajuste> ObtenerAnimalesDuplicados(this List<Ajuste> me)
    {
        var q = from m in me
                group m by m.AnimalID into g
                where g.Count() > 1
                select new { g.Key, Lista = g.ToList() };

        return q.SelectMany(e => e.Lista).ToList();
    }
    public static List<Ajuste> ObtenerAretesDuplicados(this List<Ajuste> me)
    {
        var q = from m in me
                group m by m.Arete into g
                where g.Count() > 1
                select new { g.Key, Lista = g.ToList() };

        return q.SelectMany(e => e.Lista).ToList();
    }

    public static List<List<Ajuste>> Split(this List<Ajuste> source, int listas)
    {
        var nlistas = Math.Ceiling((decimal)source.Count / (decimal)listas);
        return source
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index % nlistas)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();
    }

#if local
        public static void GrabarEnDisco(this List<Ajuste> me)
        {
            var xml = new XElement("Sacrificio",
                from x in me
                select new XElement("Animal",
                    new XElement("Arete", x.Arete),
                    new XElement("Id", x.AnimalID),
                    new XElement("Corral", x.Corral)
                    ));
            xml.ToString().Grabar("Ajuste", false);
        }
        public static void GrabarEnDisco(this List<AnimalMovimiento> me)
        {
            var xml = new XElement("Sacrificio",
                from x in me
                select new XElement("Animal",
                    new XElement("Arete", x.Arete),
                    new XElement("Id", x.AnimalID),
                    new XElement("Corral", x.LoteID.ToString())
                    ));
            xml.ToString().Grabar("AnimalMovimiento", false);
        }
        public static void Grabar(this string me, string prefix, bool conFecha = true)
        {
            System.IO.File.WriteAllText(
                conFecha ?
                string.Format("C:\\Xml\\{0}_{1}.xml", prefix, DateTime.Now.ToString("yyyyMMdd_HHmmss_fff")) :
                string.Format("C:\\Xml\\{0}.xml", prefix),
                me
                );
        }
#endif
}

