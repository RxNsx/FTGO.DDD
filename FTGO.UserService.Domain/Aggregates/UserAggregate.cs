using System.Security.Principal;
using FluentResults;
using FTGO.UserService.Domain.ValueObjects;
using SharedKernel.Abstractions;

namespace FTGO.UserService.Domain.Aggregates;

public class UserAggregate : IAggregateRoot
{
    //TODO: Twillio
    //TODO: SmtpClient
    
    private UserProfileData _userProfileData;
    private List<Address> _addresses = [];
    
    public Guid Id { get; set; }
    public string Email => _userProfileData.Email;
    public string FirstName => _userProfileData.FirstName;
    public string LastName => _userProfileData.LastName;
    public string Phone => _userProfileData.FirstName;
    public DateOnly BirthDayDate => _userProfileData.BirthdayDate;
    
    public UserProfileData UserProfileData => _userProfileData;
    public IEnumerable<Address> Addresses => _addresses;
    
    private UserAggregate() { }

    private UserAggregate(UserProfileData userProfileData)
    {
        Id = Guid.NewGuid();
        _userProfileData = userProfileData;
        _addresses = [];
    }

    private UserAggregate(UserProfileData userProfileData, List<Address> addresses) : this(userProfileData)
    {
        _addresses = addresses;
    }
    
    public Result<UserProfileData> SetEmail(string newEmail)
    {
        var resultUserProfileData = UserProfileData.Create(_userProfileData.FirstName, _userProfileData.LastName, _userProfileData.PhoneNumber, newEmail, _userProfileData.BirthdayDate);
        if (resultUserProfileData.IsSuccess)
        {
            _userProfileData = resultUserProfileData.Value;
        }

        return resultUserProfileData;
    }

    public Result<UserProfileData> SetFirstName(string newFirstName)
    {
        var resultUserProfileData = UserProfileData.Create(newFirstName, _userProfileData.LastName, _userProfileData.PhoneNumber, _userProfileData.Email, _userProfileData.BirthdayDate);
        if (resultUserProfileData.IsSuccess)
        {
            _userProfileData = resultUserProfileData.Value;
        }
        
        return resultUserProfileData;
    }

    public Result<UserProfileData> SetLastName(string newLastName)
    {
        var resultUserProfileData = UserProfileData.Create(_userProfileData.FirstName, newLastName, _userProfileData.PhoneNumber, _userProfileData.Email, _userProfileData.BirthdayDate);
        if (resultUserProfileData.IsSuccess)
        {
            _userProfileData = resultUserProfileData.Value;
        }
        
        return resultUserProfileData;
    }

    public Result<UserProfileData> SetPhoneNumber(string newPhoneNumber)
    {
        var resultUserProfileData = UserProfileData.Create(_userProfileData.FirstName, _userProfileData.LastName, newPhoneNumber, _userProfileData.Email, _userProfileData.BirthdayDate);
        if (resultUserProfileData.IsSuccess)
        {
            _userProfileData = resultUserProfileData.Value;
        }
        
        return resultUserProfileData;
    }
    
    public Result<UserProfileData> SetBirthdayDate(DateOnly newBirthdayDate)
    {
        var resultUserProfileData = UserProfileData.Create(_userProfileData.FirstName, _userProfileData.LastName, _userProfileData.PhoneNumber, _userProfileData.Email, newBirthdayDate);
        if (resultUserProfileData.IsSuccess)
        {
            _userProfileData = resultUserProfileData.Value;
        }

        return resultUserProfileData;
    }
    
    public Result<Address> AddAddress(string city, string street, string houseNumber, int entrance, int floor, string details)
    {
        var resultCreateAddress = Address.Create(city, street, houseNumber, entrance, floor, details);
        if (resultCreateAddress.IsSuccess)
        {
            _addresses.Add(resultCreateAddress.Value);
        }
        
        return resultCreateAddress;
    }

    public Result RemoveAddress(string city, string street, string houseNumber)
    {
        var addressToRemove = _addresses.FirstOrDefault(x => x.City.Equals(city, StringComparison.OrdinalIgnoreCase) 
                                       && x.Street.Equals(street, StringComparison.OrdinalIgnoreCase) 
                                       && x.HouseNumber.Equals(houseNumber, StringComparison.OrdinalIgnoreCase));

        if (addressToRemove is null)
        {
            return Result.Fail($"{nameof(addressToRemove)} cannot be removed");
        }

        return Result.Ok();
    }

    public UserAggregate GetUser()
    {
        return this;
    }

    public static Result<UserAggregate> Create(string firstName, string lastName, string phoneNumber, string email, DateOnly birthdayDate)
    {
        var userProfileData = UserProfileData.Create(firstName, lastName, phoneNumber, email, birthdayDate);
        if (userProfileData.IsFailed)
        {
            return Result.Fail<UserAggregate>(userProfileData.Errors);
        }
        
        return Result.Ok(new UserAggregate(userProfileData.Value));
    }
}