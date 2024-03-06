using Common.DTOs.Teacher;
using Common.Helper;
using Common.ViewModel;
using Models.Models.Users;
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
        Task<APIResponse<string>> CreateSubject(AddSubjectDto classSubject);
        Task<APIResponse<List<TeacherViewModel>>> GetTeachers( );
        Task<APIResponse<List<ClassViewModel>>> GetAllClasses();

    }
}
