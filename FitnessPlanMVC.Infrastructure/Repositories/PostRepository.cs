using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly FitnessPlanMVCDbContext _context;

        public PostRepository(FitnessPlanMVCDbContext context)
        {
            _context = context;
        }
        public int AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post.Id;
        }
        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
        public IQueryable<Post> GetAllPost()
        {
            var posts = _context.Posts.AsQueryable();
            return posts;
        }
        public Post GetDetail(int id)
        {
            return _context.Posts.FirstOrDefault(e => e.Id == id);
        }
    }
}