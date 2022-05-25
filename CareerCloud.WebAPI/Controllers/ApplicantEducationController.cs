using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/education/v1")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationController(ApplicantEducationLogic logic)
        {
            _logic = logic;
        }

      [HttpGet]
      [Route("education/{applicantEducationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetApplicantEducation(Guid id)
        {
            ApplicantEducationPoco poco = _logic.Get(id);
            if(poco == null)
            {
                return null;
            }
            return Ok(poco);

        }
        [HttpGet]
        [Route("education")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult GetAllApplicantEducation()
        {
            List<ApplicantEducationPoco> pocos = _logic.GetAll();
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
        [Route("education")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] applicantEducationPoco)
        {
            _logic.Add(applicantEducationPoco);
            return Ok();

        }
        [HttpPut]
        [Route("education")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] applicantEducationPoco)
        {
            _logic.Update(applicantEducationPoco);
            return Ok();

        }
        [HttpDelete]
        [Route("education")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] applicantEducationPocos)
        {
            _logic.Delete(applicantEducationPocos);
            return Ok();
        }


    }
}
