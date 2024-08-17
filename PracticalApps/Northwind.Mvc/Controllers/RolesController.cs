using Microsoft.AspNetCore.Identity; // To use RoleManager, UserManager.
using Microsoft.AspNetCore.Mvc; // To use Controller, IActionResult.

namespace Northwind.Mvc.Controllers;

public class RolesController : Controller
{
    private readonly string _adminRole = "Administrators";
    private readonly string _adminUserEmail = "admin@example.com";
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RolesController(RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        if (!await _roleManager.RoleExistsAsync(_adminRole))
        {
            await _roleManager.CreateAsync(new IdentityRole(_adminRole));
        }

        IdentityUser? user = await _userManager.FindByEmailAsync(_adminUserEmail);

        if (user is null)
        {
            user = new()
            {
                UserName = _adminUserEmail,
                Email = _adminUserEmail
            };

            IdentityResult identityResult = await _userManager.CreateAsync(user, "Pa$$w0rd");
            LogIdentityResult(identityResult, $"User {user.UserName} created successfully.");
        }

        if (!user.EmailConfirmed)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            IdentityResult identityResult = await _userManager.ConfirmEmailAsync(user, token);
            LogIdentityResult(identityResult, $"User {user.UserName} email confirmed successfully.");
        }

        if (!await _userManager.IsInRoleAsync(user, _adminRole))
        {
            IdentityResult identityResult = await _userManager.AddToRoleAsync(user, _adminRole);
            LogIdentityResult(identityResult, $"User {user.UserName} added to {_adminRole} successfully.");
        }

        return Redirect("/");
    }

    private void LogIdentityResult(IdentityResult identityResult, string successMessage)
    {
        if (identityResult.Succeeded)
        {
            WriteLine(successMessage);
        }
        else
        {
            foreach (IdentityError error in identityResult.Errors)
            {
                WriteLine(error.Description);
            }
        }
    }
}