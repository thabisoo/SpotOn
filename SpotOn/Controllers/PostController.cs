using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SpotOn.ApplicationLogic.Entities.Posts;
using SpotOn.ApplicationLogic.Interfaces;
using SpotOn.ApplicationLogic.ViewModels.Posts;
using SpotOn.Shared.Helpers;

namespace SpotOn.Controllers
{
    [Route("posts")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService,
            IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(PostViewModel model)
        {
            if (UserLoggedIn() != null) return UserLoggedIn();

            var max = int.Parse(AppSettingsHelper.GetMaxPost());
            var posts = _postService.GetAllPostsAsync(max + model.PostsInPage);

            var postViewModel = new PostViewModel
            {
                Posts = posts.Select(p => _mapper.Map<PostViewModel>(p))
            };

            return View(postViewModel);
        }



        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel model)
        {
            if (UserLoggedIn() != null) return UserLoggedIn();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Post", model);
            }

            if(model.TagsList != null)
                model.Tags = model.TagsList.Split(',');

            var postEntity = _mapper.Map<PostEntity>(model);
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            postEntity.UserId = Guid.Parse(id);

            var result = await _postService.CreateAsync(postEntity);

            var postViewModel = _mapper.Map<PostViewModel>(result);

            return RedirectToAction("Index", "Post");
        }

        [HttpGet("{id}")]
        public IActionResult SinglePost(Guid id)
        {
            if (UserLoggedIn() != null) return UserLoggedIn();

            var result = _postService.GetPostAsync(id);
            var postViewModel = _mapper.Map<PostViewModel>(result);

            return View(postViewModel);
        }

        [HttpGet("user/{id}")]
        public IActionResult UserPosts(Guid id)
        {
            if (UserLoggedIn() != null) return UserLoggedIn();

            var result = _postService.GetUserPostsAsync(id);
            var postViewModel = result.Select(p => _mapper.Map<PostViewModel>(p));

            return View(postViewModel);
        }

        [HttpGet("tag/{id}")]
        public IActionResult TagPosts(string id)
        {
            if (UserLoggedIn() != null) return UserLoggedIn();

            var result = _postService.GetTagPostsAsync(id);
            var postViewModel = result.Select(p => _mapper.Map<PostViewModel>(p));

            return View(postViewModel);
        }

        private IActionResult UserLoggedIn()
        {
            var loggedInUser = HttpContext.User.Claims;

            if (!loggedInUser.Any())
                return RedirectToAction("LogIn", "Auth");

            return null;
        }
    }
}