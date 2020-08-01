// <copyright file="RoverRepositoryServiceShould.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using RobotRover.App.Services;
using Xunit;

namespace RobotRover.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class RoverRepositoryServiceShould
    {
        [Fact]
        [Trait("Category", "UnitTests")]
        public void Increase_RoverCount_When_Adding_Rover()
        {
            // Assume
            var roverRepository = new RoverRepositoryService();

            // Act
            roverRepository.AddRover();

            // Assert
            roverRepository.RoverCount.Should().Be(1);
        }
        
        [Fact]
        [Trait("Category", "UnitTests")]
        public void Throw_When_GetRover_By_Id_With_NonExisting_Id()
        {
            // Assume
            var roverRepository = new RoverRepositoryService();

            // Act
            Action setPositionAction = () => roverRepository.GetRover(99);

            // Assert
            setPositionAction
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage(new ArgumentOutOfRangeException($"Id").Message);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        public void Return_Rover_When_GetRover_By_Id_With_Existing_Id()
        {
            // Assume
            var roverRepository = new RoverRepositoryService();

            var roverId = 3;
            for (var i = 1; i <= roverId; i++) roverRepository.AddRover();

            // Act
            var rover = roverRepository.GetRover(roverId);

            // Assert
            rover.Should().NotBeNull();
            rover.Id.Should().Be(roverId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        public void Set_Properties_To_Default_When_Reset()
        {
            // Assume
            var roverRepository = new RoverRepositoryService();

            // Act
            roverRepository.Reset();

            // Assert
            roverRepository.RoverCount.Should().Be(0);
        }
    }
}