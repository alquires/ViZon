using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.fonts;


/*
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
 */

namespace ZonView017
{
    public partial class frmFormatRel : Form
    {
        //=========================================
        //          Declarações de Variaveis
        //=========================================
        /// <summary>
        /// guarda a sintaxe de pesquisa, onde se aplica restrições de "area de estudo"
        /// </summary>
        string Sintaxe;
        /// <summary>
        /// tabela com os dados
        /// </summary>
        DataTable DT_Dados;
        /// <summary>
        /// nome amigavel das colunas da tabela
        /// </summary>
        string[,] ColunasTabelas;
        /// <summary>
        /// parametros para construção do relatório
        /// </summary>
        string[] ParRelatorio;
        /// <summary>
        /// controle para a alternancia nas cores das linhas do relatório
        /// </summary>
        bool linhas = true;
        bool Veiopesquisa = true;
        /// <summary>
        /// Documento PDF a ser criado
        /// </summary>
        Document DocPDF; // = new Document(PageSize.A4, 57f, 57f, 57f, 57f);
        
        //=========================================
        //             Construtor
        //=========================================
        /// <summary>
        /// construtor da classe, onde incluí a passagem de alguns parametros
        /// </summary>
        /// <param name="SintaxePesquisa">sintaxe da pesquisa realizada. O relatorio levará em conta a pesquisa do usuário</param>
        /// <param name="TabelaPesquisa">tabela da pesquisa</param>
        /// <param name="parametrosTabela">nome amigavel das colunas da tabela</param>
        /// <param name="ParametrosRelatorio">recebe os parametros para a confecção do relatório</param>
        public frmFormatRel(string SintaxePesquisa, DataTable TabelaPesquisa,
            string[,] parametrosTabela, string[] ParametrosRelatorio)
        {
            Sintaxe = SintaxePesquisa;
            DT_Dados = TabelaPesquisa;
            ColunasTabelas = parametrosTabela;
            ParRelatorio = ParametrosRelatorio;
            
            InitializeComponent();
        }


        //=========================================
        //             propriedades
        //=========================================

        public bool prpFalso
        {
            get { return this.Veiopesquisa; }
            set { this.Veiopesquisa = value; }
        }



