using AutoMapper;
using SpotOn.ApplicationLogic.Entities.Posts;
using SpotOn.ApplicationLogic.Entities.Users;
using SpotOn.ApplicationLogic.Interfaces;
using SpotOn.Domain;
using SpotOn.Domain.Models;
using SpotOn.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotOn.ApplicationLogic.Services
{
    public class PostService : IPostService
    {
        private readonly IBaseRepository<Post> _postRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ITagService _tagService;
        private readonly IPostTagService _postTagService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        public PostService(IBaseRepository<Post> postRepository,
            IBaseRepository<User> userRepository,
            ITagService tagService,
            IPostTagService postTagService,
            IFileUploadService fileUploadService,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _tagService = tagService;
            _postTagService = postTagService;
            _fileUploadService = fileUploadService;
            _mapper = mapper;
        }

        public async Task<PostEntity> CreateAsync(PostEntity postEntity)
        {
            var imagePath =_fileUploadService.UploadFile(postEntity.Image, AppSettingsHelper.GetImageFolder());

            var post = _mapper.Map<Post>(postEntity);
            post.ImagePath = imagePath != null ? $"{AppSettingsHelper.GetImageFolder()}/{imagePath}" 
                                : AppSettingsHelper.GetDefaultImagePath();
            post.CreatedAt = DateTimeOffset.Now;
            post.UpdatedAt = DateTimeOffset.Now;

            _postRepository.Add(post);
            await _postRepository.SaveAsync();

            var tags = await _tagService.AddTags(post.Id, postEntity.Tags);

            var postResult= _mapper.Map<PostEntity>(post);
            postResult.Tags = tags.Select(t => $"#{t.Title}");

            return postResult;
        }

        public PostEntity GetPostAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Post Id is null.");

            var post = _postRepository.Where(p => p.Id == id).FirstOrDefault();

            if (post == null)
                throw new NullReferenceException("Post not found.");
            
            var postEntity = _mapper.Map<PostEntity>(post);
            postEntity.Date = post.CreatedAt;
            postEntity.Author = _mapper.Map<UserEntity>(_userRepository
                .Where(u => u.Id == post.UserId).FirstOrDefault());
            postEntity.Tags =  _postTagService.GetTagsForPost(id).Select(t => $"#{t.Title}") ?? null;

            return postEntity; 
        }

        public IEnumerable<PostEntity> GetAllPostsAsync(int max = 0)
        {
            var posts = _postRepository.All()
                .OrderByDescending(p => p.CreatedAt)
                .Take(max);
               
            return posts.Select(p => _mapper.Map<PostEntity>(p));
        }

        public IEnumerable<PostEntity> GetUserPostsAsync(Guid userId)
        {
            var posts = _postRepository
                .Where(p => p.UserId == userId)
                .OrderByDescending(c => c.CreatedAt);

            return posts.Select(p => _mapper.Map<PostEntity>(p));
        }

        public IEnumerable<PostEntity> GetTagPostsAsync(string tag)
        {
            var postIds = _postTagService.GetPostsWithParticularTag(tag).ToList();

            var posts = _postRepository
               .Where(p => postIds.Contains(p.Id))
               .OrderByDescending(c => c.CreatedAt);

            return posts.Select(p => _mapper.Map<PostEntity>(p));
        }
    }
}
