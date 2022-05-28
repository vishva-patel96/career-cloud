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
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleController()
        {
            var repo = new EFGenericRepository<SecurityRolePoco>();
            _logic = new SecurityRoleLogic(repo);
        }

        //Get on ID
        [HttpGet]
        [Route("role/{id}")]
        
        [ProducesResponseType(200, Type = typeof(SecurityRolePoco))]
        public ActionResult GetSecurityRole(Guid id)
        {
            SecurityRolePoco poco = _logic.Get(id);
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
        [Route("role")]
        
        [ProducesResponseType(200, Type = typeof(List<SecurityRolePoco>))]
        public ActionResult GetAllSecurityLoginsRole()
        {
            List<SecurityRolePoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
            
                return NotFound();
            }
            else
            {
                
                return Ok(pocos);
            }

        }

       
        [HttpPost]
        [Route("role")]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] securityRolePocos)
        {
            _logic.Add(securityRolePocos);
            return Ok();
        }

       
        [HttpPut]
        [Route("role")]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] securityRolePocos)
        {
            _logic.Update(securityRolePocos);
            return Ok();
        }

       
        [HttpDelete]
        [Route("role")]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] securityRolePocos)
        {
            _logic.Delete(securityRolePocos);
            return Ok();
        }

    }
}