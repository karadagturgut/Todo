using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;
using Todo.Core.DTO.OrganizationParticipation;
using Todo.Core.Entity;
using Todo.Core.Interface.Service.OrganizationParticipation;

namespace Todo.Service.OrganizationParticipation
{
    public class OrganizationParticipationService : IOrganizationParticipationService
    {
        private readonly IGenericRepository<OrganizationParticipationRequest> _participationRepository;
        private readonly IGenericRepository<Organization> _organizationRepository;
        private readonly IGenericRepository<TodoUser> _userRepository;

        public OrganizationParticipationService(IGenericRepository<OrganizationParticipationRequest> participationRepository, IGenericRepository<Organization> organizationRepository, IGenericRepository<TodoUser> userRepository)
        {
            _participationRepository = participationRepository;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
        }

        public ApiResponseDTO DecideUserJoinStatus(DecideUserJoinStatusDTO model)
        {
            var existingRecord = _participationRepository.GetById(model.ParticipationRequestId);
            if (!existingRecord.IsSuccess || existingRecord.Data is null) { return ApiResponseDTO.Failed("Kayıt bulunamadı."); }

            if (!model.Status)
            {
                var deleteResponse = _participationRepository.DeleteById(model.ParticipationRequestId);
                return ApiResponseDTO.Success(deleteResponse.Data, "Davet reddedildi.");
            }
            else
            {
                var user = existingRecord.Data.User;
                user.OrganizationId = existingRecord.Data.OrganizationId;
                var updateResponse = _userRepository.Update(user);
                return ApiResponseDTO.Success(updateResponse.Data, "Davet onaylandı.");
            }
        }

        public ApiResponseDTO GetAll(JoinOrganizationRequestDTO model)
        {
            throw new NotImplementedException();
        }

        public ApiResponseDTO OrganizationJoinRequest(JoinOrganizationRequestDTO model)
        {
            var existingRecord = _organizationRepository.GetById(model.OrganizationId);
            if (!existingRecord.IsSuccess || existingRecord.Data is null) { return ApiResponseDTO.Failed("Kayıt bulunamadı."); }

            var result = _participationRepository.Add(new() { OrganizationId = model.OrganizationId, UserId = model.UserId });
            if (!result.IsSuccess)
                return ApiResponseDTO.Failed("Katılım isteği gönderilirken hata oluştu.");

            return ApiResponseDTO.Success(result.IsSuccess, "Katılım isteği gönderildi.");
        }

    }
}
