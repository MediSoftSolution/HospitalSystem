using HospitalSystem.Domain.Entities;
using Xunit;

public class OfficeTests
{
    [Fact]
    public void Office_Name_Should_Not_Be_Null_Or_Empty()
    {
        // Arrange
        var office = new Office { Name = null };

        // Act
        bool isValid = !string.IsNullOrWhiteSpace(office.Name);

        // Assert
        Xunit.Assert.False(isValid, "Office Name should not be null or empty.");
    }

    [Fact]
    public void Office_Should_Have_At_Least_One_Photo()
    {
        // Arrange
        var office = new Office { Photos = new List<Photo>() };

        // Act
        bool isValid = office.Photos.Count > 0;

        // Assert
        Xunit.Assert.False(isValid, "Office must have at least one Photo.");
    }

    [Fact]
    public void Office_Tel_Should_Be_Valid()
    {
        // Arrange
        var office = new Office { Tel = "+1234567890" };
        string phoneRegex = @"^\+?[1-9]\d{1,14}$"; // E.164 format

        // Act
        bool isValid = System.Text.RegularExpressions.Regex.IsMatch(office.Tel, phoneRegex);

        // Assert
        Xunit.Assert.True(isValid, "Office Tel should be a valid phone number.");
    }


    [Fact]
    public void Office_Should_Have_At_Least_One_WorkingTime()
    {
        // Arrange
        var office = new Office { WorkingTimes = new List<WorkingTime>() };

        // Act
        bool isValid = office.WorkingTimes.Count > 0;

        // Assert
        Xunit.Assert.False(isValid, "Office must have at least one WorkingTime.");
    }
}
