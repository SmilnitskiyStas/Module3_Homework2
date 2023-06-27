using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Interfaces;

namespace PhoneBook.Models
{
    internal class CatalogueEnumerator : IEnumerator<IContact>
    {
        private List<IContact> contacts;

        private int _position = -1;

        public CatalogueEnumerator()
        {
        }

        public CatalogueEnumerator(List<IContact> contacts)
        {
            this.contacts = contacts;
        }

        public IContact Current
        {
            get
            {
                if (_position == -1 && _position >= contacts.Count)
                {
                    throw new InvalidOperationException();
                }

                return contacts[_position];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_position < contacts.Count - 1)
            {
                _position++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
