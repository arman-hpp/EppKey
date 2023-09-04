using System.Timers;

namespace EppKey
{
    internal static class Program
    {
        private static readonly Timer CoreTimer = new Timer { Interval = 7200000 };

        private static void Main()
        {
            if (DllHelper.IsDllExists() == false)
            {
                Logger.Log("KY_EPP106_DLL Library Not Found");
                return;
            }

            Run();

            CoreTimer.Elapsed += (sender, args) => { Run(); };
        }

        private static void Run()
        {
            EppHelper.ClosePort();

            var portNum = PortFinder.FindPort4();
            if (portNum < 0)
                return;

            var openState = EppHelper.OpenPort(portNum);
            if (openState < 0)
                return;

            EppHelper.EnterPinCode();

            EppHelper.SetPlainTextMode();

            EppHelper.GetCode();

            EppHelper.ClosePort();
        }
    }
}
