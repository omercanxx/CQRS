using AutoMapper;
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
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CourseQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CourseDto> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
        {
            var dbCourse = await _context.Courses.FindAsync(request.Id);
            return _mapper.Map<CourseDto>(dbCourse);
        }

        public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<CourseDto>>(await _context.Courses.ToListAsync());
        }
    }
}
