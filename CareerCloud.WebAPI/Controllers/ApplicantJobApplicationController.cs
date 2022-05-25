using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationController(ApplicantJobApplicationLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route("jobapplication/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetApplicantJobApplication(Guid id)
        {
            ApplicantJobApplicationPoco poco = _logic.Get(id);
            if (poco == null)
            {
                return null;
            }
            return Ok(poco);

        }
        [HttpGet]
        [Route("jobapplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult GetAllApplicantJobApplication()
        {
            List<ApplicantJobApplicationPoco> pocos = _logic.GetAll();
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
        [Route("jobapplication")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] ApplicantJobApplicationPoco)
        {
            _logic.Add(ApplicantJobApplicationPoco);
            return Ok();

        }
        [HttpPut]
        [Route("jobapplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] ApplicantJobApplicationPoco)
        {
            _logic.Update(ApplicantJobApplicationPoco);
            return Ok();

        }
        [HttpDelete]
        [Route("jobapplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] ApplicantJobApplicationPocos)
        {
            _logic.Delete(ApplicantJobApplicationPocos);
            return Ok();
        }


    }
}
