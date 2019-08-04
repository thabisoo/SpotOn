using SpotOn.ApplicationLogic.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotOn.ApplicationLogic.Interfaces
{
    public interface IPostService
    {
        Task<PostEntity> CreateAsync(PostEntity postEntity);

        PostEntity GetPostAsync(Guid postId);

        IEnumerable<PostEntity> GetAllPostsAsync(int max);

        IEnumerable<PostEntity> GetUserPostsAsync(Guid userId);

        IEnumerable<PostEntity> GetTagPostsAsync(string tag);
    }
}
