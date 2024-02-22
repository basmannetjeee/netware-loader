using SharpMonoInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetWareLoader
{
    internal class inject
    {
        void Inject(string targetProcess, FileInfo assembly, string loaderNamespace, string loaderClass, string loaderMethod)
        {
            using Injector injector = new(targetProcess);

            string assemblyPath = assembly.FullName;
            byte[] assemblyBytes = File.ReadAllBytes(assemblyPath);

            try
            {
                IntPtr remoteAssembly = injector.Inject(assemblyBytes, loaderNamespace, loaderClass, loaderMethod);
                if (remoteAssembly == IntPtr.Zero) return;

                Console.WriteLine($"{assemblyPath}: {(injector.Is64Bit ? $"0x{remoteAssembly.ToInt64():X16}" : $"0x{remoteAssembly.ToInt32():X8}")}");
            }

            catch (InjectorException ie)
            {
                Console.WriteLine($"Injection failed: {ie}");
            }
        }
    }
}
