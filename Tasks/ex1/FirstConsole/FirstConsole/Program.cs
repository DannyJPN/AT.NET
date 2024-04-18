using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SecondConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain current = AppDomain.CurrentDomain;
            Console.WriteLine($"Current name: {current.FriendlyName}");
            Console.WriteLine($"Current name: {current.BaseDirectory}");
            PermissionSet permset = new PermissionSet(PermissionState.None);
            permset.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            permset.AddPermission(new FileIOPermission(FileIOPermissionAccess.AllAccess, Path.GetFullPath(@"C:\Users\kru0142\Desktop\AT.NET\Tasks\ex1\SecondConsole\SecondConsole\bin\Debug")));
            permset.AddPermission(new FileIOPermission(FileIOPermissionAccess.AllAccess, Path.GetFullPath("./")));

            permset.AddPermission(new UIPermission(PermissionState.Unrestricted));


            AppDomain inner = AppDomain.CreateDomain("insider",null,new AppDomainSetup() {ApplicationBase = "./substitutedir" },permset);
            //null = Evidence,nefunkční
            inner.ExecuteAssembly(@"C:\Users\kru0142\Desktop\AT.NET\Tasks\ex1\SecondConsole\SecondConsole\bin\Debug\SecondConsole.exe",new string[] { "insider1","insider2","insider3"});
            AppDomain.Unload(inner);


        }
    }
}
