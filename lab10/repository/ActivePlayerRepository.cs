using lab10.domain;
using Npgsql;

namespace lab10.repository;

public class ActivePlayerRepository : IRepository<long, ActivePlayer>
{
    private readonly string _url = "Host=127.0.0.1;Username=postgres;Password=lucacrisan;Database=nbaleague";

    public ActivePlayer GetById(long id)
    {
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("select * from active_players where id = @id", connection);
        command.Parameters.Add(new NpgsqlParameter("@id", id));
        
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            ActivePlayer activePlayer = new ActivePlayer(reader.GetInt64(reader.GetOrdinal("player_id")),
                reader.GetInt64(reader.GetOrdinal("match_id")), reader.GetInt64(reader.GetOrdinal("points")),
                (PlayerType)Enum.Parse(typeof(PlayerType), reader.GetString(reader.GetOrdinal("type"))))
            {
                _id = reader.GetInt64(reader.GetOrdinal("id"))
            };
            return activePlayer;
        }

        return null;
    }

    public IEnumerable<ActivePlayer> GetAll()
    {
        var activePlayers = new List<ActivePlayer>();
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("select * from active_players", connection);
        using var reader = command.ExecuteReader();
        
        while (reader.Read())
        {
            activePlayers.Add(new ActivePlayer(
                reader.GetInt64(reader.GetOrdinal("player_id")),
                reader.GetInt64(reader.GetOrdinal("match_id")),
                reader.GetInt64(reader.GetOrdinal("points")),
                (PlayerType)Enum.Parse(typeof(PlayerType), reader.GetString(reader.GetOrdinal("type"))))
            {
                _id = reader.GetInt64(reader.GetOrdinal("id")),
            });
        }
        return activePlayers;
    }
}