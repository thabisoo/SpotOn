using AutoMapper;
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
    public class TagService : ITagService
    {
        private readonly IBaseRepository<Tag> _tagRepository;
        private readonly IPostTagService _postTagService;
        private readonly IMapper _mapper;

        public TagService(IBaseRepository<Tag> tagRepository,
            IPostTagService postTagServe,
            IMapper mapper)
        {
            _tagRepository = tagRepository;
            _postTagService = postTagServe;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagEntity>> AddTags(Guid postId, IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                var newTag = new Tag
                {
                    Title = tag.ToUpper(),
                    CreatedAt = DateTimeOffset.Now
                };

                var tagEntity = await AddIfNotExists(newTag, tag);

                if (tagEntity != null)
                {
                    await _postTagService.AddPostTag(postId, tagEntity.Id);
                }
                else
                {
                    var tagId = _tagRepository.Where(t => t.Title == tag).FirstOrDefault().Id;
                    await _postTagService.AddPostTag(postId, tagId);
                }
            }

            return _postTagService.GetTagsForPost(postId);
        }

        private async Task<TagEntity> AddIfNotExists(Tag newTag, string tag)
        {
            var tagExists = _tagRepository.Where(t => t.Title == tag).FirstOrDefault();

            if (tagExists == null)
            {
                _tagRepository.Add(newTag);
                await _tagRepository.SaveAsync();

                return _mapper.Map<TagEntity>(newTag);
            }
            else
            {
                return null;
            }
        }
    }
}
