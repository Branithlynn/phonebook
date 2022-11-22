using ConsolePhonebook.Entity;
using ConsolePhonebook.Repository;
using ConsolePhonebook.Service;
using ConsolePhonebook.Tools;
using System;
using System.Collections.Generic;

namespace ConsolePhonebook.View
{
    public class ContactsManagerView
    {
        public void Show()
        {
            while (true)
            {
                ContactManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case ContactManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case ContactManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case ContactManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case ContactManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case ContactManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case ContactManagementEnum.Exit:
                            {
                                return;
                            }
                    }
                }
                catch(Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
        }

        private ContactManagementEnum RenderMenu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Contacts management:");
                Console.WriteLine("[G]et all Contacts");
                Console.WriteLine("[V]iew Contact");
                Console.WriteLine("[A]dd Contact");
                Console.WriteLine("[E]dit Contact");
                Console.WriteLine("[D]elete Contact");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();
                switch(choice.ToUpper())
                {
                    case "G":
                    {
                        return ContactManagementEnum.Select;
                    }
                    case "V":
                    {
                        return ContactManagementEnum.View;
                    }
                    case "A":
                    {
                        return ContactManagementEnum.Insert;
                    }
                    case "E":
                    {
                        return ContactManagementEnum.Update;
                    }
                    case "D":
                    {
                        return ContactManagementEnum.Delete;
                    }
                    case "X":
                    {
                        return ContactManagementEnum.Exit;
                    }
                    default :
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

            ContactsRepository contactsRepository = new ContactsRepository("contacts.txt");
            PhonesRepository phonesRepository = new PhonesRepository("phones.txt");
            List<Contact> contacts = contactsRepository.GetAll(AuthenticationService.LoggedUser.Id);

            foreach(Contact contact in contacts)
            {
                Console.WriteLine("ID:" + contact.Id);
                Console.WriteLine("Name :" + contact.FullName);
                Console.WriteLine("Email :" + contact.Email);

                List<Phone> phones = phonesRepository.GetAll(contact.Id);
                foreach (Phone phone in phones)
                {
                    Console.WriteLine("ID:" + phone.Id);
                    Console.WriteLine("Phone :" + phone.PhoneNumber);
                }
                Console.WriteLine("########################################");
            }

            Console.ReadKey(true);
        }

        private void View()
        {
            Console.Clear();

            Console.Write("Contact ID: ");
            int contactId = Convert.ToInt32(Console.ReadLine());

            ContactsRepository contactsRepository = new ContactsRepository("contacts.txt");
            PhonesRepository phonesRepository = new PhonesRepository("phones.txt");

            Contact contact = contactsRepository.GetById(contactId);
            if (contact == null)
            {
                Console.Clear();
                Console.WriteLine("Contact not found.");
                Console.ReadKey(true);
                return;
            }

            PhonesManagerView phonesManagerView = new PhonesManagerView(contact);
            phonesManagerView.Show();
        }

        private void Add()
        {
            Console.Clear();

            Contact contact = new Contact();
            contact.ParentUserId = AuthenticationService.LoggedUser.Id;

            Console.WriteLine("Add new Contact:");
            Console.Write("Full Name: ");
            contact.FullName = Console.ReadLine();
            Console.Write("Email: ");
            contact.Email = Console.ReadLine();

            ContactsRepository contactsRepository = new ContactsRepository("contacts.txt");
            contactsRepository.Save(contact);

            Console.WriteLine("Contact saved successfully.");
            Console.ReadKey(true);

            PhonesManagerView phoneManagerView = new PhonesManagerView(contact);
            phoneManagerView.Show();
        }

        private void Update()
        {
            Console.Clear();

            Console.Write("Contact ID: ");
            int contactId = Convert.ToInt32(Console.ReadLine());

            ContactsRepository contactsRepository = new ContactsRepository("contacts.txt");
            Contact contact = contactsRepository.GetById(contactId);

            if (contact == null)
            {
                Console.Clear();
                Console.WriteLine("Contact not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Editing Contact (" + contact.FullName + ")");
            Console.WriteLine("ID:" + contact.Id);

            Console.WriteLine("Name :" + contact.FullName);
            Console.Write("New Name:");
            string fullName = Console.ReadLine();
            Console.WriteLine("Email :" + contact.Email);
            Console.Write("New Email :");
            string email = Console.ReadLine();

            if (!string.IsNullOrEmpty(fullName))
                contact.FullName = fullName;
            if (!string.IsNullOrEmpty(email))
                contact.Email = email;

            contactsRepository.Save(contact);

            Console.WriteLine("Contact saved successfully.");
            Console.ReadKey(true);

            PhonesManagerView phoneManagerView = new PhonesManagerView(contact);
            phoneManagerView.Show();
        }
        
        private void Delete()
        {
            ContactsRepository contactsRepository = new ContactsRepository("contacts.txt");

            Console.Clear();

            Console.WriteLine("Delete Contact:");
            Console.Write("Contact Id: ");
            int contactId = Convert.ToInt32(Console.ReadLine());

            Contact contact = contactsRepository.GetById(contactId);
            if (contact == null)
            {
                Console.WriteLine("Contact not found!");
            }
            else
            {
                contactsRepository.Delete(contact);
                Console.WriteLine("Contact deleted successfully.");
            }
            Console.ReadKey(true);
        }
    }
}
