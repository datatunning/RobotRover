// <copyright file="PointExtensionsShould.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using FluentAssertions;
using RobotRover.App.Extensions;
using RobotRover.App.Models;
using Xunit;

namespace RobotRover.UnitTests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class PointExtensionsShould
    {
        [Theory]
        [InlineData(10, 10, DirectionEnum.N, 2, 10, 12)]
        [InlineData(10, 10, DirectionEnum.E, 2, 12, 10)]
        [InlineData(10, 10, DirectionEnum.W, 2, 8, 10)]
        [InlineData(10, 10, DirectionEnum.S, 2, 10, 8)]
        [Trait("Category", "UnitTests")]
        public void Shift_Position(int x, int y, DirectionEnum direction, int shift, int expectedX, int expectedY)
        {
            // Assume
            var initialLocation = new Point(x, y);

            // Act
            var shiftedLocation = initialLocation.Shift(direction, shift);

            // Assert
            shiftedLocation.Should().Be(new Point(expectedX, expectedY));
        }
    }
}