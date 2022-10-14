using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public partial class Employee
    {
        public Employee(long id, string firstName, string lastName, string email, string phoneNumber, string country)
        {
            this.Id = id;
            this.FirstName = firstName ;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber; 
            this.Country = country;
    }

        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
    }
}
