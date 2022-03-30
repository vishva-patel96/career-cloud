using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantResumeLogic : BaseLogic<ApplicantResumePoco>
    {
        public ApplicantResumeLogic(IDataRepository<ApplicantResumePoco> repo) : base(repo)
        {

        }
        public override void Update(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        public override void Add(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        protected override void Verify(ApplicantResumePoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (ApplicantResumePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Resume))
                {
                    errors.Add(new ValidationException(113, "Resume cannot be empty"));
                }
              
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }

        }
    }
}
