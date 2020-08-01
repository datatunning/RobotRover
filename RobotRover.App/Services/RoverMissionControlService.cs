// <copyright file="RoverMissionControlService.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Drawing;

namespace RobotRover.App.Services
{
    public class RoverMissionControlService : IRoverMissionControlService
    {
        private readonly IRoverNavigationService _navigationService;
        private readonly IRoverRepositoryService _roverRepository;

        public RoverMissionControlService(IRoverRepositoryService roverRepository,
            IRoverNavigationService navigationService)
        {
            _roverRepository = roverRepository;
            _navigationService = navigationService;
        }

        /// <inheritdoc />
        public Point Plateau => _navigationService.Plateau;

        /// <inheritdoc />
        public void InitializePlateau(int sizeX, int sizeY)
        {
            _navigationService.InitializePlateau(sizeX, sizeY);
        }

        /// <inheritdoc />
        public void SetPosition(int roverId, int x, int y, string direction)
        {
            _navigationService.SetPosition(roverId, x, y, direction);
        }

        /// <inheritdoc />
        public void Move(int roverId, string commands)
        {
            _navigationService.Move(roverId, commands);
        }

        /// <inheritdoc />
        public void Reset()
        {
            _roverRepository.Reset();
            _navigationService.Reset();
        }
    }
}