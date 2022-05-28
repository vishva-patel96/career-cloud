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
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginController()
        {
            var repo = new EFGenericRepository<SecurityLoginPoco>();
            _logic = new SecurityLoginLogic(repo);
        }

        //Get on ID
        [HttpGet]
        [Route("login/{id}")]
       
        [ProducesResponseType(200, Type = typeof(SecurityLoginPoco))]
        public ActionResult GetSecurityLogin(Guid id)
        {
            SecurityLoginPoco poco = _logic.Get(id);
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

       
        [HttpGet]
        [Route("login")]
       
        [ProducesResponseType(200, Type = typeof(List<SecurityLoginPoco>))]
        public ActionResult GetAllSecurityLogin()
        {
            List<SecurityLoginPoco> pocos = _logic.GetAll();
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
        [Route("login")]
        public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] securityLoginPocos)
        {
            _logic.Add(securityLoginPocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("login")]
        public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] securityLoginPocos)
        {
            _logic.Update(securityLoginPocos);
            return Ok();
        }

        
        [HttpDelete]
        [Route("login")]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] securityLoginPocos)
        {
            _logic.Delete(securityLoginPocos);
            return Ok();
        }

    }
}