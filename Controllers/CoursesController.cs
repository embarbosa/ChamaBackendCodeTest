using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chama.WebApi.DTOs;
using Chama.WebApi.Repositories;
using Chama.WebApi.ServiceBus;
using Chama.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Azure.ServiceBus;
using Chama.WebApi.Models;

namespace Chama.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller, ICoursesController
    {
        private Object addLock = new Object();

        private readonly ICoursesRepository _coursesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IChamaQueueSender<SignUpDTO> _chamaQueueSender;

        public CoursesController(ICoursesRepository coursesRepository, IUsersRepository usersRepository, IChamaQueueSender<SignUpDTO> chamaQueueSender)
        {
            _coursesRepository = coursesRepository;
            _usersRepository = usersRepository;
            _chamaQueueSender = chamaQueueSender ?? new ChamaQueueSender<SignUpDTO>();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("signUp")]
        public IActionResult SignUp([FromBody]SignUpDTO signUpDTO)
        {
            var result = Json(new RequestResult
            {
                State = RequestState.Failed,
                Msg = "Ops, something wrong! Please check the information and try again!"
            });

            if (signUpDTO.CourseId > 0 && !string.IsNullOrEmpty(signUpDTO.StudentName) && signUpDTO.StudentAge > 0)
            {
                lock (addLock)
                {
                    var course = _coursesRepository.GetById(signUpDTO.CourseId);
                    var student = _usersRepository.RegisterOrUpdate(signUpDTO.StudentName, signUpDTO.StudentAge);

                    if (course != null && student != null)
                    {
                        if (course != null && course.Students.Any(c => c.Name == signUpDTO.StudentName))
                        {
                            result = Json(new RequestResult
                            {
                                State = RequestState.Failed,
                                Msg = "Student already enrolled in this course"
                            });
                        }
                        if (course.Students == null || course.Students.Count < course.MaxStudents)
                        {
                            _coursesRepository.SignUp(course, student);


                            result = Json(new RequestResult
                            {
                                State = RequestState.Success,
                                Msg = "Student successfully enrolled"
                            });
                        }
                        else
                        {
                            result = Json(new RequestResult
                            {
                                State = RequestState.Failed,
                                Msg = "Sorry, the Course is full"
                            });
                        }
                    }
                }
            }
            return result;

        }

        [HttpPost]
        [Route("signupasync")]
        public async Task<IActionResult> SignUpAsync([FromBody]SignUpDTO signUpDTO)
        {
            var result = Json(new RequestResult
            {
                State = RequestState.Failed,
                Msg = "Ops, something wrong! The enrolling process was not started, please check the information and try again!"
            });

            if (signUpDTO.CourseId > 0 && !string.IsNullOrEmpty(signUpDTO.StudentName) && signUpDTO.StudentAge > 0)
            {
                await _chamaQueueSender.SendAsync(signUpDTO);

                result = Json(new RequestResult
                {
                    State = RequestState.Success,
                    Msg = "Enrolling process was started successfully. Please check your e-mail within a few minutes."
                });
            }
            return result;
        }

        [HttpPost]
        [Route("Process")]
        public void Process()
        {
            _coursesRepository.Process();
        }

        [HttpGet]
        [Route("GetProcessedCourses")]
        public List<ProcessedCourseModel> GetProcessedCourses()
        {
            return _coursesRepository.GetProcessedCourses();
        }
    }
}
