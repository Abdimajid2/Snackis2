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

namespace Snackis2.Pages.Admin.reportedAdmin
{
    public class EditModel : PageModel
    {
        private readonly Snackis2.Data.Snackis2Context _context;

        public EditModel(Snackis2.Data.Snackis2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report =  await _context.Report.FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }
            Report = report;
           ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id");
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

            _context.Attach(Report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(Report.Id))
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

        private bool ReportExists(Guid id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
