using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Models;

namespace PhoneBook
{
    internal class Starter
    {
        private List<CultureInfo> CultureInfos { get; set; }

        public Starter()
        {
            CultureInfos = CreateCulture();
        }

        public void Run()
        {
            Catalogue catalogue = new Catalogue();

            Console.WriteLine("How many contacts do you want to create?");
            int countContacts = int.Parse(Console.ReadLine());

            for (int i = 0; i < countContacts; i++)
            {
                Contact contact = CreateNewContact();

                catalogue.AddContact(contact);
            }

            foreach (Contact contact in catalogue)
            {
                Console.WriteLine(contact.FirstName);
            }
        }

        /// <summary>
        /// Створення контакту.
        /// </summary>
        /// <returns>Готовий контакт.</returns>
        private Contact CreateNewContact()
        {
            CultureInfo culture = SelectCultureOfUser();

            Console.WriteLine("Input FirstName of user:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Input LastName of user:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Input phone number of user");
            string phoneNumber = Console.ReadLine();

            Contact contact = new Contact(firstName, lastName, phoneNumber, culture.EnglishName);

            return contact;
        }

        /// <summary>
        /// Створення культур.
        /// </summary>
        /// <returns>Список культур.</returns>
        private List<CultureInfo> CreateCulture()
        {
            List<CultureInfo> list = new List<CultureInfo>();

            string[] cultureName = { "en-US", "uk" };

            for (int i = 0; i < cultureName.Length; i++)
            {
                CultureInfo culture = new CultureInfo(cultureName[i]);

                list.Add(culture);
            }

            return list;
        }

        /// <summary>
        /// Визначення кольтури для користувача.
        /// </summary>
        /// <returns>Культура.</returns>
        private CultureInfo SelectCultureOfUser()
        {
            ShowCulture();

            Console.WriteLine("\nSelect your culture");
            int selectIdForCulture = int.Parse(Console.ReadLine());

            CultureInfo getCulture = selectIdForCulture < CultureInfos.Count ? CultureInfos[selectIdForCulture - 1] : new CultureInfo("en-US");

            return getCulture;
        }

        /// <summary>
        /// Відображення культур.
        /// </summary>
        private void ShowCulture()
        {
            for (int i = 0; i < CultureInfos.Count; i++)
            {
                Console.WriteLine($"ID - {i + 1}, Name culture {CultureInfos[i].NativeName}");
            }
        }
    }
}
