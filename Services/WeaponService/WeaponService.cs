using dotnet_api_first.DTOs.Character;
using dotnet_api_first.DTOs.Weapon;
using dotnet_api_first.models;
using dotnet_api_first.Data;
using AutoMapper;

namespace dotnet_api_first.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContex _contex;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public WeaponService(DataContex context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _contex = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceRespinse<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon)
        {
            var serviceResponse = new ServiceRespinse<GetCharacterDTO>();

            try
            {

            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }
    }
}