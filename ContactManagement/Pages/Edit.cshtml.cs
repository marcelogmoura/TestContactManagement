using ContactManagement.Data;
using ContactManagement.Models;
using ContactManagement.Models.Dtos;  
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ContactManagement.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
                
        [BindProperty]
        public EditContactDto Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                        
            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);

            if (contact == null)
            {
                return NotFound();
            }
                        
            Input.Id = contact.Id;
            Input.Name = contact.Name;
            Input.Phone = contact.Phone;
            Input.Email = contact.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {            
            if (!ModelState.IsValid)
            {
                return Page();
            }
                        
            var contactToUpdate = await _context.Contacts.FindAsync(Input.Id);

            if (contactToUpdate == null)
            {
                return NotFound();
            }
                        
            if (await _context.Contacts.AnyAsync(c => c.Id != Input.Id && c.Email == Input.Email))
            {
                ModelState.AddModelError("Input.Email", "Verifique o email informado.");
            }
            if (await _context.Contacts.AnyAsync(c => c.Id != Input.Id && c.Phone == Input.Phone))
            {
                ModelState.AddModelError("Input.Phone", "Verifique o número informado.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
                        
            contactToUpdate.Name = Input.Name;
            contactToUpdate.Phone = Input.Phone;
            contactToUpdate.Email = Input.Email;
            
            try
            {            
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Contacts.AnyAsync(e => e.Id == Input.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}