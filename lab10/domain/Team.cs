namespace lab10.domain;

public class Team : Entity<long>
{
    private string _name;

    public Team(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }
    
    
}