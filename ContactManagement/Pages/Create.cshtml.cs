using ContactManagement.Data;
using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public Contact Contact { get; set; } = new();
                
        public IActionResult OnGet()
        {
            return Page();
        }
                
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (await _context.Contacts.AnyAsync(c => c.Email == Contact.Email))
            {
                ModelState.AddModelError("Contact.Email", "Este e-mail já está cadastrado.");
            }
            if (await _context.Contacts.AnyAsync(c => c.Phone == Contact.Phone))
            {
                ModelState.AddModelError("Contact.Phone", "Este telefone (contato) já está cadastrado.");
            }

            
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            
            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();
                        
            return RedirectToPage("./Index");
        }
    }
}