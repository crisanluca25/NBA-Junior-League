using System.Data;
using lab10.domain;
using Npgsql;

namespace lab10.repository;

public class StudentRepository : IRepository<long, Student>
{
    private readonly string _url = "Host=127.0.0.1;Username=postgres;Password=lucacrisan;Database=nbaleague";

    public Student GetById(long id)
    {
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("SELECT * FROM students WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            Student student = new Student(reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("school")))
            {
                _id = reader.GetInt64(reader.GetOrdinal("id"))
            };
            return student;
        }
        return null;
    }

    public IEnumerable<Student> GetAll()
    {
        var students = new List<Student>();
        using var connection = new NpgsqlConnection(_url);
        connection.Open();
        var command = new NpgsqlCommand("SELECT * FROM students", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            students.Add(new Student(reader.GetString("name"), reader.GetString("school"))
            {
                _id = reader.GetInt64("id")
            });
        }
        return students;
    }
    
    
}