using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.WinForm.Auxiliar;
using SuKarne.Controls.Enum;
using SuKarne.Controls.MessageBox;

namespace SIE.WinForm.Catalogos
{
    /// <summary>
    /// Interaction logic for ProblemaSintomaEdicion.xaml
    /// </summary>
    public partial class ProblemaSintomaEdicion
    {
        #region Propiedades

        /// <summary>
        /// Propiedad para manejar el resultado de los sintomas
        /// </summary>
        private ResultadoInfo<SintomaInfo> resultadoInfo;

        /// <summary>
        /// Contenerdor de la clase
        /// </summary>
        private ProblemaSintomaInfo Contexto
        {
            get
            {
                if (DataContext == null)
                {
                    InicializaContexto();
                }
                return (ProblemaSintomaInfo) DataContext;
            }
            set { DataContext = value; }
        }

        /// <summary>
        /// Se utiliza para indentificar la confimación del mensaje 
        /// </summary>
        private bool confirmaSalir = true;

        /// <summary>
        /// Se utiliza para llenar la lista de sintomas
        /// </summary>
        private IList<ProblemaSintomaInfo> listaProblemasSintoma;

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ProblemaSintomaEdicion()
        {
            InitializeComponent();
            InicializaContexto();
            CargaComboTipoProblema();
        }

        /// <summary>
        /// Constructor para editar una entidad ProblemaSintoma Existente
        /// </summary>
        /// <param name="problemaSintomaInfo"></param>
        public ProblemaSintomaEdicion(ProblemaSintomaInfo problemaSintomaInfo)
        {
            InitializeComponent();
            problemaSintomaInfo.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
            CargaComboTipoProblema();
            Contexto = problemaSintomaInfo;

            if (Contexto.ListaSintomas != null && Contexto.ListaSintomas.Any())
            {
                int ordenSecuencia = 0;
                Contexto.ListaSintomas.ForEach(sintoma =>
                                                   {
                                                       ordenSecuencia = ordenSecuencia + 1;
                                                       sintoma.Orden = ordenSecuencia;
                                                   });
            }
        }

        #endregion Constructores

        #region Eventos

        /// <summary>
        /// Evento de Carga de la forma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarListaProblemasSintomas();
            skAyudaProblema.ObjetoNegocio = new ProblemaBL();
            skAyudaProblema.PuedeBuscar = () =>
                                              {
                                                  return (Contexto.Problema != null &&
                                                          Contexto.Problema.TipoProblema != null &&
                                                          Contexto.Problema.TipoProblema.TipoProblemaId > 0);
                                              };
            skAyudaProblema.AyudaConDatos += (o, args) =>
                                                 {
                                                     var tipoProblema =
                                                         cmbTipoProblema.SelectedItem as TipoProblemaInfo;
                                                     var contextoAyuda = skAyudaProblema.Contexto as ProblemaInfo;
                                                     contextoAyuda.TipoProblema = tipoProblema;
                                                     ObtenerListaSintomas();
                                                     if (Contexto.ListaSintomas != null && Contexto.ListaSintomas.Any())
                                                     {
                                                         int ordenSecuencia = 0;
                                                         Contexto.ListaSintomas.ForEach(sintoma =>
                                                                                            {
                                                                                                ordenSecuencia =
                                                                                                    ordenSecuencia + 1;
                                                                                                sintoma.Orden =
                                                                                                    ordenSecuencia;
                                                                                            });
                                                     }
                                                     CargarGridSintomas(ucPaginacionSintoma.Inicio,
                                                                        ucPaginacionSintoma.Limite);
                                                 };
            skAyudaProblema.AyudaLimpia += (o, args) =>
                                               {
                                                   gridDatosSintoma.ItemsSource = null;
                                                   CargarListaProblemasSintomas();
                                               };

            ucPaginacionSintoma.DatosDelegado += CargarGridSintomas;
            ucPaginacionSintoma.AsignarValoresIniciales();
            ucPaginacionSintoma.Contexto = Contexto;

