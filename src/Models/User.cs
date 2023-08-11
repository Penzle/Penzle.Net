namespace Penzle.Core.Models;

/// <summary>
///     Represents a User with properties related to personal information, contact details, and preferences.
/// </summary>
public class User
{
    public User()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="User" /> class with specified user name, email, first name, and last
    ///     name.
    /// </summary>
    /// <param name="userName">User's user name.</param>
    /// <param name="email">User's email address.</param>
    /// <param name="firstName">User's first name.</param>
    /// <param name="lastName">User's last name.</param>
    public User(string userName, string email, string firstName, string lastName)
    {
        UserName = userName;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    ///     Gets or sets the user name.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     Gets or sets the email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Gets or sets the phone number.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the user is active or not. Default is true, meaning the user is active.
    /// </summary>
    public bool Status { get; set; } = true;


    /// <summary>
    ///     Gets or sets the comment related to the user.
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    ///     Gets or sets the client's language preference.
    /// </summary>
    public string ClientLanguage { get; set; }

    /// <summary>
    ///     Gets or sets the default content language, defaulting to "en-US".
    /// </summary>
    public string DefaultContentLanguage { get; set; } = "en-US";

    /// <summary>
    ///     Gets or sets the street address.
    /// </summary>
    public string StreetAddress { get; set; }

    /// <summary>
    ///     Gets or sets the state or province.
    /// </summary>
    public string StateOrProvince { get; set; }

    /// <summary>
    ///     Gets or sets the country.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    ///     Gets or sets the city.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    ///     Gets or sets the office location.
    /// </summary>
    public string Office { get; set; }

    /// <summary>
    ///     Gets or sets the postal code.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    ///     Gets or sets the office phone number.
    /// </summary>
    public string OfficePhone { get; set; }
}
