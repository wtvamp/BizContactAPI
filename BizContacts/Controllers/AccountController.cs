using Microsoft.AspNetCore.Mvc;
using BizContacts.API.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BizContacts.DAL;
using BizContacts.API.Helpers;

namespace BizContacts.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly BizContactIdentityContext _appDbContext;
        private readonly UserManager<BizContactIdentity> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<BizContactIdentity> userManager, IMapper mapper, BizContactIdentityContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<BizContactIdentity>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _appDbContext.BizContactIdentities.AddAsync(new BizContactIdentity { Id = userIdentity.Id, FirstName = model.FirstName });
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}