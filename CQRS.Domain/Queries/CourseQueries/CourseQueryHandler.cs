using AutoMapper;
using CQRS.Core.Interfaces;
using CQRS.Domain.Dtos.CourseDtos;
using CQRS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.CourseQueries
{
    public class CourseQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>,
                                      IRequestHandler<GetCourseDetailQuery, CourseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseQueryHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<CourseDto> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
        {
            var dbCourse = await _courseRepository.GetByIdAsync(request.Id);
            return _mapper.Map<CourseDto>(dbCourse);
        }

        public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<CourseDto>>(await _courseRepository.GetAllAsync());
        }
    }
}
