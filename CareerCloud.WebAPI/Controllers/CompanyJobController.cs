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
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobController()
        {
            var repo = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repo);


        }

        //Get on ID
        [HttpGet]
        [Route("job/{id}")]
        
        [ProducesResponseType(200, Type = typeof(CompanyJobPoco))]
        public ActionResult GetCompanyJob(Guid id)
        {
            CompanyJobPoco poco = _logic.Get(id);
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
        [Route("job")]
        
        [ProducesResponseType(200, Type = typeof(List<CompanyJobPoco>))]
        public ActionResult GetAllCompanyJob()
        {
            List<CompanyJobPoco> pocos = _logic.GetAll();
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
        [Route("job")]
        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] companyJobPocos)
        {
            _logic.Add(companyJobPocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("job")]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] companyJobPocos)
        {
            _logic.Update(companyJobPocos);
            return Ok();
        }

       
        [HttpDelete]
        [Route("job")]
        public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] companyJobPocos)
        {
            _logic.Delete(companyJobPocos);
            return Ok();
        }

    }
}