using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo
{
    class CustomerRepository :ICustomerRepository
    {
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        //Get Customer By CustomerID
        public Customer GetById(int CustomerID)
        {
            return this.db.Query<Customer>("Select * From Customer Where CustomerID=@CustomerID", new { Id = CustomerID }).FirstOrDefault();
        }

        //Retreive the data from the table.
        public List<Customer> GetAll()
        {
            return this.db.Query<Customer>("Select * From Customer").ToList();
        }
        //Add Customer Data
        public bool Add(Customer customer)
        {
            try
            {
                string sql = "INSERT INTO Customer(CustomerFirstName,CustomerLastName,IsActive) values(@CustomerFirstName,@CustomerLastName,@IsActive); SELECT CAST(SCOPE_IDENTITY() as int)";
                var returnId = this.db.Query<int>(sql, customer).SingleOrDefault();
                customer.CustomerID = returnId;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        //Update The Customer Record
        public bool Update(Customer customer, String ColumnName)
        {
            string query = "update Customer set " + ColumnName + "=@" + ColumnName + " Where CustomerID=@CustomerID";
            var count = this.db.Execute(query, customer);
            return count > 0;
        }
        public bool Delete(int CustomerID)
        {
            var affectedrows = this.db.Execute("Delete from Customer where CustomerID=@CustomerID", new { CustomerID = CustomerID});
            return affectedrows > 0;
        }
    }
}