            CargarGridSintomas(ucPaginacionSintoma.Inicio, ucPaginacionSintoma.Limite);
            //skAyudaSintoma.ObjetoNegocio = new SintomaBL();
        }

        /// <summary>
        /// Evento que se ejecuta mientras se esta cerrando la ventana
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (confirmaSalir)
            {
                MessageBoxResult result = SkMessageBox.Show(this, Properties.Resources.Msg_CerrarSinGuardar,
                                                            MessageBoxButton.YesNo,
                                                            MessageImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Contexto = null;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Evento que guarda los datos de la entidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Guardar();
        }

        /// <summary>
        /// Evento para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = (Button)e.Source;
                var sintomaEditar = (SintomaInfo)btn.CommandParameter;

                var problemaSintomaEditar = new ProblemaSintomaInfo
                {
                    Sintoma = new SintomaInfo
                    {
                        SintomaID = sintomaEditar.SintomaID,
                        Descripcion = sintomaEditar.Descripcion,
                        Activo = sintomaEditar.Activo,
                        HabilitaEdicion = false
                    }
                };

                var sintomaOriginal = (SintomaInfo)Extensor.ClonarInfo(sintomaEditar);
                var problemaSintomaEdicionSintoma =
                    new ProblemaSintomaEdicionSintoma(problemaSintomaEditar)
                    {
                        ucTitulo = { TextoTitulo = Properties.Resources.ProblemaSintomaEdicion_Edicion }
                    };
                MostrarCentrado(problemaSintomaEdicionSintoma);
                ReiniciarValoresPaginador();
                if (problemaSintomaEdicionSintoma.ConfirmaSalir)
                {
                    var sintomaModificado =
                        Contexto.ListaSintomas.FirstOrDefault(
                            sinto => sinto.SintomaID == sintomaOriginal.SintomaID);
                    if (sintomaModificado == null)
                    {
                        return;
                    }
                    sintomaModificado.Activo = sintomaOriginal.Activo;
                    gridDatosSintoma.ItemsSource = null;
                    gridDatosSintoma.ItemsSource = resultadoInfo.Lista;
                }
                else
                {
                    var sintomaModificado =
                         Contexto.ListaSintomas.FirstOrDefault(
                             sinto => sinto.SintomaID == sintomaOriginal.SintomaID);
                    if (sintomaModificado == null)
                    {
                        return;
                    }
                    sintomaModificado.UsuarioModificacionID = AuxConfiguracion.ObtenerUsuarioLogueado();
                    sintomaModificado.Activo = problemaSintomaEditar.Sintoma.Activo;
                    gridDatosSintoma.ItemsSource = null;
                    gridDatosSintoma.ItemsSource = resultadoInfo.Lista;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                SkMessageBox.Show(this, Properties.Resources.TratamiendoEdicion_ErrorEditar, MessageBoxButton.OK, MessageImage.Error);
            }
        }

