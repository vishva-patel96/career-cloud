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
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileController()
        {
            var repo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repo);
        }

        
        [HttpGet]
        [Route("profile/{id}")]
        
        [ProducesResponseType(200, Type = typeof(CompanyProfilePoco))]
        public ActionResult GetCompanyProfile(Guid id)
        {
            CompanyProfilePoco poco = _logic.Get(id);
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
        [Route("profile")]
        
        [ProducesResponseType(200, Type = typeof(List<CompanyProfilePoco>))]
        public ActionResult GetAllCompanyProfile()
        {
            List<CompanyProfilePoco> pocos = _logic.GetAll();
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
        [Route("profile")]
        public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] companyProfilePocos)
        {
            _logic.Add(companyProfilePocos);
            return Ok();
        }

       
        [HttpPut]
        [Route("profile")]
        public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] companyProfilePocos)
        {
            _logic.Update(companyProfilePocos);
            return Ok();
        }

       
        [HttpDelete]
        [Route("profile")]
        public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] companyProfilePocos)
        {
            _logic.Delete(companyProfilePocos);
            return Ok();
        }

    }
}