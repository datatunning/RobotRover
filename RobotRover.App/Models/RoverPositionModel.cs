// <copyright file="RoverPositionModel.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.Drawing;

namespace RobotRover.App.Models
{
    public struct RoverPositionModel
    {
        /// <summary>Initializes a new instance of the <see cref="RoverPositionModel" /> struct.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="direction">The direction.</param>
        public RoverPositionModel(int x, int y, DirectionEnum direction)
        {
            Position = new Point(x,y);
            Direction = direction;
        }

        /// <summary>Gets or sets the position.</summary>
        /// <value>The position.</value>
        public Point Position { get; set; }

        /// <summary>Gets or sets the direction.</summary>
        /// <value>The direction.</value>
        public DirectionEnum Direction { get; set; }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{Position.X}, {Position.Y}, {Direction}]";
        }
    }
}