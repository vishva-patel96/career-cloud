using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantEducationLogic _logic;

        public ApplicantProfileController(ApplicantProfileLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route("profile/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetApplicantProfile(Guid id)
        {
            ApplicantProfilePoco poco = _logic.Get(id);
            if (poco == null)
            {
                return null;
            }
            return Ok(poco);

        }
        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult GetAllApplicantProfile()
        {
            List<ApplicantProfilePoco> pocos = _logic.GetAll();
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
        [Route("profile")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostApplicantProfile([FromBody] ApplicantProfilePoco[] applicantProfilePoco)
        {
            _logic.Add(applicantProfilePoco);
            return Ok();

        }
        [HttpPut]
        [Route("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PutApplicantProfile([FromBody] ApplicantProfilePoco[] applicantProfilePocos)
        {
            _logic.Update(applicantProfilePocos);
            return Ok();

        }
        [HttpDelete]
        [Route("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] applicantProfilePocos)
        {
            _logic.Delete(applicantProfilePocos);
            return Ok();
        }


    }
}
