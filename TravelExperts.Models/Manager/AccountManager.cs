using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData.Models;
using TravelExpertsData.ViewModel;

namespace TravelExpertsData.Manager
{
    public class AccountManager
    {
        //first save customer and get custID then with that save User
        public static Customer? SaveCustomer(TravelExpertsContext db, RegisterViewModel rvm)
        {
            Customer customer = new Customer
            {
                CustFirstName = rvm.Customer.FirstName,
                CustLastName = rvm.Customer.LastName,
                CustCity = rvm.Customer.City,
                CustCountry = rvm.Customer.Country,
                CustPostal = rvm.Customer.PostalCode,
                CustProv = rvm.Customer.CustProv,
                CustHomePhone = rvm.Customer.CustBusPhone,//same as bus no
                CustBusPhone = rvm.Customer.CustBusPhone,
                CustEmail = rvm.Email,//same as user email
                CustAddress = rvm.Customer.CustAddress
            };
            db.Customers.Add(customer);
            int rowsAffected = db.SaveChanges();
            if (rowsAffected <= 0) {
                return null;
            }
            return customer;//this has custID generated
        }

        
    }
}
