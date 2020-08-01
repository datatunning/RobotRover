// <copyright file="RoverModel.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Diagnostics.CodeAnalysis;

namespace RobotRover.App.Models
{
    [ExcludeFromCodeCoverage]
    public class RoverModel
    {
        public RoverModel(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public RoverPositionModel Location { get; set; } = new RoverPositionModel();
    }
}