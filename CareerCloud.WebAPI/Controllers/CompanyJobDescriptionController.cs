using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobDescriptionController(CompanyJobDescriptionLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route("jobsdescription/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCompanyJobDescription(Guid id)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(id);
            if (poco == null)
            {
                return null;
            }
            return Ok(poco);

        }
        [HttpGet]
        [Route("jobsdescription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult GetAllCompanyJobDescription()
        {
            List<CompanyJobDescriptionPoco> pocos = _logic.GetAll();
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
        [Route("jobsdescription")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] companyJobDescriptionPoco)
        {
            _logic.Add(companyJobDescriptionPoco);
            return Ok();

        }
        [HttpPut]
        [Route("jobsdescription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PutCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] companyJobDescriptionPoco)
        {
            _logic.Update(companyJobDescriptionPoco);
            return Ok();

        }
        [HttpDelete]
        [Route("jobsdescription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteCompanyJobDescription([FromBody] CompanyJobDescriptionPoco[] companyJobDescriptionPocos)
        {
            _logic.Delete(companyJobDescriptionPocos);
            return Ok();
        }

    }
}
