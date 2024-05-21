using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Snackis2.Data;
using Snackis2.Models;

namespace Snackis2.Pages.Admin.MessageAdmin
{
    public class EditModel : PageModel
    {
        private readonly Snackis2.Data.Snackis2Context _context;

        public EditModel(Snackis2.Data.Snackis2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Message Message { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message =  await _context.Message.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            Message = message;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(Message.Id))
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

        private bool MessageExists(Guid id)
        {
            return _context.Message.Any(e => e.Id == id);
        }
    }
}
