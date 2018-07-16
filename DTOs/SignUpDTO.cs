using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chama.WebApi.DTOs
{
    public class SignUpDTO
    {
        public int CourseId { get; set; }
        public string StudentName { get; set; }
        public int StudentAge { get; set; }
    }
}
