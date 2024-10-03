using AutoMapper;
using ExamForms.Models.Accounts;
using ExamForms.ViewModel.Account;

namespace ExamForms.Configure;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
    }
}
