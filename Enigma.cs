using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ZonView017
{
    class Enigma
    { 
        public Enigma()
        {

        }

        public string Descriptografa(string CaminhoArquivo)
        {
            //LOG dispensado (valor insignificante)

            string TextoDecodificado = "";

            try
            {

                TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider();
                triDes.Key = ASCIIEncoding.ASCII.GetBytes("************************");
                triDes.IV = ASCIIEncoding.ASCII.GetBytes("********");
				// consulte o email "crypto vizon" para saber os parametros certos
				
				
				
				
                //Create a file stream to read the encrypted file back.
                FileStream fsread = new FileStream(CaminhoArquivo,
                                               FileMode.Open,
                                               FileAccess.Read);

                //Create a DES decryptor from the DES instance.
                ICryptoTransform desdecrypt = triDes.CreateDecryptor();

                //Create crypto stream set to read and do a 
                //DES decryption transform on incoming bytes.
                CryptoStream cryptostreamDecr = new CryptoStream(fsread,
                                                             desdecrypt,
                                                             CryptoStreamMode.Read);
                string sOutputFilename2 = CaminhoArquivo.Substring(0, CaminhoArquivo.Length - 4) + "1.tmp";

                //Print the contents of the decrypted file.
                StreamWriter fsDecrypted = new StreamWriter(sOutputFilename2);
                fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
                fsDecrypted.Close();

                StreamReader srLeitor = new StreamReader(sOutputFilename2);
                TextoDecodificado = srLeitor.ReadToEnd();
                srLeitor.Close();

                //deleta o arquivo temporario
                FileInfo fiArquivo = new FileInfo(sOutputFilename2);
                fiArquivo.Delete();
            }
            catch
            {
            }


            return TextoDecodificado;
        }

    }//fim da classe
}//fim do namespace
