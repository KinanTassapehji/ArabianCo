using System.ComponentModel.DataAnnotations;

namespace ArabianCo.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}