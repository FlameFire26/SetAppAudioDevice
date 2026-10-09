using System;
using NAudio.CoreAudioApi;

namespace SetAppAudioDevice
{
    public static class AudioManager
    {
        public static void ListRenderDevices()
        {
            var enumerator = new MMDeviceEnumerator();
            foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                Console.WriteLine($"设备名: {device.FriendlyName}");
                Console.WriteLine($"ID: {device.ID}\n");
            }
        }
    }
}
