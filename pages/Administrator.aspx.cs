using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4WoodsVoglewede.pages
{

    public partial class Adminstrator : System.Web.UI.Page
    {

        Logon logon = new Logon();
        KarateDataContext dbcon;
        string userName;
        String connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";

        protected void Page_Load(object sender, EventArgs e)
        {
            //retrieves username used on successful login
            userName = logon.GetUserName();

            if (userName == null)
            {
                Response.Redirect("logon.aspx");
            }

            //create new data connection
            dbcon = new KarateDataContext(connString);

            try
            {
                // LINQ
                var checkAdmin = from user in dbcon.NetUsers
                                 where user.UserName == userName
                                 select user.UserType;

                string userType = checkAdmin.FirstOrDefault();

                if(userType != "Admin")
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                terminateAccess();
            }



        }

        // for logging out / preventing unauthorized access
        public void terminateAccess()
        {
            logon.DeleteUserName();
            Response.Redirect("logon.aspx");
        }

        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            terminateAccess();
        }
    }
}