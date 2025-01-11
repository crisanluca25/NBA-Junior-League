using lab10.domain;
using Npgsql;

namespace lab10.repository;

public class MatchRepository : IRepository<long, Match>
{
    private readonly string _url = "Host=127.0.0.1;Username=postgres;Password=lucacrisan;Database=nbaleague";
    private TeamRepository _teamRepository;

    public MatchRepository(TeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public Match GetById(long id)
    {
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("SELECT * FROM matches WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            Match match = new Match(_teamRepository.GetById(reader.GetInt64(reader.GetOrdinal("host_id"))), _teamRepository.GetById(reader.GetInt64(reader.GetOrdinal("guest_id"))), reader.GetDateTime(reader.GetOrdinal("date")))
            {
                _id = reader.GetInt64(reader.GetOrdinal("id")),
            };
            return match;
        }

        return null;
    }

    public IEnumerable<Match> GetAll()
    {
        var matches = new List<Match>();
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("SELECT * FROM matches", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            matches.Add(new Match(
                _teamRepository.GetById(reader.GetInt64(reader.GetOrdinal("host_id"))),
                _teamRepository.GetById(reader.GetInt64(reader.GetOrdinal("guest_id"))),
                reader.GetDateTime(reader.GetOrdinal("date")))
            {
                _id = reader.GetInt64(reader.GetOrdinal("id")),
            });
        }

        return matches;
    }
}