using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecondaApp.Abstract;
using SecondaApp.Model;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            Nome = model.Nome,
            Cognome = model.Cognome,
            NumeroDiTelefono = model.PhoneNumber // Usa la tua proprietà personalizzata
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Genera il token di conferma email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Crea il link di conferma
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token = token }, Request.Scheme);

            // Invia email
            await _emailSender.SendEmailAsync(user.Email, "Conferma la tua email", $"Per favore conferma la tua email cliccando su questo link: <a href='{confirmationLink}'>link</a>");

            return Ok(new { Message = "User created successfully. Please confirm your email." });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            return BadRequest("User not found or email not confirmed.");
        }

        // Genera il token di reset password
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Crea il link per resettare la password
        var resetLink = Url.Action(nameof(ResetPassword), "Account", new { token = token, email = user.Email }, Request.Scheme);

        // Invia email
        await _emailSender.SendEmailAsync(user.Email, "Reset Password", $"Resetta la tua password cliccando su questo link: <a href='{resetLink}'>link</a>");

        return Ok("Password reset email sent.");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (result.Succeeded)
        {
            return Ok("Password has been reset.");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return BadRequest(ModelState);
    }


    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (userId == null || token == null)
            return BadRequest("Invalid email confirmation request.");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return BadRequest("Invalid user ID.");

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return Ok("Email confirmed successfully.");
        }

        return BadRequest("Email confirmation failed.");
    }


    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        user.Nome = model.Nome;
        user.Cognome = model.Cognome;
        user.NumeroDiTelefono = model.PhoneNumber;
        user.Email = model.Email;
        user.UserName = model.Email;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return Ok(new { Message = "Profile updated successfully" });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpDelete("delete-profile")]
    public async Task<IActionResult> DeleteProfile(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new { Message = "User not found" });
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return Ok(new { Message = "User deleted successfully" });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return BadRequest(ModelState);
    }

}
