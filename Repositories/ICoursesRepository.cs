using Chama.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chama.WebApi.Repositories
{
    public interface ICoursesRepository
    {
        IEnumerable<CourseModel> Get();
        CourseModel GetById(int id);
        void Register(CourseModel model);
        void SignUp(CourseModel course, UserModel student);
        void Process();
        List<ProcessedCourseModel> GetProcessedCourses();
    }
}
