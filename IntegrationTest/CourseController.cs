using CQRS.Application.Requests.CourseRequests;
using CQRS.Domain.Dtos.CourseDtos;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class CourseController : IClassFixture<ApiFactory>
    {
        private readonly ApiFactory _factory;
        public CourseController(ApiFactory factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task Post_Should_Return_Fail_With_Error_Response_When_Insert_Price_Is_Empty()
        {
            var courseRequest = new CourseCreateRequest { Title = "deneme" };

            var json = JsonSerializer.Serialize(courseRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();

            var response = await client.PostAsync("https://localhost:44341/api/Course", content);
            var actualStatusCode = response.StatusCode;

            Assert.Equal(HttpStatusCode.BadRequest, actualStatusCode);
        }
        [Fact]
        public async Task Post_Should_Return_Fail_With_Error_Response_When_Insert_Title_Has_Already()
        {
            var courseRequest = new CourseCreateRequest { Title = "1. ürün", Price = 12 };
            var contentSameTitle = new StringContent(JsonSerializer.Serialize(courseRequest), Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();


            var exception = await Assert.ThrowsAsync<ApplicationException>(async () => await client.PostAsync("https://localhost:44341/api/Course", contentSameTitle));

            Assert.Equal($"{courseRequest.Title} isimli kurs mevcut", exception.Message);
        }
        [Fact]
        public async Task Post_Should_Return_Success()
        {
            var courseRequest = new CourseCreateRequest { Title = "deneme", Price = 10 };

            var json = JsonSerializer.Serialize(courseRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();


            var response = await client.PostAsync("api/Course", content);
            var actualStatusCode = response.StatusCode;
            //CommandResult değeri olarak aldığım name değerini karşılaştıyorum
            var resultCreateDataName = await response.Content.ReadAsStringAsync();


            Assert.Equal(HttpStatusCode.OK, actualStatusCode);
            Assert.Equal(courseRequest.Title, resultCreateDataName);


            var responseGetAll = await client.GetAsync("api/Course");
            responseGetAll.EnsureSuccessStatusCode();

            var courseListData = await responseGetAll.Content.ReadAsStringAsync();
            var courseList = JsonSerializer.Deserialize<List<CourseDto>>(courseListData);

            Assert.NotEmpty(courseList);
            Assert.NotNull(courseList);

            //Yeni eklenen kurs en sona gelsin diye created on alanına göre sıralanıyor
            var dbLatestCourse = courseList.OrderByDescending(x => x.CreatedOn).First();
            Assert.Equal(dbLatestCourse.Title, courseRequest.Title);
            Assert.Equal(dbLatestCourse.Price, courseRequest.Price);
        }
    }
}
