using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4WoodsVoglewede.pages
{
    public partial class Logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        String connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        KarateDataContext dbcon;

        //On a successful login, set this string to the username used
        //This will be passed to the Member and Instructor forms
        //in their page load events to help access info from database

        public static string userName;


        public string GetUserName()
        {
            return userName;
        }

        public void DeleteUserName()
        {
            userName = null;
        }

        protected void loginElement_Authenticate(object sender, AuthenticateEventArgs e)
        {
            dbcon = new KarateDataContext(connString);

            string usernameAttempt = loginElement.UserName;
            string passwordAttempt = loginElement.Password;
            
            
            try
            {
                // LINQ
                var confirmUsername = from user in dbcon.NetUsers
                                      where user.UserName == usernameAttempt
                                      select user.UserName;

                string confirmedUsername = confirmUsername.FirstOrDefault();

                var confirmPassword = from user in dbcon.NetUsers
                                      where user.UserName == confirmedUsername
                                      select user.UserPassword;

                string password = confirmPassword.FirstOrDefault();

                if (password != passwordAttempt)
                {
                    
                    throw new Exception();
                }
                // LINQ
                var getUserType = from user in dbcon.NetUsers
                                  where user.UserName == confirmedUsername
                                  select user.UserType;

                string userType = getUserType.FirstOrDefault();

                if (userType == "Admin") {
                    userName = confirmedUsername;
                    Response.Redirect("Administrator.aspx");
                }
                else if (userType == "Instructor")
                {
                    userName = confirmedUsername;
                    Response.Redirect("Instructor.aspx");
                }
                else if (userType == "Member")
                {
                    userName = confirmedUsername;
                    Response.Redirect("Member.aspx");
                }
                else
                {
                    
                    throw new Exception();
                    
                }

            }
            catch (Exception ex)
            {
                
            }

        }
    }
}