using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleXUnitTest.WebApi;
using SampleXUnitTest.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleXUnitTest.UnitTest
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(BloggingContext bloggingContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!bloggingContext.Blogs.Any())
                {
                    bloggingContext.Blogs.AddRange(
                        GetBlogs());

                    await bloggingContext.SaveChangesAsync();
                }

                if (!bloggingContext.Posts.Any())
                {
                    bloggingContext.Posts.AddRange(
                        GetPosts());

                    await bloggingContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CatalogContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(bloggingContext, loggerFactory, retryForAvailability);
                }
            }
        }

        private static Post[] GetPosts()
        {
            return new Post[]
              {
                 new Post(){ Title = "Jumpstart for X unit", Content ="Unit make your project perfect.", BlogForeignKey=1}
              };
        }

        private static Blog[] GetBlogs()
        {
            return new Blog[]
            {
                new Blog() { Url = "http://blogs.msdn.com/dotnet" },
                new Blog() { Url = "http://blogs.msdn.com/webdev" },
                new Blog() { Url = "http://blogs.msdn.com/visualstudio" },
            };
        }
    }
}
