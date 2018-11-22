using AutoMapper;
using BizContacts.DAL;

namespace BizContacts.API.ViewModel.Validations.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
              CreateMap<RegistrationViewModel,BizContactIdentity>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}