using CustomerAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Data
{
    public class TestCustomerData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CustomerContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();

                if (context.Customers != null && context.Customers.Any())
                {
                    return;
                }

                var customers = GetCustomers().ToArray();
                context.Customers.AddRange(customers);
                context.SaveChanges();

            }
        }

        private static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer() { FirstName = "Pryesh", Surname = "Gohil", EmailAddress = "gohilp@hotmail.com", Password = "test123;" });
            customers.Add(new Customer() { FirstName = "Bob", Surname = "Builder", EmailAddress = "builderb@hotmail.com", Password = "test124" });
            return customers;
        }
    }
}
