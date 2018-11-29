using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using CoffeeTalk.Models;

namespace CoffeeTalk.Services
{
    public class DBServices
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        private int queries = 0;

        //Constructor
        public DBServices()
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
        public bool Insert(Coffee coffee)
        {
            string query = "Insert Into CoffeeDB.CoffeeDB (ProductID, CoffeeName, Price) Values(" + coffee.ProductID + ", " + coffee.CoffeeName + ", " + coffee.Price + "); ";

            try
            {
                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //Update statement
        public bool Update(Coffee coffee)
        {
            string query = "UPDATE CoffeeDB.CoffeeDB SET Price="+ coffee.Price +" WHERE ProductID="+ coffee.ProductID;

            try
            {

                //Open connection
                if (this.OpenConnection() == true)
                {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();

                    //Assign the query using CommandText
                    cmd.CommandText = query;

                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //Delete statement
        public bool Delete(Coffee coffee) //deletes the coffee from the DB
        {
            string query = "DELETE FROM CoffeeDB.CoffeeDB WHERE ProductID="+ coffee.ProductID;

            try
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //Select statement
        public Coffee[] Select() //returns a list of all the coffee rows in CoffeeDB
        {
            string query = "SELECT * FROM CoffeeDB.CoffeeDB";

            //list of Coffees
            Coffee[] coffeeList;

            Count();
            int index = 0;

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create the data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                coffeeList = new Coffee[queries];
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //creates the coffee object
                    Coffee coffee = new Coffee();

                    //sets the coffee's properties
                    coffee.ProductID = Convert.ToInt32(dataReader["ProductID"]);
                    coffee.CoffeeName = Convert.ToString(dataReader["CoffeeName"]);
                    coffee.Price = Convert.ToString(dataReader["Price"]);

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
            string query = "SELECT Count(*) FROM CoffeeDB.CoffeeDB";
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