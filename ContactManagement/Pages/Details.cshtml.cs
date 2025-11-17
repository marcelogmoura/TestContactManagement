using ContactManagement.Data;
using ContactManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}