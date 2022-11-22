using ConsolePhonebook.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsolePhonebook.Repository
{
    public class ContactsRepository
    {
        private readonly string filePath;

        public ContactsRepository(string filePath)
        {
            this.filePath = filePath;
        }

        private int GetNextId()
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            int id = 1;
            try
            {
                while (!sr.EndOfStream)
                {
                    Contact contact = new Contact();
                    contact.Id = Convert.ToInt32(sr.ReadLine());
                    contact.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    contact.FullName = sr.ReadLine();
                    contact.Email = sr.ReadLine();

                    if (id <= contact.Id)
                    {
                        id = contact.Id + 1;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return id;
        }

        private void Insert(Contact item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                sw.WriteLine(item.ParentUserId);
                sw.WriteLine(item.FullName);
                sw.WriteLine(item.Email);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(Contact item)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Contact contact = new Contact();
                    contact.Id = Convert.ToInt32(sr.ReadLine());
                    contact.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    contact.FullName = sr.ReadLine();
                    contact.Email = sr.ReadLine();

                    if (contact.Id != item.Id)
                    {
                        sw.WriteLine(contact.Id);
                        sw.WriteLine(contact.ParentUserId);
                        sw.WriteLine(contact.FullName);
                        sw.WriteLine(contact.Email);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        sw.WriteLine(item.ParentUserId);
                        sw.WriteLine(item.FullName);
                        sw.WriteLine(item.Email);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public Contact GetById(int id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Contact contact = new Contact();
                    contact.Id = Convert.ToInt32(sr.ReadLine());
                    contact.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    contact.FullName = sr.ReadLine();
                    contact.Email = sr.ReadLine();

                    if (contact.Id == id)
                    {
                        return contact;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return null;
        }

        public List<Contact> GetAll()
        {
            List<Contact> result = new List<Contact>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Contact contact = new Contact();
                    contact.Id = Convert.ToInt32(sr.ReadLine());
                    contact.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    contact.FullName = sr.ReadLine();
                    contact.Email = sr.ReadLine();

                    result.Add(contact);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public List<Contact> GetAll(int parentUserId)
        {
            List<Contact> result = new List<Contact>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Contact contact = new Contact();
                    contact.Id = Convert.ToInt32(sr.ReadLine());
                    contact.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    contact.FullName = sr.ReadLine();
                    contact.Email = sr.ReadLine();

                    if (contact.ParentUserId == parentUserId)
                    {
                        result.Add(contact);
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public void Delete(Contact item)
        {
            string tempFilePath = "temp." + filePath;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Contact contact = new Contact();
                    contact.Id = Convert.ToInt32(sr.ReadLine());
                    contact.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    contact.FullName = sr.ReadLine();
                    contact.Email = sr.ReadLine();

                    if (contact.Id != item.Id)
                    {
                        sw.WriteLine(contact.Id);
                        sw.WriteLine(contact.ParentUserId);
                        sw.WriteLine(contact.FullName);
                        sw.WriteLine(contact.Email);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public void Save(Contact item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }
    }
}
