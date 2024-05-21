using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Snackis2.Data;
using Snackis2.Models;

namespace Snackis2.Pages.Admin.reportedAdmin
{
    public class CreateModel : PageModel
    {
        private readonly Snackis2.Data.Snackis2Context _context;

        public CreateModel(Snackis2.Data.Snackis2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Report Report { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Report.Add(Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
