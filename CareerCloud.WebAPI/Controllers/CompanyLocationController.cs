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
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationController()
        {
            var repo = new EFGenericRepository<CompanyLocationPoco>();
            _logic = new CompanyLocationLogic(repo);
        }

        //Get on ID
        [HttpGet]
        [Route("location/{id}")]
        
        [ProducesResponseType(200, Type = typeof(CompanyLocationPoco))]
        public ActionResult GetCompanyLocation(Guid id)
        {
            CompanyLocationPoco poco = _logic.Get(id);
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
        [Route("location")]
        
        [ProducesResponseType(200, Type = typeof(List<CompanyLocationPoco>))]
        public ActionResult GetAllCompanyLocation()
        {
            List<CompanyLocationPoco> pocos = _logic.GetAll();
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
        [Route("location")]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] companyLocationPocos)
        {
            _logic.Add(companyLocationPocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("location")]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] companyLocationPocos)
        {
            _logic.Update(companyLocationPocos);
            return Ok();
        }

       
        [HttpDelete]
        [Route("location")]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] companyLocationPocos)
        {
            _logic.Delete(companyLocationPocos);
            return Ok();
        }

    }
}