using API2.Context;
using API2.Models;
using API2.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BCrypt.Net;

namespace API2.Repository.Data
{
    
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        //hash
        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public static bool ValidatePassword(string password, string correctHas)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHas);
        }
        public int Register(RegisterVM inputData)
        {
            Employee emp = new Employee()
            {
                FirstName = inputData.FirstName,
                LastName = inputData.LastName,
                Phone = inputData.Phone,
                BirtDate = inputData.BirthDate,
                Salary = inputData.Salary,
                Email = inputData.Email,
                Gender = inputData.Gender
            };

            Account acc = new Account()
            {
             
                Password = BCrypt.Net.BCrypt.HashPassword(inputData.Password),
            };

            Education edu = new Education()
            {
                Degree = inputData.Degree,
                GPA = inputData.GPA,
                University_id = inputData.University_Id,
            };

            Profiling prf = new Profiling();

            // Auto Increment NIK
            var empCount = myContext.Employees.Count() + 1;
            var Year = DateTime.Now.Year;
            emp.NIK = Year + "00" + empCount.ToString();
            acc.NIK = emp.NIK;

            // Set Data Tabel Profiling
            prf.NIK = acc.NIK;
            prf.Education = edu;

            // Cek phone dan email tidak boleh sama
            var dataEmail = myContext.Employees.SingleOrDefault(e => e.Email == inputData.Email);
            var dataPhone = myContext.Employees.SingleOrDefault(e => e.Phone == inputData.Phone);

            if (dataEmail == null && dataPhone == null) // email dan phone aman
            {
                myContext.Employees.Add(emp);
                myContext.Accounts.Add(acc);              
                myContext.Educations.Add(edu);
                myContext.Profilings.Add(prf);
                myContext.SaveChanges();
                return 1;
            }
            else if (dataEmail != null && dataPhone == null) // email sudah ada
            {
                return -1;
            }
            else if (dataEmail == null && dataPhone != null) // phone sudah ada
            {
                return -2;
            }
            else
            {
                return 0;
            }
        }

        // Login menggunakan Email dan Password
        public int Login(LoginVM loginVM)
        {
            // ambil data berdasarkan email
            var dataDiambil = myContext.Employees.SingleOrDefault(e => e.Email == loginVM.Email);

            // cek ke database apakah data email ada\
            if (dataDiambil != null)
            {
                // ambil data join tabel Employee dan Account
                var data = myContext.Employees.Join(myContext.Accounts,
                    e => e.NIK,
                    a => a.NIK,
                    (e, a) => new
                    {
                        Email = e.Email,
                        Password = a.Password
                    });

                // ambil 1 data dari hasil Join pilih email yang diinput
                var dataHasilJoin = data.SingleOrDefault(e => e.Email == loginVM.Email);
               
                // cek apakah Email cocok dengan Password
                if (dataHasilJoin.Email == loginVM.Email && ValidatePassword(loginVM.Password, dataHasilJoin.Password))
                {
                    return 1; //sukses login
                }
                else if (dataHasilJoin.Email == loginVM.Email && ValidatePassword(loginVM.Password, dataHasilJoin.Password) == false)
                {
                    return -1; // password salah
                }
                else
                {
                    return -2; // email salah
                }

            }
            else
            {
                return 0; // data email tidak ada
            }
        }

        //send OTP forget pass
        public int SendOTP(RegisterVM registerVM)
            {
            var sendMail = myContext.Employees.Where(e => e.Email == registerVM.Email).FirstOrDefault();
            if (sendMail != null)
            { 
                //random number
                Random r = new Random();
                int OTP = r.Next(100000, 999999);
                var obj = myContext.Accounts.Where(e => e.NIK == sendMail.NIK).FirstOrDefault();
                obj.OTP = OTP;
                obj.ExpiredToken = DateTime.Now.AddMinutes(5);
                obj.IsUsed = false;

                myContext.Entry(obj).State = EntityState.Modified;
                myContext.SaveChanges();
                //end random number

                //ngirim otp via email
                var fromEmail = new MailAddress("amadfingerboard@gmail.com", "CS"); //email yg buat ngirim
                var toEmail = new MailAddress(registerVM.Email, "Client");
                const string fromPassword = "Indonesi4";
                var subject = "OTP Reset Password" + DateTime.Now.ToString();
                var body = "Hello According to your OTP is :" + OTP.ToString();

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromPassword),

                };
                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return 1;
            }else
            {
                return 0;
            }
            
        }
        //change pass
        public int ChangePass(ChangeVM changeVM)
        {
            var cekEmail = myContext.Employees.Where(e => e.Email == changeVM.Email).FirstOrDefault();
            if (cekEmail != null)
            {
                var obj = myContext.Accounts.Where(n => n.NIK == cekEmail.NIK).FirstOrDefault();
                if (DateTime.Now < obj.ExpiredToken) //cek token expierd
                {
                    if(obj.OTP == changeVM.Otp) 
                    {
                        if(obj.IsUsed == false)
                        {
                            if (changeVM.Password == changeVM.ConfirmPass)
                            {
                                obj.Password = BCrypt.Net.BCrypt.HashPassword(changeVM.ConfirmPass);
                                obj.IsUsed = true;
                                myContext.Entry(obj).State = EntityState.Modified;
                                myContext.SaveChanges();
                                return 1;
                            }
                            else
                            {
                                return -1;
                            }
                        }else
                        {
                            return -2;
                        }
                    }else
                    {
                        return -3;
                    }
                }else
                {
                    return -4;
                }
            }else
            {
                return 0;
            }
        }

    }
    
}
