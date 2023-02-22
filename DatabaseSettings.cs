using System.ComponentModel.DataAnnotations;

namespace FluentSettingsValidation
{
    public class DatabaseSettings
    {
        [Required]
        public string Name { get; set; }

        [Range(0,10)]
        public int RetryInterval { get; set; }
    }
}
