using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SharedMemory
{
    public class SharedMemory
    {
        const string DllName = "shared_memory";

        public static IntPtr Module = IntPtr.Zero;

        public static bool Load(string folderName = null)
        {
            if (Module == IntPtr.Zero)
            {
                var fileName = default(string);
                if (!string.IsNullOrEmpty(folderName))
                {
                    fileName = Path.Combine(folderName, DllName);
                }
                else
                {
                    fileName = Path.Combine(Loader.FolderName, DllName);
                }
                Module = Loader.LoadLibrary(fileName);
            }
            return Module != IntPtr.Zero;
        }

        public static bool Unload()
        {
            if (Module != IntPtr.Zero)
            {
                if (!Loader.FreeLibrary(Module))
                {
                    return false;
                }
                Module = IntPtr.Zero;
            }
            return true;
        }

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        static extern HRESULT create_shared_memory(string name, long size, out IntPtr handle);

        public static HRESULT Create(string name, long size, out IntPtr handle)
        {
            return create_shared_memory(name, size, out handle);
        }

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        static extern HRESULT release_shared_memory(IntPtr handle);

        public static HRESULT Release(IntPtr handle)
        {
            return release_shared_memory(handle);
        }

        public enum HRESULT : long
        {
            OK = 0,
            Fail = 0x80004005L
        }
    }
}
