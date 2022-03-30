using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repo) : base(repo)
        {

        }
        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (CompanyLocationPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CountryCode))
                {
                    errors.Add(new ValidationException(500, "CountryCode cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Province))
                {
                    errors.Add(new ValidationException(501, "Province cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Street))
                {
                    errors.Add(new ValidationException(502, "Street cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.City))
                {
                    errors.Add(new ValidationException(503, "City cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.PostalCode))
                {
                    errors.Add(new ValidationException(504, "PostalCode cannot be empty"));
                }

                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }

        }
    }
}
