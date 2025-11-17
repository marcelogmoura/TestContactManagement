using ContactManagement.Data;
using ContactManagement.Models;
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
        public Contact Contact { get; set; } = new();
                
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

            Contact = contact;
            return Page();
        }
                
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (await _context.Contacts.AnyAsync(c => c.Email == Contact.Email && c.Id != Contact.Id))
            {
                ModelState.AddModelError("Contact.Email", "Este e-mail já está cadastrado em outro contato.");
            }
            if (await _context.Contacts.AnyAsync(c => c.Phone == Contact.Phone && c.Id != Contact.Id))
            {
                ModelState.AddModelError("Contact.Phone", "Este telefone (contato) já está cadastrado em outro contato.");
            }
                        
            if (!ModelState.IsValid)
            {
                return Page();
            }
                        
            _context.Attach(Contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {                
                if (!_context.Contacts.Any(e => e.Id == Contact.Id))
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