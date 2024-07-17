using System.ComponentModel;

namespace CSVHelper
{
    internal class Student
    {
        public string Age { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

        public string Num { get; set; }


        public Student(string name, string num, string age)
        {
            Name = name;
            Num = num;
            Age = age;
        }

        public Student() { }
    }
}
