// <copyright file="RoverNavigationService.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.Drawing;
using System.Text.RegularExpressions;
using RobotRover.App.Extensions;
using RobotRover.App.Models;

namespace RobotRover.App.Services
{
    public class RoverNavigationService : IRoverNavigationService
    {
        private readonly IRoverRepositoryService _roverRepository;

        public RoverNavigationService(IRoverRepositoryService roverRepository)
        {
            _roverRepository = roverRepository;
        }

        /// <inheritdoc />
        public Point Plateau { get; private set; }

        /// <inheritdoc />
        public void InitializePlateau(int sizeX, int sizeY)
        {
            Plateau = new Point(sizeX, sizeY);
        }

        /// <inheritdoc />
        public void SetPosition(int roverId, int x, int y, string direction)
        {
            if (x < 0 || x > Plateau.X) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y > Plateau.Y) throw new ArgumentOutOfRangeException(nameof(y));

            Enum.TryParse(typeof(DirectionEnum), direction, true, out var directionObj);
            if (!(directionObj is DirectionEnum directionEnum))
                throw new ArgumentOutOfRangeException(nameof(direction));

            var rover = _roverRepository.GetRover(roverId);
            rover.Location = new RoverPositionModel(x, y, directionEnum);
        }

        /// <inheritdoc />
        public void Move(int roverId, string commands)
        {
            if (string.IsNullOrWhiteSpace(commands)) throw new ArgumentNullException(nameof(commands));

            var rover = _roverRepository.GetRover(roverId);

            // Parse commands & Calculate new position
            var splitArray = Regex.Split(commands.ToUpper(), @"(?<=\D)(?=\d)|(?<=\d)(?=\D)");

            // Ignore empty or odd number of commands, This reduce the amount of processing of obvious malformed command string.
            if (splitArray?.Length <= 0 || splitArray.Length % 2 != 0)
            {
                return;
            }

            // Process moves.
            for (var i = 0; i < splitArray.Length; i++)
            {
                // Get the shifting direction.
                if (!Enum.TryParse(typeof(DirectionShiftEnum), splitArray[i], out var directionShift))
                {
                    continue;
                }

                // move to 2d part of command is any left.
                i++;
                if (i >= splitArray.Length)
                {
                    continue;
                }

                // Get the number of grid cell to move.
                if (!int.TryParse(splitArray[i], out var cells))
                {
                    continue;
                }

                var direction = rover.Location.Direction.Shift((DirectionShiftEnum) directionShift);
                var position = rover.Location.Position.Shift(direction, cells);

                // Stop processing command as soon as new position is out of Plateau bounds.
                if (position.X > Plateau.X || position.X < 0 || position.Y > Plateau.Y || position.Y < 0)
                {
                    return;
                }
                rover.Location = new RoverPositionModel(position.X, position.Y, direction);
            }
        }

        /// <inheritdoc />
        public void Reset()
        {
            Plateau = new Point();
        }
    }
}