using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotOn.ApplicationLogic.Entities.Tags;
using SpotOn.ApplicationLogic.Interfaces;
using SpotOn.ApplicationLogic.Services;
using SpotOn.Domain;
using SpotOn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpotOn.UnitTests
{
    [TestClass]
    public class PostTagServiceTests
    {
        private PostTagService _postTagService;
        private Mock<IBaseRepository<PostTag>> _postTagRepositoryMock;
        private Mock<IBaseRepository<Tag>> _tagRepositoryMock;

        private Guid _tagId = Guid.NewGuid();
        private Guid _postId = Guid.NewGuid();
        DateTimeOffset _createdAt = DateTimeOffset.Now;

        private Guid _tagId2 = Guid.NewGuid();
        private Guid _postId2 = Guid.NewGuid();
        DateTimeOffset _createdAt2 = DateTimeOffset.Now;

        private string _tag = "YOLO";
        private string _tag2 = "AZURE";

        [TestInitialize]
        public void SetUp()
        {

            var mockPostTagList = new List<PostTag>
            {
                new PostTag
                {
                    PostId = _postId,
                    TagId = _tagId,
                    CreatedAt = _createdAt
                },
                new PostTag
                {
                    PostId = _postId2,
                    TagId = _tagId2,
                    CreatedAt = _createdAt2
                }
            };

            var mockTagList = new List<Tag>
            {
                new Tag
                {
                    Id = _tagId,
                    Title = _tag,
                    CreatedAt = _createdAt
                },
                new Tag
                {
                    Id = _tagId2,
                    Title = _tag2,
                    CreatedAt = _createdAt
                }
            };

            _postTagRepositoryMock = new Mock<IBaseRepository<PostTag>>();
            _tagRepositoryMock = new Mock<IBaseRepository<Tag>>();

            _postTagRepositoryMock.Setup(repo => repo.All())
                .Returns(mockPostTagList);

            _postTagRepositoryMock.Setup(repo => repo.Where(It.IsAny<Expression<Func<PostTag, bool>>>()))
                .Returns((Expression<Func<PostTag, bool>> expression) => mockPostTagList.AsQueryable().Where(expression));

            _tagRepositoryMock.Setup(repo => repo.All())
                .Returns(mockTagList);

            _tagRepositoryMock.Setup(repo => repo.Where(It.IsAny<Expression<Func<Tag, bool>>>()))
                .Returns((Expression<Func<Tag, bool>> expression) => mockTagList.AsQueryable().Where(expression));

            _postTagService = new PostTagService(_postTagRepositoryMock.Object,
                _tagRepositoryMock.Object);

        }

        [TestMethod]
        public async Task AddPostTag_AssociatesTagWithPost_ReturnsTrue()
        {
            var result = await _postTagService.AddPostTag(_postId, _tagId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetTagsForPost_PostIdNotNull_ReturnsAllTagsForAPost()
        {
            var result = _postTagService.GetTagsForPost(_postId);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.FirstOrDefault().Id, _tagId);
        }

        [TestMethod]
        public void GetPostsWithParticularTag_TagNotNull_ReturnsAListOfPostIds()
        {
            var result = _postTagService.GetPostsWithParticularTag(_tag);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.FirstOrDefault(),_postId);
        }
    }
}
