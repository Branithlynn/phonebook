using ConsolePhonebook.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhonebook.Repository
{
    public class PhonesRepository
    {
        private readonly string filePath;

        public PhonesRepository(string filePath)
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
                    Phone phone = new Phone();
                    phone.Id = Convert.ToInt32(sr.ReadLine());
                    phone.ParentContactId = Convert.ToInt32(sr.ReadLine());
                    phone.PhoneNumber = sr.ReadLine();

                    if (id <= phone.Id)
                    {
                        id = phone.Id + 1;
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

        private void Insert(Phone item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                sw.WriteLine(item.ParentContactId);
                sw.WriteLine(item.PhoneNumber);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(Phone item)
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
                    Phone phone = new Phone();
                    phone.Id = Convert.ToInt32(sr.ReadLine());
                    phone.ParentContactId = Convert.ToInt32(sr.ReadLine());
                    phone.PhoneNumber = sr.ReadLine();

                    if (phone.Id != item.Id)
                    {
                        sw.WriteLine(phone.Id);
                        sw.WriteLine(phone.ParentContactId);
                        sw.WriteLine(phone.PhoneNumber);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        sw.WriteLine(item.ParentContactId);
                        sw.WriteLine(item.PhoneNumber);
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

        public Phone GetById(int id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Phone phone = new Phone();
                    phone.Id = Convert.ToInt32(sr.ReadLine());
                    phone.ParentContactId = Convert.ToInt32(sr.ReadLine());
                    phone.PhoneNumber = sr.ReadLine();

                    if (phone.Id == id)
                    {
                        return phone;
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

        public List<Phone> GetAll()
        {
            List<Phone> result = new List<Phone>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Phone phone = new Phone();
                    phone.Id = Convert.ToInt32(sr.ReadLine());
                    phone.ParentContactId = Convert.ToInt32(sr.ReadLine());
                    phone.PhoneNumber = sr.ReadLine();

                    result.Add(phone);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public List<Phone> GetAll(int parentContactId)
        {
            List<Phone> result = new List<Phone>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Phone phone = new Phone();
                    phone.Id = Convert.ToInt32(sr.ReadLine());
                    phone.ParentContactId = Convert.ToInt32(sr.ReadLine());
                    phone.PhoneNumber = sr.ReadLine();

                    if (phone.ParentContactId == parentContactId)
                    {
                        result.Add(phone);
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

        public void Delete(Phone item)
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
                    Phone phone = new Phone();
                    phone.Id = Convert.ToInt32(sr.ReadLine());
                    phone.ParentContactId = Convert.ToInt32(sr.ReadLine());
                    phone.PhoneNumber = sr.ReadLine();

                    if (phone.Id != item.Id)
                    {
                        sw.WriteLine(phone.Id);
                        sw.WriteLine(phone.ParentContactId);
                        sw.WriteLine(phone.PhoneNumber);
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

        public void Save(Phone item)
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
