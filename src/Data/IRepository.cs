using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRepository<T>
{
    Task<IEnumerable<T>> Search(string s, int l, int p);
    Task<T> Find(long id);
    Task<T> Create(T t);
    Task<T> Update(T t);
    void Delete(long id);
}