        //=========================================
        //                Métodos
        //=========================================
        //metodo de geração do PDF
        /// <summary>
        /// verifica se o arquivo pdf padrão já está aberto
        /// </summary>
        /// <param name="filePath">endereço do arquivo pdf padrão</param>
        /// <returns>true = o arquivo esta aberto (não pode ser salvo); false = o arquivo esta fechado (pode ser salvo);</returns>
        public bool IsFileOpen(string filePath)
        {
            bool fileOpened = false;
                try
                {
                    System.IO.FileStream fs = System.IO.File.OpenWrite(filePath);
                    fs.Close();
                }
                catch
                {
                    fileOpened = true;
                    MessageBox.Show
                        ("Já existe um relatório aberto. Você poderá salvar uma cópia, mas terá que fecha-lo antes de continuar.");
                }

            return fileOpened;
        }
        /// <summary>
        /// Monta a estrutura para a inserção do cabeçalho no PDF
        /// </summary>
        /// <returns>retorna a imagem, de acordo com os parametros do relatório</returns>
        private Image AddCabecalho()
        {
            string caminho = ParRelatorio[7] + "\\" +  ParRelatorio[2];

            iTextSharp.text.Image ImagemCabe =  iTextSharp.text.Image.GetInstance(caminho);
            if (ImagemCabe.Width > 482)
            {
                float v = 482f / ImagemCabe.Width;
                v = v * 100;
                ImagemCabe.ScalePercent(v);
            }
            ImagemCabe.Alignment = Image.ALIGN_CENTER;

            return ImagemCabe;
        }
        /// <summary>
        /// Recebe os textos e aplica as formatações para a inclusão do grupo
        /// </summary>
        /// <param name="Campo1">Nome do grupo</param>
        /// <param name="Campo2"> valor do grupo</param>
        /// <returns>texto formatado para ser incluído no documento pdf</returns>
        private PdfPTable AddGrupo(string Campo1, string Campo2)
        {
            PdfPTable tabelaG = new PdfPTable(2);
            try
            {
                PdfPCell celulaG = new PdfPCell();
                celulaG.BackgroundColor = BaseColor.BLACK;
                celulaG.BorderColor = BaseColor.WHITE;

                Font FCabeca = FontFactory.GetFont("Verdana", 11, BaseColor.WHITE);
                celulaG.Phrase = new Phrase(Campo1, FCabeca);
                tabelaG.AddCell(celulaG);

                celulaG.Phrase = new Phrase(Campo2, FCabeca);
                tabelaG.AddCell(celulaG);
                float[] flutuante = new float[2] { 238f, 357f };
                tabelaG.SetWidthPercentage(flutuante, PageSize.A4);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }

            return tabelaG;
        }
        /// <summary>
        /// Recebe os textos e aplica as formatações para a inclusão do subgrupo
        /// </summary>
        /// <param name="Campo1">Nome do grupo</param>
        /// <param name="Campo2">valor do subgrupo</param>
        /// <returns>texto formatado para ser incluído no documento pdf</returns>
        private PdfPTable AddSubgrupo(string Campo1, string Campo2)
        {
            float[] flutuante = new float[2] { 238f, 357f };
            PdfPTable tabelaS = new PdfPTable(flutuante);
            try
            {
                PdfPCell celulaS = new PdfPCell();
                //celulaS.Width = 20;
                celulaS.BackgroundColor = BaseColor.LIGHT_GRAY;
                celulaS.BorderColor = BaseColor.WHITE;

                celulaS.Phrase = new Phrase(Campo1);
                celulaS.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                tabelaS.AddCell(celulaS);

                celulaS.Phrase = new Phrase(Campo2);
                celulaS.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                tabelaS.AddCell(celulaS);
                tabelaS.SetWidthPercentage(flutuante, PageSize.A4);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
            return tabelaS;
        }
        /// <summary>
        /// Recebe os textos e aplica as formatações para a inclusão da linha título da classe
        /// </summary>
        /// <param name="Campo1">Nome do da classe</param>
        /// <param name="Campo2"> Indicação do valor da área e a unidade de medida discriminada</param>
        /// <returns>texto formatado para ser incluído no documento pdf</returns>
        private PdfPTable AddTClasse(string Campo1, string Campo2)
        {

            float[] flutuante = new float[3] { 60f, 297f, 238f };
            PdfPTable tabelaCl = new PdfPTable(flutuante);
            try
            {
                PdfPCell celulaCl = new PdfPCell();
                celulaCl.BorderColor = BaseColor.WHITE;
                celulaCl.VerticalAlignment = Element.ALIGN_TOP;


                celulaCl.Phrase = new Phrase("");
                tabelaCl.AddCell(celulaCl);

                celulaCl.BorderColorBottom = BaseColor.BLACK;
                celulaCl.BorderColorTop = BaseColor.BLACK;
                celulaCl.BorderWidthBottom = 1f;
                celulaCl.BorderWidthTop = 1f;
                celulaCl.Phrase = new Phrase(Campo1);
                tabelaCl.AddCell(celulaCl);


                celulaCl.Phrase = new Phrase(Campo2);
                celulaCl.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                tabelaCl.SetWidthPercentage(flutuante, PageSize.A4);
                tabelaCl.AddCell(celulaCl);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }

            return tabelaCl;
        }
        /// <summary>
        /// Recebe os textos e aplica as formatações para a inclusão da classe e dos valores
        /// </summary>
        /// <param name="Campo1">Nome da classe</param>
        /// <param name="Campo2">Valor da classe (area)</param>
        /// <returns>texto formatado para ser incluído no documento pdf</returns>
        private PdfPTable AddClasse(string Campo1, string Campo2)
        {
            float[] flutuante = new float[3] { 60f, 297f, 238f};
            PdfPTable tabelaCl = new PdfPTable(flutuante);
            try
            {
                PdfPCell celulaCl = new PdfPCell();
                celulaCl.BorderColor = BaseColor.WHITE;

                BaseColor Collor = new BaseColor(240, 255, 240);

                celulaCl.Phrase = new Phrase("");
                tabelaCl.AddCell(celulaCl);


                if (linhas)
                {
                    celulaCl.BackgroundColor = BaseColor.WHITE;
                    linhas = false;
                }
                else
                {
                    celulaCl.BackgroundColor = Collor;
                    linhas = true;
                }

                celulaCl.Phrase = new Phrase(Campo1);
                tabelaCl.AddCell(celulaCl);

                celulaCl.Phrase = new Phrase(Campo2);
                celulaCl.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                tabelaCl.AddCell(celulaCl);
                tabelaCl.SetWidthPercentage(flutuante, PageSize.A4);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
            
            return tabelaCl;
        }
        /// <summary>
        /// Monta a estrutura para a inserção do rodapé no PDF
        /// </summary>
        /// <returns>retorna a imagem, de acordo com os parametros do relatório</returns>
        private Image AddRodape()
        {
            string caminho = ParRelatorio[7] + "\\" + ParRelatorio[3];

            iTextSharp.text.Image ImagemCabe =
    iTextSharp.text.Image.GetInstance(caminho);
            if (ImagemCabe.Width > 482)
            {
                float v = 482f / ImagemCabe.Width;
                v = v * 100;
                ImagemCabe.ScalePercent(v);
            }
            ImagemCabe.Alignment = Image.ALIGN_CENTER;

            return ImagemCabe;
        }
        /// <summary>
        /// Controla os valores de linhas para criar novas páginas
        /// </summary>
        /// <param name="linhas">valor inteiro com o valor da linha atual</param>
        /// <returns> retorna o valor de linhas acrescido mais um ou zerado</returns>
        private int ControlaLinhas(int linhas)
        {
            if (linhas > 32)
            {
                Phrase frase = new Phrase("");
                DocPDF.Add(frase);
                DocPDF.Add(AddRodape());
                DocPDF.NewPage(); //incluir aqui, todo o H
                DocPDF.Add(AddCabecalho());
                linhas = 0;
            }
            else
            {
                linhas += 1;
            }
            return linhas;
        }
        /// <summary>
        /// metodo que controla a pesquisa e a montagem do documento PDF.
        /// </summary>
        /// 
        private void GeraPDF()
        {


            try
            {
            //############################
            //    DECLARAÇÃO VARIAVEIS
            //############################
                DateTime AGORA = DateTime.Now;
                string nomeArquivo = "Relatorio_(" + AGORA.Day.ToString() + "-" + AGORA.Month.ToString() + "-" + AGORA.Year.ToString()
                    + ")_" + AGORA.Hour.ToString() + "." + AGORA.Minute.ToString() + "." + AGORA.Second.ToString() + ".pdf";

                string PathDoc = Path.GetTempPath() + nomeArquivo;
            if (IsFileOpen(PathDoc))
            {
                return;
            }
            else
            {
                DataRow[] Linhas = DT_Dados.Select(Sintaxe);
                //pega a posição da coluna
                int ind_Grupo = PosColuna(cboGrupo.SelectedItem.ToString());
                int ind_Subgrupo = PosColuna(cboSubGrupo.SelectedItem.ToString());
                int ind_Classe = PosColuna(cboClasse.SelectedItem.ToString());
                int ind_Valor = PosColuna(ParRelatorio[5]);
                    //ind_Valor 

                string tmpCampo1Grupo = "";
                string tmpCampo2Grupo = "";

                double calculoValores = 0;
                int ContagemLinhas = 0;

                int arredondamento = Int32.Parse(nudArredonda.Value.ToString());

                //arrays definitivos
                string[] ValGrupos;
                string[] ValSubgrupos;
                string[] ValClasse;

                //variaveis que calcula os valores
                //totais, para cálculo de porcentagem
                double TotSugGrupo = 0;
                double TotGrupo = 0;
                double TotAreaEstudo = Calculo(ind_Valor, ckbAreaPerc.Checked); ;

 
                //############################
                //          CODIGO
                //############################

                ValGrupos = pesquisa(ind_Grupo);

                //Inicio do arquivo PDF
                //Document DocPDF1 = new Document(PageSize.A4, 57f, 57f, 57f, 57f);
                DocPDF = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
                // cria um Writer para o documento

                PdfWriter.GetInstance(DocPDF, new
                  FileStream(PathDoc, FileMode.Create));

                string CalculoRelatorio = "";
                if (radSoma.Checked)
                {CalculoRelatorio = radSoma.Text;}
                else if(radPercEstudo.Checked)
                {CalculoRelatorio = radPercEstudo.Text;}
                else if(radPercGrupo.Checked)
                {CalculoRelatorio = radPercGrupo.Text;}
                else if (radPercSubGrupo.Checked)
                {CalculoRelatorio = radPercSubGrupo.Text;}
                else if(radOmitirArea.Checked)
                {CalculoRelatorio = radOmitirArea.Text;}
                
                // abre o documento
                DocPDF.Open();
                //DocPDF.AddAuthor(Sintaxe);
                DocPDF.AddKeywords(Sintaxe + "\r\n" + cboGrupo.SelectedItem.ToString() + "\r\n" + cboSubGrupo.SelectedItem.ToString() +
                    "\r\n" + cboClasse.SelectedItem.ToString()+ "\r\n" + CalculoRelatorio+ "\r\n" + ckbAreaPerc.Checked.ToString());
                //DocPDF.AddAuthor("Embrapa Solos\r\nteste");
                //DocPDF.AddTitle("Sintaxe");
                DocPDF.Add(AddCabecalho());
                
                for (int a = 0; a < ValGrupos.Length; a++)
                {
                    if (radPercGrupo.Checked)
                    {
                        //=== calculo todo o grupo ===
                        TotGrupo = Calculo(ValGrupos[a], ind_Grupo, ind_Valor, ckbAreaPerc.Checked);
                        //=== calculo todo o grupo ===
                    }

                    Phrase frase = new Phrase("");
                    if (ContagemLinhas == 0)
                    {
                        //insere o cabeçalho PDF da pagina
                        
                        DocPDF.Add(frase);
                        if (ind_Grupo == ind_Subgrupo | ind_Subgrupo == ind_Classe | ind_Grupo == ind_Classe)
                        {
                            //=======================================================
                            //         Insere o cabeçalho da classe da tabela
                            //=======================================================
                            Dados dd = new Dados();
                            string unidade = dd.RetornaUnidade(0, Int32.Parse(ParRelatorio[4]));

                            string tmpCampo2TCl = cboClasse.SelectedItem.ToString();
                            string tmpCampo3TCl = "Área - " + unidade;

                            DocPDF.Add(AddTClasse(tmpCampo2TCl, tmpCampo3TCl));
                            ContagemLinhas = ControlaLinhas(ContagemLinhas);
                            //=======================================================
                        }
                    } // if (ContagemLinhas == 0)


                    if (ind_Grupo != ind_Subgrupo | ind_Grupo != ind_Classe)
                    {
                        //=======================================================
                        //         Insere a linha de grupo da tabela
                        //=======================================================
                        tmpCampo1Grupo = cboGrupo.SelectedItem.ToString();
                        tmpCampo2Grupo = ValGrupos[a];
                        DocPDF.Add(AddGrupo(tmpCampo1Grupo, tmpCampo2Grupo));
                        ContagemLinhas = ControlaLinhas(ContagemLinhas);
                        //=======================================================
                    }

                    ValSubgrupos = pesquisa(ValGrupos[a], ind_Grupo, ind_Subgrupo);
                    for (int b = 0; b < ValSubgrupos.Length; b++)
                    {
                        if (radPercSubGrupo.Checked)
                        {
                            //=== calculo todo o subgrupo ===
                            TotSugGrupo = Calculo(ValSubgrupos[b], ind_Subgrupo, ind_Valor, ckbAreaPerc.Checked);
                            //=== calculo todo o subgrupo ===
                        }
                        if (ind_Grupo != ind_Subgrupo | ind_Subgrupo != ind_Classe)
                        {
                            //=======================================================
                            //         Insere a linha de SubGrupo da tabela
                            //=======================================================
                            string tmpCampo1SG = cboSubGrupo.SelectedItem.ToString();
                            string tmpCampo2SG = ValSubgrupos[b];

                            DocPDF.Add(AddSubgrupo(tmpCampo1SG, tmpCampo2SG));
                            ContagemLinhas = ControlaLinhas(ContagemLinhas);
                            //=======================================================

                            //=======================================================
                            //         Insere o cabeçalho da classe da tabela
                            //=======================================================
                            Dados dd = new Dados();
                            string unidade = dd.RetornaUnidade(0, Int32.Parse(ParRelatorio[4]));

                            string tmpCampo2TCl = "";
                            string tmpCampo3TCl = "";

                            if (radSoma.Checked)
                            {
                                tmpCampo2TCl = cboClasse.SelectedItem.ToString();
                                tmpCampo3TCl = "Área - " + unidade;
                            }
                            else if (radPercSubGrupo.Checked)
                            {
                                tmpCampo2TCl = cboClasse.SelectedItem.ToString();
                                tmpCampo3TCl = "% da área para " + tmpCampo1SG + " = " + tmpCampo2SG;
                            }
                            else if (radPercGrupo.Checked)
                            {
                                tmpCampo2TCl = cboClasse.SelectedItem.ToString();
                                tmpCampo3TCl = "% da área para " + tmpCampo1Grupo + " = " + tmpCampo2Grupo;
                            }
                            else if (radPercEstudo.Checked)
                            {
                                tmpCampo2TCl = cboClasse.SelectedItem.ToString();
                                tmpCampo3TCl = "% da área em relação a região de estudo";
                            }
                            else if (radOmitirArea.Checked)
                            {
                                tmpCampo2TCl = cboClasse.SelectedItem.ToString();
                                tmpCampo3TCl = "";
                            }

                            DocPDF.Add(AddTClasse(tmpCampo2TCl, tmpCampo3TCl));
                            ContagemLinhas = ControlaLinhas(ContagemLinhas);
                            //=======================================================
                        }

                        ValClasse = pesquisa(ValGrupos[a], ind_Grupo, ValSubgrupos[b], ind_Subgrupo, ind_Classe);
                        for (int c = 0; c < ValClasse.Length; c++)
                        {

                            //calculoValores = Calculo(ValClasse[c], ind_Classe, ind_Valor);
                            calculoValores = Calculo(ValClasse[c], ind_Classe, ValSubgrupos[b], ind_Subgrupo,
                                ValGrupos[a], ind_Grupo, ind_Valor);
                            //=======================================================
                            //         Insere a linha da Classe da tabela
                            //=======================================================

                            string tmpCampo2Cl = "";
                            string tmpCampo3Cl = "";

                            if (radSoma.Checked)
                            {
                                tmpCampo2Cl = ValClasse[c].ToString();
                                tmpCampo3Cl = Math.Round(calculoValores, arredondamento).ToString();
                                //tmpCampo3Cl =  calculoValores.ToString();
                            }
                            else if (radPercSubGrupo.Checked)
                            {
                                tmpCampo2Cl = ValClasse[c].ToString();
                                tmpCampo3Cl = CalculoPorcentagem(TotSugGrupo,calculoValores).ToString(); //marcar este
                            }
                            else if (radPercGrupo.Checked)
                            {
                                tmpCampo2Cl = ValClasse[c].ToString();
                                tmpCampo3Cl = CalculoPorcentagem(TotGrupo, calculoValores).ToString();
                            }
                            else if (radPercEstudo.Checked)
                            {
                                tmpCampo2Cl = ValClasse[c].ToString();
                                if (CalculoPorcentagem(TotAreaEstudo, calculoValores) == 0)
                                {

                                }

                                tmpCampo3Cl = CalculoPorcentagem(TotAreaEstudo, calculoValores).ToString();
                            }
                            else if (radOmitirArea.Checked)
                            {
                                tmpCampo2Cl = ValClasse[c].ToString();
                                tmpCampo3Cl = "";
                            }
                            PdfPTable tabelaClasse = AddClasse(tmpCampo2Cl, tmpCampo3Cl);
                            DocPDF.Add(tabelaClasse);
                            ContagemLinhas = ControlaLinhas(ContagemLinhas);
                            //=======================================================
                        }
                    }

                    prgAndamento.Value = Convert.ToInt32(a * 99 / ValGrupos.Length);

                }//fim do laço for a
                DocPDF.Add(AddRodape());
                DocPDF.Close();
                System.Diagnostics.Process.Start(PathDoc);
            
            }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
            prgAndamento.Value = 0;
        }

        /// <summary>
        /// <para>Retorna o valor de ordem (ordinal) da posição da coluna selecionada</para>
        /// <param name="NomeColuna">Nome da coluna a ser pesquisada</param>
        /// </summary>
        private int PosColuna(string NomeColuna)
        {

            int val = -1;
            try
            {
                int ciclos = ColunasTabelas.Length / 3;
                for (int i = 0; i < ciclos; i++)
                {
                    if (NomeColuna == ColunasTabelas[i, 0])
                    {
                        val = DT_Dados.Columns[ColunasTabelas[i, 1]].Ordinal;
                    }
                }
                if (val == -1)
                {
                    val = DT_Dados.Columns[NomeColuna].Ordinal;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
            
            return val;
        }
        /// <summary>
        /// <para>Retorna valores unicos para cada coluna pesquisada (I)</para>
        /// <param name="nivelSup"> valor do grupo a ser pesquisado</param>
        /// <param name="PosSup"> Posicao da coluna correspondente ao nivel hierarquico superior</param>
        /// <param name="PosAnalise">Posição da coluna a ser analisada</param>
        /// </summary>
        private string[] pesquisa(string nivelSup, int PosSup, int PosAnalise)
        {
            DataRow[] Linhas = DT_Dados.Select(Sintaxe);

            //string tmpTXT1 = "";
            string[] tempTXT = new string[Linhas.Length];
            string[] ValUnicos;
            int valor = 0;


            for (int b = 0; b < Linhas.Length; b++)
            {
                if (Linhas[b][PosSup].ToString() == nivelSup)
                {
                    tempTXT[b] = Linhas[b][PosAnalise].ToString(); 
                    if(tempTXT[b] !=null)
                    {
                        valor += 1;
                    }
                }
            }
            string[] valores = new string[valor];
            int V2 = 0;
            for (int a = 0; a < Linhas.Length; a++)
            {
                if (Linhas[a][PosSup].ToString() == nivelSup)
                {
                    valores[V2] = Linhas[a][PosAnalise].ToString();
                    V2 += 1;
                }
            }
            

            Dados dd = new Dados();

            ValUnicos = dd.GetDistinctValues(valores);
            ArrayList valoresOrdenados = new ArrayList(ValUnicos);
            valoresOrdenados.Sort();

            for (int b = 0; b < valoresOrdenados.Count; b++)
            {
                ValUnicos[b] = valoresOrdenados[b].ToString();
            }


            //Type TipoArray = ValUnicos.GetType();
            //ValUnicos = valoresOrdenados.;
            return ValUnicos;
        }
        /// <summary>
        /// Retorna valores unicos para cada coluna pesquisada (II)
        /// </summary>
        /// <param name="nivelSup">valor do grupo a ser pesquisado</param>
        /// <param name="PosSup">Posicao da coluna correspondente ao grupo</param>
        /// <param name="nivelInf">valor do subgrupo a ser pesquisado</param>
        /// <param name="PosInf">Posicao da coluna correspondente ao subgrupo</param>
        /// <param name="PosAnalise">Posição da coluna a ser analisada</param>
        /// <returns>Retorna valores unicos para cada coluna pesquisada</returns>
        private string[] pesquisa(string nivelSup, int PosSup, string nivelInf, int PosInf, int PosAnalise)
        {

            DataRow[] Linhas = DT_Dados.Select(Sintaxe);

            //string tmpTXT1 = "";
            string[] tempTXT = new string[Linhas.Length];
            string[] ValUnicos;
            int valor = 0;


            for (int b = 0; b < Linhas.Length; b++)
            {
                if (Linhas[b][PosSup].ToString() == nivelSup & 
                    Linhas[b][PosInf].ToString() == nivelInf)
                {
                    tempTXT[b] = Linhas[b][PosAnalise].ToString();
                    if (tempTXT[b] != null)
                    {
                        valor += 1;
                    }
                }
            }
            string[] valores = new string[valor];
            int V2 = 0;
            for (int a = 0; a < Linhas.Length; a++)
            {
                if (Linhas[a][PosSup].ToString() == nivelSup &
                    Linhas[a][PosInf].ToString() == nivelInf)
                {
                    valores[V2] = Linhas[a][PosAnalise].ToString();
                    V2 += 1;
                }
            }


            Dados dd = new Dados();

            ValUnicos = dd.GetDistinctValues(valores);
            ArrayList valoresOrdenados = new ArrayList(ValUnicos);
            valoresOrdenados.Sort();

            for (int b = 0; b < valoresOrdenados.Count; b++)
            {
                ValUnicos[b] = valoresOrdenados[b].ToString();
            }
            return ValUnicos;
        }
        /// <summary>
        /// <para>Retorna valores unicos para cada coluna pesquisada (III)</para>
        /// <param name="PosAnalise">Posição da coluna a ser analisada</param>
        /// </summary>
        private string[] pesquisa(int PosAnalise)
        {



            DataRow[] Linhas = DT_Dados.Select(Sintaxe);


            string[] tempTXT = new string[Linhas.Length];
            string[] ValUnicos;

            for (int b = 0; b < Linhas.Length; b++)
            {    
                tempTXT[b] = Linhas[b][PosAnalise].ToString();
            }
            Dados dd = new Dados();
            ValUnicos = dd.GetDistinctValues(tempTXT);
            ArrayList valoresOrdenados = new ArrayList(ValUnicos);
            valoresOrdenados.Sort();

            for (int b = 0; b < valoresOrdenados.Count; b++)
            {
                ValUnicos[b] = valoresOrdenados[b].ToString();
            }

            return ValUnicos;
        }

        /// <summary>
        /// Faz a soma das áreas das classes
        /// </summary>
        /// <param name="ValorClasse">Valor da classe a ser analisada</param>
        /// <param name="PosClasse">Posição da coluna de classe</param>
        /// <param name="ValorSubGrupo">Posição da coluna de subgrupo</param>
        /// <param name="PosSubGrupo">Posição da coluna de subgrupo</param>
        /// <param name="ValorGrupo">Posição da coluna de grupo</param>
        /// <param name="PosGrupo">Posição da coluna de subgrupo</param>
        /// <param name="PosVal">Posição da coluna com o valor a ser calculado</param>
        /// <returns>retorna o valor (em Double) com a soma</returns>
        private double Calculo(string ValorClasse, int PosClasse, 
                            string ValorSubGrupo, int PosSubGrupo, 
                        string ValorGrupo, int PosGrupo, int PosVal)
        {

            double tmpDouble = 0;
            try
            {
                DataRow[] Linhas = DT_Dados.Select(Sintaxe);

                for (int a = 0; a < Linhas.Length; a++)
                {
                    if (Linhas[a][PosClasse].ToString() == ValorClasse &
                        Linhas[a][PosGrupo].ToString() == ValorGrupo & 
                        Linhas[a][PosSubGrupo].ToString() == ValorSubGrupo)
                    {
                        string tempTXT = Linhas[a][PosVal].ToString();
                        tmpDouble += double.Parse(tempTXT);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }

            return tmpDouble;
        }

        /// <summary>
        /// <para>Retorna o valor absoluto para a classe</para>
        /// <param name="ValorClasse">Valor da classe, subgrupo ou grupo a ser analisada</param>
        /// <param name="PosClasse">Posição da coluna de classe</param>
        /// <param name="PosVal">Posição da coluna com o valor a ser calculado</param>
        /// </summary>
        /*private double CalculoX(string ValorClasse, int PosClasse, int PosVal)
        {


            double tmpDouble = 0;
            try
            {
                DataRow[] Linhas = DT_Dados.Select(Sintaxe);

                for (int a = 0; a < Linhas.Length; a++)
                {
                    if (Linhas[a][PosClasse].ToString() == ValorClasse)
                    {
                        string tempTXT = Linhas[a][PosVal].ToString();
                        tmpDouble += double.Parse(tempTXT);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
            
            return tmpDouble;
        }*/
        /// <summary>
        /// Retorna o valor absoluto para a classe, subgrupo ou grupo
        /// </summary>
        /// <param name="ValorClasse">Valor da classe, subgrupo ou grupo a ser analisada</param>
        /// <param name="PosClasse">Posição da coluna de classe</param>
        /// <param name="PosVal">Posição da coluna com o valor a ser calculado</param>
        /// <param name="areaToda">considerar a sintaxe de pesquisa?</param>
        /// <returns></returns>
        private double Calculo(string ValorClasse, int PosClasse, int PosVal, bool areaToda)
        {
            string SintaxeLocal = "";

            if (areaToda != true)
            {
                SintaxeLocal = Sintaxe;
            }


            double tmpDouble = 0;
            try
            {
                DataRow[] Linhas = DT_Dados.Select(SintaxeLocal);

                for (int a = 0; a < Linhas.Length; a++)
                {
                    if (Linhas[a][PosClasse].ToString() == ValorClasse)
                    {
                        string tempTXT = Linhas[a][PosVal].ToString();
                        tmpDouble += double.Parse(tempTXT);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }

            return tmpDouble;
        }
        /// <summary>
        /// Retorna a soma de todos os itens selecionados
        /// </summary>
        /// <param name="PosVal">Posição da coluna de cálculo</param>
        /// <returns></returns>
        private double Calculo(int PosVal, bool areaToda)
        {
            string SintaxeLocal = "";

            if (areaToda != true)
            {
                SintaxeLocal = Sintaxe;
            }

            double tmpDouble = 0;
            try
            {
                DataRow[] Linhas = DT_Dados.Select(SintaxeLocal);

                for (int a = 0; a < Linhas.Length; a++)
                {
                    string tempTXT = Linhas[a][PosVal].ToString();
                    tmpDouble += double.Parse(tempTXT);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }


            return tmpDouble;
        }

        /// <summary>
        /// realiza o calculo da porcentagem
        /// </summary>
        /// <param name="ValorTotal">Valor total do grupo, subgrupo ou area de estudo</param>
        /// <param name="ValorParcial">Valor da classe</param>
        /// <returns>retorna a porcentagem referente ao nível escolhido</returns>
        private double CalculoPorcentagem(double ValorTotal, double ValorParcial)
        {
            double tmpDouble = 0;
            try
            {
                int arredondamento = Int32.Parse(nudArredonda.Value.ToString());

                tmpDouble = Math.Round(ValorParcial * 100 / ValorTotal, arredondamento);
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
            return tmpDouble;
        }

        //=========================================
        //             Eventos Diversos
        //=========================================
        private void frmFormatRel_Load(object sender, EventArgs e)
        {
            try
            {                
              
                for (int i = 0; i < ColunasTabelas.Length / 3; i++)
                {
                    string Nome_da_coluna = ColunasTabelas[i, 1];
                    if (Nome_da_coluna != null)
                    {
                        string Dado_da_coluna = DT_Dados.Columns[Nome_da_coluna].DataType.ToString();

                        cboGrupo.Items.Add(ColunasTabelas[i, 0]);
                        cboSubGrupo.Items.Add(ColunasTabelas[i, 0]);
                        cboClasse.Items.Add(ColunasTabelas[i, 0]);
                    }

                }
                Dados dd = new Dados();
                string unidade = dd.RetornaUnidade(0, Int32.Parse(ParRelatorio[4]));
                //cboValores.Items.Add("Área - " + unidade);

                cboGrupo.SelectedIndex = 0;
                cboSubGrupo.SelectedIndex = 0;
                cboClasse.SelectedIndex = 0;
                //cboValores.SelectedIndex = 0;
            }
             
            catch
            {

            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            btnOk.Enabled = false;

            GeraPDF();

            btnOk.Enabled = true;
            this.UseWaitCursor = false;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

    }
}
