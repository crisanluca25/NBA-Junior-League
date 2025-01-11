namespace lab10.domain;

public class Entity<ID>
{
    public ID _id;
    
    public ID GetId() => _id;
    
    public void SetId(ID id) => _id = id;
    
    
}