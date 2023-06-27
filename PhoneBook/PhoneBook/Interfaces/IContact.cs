using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Interfaces
{
    internal interface IContact
    {
        string FirstName { get; }

        string LastName { get; }

        string PhoneNumber { get; }
    }
}
