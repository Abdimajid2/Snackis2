using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Snackis2.Migrations;
using System.Security.Claims;

namespace Snackis2.Pages
{
    [Authorize]
    public class PrivateMessagesModel : PageModel
    {

        //public Areas.Identity.Data.Snackis2User user { get; set; }

        private readonly Data.Snackis2Context _context;
        public UserManager<Areas.Identity.Data.Snackis2User> _userManager { get; set; }
        public PrivateMessagesModel(UserManager<Areas.Identity.Data.Snackis2User> userManager, Data.Snackis2Context context)
        {
            _userManager = userManager;
            _context = context;

        }

        [BindProperty]
        public Models.Message NewMessages { get; set; } // ska kunna användas på html sidan/nya meddelande

        [BindProperty]
        public List<Areas.Identity.Data.Snackis2User> Users { get; set; } //hämtar alla användare som är sparat

        [BindProperty]

        public List<Models.Message> Messages { get; set; } //kunna visa alla meddelande i en lista som sedan loopas igenom på html-sidan


        public async Task OnGet(string userId)
        {
            //hämtar användarna som är registrerade
            Users = await _userManager.Users.ToListAsync();

            //hämtar dem meddelande som den inloggade har fått
            Messages = await _context.Message
                .Where(m => m.SendTo == User.FindFirstValue(ClaimTypes.NameIdentifier)).OrderByDescending(m => m.SendDate).ToListAsync();
         
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {



                //personen som är inloggad blir avsändaren
                NewMessages.SendFrom = User.FindFirstValue(ClaimTypes.NameIdentifier);
                NewMessages.SendDate = DateTime.Now;
                _context.Add(NewMessages);
                await _context.SaveChangesAsync();

                return RedirectToPage("/PrivateMessages");

            }
            return Page();
        }     
   
    }
}
