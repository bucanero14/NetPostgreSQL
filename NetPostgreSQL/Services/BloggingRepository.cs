using NetPostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPostgreSQL.Services
{
    public class BloggingRepository : IBloggingRepository, IDisposable
    {
        private readonly BloggingContext _ctx;
        private bool disposedValue;

        public BloggingRepository(BloggingContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public IEnumerable<Blog> GetBlogs()
        {
            return _ctx.Blogs;
        }

        public Blog GetBlog(int blogId)
        {
            return _ctx.Blogs.Find(blogId);
        }

        public void AddBlog(Blog blog)
        {
            _ctx.Blogs.Add(blog);
        }

        public void UpdateBlog(Blog blog)
        {
            _ctx.Blogs.Update(blog);
        }

        public void DeleteBlog(Blog blog)
        {
            _ctx.Blogs.Remove(blog);
        }

        public bool Save()
        {
            return (_ctx.SaveChanges() >= 0);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BloggingRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
