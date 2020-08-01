// <copyright file="EnumExtensions.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using RobotRover.App.Models;

namespace RobotRover.App.Extensions
{
    public static class EnumExtensions
    {
        public static DirectionEnum Shift(this DirectionEnum direction, DirectionShiftEnum directionShift)
        {
            var shifted = (int)direction + (int)directionShift;

            if (shifted < 0)
            {
                return (DirectionEnum)(360 + shifted); // i.e 360 + -90 = 240 / W
            }
            
            if (shifted == 360)
            {
                return (DirectionEnum)0; // i.e. 0 == 360 == N
            }
            
            return (DirectionEnum)shifted;
        }
    }
}