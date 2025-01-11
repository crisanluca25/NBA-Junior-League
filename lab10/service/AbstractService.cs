using lab10.domain;

namespace lab10.service;

public abstract class AbstractService<ID, E> where E : Entity<ID>
{
    public abstract E GetById(ID id);
    
    public abstract IEnumerable<E> GetAll();

    public delegate bool FilterPredicate(E e);

    public IEnumerable<E> Filter(FilterPredicate predicate)
    {
        return GetAll().Where(e => predicate(e));
    }

}