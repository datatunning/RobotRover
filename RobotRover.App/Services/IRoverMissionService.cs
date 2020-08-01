// <copyright file="IRoverMissionService.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Drawing;

namespace RobotRover.App.Services
{
    public interface IRoverMissionControlService
    {
        /// <summary>Gets the plateau' size.</summary>
        /// <value>The plateau size as Point.</value>
        Point Plateau { get; }

        /// <summary>Initializes the plateau.</summary>
        /// <param name="sizeX">The Width.</param>
        /// <param name="sizeY">The Height.</param>
        void InitializePlateau(int sizeX, int sizeY);

        /// <summary>Sets the initial position of a Rover.</summary>
        /// <param name="roverId">The Rover Id</param>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        /// <param name="direction">The direction where the over face</param>
        /// <remarks>
        ///     Deploys the rover to an initial grid location [x y],
        ///     where direction is the initial compass direction.
        ///     For example, [0 0 N] means the rover is situated at the bottom left corner, facing North.
        ///     Assume that the square directly North from(x, y) is (x, y+1).
        /// </remarks>
        void SetPosition(int roverId, int x, int y, string direction);

        /// <summary>Moves the specified commands.</summary>
        /// <param name="roverId">The Rover Id</param>
        /// <param name="commands">The commands to apply to a Rover</param>
        void Move(int roverId, string commands);

        /// <summary>Resets this instance.</summary>
        void Reset();
    }
}