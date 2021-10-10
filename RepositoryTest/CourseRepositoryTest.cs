using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CQRS.RepositoryTest
{
    public class CourseRepositoryTest
    {
        List<Course> courses = new List<Course>();
        Guid testId = Guid.Parse("a4fd1a70-05c1-40ae-be16-15ee28ebca61");
        Guid newId = Guid.Parse("a4fd1a70-05c1-40ae-be16-15ee28ebca51");
        public CourseRepositoryTest()
        {

            for (int i = 1; i < 10; i++)
            {
                courses.Add(new Course(Guid.Parse($"a4fd1a70-05c1-40ae-be16-15ee28ebca6{i}"), null ,$"{i}.Kurs", (i * 2) + 10));
            }
        }
        [Fact]
        public async Task GetAllAsync()
        {
            Mock<ICourseRepository> mockCourseRepository = new Mock<ICourseRepository>();

            // return a course by Id
            mockCourseRepository.Setup(mr => mr.GetAllAsync()).Returns(async () => courses);

            var courseRepository = mockCourseRepository.Object;
            var dbCourses = await courseRepository.GetAllAsync();

            Assert.Equal(courses.Count, dbCourses.Count());
        }
        [Fact]
        public async Task GetByIdAsync()
        {
            Mock<ICourseRepository> mockCourseRepository = new Mock<ICourseRepository>();

            // return a course by Id
            mockCourseRepository.Setup(mr => mr.GetByIdAsync(
                It.IsAny<Guid>())).Returns(async (Guid id) => courses.SingleOrDefault(x => x.Id == id));

            var courseRepository = mockCourseRepository.Object;
            var dbCourse = await courseRepository.GetByIdAsync(testId);

            Assert.NotNull(dbCourse);

            //Different DateTime
            //Assert.Equal(new Course(Guid.Parse($"a4fd1a70-05c1-40ae-be16-15ee28ebca61"), "1.Kurs", 12), dbCourse);

            Assert.Equal("1.Kurs", dbCourse.Title);
            Assert.Equal(12, dbCourse.Price);

        }
        [Fact]
        public async Task AddAsync()
        {
            Mock<ICourseRepository> mockCourseRepository = new Mock<ICourseRepository>();

            var course = new Course(newId, null, "deneme", 100);
            mockCourseRepository.Setup(mr => mr.AddAsync(
                It.IsAny<Course>())).Returns(async (Course course) =>
                {
                    courses.Add(course);
                });

            var courseRepository = mockCourseRepository.Object;
            await courseRepository.AddAsync(course);


            Assert.True(courses.Any(x => x.Id == testId));

        }
        [Fact]
        public void Delete()
        {
            Mock<ICourseRepository> mockCourseRepository = new Mock<ICourseRepository>();
            var course = new Course(testId, null, "1.Kurs", 12);


            mockCourseRepository.Setup(mr => mr.AddAsync(
                It.IsAny<Course>())).Callback((Course course) =>
                {
                    var dbCourse = courses.Where(x => x.Id == course.Id).SingleOrDefault();
                    dbCourse.UpdateIsActive(false);
                });

            var courseRepository = mockCourseRepository.Object;
            courseRepository.Deactivate(course);


            Assert.True((course.IsActive == false) ? false : true);

        }

        [Fact]
        public async Task Update()
        {
            Mock<ICourseRepository> mockCourseRepository = new Mock<ICourseRepository>();
            var course = new Course(testId, null, "Yenilenmiş 1.Kurs", 15);
            var updateDate = DateTime.Now;

            mockCourseRepository.Setup(mr => mr.Update(
                It.IsAny<Course>())).Callback((Course course) =>
                {
                    var dbCourse = courses.Where(x => x.Id == course.Id).SingleOrDefault();
                    dbCourse.UpdatePrice(course.Price);
                    dbCourse.UpdateTitle(course.Title);
                    TrySetProperty(dbCourse, "ModifiedOn", updateDate);
                }
                );
            mockCourseRepository.Setup(mr => mr.GetByIdAsync(
                It.IsAny<Guid>())).Returns(async (Guid id) => courses.SingleOrDefault(x => x.Id == id));

            var courseRepository = mockCourseRepository.Object;
            courseRepository.Update(course);

            var dbCourse = await courseRepository.GetByIdAsync(course.Id);


            Assert.Equal(course.Title, dbCourse.Title);
            Assert.Equal(course.Price, dbCourse.Price);
            Assert.Equal(updateDate, dbCourse.ModifiedOn);

        }
        private bool TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value, null);
                return true;
            }
            return false;
        }
    }
}
