using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PermissionSet pset = new PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);

            //AppDomain testplugin = AppDomain.CreateDomain("testplugin", null, new AppDomainSetup() { ApplicationBase = @"C:\Users\kru0142\Desktop\AT.NET\Tasks\ex9\Test1\ReflectionConsoleApp\TestPlugin" }, pset);

            /* object o = testplugin.CreateInstance("Plugin", "Plugin.Blackbox");

             Type blacktype = o.GetType();
             MethodInfo[] methods = blacktype.GetMethods(BindingFlags.Public|BindingFlags.Instance);
             string input = "string", output = "int";

             for (int i = 0; i < methods.Length; i++)
             {


                 Console.WriteLine(methods[i].Name);
                // Console.WriteLine(methods[i].GetBaseDefinition());




             };
             Console.WriteLine("_______________________________________");
             MethodInfo[] allmethods = blacktype.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
             MethodInfo twostringsmethod = null;
             for (int i = 0; i < allmethods.Length; i++)
             {

                 if (allmethods[i].GetParameters().Length == 2 && allmethods[i].GetParameters()[0].ParameterType == typeof(string) && allmethods[i].GetParameters()[0].ParameterType == typeof(string))
                 {
                     Console.WriteLine(allmethods[i].Name);
                    // Console.WriteLine(allmethods[i].GetBaseDefinition());
                     twostringsmethod = allmethods[i];
                     break;
                 }


             };

             bool caster=(bool)twostringsmethod.Invoke(o,  new object[] { input, output });
             Console.WriteLine("{0} {1} {2}",input,output,caster);

             */

            List<string> dlls = Load();
            string path = @"C:\Users\Dan\Dropbox\UNI\VSB\VSB\BC_IVT_III\S5\VIS\Project\LeisureRegister\DataAccessLayer";
            foreach (string s in dlls)
            {/*
Assembly assem = Assembly.LoadFile(s);
                AppDomain testplugin = AppDomain.CreateDomain("testplugin", null, new AppDomainSetup() { ApplicationBase = path}, pset);

                AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                         .Where(x =>  !x.IsInterface && !x.IsAbstract)
                         .Select(x =>String.Format("{0}:{1}",x.Assembly,x.Name)).ToList();
                
                */
                Console.WriteLine(s);

                

            }



        }

        public static List<string> Load()
        {

            string path = @"C:\Users\Dan\Dropbox\UNI\VSB\VSB\BC_IVT_III\S5\VIS\Project\LeisureRegister\DataAccessLayer";
            /* string[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
             foreach (string s in files)
             { Console.WriteLine(s); }*/
            PermissionSet pset = new PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);

            AppDomain testplugin = AppDomain.CreateDomain("testplugin", null, new AppDomainSetup() { ApplicationBase = path }, pset);

           List<string> files=testplugin.GetAssemblies().SelectMany(x => x.GetTypes())
                           .Where(x => !x.IsInterface && !x.IsAbstract)
                           .Select(x => String.Format("{0}:{1}", x.Assembly.FullName, x.Name)).ToList();


            return files.ToList();
        }
    }
}
