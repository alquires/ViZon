using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net.Mail;

namespace ZonView017
{
    class LogUso
    {
        public LogUso()
        {
        }

        /// <summary>
        /// Monta o log de uso
        /// </summary>
        /// <param name="TempoExec">Tempo de execução do processo</param>
        /// <param name="Evento">nome do projeto</param>
        public void MontaLog(string TempoExec, string Evento, string Projeto)
        {
            RegistryKey RegKey = Registry.LocalMachine;
            RegKey = RegKey.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
            Object cpuSpeed = RegKey.GetValue("~MHz");
            Object cpuType = RegKey.GetValue("VendorIdentifier");
            //Console.WriteLine("You have a {0} running at {1} MHz.", cpuType, cpuSpeed);             
            
            OperatingSystem os = Environment.OSVersion;

            string NumProc = Environment.ProcessorCount.ToString();
            string Memoria = Environment.WorkingSet.ToString();

            string strLog = cpuType + ";" + cpuSpeed + ";" + 
                os.Platform.ToString() + ";" + os.ServicePack + ";" + os.VersionString +
                ";" + TempoExec + ";" + Evento;
            GravaLog(strLog);
        }

        /// <summary>
        /// faz a gravação do logo no arquivo de log
        /// </summary>
        /// <param name="LOG">Log de uso a ser gravado</param>
        private void GravaLog(string LOG)
        {
            string CaminhoLOG = Application.StartupPath + "\\LOG.txt";
            StreamReader sr = new StreamReader(CaminhoLOG);
            string TodoLOG = sr.ReadToEnd();
            sr.Close();

            TodoLOG = TodoLOG + "\t\r" + LOG;

            StreamWriter sw = new StreamWriter(CaminhoLOG);
            sw.Write(TodoLOG);
            sw.Close();
        }

        /// <summary>
        /// Questiona se o usuário deseja enviar o log de uso
        /// </summary>
        public void Questiona()
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 10);
            if (i < 3)
            {
                string mensagem = "Esta é uma versão de testes. \t\r" +
                    "Gostaria de auxiliar no desenvolvimento nos enviando o arquivo de registro de uso?\t\r" +
                    "Este arquivo não contém informações pessoais, e pode ser lido em: " +
                    Application.StartupPath + "LOG.txt";

                if (MessageBox.Show(mensagem, "Ajude-nos a melhorar este software", MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
                {
                    string CaminhoLOG = Application.StartupPath + "\\LOG.txt";
                    StreamReader sr = new StreamReader(CaminhoLOG);
                    string TodoLOG = sr.ReadToEnd();
                    sr.Close();

                    EnviaLog(TodoLOG);

                    StreamWriter sw = new StreamWriter(CaminhoLOG);
                    sw.Write("");
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// envia o log via email
        /// </summary>
        /// <param name="Mensagem">texto do arquivo de log a ser enviado</param>
        private void EnviaLog(string Mensagem)
        {
            try
            {
                //Command line argument must the the SMTP host.
                SmtpClient client = new SmtpClient("smtp.uep.cnps.embrapa.br");

                // Specify the e-mail sender.
                // Create a mailing address that includes a UTF8 character
                // in the display name.

                MailAddress from = new MailAddress("vizon@uep.cnps.embrapa.br",
                    "ViZon",
                    System.Text.Encoding.UTF8);

                // Set destinations for the e-mail message.
                MailAddress to = new MailAddress("lab.geo.embrapa.solos@gmail.com");

                // Specify the message content.
                MailMessage message = new MailMessage(from, to);
                message.Body = Mensagem;

                // Include some non-ASCII characters in body and subject.
                message.Body += Environment.NewLine;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = "[LOG - Vizon]";
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                string userState = "test message1";
                client.SendAsync(message, userState);
                MessageBox.Show("O registro foi enviado com sucesso! Obrigado!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }// fim classe
}// fim namespace
