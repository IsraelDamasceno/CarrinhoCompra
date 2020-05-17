using System;
using System.Collections.Generic;

namespace CarrinhoCompra.Domain.Interfaces
{
    public interface IBaseRepository<T>: IDisposable
    {
        T Insert(T value);
        bool Edit(T value);
        bool Pacth(T value);
        bool Delete(T value);
        bool Delete(object id);
        T Find(object id);
        IEnumerable<T> List();
    }
}
