using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhonebook.View
{
    public class AdminView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Administration View:");
                    Console.WriteLine("[U]ser Management");
                    Console.WriteLine("[C]ontacts Management");
                    Console.WriteLine("E[x]it");

                    string choice = Console.ReadLine();
                    switch (choice.ToUpper())
                    {
                        case "U":
                            {
                                UserManagementView view = new UserManagementView();
                                view.Show();

                                break;
                            }
                        case "C":
                            {
                                ContactsManagerView view = new ContactsManagerView();
                                view.Show();

                                break;
                            }
                        case "X":
                            {
                                return;
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
        }
    }
}
