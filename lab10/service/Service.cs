using lab10.domain;
using lab10.repository;

namespace lab10.service;

public class Service<ID, E> : AbstractService<ID, E> where E : Entity<ID>
{
    private IRepository<ID, E> repository;

    public Service(IRepository<ID, E> repository)
    {
        this.repository = repository;
    }

    public override E GetById(ID id)
    {
        return repository.GetById(id);
    }

    public override IEnumerable<E> GetAll()
    {
        return repository.GetAll();
    }
}