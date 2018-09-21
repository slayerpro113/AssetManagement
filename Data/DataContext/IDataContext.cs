using System;

namespace Data.DataContext
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
    }
}