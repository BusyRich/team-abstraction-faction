using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using CoffeeTalk.Models;
using System.Data;
using System.Diagnostics;

namespace CoffeeTalk.Services
{
    public class TalkDBServices
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        private int queries = 0;

        //Constructor
        public TalkDBServices()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "abstractfaction.csahxp0t2if6.us-east-2.rds.amazonaws.com";
            database = "CoffeeDB";
            uid = "admin";
            password = "password";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public bool Insert(Talk coffee)
        {
            string query = "Insert Into CoffeeDB.CoffeeTalk (ProductID, CoffeeName, PicUrl, Description) Values(?ProductID, ?CoffeeName, ?PicUrl, ?Description);";

            
            using (MySqlCommand comm = new MySqlCommand())
            {
                comm.Connection = connection;
                comm.CommandText = query;
                comm.CommandType = CommandType.Text;

                comm.Parameters.AddWithValue("?ProductID", coffee.ProductID);
                comm.Parameters.AddWithValue("?CoffeeName", coffee.CoffeeName);
                comm.Parameters.AddWithValue("?PicUrl", coffee.PicUrl);
                comm.Parameters.AddWithValue("?Description", coffee.Description);

                try
                {
                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (MySqlException e)
                {
                    // do something with the exception
                    // do not hide it
                    // e.Message.ToString()
                    Debug.WriteLine(e.Message.ToString());
                    connection.Close();
                    return false;
                }
            }
        }

        //Select statement
        public Talk[] Select() //returns a list of all the coffee rows in CoffeeDB
        {
            string query = "SELECT * FROM CoffeeDB.CoffeeTalk";

            //list of Coffees
            Talk[] coffeeList;

            Count();
            int index = 0;

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create the data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                coffeeList = new Talk[queries];
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //creates the coffee object
                    Talk coffee = new Talk();

                    //sets the coffee's properties
                    coffee.ProductID = Convert.ToInt32(dataReader["ProductID"]);
                    coffee.CoffeeName = Convert.ToString(dataReader["CoffeeName"]);
                    coffee.PicUrl = Convert.ToString(dataReader["PicUrl"]);
                    coffee.Description = Convert.ToString(dataReader["Description"]);

                    //adds the coffee to the list of coffees
                    coffeeList[index] = coffee;
                    index++;
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return coffeeList;
            }
            else
            {
                return null;
            }
        }

        //Count statement
        public void Count() //returns a count of all the rows within the CoffeeDB
        {
            string query = "SELECT Count(*) FROM CoffeeDB.CoffeeTalk";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                //return the amount of coffees
                queries = Count;
            }
            else
            {
                
            }
        }
    }
}