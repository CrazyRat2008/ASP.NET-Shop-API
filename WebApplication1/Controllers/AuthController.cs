using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WebApplication1.Abstract;
using WebApplication1.Constants;
using WebApplication1.Data.Entities.Identety;
using WebApplication1.Data.Model.Auth;

namespace WebApplication1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService; 
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<UserEntity> userManager, IConfiguration conf, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _configuration = conf;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            String imageName = string.Empty;
            if (model.Image != null)
            {
                var fileExp = Path.GetExtension(model.Image.FileName);
                var dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images");
                imageName = Path.GetRandomFileName() + fileExp;
                using (var ms = new MemoryStream())
                {
                    await model.Image.CopyToAsync(ms);
                    var bmp = new Bitmap(Image.FromStream(ms));
                    string[] sizes = ((string)_configuration.GetValue<string>("ImageSizes")).Split(" ");
                    foreach (var s in sizes)
                    {
                        int size = Convert.ToInt32(s);
                        var saveImage = ImageWorker.CompressImage(bmp, size, size, false);
                        saveImage.Save(Path.Combine(dirSave, s + "_" + imageName));
                    }
                }
            }
            var user = new UserEntity()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                Image = imageName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, Roles.Admin);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return BadRequest("Не вірно вказані дані");
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                    return BadRequest("Не вірно вказані дані");
                var token = _jwtTokenService.CreateToken(user);
                return Ok(new { token });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
