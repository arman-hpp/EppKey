using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Threading;

namespace EppKey
{
    public static class EppHelper
    {
        public static int OpenPort(int portNum)
        {
            try
            {
                return DllHelper.KY_EPP106_DLL_OPENPORT(portNum, 9600);
            }
            catch (Exception e)
            {
                Logger.Log($"OpenPort Error: {e.Message}");
                return -1;
            }
        }

        public static void ClosePort()
        {
            try
            {
                DllHelper.KY_EPP106_DLL_CLOSEPORT();
            }
            catch (Exception e)
            {
                Logger.Log($"ClosePort Error: {e.Message}");
            }
        }

        public static int EnterPinCode()
        {
            try
            {
                var ucData = new byte[100];
                for (var j = 0; j < ucData.Length; j++)
                {
                    ucData[j] = 0;
                }

                ucData[0] = 4;
                ucData[1] = 0x01;
                ucData[2] = 0x00;
                ucData[3] = 0x00;
                ucData[4] = 50;

                return DllHelper.KY_EPP106_DLL_COMMAND(0x35, ucData, 5, null);
            }
            catch (Exception e)
            {
                Logger.Log($"EnterPinCode Error: {e.Message}");
                return -1;
            }
        }

        public static int SetPlainTextMode()
        {
            try
            {
                var ud = new byte[1];
                ud[0] = 0x03;

                return DllHelper.KY_EPP106_DLL_COMMAND(0x45, ud, 1, null);
            }
            catch (Exception e)
            {
                Logger.Log($"SetPlainTextMode Error: {e.Message}");
                return -1;
            }
        }

        [SuppressMessage("ReSharper", "FunctionNeverReturns")]
        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        public static void GetCode()
        {
            while (true)
            {
                try
                {
                    var ucKey = new byte[1];
                    DllHelper.KY_EPP106_DLL_GETCODE(ucKey);

                    var num = ucKey[0];

                    if (ucKey[0] == 0x00)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    switch (num)
                    {
                        case 63: //000
                            Send(48);
                            Send(48);
                            Send(48);
                            break;

                        case 42:
                        case 46:
                            break;

                        default: //other keys
                            Send(num);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Logger.Log($"GetCode Error: {e.Message}");
                }
            }
        }

        private static void Send(int uk)
        {
            try
            {
                var handle = NativeWin32.FindActiveWindow();
                NativeWin32.SetForegroundWindow(handle);
                var keys = Convert.ToChar(uk).ToString(CultureInfo.InvariantCulture);
                System.Windows.Forms.SendKeys.SendWait(keys);
            }
            catch (Exception e)
            {
                Logger.Log($"Send Error: {e.Message}");
            }
        }

        public static string GetVersion()
        {
            try
            {
                var chVer = new byte[100];
                for (var j = 0; j < chVer.Length; j++)
                    chVer[j] = 0;

                DllHelper.KY_EPP106_DLL_COMMAND(0x30, null, 0, chVer);

                return Encoding.ASCII.GetString(chVer);

            }
            catch (Exception e)
            {
                Logger.Log($"Send Error: {e.Message}");
                throw;
            }
        }
    }
}
