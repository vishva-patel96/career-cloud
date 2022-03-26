using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repo) : base(repo)
        {

        }
        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (CompanyDescriptionPoco poco in pocos)
            {
               
                if (poco.CompanyDescription.Length > 2)
                {
                    errors.Add(new ValidationException(107, "CompanyDescription must be greater than 2 characters"));
                }
                if (poco.CompanyName.Length > 2)
                {
                    errors.Add(new ValidationException(106, "CompanyName must be greater than 2 characters"));
                }
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }

        }
    }
}
