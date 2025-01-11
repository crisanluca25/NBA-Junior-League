using lab10.domain;
using lab10.utils;

namespace lab10.service;

public class NBAService
{
    public Service <long, Team> _teamService { get; set; }
    public Service <long, Player> _playerService { get; set; }
    public Service <long, ActivePlayer> _activePlayerService { get; set; }
    public Service <long, Match> _matchService { get; set; }

    public NBAService(Service<long, Team> teamService, Service<long, Player> playerService,
        Service<long, ActivePlayer> activePlayerService, Service<long, Match> matchService)
    {
        _teamService = teamService;
        _playerService = playerService;
        _activePlayerService = activePlayerService;
        _matchService = matchService;
    }

    public IEnumerable<Player> GetPlayers(long teamId)
    {
        return _playerService.Filter(p => p.GetTeam.GetId() == teamId);
    }

    public IEnumerable<ActivePlayer> GetActivePlayers(long teamId, long matchId)
    {
        return _activePlayerService.Filter(ap => ap.GetMatchId == matchId)
            .Where(ap =>
            {
                Player p = _playerService.GetById(ap.GetPlayerId);
                return p.GetTeam.GetId() == teamId;
            });
    }

    public IEnumerable<Match> GetMatches(DateTime start, DateTime end)
    {
        return _matchService.Filter(g => g.GetDate.IsBetween(start, end));
    }

    public Dictionary<Team, long> GetScore(long matchId)
    {
        Dictionary<Team, long> teams = new Dictionary<Team, long>();
        Match match = _matchService.GetById(matchId);
        Team host = _teamService.GetById(match.GetHost.GetId());
        Team guest = _teamService.GetById(match.GetGuest.GetId());
        teams[host] = GetActivePlayers(host.GetId(), matchId).Sum(ap => ap.GetPoints);
        teams[guest] = GetActivePlayers(guest.GetId(), matchId).Sum(ap => ap.GetPoints);
        return teams;
    }
}