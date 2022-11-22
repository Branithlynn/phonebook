using System;

namespace ConsolePhonebook.Entity
{
    public class Contact
    {
        public int Id { get; set; }
        public int ParentUserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return this.FullName + " (" + this.Email + ")";
        }
    }
}
