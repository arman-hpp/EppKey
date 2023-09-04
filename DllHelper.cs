using System.IO;
using System.Runtime.InteropServices;

namespace EppKey
{
    public static class DllHelper
    {
        [DllImport(@"KY_EPP106_DLL.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "KY_EPP106_DLL_OPENPORT")]
        public static extern int KY_EPP106_DLL_OPENPORT(int iPort, int iBaudrate);

        [DllImport(@"KY_EPP106_DLL.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "KY_EPP106_DLL_COMMAND")]
        public static extern int KY_EPP106_DLL_COMMAND(byte ucCmd, [MarshalAs(UnmanagedType.LPArray)] byte[] ucData,
            byte ucDLen, [MarshalAs(UnmanagedType.LPArray)] byte[] ucResponse);

        [DllImport(@"KY_EPP106_DLL.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "KY_EPP106_DLL_GETCODE")]
        public static extern void KY_EPP106_DLL_GETCODE([MarshalAs(UnmanagedType.LPArray)] byte[] ucCode);

        [DllImport(@"KY_EPP106_DLL.dll", CallingConvention = CallingConvention.StdCall,
            EntryPoint = "KY_EPP106_DLL_CLOSEPORT")]
        public static extern void KY_EPP106_DLL_CLOSEPORT();

        public static bool IsDllExists()
        {
            return File.Exists("KY_EPP106_DLL.dll");
        }
    }
}
