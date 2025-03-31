using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Domain.Interfaces
{
    public interface IPostRepository
    {
        IQueryable<Post> GetAllPost();
        int AddPost(Post post);
        Post GetDetail(int id);
        void DeletePost(Post post);
    }
}
