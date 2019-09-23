using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ZonView017
{
    public partial class frmPesquisaAvancada : Form
    {
        //=========================================
        //          Declarações de Variaveis
        //=========================================
        DataTable dtTabelaPA = new DataTable();
        string strInformativos;
        string strAdicional;
        string[,] strParametrosPesquisa;
        string[] strPrmAdicional;
        bool bolFormatRel = false;
        int intPosCursor = 0;
        int intListAtributos = -1;
        double SegundosAtr = DateTime.Now.TimeOfDay.TotalSeconds;
        int indiceCBOAtr = 0;
        double SegundosVal = DateTime.Now.TimeOfDay.TotalSeconds;
        int indiceCBOVal = 0;

        //=========================================
        //               Construtor
        //=========================================
        public frmPesquisaAvancada()
        {
            InitializeComponent();
        }

        //=========================================
        //                Métodos
        //=========================================
        protected void AdicionaSinal(string sinal)
        {
            try
            {
                int posCursor = txtSQLSintaxe.SelectionStart;
                string valor = " " + sinal + " ";
                txtSQLSintaxe.Text = txtSQLSintaxe.Text.Insert(posCursor, valor);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
        }
        public void QuebraParametros(string parametros)
        {
            //LOG dispensado (valor insignificante)


            try
            {
                //string strTmp001 = "";
                parametros = parametros.Replace("\n", "");
                //strTmp001 = strTmp001
                string[] strTmp002;
                strTmp002 = parametros.Split('\r');
                int intTmp001;
                intTmp001 = strTmp002.Length;
                int intTmp002;
                intTmp002 = strTmp002[0].Split('#').Length;

                strParametrosPesquisa = new string[intTmp001, intTmp002];

                for (int i = 0; i < intTmp001 - 1; i++)
                {
                    string[] strTmp004 = strTmp002[i].Split('#');
                    int intTmp003 = strTmp004.Length;

                    for (int i2 = 0; i2 < intTmp002 - 1; i2++)
                    {
                        strParametrosPesquisa[i, i2] = strTmp004[i2];
                    }

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
        }
        private void QuebraParametros2()
        {
            try
            {
                strPrmAdicional = strAdicional.Split('#');
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
        }
        public void Preenche_cboAtributos(string[,] informacoes)
        {
            //LOG dispensado (valor insignificante)

            try
            {
                cboAtributo.Items.Clear();
                int intTmp001 = (informacoes.Length / 3) - 1;
                for (int i = 0; i < intTmp001; i++)
                {
                    cboAtributo.Items.Add(informacoes[i, 0]);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
        }
        public void Preenche_cboValor(int indexSelecao)
        {
            //método destinado a encher o segundo listbox, com valores únicos e ordenados da coluna selecionada

            //LOG dispensado (valor insignificante)

            cboValor.Items.Clear();
            try
            {
                intListAtributos = indexSelecao;

                string strTmp001 = strParametrosPesquisa[indexSelecao, 1]; //pega o nome da coluna
                int intTmp001 = dtTabelaPA.Columns[strTmp001].Ordinal; // indica a posição da coluna na tabela
                //intTmp001 = ;
                string strTmp002 = dtTabelaPA.Columns[strTmp001].DataType.ToString(); //pega o tipo de dado da coluna
                //DataRow[] drLinhas = dtTabelaPB.Select(); //faz a seleção de todas as linhas (não sei pra que)
                Dados dd = new Dados();

                //segundo o tipo de dados da coluna, o programa inicia a montagem correta, 
                //incluindo aspas nas strings;

                switch (strTmp002)
                {
                    case "System.String":
                        string[] strLista = new string[dtTabelaPA.Rows.Count - 1];

                        //transporta os valores da coluna selecionada para um Array, para que possa ser ordenado
                        for (int i = 0; i < dtTabelaPA.Rows.Count - 1; i++) //resgata os valores da tabela em um array
                        {
                            //strLista[i] = drLinhas[i].ItemArray[intTmp001].ToString();
                            strLista[i] = dtTabelaPA.Rows[i].ItemArray[intTmp001].ToString();
                        }

                        //faz o retorno de apenas valores unicos e ordena os valores
                        int intTmp002 = dd.GetDistinctValues<string>(strLista).Length;
                        string[] strTmp003 = new string[intTmp002];
                        strTmp003 = dd.GetDistinctValues<string>(strLista);
                        Array.Sort(strTmp003);

                        //preenche o cboValor
                        for (int i = 0; i < intTmp002; i++)
                        {
                            cboValor.Items.Add("'" + strTmp003[i] + "'");
                        }
                        break;


                    case "System.Int":
                        int[] intLista = new int[dtTabelaPA.Rows.Count - 1]; // drLinhas.Length 

                        //transporta os valores da coluna selecionada para um Array, para que possa ser ordenado
                        for (int i = 0; i < dtTabelaPA.Rows.Count - 1; i++) //resgata os valores da tabela em um array (drLinhas.Length)
                        {
                            string strIntString = dtTabelaPA.Rows[i].ItemArray[intTmp001].ToString();
                            //drLinhas[i].ItemArray[intTmp001].ToString();
                            if (strIntString != "")
                            {
                                intLista[i] = System.Int32.Parse(strIntString);
                            }
                            else
                            {
                                intLista[i] = -1;
                            }
                        }

                        //faz o retorno de apenas valores unicos e ordena os valores
                        int intTmp003 = dd.GetDistinctValues<int>(intLista).Length;
                        int[] intTmp004 = new int[intTmp003];
                        intTmp004 = dd.GetDistinctValues<int>(intLista);
                        Array.Sort(intTmp004);

                        //preenche o cboValor
                        for (int i = 0; i < intTmp001; i++)
                        {
                            cboValor.Items.Add(intTmp004[i].ToString());
                        }
                        break;

                    case "System.Double":
                        double[] dblLista = new double[dtTabelaPA.Rows.Count - 1];

                        for (int i = 0; i < dtTabelaPA.Rows.Count - 1; i++) //resgata os valores da tabela em um array
                        {
                            string strDoubleString = dtTabelaPA.Rows[i].ItemArray[intTmp001].ToString();
                            //drLinhas[i].ItemArray[intTmp001].ToString();
                            if (strDoubleString != "")
                            {
                                dblLista[i] = System.Double.Parse(strDoubleString);
                            }
                            else
                            {
                                dblLista[i] = -1;
                            }
                        }
                        //faz o retorno de apenas valores unicos e ordena os valores

                        int intTmp003_d = dd.GetDistinctValues<double>(dblLista).Length;
                        double[] dblTmp004_d = new double[intTmp003_d];
                        dblTmp004_d = dd.GetDistinctValues<double>(dblLista);
                        Array.Sort(dblTmp004_d);

                        //preenche o cboValor
                        for (int i = 0;
                            i < intTmp003_d;
                            i++)
                        {
                            string strValor = dblTmp004_d[i].ToString();
                            strValor = strValor.Replace(',', '.');
                            cboValor.Items.Add(strValor);
                            //cboValor.Items.Add(dblTmp004_d[i].ToString());
                        }
                        break;
                    case "System.Decimal":
                        decimal[] dcmLista = new decimal[dtTabelaPA.Rows.Count - 1];

                        for (int i = 0; i < dtTabelaPA.Rows.Count - 1; i++) //resgata os valores da tabela em um array
                        {
                            string strDecimalString = dtTabelaPA.Rows[i].ItemArray[intTmp001].ToString();
                            //drLinhas[i].ItemArray[intTmp001].ToString();
                            if (strDecimalString != "")
                            {
                                dcmLista[i] = System.Decimal.Parse(strDecimalString);
                            }
                            else
                            {
                                dcmLista[i] = -1;
                            }
                        }
                        //faz o retorno de apenas valores unicos e ordena os valores

                        int intTmp003_dc = dd.GetDistinctValues<decimal>(dcmLista).Length;
                        decimal[] dblTmp004_dc = new decimal[intTmp003_dc];
                        dblTmp004_dc = dd.GetDistinctValues<decimal>(dcmLista);
                        Array.Sort(dblTmp004_dc);

                        //preenche o cboValor
                        for (int i = 0;
                            i < intTmp003_dc;
                            i++)
                        {
                            string strValor = dblTmp004_dc[i].ToString();
                            strValor = strValor.Replace(',', '.');
                            cboValor.Items.Add(strValor);
                            //cboValor.Items.Add(dblTmp004_dc[i].ToString());
                        }
                        break;


                    //=================
                    // pra baixo nao
                    //=================

                    case "System.Boolean":
                        bool[] boolLista = new bool[dtTabelaPA.Rows.Count - 1];

                        for (int i = 0; i < dtTabelaPA.Rows.Count - 1; i++) //resgata os valores da tabela em um array
                        {
                            string strBoolString = dtTabelaPA.Rows[i].ItemArray[intTmp001].ToString();
                            //drLinhas[i].ItemArray[intTmp001].ToString();
                            if (strBoolString != "")
                            {
                                boolLista[i] = System.Boolean.Parse(strBoolString);
                            }
                            else
                            {
                                boolLista[i] = false;
                            }
                        }
                        //faz o retorno de apenas valores unicos e ordena os valores

                        int intTmp003_b = dd.GetDistinctValues<bool>(boolLista).Length;
                        bool[] dblTmp004_b = new bool[intTmp003_b];
                        dblTmp004_b = dd.GetDistinctValues<bool>(boolLista);
                        Array.Sort(dblTmp004_b);

                        //preenche o cboValor
                        for (int i = 0;
                            i < intTmp003_b;
                            i++)
                        {
                            cboValor.Items.Add(dblTmp004_b[i].ToString());
                        }
                        break;
                }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
        }
        public bool Pesquisa(string sintaxe, bool ehpesquisa)
        {
            bool PesquisaCorreta = false;
            string[] strResultado = { "", "", "", "", "" };
            int parte = 0;
            try
            {
                DataRow[] dr1 = dtTabelaPA.Select(sintaxe);
                int intTmp001 = dr1.Length;

                if (sintaxe != "")
                {
                    double dblTmp001 = 0;
                    for (int i = 0; i < intTmp001; i++)
                    {
                        int intColArea = dtTabelaPA.Columns[strPrmAdicional[5]].Ordinal;//int.Parse(tmp001);
                        int intColShape = dtTabelaPA.Columns[strPrmAdicional[6]].Ordinal;

                        string strTmp001 = dr1[i][intColArea].ToString();
                        dblTmp001 = dblTmp001 + System.Double.Parse(strTmp001);

                        string ColunaDataTipo = dtTabelaPA.Columns[strPrmAdicional[6]].DataType.ToString();

                        if (strResultado[parte].Length > 0)
                        {
                            if (ColunaDataTipo == "System.String")
                            {
                                strResultado[parte] = strResultado[parte] + " OR " + strPrmAdicional[0] +
                                    "='" + dr1[i][intColShape].ToString() + "'";
                            }
                            else
                            {
                                strResultado[parte] = strResultado[parte] + " OR " + strPrmAdicional[0] +
                                    "=" + dr1[i][intColShape].ToString() + "";
                            }

                        }
                        else
                        {
                            if (ColunaDataTipo == "System.String")
                            {
                                strResultado[parte] = strPrmAdicional[0] + "=" + dr1[i][intColShape].ToString();
                            }
                            else
                            {
                                strResultado[parte] = strPrmAdicional[0] + "=" + dr1[i][intColShape].ToString() + "";
                            }
                        }

                        if (strResultado[parte].Length > 30000)
                        {
                            parte = parte + 1;
                        }

                    }
                    string strResulFim = strResultado[0] + "*" + strResultado[1] + "*" + strResultado[2] +
                        "*" + strResultado[3] + "*" + strResultado[4];
                    //dblTmp001 = dblTmp001 / 10000;
                    dblTmp001 = Math.Round(dblTmp001, 1);

                    switch (intTmp001)
                    {
                        case 0:
                            MessageBox.Show("A sintaxe está correta, entretanto, não foram encontrados polígonos"
                                + " que satisfaçam as condições da sintaxe elaborada.");
                            break;

                        default:
                            Dados dd = new Dados();
                            int temp001 = int.Parse(strPrmAdicional[4]);
                            MessageBox.Show("A área encontrada é de " + dblTmp001.ToString() + " " + dd.RetornaUnidade(0, temp001));
                            if (ehpesquisa)
                            {
                                PesquisaCorreta = true;
                                prpStrAvancada = strResulFim;
                            }
                            if (chkRelatorio.Checked)
                            {
                                frmFormatRel FormataRelatorio = new frmFormatRel(sintaxe, dtTabelaPA,
                                    strParametrosPesquisa, strPrmAdicional);
                                FormataRelatorio.prpFalso = this.prpRel;
                                FormataRelatorio.ShowDialog(this);
                            }
                            break;
                    }//switch
                }//if
                else
                {
                    if (chkRelatorio.Checked)
                    {
                        frmFormatRel FormataRelatorio = new frmFormatRel(sintaxe, dtTabelaPA,
                            strParametrosPesquisa, strPrmAdicional);
                        FormataRelatorio.prpFalso = this.prpRel;
                        FormataRelatorio.ShowDialog(this);
                    }
                    else
                    {

                        MessageBox.Show("A caixa de pesquisa está vazia.");
                    }
                }
            }//try
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\r\nOcorreu um erro com a sintaxe. Verifique");
            }
            //this.UseWaitCursor = false;

            return PesquisaCorreta;
        }
        private void PesquisaAreaEstudo(DataRow[] Linhas)
        {
            try
            {
                DataTable tabela = new DataTable();
                for (int i = 0; i < dtTabelaPA.Columns.Count; i++)
                {
                    DataColumn coluna = new DataColumn(dtTabelaPA.Columns[i].ColumnName, dtTabelaPA.Columns[i].DataType);
                    tabela.Columns.Add(coluna);
                }
                for (int i = 0; i < Linhas.Length - 1; i++)
                {
                    //DataRow linha; 

                    //linha.ItemArray = Linhas[i].ItemArray;
                    tabela.Rows.Add(Linhas[i].ItemArray);
                }
                //dtTabelaPA.Clear();
                dtTabelaPA = tabela;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message);
            }
        }


        //=========================================
        //             Propriedades
        //=========================================
        public DataTable prpAvancada
        {
            get { return this.dtTabelaPA; }
            set { this.dtTabelaPA = value; }
        }
        public string prpStrAvancada
        {
            get { return this.strInformativos; }
            set { this.strInformativos = value; }
        }
        public string prpAdicionais
        {
            get { return this.strAdicional; }
            set { this.strAdicional = value; }
        }
        public bool prpRel
        {
            get { return this.bolFormatRel; }
            set { this.bolFormatRel = value; }
        }

        //=========================================
        //             Eventos Diversos
        //=========================================
        private void txtSQLSintaxe_Click(object sender, EventArgs e)
        {
            intPosCursor = txtSQLSintaxe.SelectionStart;
        }
        private void txtSQLSintaxe_KeyUp(object sender, KeyEventArgs e)
        {
            intPosCursor = txtSQLSintaxe.SelectionStart;
        }
        private void frmPesquisaAvancada_Load(object sender, EventArgs e)
        {
            QuebraParametros(strInformativos);
            Preenche_cboAtributos(strParametrosPesquisa);

            QuebraParametros2();
            cboAtributo.SelectedIndex = 0;
            Preenche_cboValor(0);
            cboValor.SelectedIndex = 0;
            //lstRelacional.SelectedIndex = 0;
            //lstLogico.SelectedIndex = 0;
        }
        private void cboAtributo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (intListAtributos != cboAtributo.SelectedIndex)
            {
                int intTmp001 = cboAtributo.SelectedIndex;
                Preenche_cboValor(intTmp001);
                intListAtributos = cboAtributo.SelectedIndex;
            }
        }
        private void cboAtributo_Click(object sender, EventArgs e)
        {
            DateTime dtf = new DateTime();
            dtf = DateTime.Now;
            TimeSpan dtd = dtf.TimeOfDay;

            double diferencaTemp = dtd.TotalSeconds - SegundosAtr;

            if (diferencaTemp < 0.5 & indiceCBOAtr == cboAtributo.SelectedIndex)
            {
                int intTmp001 = cboAtributo.SelectedIndex;
                //Preenche_cboValor(intTmp001);
                string strTmp001 = strParametrosPesquisa[intTmp001, 1];
                AdicionaSinal(strTmp001);
                intPosCursor += strTmp001.Length + 2;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }

            SegundosAtr = dtd.TotalSeconds;
            indiceCBOAtr = cboAtributo.SelectedIndex;
        }
        private void cboValor_Click(object sender, EventArgs e)
        {
            DateTime dtf = new DateTime();
            dtf = DateTime.Now;
            TimeSpan dtd = dtf.TimeOfDay;

            double diferencaTemp = dtd.TotalSeconds - SegundosVal;

            if (diferencaTemp < 0.5 & indiceCBOVal == cboValor.SelectedIndex)
            {
                int tmp001 = cboValor.SelectedIndex;
                string tmp002 = cboValor.Items[tmp001].ToString();
                AdicionaSinal(tmp002);
                intPosCursor += tmp002.Length + 2;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }

            SegundosVal = dtd.TotalSeconds;
            indiceCBOVal = cboValor.SelectedIndex;
        }

        //botões aritméticos
        private void btnIgual_Click(object sender, EventArgs e)
        {
            AdicionaSinal("=");
            intPosCursor += 3;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnDiferente_Click(object sender, EventArgs e)
        {
            AdicionaSinal("<>");
            intPosCursor += 4;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();

        }
        private void btnMaior_Click(object sender, EventArgs e)
        {
            AdicionaSinal(">");
            intPosCursor += 3;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnMenor_Click(object sender, EventArgs e)
        {
            AdicionaSinal("<");
            intPosCursor += 3;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnMaiorIgual_Click(object sender, EventArgs e)
        {
            AdicionaSinal(">=");
            intPosCursor += 4;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnMenorIgual_Click(object sender, EventArgs e)
        {
            AdicionaSinal("<=");
            intPosCursor += 4;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnMenos_Click(object sender, EventArgs e)
        {
            AdicionaSinal("-");
            intPosCursor += 3;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnMais_Click(object sender, EventArgs e)
        {
            AdicionaSinal("+");
            intPosCursor += 3;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnMultiplica_Click(object sender, EventArgs e)
        {
            AdicionaSinal("*");
            intPosCursor += 3;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnDivide_Click(object sender, EventArgs e)
        {
            AdicionaSinal("/");
            intPosCursor += 3;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnEleva_Click(object sender, EventArgs e)
        {
            AdicionaSinal("Like");
            intPosCursor += 6;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        //botões condicionais
        private void btnE_Click(object sender, EventArgs e)
        {
            AdicionaSinal("AND");
            intPosCursor += 5;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnOU_Click(object sender, EventArgs e)
        {
            AdicionaSinal("OR");
            intPosCursor += 4;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        private void btnNAO_Click(object sender, EventArgs e)
        {
            AdicionaSinal("NOT");
            intPosCursor += 5;
            txtSQLSintaxe.SelectionStart = intPosCursor;
            txtSQLSintaxe.Focus();
        }
        //botões de ação
        private void btnLimpa_Click(object sender, EventArgs e)
        {
            txtSQLSintaxe.Text = "";
            intPosCursor = 0;
            txtSQLSintaxe.Focus();
        }
        private void btnVerifica_Click(object sender, EventArgs e)
        {
            
            bool bobo = Pesquisa(txtSQLSintaxe.Text, false);
        }
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            //
            //abre caixa de diálogo para salvar formula de pesquisa
            //
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Abrir pesquisa";
            fDialog.Filter = "Arquivos TXT|*.TXT|Todos|*.*";
            fDialog.InitialDirectory = strPrmAdicional[7] + @"usuario\";

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader srArquivo = new StreamReader(fDialog.FileName);
                txtSQLSintaxe.Text = srArquivo.ReadToEnd();
                srArquivo.Close();
            }
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sDialog = new SaveFileDialog();
            sDialog.Title = "Salvar pesquisa";
            sDialog.Filter = "TXT Files|*.TXT|All Files|*.*";
            sDialog.InitialDirectory = strPrmAdicional[7] + @"usuario\";

            if (sDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter swArquivo = new StreamWriter(sDialog.FileName);
                swArquivo.Write(txtSQLSintaxe.Text);
                swArquivo.Close();
            }
        }
        private void btnCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            string strTmp001 = txtSQLSintaxe.Text;
            if (Pesquisa(strTmp001, true))
            {
                lock (this)
                {
                    this.UseWaitCursor = true;
                    Thread.Sleep(1000);
                }
                if (this.Owner != null && this.Owner is frmPrincipal)
                    ((frmPrincipal)this.Owner).ExecutaSelecao(this.prpStrAvancada);
                this.UseWaitCursor = false;
                this.Close();
            }
        }
        private void lblAreaEstudo_Click(object sender, EventArgs e)
        {
            string strTmp001 = txtSQLSintaxe.Text;
            if (Pesquisa(strTmp001, true))
            {
                lock (this)
                {
                    this.UseWaitCursor = true;
                    Thread.Sleep(1000);
                }
                DataRow[] dr1 = dtTabelaPA.Select(txtSQLSintaxe.Text);
                PesquisaAreaEstudo(dr1);

                if (this.Owner != null && this.Owner is frmPrincipal)
                    ((frmPrincipal)this.Owner).ExecutaAreaEstudo(this.prpStrAvancada,this.prpAvancada);

                this.UseWaitCursor = false;
                this.Close();
            }
        }
        //funçoes
        private void btnCount_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("Count(");
                intPosCursor += compSelecao + 8;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("Count()");
                intPosCursor += 7;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnSum_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("Sum(");
                intPosCursor += compSelecao + 6;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("Sum()");
                intPosCursor += 5;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnAvg_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("Avg(");
                intPosCursor += compSelecao + 6;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("Avg()");
                intPosCursor += 5;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("Min(");
                intPosCursor += compSelecao + 6;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("Min()");
                intPosCursor += 5;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnVar_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("Var(");
                intPosCursor += compSelecao + 6;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("Var()");
                intPosCursor += 5;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("In(");
                intPosCursor += compSelecao + 5;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("In()");
                intPosCursor += 4;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnMax_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("Max(");
                intPosCursor += compSelecao + 6;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("Max()");
                intPosCursor += 5;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnStDev_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("StDev(");
                intPosCursor += compSelecao + 8;//numero total de caracteres faz o cursor parar no fim da selecao
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("StDev()");
                intPosCursor += 7; //numero total de caracteres faz o cursor parar entre os ()
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnParenteses_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("(");
                intPosCursor += compSelecao + 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("()");
                intPosCursor += 2;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnIIF_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("IIF(");
                intPosCursor += compSelecao + 6;//numero total de caracteres faz o cursor parar no fim da selecao
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("IIF()");
                intPosCursor += 5; //numero total de caracteres faz o cursor parar entre os ()
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("Convert(");
                intPosCursor += compSelecao + 10;//numero total de caracteres faz o cursor parar no fim da selecao
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("Convert()");
                intPosCursor += 9; //numero total de caracteres faz o cursor parar entre os ()
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnIsNull_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("IsNull(");
                intPosCursor += compSelecao + 9;//numero total de caracteres faz o cursor parar no fim da selecao
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("IsNull()");
                intPosCursor += 8; //numero total de caracteres faz o cursor parar entre os ()
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnTrim_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("Trim(");
                intPosCursor += compSelecao + 7;//numero total de caracteres faz o cursor parar no fim da selecao
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("Trim()");
                intPosCursor += 6; //numero total de caracteres faz o cursor parar entre os ()
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnLen_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("Len(");
                intPosCursor += compSelecao + 6;//numero total de caracteres faz o cursor parar no fim da selecao
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("Len()");
                intPosCursor += 5; //numero total de caracteres faz o cursor parar entre os ()
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
        }
        private void btnSubString_Click(object sender, EventArgs e)
        {
            if (txtSQLSintaxe.SelectionLength > 0)
            {
                int compSelecao = txtSQLSintaxe.SelectionLength;
                intPosCursor = txtSQLSintaxe.SelectionStart;
                AdicionaSinal("SubString(");
                intPosCursor += compSelecao + 12;//numero total de caracteres faz o cursor parar no fim da selecao
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

                AdicionaSinal(")");
                intPosCursor += 3;
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();

            }
            else
            {
                AdicionaSinal("SubString()");
                intPosCursor += 11; //numero total de caracteres faz o cursor parar entre os ()
                txtSQLSintaxe.SelectionStart = intPosCursor;
                txtSQLSintaxe.Focus();
            }
            
        }
    }//fim da classe
}//fim do namespace