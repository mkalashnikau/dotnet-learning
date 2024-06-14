using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public class Linq7CategoryGroup
    {
        public string Category { get; set; }
        public decimal UnitsInStock { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<dynamic> UnitsInStockGroup { get; set; }
    }

    public class Linq8CategoryGroup
    {
        public decimal Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class Linq9CategoryGroup
    {
        public string City { get; set; }
        public decimal AverageIncome { get; set; }
        public int AverageIntensity { get; set; }
    }

    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            return customers.Where(c => c.Orders.Sum(o => o.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));
            if (suppliers == null)
                throw new ArgumentNullException(nameof(suppliers));

            return customers.Select(c => (c, suppliers.Where(s => s.Country == c.Country && s.City == c.City)));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.GroupBy(c => new { c.Country, c.City })
                            .Select(g => (g.First(), suppliers.Where(s => s.Country == g.Key.Country && s.City == g.Key.City)));
        }

        public static IEnumerable<CustomerStat> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(customer => customer.Orders.Sum(order => order.Total) > limit)
                            .Select(c => new CustomerStat
                            {
                                Customer = c,
                                TotalByYear = c.Orders.GroupBy(o => o.OrderDate.Year)
                                      .Select(g => new { Year = g.Key, Total = g.Sum(o => o.Total) }),
                                TotalByMonth = c.Orders.GroupBy(o => o.OrderDate.Month)
                                       .Select(g => new { Month = g.Key, Total = g.Sum(o => o.Total) })
                            });
        }

        public class CustomerStat
        {
            public Customer Customer { get; set; }
            public IEnumerable<dynamic> TotalByYear { get; set; }
            public IEnumerable<dynamic> TotalByMonth { get; set; }
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
                    IEnumerable<Customer> customers
                )
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            return customers
                .Where(c => c.Orders.Any())
                .Select(c => (c, c.Orders.Min(o => o.OrderDate)));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
                    IEnumerable<Customer> customers
                )
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            return customers
                .Where(c => c.Orders.Any())
                .Select(c => (customer: c, dateOfEntry: c.Orders.Min(o => o.OrderDate)))
                .OrderBy(r => r.dateOfEntry.Year)
                .ThenBy(r => r.dateOfEntry.Month)
                .ThenByDescending(r => r.customer.Orders.Sum(o => o.Total))
                .ThenBy(r => r.customer.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            return customers.Where(cust =>
                                      !string.IsNullOrEmpty(cust.PostalCode) &&
                                      !cust.PostalCode.All(char.IsDigit) ||
                                      string.IsNullOrEmpty(cust.Region) ||
                                      !cust.Phone.Contains("("));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            return products.GroupBy(p => p.Category)
                           .Select(g => new Linq7CategoryGroup
                           {
                               Category = g.Key,
                               UnitsInStock = g.Sum(p => p.UnitsInStock),
                               Products = g
                           })
                           .OrderBy(c => c.Category);
        }

        public static IEnumerable<Linq8CategoryGroup> Linq8(IEnumerable<Product> products, decimal cheap, decimal middle, decimal expensive)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            return products
                        .GroupBy(p => p.UnitPrice < cheap ? cheap : (p.UnitPrice <= middle ? middle : expensive))
                        .Select(g => new Linq8CategoryGroup
                        {
                            Category = g.Key,
                            Products = g
                        });
        }

        public static IEnumerable<Linq9CategoryGroup> Linq9(IEnumerable<Customer> customers)
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            return customers.GroupBy(c => c.City)
                           .Select(cityGroup => new Linq9CategoryGroup
                           {
                               City = cityGroup.Key,
                               AverageIncome = cityGroup.Average(c => c.Orders.FirstOrDefault()?.Total ?? 0),
                               AverageIntensity = Convert.ToInt32(Math.Round(cityGroup.Average(c => c.Orders.Length)))
                           });
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers == null)
                throw new ArgumentNullException(nameof(suppliers));

            return suppliers.Select(s => s.Country)
                            .Distinct()
                            .OrderBy(c => c.Length)
                            .ThenBy(c => c)
                            .Aggregate(string.Empty, (acc, c) => acc + c);
        }
    }
}