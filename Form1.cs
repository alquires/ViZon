/* Classe do formulário principal (frmPrincipal)
 * Versão 17
 * Estes códigos são de propriedade da Embrapa Solos UEP Recife
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.PublisherControls;
using System.IO;



namespace ZonView017
{
    public partial class frmPrincipal : Form
    {
        //=========================================
        //         Variaveis e declaracoes
        //=========================================
        /// <summary>Guarda a tabela com os dados do projeto</summary>
        DataTable dtTabela = new DataTable();
        /// <summary>Data set que vai recebendo as tabelas e juntando os valores numa unica tabela</summary>
        public DataSet ds = new DataSet();
        public string strLayers = "";

        /// <summary>
        /// guarda o layer selecionado para o swipe
        /// </summary>
        ARLayer LeierSwipe = null;
        /// <summary>
        /// Controla as coordenadas X obtidas ao clicar com o mouse.
        /// </summary>
        double mouseXcoord = 0;
        /// <summary>
        /// Controla as coordenadas Y obtidas ao clicar com o mouse.
        /// </summary>
        double mouseYcoord = 0;

        //progresso
        /// <summary>
        /// controla o valor de progresso da barra de progresso
        /// </summary>
        int progresso = 0;
        /// <summary>
        /// controla o texto que informa o usuário o que está acontecendo
        /// </summary>
        string textoProgresso = "";        

        /// <summary>
        /// controla o estado dos botões de vetorização
        /// </summary>
        bool Vetoriza = false;
        string ValVetoriza = "";
        int ValPto = 0;
        double versao = 1.05;
        
        /// <summary>
        /// Copia os parametros de pesquisa para passagem aos formulários de pesquisa
        /// </summary>
        public string strParPesq = "";
        /// <summary>
        /// Variavel com parametros extras da pesquisa, usado no relatorio
        /// </summary>
        public string strPlus = "";
        /// <summary>
        /// Variavel com os parametros utilizados na manipulação do projeto
        /// </summary>
        string[] Parametros = new string[18];


        /// <summary>
        /// controla os valores do quadrante original do projeto
        /// </summary>
        double O_XMin = 0, O_XMax = 0, O_YMin = 0, O_YMax = 0;
        /// <summary>
        /// controla o valor da escala original do projeto, impedindo a manipulação do zoom para fora do especificado
        /// </summary>
        double O_Escala = 0;

        /// <summary>
        /// Guarda os valores de extensão da área de estudo, impedindo a manipulação do zoom fora da area de pesquisa
        /// </summary>
        double AE_XMin = 0, AE_XMax = 0, AE_YMin = 0, AE_YMax = 0;
        /// <summary>
        /// controla o valor da escala, impedindo a manipulação do zoom para fora do especificado
        /// </summary>
        double AE_Escala = 0;
        
        /// <summary> 
        /// <para>Verifica se o ViZon está com uma area de estudo customizada.</para>
        /// <para>Se for, deve definir valores personalizados de extensão para toda area</para>
        /// </summary>
        bool EhAreaEstudo = false;
        /// <summary>
        /// verifica se há a seleção de uma area de pesquisa
        /// </summary>
        bool EhPesquisa = false;



        /*feiçoes selecionadas para mapa temático
        //acho que vai voar!!
        private ARFeatureSet m_arFeatureSetMeets1;
        private bool nivel1 = false;
        private ARFeatureSet m_arFeatureSetMeets2;
        private bool nivel2 = false;
        private ARFeatureSet m_arFeatureSetMeets3;
        private bool nivel3 = false;
        private ARFeatureSet m_arFeatureSetMeets4;
        private bool nivel4 = false;
        private ARFeatureSet m_arFeatureSetMeets5;
        private bool nivel5 = false;
        private ARFeatureSet m_arFeatureSetMeets6;
        private bool nivel6 = false;
        private ARFeatureSet m_arFeatureSetMeets7;
        private bool nivel7 = false;*/

        /// <summary>
        /// Recebe as feições selecionadas pela pesquisa mostrando os polígonos selecionados
        /// </summary>
        private ARFeatureSet FeicoesPesquisa1;
        private ARFeatureSet FeicoesPesquisa2;
        private ARFeatureSet FeicoesPesquisa3;
        private ARFeatureSet FeicoesPesquisa4;
        private ARFeatureSet FeicoesPesquisa5;
        /// <summary>
        /// Recebe as feições selecionadas pela pesquisa, definindo a area de estudo
        /// </summary>
        private ARFeatureSet m_arFeatureAreaEstudo1;
        private ARFeatureSet m_arFeatureAreaEstudo2;
        private ARFeatureSet m_arFeatureAreaEstudo3;
        private ARFeatureSet m_arFeatureAreaEstudo4;
        private ARFeatureSet m_arFeatureAreaEstudo5;


        /// <summary>guarda a localização do projeto</summary>
        private string PathProjeto;

        //=========================================
        //              construtor
        //=========================================
        public frmPrincipal()
        {
            InitializeComponent();
            TravaSeguranca(false);

        }

        //=========================================
        //                Métodos
        //=========================================
        /// <summary>
        /// <para>Abre o Arquivo *.ZON</para>
        /// </summary>
        private void AbrirArquivo()
        {
            /* 
             * este método abre o arquivo *.zon e carrega todo o projeto
             */
            try
            {
                //abre caixa de diálogo
                OpenFileDialog fDialog = new OpenFileDialog();
                Config set = new Config();

                fDialog.Title = "Abrir Zoneamento";
                fDialog.Filter = "Arquivos ZON|*.ZON|Todos|*.*";
                fDialog.InitialDirectory = set.Caminho1;

                if (fDialog.ShowDialog() == DialogResult.OK)
                {
                    ExecutaAbertura(fDialog.FileName);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
    Ex.Message);
            }

        }
        
        /// <summary>Adiciona uma tabela ao Data Set
        /// <param name="Tabela">Tabela a ser adicionada</param>
        /// <param name="valori">Valor do indice da localização da tabela</param>
        /// </summary>
        private void AddTableDataSet(DataTable Tabela, int valori)
        {
            /*
             * este método adiciona uma tabela (Tabela) no dataset segundo o valor de posição (valori)
             */


            try
            {
                ds.Tables.Add();

                DataRow[] dr = new DataRow[Tabela.Rows.Count];
                DataRow dr2;
                //adiciona e monta as colunas
                for (int i4 = 0; i4 < Tabela.Columns.Count; i4++)
                {
                    ds.Tables[valori].Columns.Add();
                    ds.Tables[valori].Columns[i4].DataType = Tabela.Columns[i4].DataType;
                    ds.Tables[valori].Columns[i4].ColumnName = Tabela.Columns[i4].ColumnName;
                }

                //adiciona as linhas
                for (int i3 = 0; i3 < Tabela.Rows.Count; i3++)
                {
                    dr.SetValue(Tabela.Rows[i3], i3);
                    dr2 = dr[i3];
                    ds.Tables[valori].ImportRow(dr2);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
    Ex.Message);
            }


        }

        /// <summary>
        /// Transforma a pesquisa em um nível do mapa tematico
        /// <param name="Posicao">Indica a posicao onde a pesquisa deve ser inserida como nível de mapa temático</param>
        /// </summary>
        /*private void AdicionaNivel(int Posicao)
        {


            try
            {
                ColorDialog CD = new ColorDialog();
                int NumCor = 0;
                if (CD.ShowDialog() == DialogResult.OK)
                {
                    switch (Posicao)
                    {
                        case 1:
                            m_arFeatureSetMeets1 = FeicoesPesquisa1;
                            NumCor = ColorTranslator.ToOle(CD.Color);
                            FeicoesPesquisa1.Highlight(false, 0);
                            FeicoesPesquisa1 = null;
                            tsmN1.BackColor = CD.Color;
                            m_arFeatureSetMeets1.Highlight(true, NumCor);
                            nivel1 = true;
                            break;

                        case 2:
                            m_arFeatureSetMeets2 = FeicoesPesquisa1;
                            NumCor = ColorTranslator.ToOle(CD.Color);
                            FeicoesPesquisa1.Highlight(false, 0);
                            FeicoesPesquisa1 = null;
                            tsmN2.BackColor = CD.Color;
                            m_arFeatureSetMeets2.Highlight(true, NumCor);
                            nivel2 = true;
                            break;

                        case 3:
                            m_arFeatureSetMeets3 = FeicoesPesquisa1;
                            NumCor = ColorTranslator.ToOle(CD.Color);
                            FeicoesPesquisa1.Highlight(false, 0);
                            FeicoesPesquisa1 = null;
                            tsmN3.BackColor = CD.Color;
                            m_arFeatureSetMeets3.Highlight(true, NumCor);
                            nivel3 = true;
                            break;

                        case 4:
                            m_arFeatureSetMeets4 = FeicoesPesquisa1;
                            NumCor = ColorTranslator.ToOle(CD.Color);
                            FeicoesPesquisa1.Highlight(false, 0);
                            FeicoesPesquisa1 = null;
                            tsmN4.BackColor = CD.Color;
                            m_arFeatureSetMeets4.Highlight(true, NumCor);
                            nivel4 = true;
                            break;

                        case 5:
                            m_arFeatureSetMeets5 = FeicoesPesquisa1;
                            NumCor = ColorTranslator.ToOle(CD.Color);
                            FeicoesPesquisa1.Highlight(false, 0);
                            FeicoesPesquisa1 = null;
                            tsmN5.BackColor = CD.Color;
                            m_arFeatureSetMeets5.Highlight(true, NumCor);
                            nivel5 = true;
                            break;

                        case 6:
                            m_arFeatureSetMeets6 = FeicoesPesquisa1;
                            NumCor = ColorTranslator.ToOle(CD.Color);
                            FeicoesPesquisa1.Highlight(false, 0);
                            FeicoesPesquisa1 = null;
                            tsmN6.BackColor = CD.Color;
                            m_arFeatureSetMeets6.Highlight(true, NumCor);
                            nivel6 = true;
                            break;

                        case 7:
                            m_arFeatureSetMeets7 = FeicoesPesquisa1;
                            NumCor = ColorTranslator.ToOle(CD.Color);
                            FeicoesPesquisa1.Highlight(false, 0);
                            FeicoesPesquisa1 = null;
                            tsmN7.BackColor = CD.Color;
                            m_arFeatureSetMeets7.Highlight(true, NumCor);
                            nivel7 = true;
                            break;
                    }//fim do switch
                    EhPesquisa = false;
                    TravaPesquisa();
                    TravaMapaTematico();
                }//fim do if (CD.ShowDialog() == DialogResult.OK)
                RecarregaMapasTematicos();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }


        }//fim do metodo*/

        /// <summary>
        /// Recarrega o mapa temático, carregando individualmente cada nivel
        /// </summary>
        /*private void RecarregaMapasTematicos()
        {
            try
            {
                if (nivel1 == true)
                {
                    int NumCor = ColorTranslator.ToOle(tsmN1.BackColor);
                    m_arFeatureSetMeets1.Highlight(true, NumCor);
                }
                if (nivel2 == true)
                {
                    int NumCor = ColorTranslator.ToOle(tsmN2.BackColor);
                    m_arFeatureSetMeets2.Highlight(true, NumCor);
                }
                if (nivel3 == true)
                {
                    int NumCor = ColorTranslator.ToOle(tsmN3.BackColor);
                    m_arFeatureSetMeets3.Highlight(true, NumCor);
                }
                if (nivel4 == true)
                {
                    int NumCor = ColorTranslator.ToOle(tsmN4.BackColor);
                    m_arFeatureSetMeets4.Highlight(true, NumCor);
                }
                if (nivel5 == true)
                {
                    int NumCor = ColorTranslator.ToOle(tsmN5.BackColor);
                    m_arFeatureSetMeets5.Highlight(true, NumCor);
                }
                if (nivel6 == true)
                {
                    int NumCor = ColorTranslator.ToOle(tsmN6.BackColor);
                    m_arFeatureSetMeets6.Highlight(true, NumCor);
                }
                if (nivel7 == true)
                {
                    int NumCor = ColorTranslator.ToOle(tsmN7.BackColor);
                    m_arFeatureSetMeets7.Highlight(true, NumCor);
                }
            }
            catch
            {

            }

        }*/

        /// <summary>
        /// Faz a carregamento das ferramentas da biblioteca PublisherControl
        /// <param name="intNumTool"><para>Valor inteiro de define a ferramenta selecionada.</para>
        /// </summary>
        private void ArcFerramentas(int intNumTool)
        {
            /*
             * Todas as ferramentas disponíveis na biblioteca PublisherControl
             */



            try
            {
                switch (intNumTool)
                {
                    case 0:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMapZoomIn;
                        break;
                    case 1:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMapZoomOut;
                        break;
                    case 2:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMapPan;
                        break;
                    case 3:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMapIdentify;
                        break;
                    case 4:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolLayoutZoomIn;
                        break;
                    case 5:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolLayoutZoomOut;
                        break;
                    case 6:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolLayoutPan;
                        break;
                    case 7:
                        
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMapMeasure;
                        break;
                    case 8:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMapHyperlink;                        
                        break;
                    case 9:
                        arcVerZon.CurrentARTool =
                        ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolNoneSelected;
                        break;
                    case 10:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMapIdentifyUsingLayer;
                        break;
                    case 11:
                        if (arcVerZon.CurrentViewType == esriARViewType.esriARViewTypeMap)
                        {
                            try
                            {
                                LeierSwipe = arcVerZon.SelectedARLayer;
                                arcVerZon.SwipeLayer = LeierSwipe;
                                arcVerZon.CurrentARTool =
                                    ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMapSwipe;
                                arcVerZon.Refresh();
                            }

                            catch
                            {
                                MessageBox.Show("Você precisa selecionar um plano de informação (layer) válido.");
                            }
                        }
                        break;
                    case 12:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMapZoomInOut;
                        break;
                    case 13:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupHighlightBlue;
                        break;
                    case 14:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupHighlightGreen;
                        break;
                    case 15:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupHighlightYellow;
                        break;
                    case 16:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupPenMedium;
                        break;
                    case 17:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupPenThick;
                        break;
                    case 18:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupPenThin;
                        break;
                    case 19:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupEraserStroke;
                        break;
                    case 20:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupEraserSmall;
                        break;
                    case 21:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupEraserMedium;
                        break;
                    case 22:
                        arcVerZon.CurrentARTool =
                            ESRI.ArcGIS.PublisherControls.esriARTool.esriARToolMarkupEraserLarge;
                        break;
                }
            }
            catch
            {

            }


        }

        /// <summary>
        /// Faz a carregamento das janelas da biblioteca PublisherControl 
        /// <param name="intNumTool"><para>Valor inteiro de define a janela selecionada.</para>
        /// </summary>
        private void ArcWindows(int intNumTool)
        {
            /*
             * controla as janelas da biblioteca PublisherControl
             */


            try
            {
                switch (intNumTool)
                {
                    case 0:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsFind);
                        break;

                    case 1:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsMagnifier);
                        break;

                    case 2:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsPrinter);
                        break;

                    case 3:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsPageSetup);
                        break;

                    case 4:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsFindXY);
                        arcVerZon.CreateGraphics().Dispose();
                        break;

                    case 5:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsMyPlaces);
                        break;

                    case 6:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsPMFTitle);
                        break;

                    case 7:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsIdentify);
                        break;

                    case 8:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsFindNearbyPlacesDeprecated);
                        break;

                    case 9:
                        arcVerZon.ShowARWindow(esriARWindows.esriARWindowsInternetBrowser);
                        break;
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// faz a chamada e descriptografa o arquivo passado
        /// <param name="FilePath"><para>Caminho do arquivo a ser descriptografado</para>
        /// </summary>
        private void AtributosPesquisa(string FilePath)
        {
            /*
             * faz a chamada e descriptografa o arquivo passado (FilePath)
             * LOG dispensado (valor insignificante)
             */
            
            try
            {
                Enigma enig = new Enigma();

                string temp001 = "";
                if (strParPesq != "")
                {
                    temp001 = enig.Descriptografa(FilePath);
                    strParPesq = strParPesq + "\r" + temp001;
                 }
                else
                {
                    strParPesq = strParPesq + enig.Descriptografa(FilePath);
                }
            }
            catch
            {

            }
        }
        
        /// <summary>
        /// Atualiza o valor da escala na barra de ferramenta
        /// </summary>
        private void AtualizaEscala()
        {
            /*
             * dispensado de log
             */
            try
            {
                ARMap map = arcVerZon.ARPageLayout.FocusARMap;
                double temp = Math.Round(map.MapScale, 0);
                tscEscala.Text = temp.ToString();
                TravaMovZoom();
            }
            catch
            {

            }
        }
        
        /// <summary>
        /// Carrega o banco de dados para pesquisa do projeto
        /// </summary>
        /// <param name="ParametrosArqBD">Parametros do arquivo *.psq</param>
        /// <param name="PathToPSQ">Caminho até o arquivo *.PSQ</param>
        private void CarregaBD(string ParametrosArqBD, string PathToPSQ)
        {
            /*
             * Carrega os bancos de dados especificados no arquivo *.psq
             */

            //faz a quebra da string para uma variavel matricial -> [,]
            try
            {
                string[] ParametrosBD = ParametrosArqBD.Split('\n');
                int int001 = ParametrosBD.Length;
                string[] strTemp001 = ParametrosBD[0].Split('#');
                int int002 = strTemp001.Length;
                string[,] strParametros = new string[int001, int002];

                //quebra o array em uma matriz bidimensional
                //o laço for faz a consulta a cada tabela especificada no arquivo *.psq
                //isto é, cada volta é uma consulta

                int valorPercentagem = 80 / int001;

                for (int i = 0; i < int001; i++)
                {


                    DataTable DT1;
                    BD BDClass = new BD();
                    strTemp001 = ParametrosBD[i].Split('#');

                    //preenche item a item, linha por linha
                    for (int i2 = 0; i2 < int002; i2++)
                    { strParametros[i, i2] = strTemp001[i2]; }
                    string strPathPesquisa = Path.GetDirectoryName(PathToPSQ) + "\\";

                    progresso = progresso + valorPercentagem;
                    textoProgresso = "Abrindo base de pesquisa - Tabela " + i.ToString();
                    barraProgresso();


                    //faz a chamada das tabelas e dos banco de dados
                    switch (Int32.Parse(strParametros[i, 0]))
                    {
                        //=====================================
                        //caso seja um banco de dados em Access 
                        //=====================================
                        case 1:
                            //monta o caminho até o arquivo *.mdb
                            //strPathPesquisa = Path.GetDirectoryName(PathToPSQ) + "\\";


                            //abre, consulta e extrai os dados da classe BD
                            DT1 = BDClass.DB_Access(strTemp001[1], strTemp001[3], strTemp001[2], strPathPesquisa, i,
                                strTemp001[10], strTemp001[11],strTemp001[8]);

                            //insere a tabela no DataSet 'ds' somente se o retorno
                            //for uma tabela com pelo menos 1 linha
                            if (DT1.Columns.Count > 0)
                            {
                                string FileNameAtributos = strPathPesquisa + strTemp001[12];
                                //carrega os aliases para as colunas, facilitando a pesquisa
                                AtributosPesquisa(FileNameAtributos);

                                int vi = ds.Tables.Count;
                                AddTableDataSet(DT1, vi);
                                DT1.Clear();
                            }
                            break;

                        //=====================================
                        //caso seja um banco de dados em MySql
                        //=====================================
                        case 2:
                            //abre, consulta e extrai os dados
                            DT1 = BDClass.DB_MySql(strTemp001[6], strTemp001[1], strTemp001[3], strTemp001[2], strTemp001[7],
                                                            strTemp001[8], strTemp001[10], strTemp001[11]);

                            //insere a tabela no DataSet ds
                            if (DT1.Columns.Count > 0)
                            {
                                string FileNameAtributos = strPathPesquisa + strTemp001[12];
                                //carrega os aliases para as colunas, facilitando a pesquisa
                                AtributosPesquisa(FileNameAtributos);

                                int vi = ds.Tables.Count;
                                AddTableDataSet(DT1, vi);
                                DT1.Clear();
                            }

                            break;

                        //=========================================
                        //caso seja um banco de dados em PostgreSql
                        //=========================================
                        case 3:
                            //abre, consulta e extrai os dados
                            DT1 = BDClass.BD_PostgreSQL(strTemp001[6], strTemp001[1], strTemp001[3], strTemp001[2], strTemp001[7],
                                strTemp001[8], strTemp001[9], strTemp001[10], strTemp001[11]);

                            //insere a tabela no DataSet 'ds' somente se o retorno
                            //for uma tabela com pelo menos 1 linha
                            if (DT1.Columns.Count > 0)
                            {
                                string FileNameAtributos = strPathPesquisa + strTemp001[12];
                                //carrega os aliases para as colunas, facilitando a pesquisa
                                AtributosPesquisa(FileNameAtributos);

                                int vi = ds.Tables.Count;
                                AddTableDataSet(DT1, vi);
                                DT1.Clear();
                            }
                            break;

                        //=========================================
                        //caso seja um banco de dados em SQL Server
                        //            Não implementado
                        //=========================================
                        case 4:
                            MessageBox.Show("Esta versão de software ainda não possui suporte para Bancos de dados SQL Server");
                            /*CP.lblInfoProjeto.Text = "Conectando com o banco de dados...";
                            CP.prgProgresso.Value = CP.prgProgresso.Value + passos;
                            DataTable DT4 = BDClass.DB_SQL();
                            if (DT4.Columns.Count > 0)
                            {
                                int vi = ds.Tables.Count;
                                AddTableDataSet(DT4, vi);
                            }*/
                            break;

                        //=========================================
                        //  caso seja um banco de dados em Oracle
                        //            Não implementado
                        //=========================================
                        case 5:
                            MessageBox.Show("Esta versão de software ainda não possui suporte para Bancos de dados Oracle");
                            /*CP.lblInfoProjeto.Text = "Conectando com o banco de dados...";
                            CP.prgProgresso.Value = CP.prgProgresso.Value + passos;
                            DataTable DT5 = BDClass.DB_ORACLE();
                            if (DT5.Columns.Count > 0)
                            {
                                int vi = ds.Tables.Count;
                                AddTableDataSet(DT5, vi);
                            }*/
                            break;

                        //=====================================
                        //caso seja um banco de dados diferente
                        //          previsão futura
                        //=====================================
                        default:
                            MessageBox.Show("O tipo de banco de dados informados não é compatível com esta versão do visualizador.");
                            break;

                    }//fim do switch

                    //se o laço for não estiver na primeira volta (i > 0)
                    //isto é, se não estiver na consulta da primeira tabela,
                    // então é necessário juntar duas tabelas
                    if (i > 0)
                    {
                        //declaração dos dados que serão usados na chamada do método
                        Dados dd = new Dados();
                        DataTable dt1 = new DataTable();
                        DataTable dt2 = new DataTable();
                        DataColumn dc1 = new DataColumn();
                        DataColumn dc2 = new DataColumn();

                        //se for a 2ª tabela, será necessário nomear  todas as colunas das duas tabelas
                        //por isso, a necessidade de um método diferenciado
                        if (i == 1)
                        {
                            dt1 = ds.Tables[0];
                            dt1.TableName = strParametros[0, 3];
                            dc1.ColumnName = strParametros[i, 5];

                            dt2 = ds.Tables[1];
                            dt2.TableName = strParametros[i, 3];
                            dc2.ColumnName = strParametros[i, 4];

                            DataTable dtResult = new DataTable();
                            //chama o método de juntar tabelas na classe Dados (dd)
                            dtResult = dd.JuntarTabela(dt1, dc1, strParametros[0, 2], dt2, dc2, strParametros[i, 2]);

                            if (dtResult.Columns.Count > 0)
                            {
                                ds.Tables.Clear();
                                AddTableDataSet(dtResult, 0);

                            }
                        }
                        //entretanto, se for 3ª, 4ª (...) nª tabela, tabela primeira já estará nomeada,
                        //não havendo necessidade de nova nomeação
                        else
                        {
                            if (ds.Tables.Count > 1)
                            {
                                dt1 = ds.Tables[0];
                                //Monta o nome da coluna [NomeColuna]_[NomeTabela]
                                dc1.ColumnName = strParametros[i, 5] + "_" + strParametros[0, 2];

                                dt2 = ds.Tables[1];
                                dt2.TableName = strParametros[i, 3];
                                dc2.ColumnName = strParametros[i, 4];


                                DataTable dtResult = new DataTable();
                                //chama o método de juntar tabelas na classe Dados (dd)
                                dtResult = dd.JuntarTabela2(dt1, dc1, dt2, dc2, strParametros[i, 2]);

                                if (dtResult.Columns.Count > 0)
                                {
                                    //se o resultado for uma uma tabela com pelo menos 1 coluna
                                    //limpa-se o dataset e adiciona-se a tabela
                                    ds.Tables.Clear();
                                    AddTableDataSet(dtResult, 0);
                                }
                            }
                        }
                    }
                }//aqui termina o laço for, de cada consulta ao BD

                dtTabela = ds.Tables[0];

                progresso = 80;
                textoProgresso = "Base completa.";
                barraProgresso();

                //manda parametros adicionais aos formulários de pesquisa, para que ele possa fazer pesquisas
                //e montar relatórios
                strPlus += "#" + strParametros[0, 5] + "_" + strParametros[0, 2]
                         + "#" + strParametros[0, 4] + "_" + strParametros[0, 2]
                         + "#" + PathProjeto;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
    Ex.Message);
            }

        }
        
        /// <summary>
        /// <para>testa se o novo caminho já não está na lista, se o caminho já estiver, ele reordena</para>
        /// </summary>
        /// <param name="CaminhoNovo">Novo caminho a ser adicionado ao menu</param>
        private void ConfigPath(string CaminhoNovo)
        {
            //LOG dispensado (valor insignificante)

            try
            {
                Config set = new Config();
                //testa se o novo caminho já não está na lista
                //se o caminho já estiver, ele reordena -
                //A importancia do reordenamento está na substituiçao do mais antigo pelo mais novo
                //Faca nos dentes e POG na cabeça!
                if (CaminhoNovo == set.Caminho2)
                {
                    set.Caminho2 = set.Caminho1;
                    set.Caminho1 = CaminhoNovo;
                }
                else if (CaminhoNovo == set.Caminho3)
                {
                    set.Caminho3 = set.Caminho2;
                    set.Caminho2 = set.Caminho1;
                    set.Caminho1 = CaminhoNovo;
                }
                else if (CaminhoNovo == set.Caminho4)
                {
                    set.Caminho4 = set.Caminho3;
                    set.Caminho3 = set.Caminho2;
                    set.Caminho2 = set.Caminho1;
                    set.Caminho1 = CaminhoNovo;
                }
                else if (CaminhoNovo == set.Caminho5)
                {
                    set.Caminho5 = set.Caminho4;
                    set.Caminho4 = set.Caminho3;
                    set.Caminho3 = set.Caminho2;
                    set.Caminho2 = set.Caminho1;
                    set.Caminho1 = CaminhoNovo;
                }

                else if (CaminhoNovo != set.Caminho1 & CaminhoNovo != set.Caminho2 &
                    CaminhoNovo != set.Caminho3 & CaminhoNovo != set.Caminho4 &
                    CaminhoNovo != set.Caminho5)
                {
                    set.Caminho5 = set.Caminho4;
                    set.Caminho4 = set.Caminho3;
                    set.Caminho3 = set.Caminho2;
                    set.Caminho2 = set.Caminho1;
                    set.Caminho1 = CaminhoNovo;
                    set.Save();
                }
                MenuProjetosZON();
            }
            catch
            {

            }
        }

        /// <summary>
        /// obtém o nome da unidade em português
        /// </summary>
        /// <param name="Unidades">tipo de unidade de medida em sriARUnits</param>
        /// <returns>Retorna a unidade de medida por extenso e em português</returns>
        private string ConversorEsriArUnits(esriARUnits Unidades)
        {
            /*
             * Converte as unidades da Esri (esriARUnits) como o 'esriARUnitsMeters'
             * para unidades amigáveis, como "Metro"
             * LOG dispensado (valor insignificante)
             */

            string strRecebe = "";
            try
            {
                switch (Unidades)
                {
                    case esriARUnits.esriARUnitsUnknown:
                        strRecebe = "Desconhecido";
                        break;

                    case esriARUnits.esriARUnitsInches:
                        strRecebe = "Polegada";
                        break;

                    case esriARUnits.esriARUnitsPoints:
                        strRecebe = "Ponto";
                        break;

                    case esriARUnits.esriARUnitsFeet:
                        strRecebe = "Pé";
                        break;

                    case esriARUnits.esriARUnitsYards:
                        strRecebe = "Braça";
                        break;

                    case esriARUnits.esriARUnitsMiles:
                        strRecebe = "Milha";
                        break;

                    case esriARUnits.esriARUnitsNauticalMiles:
                        strRecebe = "Milha nautica";
                        break;

                    case esriARUnits.esriARUnitsMillimeters:
                        strRecebe = "Milímetro";
                        break;

                    case esriARUnits.esriARUnitsCentimeters:
                        strRecebe = "Centímetro";
                        break;

                    case esriARUnits.esriARUnitsMeters:
                        strRecebe = "Metro";
                        break;

                    case esriARUnits.esriARUnitsKilometers:
                        strRecebe = "Quilômetro";
                        break;

                    case esriARUnits.esriARUnitsDecimalDegrees:
                        strRecebe = "Grau decimal";
                        break;

                    case esriARUnits.esriARUnitsDecimeters:
                        strRecebe = "Decímetro";
                        break;

                    case esriARUnits.esriARUnitsDegMinSec:
                        strRecebe = "Grau, minuto e segundo";
                        break;
                }
            }
            catch
            {

            }

            return strRecebe;

        }
        
        /// <summary>
        /// Encurta o caminho para inseri-lo no menu arquivo, nos links rapidos
        /// </summary>
        /// <param name="PathLongo">caminho completo até o arquivo de zoneamento</param>
        /// <returns>retorna a string encurtada</returns>
        private string EncurtaPath(string PathLongo)
        {
            //LOG dispensado (valor insignificante)

            string PathCurto = "";

            try
            {
                int PathLength = PathLongo.Length;

                if (PathLength > 35)
                {
                    string strTmp001 = PathLongo.Substring(0, 10);
                    string strTmp002 = PathLongo.Substring(PathLength - 20, 20);

                    PathCurto = strTmp001 + "(...)" + strTmp002;
                }
                else
                {
                    PathCurto = PathLongo;
                }
            }
            catch
            {

            }

            return PathCurto;
        }
        
        /// <summary>
        /// Executa a abertura do arquivo Zon, diretamente via caminho ate' o arquivo zon
        /// </summary>
        /// <param name="Caminho">caminho completo ate' o arquivo zon</param>
        private bool ExecutaAbertura(string Caminho)
        {
            bool boolAbriu = false;
            try
            {
                
                if (arcVerZon.DocumentFilename == null)
                {
                    //Executa a leitura do arquivo *.zon 
                    string StrTextBox;
                    string strPMFPath;
                    Enigma enig = new Enigma();

                    StrTextBox = enig.Descriptografa(Caminho); //decodifica o arquivo
                    if (StrTextBox != "")
                    {
                        try
                        {
                            progresso = 5;
                            textoProgresso = "Abrindo projeto..";


                            //faz a quebra das substrings para a variavel Parametros
                            StrTextBox = StrTextBox.Replace("\n", "");
                            Parametros = StrTextBox.Split('\r');

                            //================ versão =====================
                            try
                            {
                                string[] strVersao = Parametros[4].Split('.');

                                decimal denominador = 1;
                                if (strVersao.Length > 1)
                                {
                                    for (int a = 0; a < strVersao[1].Length; a++)
                                    {
                                        denominador = denominador * 10;
                                    }
                                }
                                decimal numerador = Decimal.Parse(strVersao[0]);
                                decimal denominador2 = Decimal.Parse(strVersao[1]) / denominador;
                                double dblVersao = Double.Parse(numerador.ToString()) + Double.Parse(denominador2.ToString());




                                //double versao = Int32.Parse(Parametros[4]);

                                string PathXML = "";
                                if (Parametros[17] != null)
                                {
                                    PathXML = Parametros[17];
                                    //verifica versão do projeto
                                    bool Verificou = verificaVersao(PathXML, dblVersao);
                                    if (Verificou)
                                    {
                                        progresso = 0;
                                        Parametros = null;
                                        textoProgresso = "";
                                        boolAbriu = true;
                                        return boolAbriu;
                                    }
                                }
                            }
                            catch
                            { }
                            //================ versão =====================

                            PathProjeto = Path.GetDirectoryName(Caminho);
                            //string[] strTmp003 = Caminho.Split('\\');
                            //for (int i3 = 0; i3 < strTmp003.Length - 1; i3++)
                            //{ PathProjeto = PathProjeto + strTmp003[i3] + "\\"; }

                            //salva o caminho do path para ser usado na próxima execução.
                            ConfigPath(Caminho);
                            //carrega o H-Zão do projeto - capa com a imagem logo do projeto
                            frmCapa capa = new frmCapa();
                            capa.picLogo.ImageLocation = PathProjeto + "\\" + Parametros[7];
                            capa.picLogo.Load();
                            capa.Show(); //justo aqui, não funciona... resolvi um problema, criei outro...


                            //carrega bancos de dados
                            string strPSQ = PathProjeto + "\\" + Parametros[2];
                            //faz a descritografia do arquivo de parametros.
                            StrTextBox = enig.Descriptografa(strPSQ);

                                strPlus = Parametros[10]
                                    + "#" + Parametros[12]
                                    + "#" + Parametros[14]
                                    + "#" + Parametros[15]
                                    + "#" + Parametros[16];

 

                            CarregaBD(StrTextBox, strPSQ);

                            strPlus += "#" + Parametros[0]
                                    + "#" + Parametros[3];

                            progresso = 90;
                            textoProgresso = "Carregando mapas.";
                            barraProgresso();

                            //carrega o projeto visual
                            string strTitulo = Parametros[0] + " - [EMBRAPA SOLOS] - ViZon";
                            this.Text = strTitulo;

                            //principal.Refresh();
                            strPMFPath = PathProjeto + "\\" + Parametros[1];
                            arcVerZon.LoadDocument(strPMFPath, Parametros[8]);

                            //carrega detalhes e perfumarias
                            ARMap map = arcVerZon.ARPageLayout.FocusARMap;
                            sslMapSisRef.Text = map.SpatialReferenceName.ToString();
                            sslMapUnidade.Text = ConversorEsriArUnits(map.MapUnits);
                            sslLayoutUnidade.Text = ConversorEsriArUnits(arcVerZon.ARPageLayout.PageUnits);

                            //Retira as travas para crianças, carrega os PIs p/ swipe e escala
                            TravaSeguranca(true);
                            //EncheComboBoxLayers();
                            AtualizaEscala();

                            //guarda os valores de minimos e maximos e a escala
                            arcVerZon.ARPageLayout.FocusARMap.GetExtent(ref O_XMin, ref O_YMin, ref O_XMax, ref  O_YMax);
                            O_Escala = arcVerZon.ARPageLayout.FocusARMap.MapScale;

                            //arcVerZon.ARPageLayout.FocusARMap.GetExtent(ref AE_XMin, ref AE_YMin, ref AE_XMax, ref  AE_YMax);
                            //AE_Escala = arcVerZon.ARPageLayout.FocusARMap.MapScale;

                            //fecha o H-Zão
                            capa.Close();
                            boolAbriu = true;



                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Antes de abrir um outro projeto, é necessário fechar o projeto atual.");
                }
            }
            catch
            {

            }
            progresso = 0;
            textoProgresso = "Pronto.";
            barraProgresso();
            return boolAbriu;
        }

        /// <summary>
        /// Decompõe o parametro de posição do shapefile e retorna o shape de pesquisa
        /// </summary>
        /// <param name="Shape">parametros com a posição do shape de pesquisa</param>
        /// <returns>Retorna um shapefile ou grupo de shapes</returns>
        private IARLayer pegaPI(string Shape)
        {
            string[] strColunasLeier = Shape.Split('*');
            IARLayer Leier = null;

            for (int a = 0; a < strColunasLeier.Length-1; a++)
            {
                int Posicao = Int32.Parse(strColunasLeier[a]);
                if (a == 0)
                {
                    Leier = arcVerZon.ARPageLayout.FocusARMap.get_ARLayer(Posicao);
                }
                else
                {
                    Leier = Leier.get_ChildARLayer(Posicao);
                }
            }
            return Leier;

        }

        /// <summary>
        /// executa a seleção dos polígonos encontrados pela janela de pesquisa
        /// </summary>
        /// <param name="comando">sintaxe com a seleção dos poligonos</param>
        public void ExecutaSelecao(string comando)
        {
            try
            {
                string[] strComando = comando.Split('*');
                Config set = new Config();
                int Cor = ColorTranslator.ToOle(set.Cor);
                ArcReaderSearchDef arcReaderSearchDef = new ArcReaderSearchDefClass();
                IARLayer arLayer = pegaPI(Parametros[9]);

                progresso = 5;
                textoProgresso = "Selecionando áreas...";
                barraProgresso();

                //IARSearchDef arSearchDef = new ArcReaderSearchDefClass();

                if (strComando[0] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[0];
                    FeicoesPesquisa1 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    FeicoesPesquisa1.Highlight(true, Cor);
                    progresso = 25;
                    textoProgresso = "Selecionando áreas...";
                    barraProgresso();
                }

                if (strComando[1] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[1];
                    FeicoesPesquisa2 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    FeicoesPesquisa2.Highlight(true, Cor);
                    progresso = 45;
                    textoProgresso = "Selecionando áreas...";
                    barraProgresso();
                }

                if (strComando[2] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[2];
                    FeicoesPesquisa3 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    FeicoesPesquisa3.Highlight(true, Cor);
                    progresso = 70;
                    textoProgresso = "Selecionando áreas...";
                    barraProgresso();
                }
                if (strComando[3] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[3];
                    FeicoesPesquisa4 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    FeicoesPesquisa4.Highlight(true, Cor);
                    progresso = 95;
                    textoProgresso = "Selecionando áreas...";
                    barraProgresso();
                }

                if (strComando[4] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[4];
                    FeicoesPesquisa5 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    FeicoesPesquisa5.Highlight(true, Cor);
                    progresso = 0;
                    textoProgresso = "Pronto.";
                    barraProgresso();
                }

                EhPesquisa = true;
                TravaPesquisa();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
    Ex.Message);
            }
            progresso = 0;
            textoProgresso = "Pronto.";
            barraProgresso();
            arcVerZon.ARPageLayout.Refresh(true);
        }

        /// <summary>
        /// Executa a seleção da area de estudo
        /// </summary>
        /// <param name="comando">string com a sintaxe de seleção</param>
        /// <param name="tabela">tabela que retorna da pesquisa, e serve para restringir futuras buscas</param>
        public void ExecutaAreaEstudo(string comando, DataTable tabela)
        {
            try
            {
                //comando = comando.Replace("=", "<>");
                //comando = comando.Replace("OR", "AND");

                // ====== nova redação =====
                progresso = 5;
                textoProgresso = "Definindo área de estudo...";

                string[] strComando = comando.Split('*');
                Config set = new Config();
                int Cor = ColorTranslator.ToOle(set.Cor);
                ArcReaderSearchDef arcReaderSearchDef = new ArcReaderSearchDefClass();
                IARLayer arLayer = pegaPI(Parametros[11]);

                if (strComando[0] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[0];
                    
                    m_arFeatureAreaEstudo1 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    m_arFeatureAreaEstudo1.Highlight(true, Int32.Parse(Parametros[13]));
                    progresso = 25;
                }

                if (strComando[1] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[1];
                    m_arFeatureAreaEstudo2 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    m_arFeatureAreaEstudo2.Highlight(true, Int32.Parse(Parametros[13]));
                    progresso = 45;
                }

                if (strComando[2] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[2];
                    m_arFeatureAreaEstudo3 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    m_arFeatureAreaEstudo3.Highlight(true, Int32.Parse(Parametros[13]));
                    progresso = 75;
                }
                if (strComando[3] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[3];
                    m_arFeatureAreaEstudo4 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    m_arFeatureAreaEstudo4.Highlight(true, Int32.Parse(Parametros[13]));
                    progresso = 95;
                }

                if (strComando[4] != "")
                {
                    arcReaderSearchDef.WhereClause = strComando[4];
                    m_arFeatureAreaEstudo5 = arLayer.QueryARFeatures(arcReaderSearchDef);
                    m_arFeatureAreaEstudo5.Highlight(true, Int32.Parse(Parametros[13]));
                }
                //Não deu pra 'dar zoom' na area de pesquisa. :/
                // ========================
                

                //salvo a tabela, para que as próximas pesquisas sejam feitas somente na area de estudo.
                dtTabela = tabela;
                EhAreaEstudo = true;
                TravaAreaEstudo();
            }
            catch
            {

            }
            progresso = 0;
            textoProgresso = "Pronto.";
        }

        //ainda nao feito (não sei se vai entrar)
        /// <summary>
        /// Limita a navegação dentro do espaço do projeto
        /// </summary>
        private void LimitaArea(double xMin,double yMin,double xMax,double yMax, double diaPerc)
        {

            try
            {
                //corrige se o problema for a escala
                if (arcVerZon.ARPageLayout.FocusARMap.MapScale > AE_Escala)
                {
                    arcVerZon.ARPageLayout.FocusARMap.MapScale = AE_Escala;
                    arcVerZon.ARPageLayout.FocusARMap.SetExtent(AE_XMin, AE_YMin, AE_XMax, AE_YMax);
                }
                else //corrige se a o problema for o posicionamento
                {
                    //faz a análise de xMin
                    if (xMin + diaPerc < AE_XMin)
                    {
                        double TempDbl = xMin - (AE_XMin);

                        xMin -= TempDbl;
                        xMax -= TempDbl;
                    }
                    //faz a análise de xMax
                    if (xMax - diaPerc > AE_XMax)
                    {
                        double TempDbl = xMax - (AE_XMax);
                        xMax -= TempDbl;
                        xMin -= TempDbl;
                    }
                    //faz a análise de yMin
                    if (yMin + diaPerc < AE_YMin)
                    {
                        double TempDbl = yMin - (AE_YMin);
                        yMin -= TempDbl;
                        yMax -= TempDbl;
                    }
                    //faz a análise de yMax
                    if (yMax - diaPerc > AE_YMax)
                    {
                        double TempDbl = yMax - (AE_YMax);
                        yMax -= TempDbl;
                        yMin -= TempDbl;
                    }
                    arcVerZon.ARPageLayout.FocusARMap.SetExtent(xMin, yMin, xMax, yMax);
                }
                arcVerZon.ARPageLayout.FocusARMap.Refresh(true);
            }
            catch
            {

            }
        }
      
        
        /// <summary>
        /// Limpa a area de estudo
        /// </summary>
        private void LimpaAreaEstudo()
        {
            try
            {
                
                progresso = 5;
                textoProgresso = "Apagando áreas selecionadas...";
                AE_XMin = O_XMin;
                AE_YMin = O_YMin;
                AE_XMax = O_XMax;
                AE_YMax = O_YMax;
                AE_Escala = O_Escala;
                dtTabela = ds.Tables[0];

                if (m_arFeatureAreaEstudo1 != null)
                {
                    m_arFeatureAreaEstudo1.Highlight(false, 0);
                    m_arFeatureAreaEstudo1 = null;
                    progresso = 25;
                    textoProgresso = "Apagando áreas selecionadas...";
                    if (m_arFeatureAreaEstudo2 != null)
                    {
                        m_arFeatureAreaEstudo2.Highlight(false, 0);
                        m_arFeatureAreaEstudo2 = null;
                        progresso = 50;
                        textoProgresso = "Apagando áreas selecionadas...";
                        if (m_arFeatureAreaEstudo3 != null)
                        {
                            m_arFeatureAreaEstudo3.Highlight(false, 0);
                            m_arFeatureAreaEstudo3 = null;
                            progresso = 75;
                            textoProgresso = "Apagando áreas selecionadas...";
                            if (m_arFeatureAreaEstudo4 != null)
                            {
                                m_arFeatureAreaEstudo4.Highlight(false, 0);
                                m_arFeatureAreaEstudo4 = null;
                                progresso = 85;
                                textoProgresso = "Apagando áreas selecionadas...";
                                if (m_arFeatureAreaEstudo5 != null)
                                {
                                    m_arFeatureAreaEstudo5.Highlight(false, 0);
                                    m_arFeatureAreaEstudo5 = null;
                                }
                            }
                        }
                    }

                    EhAreaEstudo = false;
                    TravaAreaEstudo();
                }


            }
            catch
            {

            }
            progresso = 0; //a passagem aqui é mto rapida
            textoProgresso = "Pronto.";
            arcVerZon.ARPageLayout.Refresh(true);
 
            //=================================================================================
        }
       
        /// <summary>
        /// remove a seleção
        /// </summary>
        private void LimpaSelecao()
        {
            try
            {
                progresso = 5;
                textoProgresso = "Apagando áreas selecionadas...";
                if (FeicoesPesquisa1 != null)
                {
                    FeicoesPesquisa1.Highlight(false, 0);
                    FeicoesPesquisa1 = null;
                    progresso = 25;
                    textoProgresso = "Apagando áreas selecionadas...";
                    if (FeicoesPesquisa2 != null)
                    {
                        FeicoesPesquisa2.Highlight(false, 0);
                        FeicoesPesquisa2 = null;
                        progresso = 50;
                        textoProgresso = "Apagando áreas selecionadas...";
                        if (FeicoesPesquisa3 != null)
                        {
                            FeicoesPesquisa3.Highlight(false, 0);
                            FeicoesPesquisa3 = null;
                            progresso = 75;
                            textoProgresso = "Apagando áreas selecionadas...";
                            if (FeicoesPesquisa4 != null)
                            {
                                FeicoesPesquisa4.Highlight(false, 0);
                                FeicoesPesquisa4 = null;
                                progresso = 85;
                                textoProgresso = "Apagando áreas selecionadas...";
                                if (FeicoesPesquisa5 != null)
                                {
                                    FeicoesPesquisa5.Highlight(false, 0);
                                    FeicoesPesquisa5 = null;
                                }
                            }
                        }
                    }
                    
                    EhPesquisa = false;
                    TravaPesquisa();
                }
                

            }
            catch
            {

            }
            progresso = 0; //a passagem aqui é mto rapida
            textoProgresso = "Pronto.";
            arcVerZon.ARPageLayout.Refresh(true);
        }
        /*
        /// <summary>
        /// Faz a limpeza do mapa temático
        /// </summary>
        private void LimpaMapaTematico()
        {
            //pgbBarraProgresso.Value = 10;
            try
            {
                Color CorSistema = new Color();
                CorSistema = System.Drawing.SystemColors.Control;

                if (nivel1)
                {
                    m_arFeatureSetMeets1.Highlight(false, 0);
                    tsmN2.BackColor = CorSistema;
                    nivel1 = false;
                }
                //pgbBarraProgresso.Value = 20;

                if (nivel2)
                {
                    m_arFeatureSetMeets2.Highlight(false, 0);
                    nivel2 = false;
                    tsmN2.BackColor = CorSistema;
                }
                //pgbBarraProgresso.Value = 30;

                if (nivel3)
                {
                    m_arFeatureSetMeets3.Highlight(false, 0);
                    nivel3 = false;
                    tsmN3.BackColor = CorSistema;
                }
                //pgbBarraProgresso.Value = 40;

                if (nivel4)
                {
                    m_arFeatureSetMeets4.Highlight(false, 0);
                    nivel4 = false;
                    tsmN4.BackColor = CorSistema;
                }
                //pgbBarraProgresso.Value = 50;

                if (nivel5)
                {
                    m_arFeatureSetMeets5.Highlight(false, 0);
                    nivel5 = false;
                    tsmN5.BackColor = CorSistema;
                }
                //pgbBarraProgresso.Value = 60;

                if (nivel6)
                {
                    m_arFeatureSetMeets6.Highlight(false, 0);
                    nivel6 = false;
                    tsmN6.BackColor = CorSistema;
                }
                //pgbBarraProgresso.Value = 70;

                if (nivel7)
                {
                    m_arFeatureSetMeets7.Highlight(false, 0);
                    nivel7 = false;
                    tsmN7.BackColor = CorSistema;
                }
                //pgbBarraProgresso.Value = 90;

                TravaMapaTematico();
            }
            catch
            {

            }
        }*/
        
        /// <summary>
        /// carrega a lista de projetos vistos no software
        /// </summary>
        private void MenuProjetosZON()
        {
            //LOG dispensado (valor insignificante)

            try
            {
                Config set = new Config();
                tsmArq1.Text = EncurtaPath(set.Caminho1);
                tsmArq2.Text = EncurtaPath(set.Caminho2);
                tsmArq3.Text = EncurtaPath(set.Caminho3);
                tsmArq4.Text = EncurtaPath(set.Caminho4);
                tsmArq5.Text = EncurtaPath(set.Caminho5);

                if (tsmArq1.Text == "")
                { tsmArq1.Visible = false; }
                else
                {
                    tsmArq1.Visible = true;
                    tsmBarra.Visible = true;
                }

                if (tsmArq2.Text == "")
                { tsmArq2.Visible = false; }
                else
                { tsmArq2.Visible = true; }

                if (tsmArq3.Text == "")
                { tsmArq3.Visible = false; }
                else
                { tsmArq3.Visible = true; }

                if (tsmArq4.Text == "")
                { tsmArq4.Visible = false; }
                else
                { tsmArq4.Visible = true; }

                if (tsmArq5.Text == "")
                { tsmArq5.Visible = false; }
                else
                { tsmArq5.Visible = true; }
            }
            catch
            {

            }
        }
        
        /// <summary>
        /// <para>faz o travamento e o destravamento das funcionalidades e ferramentas em função do estado de carregamento do projeto</para>
        /// </summary>
        /// <param name="boolProj">variavel que controla o liga e desliga</param>
        private void TravaSeguranca(bool boolProj)
        {
            try
            {
                tsbInformacao.Enabled = boolProj;
                tsbLZI.Enabled = boolProj;
                tsbHiperlink.Enabled = boolProj;
                tsbMapZoomIn.Enabled = boolProj;
                tsbMapZoomOut.Enabled = boolProj;

                tsbMapFull.Enabled = boolProj;
                tsbLZO.Enabled = boolProj;
                tsbLayoutZoomIn.Enabled = boolProj;
                tsbLayoutZoomOut.Enabled = boolProj;
                tsbMedicao.Enabled = boolProj;
                tsbAdicionaPonto.Enabled = boolProj;
                ddbMarcador.Enabled = boolProj;
                tsbMapPan.Enabled = boolProj;
                ddbBorracha.Enabled = boolProj;
                tsbMapFull.Enabled = boolProj;
                tsbImprimir.Enabled = boolProj;
                tsbConfImpressao.Enabled = boolProj;
                tsbLayoutPan.Enabled = boolProj;
                tsbEscala11.Enabled = boolProj;
                tsbMZI.Enabled = boolProj;
                tsbMZO.Enabled = boolProj;
                tsbLayoutFull.Enabled = boolProj;
                tscEscala.Enabled = boolProj;
                ddbCaneta.Enabled = boolProj;
                tsbLupa.Enabled = boolProj;

                tsmPesqBasica.Enabled = boolProj;
                tsmPesqAvancada.Enabled = boolProj;
                tsmConfImpressao.Enabled = boolProj;
                tsmImprimir.Enabled = boolProj;
                tsmExpMap.Enabled = boolProj;
                //tsmRecarrega.Enabled = boolProj;
                tsmFecha.Enabled = boolProj;
                tsmCopiarMapa.Enabled = boolProj;
                tsmInformacao.Enabled = boolProj;
                tsmMedicao.Enabled = boolProj;
                tsmAdicionaPonto.Enabled = boolProj;
                tsmHiperlink.Enabled = boolProj;
                tsmCaneta1.Enabled = boolProj;
                tsmMarcador1.Enabled = boolProj;
                tsmBorracha1.Enabled = boolProj;
                // apenas para que o marcador, caneta e borracha tenham a mesma
                // imagem da ferramenta padrão (caneta padrão, borracha padrão, marcador padrão)
                ddbMarcador.Image = tsmMarcadorAmarelo.Image;
                ddbBorracha.Image = tsmBorrachaPequena.Image;
                ddbCaneta.Image = tsmCanetaFina.Image;
                //
                tsmAjudaProj.Enabled = boolProj;

                //tsbLimpaVetor.Enabled = boolProj;
                tsbVetoriza.Enabled = boolProj;
                //tsbSalvaVetor.Enabled = boolProj;

                if (boolProj)
                {
                    TravaView();
                }
                else
                {
                    tsbSwipe.Enabled = false;
                    tsbLayoutAntView.Enabled = false;
                    tsbLayoutFull.Enabled = false;
                    tsbLayoutPan.Enabled = false;
                    tsbLayoutProxView.Enabled = false;
                    tsbLayoutZoomIn.Enabled = false;
                    tsbLayoutZoomOut.Enabled = false;
                    tsbLZI.Enabled = false;
                    tsbLZO.Enabled = false;
                }
            }
            catch
            {

            }
        }
        
        /// <summary>
        /// faz a verificação da possibilidade de movimentação nas vizualizações bloqueando e desbloqueando as funcionalidades evitando erros
        /// </summary>
        private void TravaMovZoom()
        {
            /*
             * dispensado de log
             */
            try
            {
                tsbMapProxView.Enabled = arcVerZon.ARPageLayout.FocusARMap.CanRedoExtent;
                tsbMapAntView.Enabled = arcVerZon.ARPageLayout.FocusARMap.CanUndoExtent;
                tsbLayoutProxView.Enabled = arcVerZon.ARPageLayout.CanRedoExtent;
                tsbLayoutAntView.Enabled = arcVerZon.ARPageLayout.CanUndoExtent;
            }
            catch
            {

            }
            //TravaView();
        }
        
        /// <summary>
        /// faz o travamento e o destravamento das funcionalidades e ferramentas em função do tipo da janela exibida (data ou layout)
        /// </summary>
        private void TravaView()
        {
            /*
             * dispensado de log
             */
            try
            {
                if (arcVerZon.CurrentViewType == esriARViewType.esriARViewTypeMap)
                {
                    tsbSwipe.Enabled = true;

                    tsbLayoutAntView.Enabled = false;
                    tsbLayoutFull.Enabled = false;
                    tsbLayoutPan.Enabled = false;
                    tsbLayoutProxView.Enabled = false;
                    tsbLayoutZoomIn.Enabled = false;
                    tsbLayoutZoomOut.Enabled = false;
                    tsbLZI.Enabled = false;
                    tsbLZO.Enabled = false;
                    tsbEscala11.Enabled = false;
                    TravaMovZoom();
                }
                else if (arcVerZon.CurrentViewType == esriARViewType.esriARViewTypePageLayout)
                {
                    tsbSwipe.Enabled = false;

                    tsbEscala11.Enabled = true;
                    tsbLayoutAntView.Enabled = true;
                    tsbLayoutFull.Enabled = true;
                    tsbLayoutPan.Enabled = true;
                    tsbLayoutProxView.Enabled = true;
                    tsbLayoutZoomIn.Enabled = true;
                    tsbLayoutZoomOut.Enabled = true;
                    tsbLZI.Enabled = true;
                    tsbLZO.Enabled = true;
                    TravaMovZoom();
                }
                else if (arcVerZon.CurrentViewType == esriARViewType.esriARViewTypeNone)
                {
                    tsbSwipe.Enabled = false;

                    tsbLayoutAntView.Enabled = false;
                    tsbLayoutFull.Enabled = false;
                    tsbLayoutPan.Enabled = false;
                    tsbLayoutProxView.Enabled = false;
                    tsbLayoutZoomIn.Enabled = false;
                    tsbLayoutZoomOut.Enabled = false;
                    tsbLZI.Enabled = false;
                    tsbLZO.Enabled = false;
                    TravaMovZoom();
                }
            }
            catch
            {

            }
        }

        
        /// <summary>
        /// faz o travamento das funcionalidades relacionadas a pesquisa, evitando a execução fora do contexto
        /// </summary>
        /// <param name="pesquisa">verifica se existe uma pesquisa feita</param>
        private void TravaPesquisa()
        {

            try
            {
                tsmLimparSelecao.Enabled = EhPesquisa;
                //tsmN1.Enabled = EhPesquisa;
                //tsmN2.Enabled = EhPesquisa;
                //tsmN3.Enabled = EhPesquisa;
                //tsmN4.Enabled = EhPesquisa;
                //tsmN5.Enabled = EhPesquisa;
                //tsmN6.Enabled = EhPesquisa;
                //tsmN7.Enabled = EhPesquisa;
            }
            catch
            {

            }
        }
        
        /// <summary>
        /// faz o travamento das funcionalidades relacionadas a pesquisa, evitando a execução fora do contexto
        /// </summary>
        private void TravaAreaEstudo()
        {

            try
            {
                tsmLimparAreaEstudo.Enabled = EhAreaEstudo;
            }
            catch
            {

            }
        }

        /// <summary>
        /// faz o travamento dos comandos relacionados ao mapa temático
        /// </summary>
        /* private void TravaMapaTematico()
         {

             try
             {
                 if (nivel1 == false &
                     nivel2 == false &
                     nivel3 == false &
                     nivel4 == false &
                     nivel5 == false &
                     nivel6 == false &
                     nivel7 == false)
                 {
                     tsmLimparMapaTematico.Enabled = false;
                 }
                 else
                 {
                     tsmLimparMapaTematico.Enabled = true;
                 }
             }
             catch
             {

             }
         }*/

        /// <summary>
        /// Verifica se o projeto/software executa a versão mais atual, baixando as informações de um 
        /// arquivo xml em um servidor.
        /// </summary>
        /// <param name="enderecoXML"> endereço onde se encontra o arquivo xml</param>
        /// <param name="versaoProj">versao do projeto em execução</param>
        private bool verificaVersao(string enderecoXML, double versaoProj)
        {
            bool resposta = false;
            try
            {
                
                DataSet ds = new DataSet();
                ds.ReadXml(enderecoXML);
                decimal decVersaoProj = Decimal.Parse(versaoProj.ToString());
                string[] strVersao = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString().Split('.');

                decimal denominador = 1;
                if (strVersao.Length > 1)
                {
                    for (int a = 0; a < strVersao[1].Length; a++)
                    {
                        denominador = denominador * 10;
                    }
                }
                decimal numerador = Decimal.Parse(strVersao[0]);
                decimal denominador2 = Decimal.Parse(strVersao[1]) / denominador;
                decimal dblVersao = numerador + denominador2;
                

            if (dblVersao > decVersaoProj) //aqui o valor é convertido pra 175 quando o computador usa , como separador decimal.
                {
                    string titulo = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    string mensagem = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                    string strOpcaoMSG = ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    bool boolOpcaoMSG = Boolean.Parse(strOpcaoMSG);

                    if (boolOpcaoMSG)
                    {

                        if (DialogResult.Yes == MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo))
                        {
                            string strLink = ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                            System.Diagnostics.Process.Start(strLink);
                            resposta = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show(mensagem, titulo);
                    }

                }
            }
            catch
            { }
            return resposta;
        }

        /// <summary>
        /// Faz o H-Zão pro usuário não pensar que tá tudo travado.
        /// </summary>
        private void barraProgresso()
        {
            sslProgresso.Value = progresso;            
            sslExecutando.Text = textoProgresso;   
        }

        //=========================================
        //             Propriedades
        //=========================================
        /// <summary>
        /// GetSet para pesquisa Basica 
        /// </summary>
        public DataTable prpPrincipal //GetSet para pesquisa Basica 
        {
            get { return this.dtTabela; }
            set { this.dtTabela = value; }
        }
       
        /// <summary>
        /// Propriedades para pesquisa Basica
        /// </summary>
        public string prpStrBasicaP
        {
            get { return this.strParPesq; }
            set { this.strParPesq = value; }
        }
        
        /// <summary>
        /// Não sei pra que serve.
        /// </summary>
        public string prpAdd
        {
            get { return this.strPlus; }
            set { this.strPlus = value; }
        }


        //=========================================
        //              Eventos menus
        //=========================================
        //Arquivo
        private void tsmRecarrega_Click(object sender, EventArgs e)
        {
            Application.Restart();
            // tem q ter mais coisa aqui!
        }
        private void tsmFecha_Click(object sender, EventArgs e)
        {
            Application.Restart();
            //FechaProjeto();
        }        
        private void tsmAbrir_Click(object sender, EventArgs e)
        {
            AbrirArquivo();
        }
        private void tsmExpMap_Click(object sender, EventArgs e)
        {
            SaveFileDialog sDialog = new SaveFileDialog();
            sDialog.Title = "Salvar mapa";
            sDialog.Filter = "Arquivo bmp|*.bmp";
            sDialog.InitialDirectory = @"C:\Geoprocessamento\Visualizador\aprendizado\Proj_exemplo\proj009_zaal001";

            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                arcVerZon.ExportView(esriARExportFormat.esriARExportFormatBMP, sDialog.FileName);

            }
        }
        private void tsmConfImpressao_Click(object sender, EventArgs e)
        {
            //abre janela de configurar impressão
            ArcWindows(3);
        }
        private void tsmImprimir_Click(object sender, EventArgs e)
        {
            //abre janela de impressão
            ArcWindows(2);
        }
        private void tsmArq1_Click(object sender, EventArgs e)
        {
            Config set = new Config();
            bool boolAbriu = ExecutaAbertura(set.Caminho1);

            if (boolAbriu == false)
                {
                    string Mensagem = "Um erro ocorreu durante a tentativa de abertura do projeto " + set.Caminho1
                                    + ". O Arquivo pode estar corrompido ou não existir. " +
                                      "Deseja excluir o projeto do menu Arquivo?";

                    if (MessageBox.Show(Mensagem, "Erro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        tsmArq1.Visible = false;
                        set.Caminho1 = "";
                        set.Save();
                    }
                }    
        }
        private void tsmArq2_Click(object sender, EventArgs e)
        {
            Config set = new Config();
            bool boolAbriu = ExecutaAbertura(set.Caminho1);

            if (boolAbriu == false)
            {
                string Mensagem = "Um erro ocorreu durante a tentativa de abertura do projeto " + set.Caminho2
                                + ". O Arquivo pode estar corrompido ou não existir. " +
                                  "Deseja excluir o projeto do menu Arquivo?";

                if (MessageBox.Show(Mensagem, "Erro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    tsmArq2.Visible = false;
                    set.Caminho2 = "";
                    set.Save();
                }
            } 
        }
        private void tsmArq3_Click(object sender, EventArgs e)
        {
            Config set = new Config();
            bool boolAbriu = ExecutaAbertura(set.Caminho1);

            if (boolAbriu == false)
            {
                string Mensagem = "Um erro ocorreu durante a tentativa de abertura do projeto " + set.Caminho1
                                + ". O Arquivo pode estar corrompido ou não existir. " +
                                  "Deseja excluir o projeto do menu Arquivo?";

                if (MessageBox.Show(Mensagem, "Erro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    tsmArq3.Visible = false;
                    set.Caminho3 = "";
                    set.Save();
                }
            } 
        }
        private void tsmArq4_Click(object sender, EventArgs e)
        {
            Config set = new Config();
            bool boolAbriu = ExecutaAbertura(set.Caminho1);

            if (boolAbriu == false)
            {
                string Mensagem = "Um erro ocorreu durante a tentativa de abertura do projeto " + set.Caminho1
                                + ". O Arquivo pode estar corrompido ou não existir. " +
                                  "Deseja excluir o projeto do menu Arquivo?";

                if (MessageBox.Show(Mensagem, "Erro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    tsmArq4.Visible = false;
                    set.Caminho4 = "";
                    set.Save();
                }
            } 
        }
        private void tsmArq5_Click(object sender, EventArgs e)
        {
            Config set = new Config();
            bool boolAbriu = ExecutaAbertura(set.Caminho1);

            if (boolAbriu == false)
            {
                string Mensagem = "Um erro ocorreu durante a tentativa de abertura do projeto " + set.Caminho1
                                + ". O Arquivo pode estar corrompido ou não existir. " +
                                  "Deseja excluir o projeto do menu Arquivo?";

                if (MessageBox.Show(Mensagem, "Erro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    tsmArq5.Visible = false;
                    set.Caminho5 = "";
                    set.Save();
                }
            } 
        }
        private void tsmSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Ferramentas
        private void tsmCanetaMedia2_Click(object sender, EventArgs e)
        {
            //seleciona caneta média
            ArcFerramentas(17);
            ddbCaneta.Image = tsmCanetaMedia.Image;
        }
        private void tsmCanetaFina2_Click(object sender, EventArgs e)
        {
            //seleciona caneta fina
            ArcFerramentas(18);
            ddbCaneta.Image = tsmCanetaFina.Image;
        }
        private void tsmCanetaGrossa2_Click(object sender, EventArgs e)
        {
            //seleciona caneta grossa
            ArcFerramentas(16);
            ddbCaneta.Image = tsmCanetaGrossa.Image;
        }
        private void tsmMarcadorAmarelo2_Click(object sender, EventArgs e)
        {
            // seleciona ferramenta de marcador amarelo
            ArcFerramentas(15);
            ddbMarcador.Image = tsmMarcadorAmarelo.Image;
        }
        private void tsmMarcadorVerde2_Click(object sender, EventArgs e)
        {
            // seleciona ferramenta de marcador verde
            ArcFerramentas(14);
            ddbMarcador.Image = tsmMarcadorVerde.Image;
        }
        private void tsmMarcadorCiano2_Click(object sender, EventArgs e)
        {
            // seleciona ferramenta de marcador ciano
            ArcFerramentas(13);
            ddbMarcador.Image = tsmMarcadorCiano.Image;
        }
        private void tsmBorrachaPequena2_Click(object sender, EventArgs e)
        {

            //seleciona borracha pequena
            ArcFerramentas(20);
            ddbBorracha.Image = tsmBorrachaPequena.Image;
        }
        private void tsmBorrachaMedia2_Click(object sender, EventArgs e)
        {
            //seleciona borracha media
            ArcFerramentas(21);
            ddbBorracha.Image = tsmBorrachaMedia.Image;

        }
        private void tsmBorrachaGrande2_Click(object sender, EventArgs e)
        {
            //seleciona borracha grande
            ArcFerramentas(22);
            ddbBorracha.Image = tsmBorrachaGrande.Image;
        }
        private void tsmBorrachaTraco2_Click(object sender, EventArgs e)
        {
            //seleciona borracha traço
            ArcFerramentas(19);
            ddbBorracha.Image = tsmBorrachaTraco.Image;
        }
        private void tsmHiperlink_Click(object sender, EventArgs e)
        {
            ArcFerramentas(8);
        }
        private void tsmAdicionaPonto_Click(object sender, EventArgs e)
        {
            ArcWindows(4);
        }
        private void tsmMedicao_Click(object sender, EventArgs e)
        {
            ArcFerramentas(7);
        }
        private void tsmInformacao_Click(object sender, EventArgs e)
        {
            ArcFerramentas(3);
            ArcWindows(7);
        }
        //Editar *****
        private void tsmCopiarMapa_Click(object sender, EventArgs e)
        {
            arcVerZon.CopyViewToClipboard();
        }
        //Pesquisa
        private void tsmPesqAvancada_Click(object sender, EventArgs e)
        {
            //tmrRelogio.Enabled = true;
            //abre janela de pesquisa avançada
            //passa a primeira tabela (remontada) para a outra janela, onde será feita a pesquisa
            new Thread(LimpaSelecao).Start();
            //LimpaSelecao();

            frmPesquisaAvancada oPA = new frmPesquisaAvancada();
            oPA.prpStrAvancada = this.prpStrBasicaP;
            oPA.prpAvancada = this.prpPrincipal;
            oPA.prpAdicionais = this.prpAdd;
            oPA.ShowDialog(this);
        }
        private void tsmPesqBasica_Click(object sender, EventArgs e)
        {
            //tmrRelogio.Enabled = true;
            //abre janela de pesquisa básica e passa a tabela para a pesquisa
            new Thread(LimpaSelecao).Start();
            //LimpaSelecao();

            if (dtTabela.Rows.Count == 0)
            {
                dtTabela = ds.Tables[0];
            }
            frmPesquisaBasica oPB = new frmPesquisaBasica();
            oPB.prpStrBasica = this.prpStrBasicaP;
            oPB.prpBasica = this.prpPrincipal;
            oPB.prpAdicionais = this.prpAdd;
            oPB.ShowDialog(this);
        }
        private void tsmLimparSelecao_Click(object sender, EventArgs e)
        {
            //tmrRelogio.Enabled = true;
            LimpaSelecao();
            new Thread(LimpaSelecao).Start();
        }
        private void tsmLimparAreaEstudo_Click(object sender, EventArgs e)
        {
            new Thread(LimpaAreaEstudo).Start();
        }      
        private void tsmAlterarCor_Click(object sender, EventArgs e)
        {
            Config set = new Config();
            ColorDialog cd = new ColorDialog();
            cd.Color = set.Cor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                set.Cor = cd.Color;
                set.Save();
            }
        }
        /* private void tsmN1_Click(object sender, EventArgs e)
        {
            AdicionaNivel(1);
        }
        private void tsmN2_Click(object sender, EventArgs e)
        {
            AdicionaNivel(2);
        }
        private void tsmN3_Click(object sender, EventArgs e)
        {
            AdicionaNivel(3);
        }
        private void tsmN4_Click(object sender, EventArgs e)
        {
            AdicionaNivel(4);
        }
        private void tsmN5_Click(object sender, EventArgs e)
        {
            AdicionaNivel(5);
        }
        private void tsmN6_Click(object sender, EventArgs e)
        {
            AdicionaNivel(6);
        }
        private void tsmN7_Click(object sender, EventArgs e)
        {
            AdicionaNivel(7);
        }
        private void limparMapaTematicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Thread(LimpaMapaTematico).Start();
            LimpaMapaTematico();
        }*/
        //ajuda
        private void tsmSobre_Click(object sender, EventArgs e)
        {
            string Valor1 = "";
            string Valor2 = "";
            if (Parametros[7] != null | Parametros[3] != null)
            {
                Valor1 = PathProjeto + "\\" + Parametros[7];
                Valor2 = Parametros[3].Replace("*", "\r\n");
            }
            frmSobre sobre = new frmSobre(Valor1, Valor2, versao);
            sobre.ShowDialog();
        }
        private void tsmAjudaSoft_Click(object sender, EventArgs e)
        {
            try
            {
                string CaminhoPrograma;
                CaminhoPrograma = Application.StartupPath;
                System.Diagnostics.Process.Start(CaminhoPrograma + "\\ViZon.chm");
                //System.Diagnostics.Process.Start(@"C:\Geoprocessamento\Visualizador\Help\ZonSux.chm");

            }
            catch
            {

            }
        }
        private void tsmAjudaProj_Click(object sender, EventArgs e)
        {
            try
            {
                int intTmp001 = Int32.Parse(Parametros[5]);
                switch (intTmp001)
                {
                    case 1:
                        //arquivo comum (PDF/DOC/TXT)
                        System.Diagnostics.Process.Start(PathProjeto + "\\" + Parametros[6]);
                        break;

                    case 2:
                        //internet
                        System.Diagnostics.Process.Start(Parametros[6]);
                        break;

                    case 3:
                        //arquivo de ajuda 
                        System.Diagnostics.Process.Start(PathProjeto + "\\" + Parametros[6]);
                        break;

                    default:
                        MessageBox.Show("Não foi possível abrir o arquivo de ajuda especificado.");
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }


        //=========================================
        //    Eventos Barra de Ferramentas 1
        //=========================================
        private void tsbAbrir_Click(object sender, EventArgs e)
        {
            //abre o zoneamento
            AbrirArquivo();
        }
        //zoom no mapa
        private void tsbMapZoomIn_Click(object sender, EventArgs e)
        {
            //Seleciona a ferramenta de aumento de zoom
            ArcFerramentas(0);
            AtualizaEscala();
        }
        private void tsbMapZoomOut_Click(object sender, EventArgs e)
        {
            //Seleciona a ferramenta de diminuição de zoom
            ArcFerramentas(1);
            AtualizaEscala();
        }
        private void tsbMapPan_Click(object sender, EventArgs e)
        {
            //Seleciona a ferramenta de movimentação
            ArcFerramentas(2);
            AtualizaEscala();
        }
        private void tsbMapFull_Click(object sender, EventArgs e)
        {
            //mostra todo o mapa
            if (EhAreaEstudo)
            {
                arcVerZon.ARPageLayout.FocusARMap.SetExtent(AE_XMin, AE_YMin, AE_XMax, AE_YMax);
            }
            else
            {
                arcVerZon.ARPageLayout.FocusARMap.ZoomToFullExtent();
            }
            AtualizaEscala();
        }
        private void tsbMZI_Click(object sender, EventArgs e)
        {
            // aumenta o zoom em 20%
            arcVerZon.ARPageLayout.FocusARMap.ZoomIn(1.20);
            AtualizaEscala();
        }
        private void tsbMZO_Click(object sender, EventArgs e)
        {
            //diminui o zoom em 20%
            arcVerZon.ARPageLayout.FocusARMap.ZoomIn(0.80);
            AtualizaEscala();
        }
        private void tsbMapProxView_Click(object sender, EventArgs e)
        {
            //vai para a próxima vizualização
            if (arcVerZon.ARPageLayout.FocusARMap.CanRedoExtent)
            {
                arcVerZon.ARPageLayout.FocusARMap.RedoExtent();
                AtualizaEscala();
            }
            tsbMapProxView.Enabled = arcVerZon.ARPageLayout.FocusARMap.CanRedoExtent;
        }
        private void tsbMapAntView_Click(object sender, EventArgs e)
        {
            //vai para a vizualização anterior
            if (arcVerZon.ARPageLayout.FocusARMap.CanUndoExtent)
            {
                arcVerZon.ARPageLayout.FocusARMap.UndoExtent();
                AtualizaEscala();
            }
            tsbMapAntView.Enabled = arcVerZon.ARPageLayout.FocusARMap.CanUndoExtent;
        }
        //zoom no layout
        private void tsbLayoutZoomIn_Click(object sender, EventArgs e)
        {
            //Seleciona a ferramenta de aumento de zoom no layout
            ArcFerramentas(4);
            AtualizaEscala();
        }
        private void tsbLayoutZoomOut_Click(object sender, EventArgs e)
        {
            //Seleciona a ferramenta de diminuição de zoom no layout
            ArcFerramentas(5);
            AtualizaEscala();
        }
        private void tsbLayoutPan_Click(object sender, EventArgs e)
        {
            //Seleciona a ferramenta de movimentação do layout
            ArcFerramentas(6);
            AtualizaEscala();
        }
        private void tsbLayoutFull_Click(object sender, EventArgs e)
        {
            //mostra todo o layout
            arcVerZon.ARPageLayout.ZoomToWholePage();
            AtualizaEscala();
        }
        private void tsbLZI_Click(object sender, EventArgs e)
        {
            //aumenta o zoom do layout em 20%
            arcVerZon.ARPageLayout.ZoomIn(1.2);
            AtualizaEscala();
        }
        private void tsbLZO_Click(object sender, EventArgs e)
        {
            //diminui o zoom do layout em 20%
            arcVerZon.ARPageLayout.ZoomIn(0.8);
            AtualizaEscala();
        }
        private void tsbLayoutProxView_Click(object sender, EventArgs e)
        {
            //vai para a próxima vizualização do layout
            if (arcVerZon.ARPageLayout.CanRedoExtent)
            {
                arcVerZon.ARPageLayout.RedoExtent();
                AtualizaEscala();
            }
            tsbLayoutProxView.Enabled = arcVerZon.ARPageLayout.CanRedoExtent;
        }
        private void tsbLayoutAntView_Click(object sender, EventArgs e)
        {
            //vai para a vizualização anterior do layout
            if (arcVerZon.ARPageLayout.CanUndoExtent)
            {
                arcVerZon.ARPageLayout.UndoExtent();
                AtualizaEscala();
            }
            tsbLayoutAntView.Enabled = arcVerZon.ARPageLayout.CanUndoExtent;
        }
        //outras ferramentas
        private void tsbEscala11_Click(object sender, EventArgs e)
        {
            //mostra o layout na escala 1:1 de impressão
            arcVerZon.ARPageLayout.ZoomToPercent(100);
            AtualizaEscala();
        }
        private void tsbInformacao_Click(object sender, EventArgs e)
        {
            ArcFerramentas(3);
            ArcWindows(7);
        }
        private void tsbMedicao_Click(object sender, EventArgs e)
        {
            ArcFerramentas(7);
        }
        private void tsbAdicionaPonto_Click(object sender, EventArgs e)
        {
            ArcWindows(4);
        }
        private void tsbHiperlink_Click(object sender, EventArgs e)
        {
            ArcFerramentas(8);
        }
        private void tsbSwipe_Click(object sender, EventArgs e)
        {
            ArcFerramentas(11);
        }
        private void tsbLupa_Click(object sender, EventArgs e)
        {
            ArcWindows(1);
        }
        //canetas
        private void ddbCaneta_Click(object sender, EventArgs e)
        {
            //primeira ferramenta de caneta
            //altera sua funcionalidade em função da caneta anteriormente escolhida
            if (ddbCaneta.Image.Equals(tsmCanetaFina.Image))
            {
                ArcFerramentas(18);
            }
            else if (ddbCaneta.Image.Equals(tsmCanetaMedia.Image))
            {
                ArcFerramentas(17);
            }
            else if (ddbCaneta.Image.Equals(tsmCanetaGrossa.Image))
            {
                ArcFerramentas(16);
            }
        }
        private void tsmCanetaFina_Click(object sender, EventArgs e)
        {
            //seleciona caneta fina
            ArcFerramentas(18);
            ddbCaneta.Image = tsmCanetaFina.Image;
        }
        private void tsmCanetaMedia_Click(object sender, EventArgs e)
        { //seleciona caneta média
            ArcFerramentas(17);
            ddbCaneta.Image = tsmCanetaMedia.Image;
        }
        private void tsmCanetaGrossa_Click(object sender, EventArgs e)
        { //seleciona caneta grossa
            ArcFerramentas(16);
            ddbCaneta.Image = tsmCanetaGrossa.Image;
        }
        //marcador
        private void ddbMarcador_Click(object sender, EventArgs e)
        {
            //primeira ferramenta de marcador
            //altera sua funcionalidade em função do marcador anteriormente escolhido
            if (ddbMarcador.Image.Equals(tsmMarcadorAmarelo.Image))
            {
                ArcFerramentas(15);
            }
            else if (ddbMarcador.Image.Equals(tsmMarcadorVerde.Image))
            {
                ArcFerramentas(14);
            }
            else if (ddbMarcador.Image.Equals(tsmMarcadorCiano.Image))
            {
                ArcFerramentas(13);
            }
        }
        private void tsmMarcadorAmarelo_Click(object sender, EventArgs e)
        { // seleciona ferramenta de marcador amarelo
            ArcFerramentas(15);
            ddbMarcador.Image = tsmMarcadorAmarelo.Image;
        }
        private void tsmMarcadorVerde_Click(object sender, EventArgs e)
        {
            // seleciona ferramenta de marcador verde
            ArcFerramentas(14);
            ddbMarcador.Image = tsmMarcadorVerde.Image;
        }
        private void tsmMarcadorCiano_Click(object sender, EventArgs e)
        {// seleciona ferramenta de marcador ciano
            ArcFerramentas(13);
            ddbMarcador.Image = tsmMarcadorCiano.Image;
        }
        //borracha
        private void ddbBorracha_Click(object sender, EventArgs e)
        {
            //primeira ferramenta de apagar
            //altera sua funcionalidade em função da borracha anteriormente escolhida
            if (ddbBorracha.Image.Equals(tsmBorrachaPequena.Image))
            {
                ArcFerramentas(20);
            }
            else if (ddbBorracha.Image.Equals(tsmBorrachaTraco.Image))
            {
                ArcFerramentas(19);
            }
            else if (ddbBorracha.Image.Equals(tsmBorrachaMedia.Image))
            {
                ArcFerramentas(21);
            }
            else if (ddbBorracha.Image.Equals(tsmBorrachaGrande.Image))
            {
                ArcFerramentas(22);
            }
        }
        private void tsmBorrachaPequena_Click(object sender, EventArgs e)
        { //seleciona borracha pequena
            ArcFerramentas(20);
            ddbBorracha.Image = tsmBorrachaPequena.Image;
        }
        private void tsmBorrachaTraco_Click(object sender, EventArgs e)
        { //seleciona borracha traço
            ArcFerramentas(19);
            ddbBorracha.Image = tsmBorrachaTraco.Image;
        }
        private void tsmBorrachaMedia_Click(object sender, EventArgs e)
        { //seleciona borracha media
            ArcFerramentas(21);
            ddbBorracha.Image = tsmBorrachaMedia.Image;
        }
        private void tsmBorrachaGrande_Click(object sender, EventArgs e)
        { //seleciona borracha grande
            ArcFerramentas(22);
            ddbBorracha.Image = tsmBorrachaGrande.Image;
        }
        //impressao
        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            //abre janela de impressão
            ArcWindows(2);
        }
        private void tsbConfImpressao_Click(object sender, EventArgs e)
        {
            //abre janela de configurar impressão
            ArcWindows(3);
        }
        //nenhuma ferramenta
        private void tsbNenhum_Click(object sender, EventArgs e)
        {
            ArcFerramentas(9);
            arcVerZon.MousePointer = esriARMousePointer.esriARMousePointerDefault;
        }
        //vetoriza
        private void tsbVetoriza_Click(object sender, EventArgs e)
        {
            Vetoriza = true;
            ValVetoriza = "ponto\tX\tY\r\n";
            tsbSalvaVetor.Enabled = true;
            tsbLimpaVetor.Enabled = true;
            ArcFerramentas(9);
            arcVerZon.MousePointer = esriARMousePointer.esriARMousePointerCrosshair;
        }
        private void tsbSalvaVetor_Click(object sender, EventArgs e)
        {
            Vetoriza = false;
            tsbSalvaVetor.Enabled = false;
            tsbLimpaVetor.Enabled = false;
            Clipboard.SetDataObject(ValVetoriza);
            ValVetoriza = "";
            ValPto = 0;
            ArcFerramentas(9);
            label1.Visible = false;
            arcVerZon.MousePointer = esriARMousePointer.esriARMousePointerDefault;
            tsbSalvaVetor.Enabled = false;
        }
        private void tsbLimpaVetor_Click(object sender, EventArgs e)
        {
            Vetoriza = false;
            tsbSalvaVetor.Enabled = false;
            tsbLimpaVetor.Enabled = false;
            ValVetoriza = "";
            ValPto = 0;
            ArcFerramentas(9);
            label1.Visible = false;
            arcVerZon.MousePointer = esriARMousePointer.esriARMousePointerDefault;
            tsbLimpaVetor.Enabled = false;
        }
        //=========================================
        //             Eventos Diversos
        //=========================================
        private void arcVerZon_OnCurrentViewChanged(object sender, 
            ESRI.ArcGIS.PublisherControls.IARControlEvents_OnCurrentViewChangedEvent e)
        {
            TravaView();
            TravaMovZoom();
        }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            MenuProjetosZON();
            //verifica versão do software
            
            bool Verificou = verificaVersao(@"http://www.uep.cnps.embrapa.br/vizon/vizon.xml",versao);

            if (Verificou)
            {
                string mensagem = "Para concluir a atualização do ViZon, você deverá:\r\n" +
                    "# Desistalar a versão antiga por meio do Painel de Controle do seu computador;\r\n" +
                    "# Executar o instalador que foi baixado em seu computador;";
                MessageBox.Show(mensagem, "Atualizando seu ViZon.");
                Application.Exit();
            }
        }
        private void arcVerZon_OnAfterScreenDraw(object sender, EventArgs e)
        {
            TravaView();
            TravaMovZoom();            
            tscEscala.Text = arcVerZon.ARPageLayout.FocusARMap.MapScale.ToString();
            LeierSwipe = arcVerZon.SelectedARLayer;
            arcVerZon.SwipeLayer = LeierSwipe;


            if (Vetoriza)
            {

            }

        }
        private void tscEscala_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                try
                {
                    string strValEscala = tscEscala.Text;
                    double dblValorEscala = Double.Parse(strValEscala);
                    ARMap map = arcVerZon.ARPageLayout.FocusARMap;
                    map.MapScale = dblValorEscala;
                }
                catch (Exception)
                { 
                    MessageBox.Show("O valor de escala deve ser numérico. Verifique o valor digitado.");
                }
            }
        }
        private void tsmSobreArcMap_Click(object sender, EventArgs e)
        {
            arcVerZon.AboutBox();
        }
        private void arcVerZon_OnMouseMove(object sender, IARControlEvents_OnMouseMoveEvent e)
        {
            double dXcoord = 0;
            double dYcoord = 0;
            Config set = new Config();

            if (arcVerZon.CurrentViewType == esriARViewType.esriARViewTypeMap)
            {
                //somente coordenadas do mapa
                ARMap map = arcVerZon.ARPageLayout.FocusARMap;
                map.ToMapPoint(e.x, e.y, ref dXcoord, ref dYcoord);

                double temp1 = Math.Round(dXcoord, 5);
                double temp2 = Math.Round(dYcoord, 5);

                sslMapCoordX.Text = dXcoord.ToString();
                sslMapCoordY.Text = dYcoord.ToString();

                sslLayoutCoordX.Text = "";
                sslLayoutCoordY.Text = "";
            }

            else if (arcVerZon.CurrentViewType == esriARViewType.esriARViewTypePageLayout)
            {
                //geração das coordenadas do layout
                arcVerZon.ARPageLayout.ToPagePoint(e.x, e.y, ref dXcoord, ref dYcoord);
                dXcoord = Math.Round(dXcoord, 2);
                dYcoord = Math.Round(dYcoord, 2);
                sslLayoutCoordX.Text = dXcoord.ToString();
                sslLayoutCoordY.Text = dYcoord.ToString();


                //geração de coordenadas Lat Long
                ARMap map = arcVerZon.ARPageLayout.HitTestMap(e.x, e.y);
                map.ToMapPoint(e.x, e.y, ref dXcoord, ref dYcoord);

                dXcoord = Math.Round(dXcoord, 5);
                dYcoord = Math.Round(dYcoord, 5);
                sslMapCoordX.Text = dXcoord.ToString();
                sslMapCoordY.Text = dYcoord.ToString();
            }

            else
            {
                sslMapCoordX.Text = "";
                sslMapCoordY.Text = "";
                sslLayoutCoordX.Text = "";
                sslLayoutCoordY.Text = "";

            }
        }
        private void arcVerZon_OnMouseDown(object sender, IARControlEvents_OnMouseDownEvent e)
        {
            try
            {
                if (e.button == 2)
                {
                    arcVerZon.ContextMenuStrip = cmsMenu;
                    arcVerZon.ContextMenuStrip.Show(MousePosition);
                    //somente coordenadas do mapa
                    ARMap map = arcVerZon.ARPageLayout.FocusARMap;
                    map.ToMapPoint(e.x, e.y, ref mouseXcoord, ref mouseYcoord);
                }
                else
                {
                    if (Vetoriza & arcVerZon.CurrentARTool == esriARTool.esriARToolNoneSelected)
                    {
                        //somente coordenadas do mapa
                        ARMap map = arcVerZon.ARPageLayout.FocusARMap;
                        map.ToMapPoint(e.x, e.y, ref mouseXcoord, ref mouseYcoord);
                        ValVetoriza += ValPto.ToString() + "\t" + mouseXcoord.ToString()
                            + "\t" + mouseYcoord.ToString() + "\r\n";

                        label1.Visible = true;
                        label1.Top = e.y + 3;
                        label1.Left = e.x + arcVerZon.TOCWidth + 3;
                        ValPto += 1;
                        tsbLimpaVetor.Enabled = true;
                        tsbSalvaVetor.Enabled = true;
                    }
                    if (arcVerZon.SelectedARLayer != LeierSwipe)
                    {
                        arcVerZon.SwipeLayer = arcVerZon.SelectedARLayer;
                        arcVerZon.Refresh();

                    }





                }
            }
            catch
            {

            }
        }
        private void tsmCopiarCoord_Click(object sender, EventArgs e)
        {
            string coordenadas = "X: " + mouseXcoord.ToString() + " Y: " + mouseYcoord.ToString();
            Clipboard.SetDataObject(coordenadas);
        }
        private void tmrRelogio_Tick(object sender, EventArgs e)
        {
            barraProgresso();

        }
    }//fim da classe
}//fim do namespace