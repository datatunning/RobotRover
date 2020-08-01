// <copyright file="RoverMissionShould.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RobotRover.App.Models;
using RobotRover.App.Services;
using Xunit;

namespace RobotRover.IntegrationTests.Services
{
    [ExcludeFromCodeCoverage]
    public class RoverMissionControlServiceShould
    {
        private readonly IServiceScope _serviceScope;

        public RoverMissionControlServiceShould(IServiceProvider provider)
        {
            _serviceScope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }
        
        [Fact]
        [Trait("Category", "IntegrationTests")]
        public void Move_Specified_Rover()
        {
            // Assume
            var roverRepository = _serviceScope.ServiceProvider.GetRequiredService<IRoverRepositoryService>();
            var missionControl = _serviceScope.ServiceProvider.GetRequiredService<IRoverMissionControlService>();

            missionControl.Reset();
            missionControl.InitializePlateau(40, 40);

            roverRepository.AddRover();
            missionControl.SetPosition(1, 10, 10, "N");

            roverRepository.AddRover();
            missionControl.SetPosition(2, 30, 30, "S");

            // Act
            missionControl.Move(1, "R1R3L2L1");

            // Assert
            var roverOne = roverRepository.GetRover(1);
            roverOne.Location.Position.Should().Be(new Point(13, 8));
            roverOne.Location.Direction.Should().Be(DirectionEnum.N);

            var roverTwo = roverRepository.GetRover(2);
            roverTwo.Location.Position.Should().Be(new Point(30, 30));
            roverTwo.Location.Direction.Should().Be(DirectionEnum.S);
        }
    }
}