using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FourthApp
{

    public class IgnoreSQLAttribute : Attribute
    {


    }


    public class TableNameAttribute : Attribute
    {
        public string Name { get; private set; }
        public TableNameAttribute(string name)
        {
            Name = name;
        }

    }


    [TableName("PersonTab")]
    public class Person
    {
        [IgnoreSQL]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }


    }







    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath("../../../FourthAppLib/bin/Debug/FourthAppLib.dll");
            Assembly assem = Assembly.LoadFile(path);
            MethodInfo[] met = assem.GetType("FourthAppLib.SimpleCalc").GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < met.Length; i++)
            {

                Console.WriteLine("{0} {1}", met[i].IsPrivate ? "private" : met[i].IsPublic ? "public" : "protected", met[i]);
                Console.WriteLine("{0}", string.Join("-", met[i].GetParameters().Select(x => String.Format("{0} {1}", x.ParameterType, x.Name))));
            }

            object simcal = assem.CreateInstance("FourthAppLib.SimpleCalc");
            object actsimcal = Activator.CreateInstance(assem.GetType("FourthAppLib.SimpleCalc"));



            MethodInfo setxy = assem.GetType("FourthAppLib.SimpleCalc").GetMethod("SetXY");
            setxy.Invoke(simcal, new object[] { 20, 60 });

            MethodInfo add = assem.GetType("FourthAppLib.SimpleCalc").GetMethod("Add");
            add.Invoke(simcal, new object[] { });

            MethodInfo mult = assem.GetType("FourthAppLib.SimpleCalc").GetMethod("Multiply",BindingFlags.NonPublic|BindingFlags.Instance);
            mult.Invoke(simcal, new object[] { });
            //Array.Empty<object>
            MethodInfo showres = assem.GetType("FourthAppLib.SimpleCalc").GetMethod("ShowResult", BindingFlags.NonPublic | BindingFlags.Instance);
            showres.Invoke(simcal, new object[] { });
            Person p = new Person() { ID = 1, Name = "Jan Nový", Age = 40, Address = "Janová 6" };
            CreateInsert(p);
            //anonymous class
            CreateInsert(new { });

        }


        public static SqlCommand CreateInsert(object o)
        {

            Type ot = o.GetType();
            PropertyInfo[] pi=ot.GetProperties();
            StringBuilder sb = new StringBuilder("insert into "+(ot.GetCustomAttributes().Where(x=>x is TableNameAttribute).FirstOrDefault() as TableNameAttribute)?.Name+"(");
            for (int i = 0; i < pi.Length; i++)
            {
                if (!pi[i].GetCustomAttributes().Where(x => x is IgnoreSQLAttribute).Any())
                {
                    sb.Append(String.Format("[{0}],", pi[i].Name));
                }
            }
            sb.Append(") values (");
            for (int i = 0; i < pi.Length; i++)
            {
                if (!pi[i].GetCustomAttributes().Where(x => x is IgnoreSQLAttribute).Any())
                {
                    sb.Append(String.Format("@{0},", pi[i].Name));
                }
            }
            sb.Append(")");
            sb.Replace(",)", ")");
            Console.WriteLine(sb.ToString());
            return new SqlCommand(sb.ToString());
        }



    }
}
