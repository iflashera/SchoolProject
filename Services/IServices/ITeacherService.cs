using Common.DTOs.Teacher;
using Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ITeacherService
    {
        Task<APIResponse<string>> CreateTeacher(AddTeacherDto addTeacher);
        Task<APIResponse<string>> AddClass(AddClassDto addClass);
    }
}