        private void BotonNuevoSintoma_Click(object sender, RoutedEventArgs e)
        {
            var problemaSintoma = new ProblemaSintomaInfo
            {
                Sintoma = new SintomaInfo
                {
                    HabilitaEdicion = true,
                    UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()
                },
                UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado()

            };
            var problemaSintomaEdicionSintoma = new ProblemaSintomaEdicionSintoma(problemaSintoma)
            {
                ucTitulo = { TextoTitulo = Properties.Resources.ProblemaSintomaEdicion_Nuevo }
            };
            MostrarCentrado(problemaSintomaEdicionSintoma);
            ReiniciarValoresPaginador();

            if (problemaSintomaEdicionSintoma.ConfirmaSalir)
            {
                return;
            }
            if (problemaSintoma.Sintoma != null && problemaSintoma.Sintoma.SintomaID != 0)
            {
                var sintomaRepetido =
                    Contexto.ListaSintomas.FirstOrDefault(
                        sinto => sinto.SintomaID == problemaSintoma.Sintoma.SintomaID);
                if (sintomaRepetido != null)
                {
                    SkMessageBox.Show(this, Properties.Resources.ProblemaSintomaEdicion_SintomaRepetido,
                                      MessageBoxButton.OK, MessageImage.Warning);
                    return;
                }
                int ordenMaximo = 0;
                if (Contexto.ListaSintomas.Any())
                {
                    ordenMaximo = Contexto.ListaSintomas.Max(sinto => sinto.Orden);
                }
                problemaSintoma.Sintoma.UsuarioCreacionID = problemaSintoma.UsuarioCreacionID;
                problemaSintoma.Sintoma.Orden = ordenMaximo + 1;
                Contexto.ListaSintomas.Add(problemaSintoma.Sintoma);
                resultadoInfo.Lista.Add(problemaSintoma.Sintoma);

                gridDatosSintoma.ItemsSource = null;
                gridDatosSintoma.ItemsSource = resultadoInfo.Lista;
                resultadoInfo.TotalRegistros = Contexto.ListaSintomas.Count;
                ucPaginacionSintoma.TotalRegistros = resultadoInfo.TotalRegistros;
            }

        }

        #endregion Eventos

        #region Métodos

        private void CargarListaProblemasSintomas()
        {
            var problemaSintomaBL = new ProblemaSintomaBL();
            listaProblemasSintoma = problemaSintomaBL.ObtenerProblemasSintomaTodos();
        }

        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void ObtenerListaSintomas()
        {
            var sintomas = (from sinto in listaProblemasSintoma
                            where sinto.ProblemaID == Contexto.Problema.ProblemaID
                            select new SintomaInfo
                                       {
                                           SintomaID = sinto.SintomaID,
                                           Descripcion = sinto.Sintoma.Descripcion,
                                           Activo = sinto.Activo,
                                           ProblemaSintomaID = sinto.ProblemaSintomaID,
                                           UsuarioCreacionID = sinto.UsuarioCreacionID,
                                           UsuarioModificacionID = sinto.UsuarioModificacionID
                                       }).ToList();

            Contexto.ListaSintomas = sintomas;
        }

        private void CargarGridSintomas(int inicio, int limite)
        {
            if (Contexto.ListaSintomas == null || !Contexto.ListaSintomas.Any())
            {
                Contexto.ListaSintomas = new List<SintomaInfo>();
                resultadoInfo = new ResultadoInfo<SintomaInfo>
                                    {
                                        Lista = new List<SintomaInfo>()
                                    };

                return;
            }
            resultadoInfo = new ResultadoInfo<SintomaInfo>
                                {
                                    TotalRegistros = Contexto.ListaSintomas.Count,
                                    Lista =
                                        Contexto.ListaSintomas.Where(
                                            prod => prod.Orden >= inicio && prod.Orden <= limite).ToList()
                                };
            gridDatosSintoma.ItemsSource = null;
            if (resultadoInfo != null && resultadoInfo.Lista != null &&
                resultadoInfo.Lista.Count > 0)
            {
                gridDatosSintoma.ItemsSource = resultadoInfo.Lista;
                ucPaginacionSintoma.TotalRegistros = resultadoInfo.TotalRegistros;
            }
            else
            {
                ucPaginacionSintoma.TotalRegistros = 0;
                resultadoInfo = new ResultadoInfo<SintomaInfo>
                                    {
                                        TotalRegistros = 0,
                                        Lista = new List<SintomaInfo>()
                                    };
                gridDatosSintoma.ItemsSource = new List<SintomaInfo>();
            }
        }


        /// <summary>
        /// Inicializa el Contexto
        /// </summary>
        private void InicializaContexto()
        {
            Contexto = new ProblemaSintomaInfo
                           {
                               UsuarioCreacionID = AuxConfiguracion.ObtenerUsuarioLogueado(),
                               Problema = new ProblemaInfo
                                              {
                                                  TipoProblema = new TipoProblemaInfo()
                                              },
                               Sintoma = new SintomaInfo(),
                               ListaSintomas = new List<SintomaInfo>()
                           };
        }

