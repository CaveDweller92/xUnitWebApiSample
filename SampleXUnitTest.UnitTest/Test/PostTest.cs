
using Newtonsoft.Json;
using SampleXUnitTest.WebApi;
using SampleXUnitTest.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleXUnitTest.UnitTest.Test
{
    public class PostTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public PostTest(CustomWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task Get_Posts_ReturnPost()
        {
            // Arrange  
            int postId = 1;

            // Act
            var response = await Client.GetAsync($"/api/posts/{postId}");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            Post oPost = JsonConvert.DeserializeObject<Post>(stringResponse);

            // Assert
            Assert.Equal(postId, oPost.PostId);
        }
    }
}
