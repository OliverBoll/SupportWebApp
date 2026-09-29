using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SupportWebApp.Models;

public class SupportMessage
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "Navn er påkrævet")]
    [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-mail er påkrævet")]
    [EmailAddress(ErrorMessage = "Indtast en gyldig e-mailadresse")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefonnummer er påkrævet")]
    [RegularExpression(@"^\d{8}$",
        ErrorMessage = "Telefonnummeret skal bestå af 8 cifre")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Beskrivelse er påkrævet")]
    [MinLength(10, ErrorMessage = "Beskrivelsen skal være mindst 10 tegn")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Du skal vælge en kategori")]
    [JsonProperty(PropertyName = "category")]
    public string Category { get; set; } = string.Empty;

    public DateTime Date { get; set; } = DateTime.UtcNow;
}