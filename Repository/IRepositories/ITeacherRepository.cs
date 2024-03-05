using Common.DTOs.Teacher;
using Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface ITeacherRepository
    {
        Task<APIResponse<string>> AddTeacher(AddTeacherDto addTeacher);
        Task<APIResponse<string>> CreateClass(AddClassDto classTeacher);
    }
}
