using lab10.domain;
using Npgsql;

namespace lab10.repository;

public class PlayerRepository : IRepository<long, Player>
{
    private readonly string _url = "Host=127.0.0.1;Username=postgres;Password=lucacrisan;Database=nbaleague";
    
    private StudentRepository _studentRepository;
    private TeamRepository _teamRepository;

    public PlayerRepository(StudentRepository studentRepository, TeamRepository teamRepository)
    {
        _studentRepository = studentRepository;
        _teamRepository = teamRepository;
    }

    public Player GetById(long id)
    {
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("SELECT * FROM players WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            Player player = new Player(_studentRepository.GetById(reader.GetInt64(reader.GetOrdinal("student_id"))).GetName(), _studentRepository.GetById(reader.GetInt64(reader.GetOrdinal("student_id"))).GetSchool(), _teamRepository.GetById(reader.GetInt64(reader.GetOrdinal("team_id"))))
            {
                _id = reader.GetInt64(reader.GetOrdinal("id")),
            };
            return player;
        }

        return null;
    }
    
    public IEnumerable<Player> GetAll()
    {
        var players = new List<Player>();
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("SELECT * FROM players", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            players.Add(new Player(
                _studentRepository.GetById(reader.GetInt64(reader.GetOrdinal("student_id"))).GetName(),
                _studentRepository.GetById(reader.GetInt64(reader.GetOrdinal("student_id"))).GetSchool(),
                _teamRepository.GetById(reader.GetInt64(reader.GetOrdinal("team_id"))))
            {
                _id = reader.GetInt64(reader.GetOrdinal("id")),
            });
        }

        return players;
    }
}