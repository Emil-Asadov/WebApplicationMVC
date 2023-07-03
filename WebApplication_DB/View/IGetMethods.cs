using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_DB.Models;

namespace WebApplication_DB.View
{
    public interface IGetMethods
    {
        (DataTable dt, string err) GetGender();
        (DataTable dt, string err) GetCustomer(int custID = 0);
        (List<ClassControls> res, string err) GetCustomerLast();
        (ClassControls res, string err) GetCustomerById(int custId);
    }
}
