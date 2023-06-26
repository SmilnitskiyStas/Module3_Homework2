using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Interfaces;

namespace PhoneBook.Models
{
    internal class Contact : IContact, ICultureContact, IComparable
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string CultureName { get; set; }

        public Contact(string firstName, string lastName, string phoneNumber, string cultureName)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            CultureName = cultureName;
        }

        public int CompareTo(object? obj)
        {
            if (obj is Contact contact)
            {
                return FirstName.CompareTo(contact.FirstName);
            }
            else
            {
                throw new ArgumentException("Error");
            }
        }
    }
}
