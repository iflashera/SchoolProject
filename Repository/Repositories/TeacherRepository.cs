using Common.CurrentUser;
using Common.CustomException;
using Common.DTOs.Teacher;
using Common.Helper;
using DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models.Classes;
using Models.Models.Identity;
using Models.Models.Users;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ICurrentUser _currentUser;

        public TeacherRepository(SmsDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
        ICurrentUser currentUser, IRepository<Class> classRepository)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUser = currentUser;
            _classRepository = classRepository; 
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

                IdentityResult result = _userManager.CreateAsync(user).Result;
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
           
            var stClass = await _dbContext.Classes.FirstOrDefaultAsync(c => c.ClassSection.ToLower() == addClassDto.ClassSection.ToLower() && c.ClassName == addClassDto.Classname && c.Id == _currentUser.Id);
            if (stClass != null)
            {
                return null;
            }
            var payload = new Class
            {
                ClassName = addClassDto.Classname,                
                ClassSection = addClassDto.ClassSection,
            };
            // await _classRepository.Create(payload);
            _dbContext.Classes.Add(payload);
            _dbContext.SaveChanges();
            return ResponseHelper<string>.CreateSuccessRes("Class created", new List<string> { "Success." });
        }

    }
}
   
