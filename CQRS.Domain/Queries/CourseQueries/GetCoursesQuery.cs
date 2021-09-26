using CQRS.Domain.Dtos.CourseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.CourseQueries
{
    public class GetCoursesQuery : IRequest<List<CourseDto>>
    {
    }
}
