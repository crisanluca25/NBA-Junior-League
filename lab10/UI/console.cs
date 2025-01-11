using lab10.domain;
using lab10.service;
    public class Console
    {
        private NBAService _nbaService;

        public Console(NBAService nbaService)
        {
            _nbaService = nbaService;
        }

        public void Run()
        {
            while (true)
            {
                DisplayMenu();
                long choice = GetUserChoice();

                switch (choice)
                {
                    case 0:
                        System.Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                    case 1:
                        HandleGetPlayers();
                        break;
                    case 2:
                        HandleGetActivePlayers();
                        break;
                    case 3:
                        HandleGetGames();
                        break;
                    case 4:
                        HandleGetScore();
                        break;
                    default:
                        System.Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void DisplayMenu()
        {
            System.Console.WriteLine("\nBasketball Management System:");
            System.Console.WriteLine("1. View players of a team");
            System.Console.WriteLine("2. View active players for a team in a game");
            System.Console.WriteLine("3. View games within a date range");
            System.Console.WriteLine("4. View the score of a game");
            System.Console.WriteLine("0. Exit");
            System.Console.Write("Enter your choice: ");
        }

        private long GetUserChoice()
        {
            if (long.TryParse(System.Console.ReadLine(), out long choice))
            {
                return choice;
            }
            System.Console.WriteLine("Invalid input. Please enter a number.");
            return -1;
        }

        private void HandleGetPlayers()
        {
            System.Console.Write("Enter team ID: ");
            if (long.TryParse(System.Console.ReadLine(), out long teamId))
            {
                var players = _nbaService.GetPlayers(teamId);
                System.Console.WriteLine($"Players in team {teamId}:");
                foreach (var player in players)
                {
                    System.Console.WriteLine($"- {player.GetName()}");
                }
            }
            else
            {
                System.Console.WriteLine("Invalid team ID.");
            }
        }

        private void HandleGetActivePlayers()
        {
            System. Console.Write("Enter team ID: ");
            if (long.TryParse(System.Console.ReadLine(), out long teamId))
            {
                System.Console.Write("Enter game ID: ");
                if (long.TryParse(System.Console.ReadLine(), out long gameId))
                {
                    var activePlayers = _nbaService.GetActivePlayers(teamId, gameId);
                    System.Console.WriteLine($"Active players in team {teamId} for game {gameId}:");
                    foreach (var ap in activePlayers)
                    {
                        System.Console.WriteLine($"- {ap.GetPlayerId}, Points: {ap.GetPoints}");
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid game ID.");
                }
            }
            else
            {
                System.Console.WriteLine("Invalid team ID.");
            }
        }

        private void HandleGetGames()
        {
            System.Console.Write("Enter start date (yyyy-MM-dd): ");
            if (DateTime.TryParse(System.Console.ReadLine(), out DateTime startDate))
            {
                System.Console.Write("Enter end date (yyyy-MM-dd): ");
                if (DateTime.TryParse(System.Console.ReadLine(), out DateTime endDate))
                {
                    var games = _nbaService.GetMatches(startDate, endDate);
                    System.Console.WriteLine($"Games between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}:");
                    foreach (var game in games)
                    {
                        System.Console.WriteLine($"- Game ID: {game._id}, Teams: {game.GetHost.GetName()} vs {game.GetGuest.GetName()}, Date: {game.GetDate}");
                    }
                }
                else
                {
                    System.Console.WriteLine("Invalid end date.");
                }
            }
            else
            {
                System.Console.WriteLine("Invalid start date.");
            }
        }

        private void HandleGetScore()
        {
            System.Console.Write("Enter game ID: ");
            if (long.TryParse(System.Console.ReadLine(), out long gameId))
            {
                var scores = _nbaService.GetScore(gameId);
                System.Console.WriteLine($"Scores for game {gameId}:");
                foreach (var score in scores)
                {
                    System.Console.WriteLine($"- Team {score.Key.GetName()}: {score.Value} points");
                }
            }
            else
            {
                System.Console.WriteLine("Invalid game ID.");
            }
        }
    }