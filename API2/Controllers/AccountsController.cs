using API2.Base;
using API2.Context;
using API2.Models;
using API2.Repository.Data;
using API2.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace API2.Controllers
{
    [Route("api2/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public readonly MyContext myContext;
        public readonly IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, MyContext myContext, IConfiguration _configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.myContext = myContext;
            this._configuration = _configuration;
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterVM registerVM)
        {
           
                var reg = accountRepository.Register(registerVM);
                if (reg > 0 )
                {
                    return Ok("Sukses, Register Berhasil");
                }
                else if (reg == -1 )
                {
                    return BadRequest("Maaf, Email sudah ada");
                }else if (reg == -2)
                {
                    return BadRequest("Maaf, nomor telepon sudah ada");
                }else
                {
                    return BadRequest("Maaf, email dan nomor telepon sudah ada");
                }
              
         
             
            
        }
        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                var resultLogin = accountRepository.Login(loginVM);

                //var getUserData = repository.GetUserData(login.Email);

                if (resultLogin > 0)
                {
                    var NIK = myContext.Employees.Join(myContext.Accounts, e => e.NIK, a => a.NIK, (e, a) => new
                    {
                        Email = e.Email,
                        NIK = a.NIK
                    }).Where(e => e.Email == loginVM.Email).SingleOrDefault();

                 
                    var cekrole = myContext.Roles.Where(r => r.AccountRoles.Any(ar => ar.AccountNIK == NIK.NIK)).ToList();

                    //var dataRole = new LoginVM()
                    //{
                    //    Email = getUserData.Email,
                    //    Roles = getUserData.Roles
                    //};

                    var claimes = new List<Claim>();

                    claimes.Add(new Claim("Email", NIK.Email));

                    foreach (var item in cekrole)
                    {
                        claimes.Add(new Claim("roles", item.Name));
                    }


                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claimes,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );
                    var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claimes.Add(new Claim("TokenScurity", idToken.ToString()));

                    return Ok(new { status = HttpStatusCode.OK, idToken, message = "Login Success!" });
                }
                else
                {
                    return BadRequest("Email tidak ada");
                }

                //if(resultLogin == 0)
                //{
                //    return NotFound("Email Tidak ditemukan");
                //}else if(resultLogin == -401)
                //{
                //    return BadRequest("Password yang anda Masukkan Salah");
                //}else if(resultLogin == -402)
                //{
                //    return BadRequest("Unknow Eror");
                //}
                //else
                //{
                //    return Ok("Login Berhasil");
                //}
            }
            catch (Exception)
            {

                return BadRequest("Eror Login");
            }
            //if (loginVM != null)
            //{
            //    var log = accountRepository.Login(loginVM);
            //    if (log > 0)
            //    {
            //        return Ok("Selamat Login Berhasil");
            //    }
            //    else if (log == -2)
            //    {
            //        return NotFound($"akun dengan email {loginVM.Email} tidak ditemukan di database");
            //    }
            //    else if (log == -1)
            //    {
            //        return BadRequest("Maaf, Password Anda Salah!");
            //    }
            //    else
            //    {
            //        return BadRequest("Maaf, email dan password salah");
            //    }
            //}
            //else
            //{
            //    return BadRequest($"data email tidak ditemukan");
            //}
        }
            [Route("ForgetPass")]
        [HttpPost]
        public ActionResult ForgotPassword(RegisterVM registerVM)
        {
            var hasil = accountRepository.SendOTP(registerVM);
           
                
                if (hasil >0)
                {
                    return Ok("Cek Email!");

                }else
                {
                    return BadRequest("Check Code");
                }    
                                    
        }


        [Route("ChangePass")]
        [HttpPut]
        public ActionResult<ChangeVM> ChangePass(ChangeVM changeVM)
        {
            var result = accountRepository.ChangePass(changeVM);
            if (result == 1)
            {
                return Ok("Succes Password Berhasil!");
            }
            else if(result == -1)
            {
                return BadRequest("Password tidak sesuai!");
            }
            else if (result == -2)
            {
                return BadRequest("OTP sudah digunakan!");
            }
            else if (result == -3)
            {
                return BadRequest("OTP tidak sama!");
            }
            else if (result == -4)
            {
                return BadRequest("OTP expired!");
            }else
            {
                return BadRequest("Error Email tidak terdaftar");
            }


        }
    }
}
