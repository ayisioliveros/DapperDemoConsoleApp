using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DapperDemo
{
    class Program
    {
        //static void Main(string[] args)
        //{
            //IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            //string SqlString = "SELECT TOP 100 [CustomerID],[CustomerFirstName],[CustomerLastName],[IsActive] FROM [Customer]";

            //var ourCustomers = (List<Customer>)db.Query<Customer>(SqlString);

            //foreach (var Customer in ourCustomers)
            //{
            //	Console.WriteLine(new string('*', 20));
            //	Console.WriteLine("\nCustomer ID: " + Customer.CustomerID.ToString());
            //	Console.WriteLine("First Name: " + Customer.CustomerFirstName);
            //	Console.WriteLine("Last Name: " + Customer.CustomerLastName);
            //	Console.WriteLine("Is Active? " + Customer.IsActive + "\n");
            //	Console.WriteLine(new string('*', 20));
            //}

            //Console.ReadLine();
        //}

        public static ICustomerRepository customerRepository = new CustomerRepository();
        public void InsertData()
        {
            Console.WriteLine(new string('*', 20));
            Console.WriteLine("Enter the FirstName, LastName , Company And Title Of Contacts");
            Console.Write("First Name : ");
            String CustFName = Console.ReadLine();
            Console.Write("Last Name : ");
            String CustLName = Console.ReadLine();
            Console.Write("Is Active? : ");
            bool IsAct = true;

            //inserting
            Customer customer1 = new Customer
            {
                CustomerFirstName = CustFName,
                CustomerLastName = CustLName,
                IsActive = IsAct
            };
            customerRepository.Add(customer1);
            ShowData();
        }

        public void ShowData()
        {
            Console.WriteLine(new string('*', 20));
            List<Customer> customer = customerRepository.GetAll();
            foreach (var cust in customer)
            {
                Console.WriteLine(cust.CustomerID + " " + cust.CustomerFirstName + " " + cust.CustomerLastName + " " + cust.IsActive);
            }
        }
        public void UpdatingData()
        {
            Console.WriteLine(new string('*', 20));
            //Updating
            Console.WriteLine("What CustomerID do you want to Update ");
            int CustomerID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What do you want to Update....");
            Console.Write(" Customer First Name press 1 ");
            Console.Write(" Customer Last Name press 2 ");
            Console.Write(" Is Active? press 3 ");
            int ch = Convert.ToInt32(Console.ReadLine());
            Customer customer = customerRepository.GetById(CustomerID);
            String Name = null;
            switch (ch)
            {
                case 1:
                    Console.WriteLine("Customer First Name : ");
                    string CustFName = Console.ReadLine();
                    customer.CustomerFirstName = CustFName;
                    Name = "CustomerFirstName";
                    customerRepository.Update(customer, Name);
                    GetByID(CustomerID);
                    break;
                case 2:
                    Console.WriteLine("Customer Last Name : ");
                    string CustLName = Console.ReadLine();
                    customer.CustomerLastName = CustLName;
                    Name = "CustomerLastName";
                    customerRepository.Update(customer, Name);
                    GetByID(CustomerID);
                    break;
                case 3:
                    Console.WriteLine("Is Active : ");
                    bool IsAct = true;
                    customer.IsActive = IsAct;
                    Name = "IsActive";
                    customerRepository.Update(customer, Name);
                    GetByID(CustomerID);
                    break;
                default:
                    Console.WriteLine("Please select a choice atleast");
                    break;
            }
        }
        //Get By ID Method
        public void GetByID(int CustomerID)
        {
            Console.WriteLine(new string('*', 20));
            Customer customer2 = customerRepository.GetById(CustomerID);
            if (customer2 != null)
            {
                Console.WriteLine(customer2.CustomerID + " " + customer2.CustomerFirstName + " " + customer2.CustomerLastName + " " + customer2.IsActive);
            }
        }
        //Delete Method
        public void DeleteData()
        {
            Console.WriteLine(new string('*', 20));
            ShowData();
            Console.WriteLine(new string('*', 20));

            //Deletion
            Console.Write("What id do you want to delete :");
            int CustomerID = Convert.ToInt32(Console.ReadLine());
            customerRepository.Delete(CustomerID);
            Customer con = customerRepository.GetById(3);
            if (con == null)
            {
                Console.WriteLine("Customer record is deleted already");
            }
            Console.WriteLine(new string('*', 20));
            ShowData();
        }
        public void SelectOption()
        {
            Console.WriteLine(new string('*', 20));

            Console.WriteLine("Welcome To Dapper Example :");
            Console.WriteLine(new string('*', 20));
            Console.WriteLine("For...");
            Console.WriteLine("Show Data Select 1");
            Console.WriteLine("Insert Data Select 2");
            Console.WriteLine("Update Data Select 3");
            Console.WriteLine("Delete Data Select 4");
            Console.WriteLine();
            Console.Write("Your Selection :  ");
            int selection = Convert.ToInt32(Console.ReadLine());
            switch (selection)
            {
                case 1:
                    ShowData();
                    break;
                case 2:
                    InsertData();
                    break;
                case 3:
                    UpdatingData();
                    break;
                case 4:
                    DeleteData();
                    break;
                default:
                    break;
            }

            Console.WriteLine(new string('*', 20));
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.SelectOption();


            Console.ReadLine();
        }
    }
}