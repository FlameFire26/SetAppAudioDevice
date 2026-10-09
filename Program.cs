using System;

namespace SetAppAudioDevice
{
    class Program
    {
        static void Main(string[] args)
        {
            // 如果没有参数，列出所有输出设备
            if (args.Length == 0)
            {
                Console.WriteLine("列出所有输出设备：");
                AudioManager.ListRenderDevices();
                Console.WriteLine("\n用法：SetAppAudioDevice.exe <进程名> <设备ID>");
                return;
            }

            // 如果参数不足
            if (args.Length < 2)
            {
                Console.WriteLine("参数不足：SetAppAudioDevice.exe <进程名> <设备ID>");
                return;
            }

            string processName = args[0];
            string deviceId = args[1];

            try
            {
                uint pid = ProcessFinder.FindPidByName(processName);
                var factory = new AudioPolicyConfigFactory();
                factory.SetPersistedDefaultAudioEndpoint(pid, EDataFlow.eRender, ERole.eConsole, deviceId);
                Console.WriteLine("切换成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("切换失败: " + ex.Message);
            }
        }
    }
}
