using FluentValidation.Attributes;
using BizContacts.API.ViewModel.Validations;

[Validator(typeof(CredentialsViewModelValidator))]
public class CredentialsViewModel  
{
        public string UserName { get; set; }
        public string Password { get; set; }        
}