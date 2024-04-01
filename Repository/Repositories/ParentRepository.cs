using Common;
using Common.CurrentUser;
using Common.DTOs.Parent;
using Common.Helper;
using Common.ViewModel;
using DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models.Identity;
using Models.Models.Users;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Models.Enum.Enums;

namespace Repository.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly SmsDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ICurrentUser _currentUser;

        public ParentRepository(SmsDbContext dbContext, ICurrentUser currentUser, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUser = currentUser;
        }

        public async Task<APIResponse<string>> AddChildToParent(int parentId, List<AddChildDto> children)
        {
            var parent = await _dbContext.Parents.Where(i => i.Id == parentId).Include(p=>p.Student).FirstOrDefaultAsync();
            if (parent == null)
            {
                return ResponseHelper<string>.CreateErrorRes(null, new List<string> { "Invalid Parent Id" });
            }

            foreach (var child in children)
            {
                if (_userManager.FindByEmailAsync(child.Email).Result == null)
                {
                    ApplicationUser childuser = new ApplicationUser
                    {
                        UserName = child.Email,
                        Email = child.Email,
                        FirsName = child.FirstName,
                        LastName = child.LastName,
                    };
                    var parentResult = await _userManager.CreateAsync(childuser);
                    if (parentResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(childuser, "Student");
                        var userDetailChild = new UserDetail
                        {
                            ApplicationUserId = childuser.Id,
                            RoleId = _dbContext.Roles.FirstOrDefault(r => r.ApplicationRoleId == _roleManager.FindByNameAsync("Student").Result.Id).Id,
                        };
                        _dbContext.UserDetails.AddRange(userDetailChild);
                        await _dbContext.SaveChangesAsync();
                        var dbUserChild = _dbContext.UserDetails.FirstOrDefault(r => r.ApplicationUserId == userDetailChild.ApplicationUserId);
                        var newChild = new Student
                        {
                            FirstName = child.FirstName,
                            LastName = child.LastName,
                            PhoneNumber= child.PhoneNumber, 
                            Address = child.Address,    
                            RollNo  = child.RollNo, 
                            CreatedBy = null,
                            UpdatedBy = null,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            UserId = dbUserChild.Id,
                            ParentId = parent.Id,
                            UserName = childuser.UserName,
                            ClassId = child.ClassId,

                        };
                        //_dbContext.Students.Add(newChild);
                        //_dbContext.SaveChanges();
                        parent.Student.Add(newChild);
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
            return ResponseHelper<string>.CreateSuccessRes(null, new List<string> { Constant.AddSuccess });


        }
    
        public async Task<int> AddParent(AddparentDto parentDto)
        {
            if (await _userManager.FindByNameAsync(parentDto.Email) != null)
            {
                return (int)AddUpdateResults.Duplicate;
            }

            var parentuser = new ApplicationUser
            {
                UserName = parentDto.Email,
                Email = parentDto.Email,
                FirsName = parentDto.FirstName,
                LastName = parentDto.LastName,
            };
            var parentResult = await _userManager.CreateAsync(parentuser);
            if (parentResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(parentuser, "Parent");
                var userDetailParent = new UserDetail
                {
                    ApplicationUserId = parentuser.Id,
                    RoleId = _dbContext.Roles.FirstOrDefault(r => r.ApplicationRoleId == _roleManager.FindByNameAsync("Parent").Result.Id).Id,
                };
                _dbContext.UserDetails.AddRange(userDetailParent);
                await _dbContext.SaveChangesAsync();

                var parent = new Parent
                {
                    ParentFirstName = parentDto.FirstName,
                    ParentLastName = parentDto.LastName,
                    ParentUserName = parentuser.UserName,
                    CreatedBy = null,
                    UpdatedBy = null,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    ParentUserId = userDetailParent.Id,
                    Student = new List<Student>()
                };
                _dbContext.Parents.Add(parent);
                await _dbContext.SaveChangesAsync();
                
                foreach( var child in parentDto.Childrens)
                {
                    var ch = await _userManager.FindByNameAsync(child.Email);
                    if ( ch == null)
                    {
                        var childuser = new ApplicationUser
                        {
                            UserName = child.Email,
                            Email = child.Email,
                            FirsName = child.FirstName,
                            LastName = child.LastName,
                        };
                        var childResult = await _userManager.CreateAsync(childuser,"Password@1");
                        if (childResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(childuser, "Student");
                            var userDetailChild = new UserDetail
                            {
                                ApplicationUserId = childuser.Id,
                                RoleId = _dbContext.Roles.FirstOrDefault(r => r.ApplicationRoleId == _roleManager.FindByNameAsync("Student").Result.Id).Id,
                            };
                            _dbContext.UserDetails.Add(userDetailChild);
                            await _dbContext.SaveChangesAsync();
                            var newChild = new Student
                            {
                                FirstName = child.FirstName,
                                LastName = child.LastName,
                                Address = child.Address,
                                RollNo = child.RollNo,
                                PhoneNumber = child.PhoneNumber,
                                CreatedBy = null,
                                UpdatedBy = null,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                                UserId = userDetailChild.Id,
                                ParentId = parent.Id,
                                UserName = childuser.UserName,
                                ClassId = child.ClassId,
                            };

                            parent.Student.Add(newChild);
                           
                        }
                    }
                    else return (int)AddUpdateResults.Duplicate;
                }
                await _dbContext.SaveChangesAsync();
                return (int)AddUpdateResults.Success;
            }
            return (int)AddUpdateResults.Exception;
        }

        public async Task<APIResponse<List<ParentViewModel>>> GetAllParents()
        {
            var parents = await _dbContext.Parents.Where(l => l.IsActive).Include(l => l.Student).ThenInclude(s=>s.Class).ToListAsync();

            if( parents == null || parents.Count == 0 ) 
            {
                return ResponseHelper<List<ParentViewModel>>.CreateSuccessRes(null, HttpStatusCode.OK, new List<string> { "No Parent found" });
            }

            var parentVM = parents.Select(parent => new ParentViewModel
            {
                FullName = $"{parent.ParentFirstName}  {parent.ParentLastName}",
                Childrens = parent.Student.Select(students => new StudentViewModel
                {
                    FullName = $"{students.FirstName}  {students.LastName}",
                    RollNo = students.RollNo,
                    ClassName = students.Class.ClassName,

              }).ToList(),
            }).ToList();
            return ResponseHelper<List<ParentViewModel>>.CreateGetSuccessResponse(parentVM, parentVM.Count);

        }
         
        public async Task<APIResponse<string>> UpdateParent(UpdateParentDto ParentDto)
        {
            throw new NotImplementedException();
        }
    }
}
