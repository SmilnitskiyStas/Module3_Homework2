using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PhoneBook.Interfaces;

namespace PhoneBook.Models
{
    internal class Catalogue : IEnumerable
    {
        private Dictionary<string, List<IContact>> _data = new Dictionary<string, List<IContact>>();

        private string[] _alphabet = null;

        private string _section = null;

        /// <summary>
        /// Додавання нового контакту.
        /// </summary>
        /// <param name="contact">Контакт для додавання.</param>
        public void AddContact(IContact contact)
        {
            // Alphabet
            CheckCulture((ICultureContact)contact);

            // Section
            CheckCultureInfoBySimbolAndNumber(contact.FirstName);

            if (_section != null)
            {
                AddUserToContact(contact, _section);
            }
            else
            {
                string key = Convert.ToString(contact.FirstName[0]);

                AddUserToContact(contact, key);
            }
        }

        /// <summary>
        /// Видалення контакту із списку.
        /// </summary>
        /// <param name="contact">Контакт для видалення.</param>
        public void RemoveContact(IContact contact)
        {
            string key = Convert.ToString(contact.FirstName[0]);

            List<IContact> contacts = _data[key];

            for (int i = 0; i < contacts.Count; i++)
            {
                if (contact.FirstName == contacts[i].FirstName && contact.LastName == contacts[i].LastName)
                {
                    contacts.Remove(contacts[i]);
                    contacts.Sort();
                }
            }

            _data[key] = contacts;
        }

        public IEnumerator GetEnumerator()
        {
            List<IContact> contacts = GetForEnumerableList();

            return new CatalogueEnumerator(contacts);
        }

        /// <summary>
        /// Додавання нового контакту.
        /// </summary>
        /// <param name="contact">Контакт для додавання.</param>
        /// <param name="key">Ключ для додавання.</param>
        private void AddUserToContact(IContact contact, string key)
        {
            List<IContact> contacts = new List<IContact>();

            if (!_data.ContainsKey(key))
            {
                contacts.Add(contact);

                _data.Add(key, contacts);
            }
            else
            {
                contacts = _data[key];

                contacts.Add(contact);
                contacts.Sort();

                _data[key] = contacts;
            }
        }

        /// <summary>
        /// Визначення мови для присвоєння алфавіту.
        /// </summary>
        /// <param name="cultureContact">Культура користувача.</param>
        private void CheckCulture(ICultureContact cultureContact)
        {
            if (_alphabet == null)
            {
                if (cultureContact.CultureName.ToLower() is "ukrainian")
                {
                    _alphabet = "а, б, в, г, ґ, д, е, є, ж, з, и, і, ї, й, к, л, м, н, о, п, р, с, т, у, ф, х, ц, ч, ш, щ, ь, ю, я".Split(',');
                }
                else if (cultureContact.CultureName.ToLower() is "english (united states)")
                {
                    _alphabet = "A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z".ToLower().Split(',');
                }
            }
        }

        /// <summary>
        /// Перевірка на символи.
        /// </summary>
        /// <param name="name">Ім'я користувача для перевірки на символи.</param>
        private void CheckCultureInfoBySimbolAndNumber(string name)
        {
            string firstChar = Convert.ToString(name[0]);

            if (Regex.IsMatch(firstChar, "^[0-9]*$"))
            {
                _section = "0-9";
            }
            else if (_section == null)
            {
                for (int i = 0; i < name.Length; i++)
                {
                    if (Regex.IsMatch(Convert.ToString(name[i]), "^[\"\\|!#$%&/()=?»«@£§€{}.-;'\\\"<>_,\"]*$"))
                    {
                        _section = "#";
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Отримання списку для відображення.
        /// </summary>
        /// <returns>Список контактів по літері.</returns>
        private List<IContact> GetForEnumerableList()
        {
            for (int i = 0; i < _alphabet.Length; i++)
            {
                bool existKey = _data.ContainsKey(_alphabet[i].Trim().ToUpper());

                if (existKey)
                {
                    Console.WriteLine($"\nAlphabet contacts: {_alphabet[i].ToUpper()}\n");
                    return _data[Convert.ToString(_alphabet[i].Trim().ToUpper())];
                }
            }

            return new List<IContact>();
        }
    }
}
