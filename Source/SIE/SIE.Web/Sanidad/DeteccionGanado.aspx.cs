using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Sanidad
{
    public partial class DeteccionGanado : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int rolIdUsuario = 0;
            txtHora.Text = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

            if (seguridad != null)
            {
                if (seguridad.Usuario.Operador != null)
                {
                    rolIdUsuario = seguridad.Usuario.Operador.Rol.RolID;
                    txtNombreVaquero.Text = seguridad.Usuario.Nombre;
                    LlenarcomboGolpeado();
                    LlenarcomboDescripcionGanado();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myScript", "EnviarMensajeUsuario();", true);
            }
        }

        /// <summary>
        /// Metodo que llena el combo golpeado
        /// </summary>
        private void LlenarcomboDescripcionGanado()
        {
            try
            {
                var descripcionGanadoBL = new DescripcionGanadoBL();
                IList<DescripcionGanadoInfo> descripcionesGanado = descripcionGanadoBL.ObtenerTodos(EstatusEnum.Activo);
                if (descripcionesGanado != null && descripcionesGanado.Any())
                {
                    var localResourceObject = GetLocalResourceObject("Seleccione");
                    if (localResourceObject != null)
                    {
                        var itemSeleccione = new DescripcionGanadoInfo
                            {
                                DescripcionGanadoID = 0,
                                Descripcion = localResourceObject.ToString()
                            };
                        descripcionesGanado.Insert(0, itemSeleccione);
                    }
                    ddlDescripcionGanado.DataSource = descripcionesGanado;
                    ddlDescripcionGanado.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        /// <summary>
        /// Metodo que llena el combo golpeado
        /// </summary>
        private void LlenarcomboGolpeado()
        {
            try
            {
                var sintoma = new SintomaPL();
                IList<SintomaInfo> sintomas = sintoma.ObtenerPorProblema((int)ProblemasEnum.Golpeado);
                if (sintomas != null)
                {
                    var listasintomas = from item in sintomas
                                        select new
                                        {
                                            item.Descripcion,
                                            item.SintomaID
                                        };
                    cmbParteGolpeada.DataSource = listasintomas;
                    cmbParteGolpeada.DataTextField = "Descripcion";
                    cmbParteGolpeada.DataValueField = "SintomaID";
                    cmbParteGolpeada.DataBind();

                    ListItem itemCombo = new ListItem();
                    itemCombo.Text = GetLocalResourceObject("cmbItemSeleccione").ToString();
                    itemCombo.Value = Numeros.ValorCero.GetHashCode().ToString();
                    cmbParteGolpeada.Items.Insert(0, itemCombo);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Metodo para Consultar los sintomas de un problema
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<SintomaInfo> ObtenerListaSintomas(int problema)
        {
            IList<SintomaInfo> sintomas = null;
            try
            {
                var sintoma = new SintomaPL();
                sintomas = sintoma.ObtenerPorProblema(problema);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return sintomas;
        }

        /// <summary>
        /// Metodo para Consultar los Corrales disponibles para Cerrar y/o Reimprimir
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<ProblemaInfo> ObtenerProblemasLista()
        {
            IList<ProblemaInfo> retValue = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    var problemaPL = new ProblemaPL();
                    retValue = problemaPL.ObtenerListaProblemas();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return retValue;
        }

        /// <summary>
        /// Metodo para Consultar los Corrales disponibles para Cerrar y/o Reimprimir
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<GradoInfo> ObtenerGrados()
        {
            IList<GradoInfo> listaGrado = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                if (seguridad != null)
                {
                    var gradoPL = new GradoPL();
                    listaGrado = gradoPL.ObtenerGrados();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return listaGrado;
        }

        /// <summary>
        /// Metodo para Consultar la informacion de un corral
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static CorralInfo ObtenerCorral(string corralCodigo)
        {
            CorralInfo corral = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                if (seguridad != null)
                {
                    var corralPL = new CorralPL();
                    corral = corralPL.ObtenerCorralPorCodigo(organizacionId, corralCodigo);
                    if (corral.GrupoCorral == (int)GrupoCorralEnum.Corraleta)
                    {
                        if (corral.TipoCorral.TipoCorralID != (int)TipoCorral.CorraletaSacrificio
                            && corral.TipoCorral.TipoCorralID != (int)TipoCorral.CorraletaRecuperado
                            && corral.TipoCorral.TipoCorralID != (int)TipoCorral.CorraletaRecuperadosPartida)
                        {
                            corral.TipoCorral.TipoCorralID = -1; //Corraleta no es igual a sacrificio
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return corral;
        }

        /// <summary>
        /// Metodo para Consultar el lote activo del corral
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static LoteInfo ObtenerLotesCorral(int corralID)
        {
            LoteInfo lote = null;
            CorralInfo corral = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                if (seguridad != null)
                {
                    var corralPL = new CorralPL();
                    corral = corralPL.ObtenerPorId(corralID);
                    if (corral.TipoCorral.TipoCorralID != (int)TipoCorral.CorraletaSacrificio)
                    {
                        var lotePL = new LotePL();
                        lote = lotePL.DeteccionObtenerPorCorral(organizacionId, corralID);
                    }
                    else
                    {
                        lote = new LoteInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return lote;
        }

        /// <summary>
        /// Metodo para Consultar si el corral pertenece al operador
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerPerteneceCorralOperador(int corralID)
        {
            int retValue = 0;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                int operadorId = seguridad.Usuario.Operador.OperadorID;

                if (seguridad != null)
                {
                    var operadorPL = new OperadorPL();
                    OperadorInfo operadorCorral = operadorPL.ObtenerDetectorCorral(organizacionId, corralID, operadorId);
                    if (operadorCorral != null)
                    {
                        if (operadorCorral.OperadorID == operadorId)
                        {
                            retValue = 1;
                        }
                        else
                        {
                            retValue = 0;
                        }
                    }
                    else
                    {
                        retValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return retValue;
        }

        /// <summary>
        /// Metodo para Consultar si el corral pertenece al operador
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static EntradaGanadoInfo ObtenerPartida(int corralID)
        {
            EntradaGanadoInfo retValue = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                var corralPl = new CorralPL();
                CorralInfo corral = corralPl.ObtenerPorId(corralID);
                if (corral != null && seguridad != null)
                {
                    if (corral.GrupoCorral != GrupoCorralEnum.Recepcion.GetHashCode())
                    {
                        retValue = new EntradaGanadoInfo();
                    }
                    else
                    {
                        retValue = corralPl.ObtenerPartidaCorral(organizacionId, corralID);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return retValue;
        }

        /// <summary>
        /// Metodo para Consultar si el corral pertenece al operador
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<NotificacionInfo> ObtenerNotificacionesOperador()
        {
            IList<NotificacionInfo> retValue = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                if (seguridad.Usuario.Operador != null)
                {
                    int operadorId = seguridad.Usuario.Operador.OperadorID;
                    var operadorPL = new OperadorPL();
                    retValue = operadorPL.ObtenerNotificacionesDeteccionLista(organizacionId, operadorId);
                    if (retValue != null)
                    {
                        string ruta = ObtenerParametrosRuta(organizacionId).Valor;
                        for (int i = 0; i < retValue.Count; i++)
                        {
                            if (retValue[i].fotoSupervision != "")
                            {
                                retValue[i].fotoSupervision = ruta + retValue[i].fotoSupervision;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return retValue;
        }

        private static ConfiguracionParametrosInfo ObtenerParametrosRuta(int organizacionid)
        {
            ConfiguracionParametrosInfo configuracion = null;
            try
            {
                var configuracionPL = new ConfiguracionParametrosPL();
                configuracion = configuracionPL.ObtenerPorOrganizacionTipoParametroClave(new ConfiguracionParametrosInfo()
                {
                    Clave = ParametrosEnum.ubicacionFotos.ToString(),
                    TipoParametro = (int)TiposParametrosEnum.Imagenes,
                    OrganizacionID = organizacionid
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return configuracion;
        }

        /// <summary>
        /// Metodo para obtener los aretes disponibles
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<AnimalInfo> ObtenerAnimalesPorCodigoCorral(string corralCodigo)
        {
            List<AnimalInfo> lista = null;
            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                var corralPl = new CorralPL();
                CorralInfo corral = corralPl.ObtenerCorralPorCodigo(organizacionId, corralCodigo);
                corral.Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId };
                if (corral.GrupoCorral == (int)GrupoCorralEnum.Recepcion)
                {
                    lista = new List<AnimalInfo>();
                    var interfaz = new InterfaceSalidaAnimalPL();

                    var anim = interfaz.ObtenerAretesInterfazSalidaAnimal(corralCodigo, organizacionId);
                    if (anim != null)
                    {
                        foreach (InterfaceSalidaAnimalInfo interfaceAnimal in anim)
                        {
                            var animal = new AnimalInfo { Arete = interfaceAnimal.Arete };
                            lista.Add(animal);
                        }
                    }
                }
                else
                {
                    var animalPl = new AnimalPL();
                    lista = animalPl.ObtenerAnimalesPorCorral(corral, organizacionId);
                }
                return lista;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

            }
            return lista;
        }

        /// <summary>
        /// Metodo para obtener si existe el arete, o es de un corral de recepcion
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerExisteArete(string corralCodigo, string arete, bool validarDeteccion)
        {
            int retorno = 0;
            string areteDeteccion = string.Empty, areteMuerte = string.Empty;
            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                IList<AnimalInfo> animales = null;
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                var animalPl = new AnimalPL();
                var corralPl = new CorralPL();
                var lotePL = new LotePL();

                AnimalInfo animal = null;
                CorralInfo corral = null;
                LoteInfo lote = null;

                corral = corralPl.ObtenerCorralPorCodigo(organizacionId, corralCodigo);
                corral.Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId };

                if (validarDeteccion)
                {
                    areteDeteccion = animalPl.obtenerExisteDeteccion(arete);
                }

                if (areteDeteccion == string.Empty)
                {
                    areteMuerte = animalPl.obtenerExisteMuerte(arete);
                    if (areteMuerte == string.Empty)
                    {
                        if (corral.TipoCorral.TipoCorralID != (int)TipoCorral.Recepcion &&
                            corral.TipoCorral.TipoCorralID != (int)TipoCorral.CorraletaSacrificio)
                        {
                            animales = animalPl.ObtenerAnimalesPorCorral(corral, organizacionId);
                            if (animales != null)
                            {
                                for (int i = 0; i < animales.Count; i++)
                                {
                                    if (arete == animales[i].Arete)
                                    {
                                        return 1;
                                    }
                                }
                            }
                            /* Validar Si el arete existe en el inventario  y es carga inicial */
                            var animalInventario = animalPl.ObtenerAnimalPorArete(arete, organizacionId);
                            if (animalInventario == null)
                            {
                                retorno = 0;
                            }
                        }
                        else if (corral.TipoCorral.TipoCorralID == (int)TipoCorral.CorraletaSacrificio)
                        {
                            animal = animalPl.ObtenerAnimalPorArete(arete, organizacionId);
                            if (animal != null)
                            {
                                if (animalPl.ObtenerExisteSalida(animal.AnimalID) > 0)
                                {
                                    retorno = 1;
                                }
                            }
                        }
                        else
                        {
                            var entrada = new EntradaGanadoPL();
                            lote = lotePL.ObtenerLotesActivos(organizacionId, corral.CorralID);

                            if (lote != null)
                            {
                                var datosEntrada = entrada.ObtenerEntradaPorLote(lote);

                                if (datosEntrada != null)
                                {
                                    if (datosEntrada.TipoOrganizacionOrigenId != (int)TipoOrganizacion.CompraDirecta)
                                    {
                                        int salida = corralPl.ObtenerExisteInterfaceSalida(organizacionId, corralCodigo, arete);
                                        if (salida > 0)
                                        {
                                            retorno = 1; // Existe arete en la interface
                                        }
                                        else
                                        {
                                            retorno = 0;//Valido, pero aqui va la validacion de interfaz salida
                                        }
                                    }
                                    else
                                    {
                                        /* Si es compra directa validar q el arete no exista en el inventario */
                                        var animalInventario = animalPl.ObtenerAnimalPorArete(arete, organizacionId);
                                        if (animalInventario != null)
                                        {
                                            retorno = 0;// el arete ya existe en el inventario
                                        }
                                        else
                                        {
                                            retorno = 1;//Valido, es compra directa y no existe en el inventario
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        retorno = 3;//Muerto
                    }
                }
                else
                {
                    retorno = 2;//Detectado
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return retorno;
        }

        /// <summary>
        /// Metodo para obtener si existe el arete, o es de un corral de recepcion
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int ObtenerExisteAreteTestigo(string corralCodigo, string arete, string areteTestigo, bool validarDeteccion)
        {
            int retorno = 0;
            string areteDeteccion = string.Empty, areteMuerte = string.Empty;
            try
            {
                SeguridadInfo seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                IList<AnimalInfo> animales = null;
                int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;

                var animalPl = new AnimalPL();
                var corralPl = new CorralPL();

                AnimalInfo animal = new AnimalInfo();
                CorralInfo corral = null;

                corral = corralPl.ObtenerCorralPorCodigo(organizacionId, corralCodigo);
                corral.Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId };

                if (validarDeteccion)
                    areteDeteccion = animalPl.obtenerExisteDeteccionTestigo(areteTestigo);

                if (areteDeteccion == string.Empty)
                {
                    areteMuerte = animalPl.obtenerExisteMuerteTestigo(areteTestigo);
                    if (areteMuerte == string.Empty)
                    {
                        if (corral.TipoCorral.TipoCorralID != (int)TipoCorral.Recepcion && corral.TipoCorral.TipoCorralID != (int)TipoCorral.CorraletaSacrificio)
                        {
                            animales = animalPl.ObtenerAnimalesPorCorral(corral, organizacionId);
                            if (animales != null)
                            {
                                for (int i = 0; i < animales.Count; i++)
                                {
                                    if (areteTestigo == animales[i].AreteMetalico)
                                    {
                                        if (arete != string.Empty)
                                        {
                                            if (arete == animales[i].Arete)
                                            {
                                                retorno = 1; //Si corresponde
                                                break;
                                            }
                                            else
                                            {
                                                retorno = 2; //No corresponde
                                            }
                                        }
                                        else
                                        {
                                            retorno = 1; //No trae arete
                                        }
                                    }
                                }
                            }
                        }
                        else if (corral.TipoCorral.TipoCorralID == (int)TipoCorral.CorraletaSacrificio)
                        {
                            animal = animalPl.ObtenerAnimalPorAreteTestigo(areteTestigo, organizacionId);
                            if (animal != null)
                            {
                                if (animalPl.ObtenerExisteSalida(animal.AnimalID) > 0 && (arete == animal.Arete || arete == ""))
                                {
                                    retorno = 1;
                                }
                            }
                        }
                        else
                        {
                            retorno = 1;//No se valida si es recepcion
                        }
                    }
                    else
                    {
                        retorno = 3;//Muerto
                    }
                }
                else
                {
                    retorno = 2;//Detectado
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return retorno;
        }

        /// <summary>
        /// Metodo para guardar
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int GuardarMuerte(MuerteInfo muerte)
        {
            int retValue = 0;
            try
            {
                CorralInfo corral = new CorralInfo();
                CorralPL corralPL = new CorralPL();
                AnimalPL animalPL = new AnimalPL();
                LotePL lotePL = new LotePL();
                corral = ObtenerCorral(muerte.CorralCodigo);
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                LoteInfo lote = ObtenerLotesCorral(corral.CorralID);
                int organizacionID = 0;
                AnimalInfo animal = null;
                MuerteInfo muerteGrabar = null;
                // 1 se encuentra en corral 2 Carga Inicial
                var esCargaInicial = FlagCargaInicial.Default;
                //informacion del la organzacion y usuario
                if (seguridad != null)
                {
                    organizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                    var deteccionPl = new DeteccionPL();

                    if (corral.GrupoCorral != (int)GrupoCorralEnum.Recepcion)
                    {
                        if (muerte.Arete != string.Empty && muerte.AreteMetalico == string.Empty)
                        {
                            var animalPl = new AnimalPL();
                            var animales = animalPl.ObtenerAnimalesPorCorral(corral, organizacionID);
                            if (animales != null)
                            {
                                for (int i = 0; i < animales.Count; i++)
                                {
                                    if (muerte.Arete == animales[i].Arete)
                                    {
                                        esCargaInicial = FlagCargaInicial.SeEncuentraEnCorral;
                                        break;
                                    }
                                }
                            }
                            /* Validar Si el arete existe en el inventario */
                            animal = animalPl.ObtenerAnimalPorArete(muerte.Arete, organizacionID);
                            if (animal != null && animal.CargaInicial && esCargaInicial == FlagCargaInicial.Default)
                            {
                                esCargaInicial = FlagCargaInicial.EsCargaInicial;
                            }
                            else if (animal == null)
                            {
                                animal = new AnimalInfo()
                                {
                                    Arete = muerte.Arete,
                                    AreteMetalico = muerte.AreteMetalico,
                                    LoteID = lote.LoteID,
                                    CorralID = corral.CorralID,
                                    UsuarioModificacionID = seguridad.Usuario.UsuarioID,
                                };
                                // Este arete se dara de alta en el corral
                                esCargaInicial = FlagCargaInicial.EsAreteNuevo;
                            }
                        }
                        if (muerte.AreteMetalico != string.Empty && muerte.Arete == string.Empty)
                        {
                            animal = animalPL.ObtenerAnimalPorAreteTestigo(muerte.AreteMetalico, organizacionID);
                        }
                    }
                    var guardarPl = new MuertePL();
                    string foto = string.Empty;
                    if (muerte.FotoDeteccion != string.Empty)
                    {
                        foto = TipoFoto.Muerte.ToString() + '/' + muerte.FotoDeteccion;
                    }

                    if (animal != null)
                    {
                        muerteGrabar = new MuerteInfo
                        {
                            LoteId = lote.LoteID,
                            CorralId = corral.CorralID,
                            OrganizacionId = seguridad.Usuario.Organizacion.OrganizacionID,
                            FotoDeteccion = foto,
                            Observaciones = muerte.Observaciones,
                            Arete = animal.Arete,
                            AreteMetalico = animal.AreteMetalico,
                            OperadorDeteccionId = seguridad.Usuario.Operador.OperadorID,
                            UsuarioCreacionID = seguridad.Usuario.UsuarioID,
                            EstatusId = (int)EstatusMuertes.Detectado
                        };
                    }
                    else
                    {
                        muerteGrabar = new MuerteInfo
                        {
                            LoteId = lote.LoteID,
                            CorralId = corral.CorralID,
                            OrganizacionId = seguridad.Usuario.Organizacion.OrganizacionID,
                            FotoDeteccion = foto,
                            Observaciones = muerte.Observaciones,
                            Arete = muerte.Arete,
                            AreteMetalico = muerte.AreteMetalico,
                            OperadorDeteccionId = seguridad.Usuario.Operador.OperadorID,
                            UsuarioCreacionID = seguridad.Usuario.UsuarioID,
                            EstatusId = (int)EstatusMuertes.Detectado
                        };
                    }

                    if (corral.TipoCorral.TipoCorralID == (int)TipoCorral.CorraletaSacrificio)
                    {
                        muerteGrabar.LoteId = animalPL.ObtenerLoteSalidaAnimal(muerteGrabar.Arete, muerteGrabar.AreteMetalico, organizacionID);
                    }

                    if (muerteGrabar != null)
                    {
                        muerteGrabar.Corral = corral;
                        var resultado = guardarPl.GuardarMuerte(muerteGrabar,
                                                                esCargaInicial,
                                                                animal);
                        retValue = resultado;
                    }
                }
                else
                {
                    retValue = 0;
                }
            }
            catch (Exception ex)
            {
                retValue = -1;
            }
            return retValue;
        }

        /// <summary>
        /// Metodo para guardar
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int GuardarDeteccion(DeteccionInfo deteccion)
        {
            int retValue = 0;
            try
            {
                CorralInfo corral = new CorralInfo();
                CorralPL corralPL = new CorralPL();
                corral = ObtenerCorral(deteccion.CorralCodigo);
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
                LoteInfo lote = ObtenerLotesCorral(corral.CorralID);
                AnimalPL animalPL = new AnimalPL();
                AnimalInfo animal = null;
                var esCargaInicial = FlagCargaInicial.Default;
                string areteSalida = string.Empty;
                int salida = 0;
                int organizacionID = 0;
                DeteccionInfo deteccionGrabar = null;
                //informacion del la organzacion y usuario
                if (seguridad != null)
                {
                    organizacionID = seguridad.Usuario.Organizacion.OrganizacionID;
                    var deteccionPl = new DeteccionPL();

                    string areteDeteccion = animalPL.obtenerExisteDeteccion(deteccion.Arete);
                    if (!string.IsNullOrWhiteSpace(areteDeteccion))
                    {
                        return 3;
                    }
                    string areteDeteccionMetalico = animalPL.obtenerExisteDeteccionTestigo(deteccion.AreteMetalico);
                    if (!string.IsNullOrWhiteSpace(areteDeteccionMetalico))
                    {
                        return 3;
                    }

                    if (corral.GrupoCorral != (int)GrupoCorralEnum.Recepcion)
                    {
                        if (deteccion.Arete != string.Empty && deteccion.AreteMetalico == string.Empty)
                        {
                            var animalPl = new AnimalPL();
                            var animales = animalPl.ObtenerAnimalesPorCorral(corral, organizacionID);
                            if (animales != null)
                            {
                                for (int i = 0; i < animales.Count; i++)
                                {
                                    if (deteccion.Arete == animales[i].Arete)
                                    {
                                        esCargaInicial = FlagCargaInicial.SeEncuentraEnCorral;
                                        break;
                                    }
                                }
                            }
                            /* Validar Si el arete existe en el inventario */
                            animal = animalPl.ObtenerAnimalPorArete(deteccion.Arete, organizacionID);
                            if (animal != null && animal.CargaInicial && esCargaInicial == FlagCargaInicial.Default)
                            {
                                esCargaInicial = FlagCargaInicial.EsCargaInicial;
                            }
                            else if (animal == null)
                            {
                                animal = new AnimalInfo()
                                {
                                    Arete = deteccion.Arete,
                                    AreteMetalico = deteccion.AreteMetalico,
                                    LoteID = lote.LoteID,
                                    CorralID = corral.CorralID,
                                    UsuarioModificacionID = seguridad.Usuario.UsuarioID,
                                };
                                // Este arete se dara de alta en el corral
                                esCargaInicial = FlagCargaInicial.EsAreteNuevo;
                            }

                        }

                        if (deteccion.AreteMetalico != string.Empty && deteccion.Arete == string.Empty)
                        {
                            animal = animalPL.ObtenerAnimalPorAreteTestigo(deteccion.AreteMetalico, organizacionID);
                        }
                    }
                    else
                    {
                        if (deteccion.AreteMetalico == string.Empty && deteccion.Arete != string.Empty)
                        {
                            var interfaceSalidaAnimalPl = new InterfaceSalidaAnimalPL();
                            InterfaceSalidaAnimalInfo interfaceSalidaAnimalInfo = interfaceSalidaAnimalPl.ObtenerNumeroAreteIndividual(deteccion.Arete, organizacionID);

                            if (interfaceSalidaAnimalInfo != null)
                            {
                                deteccion.AreteMetalico = interfaceSalidaAnimalInfo.AreteMetalico;
                            }
                        }
                        else if (deteccion.AreteMetalico != string.Empty && deteccion.Arete == string.Empty)
                        {
                            var interfaceSalidaAnimalPl = new InterfaceSalidaAnimalPL();
                            InterfaceSalidaAnimalInfo interfaceSalidaAnimalInfo = interfaceSalidaAnimalPl.ObtenerNumeroAreteMetalico(deteccion.Arete, organizacionID);

                            if (interfaceSalidaAnimalInfo != null)
                            {
                                deteccion.Arete = interfaceSalidaAnimalInfo.Arete;
                            }

                        }
                    }

                    string foto = string.Empty;
                    if (deteccion.FotoDeteccion != string.Empty)
                    {
                        foto = TipoFoto.Enfermo.ToString() + '/' + deteccion.FotoDeteccion;
                    }

                    if (animal != null)
                    {
                        deteccionGrabar = new DeteccionInfo()
                        {
                            LoteID = lote.LoteID,
                            CorralID = corral.CorralID,
                            FotoDeteccion = foto,
                            Observaciones = deteccion.Observaciones,
                            Arete = animal.Arete,
                            AreteMetalico = animal.AreteMetalico,
                            OperadorID = seguridad.Usuario.Operador.OperadorID,
                            UsuarioCreacionID = seguridad.Usuario.UsuarioID,
                            DescripcionGanado = deteccion.DescripcionGanado,
                            Problemas = deteccion.Problemas,
                            Sintomas = deteccion.Sintomas,
                            GradoID = deteccion.GradoID,
                            NoFierro = deteccion.NoFierro,
                            GrupoCorral = corral.GrupoCorral,
                            DescripcionGanadoID = deteccion.DescripcionGanadoID,
                            Activo = 1
                        };
                    }
                    else
                    {
                        deteccionGrabar = new DeteccionInfo()
                        {
                            LoteID = lote.LoteID,
                            CorralID = corral.CorralID,
                            FotoDeteccion = foto,
                            Observaciones = deteccion.Observaciones,
                            Arete = deteccion.Arete,
                            AreteMetalico = deteccion.AreteMetalico,
                            OperadorID = seguridad.Usuario.Operador.OperadorID,
                            UsuarioCreacionID = seguridad.Usuario.UsuarioID,
                            DescripcionGanado = deteccion.DescripcionGanado,
                            Problemas = deteccion.Problemas,
                            Sintomas = deteccion.Sintomas,
                            GradoID = deteccion.GradoID,
                            NoFierro = deteccion.NoFierro,
                            GrupoCorral = corral.GrupoCorral,
                            DescripcionGanadoID = deteccion.DescripcionGanadoID,
                            Activo = 1
                        };
                    }

                    if (corral.TipoCorral.TipoCorralID == (int)TipoCorral.CorraletaSacrificio)
                    {
                        deteccionGrabar.LoteID = animalPL.ObtenerLoteSalidaAnimal(deteccionGrabar.Arete, deteccionGrabar.AreteMetalico, organizacionID);
                        corral = corralPL.ObtenerCorralPorLoteID(deteccionGrabar.LoteID, organizacionID);
                        deteccionGrabar.GrupoCorral = corral.GrupoCorral;
                    }


                    if (corral.GrupoCorral == (int)GrupoCorralEnum.Recepcion)
                    {
                        if (deteccionGrabar.GradoID == GradoEnfermedadEnum.Level1.GetHashCode() ||
                            deteccionGrabar.GradoID == GradoEnfermedadEnum.Level2.GetHashCode())
                        {
                            return 2; //Error de Ganado Grado 2 en corral de Recepcion
                        }
                    }

                    var resultado = deteccionPl.GuardarDeteccion(deteccionGrabar,
                                                                 esCargaInicial,
                                                                 animal);
                    if (resultado == 1)
                    {
                        retValue = resultado;
                    }

                }
                else
                {
                    retValue = 0;
                }
            }
            catch (Exception ex)
            {
                retValue = -1;
            }
            return retValue;
        }

        /// <summary>
        /// Metodo para Consultar los Aretes del corral
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<AnimalInfo> ObtenerAretes(CorralInfo corralInfo)
        {
            List<AnimalInfo> listaAnimales = null;
            try
            {
                var seguridad = (SeguridadInfo)HttpContext.Current.Session["Seguridad"];
               
                if (seguridad != null)
                {
                    bool esPartida = false;
                    int organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                    
                    var corralPl = new CorralPL();
                    CorralInfo corral = corralPl.ObtenerCorralPorCodigo(organizacionId, corralInfo.Codigo);
                    if (corral.GrupoCorral == GrupoCorralEnum.Recepcion.GetHashCode())
                    {
                        esPartida = true;
                    }
                    var animalPL = new AnimalPL();
                    listaAnimales = animalPL.ObtenerAnimalesPorCorralDeteccion(corral.CorralID, esPartida);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return listaAnimales;
        }
    }
}