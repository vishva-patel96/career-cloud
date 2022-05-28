using System;
using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationController()
        {
            var repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logic = new ApplicantJobApplicationLogic(repo);


        }

       
        [HttpGet]
        [Route("jobapplication/{id}")]
        
        [ProducesResponseType(200, Type = typeof(ApplicantJobApplicationPoco))]
        public ActionResult GetApplicantJobApplication(Guid id)
        {
            ApplicantJobApplicationPoco poco = _logic.Get(id);
            if (poco == null)
            {
                
                return NotFound();
            }
            else
            {
                
                return Ok(poco);
            }
        }

  
        [HttpGet]
        [Route("jobapplication")]
        
        [ProducesResponseType(200, Type = typeof(List<ApplicantJobApplicationPoco>))]
        public ActionResult GetAllApplicantJobApplication()
        {
            List<ApplicantJobApplicationPoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
               
                return NotFound();
            }
            else
            {
                //200
                return Ok(pocos);
            }

        }

        
        [HttpPost]
        [Route("jobapplication")]
        public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] applicantJobApplicationPocos)
        {
            _logic.Add(applicantJobApplicationPocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("jobapplication")]
        public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] applicantJobApplicationPocos)
        {
            _logic.Update(applicantJobApplicationPocos);
            return Ok();
        }

        
        [HttpDelete]
        [Route("jobapplication")]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] applicantJobApplicationPocos)
        {
            _logic.Delete(applicantJobApplicationPocos);
            return Ok();
        }




    }
}