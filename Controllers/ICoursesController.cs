using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chama.WebApi.DTOs;
using Chama.WebApi.Models;

namespace Chama.WebApi.Controllers
{
    public interface ICoursesController
    {
        IActionResult SignUp([FromBody] SignUpDTO chamaDTO);
        Task<IActionResult> SignUpAsync([FromBody] SignUpDTO chamaDTO);
        void Process();
        List<ProcessedCourseModel> GetProcessedCourses();
    }
}