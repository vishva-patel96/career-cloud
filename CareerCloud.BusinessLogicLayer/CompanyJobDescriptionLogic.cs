using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repo) : base(repo)
        {

        }
        public override void Update(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        public override void Add(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        protected override void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (CompanyJobDescriptionPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.JobName))
                {
                    errors.Add(new ValidationException(300, "JobName cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.JobDescriptions))
                {
                    errors.Add(new ValidationException(301, "JobDescriptions cannot be empty"));
                }


                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }

        }
    }
}
