using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Snackis2.Pages
{
    [Authorize(Roles = "Admin")]
    public class ReportedPostsModel : PageModel
    {
        public   Data.Snackis2Context _context;

        public UserManager<Areas.Identity.Data.Snackis2User> _userManager { get; set; }
        public ReportedPostsModel(Data.Snackis2Context context, UserManager<Areas.Identity.Data.Snackis2User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public List<Models.Report> ReportedPost { get; set; }
        public async Task OnGet()
        {
            ReportedPost = await _context.Report.Include(r => r.Post).OrderByDescending(r => r.ReportDate).ToListAsync();

        }
        public async Task<IActionResult> OnPost(Guid id)
        {
            var report = await _context.Report.FindAsync(id);

            if (report != null)
            {
                _context.Report.Remove(report);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }


    }
}
