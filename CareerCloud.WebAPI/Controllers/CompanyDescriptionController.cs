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
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _logic = new CompanyDescriptionLogic(repo);


        }


        [HttpGet]
        [Route("description/{id}")]
        
        [ProducesResponseType(200, Type = typeof(CompanyDescriptionPoco))]
        public ActionResult GetCompanyDescription(Guid id)
        {
            CompanyDescriptionPoco poco = _logic.Get(id);
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
        [Route("description")]
        
        [ProducesResponseType(200, Type = typeof(List<CompanyDescriptionPoco>))]
        public ActionResult GetAllCompanyDescription()
        {
            List<CompanyDescriptionPoco> pocos = _logic.GetAll();
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
        [Route("description")]
        public ActionResult PostCompanyDescription([FromBody] CompanyDescriptionPoco[] companyDescriptionPocos)
        {
            _logic.Add(companyDescriptionPocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("description")]
        public ActionResult PutCompanyDescription([FromBody] CompanyDescriptionPoco[] companyDescriptionPocos)
        {
            _logic.Update(companyDescriptionPocos);
            return Ok();
        }

        
        [HttpDelete]
        [Route("workhistory")]
        public ActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[] companyDescriptionPocos)
        {
            _logic.Delete(companyDescriptionPocos);
            return Ok();
        }

    }
}