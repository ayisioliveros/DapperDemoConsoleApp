using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo
{
    interface ICustomerRepository
    {
        List<Customer> GetAll();
        bool Add(Customer customer);
        Customer GetById(int CustomerID);
        bool Update(Customer customer, String ColumnWidth);
        bool Delete(int CustomerID);
    }
}
