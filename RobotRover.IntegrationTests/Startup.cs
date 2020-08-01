// <copyright file="Startup.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RobotRover.App.Services;

namespace RobotRover.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IRoverRepositoryService, RoverRepositoryService>();
            services.TryAddTransient<IRoverNavigationService, RoverNavigationService>();
            services.TryAddSingleton<IRoverMissionControlService, RoverMissionControlService>();
        }
    }
}