        /// <summary>
        /// Metodo que valida los datos para guardar
        /// </summary>
        /// <returns></returns>
        private bool ValidaGuardar()
        {
            bool resultado = true;
            string mensaje = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(txtProblemaSintomalID.Text))
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaSintomaEdicion_MsgProblemaSintomalIDRequerida;
                    txtProblemaSintomalID.Focus();
                }
                else if (Contexto.Problema.TipoProblema == null || Contexto.Problema.TipoProblema.TipoProblemaId == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaSintomaEdicion_TipoProblemaRequerido;
                    cmbTipoProblema.Focus();
                }
                else if (Contexto.Problema == null || Contexto.Problema.ProblemaID == 0)
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaSintomaEdicion_MsgProblemaIDRequerida;
                    skAyudaProblema.AsignarFoco();
                }
                else if (Contexto.ListaSintomas == null || !Contexto.ListaSintomas.Any())
                {
                    resultado = false;
                    mensaje = Properties.Resources.ProblemaSintomaEdicion_MsgSinSintomas;
                    skAyudaProblema.AsignarFoco();
                }
            }
            catch (Exception ex)
            {
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                SkMessageBox.Show(this, mensaje, MessageBoxButton.OK, MessageImage.Warning);
            }
            return resultado;
        }


        /// <summary>
        /// Método para guardar los valores del contexto
        /// </summary>
        private void Guardar()
        {
            bool guardar = ValidaGuardar();

            if (guardar)
            {
                try
                {
                    int problemaSintomaID = Contexto.ProblemaSintomaID;
                    var problemaSintomaBL = new ProblemaSintomaBL();
                    Contexto.ProblemaID = Contexto.Problema.ProblemaID;
                    //Contexto.SintomaID = Contexto.Sintoma.SintomaID;
                    problemaSintomaBL.Guardar(Contexto);
                    SkMessageBox.Show(this, Properties.Resources.GuardadoConExito, MessageBoxButton.OK,
                                      MessageImage.Correct);
                    gridDatosSintoma.ItemsSource = null;
                    CargarListaProblemasSintomas();
                    if (problemaSintomaID != 0)
                    {
                        confirmaSalir = false;
                        Close();
                    }
                    else
                    {
                        InicializaContexto();
                    }
                }
                catch (ExcepcionGenerica)
                {
                    SkMessageBox.Show(this, Properties.Resources.ProblemaSintoma_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    SkMessageBox.Show(this, Properties.Resources.ProblemaSintoma_ErrorGuardar, MessageBoxButton.OK,
                                      MessageImage.Error);
                }
            }
        }

        /// <summary>
        /// Carga los datos de la entidad Tipo Problema 
        /// </summary>
        private void CargaComboTipoProblema()
        {
            using (var tipoProblemaBL = new TipoProblemaBL())
            {
                var tipoProblema = new TipoProblemaInfo
                                       {
                                           TipoProblemaId = 0,
                                           Descripcion = Properties.Resources.cbo_Seleccione,
                                       };
                IList<TipoProblemaInfo> listaTipoProblema = tipoProblemaBL.ObtenerTodos(EstatusEnum.Activo);
                listaTipoProblema.Insert(0, tipoProblema);
                cmbTipoProblema.ItemsSource = listaTipoProblema;
                if (Contexto.ProblemaID == 0)
                {
                    cmbTipoProblema.SelectedItem = tipoProblema;
                }
            }
        }

        /// <summary>
        /// Reinicia el valor del inicio de la página.
        /// </summary>
        private void ReiniciarValoresPaginador()
        {
            ucPaginacionSintoma.Inicio = 1;
        }

        #endregion Métodos
    }
}

