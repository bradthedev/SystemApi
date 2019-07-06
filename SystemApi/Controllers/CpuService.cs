using HardwareProviders.CPU;
using System.Collections.Generic;

namespace SystemApi.Controllers
{
    public class CpuService
    {
        public Cpu[] GetCpus()
        {
            return Cpu.Discover();
        }

        public Cpu GetCpu(int id)
        {
            var cpus = GetCpus();

            if(id < cpus.Length)
                return cpus[id];
            else
                throw new System.Exception($"CPU with id '{id}' does not exist.");
            
        }
        public CpuTempModel GetCpuTempModel(int id)
        {
            List<CpuCoreTempModel> cpuTemps = new List<CpuCoreTempModel>();
            Cpu cpu = GetCpu(id);

            foreach (var temp in GetCpu(id).CoreTemperatures)
            {
                cpuTemps.Add(new CpuCoreTempModel()
                {
                    Index = temp.Index,
                    Name = temp.Name,
                    Temperature = temp.Value
                });
            }

            return new CpuTempModel()
            {
                PackageTemperature = cpu.PackageTemperature.Value,
                CoreTemperatures = cpuTemps
            };
        }
    }

    public class CpuTempModel
    {
        public float? PackageTemperature { get; set; }
        public List<CpuCoreTempModel> CoreTemperatures { get; set; }
    }

    public class CpuCoreTempModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public float? Temperature { get; set; }
    }
}