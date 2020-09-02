using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBRepository.Factories
{
	/// <summary>
	///в фабрике мы конфигурируем dbcontext для работы с SQL Server и передаем строку подключения к бд
	/// </summary>
	public class RepositoryContextFactory : IRepositoryContextFactory
	{
		/// <summary>
		/// ППодключение и работа с Бд
		/// </summary>
		/// <param name="connectionString">Строка подключения к Бд</param>
		/// <returns></returns>
		public RepositoryContext CreateDbContext(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
			optionsBuilder.UseSqlServer(connectionString); // подключение к SqlServer

			return new RepositoryContext(optionsBuilder.Options);
		}

	}
}
