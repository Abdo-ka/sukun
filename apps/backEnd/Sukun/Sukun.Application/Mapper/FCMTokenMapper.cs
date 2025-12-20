using Sukun.Application.Dtos.FCMToken.Request;
using Sukun.Application.Dtos.FCMToken.Response;
using Sukun.Domin.Entities;

namespace Sukun.Application.Mapper
{
    public static class FCMTokenMapper
    {
        public static FCMTokenResponseDto ToResponseDto(FCMToken token)
        {
            return new FCMTokenResponseDto
            {
                Id = token.Id,
                UserDeviceId = token.UserDeviceId,
                Token = token.Token,
                LastUsedAt = token.LastUsedAt,
                IsActive = token.IsActive,
                CreateAt = token.CreateAt,
                UpdateAt = token.UpdatedAt
            };
        }
        public static FCMToken FromCreateDto(FCMTokenCreateDto dto)
        {
            return new FCMToken
            {
                UserDeviceId = dto.UserDeviceId,
                Token = dto.Token,
                IsActive = true,
                CreateAt = DateTime.UtcNow,
                LastUsedAt = DateTime.UtcNow,

            };
        }
    }
}
