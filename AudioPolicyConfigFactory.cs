using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SetAppAudioDevice
{
    public class AudioPolicyConfigFactory
    {
        private IAudioPolicyConfigFactory _factory;

        public AudioPolicyConfigFactory()
        {
            Console.WriteLine("开始动态扫描 Windows.Media.* 类型...");

            // 获取加载到 AppDomain 的所有程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // 过滤出包含 Windows.Media 的程序集
            var wmAssemblies = assemblies
                .Where(a => a.FullName != null && a.FullName.StartsWith("Windows"))
                .ToArray();

            foreach (var asm in wmAssemblies)
            {
                Type[] types;
                try
                {
                    types = asm.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    types = ex.Types.Where(t => t != null).ToArray();
                }

                foreach (var type in types)
                {
                    try
                    {
                        if (type.IsClass && !type.IsAbstract)
                        {
                            // 判断是否实现了 IAudioPolicyConfigFactory 接口
                            var interfaces = type.GetInterfaces();
                            if (interfaces.Contains(typeof(IAudioPolicyConfigFactory)))
                            {
                                Console.WriteLine($"找到实现类: {type.FullName}");
                                _factory = Activator.CreateInstance(type) as IAudioPolicyConfigFactory;
                                if (_factory != null)
                                {
                                    Console.WriteLine("接口激活成功");
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("创建实例失败，继续扫描...");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"扫描类型 {type.FullName} 时错误: {ex.Message}");
                    }
                }
            }

            throw new NotSupportedException("无法找到 IAudioPolicyConfigFactory 的实现类");
        }

        public void SetPersistedDefaultAudioEndpoint(uint processId, EDataFlow flow, ERole role, string deviceId)
        {
            int hr = _factory.SetPersistedDefaultAudioEndpoint(processId, flow, role, deviceId);
            if (hr != 0) Marshal.ThrowExceptionForHR(hr);
        }
    }
}
