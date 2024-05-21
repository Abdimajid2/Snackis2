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
        public Models.Message NewMessages { get; set; } // ska kunna anv�ndas p� html sidan/nya meddelande

        [BindProperty]
        public List<Areas.Identity.Data.Snackis2User> Users { get; set; } //h�mtar alla anv�ndare som �r sparat

        [BindProperty]

        public List<Models.Message> Messages { get; set; } //kunna visa alla meddelande i en lista som sedan loopas igenom p� html-sidan


        public async Task OnGet(string userId)
        {
            //h�mtar anv�ndarna som �r registrerade
            Users = await _userManager.Users.ToListAsync();

            //h�mtar dem meddelande som den inloggade har f�tt
            Messages = await _context.Message
                .Where(m => m.SendTo == User.FindFirstValue(ClaimTypes.NameIdentifier)).OrderByDescending(m => m.SendDate).ToListAsync();
         
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {



                //personen som �r inloggad blir avs�ndaren
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
