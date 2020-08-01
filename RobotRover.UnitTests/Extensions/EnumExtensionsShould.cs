// <copyright file="EnumExtensionsShould.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NSubstitute;
using RobotRover.App.Extensions;
using RobotRover.App.Models;
using RobotRover.App.Services;
using Xunit;

namespace RobotRover.UnitTests.Extensions
{
    [ExcludeFromCodeCoverage]
    public class EnumExtensionsShould
    {
        [Theory]
        [InlineDataAttribute(DirectionEnum.N, DirectionShiftEnum.R, DirectionEnum.E)]
        [InlineDataAttribute(DirectionEnum.E, DirectionShiftEnum.R, DirectionEnum.S)]
        [InlineDataAttribute(DirectionEnum.S, DirectionShiftEnum.R, DirectionEnum.W)]
        [InlineDataAttribute(DirectionEnum.W, DirectionShiftEnum.R, DirectionEnum.N)]
        [InlineDataAttribute(DirectionEnum.N, DirectionShiftEnum.L, DirectionEnum.W)]
        [InlineDataAttribute(DirectionEnum.W, DirectionShiftEnum.L, DirectionEnum.S)]
        [InlineDataAttribute(DirectionEnum.S, DirectionShiftEnum.L, DirectionEnum.E)]
        [InlineDataAttribute(DirectionEnum.E, DirectionShiftEnum.L, DirectionEnum.N)]
        [Trait("Category", "UnitTests")]
        public void Shift_Direction(DirectionEnum originalDirection, DirectionShiftEnum shift, DirectionEnum expectedDirection)
        {
            // Assume
            var initialDirection = originalDirection;

            // Act
            var shiftedDirection = initialDirection.Shift(shift);

            // Assert
            shiftedDirection.Should().Be(expectedDirection);
        }
    }
}