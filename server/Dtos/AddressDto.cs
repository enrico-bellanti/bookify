namespace Bookify.Dtos
{
    public class AddressDto
    {
        public required string Street { get; set; }
        public required string Number { get; set; }
        public required string City { get; set; }
        public required string Province { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }
        public string? AdditionalInfo { get; set; }

        // Coordinate geografiche (opzionali)
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
