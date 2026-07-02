namespace Irt.Core.ValueObjects;
public record Address
{
    private Address(
    
        string street,
        string city,
        string state,
        string country,
        string zipCode
    )
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; set; }
    public string ZipCode { get; init; }

    //public static Result<Address> Create(
    public static Address Create(
        string street,
        string city,
        string state,
        string country,
        string zipCode
    )
    {
        // Check if address is valid

        return new Address(
            street,
            city,
            state,
            country,
            zipCode
        );
    }
}