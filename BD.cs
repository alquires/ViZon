using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using Oracle.DataAccess.Client;
using System.Data.OleDb;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Devart.Data;
using Devart.Data.PostgreSql;


namespace ZonView017
{
    public class BD
    {
        public BD()
        {
        }

        //=====================================
        //             METODOS ACCESS
        //=====================================
        public DataTable DB_Access(string NomeArq, string NomeTabela, string AliasTabela, string PathArquivo, int SequenciaTabela,
            string ClauSelect, string ClauWhere, string senha)
        {


            DataSet ds = new DataSet();

            try
            {
                //montagem da string de conexao com parte dos dados obtidos na chamada do metodo Provider=Microsoft.Jet.OLEDB.4.0;
                string strConexao = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + PathArquivo + NomeArq;

                if (senha != "")
                {
                    strConexao += ";Jet OLEDB:Database Password=" + senha;
                    //MessageBox.Show("Incluir aqui a configuracao da senha");
                }



                //chamada dos objetos de banco de dados
                OleDbConnection OleDBconn = new OleDbConnection(strConexao);
                //OleDBconn.ConnectionString = strConexao;
                string strSintaxeSQL = "SELECT " + ClauSelect + " FROM " + NomeTabela;
                if(ClauWhere!="")
                {
                    strSintaxeSQL += " WHERE " + ClauWhere;
                }

                OleDbDataAdapter OleDBda = new OleDbDataAdapter(strSintaxeSQL, OleDBconn);
                OleDBda.Fill(ds);
            }
            catch (OleDbException Ex)
            {


                MessageBox.Show("Não foi possível carregar o banco de dados Access.\r\n" + Ex.Message + "\r\nErro número: " +
                    Ex.ErrorCode.ToString());
                ds.Tables.Add();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Não foi possível carregar o banco de dados Oracle.\r\n" + Ex.Message, NomeArq);
                ds.Tables.Add();
            }
            ds.Tables[0].TableName = AliasTabela;
            


            return ds.Tables[0];
        }


        //=====================================
        //             METODOS MYSQL
        //=====================================
        public DataTable DB_MySql(string EndServidor, string NomeBD, string NomeTabela, string AliasTabela, string Login, string Senha,
            string ClauSelect, string ClauWhere)
        {
            DataSet ds = new DataSet();


            try
            {
                string strConexao = "server=" + EndServidor + ";User Id=" + Login + ";Password=" + Senha + ";database=" + NomeBD;
                MySqlConnection MySqlconn = new MySqlConnection(strConexao);
                MySqlDataAdapter MySqlda = new MySqlDataAdapter("SELECT " + ClauSelect + " FROM " + NomeTabela + " WHERE " +
                    ClauWhere, MySqlconn);
                MySqlda.Fill(ds);
            }
            catch (MySql.Data.MySqlClient.MySqlException Ex)
            {
                MessageBox.Show("Não foi possível carregar o banco de dados MySQL.\r\n" + Ex.Message + "\r\n Erro número: " +
                    Ex.Number.ToString());
                ds.Tables.Add();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Não foi possível carregar o banco de dados Oracle.\r\n" + Ex.Message);
                ds.Tables.Add();
            }
            ds.Tables[0].TableName = AliasTabela;

            return ds.Tables[0];
        }


        //=====================================
        //          METODOS PostgreSQL
        //=====================================
        public DataTable BD_PostgreSQL(string EndServidor, string NomeBD, string NomeTabela, string AliasTabela, string Login, string Senha,
            string Porta, string ClauSelect, string ClauWhere)
        {
            DataSet ds = new DataSet();


            try
            {
                string strConexao = "Server=" + EndServidor + ";Port=" + Porta + ";Database=" + NomeBD + ";User Id=" + Login +
                    ";Password=" + Senha;
                PgSqlConnection Postgreconn = new PgSqlConnection(strConexao);
                PgSqlDataAdapter Postgreda = new PgSqlDataAdapter("SELECT " + ClauSelect + " FROM " + NomeTabela + " WHERE " +
                    ClauWhere + ";", Postgreconn);
                Postgreda.Fill(ds);
            }
            catch (PgSqlException Ex)
            {

                MessageBox.Show("Não foi possível carregar o banco de dados PostgreSQL.\r\n" + Ex.Message + "\r\n" +
                    Ex.DetailMessage + "\r\n" + Ex.ErrorCode.ToString());
                ds.Tables.Add();
            }
            catch (Exception Ex)
            {

                MessageBox.Show("Não foi possível carregar o banco de dados Oracle.\r\n" + Ex.Message);
                ds.Tables.Add();
            }
            ds.Tables[0].TableName = AliasTabela;

            return ds.Tables[0];
        }


        //=====================================
        //          METODOS SQL-SERVER
        //=====================================
        public DataTable DB_SQL()
        {
            DataSet ds = new DataSet();

            try
            {
                //string strConexao = @"Source=.\SQLEXPRESS;attaachDbFilenameServer=";
                ds.Tables.Add();
            }
            catch (Exception Ex)
            {


                MessageBox.Show("Não foi possível carregar o banco de dados SQL Server.\r\n" + Ex.Message + "\r\n" + Ex.Source);
                ds.Tables.Add();
            }
            /*catch (Exception Ex)
            {
                MessageBox.Show("Não foi possível carregar o banco de dados Oracle.\r\n" + Ex.Message);
                ds.Tables.Add();
            }*/
            return ds.Tables[0];
        }

        //=====================================
        //          METODOS ORACLE
        //=====================================
        public DataTable DB_ORACLE()
        {
            DataSet ds = new DataSet();

            try
            {
                string strConexao = "Data Source=MyOracleDB;User Id=myUsername;Password=myPassword;Integrated Security=no;";
                OracleConnection Oracleconn = new OracleConnection(strConexao);
                OracleDataAdapter Oracleda = new OracleDataAdapter("", Oracleconn);
                Oracleda.Fill(ds);

            }
            catch (OracleException Ex)
            {
                MessageBox.Show("Não foi possível carregar o banco de dados Oracle.\r\n" + Ex.Message + "\r\nNumber: " +
                    Ex.Number.ToString() + "\r\nErrorCode: " + Ex.ErrorCode.ToString());
                ds.Tables.Add();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Não foi possível carregar o banco de dados Oracle.\r\n" + Ex.Message);
                ds.Tables.Add();
            }
            return ds.Tables[0];
        }
    }
}
