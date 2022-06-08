using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static CareerCloud.gRPC.ApplicantEducationService;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationController : ApplicantEducationServiceBase
    {
        private readonly ApplicantEducationLogic _logic;
        public ApplicantEducationController()
        {
            _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
        }

        public override Task<ApplicantEducation> GetApplicantEducation(IdRequest request, ServerCallContext context)
        {
            Guid id = Guid.Parse(request.Id);
            return new Task<ApplicantEducation>(() => TranslateTo(_logic.Get(id)));
        }

        public override Task<Empty> AddApplicantEducation (ApplicantEducations request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
                foreach(var item in request.AppEdu)
            {
                pocos.Add(TranslateFrom(item));
            }
            _logic.Add(pocos.ToArray());
            return Task.FromResult<Empty>(null);

        }

        public override Task<Empty> UpdateApplicantEducation(ApplicantEducations request, ServerCallContext context)
        {
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (var item in request.AppEdu)
            {
                pocos.Add(TranslateFrom(item));
            }
            _logic.Update(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }

        public override Task<Empty> DeleteApplicantEducation(ApplicantEducations request, ServerCallContext context)
        { 
         List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach (var item in request.AppEdu)
            {
                pocos.Add(TranslateFrom(item));
            }
             _logic.Delete(pocos.ToArray());
            return Task.FromResult<Empty>(null);
        }
        public override Task<ApplicantEducations> GetApplicantEducations(Empty request, ServerCallContext context)
        {
            List<ApplicantEducation> protos = new List<ApplicantEducation>();
            foreach(var item in _logic.GetAll())
            {
                protos.Add(TranslateTo(item));
            }
            ApplicantEducations appEdu = new ApplicantEducations();
            appEdu.AppEdu.AddRange(protos);
            
            return new Task<ApplicantEducations> (()=>appEdu);
        }

        private ApplicantEducationPoco TranslateFrom(ApplicantEducation proto)
            
            {
            return new ApplicantEducationPoco()
            {
                Id = Guid.Parse(proto.Id),
                Applicant = Guid.Parse(proto.Applicant),
                CertificateDiploma = proto.CertificateDiploma,
                CompletionPercent = (byte?) proto.CompletionPercent,
                CompletionDate = proto.CompletionDate.ToDateTime(),
                Major = proto.Major,
                StartDate = proto.StartDate.ToDateTime()
            };
        }


        private ApplicantEducation TranslateTo(ApplicantEducationPoco poco)
        {
            return new ApplicantEducation()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                CertificateDiploma = poco.CertificateDiploma,
                CompletionPercent = poco.CompletionPercent,
                CompletionDate = poco.CompletionDate == null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                Major = poco.Major,
                StartDate = poco.StartDate == null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate)

            };
        }
    }
}


    