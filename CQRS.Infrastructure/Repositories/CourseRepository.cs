using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _appDbContext.Entry(course).State = EntityState.Modified;
        }

        private void CheckDuplicate(Course course)
        {
            if (_appDbContext.Courses.Any(x => x.Title == course.Title && x.IsActive))
                throw new ApplicationException($"{course.Title} isimli kurs mevcut");
        }
    }
}