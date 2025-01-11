using lab10.domain;

namespace lab10.repository;

public interface IRepository<ID, E> where E : Entity<ID>
{
    E GetById(ID id);
    
    IEnumerable<E> GetAll();
}