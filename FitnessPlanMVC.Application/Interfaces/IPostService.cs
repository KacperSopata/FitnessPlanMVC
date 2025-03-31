using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Application.ViewModels.Post;

namespace FitnessPlanMVC.Application.Interfaces
{
    public interface IPostService
    {
        ListPost GetAllPostForList(int pageSize, int pageNO, string searchString);
        int AddPost(NewPostVm model);
        PostDetailVm GetPostDetail(int id);
        void DeletePost(int id);

    }
}
