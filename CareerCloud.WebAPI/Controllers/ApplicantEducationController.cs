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
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _logic;
        public ApplicantEducationController()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);


        }

        
        [HttpGet]
        [Route("education/{id}")]
        
        [ProducesResponseType(200, Type = typeof(ApplicantEducationPoco))]
        public ActionResult GetApplicantEducation(Guid id)
        {
            ApplicantEducationPoco poco = _logic.Get(id);
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
        [Route("education")]
        
        [ProducesResponseType(200, Type = typeof(List<ApplicantEducationPoco>))]
        public ActionResult GetAllApplicantEducation()
        {
            List<ApplicantEducationPoco> pocos = _logic.GetAll();
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
        [Route("education")]
        public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] applicantEducationPocos)
        {
            _logic.Add(applicantEducationPocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("education")]
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] applicantEducationPocos)
        {
            _logic.Update(applicantEducationPocos);
            return Ok();
        }

        
        [HttpDelete]
        [Route("education")]
        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] applicantEducationPocos)
        {
            _logic.Delete(applicantEducationPocos);
            return Ok();
        }




    }
}