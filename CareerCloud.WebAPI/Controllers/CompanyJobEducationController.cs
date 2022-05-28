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
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _logic;

        public CompanyJobEducationController()
        {
            var repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repo);


        }

        
        [HttpGet]
        [Route("jobeducation/{id}")]
        
        [ProducesResponseType(200, Type = typeof(CompanyJobEducationPoco))]
        public ActionResult GetCompanyJobEducation(Guid id)
        {
            CompanyJobEducationPoco poco = _logic.Get(id);
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
        [Route("jobeducation")]
       
        [ProducesResponseType(200, Type = typeof(List<CompanyJobEducationPoco>))]
        public ActionResult GetAllCompanyJobEducation()
        {
            List<CompanyJobEducationPoco> pocos = _logic.GetAll();
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
        [Route("jobeducation")]
        public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] companyJobEducationPocos)
        {
            _logic.Add(companyJobEducationPocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("jobeducation")]
        public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] companyJobEducationPocos)
        {
            _logic.Update(companyJobEducationPocos);
            return Ok();
        }

        
        [HttpDelete]
        [Route("jobeducation")]
        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] companyJobEducationPocos)
        {
            _logic.Delete(companyJobEducationPocos);
            return Ok();
        }

    }
}