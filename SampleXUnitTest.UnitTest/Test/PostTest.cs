
using Newtonsoft.Json;
using SampleXUnitTest.WebApi;
using SampleXUnitTest.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleXUnitTest.UnitTest.Test
{
    public class PostTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }

        public PostTest(CustomWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient();
        }

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

        [Fact]
        public async Task Post_Posts_ReturnOk()
        {
            // Arrange  
            Post oPost = new Post() { Title = "Test Title ", Content = "test content ", BlogForeignKey = 2 };

            // Act
            var response = await Client.PostAsJsonAsync($"/api/posts/", oPost);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            Post oResponsePost = JsonConvert.DeserializeObject<Post>(stringResponse);

            // Assert
            Assert.NotNull(oResponsePost);
        }
    }
}
