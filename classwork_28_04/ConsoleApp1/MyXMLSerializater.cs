using System.Xml.Serialization;

namespace MySerialization;

public class MyXMLSelializer : MySerializater
{
    public new void Serialize()
    {
        var ser = new XmlSerializer(typeof(DTOStudent[]));

        _path  = Path.Combine(_desktopPath, "exmaple.xml");

        using (var fs = new StreamWriter(_path))
        {
            var dtoObjects = new List<DTOStudent>(_students.Count);
            foreach(var student in _students)
            {
                dtoObjects.Add(new DTOStudent(student));
            }
            ser.Serialize(fs, dtoObjects.ToArray());
        }
    }

    public new void Deserialize()
    {
        var ser = new XmlSerializer(typeof(DTOStudent[]));

        using (var sr = new StreamReader(_path))
        {
            var objects = ser.Deserialize(sr) as DTOStudent[];
            _students = new List<Student>();

            foreach (var obj in objects)
            {
                _students.Add(obj.GetStudent());
            }
            System.Console.WriteLine();
            foreach (var student in _students)
        {
            Console.WriteLine($"{student.Id} {student.Name} has marks: " +
                $"{string.Join(" ", student.Subjects.Select(x => x.FinalMark))}");
        }
        }
    }

    public class DTOStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DTOSubject[] Subjects { get; set; }
        
        public DTOStudent()
        {
            
        }
        public DTOStudent(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            Surname = student.Surname;
            var dtoObject = new List<DTOSubject>(student.Subjects.Length);
            foreach (var subject in student.Subjects)
            {
                dtoObject.Add(new DTOSubject(subject));
            }
            Subjects = dtoObject.ToArray();
        }

        public Student GetStudent()
        {
            var subjects = new Subject[Subjects.Length];
            for (int i = 0; i < Subjects.Length; i++)
                subjects[i] = Subjects[i].GetSubject();
            return new Student(Id, Name, Surname, subjects);
        }
    }

    [XmlInclude(typeof(DTOCourse))]
    public class DTOSubject
    {
        [XmlElement(ElementName = "Subject")]
        public string Name { get; set; }
        public int[] Marks { get; set; }

        public DTOSubject()
        {
            
        }
        public DTOSubject(Subject subject)
        {
            Name = subject.Name;
            Marks = subject.Marks;
        }

        public virtual Subject GetSubject()
        {
            return new Subject(Name, Marks);
        }
    }

    public class DTOCourse : DTOSubject
    {
        [XmlElement(ElementName = "Subject")]
        public string Name { get; set; }
        public int[] Marks { get; set; }
        public int Duration { get; set; }

        public DTOCourse()
        {
            
        }
        public DTOCourse(Course course)
        {
            Name = course.Name;
            Marks = course.Marks;
            Duration = course.Duration;
        }

        public override Course GetSubject()
        {
            return new Course(Name, Marks, Duration);
        }
    }
}