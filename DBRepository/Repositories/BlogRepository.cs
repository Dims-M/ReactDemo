using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Repositories
{
	public class BlogRepository : BaseRepository, IBlogRepository
	{
		/// <summary>
		/// строка подключения к Бд
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="contextFactory"></param>
		public BlogRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }

		/// <summary>
		/// Получаем все Посты из Бд
		/// </summary>
		/// <param name="index"></param>
		/// <param name="pageSize"></param>
		/// <param name="tag"></param>
		/// <returns></returns>
		public async Task<Page<Post>> GetPosts(int index, int pageSize, string tag = null)
		{

			//LINQ методы выполняются lazy, и поэтому само обращение к базе будет только после вызова метода ToListAsync  (и CountAsync)
			//модель дто
			var result = new Page<Post>() { CurrentPage = index, PageSize = pageSize };

			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				var query = context.Posts.AsQueryable();
				if (!string.IsNullOrWhiteSpace(tag)) // собираем тэги
				{
					query = query.Where(p => p.Tags.Any(t => t.TagName == tag));
				}

				result.TotalPages = await query.CountAsync(); // получаем количество элементов
				// приводим к листу сортируем и получаем все значения по тэгу (с помощью метода Include)
				result.Records = await query.Include(p => p.Tags).Include(p => p.Comments).OrderByDescending(p => p.CreatedDate).Skip(index * pageSize).Take(pageSize).ToListAsync();
			}

			return result; // заполненный обьект
		}

		/// <summary>
		/// получаем все тэги
		/// </summary>
		/// <returns></returns>
		public async Task<List<string>> GetAllTagNames()
		{
			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				return await context.Tags.Select(t => t.TagName).Distinct().ToListAsync();
			}
		}

		/// <summary>
		/// Получаем нужный пост по Id
		/// </summary>
		/// <param name="postId"></param>
		/// <returns></returns>
		public async Task<Post> GetPost(int postId)
		{
			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				return await context.Posts.Include(p => p.Tags).Include(p => p.Comments).FirstOrDefaultAsync(p => p.PostId == postId);
			}
		}

		//добавить комент 
		public async Task AddComment(Comment comment)
		{
			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				context.Comments.Add(comment);
				await context.SaveChangesAsync(); // сохранение в Бд
			}
		}

		/// <summary>
		/// Добавть пост
		/// </summary>
		/// <param name="post"></param>
		/// <returns></returns>
		public async Task AddPost(Post post)
		{
			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				context.Posts.Add(post); 
				await context.SaveChangesAsync(); //сохранение в БД
			}
		}

		/// <summary>
		/// Удаление поста
		/// </summary>
		/// <param name="postId"></param>
		/// <returns></returns>
		public async Task DeletePost(int postId)
		{
			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				var post = new Post() { PostId = postId };
				context.Posts.Remove(post); // удаление из Бд
				await context.SaveChangesAsync();
			}
		}

		/// <summary>
		/// Удаление комента по id 
		/// </summary>
		/// <param name="commentId"></param>
		/// <returns></returns>
		public async Task DeleteComment(int commentId)
		{
			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				var coomment = new Comment() { CommentId = commentId };
				context.Comments.Remove(coomment);
				await context.SaveChangesAsync();
			}
		}
	}
}
