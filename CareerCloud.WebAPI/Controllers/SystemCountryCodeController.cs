using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeController()
        {
            
            EFGenericRepository<SystemCountryCodePoco> systemCountryCodeRepository = new EFGenericRepository<SystemCountryCodePoco>();
            _logic = new SystemCountryCodeLogic(systemCountryCodeRepository);
        }

        
        [HttpGet]
        [Route("countrycode/{id}")]
      
        [ProducesResponseType(200, Type = typeof(SystemCountryCodePoco))]
        public ActionResult GetSystemCountryCode(string id)
        {
            SystemCountryCodePoco poco = _logic.Get(id);
            if (poco == null)
            {
                //404
                return NotFound();
            }
            else
            {
                //200
                return Ok(poco);
            }
        }

        //Get All
        [HttpGet]
        [Route("countrycode")]
        
        [ProducesResponseType(200, Type = typeof(List<SystemCountryCodePoco>))]
        public ActionResult GetAllSystemCountryCode()
        {
            List<SystemCountryCodePoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                //404
                return NotFound();
            }
            else
            {
                //200
                return Ok(pocos);
            }

        }

       
        [HttpPost]
        [Route("countrycode")]
        public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco[] systemCountryCodePocos)
        {
            _logic.Add(systemCountryCodePocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("countrycode")]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] systemCountryCodePocos)
        {
            _logic.Update(systemCountryCodePocos);
            return Ok();
        }

        
        [HttpDelete]
        [Route("countrycode")]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] systemCountryCodePocos)
        {
            _logic.Delete(systemCountryCodePocos);
            return Ok();
        }

    }
}