using CQRS.Core.Entities;
using CQRS.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.CourseCommand
{
    public class CourseCommandHandler : IRequestHandler<CourseCreateCommand, Guid>,
                                        IRequestHandler<CourseUpdateCommand, Guid>
    {
        private readonly AppDbContext _context;
        public CourseCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CourseCreateCommand command, CancellationToken cancellationToken)
        {
            Course course = new Course(command.Title, command.Price);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course.Id;
        }

        public async Task<Guid> Handle(CourseUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbCourse = await _context.Courses.FindAsync(command.Id);

            dbCourse.UpdatePrice(command.Price);
            dbCourse.UpdateTitle(command.Title);
            _context.Courses.Update(dbCourse);


            await _context.SaveChangesAsync();
            return dbCourse.Id;
        }
    }
}
