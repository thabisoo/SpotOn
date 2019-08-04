using SpotOn.ApplicationLogic.Entities.Tags;
using SpotOn.ApplicationLogic.Interfaces;
using SpotOn.Domain;
using SpotOn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotOn.ApplicationLogic.Services
{
    public class PostTagService : IPostTagService
    {
        private readonly IBaseRepository<PostTag> _postTagRepository;
        private readonly IBaseRepository<Tag> _tagRepository;

        public PostTagService(IBaseRepository<PostTag> postTagRepository,
            IBaseRepository<Tag> tagRepository)
        {
            _postTagRepository = postTagRepository;
            _tagRepository = tagRepository;
        }

        public async Task<bool> AddPostTag(Guid postId, Guid tagId)
        {
            var postTag = new PostTag
            {
                PostId = postId,
                TagId = tagId,
                CreatedAt = DateTimeOffset.Now
            };

            _postTagRepository.Add(postTag);
            await _postTagRepository.SaveAsync();

            return true;
        }

        public IEnumerable<TagEntity> GetTagsForPost(Guid postId)
        {
            var tagsForPost = _postTagRepository.Where(p => p.PostId == postId);

            var tagEntities = tagsForPost.Select(t => new TagEntity
            {
                Id = t.TagId,
                Title = _tagRepository.Where(x => x.Id == t.TagId).FirstOrDefault().Title.ToUpper()
            });

            return tagEntities;
        }

        public IEnumerable<Guid> GetPostsWithParticularTag(string tag)
        {
           var tagId = _tagRepository.Where(t => t.Title == tag).FirstOrDefault().Id;

           return _postTagRepository.Where(p => p.TagId == tagId).Select(x => x.PostId);
        } 
    }
}
