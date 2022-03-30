using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repo) : base(repo)
        {

        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> errors = new List<ValidationException>();
            foreach (CompanyProfilePoco poco in pocos)

            {
                string[] domain = { ".ca", ".com", ".biz" };

                if (domain.Any(x => poco.CompanyWebsite?.EndsWith(x) == false))
                {
                    errors.Add(new ValidationException(600, $"CompanyWebsite for CompanyProfile {poco.CompanyWebsite} must end with the following extensions – " + ".ca" + "," + ".com" + "," + ".biz" + "."));
                }

                string[] PhoneNumber = poco.ContactPhone?.Split('-');
                if (PhoneNumber.Length < 3)
                {
                    errors.Add(new ValidationException(601, "Must correspond to a valid phone number"));
                }
                else
                {
                    if(PhoneNumber?[0].Length != 3)
                    {
                        errors.Add(new ValidationException(601, "Must correspond to a valid phone number"));
                    }
                    else if (PhoneNumber?[1].Length != 3)
                            {
                        errors.Add(new ValidationException(601, "Must correspond to a valid phone number"));
                    }
                    else if (PhoneNumber?[2].Length != 4)
                    {
                        errors.Add(new ValidationException(601, "Must correspond to a valid phone number"));
                    }
                }

            
                if (errors.Count > 0)
                {
                    throw new AggregateException(errors);
                }
            }

        }
    }
}
