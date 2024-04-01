using Common.DTOs.Teacher;
using Common.Helper;
using Common.ViewModel;
using Repository.IRepositories;
using Repository.Repositories;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public async Task<APIResponse<string>> CreateTeacher(AddTeacherDto addTeacher)
        {
            return await _teacherRepository.AddTeacher(addTeacher);
        }
        public async Task<APIResponse<string>> AddClass(AddClassDto addClassDto)
        {
            var classRes = await _teacherRepository.CreateClass(addClassDto);
            if (classRes == null)
            {
                return ResponseHelper<string>.CreateErrorRes(HttpStatusCode.Conflict, new List<string> { "Class with the same name already exists" });
            }
            return classRes;
        }
        public async Task<APIResponse<string>> AddSubject(AddSubjectDto addSubjectDto)
        {
            var subjectRes = await _teacherRepository.CreateSubject(addSubjectDto);
            if (subjectRes == null)
            {
                return ResponseHelper<string>.CreateErrorRes(HttpStatusCode.Conflict, new List<string> { "Subject with the same name already exists" });
            }
            return subjectRes;
        }
        public async Task<APIResponse<List<TeacherViewModel>>> GetTeachers()
        {
            return await _teacherRepository.GetTeachers();
        }
        public async Task<APIResponse<List<ClassViewModel>>> GetAllClasses()
        {
            return await _teacherRepository.GetAllClasses();
        }

        public async Task<APIResponse<string>> UpdateClass(UpdateClassDto updateClass)
        {
            var stClass = await _teacherRepository.UpdateClass(updateClass);
            if (stClass == null)
            {
                return ResponseHelper<string>.CreateErrorRes("Class with this id doesn't exist", new List<string> { "class with this id doesn't exist" });
            }
            return stClass;
        }

        public async Task<APIResponse<string>> UpdateTeacher(UpdateTeacherDto updateteacher)
        {
            return await _teacherRepository.UpdateTeacher(updateteacher);
        }
    }
}
