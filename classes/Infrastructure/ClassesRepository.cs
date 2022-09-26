using System.Collections;
using Microsoft.Extensions.Options;

namespace eventschool
{
    public class ClassesRepository : IEnumerable<Class>, IClassesRepository
    {
        private readonly IOptions<ClassOptions> options;
        private readonly List<Class> Classes = new List<Class>();

        public ClassesRepository(IOptions<ClassOptions> options)
        {
            this.options = options;
            SeedClasses();

        }

        private void SeedClasses()
        {
            Classes.Add(new Class("c1","QuantumMechanics1","CS22","2022-01",3));
            Classes.Add(new Class("c2","Ethics1","CS22","2022-01",3));
            Classes.Add(new Class("c3","MachineLearning","CS22","2022-01",3));
        }

        public async Task<Class> Get(string classCode)
        {
            return Classes.Where(c =>  c.ClassCode.ToString()
            .Equals(classCode,StringComparison.InvariantCultureIgnoreCase))
            .FirstOrDefault() ?? throw new Exception("Class not found");
        }

        public async Task<IEnumerable<Class>> Get()
        {
            return Classes;
        }

        public IEnumerator<Class> GetEnumerator()
        {
            return Classes.GetEnumerator();
        }

        public async Task Save(Class _class)
        {
            Classes.Add(_class);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}