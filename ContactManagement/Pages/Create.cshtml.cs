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
                ModelState.AddModelError("Input.Email", "Este e-mail já está cadastrado.");
            }
            if (await _context.Contacts.AnyAsync(c => c.Phone == Input.Phone))
            {
                ModelState.AddModelError("Input.Phone", "Este telefone já está cadastrado.");
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

            
            try
            {
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateException ex)
            {          
                ModelState.AddModelError(string.Empty, "Erro ao salvar o contato. Já existe um registro com o mesmo e-mail ou telefone informado.");
                          
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}