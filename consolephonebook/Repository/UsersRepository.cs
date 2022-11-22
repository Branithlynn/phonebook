using System;
using System.IO;
using System.Collections.Generic;
using ConsolePhonebook.Entity;

namespace ConsolePhonebook.Repository
{
    public class UsersRepository
    {
        private readonly string filePath;
        
        public UsersRepository(string filePath)
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
                    User user = new User();
                    user.Id = Convert.ToInt32(sr.ReadLine());
                    user.FirstName = sr.ReadLine();
                    user.LastName = sr.ReadLine();
                    user.Username = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                    if (id <= user.Id)
                    {
                        id = user.Id + 1;
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

        private void Insert(User item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                sw.WriteLine(item.FirstName);
                sw.WriteLine(item.LastName);
                sw.WriteLine(item.Username);
                sw.WriteLine(item.Password);
                sw.WriteLine(item.IsAdmin);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(User item)
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
                    User user = new User();
                    user.Id = Convert.ToInt32(sr.ReadLine());
                    user.FirstName = sr.ReadLine();
                    user.LastName = sr.ReadLine();
                    user.Username = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                    if (user.Id != item.Id)
                    {
                        sw.WriteLine(user.Id);
                        sw.WriteLine(user.FirstName);
                        sw.WriteLine(user.LastName);
                        sw.WriteLine(user.Username);
                        sw.WriteLine(user.Password);
                        sw.WriteLine(user.IsAdmin);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        sw.WriteLine(item.FirstName);
                        sw.WriteLine(item.LastName);
                        sw.WriteLine(item.Username);
                        sw.WriteLine(item.Password);
                        sw.WriteLine(item.IsAdmin);
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

        public User GetById(int id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(sr.ReadLine());
                    user.FirstName = sr.ReadLine();
                    user.LastName = sr.ReadLine();
                    user.Username = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                    if (user.Id == id)
                    {
                        return user;
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

        public List<User> GetAll()
        {
            List<User> result = new List<User>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(sr.ReadLine());
                    user.FirstName = sr.ReadLine();
                    user.LastName = sr.ReadLine();
                    user.Username = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                    result.Add(user);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public void Delete(User item)
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
                    User user = new User();
                    user.Id = Convert.ToInt32(sr.ReadLine());
                    user.FirstName = sr.ReadLine();
                    user.LastName = sr.ReadLine();
                    user.Username = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                    if (user.Id != item.Id)
                    {
                        sw.WriteLine(user.Id);
                        sw.WriteLine(user.FirstName);
                        sw.WriteLine(user.LastName);
                        sw.WriteLine(user.Username);
                        sw.WriteLine(user.Password);
                        sw.WriteLine(user.IsAdmin);
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

        public void Save(User item)
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

        public User GetByUsernameAndPassword(string username, string password)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(sr.ReadLine());
                    user.FirstName = sr.ReadLine();
                    user.LastName = sr.ReadLine();
                    user.Username = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.IsAdmin = Convert.ToBoolean(sr.ReadLine());

                    if (user.Username == username && user.Password == password)
                    {
                        return user;
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
    }
}
