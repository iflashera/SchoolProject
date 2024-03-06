using Common.ViewModel;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Models.Models.Users;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SmsDbContext _dbContext;
        public StudentRepository( SmsDbContext dbContext)
        {
            _dbContext = dbContext;   
        }
        public async Task<List<StudentViewModel>> GetStudentsByClassId(int Id)
        {
            var childrens =  _dbContext.Students.Where(s => s.ClassId == Id).Include(c => c.Class).ToList();
            if(childrens == null)
            {
                return null;
            }
            var childVM = new List<StudentViewModel>();
            childVM = childrens.Select(child => new StudentViewModel
            {
                //ChildId = child.Id,
                FullName = $"{child.FirstName}  {child.LastName}",
                RollNo = child.RollNo,  
                Address = child.Address,    
                PhoneNumber = child.PhoneNumber,  
                ClassName = child.Class.ClassName,
            }).ToList();
            return childVM;
        }
    }
 }

