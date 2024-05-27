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

        private readonly Data.Snackis2Context _context; //�tkomst till db anslutningen
        public UserManager<Areas.Identity.Data.Snackis2User> _userManager { get; set; }//�tkomst till user, gick ej med ID p� html sidan, fr�ga mike
        public IndexModel(UserManager<Areas.Identity.Data.Snackis2User> userManager, Data.Snackis2Context context, CategoryManager categoryManager)
        {
            _userManager = userManager;
            _context = context;
            _categoryManager = categoryManager;

        }

        [BindProperty]
        public Models.Post post { get; set; } // ska kunna anv�ndas senare p� HTML-sidan

        [BindProperty]
        public List<Models.Post> Posts { get; set; } //kunna visa lista med post p� hemsidan

        [BindProperty]
        public List<Models.Category> Categories { get; set; } //kunna visa lista med kategori p� hemsidan

        [BindProperty]
        //comment ska ej anv�ndas p� onget f�r att d� kommer alla kommentarer visas, det vill jag inte, kommentarerna m�ste filtererats f�rst
        public Models.Comment Comment { get; set; } //ska kunna anv�ndas senare p� HTML-sidan

        [BindProperty]
        public Models.Message Message { get; set; }// tror jag beh�ver g�ra en egen sida f�r meddelande, fr�ga mike

        [BindProperty]
        public Models.Report report { get; set; }




        public async Task OnGet(Guid? categoryId)
        {
            Myuser = await _userManager.GetUserAsync(User);
            if (categoryId != null) // ska visa poster baserat p� kategori
            {
                //ska kunna h�mta inl�gg fr�n databasen baserat p� villken kategori man v�ljer
                Posts = await _context.Post.Where(p => p.KategoryId == categoryId)
                    .Include(p => p.Comments).OrderByDescending(p => p.PostDate).ToListAsync(); //inkluderar kommentarerna f�r inl�gg
            }
            else // om ingen kategori v�ljs visas alla poster
            {
                Posts = await _context.Post.OrderByDescending(p => p.PostDate).ToListAsync();
            }
            foreach (var post in Posts)
            {
                post.Comments = await _context.Comment // h�mtar kommentarer som tillh�r inl�gget 
                    .Where(c => c.PostId == post.Id).ToListAsync(); //baserat p� inl�ggets id s� filtreras kommentarerna
            }

            //h�mtar alla  kategorier fr�n databasen
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
