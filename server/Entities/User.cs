using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Entities
{
    [PrimaryKey("Id")]
    public class User
    {
        public User()
        {
            RegistrationDate = DateTime.UtcNow;
            IsActive = true;
        }

        public int Id { get; set; }
        public required Guid Uuid { get; set; }
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        // Con virtual, gli Address verranno caricati solo quando si accede a questa proprietà
        public virtual Address Address { get; set; }

    }
}
