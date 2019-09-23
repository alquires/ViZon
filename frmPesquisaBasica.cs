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
    public partial class frmPesquisaBasica : Form
    {
        //=========================================
        //          Declarações de Variaveis
        //=========================================
        DataTable dtTabelaPB = new DataTable();
        string strInformativos;
        string strAdicional;
        string[] strPrmAdicional;
        string[,] strParametrosPesquisa;
        bool bolFormatRel = false;

        //=========================================
        //             Propriedades
        //=========================================

        public DataTable prpBasica
        {
            get { return this.dtTabelaPB; }
            set { this.dtTabelaPB = value; }
        }
        public string prpStrBasica
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
        //                Métodos
        //  A maioria desses metodos poderiam ir
        //      para uma nova classe, não?
        //=========================================
        public frmPesquisaBasica()
        {
            InitializeComponent();
        }
        private void QuebraParametros(string parametros)
        {
            //LOG dispensado (valor insignificante)

            try
            {//string strTmp001 = "";
                parametros = parametros.Replace("\n", "");
                //strTmp001 = strTmp001
                string[] strTmp002;
                strTmp002 = parametros.Split('\r');
                int intTmp001;
                intTmp001 = strTmp002.Length;
                int intTmp002;
                intTmp002 = strTmp002[0].Split('#').Length;

                strParametrosPesquisa = new string[intTmp001, intTmp002];

                for (int i = 0; i < intTmp001; i++)
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
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n"+
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
        public void Preenche_lstAtributos(string[,] informacoes)
        {
            //LOG dispensado (valor insignificante)

            try
            {
                //lstAtributo2.Items.Clear();
                cboAtributo.Items.Clear();
                int intTmp001 = (informacoes.Length / 3);
                for (int i = 0; i < intTmp001; i++)
                {
                    //lstAtributo2.Items.Add(informacoes[i, 0]);
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
                string strTmp001 = strParametrosPesquisa[indexSelecao, 1]; //pega o nome da coluna
                int intTmp001 = dtTabelaPB.Columns[strTmp001].Ordinal; // indica a posição da coluna na tabela
                //intTmp001 = ;
                string strTmp002 = dtTabelaPB.Columns[strTmp001].DataType.ToString(); //pega o tipo de dado da coluna
                //DataRow[] drLinhas = dtTabelaPB.Select(); //faz a seleção de todas as linhas (não sei pra que)
                Dados dd = new Dados();

                //segundo o tipo de dados da coluna, o programa inicia a montagem correta, 
                //incluindo aspas nas strings;

                switch (strTmp002)
                {
                    case "System.String":
                        string[] strLista = new string[dtTabelaPB.Rows.Count - 1];

                        //transporta os valores da coluna selecionada para um Array, para que possa ser ordenado
                        for (int i = 0; i < dtTabelaPB.Rows.Count - 1; i++) //resgata os valores da tabela em um array
                        {
                            //strLista[i] = drLinhas[i].ItemArray[intTmp001].ToString();
                            strLista[i] = dtTabelaPB.Rows[i].ItemArray[intTmp001].ToString();
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
                        int[] intLista = new int[dtTabelaPB.Rows.Count - 1]; // drLinhas.Length 

                        //transporta os valores da coluna selecionada para um Array, para que possa ser ordenado
                        for (int i = 0; i < dtTabelaPB.Rows.Count - 1; i++) //resgata os valores da tabela em um array (drLinhas.Length)
                        {
                            string strIntString = dtTabelaPB.Rows[i].ItemArray[intTmp001].ToString();
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
                        double[] dblLista = new double[dtTabelaPB.Rows.Count - 1];

                        for (int i = 0; i < dtTabelaPB.Rows.Count - 1; i++) //resgata os valores da tabela em um array
                        {
                            string strDoubleString = dtTabelaPB.Rows[i].ItemArray[intTmp001].ToString();
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
                        }
                        break;


                        //=================
                        // pra cima nao
                        //=================
                    case "System.Decimal":
                        decimal[] dcmLista = new decimal[dtTabelaPB.Rows.Count - 1];

                        for (int i = 0; i < dtTabelaPB.Rows.Count - 1; i++) //resgata os valores da tabela em um array
                        {
                            string strDecimalString = dtTabelaPB.Rows[i].ItemArray[intTmp001].ToString();
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
                        bool[] boolLista = new bool[dtTabelaPB.Rows.Count - 1];

                        for (int i = 0; i < dtTabelaPB.Rows.Count - 1; i++) //resgata os valores da tabela em um array
                        {
                            string strBoolString = dtTabelaPB.Rows[i].ItemArray[intTmp001].ToString();
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
        /// <summary>
        /// Realiza a pesquisa na tabela de acordo com a sintaxe definida pelo usuário
        /// </summary>
        /// <param name="sintaxe">sintaxe de pesquisa criada pelo usuário</param>
        /// <param name="ehpesquisa">valor para determinar se é uma pesquisa ou uma verificação</param>
        /// <returns>retorna se a pesquisa teve resultados corretos, evitando a execução erronea do software</returns>
        public bool Pesquisa(string sintaxe,bool ehpesquisa)
        {
            bool PesquisaCorreta = false;
            string[] strResultado = {"","","","",""};
            int parte = 0;

            try
            {
                DataRow[] dr1 = dtTabelaPB.Select(sintaxe);
                
                int intTmp001 = dr1.Length;

                if (sintaxe != "")
                {
                    double dblTmp001 = 0;
                    for (int i = 0; i < intTmp001; i++)
                    {
                        int intColArea = dtTabelaPB.Columns[strPrmAdicional[5]].Ordinal;//int.Parse(tmp001);
                        int intColShape = dtTabelaPB.Columns[strPrmAdicional[6]].Ordinal;

                        string strTmp001 = dr1[i][intColArea].ToString();
                        dblTmp001 = dblTmp001 + System.Double.Parse(strTmp001);

                        string ColunaDataTipo = dtTabelaPB.Columns[strPrmAdicional[6]].DataType.ToString();

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
                                prpStrBasica = strResulFim;
                            }
                            if (chkRelatorio.Checked)
                            {
                                frmFormatRel FormataRelatorio = new frmFormatRel(sintaxe, dtTabelaPB,
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
                        frmFormatRel FormataRelatorio = new frmFormatRel(sintaxe, dtTabelaPB,
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


            return PesquisaCorreta;
        }
        public bool AreaEstudo(string sintaxe, bool ehAreaEstudo)
        {
            // e agora?
            bool PesquisaCorreta = false;
            string[] strResultado = { "", "", "", "", "" };
            int parte = 0;

            try
            {
                //DataRow[] dr1 = dtTabelaPB.Select(sintaxe);
                
                DataTable dtTemp = dtTabelaPB.Copy();
                DataRow[] drTemp = dtTemp.Select(sintaxe);
                int intTmp001 = drTemp.Length;

                // testa se há sintaxe escrita
                if (sintaxe != "")
                {
                    // remove as linhas que satisfazem a pesquisa inicial
                    //isso para gerar a mascara no externo, que é o resto
                    //da pesquisa
                    for (int i = 0; i < intTmp001; i++)
                    {
                        dtTemp.Rows.Remove(drTemp[i]);
                    }
                    DataRow[] dr2 = dtTemp.Select();
                    //refaz a pesquisa inicial pra evitar erros
                    DataRow[] dr1 = dtTabelaPB.Select(sintaxe);
                    PesquisaAreaEstudo(dr1);

                    // =========================================================

                    intTmp001 = dr2.Length;
                    for (int i = 0; i < intTmp001; i++)
                    {
                        int intColArea = dtTemp.Columns[strPrmAdicional[5]].Ordinal;//int.Parse(tmp001);
                        int intColShape = dtTemp.Columns[strPrmAdicional[6]].Ordinal;

                        
                        //string strTmp001 = dr1[i][intColArea].ToString(); //dr1 pq é o conteúdo original
                        //dblTmp001 = dblTmp001 + System.Double.Parse(strTmp001);

                        string ColunaDataTipo = dtTemp.Columns[strPrmAdicional[6]].DataType.ToString();


                        // *** Montagem da sintaxe de seleção da área ***
                        if (strResultado[parte].Length > 0)
                        {
                            if (ColunaDataTipo == "System.String")
                            {
                                strResultado[parte] = strResultado[parte] + " OR " + strPrmAdicional[0] +
                                    "='" + dr2[i][intColShape].ToString() + "'";
                            }
                            else
                            {
                                strResultado[parte] = strResultado[parte] + " OR " + strPrmAdicional[0] +
                                    "=" + dr2[i][intColShape].ToString() + "";
                            }

                        }
                        else
                        {
                            if (ColunaDataTipo == "System.String")
                            {
                                strResultado[parte] = strPrmAdicional[0] + "=" + dr2[i][intColShape].ToString();
                            }
                            else
                            {
                                strResultado[parte] = strPrmAdicional[0] + "=" + dr2[i][intColShape].ToString() + "";
                            }
                        }

                        if (strResultado[parte].Length > 30000)
                        {
                            parte = parte + 1;
                        }

                    }
                    string strResulFim = strResultado[0] + "*" + strResultado[1] + "*" + strResultado[2] +
                        "*" + strResultado[3] + "*" + strResultado[4];


                    if (ehAreaEstudo)
                    {
                        PesquisaCorreta = true;
                        prpStrBasica = strResulFim;
                    }
                    if (chkRelatorio.Checked)
                    {
                        frmFormatRel FormataRelatorio = new frmFormatRel(sintaxe, dtTabelaPB,
                            strParametrosPesquisa, strPrmAdicional);
                        FormataRelatorio.prpFalso = this.prpRel;
                        FormataRelatorio.ShowDialog(this);
                    }
                    // **************************************************************
                }
                else
                {
                    if (chkRelatorio.Checked)
                    {
                        frmFormatRel FormataRelatorio = new frmFormatRel(sintaxe, dtTabelaPB,
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
            

            return PesquisaCorreta;
        }
        private void PesquisaAreaEstudo(DataRow[] Linhas)
        {
            try
            {
                DataTable tabela = new DataTable();
                for (int i = 0; i < dtTabelaPB.Columns.Count; i++)
                {
                    DataColumn coluna = new DataColumn(dtTabelaPB.Columns[i].ColumnName, dtTabelaPB.Columns[i].DataType);
                    tabela.Columns.Add(coluna);
                }
                for (int i = 0; i < Linhas.Length - 1; i++)
                {
                    tabela.Rows.Add(Linhas[i].ItemArray);
                }
                dtTabelaPB = tabela;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n" +
                    Ex.Message + " PesquisaAreaEstudo()");
            }

        }

        //=========================================
        //             Eventos Diversos
        //=========================================
        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            
            string strTmp001 = txtSQLSintaxe.Text;
            if (Pesquisa(strTmp001, true))
            {
                this.UseWaitCursor = true;
                btnPesquisa.Enabled = false;

                if (this.Owner != null && this.Owner is frmPrincipal)
                    ((frmPrincipal)this.Owner).ExecutaSelecao(this.prpStrBasica);

                this.UseWaitCursor = false;
                this.Close();
            }
        }
        private void frmPesquisaBasica_Load(object sender, EventArgs e)
        {
            lstRelacional.SelectedIndex = 0;
            lstLogico.SelectedIndex = 0;
            //this.TopLevel = true;
            //frmPesquisaBasica.ActiveForm.Height = 525;
            try
            {
                QuebraParametros(strInformativos);
                QuebraParametros2();
                Preenche_lstAtributos(strParametrosPesquisa);

                //lstAtributo2.SelectedIndex = 0;
                cboAtributo.SelectedIndex = 0;
                Preenche_cboValor(0);
                if (cboValor.Items.Count > 0)
                {
                    cboValor.SelectedIndex = 0;
                }
            }
            catch
            {

            }

        }
        private void lstAtributo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int intTmp001 = lstAtributo2.SelectedIndex;
            int intTmp001 = cboAtributo.SelectedIndex;
            Preenche_cboValor(intTmp001);
        }
        private void btnAdCriterio_Click(object sender, EventArgs e)
        {
            int intSeleLogica = lstLogico.SelectedIndex;
            //int intSeleAtributos = lstAtributo2.SelectedIndex;
            int intSeleAtributos = cboAtributo.SelectedIndex;
            int intSeleRelacional = lstRelacional.SelectedIndex;
            int intSelecValor = cboValor.SelectedIndex;

            //string strValorSQL = "";
            string temp001 = "";
            string temp002 = "";
            string temp003 = "";
            string temp004 = "";

            if ((intSeleAtributos != -1) & (intSelecValor != -1) &
                (intSeleLogica != -1) & (intSeleRelacional != -1))
            {
                temp001 = strParametrosPesquisa[intSeleAtributos, 1];

                temp003 = cboValor.Items[intSelecValor].ToString();

                switch (intSeleRelacional)
                {
                    case 0:
                        temp002 = "=";
                        break;
                    case 1:
                        temp002 = ">";
                        break;
                    case 2:
                        temp002 = "<";
                        break;
                    case 3:
                        temp002 = "<>";
                        break;
                    case 4:
                        temp002 = ">=";
                        break;
                    case 5:
                        temp002 = "<=";
                        break;
                }

                switch (intSeleLogica)
                {
                    case 0:
                        temp004 = "AND";
                        break;
                    case 1:
                        temp004 = "OR";
                        break;
                    case 2:
                        temp004 = "NOT";
                        break;
                }


                if (txtSQLSintaxe.Text == "")
                {
                    txtSQLSintaxe.Text = "( " + temp001 + " " + temp002 + " " + temp003 + " ) ";
                }
                else
                {
                    txtSQLSintaxe.Text = "( " + txtSQLSintaxe.Text + temp004 + " " + temp001 + " " + temp002 + " " + temp003 + " ) ";
                }
            }
        }
        private void btnVerifica_Click(object sender, EventArgs e)
        {
            bool boleano = Pesquisa(txtSQLSintaxe.Text, false);
        }
        private void btnLimpa_Click(object sender, EventArgs e)
        {
            txtSQLSintaxe.Text = "";
        }
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            //
            //abre caixa de diálogo para salvar formula de pesquisa
            //
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Abrir pesquisa";
            fDialog.Filter = "TXT Files|*.TXT|All Files|*.*";
            fDialog.InitialDirectory = strPrmAdicional[7] +  @"usuario\";
            
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader srArquivo = new StreamReader(fDialog.FileName);
                txtSQLSintaxe.Text = srArquivo.ReadToEnd();
                srArquivo.Close();
            }
        }
        private void btnSalva_Click(object sender, EventArgs e)
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
            //frmPrincipal.ActiveForm.Enabled = true;
        }
        private void lblAreaEstudo_Click(object sender, EventArgs e)
        {
            string strTmp001 = txtSQLSintaxe.Text;
            Pesquisa(strTmp001, false); // só para informar a área correta

            if (AreaEstudo(strTmp001, true)) //tá aqui!!
            {
                this.UseWaitCursor = true;
                lblAreaEstudo.Enabled = false;

                if (this.Owner != null && this.Owner is frmPrincipal)
                    ((frmPrincipal)this.Owner).ExecutaAreaEstudo(this.prpStrBasica, this.prpBasica);

                this.UseWaitCursor = false;
                this.Close();
            }
        }
        private void btnAjuda_Click(object sender, EventArgs e)
        {
            bool boleano = Pesquisa(txtSQLSintaxe.Text, false);
        }
        private void cboAtributo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intTmp001 = cboAtributo.SelectedIndex;
            Preenche_cboValor(intTmp001);
        }

    }//classe
}//namespace