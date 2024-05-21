using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Snackis2.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Snackis2User class
public class Snackis2User : IdentityUser
{
   

    [PersonalData]
    public string? UserPfP { get; set; }
}

