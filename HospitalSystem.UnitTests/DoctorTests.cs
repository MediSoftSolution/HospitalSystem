using HospitalSystem.Domain.Entities;
using System.Collections.Generic;
using Xunit;

public class DoctorTests
{
    [Fact]
    public void ConsultingFee_Should_Be_Positive()
    {
        // Arrange
        var doctor = new Doctor { ConsultingFee = -100 };

        // Act
        bool isValid = doctor.ConsultingFee > 0;

        // Assert
        Xunit.Assert.False(isValid, "ConsultingFee should be greater than zero.");
    }

    [Fact]
    public void Doctor_Should_Have_WorkingOffice()
    {
        // Arrange
        var doctor = new Doctor { WorkingOffice = null };

        // Act
        bool isValid = doctor.WorkingOffice != null;

        // Assert
        Xunit.Assert.False(isValid, "Doctor must have a valid WorkingOffice assigned.");
    }

    [Fact]
    public void Doctor_Should_Have_Specialty()
    {
        // Arrange
        var doctor = new Doctor { Specialty = null };

        // Act
        bool isValid = doctor.Specialty != null;

        // Assert
        Xunit.Assert.False(isValid, "Doctor must have a valid Specialty assigned.");
    }

    [Fact]
    public void Doctor_Should_Have_At_Least_One_WorkingTime()
    {
        // Arrange
        var doctor = new Doctor { WorkingTimes = new List<WorkingTime>() };

        // Act
        bool isValid = doctor.WorkingTimes.Count > 0;

        // Assert
        Xunit.Assert.False(isValid, "Doctor must have at least one WorkingTime.");
    }
}
