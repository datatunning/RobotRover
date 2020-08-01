// <copyright file="IRoverRepositoryService.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using RobotRover.App.Models;

namespace RobotRover.App.Services
{
    public interface IRoverRepositoryService
    {
        /// <summary>Gets the rover count.</summary>
        /// <value>The rover count.</value>
        int RoverCount { get; }

        /// <summary>Adds the rover.</summary>
        /// <returns></returns>
        RoverModel AddRover();

        /// <summary>Gets the rover.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        RoverModel GetRover(int id);

        /// <summary>Resets this instance.</summary>
        void Reset();
    }
}