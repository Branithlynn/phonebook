using ConsolePhonebook.Entity;
using ConsolePhonebook.Repository;
using ConsolePhonebook.Service;
using ConsolePhonebook.Tools;
using System;
using System.Collections.Generic;

namespace ConsolePhonebook.View
{
    public class PhonesManagerView
    {
        private Contact contact = null;

        public PhonesManagerView(Contact contact)
        {
            this.contact = contact;
        }

        public void Show()
        {
            while (true)
            {
                PhoneManagementEnum choice = RenderMenu();

                switch (choice)
                {
                    case PhoneManagementEnum.Select:
                        {
                            GetAll();
                            break;
                        }
                    case PhoneManagementEnum.Insert:
                        {
                            Add();
                            break;
                        }
                    case PhoneManagementEnum.Delete:
                        {
                            Delete();
                            break;
                        }
                    case PhoneManagementEnum.Exit:
                        {
                            return;
                        }
                }
            }
        }

        private PhoneManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Phones management (" + this.contact.FullName + ")");
                Console.WriteLine("ID:" + contact.Id);
                Console.WriteLine("Name :" + contact.FullName);
                Console.WriteLine("Email :" + contact.Email);
                Console.WriteLine("##########################");
                Console.WriteLine("[G]et all Phones");
                Console.WriteLine("[A]dd Phone");
                Console.WriteLine("[D]elete Phone");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return PhoneManagementEnum.Select;
                        }
                    case "A":
                        {
                            return PhoneManagementEnum.Insert;
                        }
                    case "D":
                        {
                            return PhoneManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return PhoneManagementEnum.Exit;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }

        private void GetAll()
        {
            Console.Clear();

            PhonesRepository phonesRepository = new PhonesRepository("phones.txt");
            List<Phone> phones = phonesRepository.GetAll(this.contact.Id);

            foreach (Phone phone in phones)
            {
                Console.WriteLine("ID:" + phone.Id);
                Console.WriteLine("Phone :" + phone.PhoneNumber);
                Console.WriteLine("########################################");
            }

            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            Phone phone = new Phone();
            phone.ParentContactId = this.contact.Id;

            Console.WriteLine("Add new Phone:");
            Console.Write("Phone: ");
            phone.PhoneNumber = Console.ReadLine();

            PhonesRepository phonesRepository = new PhonesRepository("phones.txt");
            phonesRepository.Save(phone);

            Console.WriteLine("Phone saved successfully.");
            Console.ReadKey(true);
        }

        private void Delete()
        {
            PhonesRepository phonesRepository = new PhonesRepository("phones.txt");

            Console.Clear();

            Console.WriteLine("Delete Phone:");
            Console.Write("Phone Id: ");
            int phoneId = Convert.ToInt32(Console.ReadLine());

            Phone phone = phonesRepository.GetById(phoneId);
            if (phone == null)
            {
                Console.WriteLine("Phone not found!");
            }
            else
            {
                phonesRepository.Delete(phone);
                Console.WriteLine("Phone deleted successfully.");
            }
            Console.ReadKey(true);
        }
    }
}
