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

        public static EditProfileViewModel? GetUserDetails(TravelExpertsContext db,string userID)
        {
            EditProfileViewModel vm = new EditProfileViewModel();
            User? u = db.Users.SingleOrDefault(u => u.Id == userID);
            Customer? cust = db.Customers.SingleOrDefault(c => c.CustomerId == u.CustomerId);
            if (cust == null || u == null) return null;

            vm.UserName = u.UserName;
            vm.CustEmail = u.Email;
            vm.TravelPreference = u.TravelPreference;
            vm.VirtualWallet = u.VirtualWallet;
            vm.ProfilePicture = u.ProfilePicture;


            vm.CustFirstName = cust.CustFirstName;
            vm.CustLastName = cust.CustLastName;
            vm.CustCity = cust.CustCity;
            vm.CustCountry = cust.CustCountry;
            vm.CustPostal = cust.CustPostal;
            vm.CustProv = cust.CustProv;
            vm.CustBusPhone = cust.CustBusPhone;
            vm.CustAddress = cust.CustAddress;
            return vm;
        }

        //will validate user null at constructor itself
        public static int UpdateUser(TravelExpertsContext db,User updatedUser, EditProfileViewModel evm)
        {
            Customer? cust = db.Customers.SingleOrDefault(c=>c.CustomerId == updatedUser.CustomerId);
            User? u = db.Users.SingleOrDefault(u => u.Id == updatedUser.Id);
            if (cust == null || u == null) return 0;

            u.TravelPreference = evm.TravelPreference;
            //u.UserName = evm.UserName; big NONO dont update username
            u.Email = evm.CustEmail;
            u.ProfilePicture = evm.ProfilePicture;

            cust.CustFirstName = evm.CustFirstName;
            cust.CustLastName = evm.CustLastName;
            cust.CustCity = evm.CustCity;
            cust.CustCountry = evm.CustCountry;
            cust.CustPostal = evm.CustPostal;
            cust.CustProv = evm.CustProv;
            cust.CustBusPhone = evm.CustBusPhone;
            cust.CustAddress = evm.CustAddress;
            cust.CustEmail = evm.CustEmail;
            cust.CustHomePhone = evm.CustHomePhone;

            return db.SaveChanges(); 
        }

        public static bool IsProfilePictureDelete(TravelExpertsContext db, string id)
        {
            User u = db.Users.SingleOrDefault(u => u.Id == id);
            if (u == null) return false;
            if (u.ProfilePicture == null || u.ProfilePicture.Length <= 0) return false;
            u.ProfilePicture = null;
            
            return db.SaveChanges() > 0;//pretty cool right?
        }
    }
}
