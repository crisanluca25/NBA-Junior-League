
using lab10.domain;
using lab10.repository;
using lab10.service;

class Program
{
    static void Main(string[] args)
    {
        var teamRepository = new TeamRepository();
        var matchRepository = new MatchRepository(teamRepository);
        var studentRepository = new StudentRepository();
        var playerRepository = new PlayerRepository(studentRepository, teamRepository);
        var activePlayerRepository = new ActivePlayerRepository();
        
        var teamService = new Service<long, Team>(teamRepository);
        var matchService = new Service<long, Match>(matchRepository);
        var studentService = new Service<long, Student>(studentRepository);
        var playerService = new Service<long, Player>(playerRepository);
        var activePlayerService = new Service<long, ActivePlayer>(activePlayerRepository);
        var nbaService = new NBAService(teamService, playerService, activePlayerService, matchService);
        
        var app = new Console(nbaService);
        app.Run();
        
    }
    
    
}