using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecondaApp.Model;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return BadRequest("Role name cannot be empty.");

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

        if (result.Succeeded)
            return Ok(new { Message = "Role created successfully" });

        return BadRequest(result.Errors);
    }

    [HttpPost("assign")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRoleToUser(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return NotFound(new { Message = "User not found" });

        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (result.Succeeded)
            return Ok(new { Message = "Role assigned to user successfully" });

        return BadRequest(result.Errors);
    }

    [HttpGet("user-roles")]
    [Authorize]
    public async Task<IActionResult> GetUserRoles(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return NotFound(new { Message = "User not found" });

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(roles);
    }
}
