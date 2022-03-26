using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repo) : base(repo)
        {

        }
        public override void Update(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantJobApplicationPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (ApplicantJobApplicationPoco poco in pocos)
            {
                
                if (poco.ApplicationDate > DateTime.Now)
                {
                    errors.Add(new ValidationException(110, "ApplicationDate Cannot be greater than today"));
                }
               
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }

        }
    }
}
