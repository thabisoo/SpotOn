using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotOn.ApplicationLogic.Entities.Posts;
using SpotOn.ApplicationLogic.Interfaces;
using SpotOn.ApplicationLogic.Mappings.AutoMapper;
using SpotOn.ApplicationLogic.Services;
using SpotOn.Domain;
using SpotOn.Domain.Models;
using SpotOn.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpotOn.UnitTests
{
    [TestClass]
    public class PostServiceTests
    {
        private PostService _postService;
        private Mock<IBaseRepository<Post>> _postRepositoryMock;
        private Mock<IBaseRepository<User>> _userRepositoryMock;
        private Mock<ITagService> _tagService;
        private Mock<IPostTagService> _postTagService;
        private Mock<IFileUploadService> _fileUploadService;
        private IMapper _mapper;

        private PostEntity _postEntity;

        Guid _postId = Guid.NewGuid();
        string _title = "First Post";
        string _body = "this is a body";
        Guid _userId = Guid.NewGuid();
        string[] _tags = new string[] {"Yolo", "that part"};
        DateTimeOffset _date = DateTimeOffset.Now;

        [TestInitialize]
        public void SetUp()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@"TestContent\Configurations\appsettings.json", optional: false, reloadOnChange: true);

            AppSettingsHelper.Configuration = builder.Build();

            _postEntity = new PostEntity
            {
                Id = _postId,
                Title = _title,
                Body = _body,
                UserId = _userId,
                Tags = _tags,
                Date = _date,
            };

            var mockPostList = new List<Post>
            {
                new Post
                {
                    Id = _postId,
                    Title = _title,
                    Body = _body,
                    UserId = _userId
                },
                new Post
                {
                    Id = _postId,
                    Title = _title,
                    Body = _body,
                    UserId = _userId
                }
            };

            _tagService = new Mock<ITagService>();
            _postTagService = new Mock<IPostTagService>();
            _postRepositoryMock = new Mock<IBaseRepository<Post>>();
            _userRepositoryMock = new Mock<IBaseRepository<User>>();
            _fileUploadService = new Mock<IFileUploadService>();

            _postRepositoryMock.Setup(repo => repo.All())
                .Returns(mockPostList);

            _postRepositoryMock.Setup(repo => repo.Where(It.IsAny<Expression<Func<Post, bool>>>()))
                .Returns((Expression<Func<Post, bool>> expression) => mockPostList.AsQueryable().Where(expression));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new EntityToViewModelProfile());
            });
            _mapper = config.CreateMapper();

            _postService = new PostService(_postRepositoryMock.Object, 
                _userRepositoryMock.Object,
                _tagService.Object, 
                _postTagService.Object, 
                _fileUploadService.Object,
                _mapper);
        }

        [TestMethod]
        public async Task CreateAsync_WhenCalled_CreatesPostAndReturnsPostEntity()
        {
            var result = await _postService.CreateAsync(_postEntity);
            Assert.IsNotNull(result);
            Assert.AreEqual(_postId, result.Id);
            Assert.AreEqual(_postEntity.Title, result.Title);
            Assert.AreEqual(_postEntity.UserId, result.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Post Id is null.")]
        public void GetPostAsync_PostIdNull_ThrowsArgumentNullException()
        {
            var result = _postService.GetPostAsync(default(Guid));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Post not found.")]
        public void GetPostAsync_PostIsNull_ThrowsNullReferenceException()
        {
            var result = _postService.GetPostAsync(Guid.NewGuid());
        }

        [TestMethod]
        public void GetPostAsync_PostNotNull_ReturnsPost()
        {
            var result = _postService.GetPostAsync(_postId);
            Assert.IsNotNull(result);
            Assert.AreEqual(_postId, result.Id);
            Assert.AreEqual(_userId, result.UserId);
        }
    }
}
