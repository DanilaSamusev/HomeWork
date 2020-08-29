using System.Collections.Generic;

namespace Northwind.Shared
{
    public class EmployeeWithTerritories
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Territory> Territories { get; set; }
    }
}
