using Common.CurrentUser;
using Common.CustomException;
using Common.DTOs.Teacher;
using Common.Helper;
using Common.ViewModel;
using DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Models.Models.Classes;
using Models.Models.Identity;
using Models.Models.Users;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SmsDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Subject> _subjectRepository;
        private readonly ICurrentUser _currentUser;


        public TeacherRepository(SmsDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
        ICurrentUser currentUser, IRepository<Class> classRepository, IRepository<Subject> subjectRepository)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUser = currentUser;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
        }
        public async Task<APIResponse<string>> AddTeacher(AddTeacherDto teacherDto)
        {
            if (_userManager.FindByNameAsync(teacherDto.Email).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = teacherDto.Email;
                user.Email = teacherDto.Email;
                user.FirsName = teacherDto.FirstName;
                user.LastName = teacherDto.LastName;

                IdentityResult result = _userManager.CreateAsync(user, "Password@1").Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Teacher").Wait();

                    var userM = _userManager.FindByEmailAsync(teacherDto.Email).Result;
                    var userEntry = new UserDetail
                    {
                        ApplicationUserId = Convert.ToString(userM.Id),
                        RoleId = _dbContext.Roles.FirstOrDefault(r => r.ApplicationRoleId == _roleManager.FindByNameAsync("Teacher").Result.Id).Id,

                    };
                    _dbContext.UserDetails.Add(userEntry);
                    _dbContext.SaveChanges();

                    var dbUser = _dbContext.UserDetails.FirstOrDefault(r => r.ApplicationUserId == userEntry.ApplicationUserId);

                    var newTeacher = new Teacher
                    {
                        FirstName = teacherDto.FirstName,
                        LastName = teacherDto.LastName,
                        CreatedBy = null,
                        UpdatedBy = null,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        UserId = dbUser.Id,
                        UserName = userM.UserName,
                    };
                    _dbContext.Teachers.Add(newTeacher);
                    _dbContext.SaveChanges();
                    if (teacherDto != null && teacherDto.ClassIds.Any())
                    {
                        var classes = _dbContext.Classes.Where(c => teacherDto.ClassIds.Contains(c.Id)).ToList();
                        if (classes.Count == teacherDto.ClassIds.Count)
                        {
                            foreach (var classItem in classes)
                            {
                                //classItem.SchoolId = _currentUser.SchoolId;
                                newTeacher.Classes.Add(classItem);
                                _dbContext.SaveChanges();
                            }
                        }
                    }
                    if (teacherDto != null && teacherDto.SubjectIds.Any())
                    {
                        var subjects = _dbContext.Subjects.Where(c => teacherDto.SubjectIds.Contains(c.Id)).ToList();
                        if (subjects.Count == teacherDto.SubjectIds.Count)
                        {
                            foreach (var subjectItem in subjects)
                            {
                                newTeacher.Subjects.Add(subjectItem);
                                _dbContext.SaveChanges();
                            }
                        }
                    }
                }
                _dbContext.SaveChanges();


                return ResponseHelper<string>.CreateSuccessRes("Teacher Added successfully", new List<string> { "Success" });
            }
            throw new CustomException("Email already exists");

        }
        public async Task<APIResponse<string>> CreateClass(AddClassDto addClassDto)
        {

            var stClass = await _dbContext.Classes.FirstOrDefaultAsync(c => c.ClassSection.ToLower() == addClassDto.ClassSection.ToLower() && c.ClassName == addClassDto.ClassName && c.Id == _currentUser.Id);
            if (stClass != null)
            {
                return null;
            }
            var payload = new Class
            {
                ClassName = addClassDto.ClassName,
                ClassSection = addClassDto.ClassSection,
            };
            // await _classRepository.Create(payload);
            _dbContext.Classes.Add(payload);
            _dbContext.SaveChanges();
            if (addClassDto != null && addClassDto.SubjectIds.Any())
            {
                var subjects = _dbContext.Subjects.Where(c => addClassDto.SubjectIds.Contains(c.Id)).ToList();
                if (subjects.Count == addClassDto.SubjectIds.Count)
                {
                    foreach (var subjectItem in subjects)
                    {
                        payload.Subjects.Add(subjectItem);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return ResponseHelper<string>.CreateSuccessRes("Class created", new List<string> { "Success." });
        }

        public async Task<APIResponse<string>> CreateSubject(AddSubjectDto addSubjectDto)
        {

            var stClass = await _dbContext.Subjects.FirstOrDefaultAsync(c => c.SubjectName.ToLower() == addSubjectDto.SubjectName.ToLower());
            if (stClass != null)
            {
                return null;
            }
            var payload = new Subject
            {
                SubjectName = addSubjectDto.SubjectName,

            };
            await _subjectRepository.Create(payload);
            //_dbContext.Subjects.Add(payload);
            //_dbContext.SaveChanges();
            return ResponseHelper<string>.CreateSuccessRes("Subject created", new List<string> { "Success." });
        }

        public async Task<APIResponse<List<TeacherViewModel>>> GetTeachers()
        {
            var teac = await _dbContext.Teachers.Where(l => l.IsActive).Include(c => c.Classes).ToListAsync();

            if (teac == null)
            {
                return ResponseHelper<List<TeacherViewModel>>.CreateSuccessRes(null, HttpStatusCode.OK, new List<string> { "No Teachers found" });
            }
            var teacherVM = teac.Select(teacher => new TeacherViewModel
            {
                TeacherName = $"{teacher.FirstName}  {teacher.LastName}",
                Classes = teacher.Classes.Select(claxx => new ClassViewModel
                {
                    ClassName = claxx.ClassName,
                    ClassSection = claxx.ClassSection,

                }).ToList()
            }).ToList();
            return ResponseHelper<List<TeacherViewModel>>.CreateGetSuccessResponse(teacherVM, teacherVM.Count);
        }
        public async Task<APIResponse<List<ClassViewModel>>> GetAllClasses()
        {
            var claxx = await _dbContext.Classes.ToListAsync();

            if(claxx == null)
            {
                return ResponseHelper<List<ClassViewModel>>.CreateSuccessRes(null, HttpStatusCode.OK, new List<string> { "No classes found" });

            }

            var classVM = claxx.Select(claxx => new ClassViewModel
            {
                ClassName = claxx.ClassName,    
                ClassSection = claxx.ClassSection, 
            }).ToList();
            return ResponseHelper<List<ClassViewModel>>.CreateGetSuccessResponse(classVM, classVM.Count);

        }
    }

}
