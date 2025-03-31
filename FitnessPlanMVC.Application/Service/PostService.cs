using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Post;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Application.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepo;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepo = postRepository;
            _mapper = mapper;
        }

        public int AddPost(NewPostVm model)
        {
            var post = _mapper.Map<Post>(model);

            if (model.ImageContent != null && model.ImageContent.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    model.ImageContent.CopyTo(ms);
                    post.Image = ms.ToArray();
                }
            }

            var id = _postRepo.AddPost(post);
            return id;
        }
        public void DeletePost(int id)
        {
            var post = _postRepo.GetDetail(id);
            if (post != null)
            {
                _postRepo.DeletePost(post);
            }
        }

        public ListPost GetAllPostForList(int pageSize, int pageNo, string searchString)
        {
            var postsQuery = _postRepo.GetAllPost();

            if (!string.IsNullOrEmpty(searchString))
            {
                postsQuery = postsQuery
                    .Where(p => p.Title.Contains(searchString));
            }

            var totalPosts = postsQuery.Count();

            var posts = postsQuery
                .Skip(pageSize * (pageNo - 1))
                .Take(pageSize)
                .ProjectTo<PostForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            var result = new ListPost
            {
                Posts = posts,
                CurrentPage = pageNo,
                PageSize = pageSize,
                Count = totalPosts,
                SearchString = searchString
            };

            return result;
        }

        public PostDetailVm GetPostDetail(int id)
        {
            var post = _postRepo.GetDetail(id);
            var postVm = _mapper.Map<PostDetailVm>(post);
            return postVm;
        }
    }
}
