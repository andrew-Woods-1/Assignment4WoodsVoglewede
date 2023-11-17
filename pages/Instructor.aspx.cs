using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace Assignment4WoodsVoglewede.pages
{
    public partial class Instructor : System.Web.UI.Page
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

            //create new data connection
            dbcon = new KarateDataContext(connString);


            try
            {
                //linq query to find instructorID from their userName and retrieve sections and members
                var query = from section in dbcon.Sections
                            from user in dbcon.NetUsers
                            where user.UserName == userName && user.UserID == section.Instructor_ID
                            join member in dbcon.Members on section.Member_ID equals member.Member_UserID
                            select new
                            {
                                SectionName = section.SectionName,
                                MemberFirstName = member.MemberFirstName,
                                MemberLastName = member.MemberLastName
                            };

                //linq query to retrieve instructors first name based on userName
                var label1 = from instructor in dbcon.Instructors
                             from user in dbcon.NetUsers
                             where user.UserName == userName &&
                             user.UserID == instructor.InstructorID
                             select instructor.InstructorFirstName;
                //display the firstname in label1
                if (label1.FirstOrDefault() == null)
                {
                    throw new Exception();
                }
                Label1.Text = label1.FirstOrDefault().ToString();

                //linq query to retrieve instructors last name based on userName
                var label2 = from instructor in dbcon.Instructors
                             from user in dbcon.NetUsers
                             where user.UserName == userName &&
                             user.UserID == instructor.InstructorID
                             select instructor.InstructorLastName;
                //display lastname in label2
                if (label2.FirstOrDefault() == null)
                {
                    throw new Exception();
                }
                Label2.Text = label2.FirstOrDefault().ToString();

                //set GridView1 DataSource to query
                GridView1.DataSource = query;
                GridView1.DataBind();
            }
            catch (Exception exception)
            {
                TerminateAccess();
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
