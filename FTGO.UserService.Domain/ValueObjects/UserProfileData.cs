using FluentResults;

namespace FTGO.UserService.Domain.ValueObjects;

public record UserProfileData
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
    public DateOnly BirthdayDate { get; init; }
    
    private UserProfileData() { }

    private UserProfileData(string firstName, string lastName, string phoneNumber, string email, DateOnly birthdayDate)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        BirthdayDate = birthdayDate;
    }

    public static Result<UserProfileData> Create(string firstName, string lastName, string phoneNumber, string email, DateOnly birthdayDate)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(firstName))
        {
            return Result.Fail($"{nameof(firstName)} cannot be null or empty");
        }
        if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(lastName))
        {
            return Result.Fail($"{nameof(lastName)} cannot be null or empty");
        }
        if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(phoneNumber))
        {
            return Result.Fail($"{nameof(phoneNumber)} cannot be null or empty");
        }
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email))
        {
            return Result.Fail($"{nameof(email)} cannot be null or empty");
        }

        return Result.Ok(new UserProfileData(firstName, lastName, phoneNumber, email, birthdayDate));
    }
}