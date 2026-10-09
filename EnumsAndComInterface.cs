using System;
using System.Runtime.InteropServices;

namespace SetAppAudioDevice
{
    // 设备类型枚举
    public enum EDataFlow { eRender, eCapture, eAll }
    public enum ERole { eConsole, eMultimedia, eCommunications }

    // IAudioPolicyConfigFactory 接口定义（微软内部 API）
    [ComImport]
    [Guid("294935CE-F637-4E7C-A41B-AB255460B862")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioPolicyConfigFactory
    {
        int Unused1();
        int Unused2();
        int Unused3();
        int Unused4();
        int Unused5();

        // 核心方法：切换应用的默认音频终端
        int SetPersistedDefaultAudioEndpoint(
            uint processId,
            EDataFlow flow,
            ERole role,
            [MarshalAs(UnmanagedType.LPWStr)] string deviceId);
    }
}
