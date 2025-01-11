namespace lab10.domain;

public class Player : Student
{
    private Team _team;

    public Player(string name, string school, Team team) : base(name, school)
    {
        _team = team;
    }
    
    public Team GetTeam => _team;
    
    public void SetTeam(Team team) => _team = team;
}