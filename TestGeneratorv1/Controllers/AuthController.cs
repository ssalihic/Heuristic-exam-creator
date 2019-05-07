using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestGeneratorv1.Services;

using TestGeneratorv1.Helpers;
using TestGeneratorv1.ViewModel;
using System.Collections.Generic;

namespace TestGeneratorv1.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;
        private readonly TestGeneratorContext context;
        public AuthController(TestGeneratorContext _context, UserManager<User> userManager,
                              SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration, JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _jwtService = jwtService;
            _jwtService = new JwtService(configuration);
            context = _context;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginRequest)
        {
            // check validity of request
            if (loginRequest.Email == "" || loginRequest.Email == null)
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Email cannot be empty."
                });
            }
            if (!Validator.IsEmailValid(loginRequest.Email))
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Email is not valid."
                });
            }
            if (loginRequest.Password == "" || loginRequest.Password == null)
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Password cannot be empty."
                });
            }

            // check if user exists in db
            try
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);
                if (result.Succeeded)
                {
                    User appUser = _userManager.Users.SingleOrDefault(r => r.Email == loginRequest.Email);
                    User userSubjects = _userManager.Users.OfType<User>().Select(k => new User
                    {
                        Email = k.Email,
                        Id = k.Id,
                        Subjects = k.Subjects,
                        Name = k.Name,
                        UserSubjects = new List<UserSubjects>(k.UserSubjects
                                                   .Select(j => new UserSubjects
                                                   {
                                                       Subject = (new Subject
                                                       {
                                                           SubjectId = j.Subject.SubjectId,
                                                           SubjectName = j.Subject.SubjectName,
                                                           Areas = new List<Area>(j.Subject.Areas.Select(m => new Area
                                                           {
                                                               AreaId = m.AreaId,
                                                               AreaName = m.AreaName,
                                                               YearOfStudy = m.YearOfStudy
                                                           }))

                                                       })
                                                   }
                                                       )

                                                   )

                    }).SingleOrDefault(r => r.Email == loginRequest.Email);
                    var helpListForSubjects = new List<Subject>();

                    foreach (var i in userSubjects.UserSubjects)
                        helpListForSubjects.Add(i.Subject);

                    userSubjects.Subjects = helpListForSubjects;
                    var token = _jwtService.GenerateJwtToken(loginRequest.Email, appUser);
                    var help = context.UserTokens.Where(x => x.UserId == appUser.Id).SingleOrDefault();
                    if (help != null)
                    {
                        context.UserTokens.Remove(help);
                        context.SaveChanges();

                    }
                    context.UserTokens.Add(new IdentityUserToken<string>()
                        {
                            UserId = appUser.Id,
                            LoginProvider = "Locale",
                            Name = "jwt",
                            Value = token
                        });
                        context.SaveChanges();
                    

                    return Ok(new
                    {
                        status = true,
                        token = token,
                        userSubjects = userSubjects,
                        roleAppAdmin = await _userManager.IsInRoleAsync(userSubjects, "Admin"),
                        roleSystemAdmin = await _userManager.IsInRoleAsync(userSubjects, "SystemAdmin")
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = false,
                    message = e.Message
                });
            }

            return BadRequest(new
            {
                status = false,
                message = "Wrong email or password."
            });
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> LogOut(string userId)
        {
            var result = context.UserTokens.Where(x => x.UserId == userId).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.UserTokens.Remove(result);
            context.SaveChanges();
            return Ok(result);
        }
        [Route("Register")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (registerViewModel.Email == "" || registerViewModel.Email == null)
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Email cannot be empty."
                });
            }
            if (!Validator.IsEmailValid(registerViewModel.Email))
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Email must be valid."
                });
            }
            // check if that email already exists in db
            User user = _userManager.Users.SingleOrDefault(usr => usr.Email == registerViewModel.Email);
            if (user != null)
            {
                return BadRequest(new
                {
                    status = true,
                    message = "User with this email already exists."
                });
            }

            // check passwords
            if (registerViewModel.Password != registerViewModel.ConfirmPassword)
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Provided passwords must match."
                });
            }

            foreach (var i in registerViewModel.Subjects)
                context.Entry(i).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;


            // create the user and return the token so the user can be logged in


            User userToCreate = new User
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email,
                Subjects = registerViewModel.Subjects,
                Name = registerViewModel.Name
            };
            userToCreate.UserSubjects = new List<UserSubjects>();
            foreach (var i in registerViewModel.Subjects)
                userToCreate.UserSubjects.Add(new UserSubjects(i));
            IdentityResult result = await _userManager.CreateAsync(userToCreate, registerViewModel.Password);

            if (result.Succeeded)
            {
                if (registerViewModel.AppAdmin == true)
                {
                    //Create a role !!!
                    //   await _signInManager.SignInAsync(userToCreate, false);
                    var roleExist = await _roleManager.RoleExistsAsync("Admin");
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database: Question 1
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));

                    }
                    var result1 = await _userManager.AddToRoleAsync(userToCreate, "Admin");

                }
                return Ok(new
                {
                    status = true,
                    token = _jwtService.GenerateJwtToken(registerViewModel.Email, userToCreate)
                });
            }
            string errorMessage = "";
            foreach (IdentityError error in result.Errors)
            {
                errorMessage += error.Description + Environment.NewLine;
            }
            return BadRequest(new
            {
                status = false,
                message = errorMessage
            });
        }

        [Route("RegisterAdmin")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (registerViewModel.Email == "" || registerViewModel.Email == null)
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Email cannot be empty."
                });
            }
            if (!Validator.IsEmailValid(registerViewModel.Email))
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Email must be valid."
                });
            }
            // check if that email already exists in db
            User user = _userManager.Users.SingleOrDefault(usr => usr.Email == registerViewModel.Email);
            if (user != null)
            {
                return BadRequest(new
                {
                    status = true,
                    message = "User with this email already exists."
                });
            }

            // check passwords
            if (registerViewModel.Password != registerViewModel.ConfirmPassword)
            {
                return BadRequest(new
                {
                    status = false,
                    message = "Provided passwords must match."
                });
            }

            foreach (var i in registerViewModel.Subjects)
                context.Entry(i).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;


            // create the user and return the token so the user can be logged in
            User userToCreate = new User
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email,
                Subjects = registerViewModel.Subjects,
                Name = registerViewModel.Name
            };
            IdentityResult result = await _userManager.CreateAsync(userToCreate, registerViewModel.Password);

            if (result.Succeeded)
            {

                //Create a role !!!
                //   await _signInManager.SignInAsync(userToCreate, false);
                var roleExist = await _roleManager.RoleExistsAsync("SystemAdmin");
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("SystemAdmin"));

                }
                var result1 = await _userManager.AddToRoleAsync(userToCreate, "SystemAdmin");


                return Ok(new
                {
                    status = true,
                    token = _jwtService.GenerateJwtToken(registerViewModel.Email, userToCreate)
                });
            }
            string errorMessage = "";
            foreach (IdentityError error in result.Errors)
            {
                errorMessage += error.Description + Environment.NewLine;
            }
            return BadRequest(new
            {
                status = false,
                message = errorMessage
            });
        }
    }
}