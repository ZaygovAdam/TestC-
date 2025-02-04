using Microsoft.AspNetCore.Mvc;
using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
   public class ContactsController : ControllerBase
   {
       private readonly ContactsDbContext _context;

       public ContactsController(ContactsDbContext context)
       {
           _context = context;
       }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<Contact>>> GetContacts(string? lastName = null)
       {
           IQueryable<Contact> contactsQuery = _context.Contacts;

           if (!string.IsNullOrEmpty(lastName))
           {
               contactsQuery = contactsQuery.Where(c => c.LastName.Contains(lastName));
           }

           return await contactsQuery.ToListAsync();
       }

       [HttpPost]
       public async Task<ActionResult<Contact>> CreateContact(Contact contact)
       {
           _context.Contacts.Add(contact);
           await _context.SaveChangesAsync();

           return CreatedAtAction(nameof(GetContacts), new { id = contact.Id }, contact);
       }

       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteContact(int id)
       {
          var contact = await _context.Contacts.FindAsync(id);
           if (contact == null)
           {
               return NotFound();
           }

           _context.Contacts.Remove(contact);
           await _context.SaveChangesAsync();

           return NoContent();
       }
   }
}