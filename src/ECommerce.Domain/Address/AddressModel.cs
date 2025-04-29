using System;
using ECommerce.Domain.Common;

namespace ECommerce.Domain.Address;

public class AddressModel : BaseModel
{
    //props
    public string Country { get; private set; }
    public string City { get; private set; }
    public string? Street { get; private set; }
    public string? Alley { get; private set; }
    public string Plate { get; private set; }

    //navigation
    public Guid UserId { get; private set; }

    //ctor
    public AddressModel(
        Guid _userId,
        string _country,
        string _city,
        string _street,
        string _alley,
        string _plate,
        DateTime _createDate,
        DateTime _modifiedDate,
        Guid? id = null) : base(_createDate,_modifiedDate)
    {
        Id = id ?? Guid.NewGuid();
        UserId = _userId;
        Country = _country;
        City = _city;
        Street = _street;
        Alley = _alley;
        Plate = _plate;   
    }

    private AddressModel()
    { }

    //methods
    #region Setters

    public void ChangeCountry(string? country)
    {
        Country = country ?? Country;
    }
    
    public void ChangeCity(string? city)
    {
        City = city ?? City;
    }
    
    public void ChangeStreet(string? street)
    {
        Street = street ?? Street;
    }
    
    public void ChangeAlley(string? alley)
    {
        Alley = alley ?? Alley;
    }
    
    public void ChangePlate(string? plate)
    {
        Plate = plate ?? Plate;
    }

    public void ChangeModifiedDate(DateTime? modifiedDate)
    {
        ModifiedDate = modifiedDate ?? ModifiedDate;
    }
    
    #endregion
}
