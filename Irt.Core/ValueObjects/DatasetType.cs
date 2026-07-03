using Irt.Core.SeedWork;

namespace Irt.Core.ValueObjects;

public class DatasetType : ValueObject
{
    public static DatasetType Derived = new("Derived");
    public static DatasetType Calculated = new("Calculated");
    public static DatasetType Internal = new("Internal");
    public string Value { get; }
    
    private DatasetType(string value) => Value = value;

    public static DatasetType From(string value)
    {
        return value switch
        {
            "Derived" => Derived,
            "Calculated" => Calculated,
            "Internal" => Internal,
            _ => throw new ArgumentException($"Unknown dataset type: {value}"),
        };
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static DatasetType Parse(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("DatasetType cannot be null or empty.", nameof(value));
        }
        
        value = value.ToLowerInvariant().Trim();

        return value switch
        {
            "Derived" => Derived,
            "Calculated" => Calculated,
            "Internal" => Internal,
            _ => throw new ArgumentException($"Unknown dataset type: {value}"),
        };
    }
}