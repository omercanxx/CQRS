using CQRS.Core;
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
    //Projede commandların handle edildiği CommandHandlerlar kullanılmıştır. Injector classı içerisinde implementasyonu gözükmektedir.
    public class CourseCommandHandler : IRequestHandler<CourseCreateCommand, CommandResult>,
                                        IRequestHandler<CourseUpdateCommand, CommandResult>,
                                        IRequestHandler<CourseDeleteCommand, CommandResult>
    {
        private readonly ICourseRepository _courseRepository;
        public CourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<CommandResult> Handle(CourseCreateCommand command, CancellationToken cancellationToken)
        {
            Course course = new Course(command.Title, command.Price);

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();

            //CommandResultun Id ve Title propertysine set edilen constructor ın içerisine girecektir.
            return new CommandResult(course.Id, course.Title);
        }

        public async Task<CommandResult> Handle(CourseUpdateCommand command, CancellationToken cancellationToken)
        {
            var dbCourse = await _courseRepository.GetByIdAsync(command.Id);

            dbCourse.UpdatePrice(command.Price);
            dbCourse.UpdateTitle(command.Title);
            _courseRepository.Update(dbCourse);


            await _courseRepository.SaveChangesAsync();

            return new CommandResult(dbCourse.Id, dbCourse.Title);
        }

        public async Task<CommandResult> Handle(CourseDeleteCommand command, CancellationToken cancellationToken)
        {
            var dbCourse = await _courseRepository.GetByIdAsync(command.Id);

            _courseRepository.Deactivate(dbCourse);

            return new CommandResult(dbCourse.Id, dbCourse.Title);
        }
    }
}
