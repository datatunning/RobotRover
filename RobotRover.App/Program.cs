// <copyright file="Program.cs" company="Bruno DUVAL.">
// Copyright (c) Bruno DUVAL.</copyright>

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Autofac;
using RobotRover.App.Models;
using RobotRover.App.Services;

namespace RobotRover.App
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        private static IContainer Container { get; set; }

        private static IRoverMissionControlService MissionControl { get; set; }
        private static IRoverRepositoryService RepositoryService { get; set; }

        static void Main(string[] args)
        {
            InitializeContainer();

            ProcessStartupCommands(args);

            ProcessMissionCommands();
        }

        private static void InitializeContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RoverRepositoryService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<RoverNavigationService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<RoverMissionControlService>()
                .AsSelf()
                .SingleInstance();

            Container = builder.Build();

            using var scope = Container.BeginLifetimeScope();

            MissionControl = scope.Resolve<RoverMissionControlService>();
            RepositoryService = scope.Resolve<RoverRepositoryService>();
        }

        private static void ProcessStartupCommands(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Option<Point>(
                    new[] {"--set-plateau", "-p"},
                    () => new Point(40, 30),
                    "The size of the plateau <x>,<y> where values are integer."),

                new Option<(int X, int Y, string Direction)>(
                    new[] {"--set-rover", "-r"},
                    () => (X: 10, Y: 10, Direction: "N"),
                    "The size of the plateau <x>,<y> where values are integer.")
            };

            rootCommand.Handler = CommandHandler.Create(
                (Point p, (int X, int Y, string Direction) r) =>
                {
                    // Add a default Rover.
                    RepositoryService.AddRover();

                    // Set plateau using either default or user input.
                    MissionControl.InitializePlateau(p.X, p.Y);

                    // Set rover position using either default or user input.
                    var (x, y, direction) = r;
                    MissionControl.SetPosition(1, x, y, direction);
                });

            rootCommand.InvokeAsync(args);
        }

        private static void ProcessMissionCommands()
        {
            var showMenu = true;
            while (showMenu) showMenu = HandleMainMenu();
        }

        private static bool HandleMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine($"\t A) Add Rover ({RepositoryService.RoverCount})");
            Console.WriteLine("\t S) Set Rover position");
            Console.WriteLine("\t M) Move Rover");
            Console.WriteLine("\r\n\t X) Exit");

            var option = Console.ReadKey();
            switch (option.Key)
            {
                case ConsoleKey.A:
                    return HandleAddRoverMenu();

                case ConsoleKey.S:
                    return HandleSetRoverPositionMenu();

                case ConsoleKey.M:
                    return HandleMoveRoverMenu();

                case ConsoleKey.X:
                    return false;

                default:
                    return true;
            }
        }

        private static bool HandleAddRoverMenu()
        {
            if (RepositoryService.AddRover() != null) return true;

            Console.Clear();
            Console.WriteLine("Can not add Rover, Go back to main menu.");
            Console.ReadKey();

            return true;
        }

        private static bool HandleSetRoverPositionMenu()
        {
            Console.Clear();
            Console.WriteLine("Enter the rover initial position as <ID,X,Y,DIRECTION>");
            Console.WriteLine($"\t Where ID is a rover identifier between [1-{RepositoryService.RoverCount}]");
            Console.WriteLine($"\t Where X is a number between [0-{MissionControl.Plateau.X}]");
            Console.WriteLine($"\t Where Y is a number between [0-{MissionControl.Plateau.Y}]");
            Console.WriteLine($"\t Where DIRECTION is one of the following values: {string.Join(',', Enum.GetNames(typeof(DirectionEnum)))}");
            Console.WriteLine("\r\nor press return to main menu");

            var roverPositionString = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(roverPositionString))
            {
                var roverPosition = roverPositionString.Split(',');
                var roverId = Convert.ToInt32(roverPosition[0]);
                var roverX = Convert.ToInt32(roverPosition[1]);
                var roverY = Convert.ToInt32(roverPosition[2]);
                var roverDirection = roverPosition[3];

                try
                {
                    Console.Clear();
                    MissionControl.SetPosition(roverId, roverX, roverY, roverDirection);
                    var rover = RepositoryService.GetRover(roverId);
                    Console.WriteLine($"Rover '{roverId}' Position: {rover.Location}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occured while trying to set the rover position ({e.Message})");
                }
                finally
                {
                    Console.WriteLine("\r\nPress any key  to return to main menu");
                    Console.ReadKey();
                }
            }

            return true;
        }

        private static bool HandleMoveRoverMenu()
        {
            Console.Clear();
            Console.WriteLine("Enter the rover move command as <ID,COMMAND>");
            Console.WriteLine($"\t Where ID is a rover identifier between [1-{RepositoryService.RoverCount}]");
            Console.WriteLine($"\t Where COMMAND is a combination of orders L: Left, R: Right and a number (i.e L2R2L4L3R5R2L1)");
            Console.WriteLine("\r\nor press return to main menu");

            var roverPositionString = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(roverPositionString))
            {
                var roverPosition = roverPositionString.Split(',');
                var roverId = Convert.ToInt32(roverPosition[0]);
                var roverCommands = roverPosition[1];

                try
                {
                    Console.Clear();
                    MissionControl.Move(roverId, roverCommands);
                    var rover = RepositoryService.GetRover(roverId);
                    Console.WriteLine($"Rover '{roverId}' Position: {rover.Location}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occured while trying to move the rover ({e.Message})");
                }
                finally
                {
                    Console.WriteLine("\r\nPress any key  to return to main menu");
                    Console.ReadKey();
                }
            }

            return true;
        }
    }
}