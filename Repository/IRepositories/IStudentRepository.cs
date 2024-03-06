using Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IStudentRepository
    {
        Task<List<StudentViewModel>> GetStudentsByClassId(int id);
    }
}
