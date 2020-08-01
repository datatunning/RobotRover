// <copyright file="RoverMissionServiceShould.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using FluentAssertions;
using NSubstitute;
using RobotRover.App.Services;
using Xunit;

namespace RobotRover.UnitTests.Services
{
    [ExcludeFromCodeCoverage]
    public class RoverMissionControlServiceShould
    {
        [Fact]
        [Trait("Category", "UnitTests")]
        public void Set_Plateau_When_InitializePlateau_Called()
        {
            // Assume
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            var navigationService = Substitute.For<IRoverNavigationService>();
            var missionControl = new RoverMissionControlService(roverRepository, navigationService);
            navigationService.Plateau.Returns(new Point(40, 40));
            
            // Act
            missionControl.InitializePlateau(40, 40);

            // Assert
            navigationService
                .Received(1)
                .InitializePlateau(Arg.Any<int>(), Arg.Any<int>());
            missionControl.Plateau.Should().Be(new Point(40, 40));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        public void Set_Rover_Initial_Position_When_SetPosition_Called()
        {
            // Assume
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            var navigationService = Substitute.For<IRoverNavigationService>();
            var missionControl = new RoverMissionControlService(roverRepository, navigationService);

            // Act
            missionControl.SetPosition(1, 2, 3, "E");

            // Assert
            navigationService
                .Received(1)
                .SetPosition(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        public void Set_Rover_Position_When_Move_Called()
        {
            // Assume
            var roverRepository = Substitute.For<IRoverRepositoryService>();
            var navigationService = Substitute.For<IRoverNavigationService>();
            var missionControl = new RoverMissionControlService(roverRepository, navigationService);

            // Act
            missionControl.Move(1, "R2D2");

            // Assert
            navigationService
                .Received(1)
                .Move(Arg.Any<int>(), Arg.Any<string>());
        }
    }
}