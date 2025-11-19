using ContactManagement.Data;
using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ContactManagement.Models.Dtos;

namespace ContactManagement.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public ContactDto Input { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }
                
        public async Task<IActionResult> OnPostAsync()
        {            
            if (await _context.Contacts.AnyAsync(c => c.Email == Input.Email))
            {
                ModelState.AddModelError("Contact.Email", "Este e-mail já está cadastrado.");
            }
            if (await _context.Contacts.AnyAsync(c => c.Phone == Input.Phone))
            {
                ModelState.AddModelError("Contact.Phone", "Este telefone (contato) já está cadastrado.");
            }

            
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            var newContact = new Contact
            {
                Name = Input.Name,
                Phone = Input.Phone,
                Email = Input.Email,
                IsDeleted = false  
            };


            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();
                        
            return RedirectToPage("./Index");
        }
    }
}