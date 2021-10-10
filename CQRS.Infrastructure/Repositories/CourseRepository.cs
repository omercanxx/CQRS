using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories
{
    public class CourseRepository : CustomRepository<Course>, ICourseRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public CourseRepository(AppDbContext context) : base(context)
        {

        }
        public async Task AddAsync(Course course)
        {
            CheckDuplicate(course);
            await _appDbContext.Courses.AddAsync(course);
        }
        public void Update(Course course)
        {
            CheckDuplicate(course);
            TrySetProperty(course, "ModifiedOn", DateTime.Now);
            _appDbContext.Entry(course).State = EntityState.Modified;
        }

        private void CheckDuplicate(Course course)
        {
            //Aynı isimli kursun sadece fiyatı da değişebileceği için && x.Id != course.Id koşulunu ekledik.
            if (_appDbContext.Courses.Any(x => x.Title == course.Title && x.IsActive && x.Id != course.Id))
                throw new ApplicationException($"{course.Title} isimli kurs mevcut");
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