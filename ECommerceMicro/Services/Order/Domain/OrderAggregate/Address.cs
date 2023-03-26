using Domain.Core;
namespace Domain.OrderAggregate;
public class Address : ValueObject
{
    public string Province { get; private set; }
    public string District { get; private set; }
    public string Street { get; private set; }
    public string ZipCode { get; private set; }
    public string Line { get; private set; }

    public Address(string province, string district, string street, string zipCode, string line)
    {
        this.Province = province;
        this.District = district;
        this.Street = street;
        this.ZipCode = zipCode;
        this.Line = line;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Province;
        yield return District;
        yield return Street;
        yield return ZipCode;
        yield return Line;
    }
}
