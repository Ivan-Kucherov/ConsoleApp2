using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp2
{
    public class Days
    {
        private static string strConnect = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Environment.CurrentDirectory}\Database1.mdf;Integrated Security=True";
        private SqlConnection connect;
        private SqlDataAdapter adapter;
        private SqlCommand command;
        private DataTable datatable;
        public Days()
        {
            connect = new SqlConnection(strConnect);
            datatable = new DataTable();
        }
        public DataTable GetData()
        {
            connect.Open();
            datatable.Clear();
            adapter = new SqlDataAdapter("SELECT * FROM Table1", connect);
            adapter.Fill(datatable);
            connect.Close();
            return datatable;
        }
        public DataTable GetDataLast()
        {
            connect.Open();
            datatable.Clear();
            adapter = new SqlDataAdapter($"SELECT * FROM Table1 WHERE month(Date)>= {DateTime.Today.Month} ORDER BY month(Date)*100+day(Date) ASC", connect);
            adapter.Fill(datatable);
            connect.Close();
            return datatable;
        }
        public bool delete(int id)
        {
            bool end;
            connect.Open();
            command = new SqlCommand($"DELETE FROM Table1 WHERE id = {id}", connect);
            end =  (command.ExecuteNonQuery() >0);
            connect.Close();
            return end;
        }
        public bool add(string name,DateTime date,string note)
        {
            bool end;
            connect.Open();
            command = new SqlCommand($"INSERT INTO Table1 (Name, Date, Note) VALUES ('{name}', '{date.ToString("yyyy-MM-dd")}', '{note}') ", connect);
            end = (command.ExecuteNonQuery() > 0);
            connect.Close();
            return end;
        }

        public bool change(int id, string name, DateTime date, string note)
        {
            bool end;
            connect.Open();
            command = new SqlCommand($"UPDATE Table1 SET Name = '{name}', Date = '{date.ToString("yyyy-MM-dd")}', Note = '{note}' WHERE id = {id}", connect);
            end = (command.ExecuteNonQuery() > 0);
            connect.Close();
            return end;
        }
    }
}
