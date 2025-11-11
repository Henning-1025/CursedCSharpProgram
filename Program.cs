using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace std
{
    internal static class c
    {
        public static cout_t cout = new cout_t();
    }
}

internal unsafe struct cstr
{
    private char* ptr;
    
    public cstr(char* p) => ptr = p;
    
    public static implicit operator char*(cstr s) => s.ptr;
    
    public static cstr operator <<(cstr dest, string src)
    {
        for (int i = 0; i < src.Length; i++)
            dest.ptr[i] = src[i];
        dest.ptr[src.Length] = '\0';
        return dest;
    }
}

internal unsafe struct cout_t
{
    public static int operator <<(cout_t cout, cstr str)
    {
        Console.WriteLine(new string((char*)str));
        return 0;
    }
}

unsafe internal class Program
{
    static int Main(string[] args)
    {
        nuint ptrSize = sizeof(char) * 11;
        cstr str = new cstr((char*)NativeMemory.Alloc(ptrSize));
        _ = std.c.cout << (str << "hello world");
        NativeMemory.Free((void*)str);
        return 0;
    }
}