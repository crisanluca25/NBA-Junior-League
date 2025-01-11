namespace lab10.domain;

public class Student : Entity<long>
{
    private string _name;
    private string _school;

    public Student(string name, string school)
    {
        _name = name;
        _school = school;
    }
    
    public string GetName() => _name;
    
    public string GetSchool() => _school;
    
    public void SetName(string name) => _name = name;
    
    public void SetSchool(string school) => _school = school;
    
    
}