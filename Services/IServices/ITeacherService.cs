using Common.DTOs.Teacher;
using Common.Helper;
using Common.ViewModel;
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
        Task<APIResponse<string>> AddSubject(AddSubjectDto addSubject);
        Task<APIResponse<List<TeacherViewModel>>> GetTeachers();
        Task<APIResponse<List<ClassViewModel>>> GetAllClasses();
        Task<APIResponse<string>> UpdateClass(UpdateClassDto updateClass);
        Task<APIResponse<string>> UpdateTeacher(UpdateTeacherDto updateteacher);

    }
}
