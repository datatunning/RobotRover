// <copyright file="RoverNavigationServiceShould.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using FluentAssertions;
using NSubstitute;
using RobotRover.App.Models;
using RobotRover.App.Services;
using Xunit;

namespace RobotRover.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class RoverNavigationServiceShould
    {
        [Theory]
        [InlineDataAttribute("A")]
        [InlineDataAttribute("B")]
        [InlineDataAttribute("C")]
        [Trait("Category", "UnitTests")]
        public void Throw_When_SetPosition_With_Invalid_Direction(string direction)
        {
            // Assume
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            var navigationService = new RoverNavigationService(roverRepository);
            navigationService.InitializePlateau(40, 40);
            roverRepository.RoverCount.Returns(1);

            // Act
            Action setPositionAction = () => navigationService.SetPosition(1, 2, 3, direction);

            // Assert
            setPositionAction
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage(new ArgumentOutOfRangeException(nameof(direction)).Message);
        }

        [Theory]
        [InlineDataAttribute(-500)]
        [InlineDataAttribute(500)]
        [Trait("Category", "UnitTests")]
        public void Throw_When_SetPosition_With_Invalid_X_Location(int x)
        {
            // Assume
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            var navigationService = new RoverNavigationService(roverRepository);
            roverRepository.RoverCount.Returns(1);

            // Act
            Action setPositionAction = () => navigationService.SetPosition(1, x, 3, "e");

            // Assert
            setPositionAction
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage(new ArgumentOutOfRangeException(nameof(x)).Message);
        }

        [Theory]
        [InlineDataAttribute(-500)]
        [InlineDataAttribute(500)]
        [Trait("Category", "UnitTests")]
        public void Throw_When_SetPosition_With_Invalid_Y_Location(int y)
        {
            // Assume
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            var navigationService = new RoverNavigationService(roverRepository);
            navigationService.InitializePlateau(40,40);
            roverRepository.RoverCount.Returns(1);

            // Act
            Action setPositionAction = () => navigationService.SetPosition(1, 2, y, "e");

            // Assert
            setPositionAction
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage(new ArgumentOutOfRangeException(nameof(y)).Message);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        public void Set_Plateau_When_InitializePlateau_Called()
        {
            // Assume
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            var navigationService = new RoverNavigationService(roverRepository);

            // Act
            navigationService.InitializePlateau(666, 999);

            // Assert
            navigationService.Plateau.Should().Be(new Point(666, 999));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        public void Set_Properties_To_Default_When_Reset()
        {
            // Assume
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            var navigationService = new RoverNavigationService(roverRepository);

            // Act
            navigationService.Reset();

            // Assert
            navigationService.Plateau.Should().Be(new Point());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        public void Throw_When_Move_With_Empty_Command()
        {
            // Assume
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            roverRepository.RoverCount.Returns(1);
            var navigationService = new RoverNavigationService(roverRepository);

            // Act
            Action setPositionAction = () => navigationService.Move(1, "");

            // Assert
            setPositionAction
                .Should()
                .Throw<ArgumentNullException>()
                .WithMessage(new ArgumentNullException($"commands").Message);
        }

        [Theory]
        [InlineDataAttribute(1, 5, 35, "N")]
        [InlineDataAttribute(1, 23, 2, "E")]
        [InlineDataAttribute(1, 33, 19, "S")]
        [InlineDataAttribute(1, 8, 17, "W")]
        [Trait("Category", "UnitTests")]
        public void Set_Rover_Initial_Position_When_SetPosition_Called(int roverId, int roverX, int roverY, string direction)
        {
            // Assume
            Enum.TryParse(typeof(DirectionEnum), direction, true, out var directionEObj);
            var rover = new RoverModel(1);
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            roverRepository.RoverCount.Returns(1);
            roverRepository.GetRover(roverId).Returns(rover);
            var navigationService = new RoverNavigationService(roverRepository);
            navigationService.InitializePlateau(40, 40);

            // Act
            navigationService.SetPosition(roverId, roverX, roverY, direction);

            // Assert
            rover.Location.Position.Should().Be(new Point(roverX, roverY));
            // ReSharper disable once PossibleNullReferenceException
            rover.Location.Direction.Should().Be((DirectionEnum)directionEObj);
        }

        [Theory]
        // Tests clockwise navigation
        [InlineDataAttribute(1, 10, 10, "N", "R2", 12, 10, DirectionEnum.E)]
        [InlineDataAttribute(1, 10, 10, "E", "R2", 10, 8, DirectionEnum.S)]
        [InlineDataAttribute(1, 10, 10, "S", "R2", 8, 10, DirectionEnum.W)]
        [InlineDataAttribute(1, 10, 10, "W", "R2", 10, 12, DirectionEnum.N)]

        // Test anti-clockwise navigation
        [InlineDataAttribute(1, 10, 10, "N", "L2", 8, 10, DirectionEnum.W)]
        [InlineDataAttribute(1, 10, 10, "W", "L2", 10, 8, DirectionEnum.S)]
        [InlineDataAttribute(1, 10, 10, "S", "L2", 12, 10, DirectionEnum.E)]
        [InlineDataAttribute(1, 10, 10, "E", "L2", 10, 12, DirectionEnum.N)]

        // Test malformed commands
        [InlineDataAttribute(1, 10, 10, "W", "XR2", 10, 10, DirectionEnum.W)] // the whole command is ignored as XR is handled as the direction part.
        [InlineDataAttribute(1, 10, 10, "N", "R2L", 10, 10, DirectionEnum.N)] // the whole command is ignored as there is an odd number of parts: [R, 2 , L]
        [InlineDataAttribute(1, 10, 10, "N", "R2*&5", 12, 10, DirectionEnum.E)]

        // Test out-of-bound navigation
        [InlineDataAttribute(1, 0, 0, "N", "R10R10", 10, 0, DirectionEnum.E)]
        [InlineDataAttribute(1, 0, 0, "E", "L10L10", 0, 10, DirectionEnum.N)]
        [InlineDataAttribute(1, 40, 40, "S", "R10R10", 30, 40, DirectionEnum.W)]
        [InlineDataAttribute(1, 40, 40, "W", "L10L10", 40, 30, DirectionEnum.S)]

        // Test complex navigation
        [InlineDataAttribute(1, 10, 10, "N", "R1R3L2L1", 13, 8, DirectionEnum.N)]

        [Trait("Category", "UnitTests")]
        public void Set_Rover_Position_When_Move_Called(
            int roverId, 
            int roverX, 
            int roverY, 
            string roverD, 
            string commands,
            int expectedX,
            int expectedY,
            DirectionEnum expectedD)
        {
            // Assume
            var rover = new RoverModel(1);

            var roverRepository = Substitute.For<IRoverRepositoryService>();
            roverRepository.RoverCount.Returns(1);
            roverRepository.GetRover(roverId).Returns(rover);

            var navigationService = new RoverNavigationService(roverRepository);
            navigationService.InitializePlateau(40, 40);
            navigationService.SetPosition(roverId, roverX, roverY, roverD);

            // Act
            navigationService.Move(roverId, commands);

            // Assert
            rover.Location.Position.Should().Be(new Point(expectedX, expectedY));
            rover.Location.Direction.Should().Be(expectedD);
        }
    }
}