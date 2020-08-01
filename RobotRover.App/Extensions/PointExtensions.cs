// <copyright file="PointExtensions.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Drawing;
using RobotRover.App.Models;

namespace RobotRover.App.Extensions
{
    public static class PointExtensions
    {
        public static Point Shift(this Point position, DirectionEnum direction, int shift)
        {
            switch (direction)
            {
                case DirectionEnum.E:
                    return new Point(position.X + shift, position.Y);

                case DirectionEnum.S:
                    return new Point(position.X, position.Y - shift);

                case DirectionEnum.W:
                    return new Point(position.X - shift, position.Y);

                // case DirectionEnum.N:
                default:
                    return new Point(position.X, position.Y + shift);
            }
        }
    }
}