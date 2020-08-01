// <copyright file="RoverPositionModelShould.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using FluentAssertions;
using RobotRover.App.Models;
using Xunit;

namespace RobotRover.UnitTests.Models
{
    [ExcludeFromCodeCoverage]
    public class RoverPositionModelShould
    {
        [Theory]
        [InlineDataAttribute(5, 94, DirectionEnum.N)]
        [InlineDataAttribute(23, 2, DirectionEnum.E)]
        [InlineDataAttribute(65, 56, DirectionEnum.S)]
        [InlineDataAttribute(8, 17, DirectionEnum.W)]
        [Trait("Category", "UnitTests")]
        public void Set_Properties_From_Constructor(int x, int y, DirectionEnum direction)
        {
            // Arrange

            // Act
            var roverPosition = new RoverPositionModel(x, y, direction);

            // Assert
            roverPosition.Position.Should().BeEquivalentTo(new Point(x,y));
            roverPosition.Direction.Should().Be(direction);
        }

        [Theory]
        [InlineDataAttribute(5, 94, DirectionEnum.N)]
        [InlineDataAttribute(23, 2, DirectionEnum.E)]
        [InlineDataAttribute(65, 56, DirectionEnum.S)]
        [InlineDataAttribute(8, 17, DirectionEnum.W)]
        [Trait("Category", "UnitTests")]
        public void Return_Formatted_String(int x, int y, DirectionEnum direction)
        {
            // Arrange
            var roverPosition = new RoverPositionModel(x, y, direction);

            // Act
            var positionString = roverPosition.ToString();

            // Assert
            positionString.Should().Be($"[{x}, {y}, {direction}]");
        }
    }
}