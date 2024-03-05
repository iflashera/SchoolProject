﻿using Common.DTOs.Teacher;
using Common.Helper;
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
    }
}
