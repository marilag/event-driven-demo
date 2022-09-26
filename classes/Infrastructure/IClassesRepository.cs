namespace eventschool
{
    public interface IClassesRepository
    {
        public Task<Class> Get(string classId);
        public Task<IEnumerable<Class>> Get();
        public Task Save(Class _class);

    }
}