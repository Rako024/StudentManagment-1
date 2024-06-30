using Business.DTOs.Account;
using Business.Services.Abstracts;
using Core.Models;
using MailKit;
using Main.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers;

public class AccountController : Controller
{
    UserManager<AppUser> _userManager;
    RoleManager<IdentityRole> _roleManager;
    SignInManager<AppUser> _signInManager;
    private readonly Business.Services.Abstracts.IMailService _mailService;

    public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, Business.Services.Abstracts.IMailService mailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        
        _mailService = mailService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return View(loginDto);
        }
        AppUser user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
        }

        if(user == null)
        {
            ModelState.AddModelError("", "Username or Password is not valid");
            return View();
        }
        if(!user.LockoutEnabled)
        {
            ModelState.AddModelError("", "You Are Blocked!");
            return View();
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Username or Password is not valid");
            return View();
        }
        var signInResult = await _signInManager.PasswordSignInAsync(user,loginDto.Password,loginDto.RememberMe,false);
        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError("", "Username or Password is not valid");
            return View();
        }
        IList<string> roleList = await _userManager.GetRolesAsync(user);
        string role = roleList.FirstOrDefault()?.ToString();
        if (role == RolesEnum.Teacher.ToString())
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Teacher", id = user.Id });
        }else if (role == RolesEnum.Student.ToString()) 
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Student", id = user.Id });
        }
        else if (role == RolesEnum.Cordinator.ToString() || role == RolesEnum.SuperAdmin.ToString() || role == RolesEnum.Admin.ToString())
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin", id = user.Id });
        }
        else
        {
            return View();
        }
        
    }

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
    public IActionResult ForgotPassword()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPassword)
    {
        if (!ModelState.IsValid)
        {
            return View();  
        }
        var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
        if (user is null)
        {
            ModelState.AddModelError("Email", "Email not foud!");
            return View();
        }
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //https://localhost:7238/account/resetpassword
        string link = Url.Action("ResetPassword","Account",new {userId = user.Id,token = token},HttpContext.Request.Scheme);


        await _mailService.SendEmailAsync(new MailRequest
        {
            Subject = "Reset Password",
            ToEmail = forgotPassword.Email,
            Body =$"\r\n    <!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n\r\n<head>\r\n  <!--[if gte mso 9]>\r\n<xml>\r\n  <o:OfficeDocumentSettings>\r\n    <o:AllowPNG/>\r\n    <o:PixelsPerInch>96</o:PixelsPerInch>\r\n  </o:OfficeDocumentSettings>\r\n</xml>\r\n<![endif]-->\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <!--[if !mso]><!-->\r\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n  <!--<![endif]-->\r\n  <title></title>\r\n\r\n  <style type=\"text/css\">\r\n    @media only screen and (min-width: 620px) {{\r\n      .u-row {{\r\n        width: 600px !important;\r\n      }}\r\n      .u-row .u-col {{\r\n        vertical-align: top;\r\n      }}\r\n      .u-row .u-col-100 {{\r\n        width: 600px !important;\r\n      }}\r\n    }}\r\n    \r\n    @media (max-width: 620px) {{\r\n      .u-row-container {{\r\n        max-width: 100% !important;\r\n        padding-left: 0px !important;\r\n        padding-right: 0px !important;\r\n      }}\r\n      .u-row .u-col {{\r\n        min-width: 320px !important;\r\n        max-width: 100% !important;\r\n        display: block !important;\r\n      }}\r\n      .u-row {{\r\n        width: 100% !important;\r\n      }}\r\n      .u-col {{\r\n        width: 100% !important;\r\n      }}\r\n      .u-col>div {{\r\n        margin: 0 auto;\r\n      }}\r\n    }}\r\n    \r\n    body {{\r\n      margin: 0;\r\n      padding: 0;\r\n    }}\r\n    \r\n    table,\r\n    tr,\r\n    td {{\r\n      vertical-align: top;\r\n      border-collapse: collapse;\r\n    }}\r\n    \r\n    p {{\r\n      margin: 0;\r\n    }}\r\n    \r\n    .ie-container table,\r\n    .mso-container table {{\r\n      table-layout: fixed;\r\n    }}\r\n    \r\n    * {{\r\n      line-height: inherit;\r\n    }}\r\n    \r\n    a[x-apple-data-detectors='true'] {{\r\n      color: inherit !important;\r\n      text-decoration: none !important;\r\n    }}\r\n    \r\n    table,\r\n    td {{\r\n      color: #000000;\r\n    }}\r\n    \r\n    #u_body a {{\r\n      color: #0000ee;\r\n      text-decoration: underline;\r\n    }}\r\n  </style>\r\n\r\n\r\n\r\n  <!--[if !mso]><!-->\r\n  <link href=\"https://fonts.googleapis.com/css?family=Cabin:400,700\" rel=\"stylesheet\" type=\"text/css\">\r\n  <!--<![endif]-->\r\n\r\n</head>\r\n\r\n<body className=\"clean-body u_body\" style=\"margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #f9f9f9;color: #000000\">\r\n  <!--[if IE]><div className=\"ie-container\"><![endif]-->\r\n  <!--[if mso]><div className=\"mso-container\"><![endif]-->\r\n  <table id=\"u_body\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #f9f9f9;width:100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n    <tbody>\r\n      <tr style=\"vertical-align: top\">\r\n        <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top\">\r\n          <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td align=\"center\" style=\"background-color: #f9f9f9;\"><![endif]-->\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #afb0c7; line-height: 170%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"font-size: 14px; line-height: 170%;\"><span style=\"font-size: 14px; line-height: 23.8px;\">View Email in Browser</span></p>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #ffffff;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #003399;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #003399;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:40px 10px 10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">\r\n                                <tr>\r\n                                  <td style=\"padding-right: 0px;padding-left: 0px;\" align=\"center\">\r\n\r\n                                    <img align=\"center\" border=\"0\" src=\"https://cdn.templates.unlayer.com/assets/1597218650916-xxxxc.png\" alt=\"Image\" title=\"Image\" style=\"outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;clear: both;display: inline-block !important;border: none;height: auto;float: none;width: 26%;max-width: 150.8px;\"\r\n                                      width=\"150.8\" />\r\n\r\n                                  </td>\r\n                                </tr>\r\n                              </table>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #e5eaf5; line-height: 140%; text-align: center; word-wrap: break-word;\">\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:0px 10px 31px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #e5eaf5; line-height: 140%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"font-size: 14px; line-height: 140%;\"><span style=\"font-size: 28px; line-height: 39.2px;\"><strong><span style=\"line-height: 39.2px; font-size: 28px;\">\r\n                                </span></strong>\r\n                                  </span>\r\n                                </p>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #ffffff;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #ffffff;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:33px 55px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; line-height: 160%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"font-size: 14px; line-height: 160%;\"><span style=\"font-size: 18px; line-height: 28.8px;\"><a href='{link}'>Reset Password</a> </span></p>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <!--[if mso]><style>.v-button {{background: transparent !important;}}</style><![endif]-->\r\n                              <div align=\"center\">\r\n                                <!--[if mso]><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"\" style=\"height:46px; v-text-anchor:middle; width:234px;\" arcsize=\"8.5%\"  stroke=\"f\" fillcolor=\"#ff6600\"><w:anchorlock/><center style=\"color:#FFFFFF;\"><![endif]-->\r\n                                <!--[if mso]></center></v:roundrect><![endif]-->\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:33px 55px 60px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; line-height: 160%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"line-height: 160%; font-size: 14px;\"><span style=\"font-size: 18px; line-height: 28.8px;\">Thanks,</span></p>\r\n                                <p style=\"line-height: 160%; font-size: 14px;\"><span style=\"font-size: 18px; line-height: 28.8px;\">{user.Name + " " +user.Surname }</span></p>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: #e5eaf5;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #e5eaf5;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:41px 55px 18px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #003399; line-height: 160%; text-align: center; word-wrap: break-word;\">\r\n                                <p style=\"font-size: 14px; line-height: 160%;\"><span style=\"font-size: 16px; line-height: 25.6px; color: #000000;\">{user.Email}</span></p>\r\n                              </div>\r\n                              <div style=\"font-size: 14px; color: #003399; line-height: 160%; text-align: center; word-wrap: break-word;\">\r\n         \r\n                            </div>\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 33px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div align=\"center\">\r\n                                <div style=\"display: table; max-width:244px;\">\r\n                                  <!--[if (mso)|(IE)]><table width=\"244\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"border-collapse:collapse;\" align=\"center\"><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-collapse:collapse; mso-table-lspace: 0pt;mso-table-rspace: 0pt; width:244px;\"><tr><![endif]-->\r\n\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 17px;\" valign=\"top\"><![endif]-->\r\n                           \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 17px;\" valign=\"top\"><![endif]-->\r\n                              \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 17px;\" valign=\"top\"><![endif]-->\r\n                            \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 17px;\" valign=\"top\"><![endif]-->\r\n                        \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n                                  <!--[if (mso)|(IE)]><td width=\"32\" style=\"width:32px; padding-right: 0px;\" valign=\"top\"><![endif]-->\r\n                            \r\n                                  <!--[if (mso)|(IE)]></td><![endif]-->\r\n\r\n\r\n                                  <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n                                </div>\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n\r\n\r\n          <div className=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n            <div className=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;\">\r\n              <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n                <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: #003399;\"><![endif]-->\r\n\r\n                <!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n                <div className=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n                  <div style=\"height: 100%;width: 100% !important;\">\r\n                    <!--[if (!mso)&(!IE)]><!-->\r\n                    <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\">\r\n                      <!--<![endif]-->\r\n\r\n                      <table style=\"font-family:'Cabin',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td style=\"overflow-wrap:break-word;word-break:break-word;padding:10px;font-family:'Cabin',sans-serif;\" align=\"left\">\r\n\r\n                              <div style=\"font-size: 14px; color: #fafafa; line-height: 180%; text-align: center; word-wrap: break-word;\">\r\n                              </div>\r\n\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table>\r\n\r\n                      <!--[if (!mso)&(!IE)]><!-->\r\n                    </div>\r\n                    <!--<![endif]-->\r\n                  </div>\r\n                </div>\r\n                <!--[if (mso)|(IE)]></td><![endif]-->\r\n                <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n              </div>\r\n            </div>\r\n          </div>\r\n\r\n\r\n\r\n          <!--[if (mso)|(IE)]></td></tr></table><![endif]-->\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table>\r\n  <!--[if mso]></div><![endif]-->\r\n  <!--[if IE]></div><![endif]-->\r\n</body>\r\n\r\n</html>\r\n"
            
            
        });
        return RedirectToAction(nameof(Login));
    }

    public async Task<IActionResult> ResetPassword(string userId , string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return View("Error");   
        }

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto, string userId,string token)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return View("Error");
        }
        var result = await _userManager.ResetPasswordAsync(user, token,dto.Password);
        return RedirectToAction(nameof(Login));
    }


    public async Task<IActionResult> ChangePassword(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return View("Error");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto,string userId)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
		var user = await _userManager.FindByIdAsync(userId);
		if (user is null)
		{
			return View("Error");
		}
		var result = await _userManager.ChangePasswordAsync(user, dto.PreviousPassword, dto.NewPassword);
		if (result.Succeeded)
		{
			await LogOut();   
			return RedirectToAction(nameof(Login));
		}
		else
		{
			
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(dto);
		}

	}
    //public async Task<IActionResult> CreateRoles()
    //{
    //    IdentityRole role1 = new IdentityRole(RolesEnum.Admin.ToString());
    //    IdentityRole role2 = new IdentityRole(RolesEnum.Cordinator.ToString());
    //    IdentityRole role3 = new IdentityRole(RolesEnum.Teacher.ToString());
    //    IdentityRole role4 = new IdentityRole(RolesEnum.Student.ToString());

    //    await _roleManager.CreateAsync(role1);
    //    await _roleManager.CreateAsync(role2);
    //    await _roleManager.CreateAsync(role3);
    //    await _roleManager.CreateAsync(role4);
    //    return Ok("salam exi");
    //}
    //public async Task<IActionResult> CreateAdmin()
    //{
    //    AppUser user = new AppUser() 
    //    {
    //        UserName="Admin",
    //        Email = "admin@gmail.com",
    //        Name = "admin",
    //        Surname = "admin",
    //    };
    //    var result = await _userManager.CreateAsync(user,"Salam123@");
    //    if(result.Succeeded)
    //    {
    //        var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
    //        if(!roleResult.Succeeded)
    //        {
    //            return BadRequest(roleResult.Errors);
    //        }
    //        return Ok("Yarandi");
    //    }
    //    else
    //    {
    //        return BadRequest(result.Errors);
    //    }
    //}
}
