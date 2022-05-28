using System;
using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogController()
        {
            var repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repo);
        }

        //Get on ID
        [HttpGet]
        [Route("loginslog/{id}")]
       
        [ProducesResponseType(200, Type = typeof(SecurityLoginsLogPoco))]
        public ActionResult GetSecurityLoginLog(Guid id)
        {
            SecurityLoginsLogPoco poco = _logic.Get(id);
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
        [Route("loginslog")]
        
        [ProducesResponseType(200, Type = typeof(List<SecurityLoginsLogPoco>))]
        public ActionResult GetAllSecurityLoginLog()
        {
            List<SecurityLoginsLogPoco> pocos = _logic.GetAll();
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
        [Route("loginslog")]
        public ActionResult PostSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] securityLoginsLogPocos)
        {
            _logic.Add(securityLoginsLogPocos);
            return Ok();
        }

        
        [Route("loginslog")]
        public ActionResult PutSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] securityLoginsLogPocos)
        {
            _logic.Update(securityLoginsLogPocos);
            return Ok();
        }

       
        [HttpDelete]
        [Route("loginslog")]
        public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] securityLoginsLogPocos)
        {
            _logic.Delete(securityLoginsLogPocos);
            return Ok();
        }

    }
}