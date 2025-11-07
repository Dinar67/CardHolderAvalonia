using System.Collections.Generic;

namespace CardHolder.Interfaces;

public interface IDatabase
{
    public List<T> Get<T>();
    public void Add<T>(T element);
    public void Edit<T>(T element);
    public void Delete<T>(T element);
    public void SaveChanges();
}