// <copyright file="RoverRepository.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.Collections.Generic;
using RobotRover.App.Models;

namespace RobotRover.App.Services
{
    public class RoverRepositoryService : IRoverRepositoryService
    {
        private static int _roverIdCounter;
        private readonly Dictionary<int, RoverModel> _roverCollection = new Dictionary<int, RoverModel>();

        /// <inheritdoc />
        public RoverModel AddRover()
        {
            var rover = new RoverModel(++_roverIdCounter);
            return _roverCollection.TryAdd(rover.Id, rover)
                ? rover
                : null;
        }

        /// <inheritdoc />
        public RoverModel GetRover(int id)
        {
            if (id <= 0 || id > RoverCount)
                throw new ArgumentOutOfRangeException(nameof(id));

            _roverCollection.TryGetValue(id, out var rover);
            return rover;
        }

        /// <inheritdoc />
        public int RoverCount => _roverCollection?.Count ?? 0;

        /// <inheritdoc />
        public void Reset()
        {
            _roverCollection.Clear();
            _roverIdCounter = 0;
        }
    }
}