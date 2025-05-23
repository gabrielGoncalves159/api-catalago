﻿using System.Linq.Expressions;

namespace APICatalago.Interfaces
{
    public interface IRepository<T>
    {
        // Não violar o principio ISP do SOLID
        IEnumerable<T> GetAll();
        T? Get(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
