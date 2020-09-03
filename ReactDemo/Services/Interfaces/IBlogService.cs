using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDemo.Services.Interfaces
{
	public interface IBlogService
	{
		//Task<Page<PostLiteViewModel>> GetPosts(int pageIndex, string tag);
		Task<Post> GetPost(int postId);
		//Task AddComment(AddCommentRequest request);
		//Task AddPost(AddPostRequest request);
		Task DeletePost(int postId);
		Task DeleteComment(int commentId);
		Task<List<string>> GetTags();
	}
}
