using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamForms.ViewModel.Account
{
    public class SignInAndSignUpViewModel
    {
        public SignInAndSignUpViewModel()
        {
            SignInViewModel = new SignInViewModel();
            SignUpViewModel = new SignUpViewModel();
        }
        public string? Message { get; set; }
        public string? Email { get; set; }
        public string? ReturnUrl { get; set; }
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
        public SignInViewModel? SignInViewModel { get; set; }
        public SignUpViewModel? SignUpViewModel { get; set; }
    }
}
