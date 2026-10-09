using System.Diagnostics;

namespace SetAppAudioDevice
{
    public static class ProcessFinder
    {
        public static uint FindPidByName(string name)
        {
            var ps = Process.GetProcessesByName(name);
            if (ps.Length == 0) throw new System.Exception($"未找到进程 {name}");
            return (uint)ps[0].Id;
        }
    }
}
