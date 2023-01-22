using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();    
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
