using NetPostgreSQL.Models;
using System.Collections.Generic;

namespace NetPostgreSQL.Services
{
    public interface IBloggingRepository
    {
        void AddBlog(Blog blog);
        void DeleteBlog(Blog blog);
        Blog GetBlog(int blogId);
        IEnumerable<Blog> GetBlogs();
        bool Save();
        void UpdateBlog(Blog blog);
    }
}