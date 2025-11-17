using ContactManagement.Data;
using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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
                
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }                      
           
            var contactToDelete = await _context.Contacts
                .IgnoreQueryFilters() 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contactToDelete == null)
            {            
                return NotFound();
            }
                        
            contactToDelete.IsDeleted = true;
            _context.Attach(contactToDelete).State = EntityState.Modified;

            await _context.SaveChangesAsync();
                        
            return RedirectToPage("./Index");
        }
    }
}