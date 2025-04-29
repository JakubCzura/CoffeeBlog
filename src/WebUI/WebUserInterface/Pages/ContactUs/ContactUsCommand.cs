using System.ComponentModel.DataAnnotations;

namespace WebUserInterface.Pages.ContactUs;

/// <summary>
/// Command to handle user input from the Contact Us form.
/// </summary>
public class ContactUsCommand
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required.")]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Message is required.")]
    [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters.")]
    public string Message { get; set; } = string.Empty;
}
