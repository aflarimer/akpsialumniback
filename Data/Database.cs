using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace api.Data
{
    public class Database //if figureing out environment variables also put them in heroku
    {
        public string ConnString {get; set;}
        public MySqlConnection Conn {get; set;}

        public Database() {
            string server = "x8autxobia7sgh74.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "qafvzj62ztj61beu";
            string port = "3306";
            string userName = "e11ipnzrdqtjty3z";
            string password = "so2fjevwl94pzqgh";

            this.ConnString = $@"server = {server}; user = {userName}; database = {database}; port = {port}; password = {password};";
            this.Conn = new MySqlConnection(this.ConnString);
        }
        public void Open() {
            this.Conn.Open();
        }
        public void Close(){
            this.Conn.Close();
        }
        public List<ExpandoObject> Select(string query)
        {
            List<ExpandoObject> results = new();
            try
            {
                using var cmd = new MySqlCommand(query, this.Conn);
                using var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var temp = new ExpandoObject() as IDictionary<string, Object>;
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        temp.TryAdd(rdr.GetName(i), rdr.GetValue(i));
                    }

                    results.Add((ExpandoObject)temp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Select Query Error");
                Console.WriteLine(e.Message);
            }

            return results;
        }

        public void Insert(string query, Dictionary<string, object> values)
        {
            QueryWithData(query, values);
        }

        public void Update(string query, Dictionary<string, object> values)
        {
            QueryWithData(query, values);
        }

        public void Delete(string query, Dictionary<string, object> values)
        {
            Update(query, values);
        }

        private void QueryWithData(string query, Dictionary<string, object> values)
        {
            try
            {
                using var cmd = new MySqlCommand(query, this.Conn);
                foreach (var p in values)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Inserting Data");
                Console.WriteLine(e.Message);
            }
        }
    }
}