using Common;
using Common.Helper;
using Common.ViewModel;
using Models.Models.Users;
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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRepository<Student> _Repository;

        public StudentService(IStudentRepository studentRepository, IRepository<Student> Repository)
        {
            _studentRepository = studentRepository;
            _Repository = Repository;
            
        }
        public async Task<APIResponse<List<StudentViewModel>>> GetStudentsByClassId(int classId)
        {
            var claxx = await _Repository.GetAsync(classId);
            if (claxx == null)
            {
                return ResponseHelper<List<StudentViewModel>>.CreateNotFoundErrorResponse(HttpStatusCode.NotFound, new List<string> { Constant.NoClassFound });
            }
            var childrens = await _studentRepository.GetStudentsByClassId(classId);
            if (childrens == null)
            {
                return ResponseHelper<List<StudentViewModel>>.CreateErrorRes(null, new List<string> { Constant.NotFound });
            }
            return ResponseHelper<List<StudentViewModel>>.CreateSuccessRes(childrens, new List<string> { Constant.Fetched });
        }
    }
    }

