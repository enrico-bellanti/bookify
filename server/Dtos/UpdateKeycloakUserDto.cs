namespace Bookify.Dtos
{
    public class UpdateKeycloakUserDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsActive { get; set; }
    }
}
