using System.ComponentModel.DataAnnotations;

namespace Ship.Ses.Extractor.UI.BlazorWeb.Models.ApiClient
{
    // Make sure this enum matches the one in your Domain/Application layer
    public enum DatabaseType
    {
        MySql = 0,
        PostgreSql = 1,
        MsSql = 2
    }

    public class EmrConnectionModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Database Type is required.")]
        public DatabaseType DatabaseType { get; set; }

        [Required(ErrorMessage = "Server address is required.")]
        [StringLength(255, ErrorMessage = "Server address cannot exceed 255 characters.")]
        public string Server { get; set; }

        [Required(ErrorMessage = "Port is required.")]
        [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535.")]
        public int Port { get; set; }

        [Required(ErrorMessage = "Database name is required.")]
        [StringLength(255, ErrorMessage = "Database name cannot exceed 255 characters.")]
        public string DatabaseName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(255, ErrorMessage = "Username cannot exceed 255 characters.")]
        public string Username { get; set; }

        // We will send password for creation/update, but not retrieve it
        // Consider making this optional/conditional based on update vs create.
        [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}

