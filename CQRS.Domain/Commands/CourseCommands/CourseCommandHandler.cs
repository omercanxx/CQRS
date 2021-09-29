using CQRS.Core.Entities;
using CQRS.Core.Interfaces;
using CQRS.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.CourseCommands
{
    public class CourseCommandHandler : IRequestHandler<CourseCreateCommand, Guid>,
                                        IRequestHandler<CourseUpdateCommand, Guid>
    {
        private readonly ICourseRepository _courseRepository;
        public CourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Guid> Handle(CourseCreateCommand command, CancellationToken cancellationToken)
        {
            Course course = new Course(command.Title, command.Price);

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();

            return course.Id;
        }

        public async Task<Guid> Handle(CourseUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbCourse = await _courseRepository.GetByIdAsync(command.Id);

            dbCourse.UpdatePrice(command.Price);
            dbCourse.UpdateTitle(command.Title);
            _courseRepository.Update(dbCourse);


            await _courseRepository.SaveChangesAsync();
            return dbCourse.Id;
        }
    }
}
