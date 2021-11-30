using API.Context;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ViewModel;
using System.Collections;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        private readonly MyContext context;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public IEnumerable<UniversitiesVM> GetUniversities()
        {
            var query = (from e in context.Set<Employee>()
                         join a in context.Set<Account>() on new { NIK = e.NIK } equals new { NIK = a.NIK }
                         join p in context.Set<Profiling>() on new { NIK = a.NIK } equals new { NIK = p.NIK }
                         join ed in context.Set<Education>() on new { EdId = p.EducationId } equals new { EdId = ed.EducationId }
                         join u in context.Set<University>() on new { UId = ed.UniversityId } equals new { UId = u.UniversityId }
                         group ed by ed.UniversityId into g
                         select new UniversitiesVM
                         {
                             UniversityId = g.First().UniversityId,
                             UniversityName = g.First().University.Name,
                             StudentCounts = g.Count()
                         });
            return query.ToList();
        }

        public KeyValuePair<List<string>, List<int>> GetStudents ()
        {
            var query = (from ed in context.Set<Education>()
                         join u in context.Set<University>() on new { UId = ed.UniversityId } equals new { UId = u.UniversityId }
                         group u by u.Name into g
                         select new
                         {
                             Name =g.Key,
                             Students = g.Count()

                         }).ToList();

            List<string> Name = new List<string>();
            List<int> StudentsCount = new List<int>();

            foreach (var i in query)
            {
                Name.Add(i.Name);
                StudentsCount.Add(i.Students);
            }

            return new KeyValuePair<List<string>, List<int>>(Name, StudentsCount);
        }

    }
    
}
