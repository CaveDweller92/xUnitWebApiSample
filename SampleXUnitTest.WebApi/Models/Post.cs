using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleXUnitTest.WebApi.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogForeignKey { get; set; }

        [ForeignKey("BlogForeignKey")]
        public Blog Blog { get; set; }
    }
}
