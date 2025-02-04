using Microsoft.EntityFrameworkCore;
using ContactsApi.Models;

 namespace ContactsApi
{
    public class ContactsDbContext : DbContext
    {
     public ContactsDbContext() 
     {
     }
     public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}