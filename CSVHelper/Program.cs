using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CSVHelper
{
    internal class Program
    {
        //static CSV csv = new CSV("D:\\data.csv");

        static void Main(string[] args)
        {

            // Hw1: Read的時候，可以多給一個布林參數，用來判斷是否要將標題放入到list中
            // HW2: 研究DAO Model 資料對位:
            // 1.沒有Header 以CSV資料為主 一個蘿蔔一個坑 把資料填到對應的Property裡面
            // 2.有Header 以Model Property為主，不管Model欄位怎麼變動位置/數量減少 (Model只有三個欄位，csv有10個欄位)
            // 2.1 如何做到自動判斷是否有無Header (Tips:偷撈第0筆資料出來看有沒有對應到PropertyName/DisplayName)

            string path = "D:\\files\\data.csv";

            //List<Student> students = CSV.Read<Student>(path);

            //List<Student> students = CreateStudentGroup();
            //CSV.Write(path, students, true, true);

            List<Student> students = CSV.Read<Student>(path, true);

            foreach (Student student in students)
            {
                Console.WriteLine(JsonConvert.SerializeObject(student));
            }

            //Header header = newHeader("Name", "Num", "Age");
            //CSV.Write(path, header);

            //foreach (Student student in students)
            //{
            //    CSV.Write(path, student);
            //}

            //string[] values = new string[] { "a", "1", "12" };
            //Student student = CreateObject<Student>(values);
            //GetObjectVaule(student);

            Console.ReadKey();
        }

        static List<Student> CreateStudentGroup()
        {
            Student student1 = new Student("A1", "10", "11");
            Student student2 = new Student("A2", "10", "11");
            Student student3 = new Student("A3", "10", "11");
            Student student4 = new Student("A4", "10", "11");

            List<Student> students = new List<Student>()
            { student1, student2, student3, student4 };

            return students;
        }

        static T CreateObject<T>(string[] values) where T : class, new()
        {
            T t = new T();
            Type type = t.GetType();
            var props = type.GetProperties();

            for (int i = 0; i < values.Length; i++)
            {
                props[i].SetValue(t, values[i]);
            }

            //foreach (var prop in props)
            //{
            //    switch (prop.Name)
            //    {
            //        case "Name":
            //            prop.SetValue(t, "a");
            //            break;

            //        case "Num":
            //            prop.SetValue(t, "12");
            //            break;

            //        case "Age":
            //            prop.SetValue(t, "13");
            //            break;
            //    }
            //}

            return t;
        }
    }
}
