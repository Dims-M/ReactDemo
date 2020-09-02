using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepository.Interfaces
{
    /// <summary>
    /// Интерфейс для RepositoryContextFactory
    /// </summary>
    public interface IRepositoryContextFactory
    {
        RepositoryContext CreateDbContext(string connectionString);

    }
}
