using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis2.Data;
using Snackis2.Models;

namespace Snackis2.Pages.Admin.reportedAdmin
{
    public class IndexModel : PageModel
    {
        private readonly Snackis2.Data.Snackis2Context _context;

        public IndexModel(Snackis2.Data.Snackis2Context context)
        {
            _context = context;
        }

        public IList<Report> Report { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Report = await _context.Report
                .Include(r => r.Post).ToListAsync();
        }
    }
}
