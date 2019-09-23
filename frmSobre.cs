using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZonView017
{
    public partial class frmSobre : Form
    {

        string PathLogoProj;
        string infoProj;
        string versao;

        public frmSobre(string caminhoImagem, string infoProjeto, double VerProj)
        {
            InitializeComponent();
            PathLogoProj = caminhoImagem;
            infoProj = infoProjeto;
            versao = VerProj.ToString();
        }

        private void frmSobre_Load(object sender, EventArgs e)
        {
            txtInfoSoft.Text = "ViZon\r\n" +
            "Versão " + versao + "\r\n" +
            "Embrapa Solos / UEP Recife\r\n" +
            "Rua Antônio Falcão, 402 - Boa Viagem\r\n" +
            "Recife, PE - Brasil - CEP 51020-240\r\n" +
            "Tel.: (81) 3198 7800 \r\n" +
            "www.uep.cnps.embrapa.br\r\n" +
            "Todos os direitos reservados, conforme Lei n° 9.610.\r\n" +
            "    \r\n" +
            "**********************\r\n" +
            "Partes deste programa de computador são de propriedade da LizarTech, Inc."+
            " e seus direitos autorais pertencem à LizardThech e/ou à University of California "+
            "(Copyright © 1995-2002 LizardThech, Inc e/ou University of California). "+
            "Todos os direitos reservados. Patente dos Estados Unidos n° 5.710.835.\r\n";



            try
            {
                if (PathLogoProj != "" & infoProj != "")
                {
                    picLogoProj.Load(PathLogoProj);
                    txtInfoProj.Text = infoProj;
                    frmSobre.ActiveForm.Width = 963;
                    btnOK.Left = 870;
                }
                else
                {
                    frmSobre.ActiveForm.Width = 496;
                    btnOK.Left = 372;
                    this.Width = 470;
                }
                txtInfoSoft.Select(0, 0);

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Um erro ocorreu com este projeto. Entre em contato com seu desenvolvedor informando o erro: \r\n"+
                    Ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}