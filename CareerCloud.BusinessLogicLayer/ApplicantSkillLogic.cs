using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repo) : base(repo)
        {

        }
        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (ApplicantSkillPoco poco in pocos)
            {
                
                if (poco.StartMonth > 12)
                {
                    errors.Add(new ValidationException(101, "Cannot be greater than 12"));
                }
                if (poco.EndMonth > 12)
                {
                    errors.Add(new ValidationException(102, "Cannot be greater than 12"));
                }
                if (poco.StartYear < 1900)
                {
                    errors.Add(new ValidationException(103, "Cannot be less then 1900"));
                }
                if (poco.EndYear < poco.StartYear)
                {
                    errors.Add(new ValidationException(104, "Cannot be less then StartYear"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }

        }
    }
}
