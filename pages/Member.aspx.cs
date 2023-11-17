using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace Assignment4WoodsVoglewede.pages
{
    public partial class Member : System.Web.UI.Page
    {

        //creates instance of logon class to call GetUsername()
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


            try
            {
                //create new data connection
                dbcon = new KarateDataContext(connString);

                //linq query to find memberID from their userName and retrieve data
                var records = from section in dbcon.Sections
                              from user in dbcon.NetUsers
                              where user.UserName == userName && user.UserID == section.Member_ID
                              join instructor in dbcon.Instructors on section.Instructor_ID equals instructor.InstructorID
                              select new
                              {
                                  SectionName = section.SectionName,
                                  InstructorFirstName = instructor.InstructorFirstName,
                                  InstructorLastName = instructor.InstructorLastName,
                                  PaymentDate = section.SectionStartDate,
                                  Amount = "$" + section.SectionFee
                              };

                //linq query to retrieve members first name based on userName
                var label1 = from member in dbcon.Members
                             from user in dbcon.NetUsers
                             where user.UserName == userName &&
                             user.UserID == member.Member_UserID
                             select member.MemberFirstName;
                Debug.Write("\n" + label1.ToString());
                //display the firstname in label1
                if(label1.FirstOrDefault() == null)
                {
                    throw new Exception();
                }
                Label1.Text = label1.FirstOrDefault().ToString();

                //linq query to retrieve members last name based on userName
                var label2 = from member in dbcon.Members
                             from user in dbcon.NetUsers
                             where user.UserName == userName &&
                             user.UserID == member.Member_UserID
                             select member.MemberLastName;
                //display lastname in label2
                if (label2.FirstOrDefault() == null)
                {
                    throw new Exception();
                }
                Label2.Text = label2.FirstOrDefault().ToString();

                //set GridView1 DataSource to show records
                GridView1.DataSource = records;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("logon.aspx");
            }
        }

        // for logging out / preventing unauthorized access
        public void TerminateAccess()
        {
            logon.DeleteUserName();
            Response.Redirect("logon.aspx");
        }

        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            TerminateAccess();
        }
    }
}