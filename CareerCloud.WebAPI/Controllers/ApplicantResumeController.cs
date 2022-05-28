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
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic _logic;

        public ApplicantResumeController()
        {
            var repo = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repo);


        }

        
        [HttpGet]
        [Route("resume/{id}")]
        
        [ProducesResponseType(200, Type = typeof(ApplicantResumePoco))]
        public ActionResult GetApplicantResume(Guid id)
        {
            ApplicantResumePoco poco = _logic.Get(id);
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
        [Route("resume")]
        
        [ProducesResponseType(200, Type = typeof(List<ApplicantResumePoco>))]
        public ActionResult GetAllApplicantResume()
        {
            List<ApplicantResumePoco> pocos = _logic.GetAll();
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
        [Route("resume")]
        public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] applicantResumePocos)
        {
            _logic.Add(applicantResumePocos);
            return Ok();
        }

        
        [HttpPut]
        [Route("resume")]
        public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] applicantResumePocos)
        {
            _logic.Update(applicantResumePocos);
            return Ok();
        }

        
        [HttpDelete]
        [Route("resume")]
        public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] applicantResumePocos)
        {
            _logic.Delete(applicantResumePocos);
            return Ok();
        }




    }
}