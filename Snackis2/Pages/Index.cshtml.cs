using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Snackis2.DAL;
using Snackis2.Models;
using System.Security.Claims;

namespace Snackis2.Pages
{
    public class IndexModel : PageModel
    {
       
        public Areas.Identity.Data.Snackis2User Myuser { get; set; }

        public CategoryManager _categoryManager;

        private readonly Data.Snackis2Context _context; //åtkomst till db anslutningen
        public UserManager<Areas.Identity.Data.Snackis2User> _userManager { get; set; }//åtkomst till user, gick ej med ID på html sidan, fråga mike
        public IndexModel(UserManager<Areas.Identity.Data.Snackis2User> userManager, Data.Snackis2Context context, CategoryManager categoryManager)
        {
            _userManager = userManager;
            _context = context;
            _categoryManager = categoryManager;

        }

        [BindProperty]
        public Models.Post post { get; set; } // ska kunna användas senare på HTML-sidan

        [BindProperty]
        public List<Models.Post> Posts { get; set; } //kunna visa lista med post på hemsidan

        [BindProperty]
        public List<Models.Category> Categories { get; set; } //kunna visa lista med kategori på hemsidan

        [BindProperty]
        //comment ska ej användas på onget för att då kommer alla kommentarer visas, det vill jag inte, kommentarerna måste filtererats först
        public Models.Comment Comment { get; set; } //ska kunna användas senare på HTML-sidan

        [BindProperty]
        public Models.Message Message { get; set; }// tror jag behöver göra en egen sida för meddelande, fråga mike

        [BindProperty]
        public Models.Report report { get; set; }




        public async Task OnGet(Guid? categoryId)
        {
            Myuser = await _userManager.GetUserAsync(User);
            if (categoryId != null) // ska visa poster baserat på kategori
            {
                //ska kunna hämta inlägg från databasen baserat på villken kategori man väljer
                Posts = await _context.Post.Where(p => p.KategoryId == categoryId)
                    .Include(p => p.Comments).OrderByDescending(p => p.PostDate).ToListAsync(); //inkluderar kommentarerna för inlägg
            }
            else // om ingen kategori väljs visas alla poster
            {
                Posts = await _context.Post.OrderByDescending(p => p.PostDate).ToListAsync();
            }
            foreach (var post in Posts)
            {
                post.Comments = await _context.Comment // hämtar kommentarer som tillhör inlägget 
                    .Where(c => c.PostId == post.Id).ToListAsync(); //baserat på inläggets id så filtreras kommentarerna
            }

            //hämtar alla  kategorier från databasen
            Categories = await CategoryManager.GetAllProductsFromAPI();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (Comment.CommentText != null)
                {
                    await OnPostCommentAsync();
                }
                else if(post.Text != null) 
                {
                    await OnPostPostAsync();
                }

                else if(report.Id != null)
                {
                    await OnReportPostAsync();

                }






                return RedirectToPage("/Index");

            }
            return Page();

        }

        public async Task<IActionResult> OnPostCommentAsync()
        {
            if (ModelState.IsValid)
            {
                //comment delen

                Comment.CommentDate = DateTime.Now;
                Comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Comment.Add(Comment);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostPostAsync()
        {
            if (ModelState.IsValid)
            {
                post.PostDate = DateTime.Now;
                post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Post.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnReportPostAsync()
        {
            if (ModelState.IsValid)
            {
                report.ReportDate = DateTime.Now;
                report.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Report.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
