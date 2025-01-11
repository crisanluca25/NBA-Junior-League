namespace lab10.domain;

public class Match : Entity<long>
{
    private Team _host;
    private Team _guest;
    private DateTime _date;

    public Match(Team host, Team guest, DateTime date)
    {
        _host = host;
        _guest = guest;
        _date = date;
    }
    
    public Team GetHost => _host;
    
    public Team GetGuest => _guest;
    
    public DateTime GetDate => _date;
    
    public void SetHost(Team host) => _host = host;
    
    public void SetGuest(Team guest) => _guest = guest;
    
    public void SetDate(DateTime date) => _date = date;
}