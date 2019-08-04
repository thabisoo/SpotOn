using SpotOn.ApplicationLogic.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotOn.ApplicationLogic.Interfaces
{
    public interface IPostTagService
    {
        Task<bool> AddPostTag(Guid postId, Guid tagId);

        IEnumerable<TagEntity> GetTagsForPost(Guid postId);

        IEnumerable<Guid> GetPostsWithParticularTag(string tag);
    }
}
