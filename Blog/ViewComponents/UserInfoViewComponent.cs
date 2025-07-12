using Blog.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        private readonly BlogContext _context;

        public UserInfoViewComponent(BlogContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string isAuthUser)
        {
            int authUserId = Convert.ToInt32(isAuthUser.Split("|")[0]);
            var authUser = _context.TblUsers.FirstOrDefault(x => x.TblUserId == authUserId);

            return View(authUser);
        }
    }

}
