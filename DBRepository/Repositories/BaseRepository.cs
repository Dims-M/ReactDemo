using DBRepository.Factories;
using DBRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepository.Repositories
{
    /// <summary>
    /// Абстрактный класс  базовым классом для всех созданных нами классов-посредников
    /// </summary>
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; }
        protected RepositoryContextFactory ContextFactory { get; }
       
        public BaseRepository(string connectionString, IRepositoryContextFactory contextFactory)
        {
            ConnectionString = connectionString;
            ContextFactory = (RepositoryContextFactory)contextFactory;
        }
    }

}
