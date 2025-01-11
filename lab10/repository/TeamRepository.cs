using System.Data;
using lab10.domain;
using Npgsql;

namespace lab10.repository;

public class TeamRepository : IRepository<long, Team>
{
    private readonly string _url = "Host=127.0.0.1;Username=postgres;Password=lucacrisan;Database=nbaleague";

    public Team GetById(long id)
    {
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("SELECT * FROM teams WHERE id = @id", connection);
        command.Parameters.AddWithValue("id", id);
        
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            Team team = new Team(reader.GetString("name"))
            {
                _id = reader.GetInt64("id")
            };
            return team;
        }
        return null;
    }

    public IEnumerable<Team> GetAll()
    {
        var teams = new List<Team>();
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("SELECT * FROM team", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            teams.Add(new Team(reader.GetString("name"))
            {
                _id = reader.GetInt64("id")
            });
        }
        return teams;
    }
    
}