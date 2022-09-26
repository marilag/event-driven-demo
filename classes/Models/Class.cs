namespace eventschool {
    
    public class Class{

        public Guid ClassId { get; set; } = System.Guid.NewGuid();
        public string ClassCode { get; set; } = String.Empty;
        public string ClassName { get; set; } = String.Empty;
        public string Program { get; set; } = String.Empty;
        public string Term { get; set; } = String.Empty;
   
        public int MaxStudents { get; set; } 

        private readonly HashSet<string> _enroledStudents  = new HashSet<string>();

        public  IReadOnlyCollection<string> EnroledStudents  => _enroledStudents;

        public Class(string code, string className, string program, string term, int maxStudents)        
        {
            ClassCode = code;
            ClassName = className;
            Program = program;
            Term = term;
            MaxStudents = maxStudents;
        }

        public void AddStudent(string id)
        {
            if (_enroledStudents.Count == MaxStudents) 
                throw new Exception("Class is full");

            _enroledStudents.Add(id);
        }

         public void RemoveStudent(string id)
        {            
            _enroledStudents.Remove(id);
        }

                                                                                                                                                              
        

    }
}