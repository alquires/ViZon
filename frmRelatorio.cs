using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;


namespace ZonView017
{
    public partial class frmRelatorio : Form
    {
        string str_DocHTML = "";

        public frmRelatorio(string dadosHTML)
        {
            InitializeComponent();
            str_DocHTML = dadosHTML;
        }
        private void frmRelatorio_Load(object sender, EventArgs e)
        {
            wbrRelatorio.DocumentText = str_DocHTML;
            wbrRelatorio.Refresh();
        }
        private void tsbSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsbImprimir_Click(object sender, EventArgs e)
        {
            wbrRelatorio.ShowPrintDialog();
            /*PrintDialog PD = new PrintDialog();
            if ( == DialogResult.OK)
            {
                
                wbrRelatorio.Print();
            }*/
        }
        private void tsbVerImpressao_Click(object sender, EventArgs e)
        {
            wbrRelatorio.ShowPrintPreviewDialog();
        }
        private void tsbExportar_Click(object sender, EventArgs e)
        {
            wbrRelatorio.ShowSaveAsDialog();
        }
        private void tsbPDF_Click(object sender, EventArgs e)
        {
            ConvertHtmlStringToPDF();


            string texto = "Vamos ver se isso funciona mesmo!\n\n";
            texto += "Mais uma linha de texto aqui\n\n";
            texto += "E mais uma linha\n\n";
            texto += "A última linha";

            // cria uma instância de iTextSharp.text.Document
            Document documento = new Document();

            // cria um Writer para o documento
            PdfWriter.GetInstance(documento, new
              FileStream(@"C:\Geoprocessamento\Visualizador\aprendizado\pdf\exemplo.pdf", FileMode.Create));

            // abre o documento
            documento.Open();

            // adiciona conteúdo
            
            Phrase Frase = new Phrase("Administration -System Users Report", FontFactory.GetFont(FontFactory.HELVETICA, 24,BaseColor.BLUE));
            documento.Add(Frase);
            documento.Add(new Paragraph(texto));

            // fecha o documento
            documento.Close();

            MessageBox.Show("Documento PDF criado com sucesso.");

        }

        public DataTable GetFirstTable()
        {
            DataTable FirstTable = new DataTable();
            // Declare DataColumn and DataRow variables.   
            DataColumn column;
            DataRow row;
            // Create new DataColumn, set DataType, ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            FirstTable.Columns.Add(column);
            // Create second column.          
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "Amount1";
            FirstTable.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "Amount2";
            FirstTable.Columns.Add(column);
            // Create new DataRow objects and add to DataTable. 
            for (int i = 0; i < 15; i++)
            {
                row = FirstTable.NewRow();
                row["id"] = i;
                row["Amount1"] = i.ToString();
                row["Amount2"] = "100" + i.ToString();
                FirstTable.Rows.Add(row);
            }
            return FirstTable;
        }
    
        public void ConvertHtmlStringToPDF()
        {
            /*
            GridView grdTemp = new GridView();
            grdTemp.DataSource = GetFirstTable();
            grdTemp.DataBind();
            HtmlForm form = new HtmlForm();
            form.Controls.Add(grdTemp);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hTextWriter = new HtmlTextWriter(sw);*/
            //form.Controls[0].RenderControl(hTextWriter);
            //string html = sw.ToString();
            string htmlDisplayText = wbrRelatorio.DocumentText;
         
            Document document = new Document();          
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, ms);        
            StringReader se = new StringReader(htmlDisplayText);
            HTMLWorker obj = new HTMLWorker(document);
            
            document.Open();
            obj.Parse(se);
            // step 5: we close the document
            document.Close();      


        }
    }
}
