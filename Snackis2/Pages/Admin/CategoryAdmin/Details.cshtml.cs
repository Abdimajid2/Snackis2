using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis2.Data;
using Snackis2.Models;

namespace Snackis2.Pages.Admin.CategoryAdmin
{
    public class DetailsModel : PageModel
    {
        private readonly Snackis2.Data.Snackis2Context _context;

        public DetailsModel(Snackis2.Data.Snackis2Context context)
        {
            _context = context;
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }
    }
}
