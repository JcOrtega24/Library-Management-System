﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using MySqlConnector;

namespace LibraryManagementSystem
{
    public class Library1
    {
        private string sqlConString;
        public int rowAffected = 0;
        public Library1(string server_address,string database, string username, string password)
        {
            //Server = server name(xampp) Uid = username Pwd = password
            sqlConString = "Server = " + server_address + "; Database = " + database + "; User Id = "
            + username + "; Password = " + password + ";";
        }
        //select
        public DataTable GetData(string sql)
        {
            //connection
            MySqlConnection Sqlcon = new MySqlConnection(sqlConString);
            //checking if the connection is close
            if (Sqlcon.State == ConnectionState.Closed) Sqlcon.Open();
            //creating a command using the connection and the sql query
            MySqlCommand SQLcom = new MySqlCommand(sql, Sqlcon);
            //creating the adapter using the created sql command
            MySqlDataAdapter SQLadap = new MySqlDataAdapter(SQLcom);
            DataSet ds = new DataSet();
            //fill the dataset using the adapter
            SQLadap.Fill(ds);
            return ds.Tables[0];
        }
        //insert, update, delete
        public void executeSQL(string sql)
        {
            //connection
            MySqlConnection Sqlcon = new MySqlConnection(sqlConString);
            //open the connection
            if (Sqlcon.State == ConnectionState.Closed) Sqlcon.Open();
            //build the sql command using the sql statement and the connection
            MySqlCommand SQLcom = new MySqlCommand(sql, Sqlcon);
            //execute the sql command
            rowAffected = SQLcom.ExecuteNonQuery();
        }
        public string SqlConString { get; set; }

    }
}
