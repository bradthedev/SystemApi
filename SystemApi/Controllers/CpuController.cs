using HardwareProviders.CPU;
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using SystemApi.Enums;

namespace SystemApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CpuController : ApiController
    {
        CpuService _cpuService;

        public CpuController()
        {
            _cpuService = new CpuService();
        }

        [Route("api/cpu")]
        // GET api/values
        public IHttpActionResult Get()
        {
            try
            {
                Cpu[] cpu = _cpuService.GetCpus();

                return Ok(new
                {
                    Status = ApiStatus.Success.ToString(),
                    Result = cpu
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = ApiStatus.Fail.ToString(),
                    Error = ex.Message
                });
            }
        }
            

        [Route("api/cpu/{id}")]
        // GET api/values
        public IHttpActionResult GetCpuById(int id)
        {
            try
            {
                Cpu cpu = _cpuService.GetCpu(id);

                return Ok(new
                {
                    Result = cpu,
                    Status = ApiStatus.Success.ToString()
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = ApiStatus.Fail.ToString(),
                    Error = ex.Message
                });
            }
        }

        [Route("api/cpu/{id}/temperatures")]
        public IHttpActionResult GetTemperatures(int id)
        {
            try
            {
                var res = _cpuService.GetCpuTempModel(id);
                return Ok(new
                {
                    Result = res,
                    Status = ApiStatus.Success.ToString()
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = ApiStatus.Fail.ToString(),
                    Error = ex.Message
                });
            }
        }
    }
}
