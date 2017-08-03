using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Services;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;


namespace SIE.Web.PlantaAlimentos
{

    public partial class ProduccionFormulasAutomaticas : PageBase
    {
        #region Globales

        readonly Dictionary<string, string> dic = new Dictionary<string, string>();
        private static List<ProduccionFormulaInfo> ListaGlobal = new List<ProduccionFormulaInfo>();
        private static List<DetalleBatchInfo> ListaGlobalDetalleBatch = new List<DetalleBatchInfo>();
        public static DateTime fecha;

        #endregion

        #region

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtFecha.Text))
            {
                txtFecha.Text = DateTime.Now.ToShortDateString();
                fecha = DateTime.Now;
            }
        }

        protected void btnCargarImagen_Click(object sender, EventArgs e)
        {
            var error = new ResultadoValidacion();
            try
            {
                ListaGlobal = new List<ProduccionFormulaInfo>();
                ListaGlobalDetalleBatch = new List<DetalleBatchInfo>();
                if (FileUploadControl.HasFile)
                {
                    // Se verifica que la extensión sea de un formato válido
                    string ext = FileUploadControl.PostedFile.FileName;
                    ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();
                    string formatos = "txt";
                    if (formatos == ext)
                    {
                        txtArchivoProduccion.Text = FileUploadControl.FileName;
                        // Se carga la ruta física de la carpeta temp del sitio
                        string ruta = Server.MapPath("~/temp");

                        // Si el directorio no existe, crearlo
                        if (!Directory.Exists(ruta))
                            Directory.CreateDirectory(ruta);

                        string archivo = String.Format("{0}\\{1}", ruta, FileUploadControl.FileName);
                        FileUploadControl.SaveAs(archivo);

                        error = CrearArchivos(archivo);

                        if (error.Mensaje != GetLocalResourceObject("OK").ToString())
                        {
                            CrearScriptsError(error.Mensaje);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "myScript", "CargarGrid();", true);
                        }
                    }
                }
                else
                {
                    error.Mensaje = GetLocalResourceObject("vacio").ToString();
                    CrearScriptsError(error.Mensaje);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if(error == null)
                {
                    error = new ResultadoValidacion();
                }
                error.Mensaje = "Ocurrio un error al procesar el archivo.";
                CrearScriptsError(error.Mensaje);
            }
        }

        #endregion

        #region Metodos

        public ResultadoValidacion CrearArchivos(string archivo)
        {
            //string resultado;
            var resultado = new ResultadoValidacion();
            resultado.Mensaje = GetLocalResourceObject("OK").ToString();
            var file = new StreamReader(archivo);
            string lineActual = string.Empty;
            try
            {
                // Read the file and display it line by line.

                var procesarlo = new ProcesarArchivoInfo();
                var listaprocesarlo = new List<ProcesarArchivoInfo>();

                string linea;
                while ((linea = file.ReadLine()) != null)
                {
                    //se validan las columnas del archivo
                    string[] words = linea.Split('\t');
                    lineActual = linea;
                    procesarlo = new ProcesarArchivoInfo
                                     {
                                         batch = int.Parse(words[0]),
                                         Formula = words[1].Trim(),
                                         Codigo = words[2].Trim(),
                                         Meta = int.Parse(words[3]),
                                         Real = int.Parse(words[4]),
                                         Fecha = words[5].Trim(),
                                         Hora = words[6].Trim(),
                                         Marca = words[7].Trim(),
                                         Rotomix = words[8].Trim()
                                     };
                    listaprocesarlo.Add(procesarlo);
                }
                file.Close();
                resultado = ValidacionesLista(listaprocesarlo);

            }
            catch (Exception er)
            {
                file.Close();
                Logger.Error(er);
                resultado.CodigoMensaje = 1;
                resultado.Mensaje = string.Format("{0} {1}", "Ocurrio un error al procesar el archivo, linea",
                                                  lineActual);
            }
            return resultado;
        }

        public string ValidarColumnas(string[] words)
        {
            string validar;
            validar = GetLocalResourceObject("OK").ToString();
            string[] columnas = new[] { 
                GetLocalResourceObject("columnaBatch").ToString(), 
                GetLocalResourceObject("columnaFormula").ToString(), 
                GetLocalResourceObject("columnaCodigo2").ToString(),
                GetLocalResourceObject("columnaMeta").ToString(), 
                GetLocalResourceObject("columnaReal").ToString(), 
                GetLocalResourceObject("columnaFecha").ToString(), 
                "", 
                GetLocalResourceObject("columnaHora").ToString(), 
                "", 
                GetLocalResourceObject("columnaMarca").ToString(), 
                GetLocalResourceObject("columnaNombre").ToString() };

            try
            {
                if (columnas[0] != words[0].Substring(1, words[0].Length - 1)) { validar = "ColumnaBatch"; }

                if (columnas[1] != words[1].Substring(0, words[1].Length)) { validar = "ColumnaFormula"; }

                if (columnas[2] != words[2].Substring(1, words[2].Length - 1)) { validar = "ColumnaCodigo"; }

                if (columnas[3] != words[3].Substring(1, words[3].Length - 1)) { validar = "ColumnaMeta"; }

                if (columnas[4] != words[4].Substring(1, words[4].Length - 1)) { validar = "ColumnaReal"; }

                if (columnas[5] != words[5].Substring(1, words[5].Length - 1)) { validar = "ColumnaFecha"; }

                if (columnas[7] != words[7].Substring(1, words[7].Length - 1)) { validar = "ColumnaHora"; }

                if (columnas[9] != words[9].Substring(1, words[9].Length - 1)) { validar = "ColumnaMarca"; }

                if (columnas[10] != words[10].Substring(1, words[10].Length - 1)) { validar = "ColumnaNombre"; }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                validar = string.Format("{0}", "Ocurrio un error al procesar el archivo, linea");
            }

            return validar;
        }

        public ResultadoValidacion ValidacionesLista(List<ProcesarArchivoInfo> listaprocesarlo)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            string mensajeOK = string.Empty;
            var localResourceObject = GetLocalResourceObject("OK");
            if (localResourceObject != null)
            {
                mensajeOK = localResourceObject.ToString();
            }

            string nombrerotomix = "";

            var resultado = new ResultadoValidacion
                                                {
                                                    Mensaje = mensajeOK
                                                };

            var listaFormulaDetalle = new List<ProduccionFormulaDetalleInfo>();
            var listaFormulaProduccionBatch = new List<ProduccionFormulaBatchInfo>();

            try
            {
                List<ProcesarArchivoInfo> listaparadetalle = listaprocesarlo;
                //sacamos las distintas formulas del archivo de texto
                List<ProcesarArchivoInfo> formulas = listaprocesarlo.GroupBy(p => p.Formula).Select(g => g.First()).ToList();

                foreach (var renglon in formulas) //recorremos por formulas
                {
                    ProcesarArchivoInfo elemento = renglon; 
                    //sacamos los batchs
                    var batches = from w in listaprocesarlo
                                  where w.Formula == elemento.Formula
                                  group w by w.batch into g
                                  select new
                                             {
                                                 FirstLetter = g.Key, 
                                                 Words = g
                                             };

                    resultado.Mensaje = ValidarCodigoFormula(elemento);
                    if (resultado.Mensaje != mensajeOK)
                    {
                        return resultado;
                    }

                    int sumatoriameta = 0; int sumatoriareal = 0;
                    listaFormulaProduccionBatch = new List<ProduccionFormulaBatchInfo>();
                    foreach (var batch in batches)
                    {
                        List<ProcesarArchivoInfo> lista = listaprocesarlo.Where(k => k.Formula == elemento.Formula && k.batch == batch.FirstLetter).ToList();
                        var detallesBatchs = new DetalleBatchInfo();
                        int sumatoriarealbatch = 0;

                        foreach (ProcesarArchivoInfo elmentoArchivo in lista)
                        {
                            if (elmentoArchivo.Marca == "2")
                            {
                                resultado.Mensaje = ValidarColumnaCodigoLista(elmentoArchivo);
                                if (resultado.Mensaje != mensajeOK)
                                {
                                    return resultado;
                                }
                            }
                            else
                            {
                                resultado.Mensaje = ValidarFechaYHora(elmentoArchivo);
                                if (resultado.Mensaje != mensajeOK)
                                {
                                    return resultado;
                                }

                                //valido que exista el codigo del producto
                                resultado.Mensaje = ValidarCodigoProducto(elmentoArchivo);
                                if (resultado.Mensaje != mensajeOK)
                                {
                                    return resultado;
                                }

                                resultado.Mensaje = ValidarCodigoRotomix(elmentoArchivo);
                                if (resultado.Mensaje != mensajeOK)
                                {
                                    return resultado;
                                }
                                resultado.Mensaje = ValidarCodigoFormula(elmentoArchivo);
                                if (resultado.Mensaje != mensajeOK)
                                {
                                    return resultado;
                                }
                                resultado.Mensaje = ValidarProduccionFormula(elmentoArchivo);
                                if (resultado.Mensaje != mensajeOK)
                                {
                                    return resultado;
                                }
                                //var elementosDuplicados =
                                //    lista.Where(det => det.Codigo.Equals(elmentoArchivo.Codigo)
                                //                         && det.Formula.ToUpper().Trim().Equals(elmentoArchivo.Formula.ToUpper().Trim())
                                //                         && det.Rotomix.ToUpper().Trim().Equals(elmentoArchivo.Rotomix.ToUpper().Trim())
                                //                         && det.Hora.Equals(elmentoArchivo.Hora)).ToList();

                                //if(elementosDuplicados.Count > 1)
                                //{
                                //    resultado.Mensaje = string.Format("El renglon se encuentra duplicado, Batch: {0}, Producto: {1}, RotoMix: {2}, Hora: {3}", elmentoArchivo.batch, elmentoArchivo.Codigo, elmentoArchivo.Rotomix, elmentoArchivo.Hora);
                                //    return resultado;
                                //}

                                sumatoriameta = sumatoriameta + elmentoArchivo.Meta;
                                sumatoriareal = sumatoriareal + elmentoArchivo.Real;
                                sumatoriarealbatch = sumatoriarealbatch + elmentoArchivo.Real;
                                nombrerotomix = elmentoArchivo.Rotomix;
                            }
                        }

                        detallesBatchs.Batch = batch.FirstLetter;
                        detallesBatchs.KilosProducidos = sumatoriarealbatch;
                        detallesBatchs.Rotomix = nombrerotomix;
                        detallesBatchs.Formula = renglon.Formula;
                        ListaGlobalDetalleBatch.Add(detallesBatchs);

                        //creamos el detalle de la tabla Produccion Formula Detalle
                        listaFormulaDetalle = CrearListaProduccionFormulaDetalle(listaparadetalle, renglon.Formula);

                        if (listaFormulaDetalle == null)
                        { 
                            resultado.Mensaje = "ingrediente";
                            return resultado;
                        }

                        //creamos el detalle de la tabla Produccion Formula Batch
                        listaFormulaProduccionBatch.AddRange(CrearListaProduccionFormulaBatch(listaparadetalle, renglon.Formula, batch.FirstLetter, nombrerotomix));
                    }//aqui termina por batch

                    //aqui llenara la lista

                    var produccionFormula = new ProduccionFormulaInfo();
                    produccionFormula.Organizacion = new OrganizacionInfo
                                                         {
                                                             OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID
                                                         };

                    var formulaPL = new FormulaPL();

                    var todasFormulas = formulaPL.ObtenerTodos(EstatusEnum.Activo);

                    FormulaInfo formula =
                        todasFormulas.FirstOrDefault(
                            fo =>
                            fo.Descripcion.ToUpper().Trim().Equals(renglon.Formula.ToUpper().Trim(),
                                                                   StringComparison.InvariantCultureIgnoreCase));

                    produccionFormula.Formula = formula;
                    produccionFormula.CantidadReparto = sumatoriameta; //cantidad programada
                    produccionFormula.CantidadProducida = sumatoriareal;//cantidad real
                    produccionFormula.FechaProduccion = DateTime.Parse(txtFecha.Text);
                    produccionFormula.Activo = EstatusEnum.Activo;
                    produccionFormula.ProduccionFormulaDetalle = listaFormulaDetalle;
                    //produccionFormula.ProduccionFormulaBatch = ListaGlobalDetalleBatch.Where(batch => batch.Formula.ToUpper().Trim().Equals(produccionFormula.Formula.Descripcion.ToUpper().Trim())).ToList();
                    produccionFormula.ProduccionFormulaBatch = listaFormulaProduccionBatch;
                    produccionFormula.UsuarioCreacionId = seguridad.Usuario.UsuarioID;

                    ListaGlobal.Add(produccionFormula);
                }
            }
            catch (Exception er)
            {
                Logger.Error(er);
                return null;
            }

            return resultado;
        }

        public List<ProduccionFormulaDetalleInfo> CrearListaProduccionFormulaDetalle(List<ProcesarArchivoInfo> lista, string formula)
        {
            var seguridad = (SeguridadInfo)ObtenerSeguridad();
            var listaGlobalFormulaDetalle = new List<ProduccionFormulaDetalleInfo>();

            var ingredientePL = new IngredientePL();

            try
            {
                //sacamos los Productos
                var Prod = from w in lista
                           where w.Formula == formula && w.Marca != "2"
                           group w by w.Codigo into g
                           select new
                                      {
                                          FirstLetter = g.Key, 
                                          Words = g
                                      };

                var formulaPL = new FormulaPL();

                var formulasTodas = formulaPL.ObtenerTodos(EstatusEnum.Activo);

                foreach (var produccion in Prod)
                {
                    var produccionFormulaDetalle = new ProduccionFormulaDetalleInfo();
                    List<ProcesarArchivoInfo> listafltrada = lista.Where(k => k.Formula == formula && k.Codigo == produccion.FirstLetter).ToList();

                    decimal cantidad = (from prod in listafltrada select prod.Real).Sum();

                    produccionFormulaDetalle.Producto = new ProductoInfo
                                                            {
                                                                ProductoId = int.Parse(produccion.FirstLetter)
                                                            };
                    produccionFormulaDetalle.CantidadProducto = cantidad;
                    produccionFormulaDetalle.Activo = EstatusEnum.Activo;

                    var formulaExiste =
                        formulasTodas.FirstOrDefault(
                            fo =>
                            fo.Descripcion.ToUpper().Trim().Equals(formula.ToUpper().Trim(),
                                                                   StringComparison.InvariantCultureIgnoreCase));

                    if (formulaExiste == null)
                    {
                        formulaExiste = new FormulaInfo();
                    }

                    IngredienteInfo ingredienteInfo = ingredientePL.ObtenerPorIdOrganizacionFormulaProducto(formulaExiste.FormulaId, int.Parse(produccion.FirstLetter), seguridad.Usuario.Organizacion.OrganizacionID);

                    if (ingredienteInfo == null)
                    {
                        return null;
                    }
                    produccionFormulaDetalle.Ingrediente = ingredienteInfo;
                    listaGlobalFormulaDetalle.Add(produccionFormulaDetalle);
                }
            }
            catch (Exception er)
            {
                Logger.Error(er);
                return null;
            }
            return listaGlobalFormulaDetalle;
        }

        public List<ProduccionFormulaBatchInfo> CrearListaProduccionFormulaBatch(List<ProcesarArchivoInfo> lista, string formula, int Batch, string nombrerotomix)
        {
            var listaFormulaProduccionBatch = new List<ProduccionFormulaBatchInfo>();
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            var formulaPL = new FormulaPL();

            var formulasTodas = formulaPL.ObtenerTodos(EstatusEnum.Activo);

            try
            {
                //sacamos los Productos
                var produccionAgrupada = from w in lista
                           where w.Formula == formula && w.Marca != "2" && w.batch == Batch
                           group w by w.Codigo into g
                           select new { FirstLetter = g.Key, Words = g };

                foreach (var z in produccionAgrupada)
                {
                    var rotomixPl = new RotomixPL();
                    RotoMixInfo rotoMix = rotomixPl.ObtenerRotoMixXOrganizacionYDescripcion(seguridad.Usuario.Organizacion.OrganizacionID, nombrerotomix);

                    var produccionformulabatch = new ProduccionFormulaBatchInfo();
                    List<ProcesarArchivoInfo> listafltrada = lista.Where(k => k.Formula == formula && k.Codigo == z.FirstLetter && k.batch == Batch).ToList();

                    int cantidadReal = (from prod in listafltrada select prod.Real).Sum();
                    int cantidadProgramada = (from prod in listafltrada select prod.Meta).Sum();

                    var formulaExiste =
                        formulasTodas.FirstOrDefault(
                            fo =>
                            fo.Descripcion.ToUpper().Trim().Equals(formula.ToUpper().Trim(),
                                                                   StringComparison.InvariantCultureIgnoreCase));

                    if (formulaExiste == null)
                    {
                        formulaExiste = new FormulaInfo();
                    }

                    produccionformulabatch.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                    produccionformulabatch.Batch = Batch;
                    produccionformulabatch.ProductoID = int.Parse(z.FirstLetter);
                    produccionformulabatch.FormulaID = formulaExiste.FormulaId;
                    produccionformulabatch.CantidadProgramada = cantidadProgramada;
                    produccionformulabatch.CantidadReal = cantidadReal;
                    produccionformulabatch.Activo = EstatusEnum.Activo;
                    produccionformulabatch.RotomixID = rotoMix.RotoMixId;

                    listaFormulaProduccionBatch.Add(produccionformulabatch);
                }
            }
            catch (Exception er)
            {
                Logger.Error(er);
                return null;
            }
            return listaFormulaProduccionBatch;
        }

        public string ValidarColumnaCodigoLista(ProcesarArchivoInfo renglonArchivo)
        {
            string error;
            error = GetLocalResourceObject("OK").ToString();

            if (renglonArchivo.Codigo != GetLocalResourceObject("msjSeis").ToString())
            {
                if (renglonArchivo.Codigo != GetLocalResourceObject("msjNueve").ToString())
                {
                    error = GetLocalResourceObject("ColumnaCodigo").ToString();
                }
            }
            if (renglonArchivo.Codigo == GetLocalResourceObject("msjSeis").ToString())
            {
                error = ValidarContenidosLista(renglonArchivo);
            }

            return error;
        }

        public string ValidarCodigoProducto(ProcesarArchivoInfo renglonArchivo)
        {
            var resultado = GetLocalResourceObject("OK").ToString();
            var productos = new ProductoPL(); 
            var prod = new ProductoInfo();

            prod.ProductoId = int.Parse(renglonArchivo.Codigo);
            prod = productos.ObtenerPorID(prod);

            if (prod == null)
            {
                resultado = GetLocalResourceObject("producto").ToString();
            }
            else
            {
                renglonArchivo.ProductoID = prod.ProductoId;
            }

            return resultado;
        }

        public string ValidarCodigoFormula(ProcesarArchivoInfo renglonArchivo)
        {
            string resultado; 
            resultado = GetLocalResourceObject("OK").ToString();
            var formulas = new FormulaPL(); 
            if (string.IsNullOrWhiteSpace(renglonArchivo.Formula))
            {
                return GetLocalResourceObject("formulas").ToString();
            }
            var formulasTodos = formulas.ObtenerTodos(EstatusEnum.Activo);

            var formulaExiste =
                formulasTodos.FirstOrDefault(fo => fo.Descripcion.ToUpper().Trim().Equals(renglonArchivo.Formula.ToUpper().Trim(), StringComparison.InvariantCultureIgnoreCase));

            if (formulaExiste == null)
            {
                return GetLocalResourceObject("formulas").ToString();
            }
            else
            {
                renglonArchivo.FormulaID = formulaExiste.FormulaId;
            }
            return resultado;
        }

        public string ValidarProduccionFormula(ProcesarArchivoInfo renglonArchivo)
        {
            var seguridad = (SeguridadInfo) ObtenerSeguridad();
            renglonArchivo.OrganizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
            string resultado;
            resultado = GetLocalResourceObject("OK").ToString();
            var produccionFormulaBatchBL = new ProduccionFormulaBatchBL();

            ProduccionFormulaBatchInfo produccion = produccionFormulaBatchBL.ValidarProduccionFormulaBatch(renglonArchivo);

            if (produccion != null)
            {
                return string.Format(GetLocalResourceObject("CodeBehind.ProduccionGenerada").ToString(),renglonArchivo.batch,renglonArchivo.ProductoID,renglonArchivo.Fecha,renglonArchivo.Rotomix);
            }
            return resultado;
        }

        public string ValidarCodigoRotomix(ProcesarArchivoInfo renglonArchivo)
        {
            var resultado = GetLocalResourceObject("OK").ToString();
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            if(seguridad == null)
            {
                seguridad = new SeguridadInfo();
            }
            var rotomixPl = new RotomixPL();
            RotoMixInfo rotoMix = rotomixPl.ObtenerRotoMixXOrganizacionYDescripcion(seguridad.Usuario.Organizacion.OrganizacionID, renglonArchivo.Rotomix);
            if (rotoMix == null)
            {
                resultado = GetLocalResourceObject("rotomix").ToString();
            }
            else
            {
                renglonArchivo.RotoMixID = rotoMix.RotoMixId;
            }
            return resultado;
        }

        public string ValidarContenidosLista(ProcesarArchivoInfo r)
        {
            string resultado;
            resultado = GetLocalResourceObject("OK").ToString();

            if (!dic.ContainsKey(r.batch.ToString() + r.Rotomix + r.Fecha))
            {
                dic.Add(r.batch.ToString() + r.Rotomix + r.Fecha, r.Rotomix.ToString());
            }
            else
            {
                //if (!dic.ContainsKey(r.Rotomix.ToString()))
                //{ dic.Add(r.batch.ToString(), r.Rotomix.ToString()); }
                //else
                //{ 
                resultado = GetLocalResourceObject("batch").ToString();
                //}
            }

            return resultado;
        }
        // Validar la Fecha de la fecha de produccion indicada en el archivo
        public string ValidarFechaYHora(ProcesarArchivoInfo renglonArchivo)
        {
            string resultado = GetLocalResourceObject("OK").ToString();
            string[] words = renglonArchivo.Fecha.Split('/');
            string[] words2 = renglonArchivo.Hora.Split(':');

            string cadena = string.Format("{0}", fecha.Year);
            cadena = cadena.Substring(0, 2);
            string year = string.Format("{0}{1}", cadena, words[2]);

            DateTime dateValue = new DateTime(int.Parse(year), int.Parse(words[1]), int.Parse(words[0]), int.Parse(words2[0]), int.Parse(words2[1]), int.Parse(words2[0]));

            //DateTime unomenos = new DateTime(fecha.Year, fecha.Month, fecha.Day == 1 ? 1 : fecha.Day - 1, 0, 0, 0);
            DateTime unomenos = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
            DateTime unomas = new DateTime(fecha.Year, fecha.Month, fecha.Day, 23, 59, 59);

            if (dateValue < unomenos)
            {
                resultado = GetLocalResourceObject("fecha").ToString();
            }
            else
            {
                if (dateValue > unomas)
                {
                    resultado = GetLocalResourceObject("fecha").ToString();
                }
            }
            renglonArchivo.FechaProduccion = dateValue;
            return resultado;
        }

        public void CrearScriptsError(string error)
        {
            ClientScript.RegisterStartupScript(GetType(), "myScript", "ValidacionErrores('" + error + "');", true);
        }

        #endregion

        #region WebMethod

        [WebMethod]
        public static string MostrarErrores(string error)
        {
            return error;
        }

        [WebMethod]
        public static List<ProduccionFormulaInfo> RegresarListas()
        {
            //var regresarlista = HttpContext.Current.Session["lista"] as List<ProduccionFormulaInfo>;
            //return regresarlista;
            return ListaGlobal;
        }

        [WebMethod]
        public static List<DetalleBatchInfo> RegresarListasDetalleBatchs(string formula)
        {
            //List<DetalleBatchInfo> regresarlistaDetalleBatch = new List<DetalleBatchInfo>();
            //regresarlistaDetalleBatch = HttpContext.Current.Session["listaGlobalDetalleBatch"] as List<DetalleBatchInfo>;
            //return regresarlistaDetalleBatch.Where(k => k.Formula.ToUpper().Trim().Equals(Formula.ToUpper().Trim())).ToList();
            return ListaGlobalDetalleBatch.Where(k => k.Formula.ToUpper().Trim().Equals(formula.ToUpper().Trim())).ToList();
        }

        [WebMethod]
        public static ProduccionFormulaAutomaticaModel Guardar()
        {
            var retorno = new ProduccionFormulaAutomaticaModel();
            retorno.CodigoMensajeRetorno = 0;
            retorno.Mensaje = "OK";
            //List<ProduccionFormulaInfo> listaaGuardar = new List<ProduccionFormulaInfo>();
            //listaaGuardar = HttpContext.Current.Session["lista"] as List<ProduccionFormulaInfo>;
            if (ListaGlobal != null)
            {
                var produccionFormulaPl = new ProduccionFormulaPL();

                retorno = produccionFormulaPl.GuardarProduccionFormulaLista(ListaGlobal,fecha);
            }

            return retorno;
        }

        [WebMethod]
        public static List<ProduccionFormulaInfo> ResumenDeProduccion()
        {
            //List<ProduccionFormulaInfo> ProduccionFormulaLista = new List<ProduccionFormulaInfo>();
            //ProduccionFormulaLista = HttpContext.Current.Session["lista"] as List<ProduccionFormulaInfo>;
            if (ListaGlobal != null)
            {
                var almacenPl = new AlmacenPL();
                var almacenes = almacenPl.ObtenerAlmacenPorTiposAlmacen(new List<TipoAlmacenEnum>() { 
                   TipoAlmacenEnum.PlantaDeAlimentos
                }, new OrganizacionInfo
                {
                    OrganizacionID = ListaGlobal[0].Organizacion.OrganizacionID
                });
                ListaGlobal[0].Almacen = almacenes.FirstOrDefault();
                var produccionFormulaPl = new ProduccionFormulaPL();
                ListaGlobal = produccionFormulaPl.ResumenProduccionFormulaLista(ListaGlobal);
            }
            return ListaGlobal;
        }

        #endregion

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            fecha = Convert.ToDateTime(txtFecha.Text);
        }

    }
}