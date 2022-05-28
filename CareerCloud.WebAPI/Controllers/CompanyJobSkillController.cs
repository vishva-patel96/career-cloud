using System;
using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillController()
        {
            var repo = new EFGenericRepository<CompanyJobSkillPoco>();
            _logic = new CompanyJobSkillLogic(repo);
        }

        //Get on ID
        [HttpGet]
        [Route("jobskill/{id}")]
        //https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1
        [ProducesResponseType(200, Type = typeof(CompanyJobSkillPoco))]
        public ActionResult GetCompanyJobSkill(Guid id)
        {
            CompanyJobSkillPoco poco = _logic.Get(id);
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
        [Route("jobskill")]
        
        [ProducesResponseType(200, Type = typeof(List<CompanyJobSkillPoco>))]
        public ActionResult GetAllCompanyJobSkill()
        {
            List<CompanyJobSkillPoco> pocos = _logic.GetAll();
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
        [Route("jobskill")]
        public ActionResult PostCompanyJobSkill([FromBody] CompanyJobSkillPoco[] companyJobSkillPocos)
        {
            _logic.Add(companyJobSkillPocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("jobskill")]
        public ActionResult PutCompanyJobSkill([FromBody] CompanyJobSkillPoco[] companyJobSkillPocos)
        {
            _logic.Update(companyJobSkillPocos);
            return Ok();
        }

       
        [HttpDelete]
        [Route("jobskill")]
        public ActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] companyJobSkillPocos)
        {
            _logic.Delete(companyJobSkillPocos);
            return Ok();
        }

    }
}