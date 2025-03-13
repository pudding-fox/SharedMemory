using System;
using System.IO;
using System.Net.Configuration;
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
        static extern HRESULT open_shared_memory(string name, long offset, long length, out IntPtr handle, out IntPtr map);

        public static HRESULT Open(string name, long offset, long length, out IntPtr handle, out IntPtr map)
        {
            return open_shared_memory(name, offset, length, out handle, out map);
        }

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        static extern HRESULT close_shared_memory(IntPtr handle, IntPtr map);

        public static HRESULT Close(IntPtr handle, IntPtr map)
        {
            return close_shared_memory(handle, map);
        }

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        static extern HRESULT read_shared_memory(IntPtr handle, IntPtr map, ref byte[] buffer, long offset, long length);

        public static HRESULT Read(IntPtr handle, IntPtr map, ref byte[] buffer, long offset, long length)
        {
            return read_shared_memory(handle, map, ref buffer, offset, length);
        }

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        static extern HRESULT write_shared_memory(IntPtr handle, IntPtr map, ref byte[] buffer, long offset, long length);

        public static HRESULT Write(IntPtr handle, IntPtr map, ref byte[] buffer, long offset, long length)
        {
            return write_shared_memory(handle, map, ref buffer, offset, length);
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
