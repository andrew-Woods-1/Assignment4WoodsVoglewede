using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Security.Cryptography;
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

                if (userType != "Admin")
                {
                    throw new Exception();
                }
                if (!IsPostBack)
                {
                    RefreshPage();
                }





            }
            catch (Exception ex)
            {
                TerminateAccess();
            }



        }

        public void RefreshPage()
        {
            dbcon = new KarateDataContext(connString);

            // LINQ, retrieve member information for gridview
            var memberInformation = from member in dbcon.Members
                                    select new
                                    {
                                        ID = member.Member_UserID,
                                        MemberFirstName = member.MemberFirstName,
                                        MemberLastName = member.MemberLastName,
                                        MemberPhone = member.MemberPhoneNumber,
                                        MemberDateJoined = member.MemberDateJoined

                                    };

            memberGridView.DataSource = memberInformation;
            memberGridView.DataBind();

            // LINQ, retrieve instructor information for gridview
            var instructorInformation = from instructor in dbcon.Instructors
                                        select new
                                        {
                                            ID = instructor.InstructorID,
                                            InstructorFirstName = instructor.InstructorFirstName,
                                            InstructorLastName = instructor.InstructorLastName

                                        };

            instructorGridView.DataSource = instructorInformation;
            instructorGridView.DataBind();

            // clear forms for easier editing of database
            tbInstructorDeleteID.Text = string.Empty;
            tbInstructorInsertFN.Text = string.Empty;
            tbInstructorInsertLN.Text = string.Empty;
            tbInstructorInsertPassword.Text = string.Empty;
            tbInstructorInsertPhone.Text = string.Empty;
            tbInstructorInsertUsername.Text = string.Empty;
            tbMemberDeleteID.Text = string.Empty;
            tbMemberInsertDate.Text = string.Empty;
            tbMemberInsertEmail.Text = string.Empty;
            tbMemberInsertFN.Text = string.Empty;
            tbMemberInsertLN.Text = string.Empty;
            tbMemberInsertPassword.Text = string.Empty;
            tbMemberInsertPhone.Text = string.Empty;
            tbMemberInsertUsername.Text = string.Empty;
            tbSectionInstructorID.Text = string.Empty;
            tbSectionMemberID.Text = string.Empty;
            rbKarateType.ClearSelection();


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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshPage();

        }

        protected void btnMemberInsert_Click(object sender, EventArgs e)
        {

            try
            {

                var newUser = new NetUser();
                newUser.UserID = dbcon.NetUsers.Max(x => x.UserID); // finds id that is usable for new user
                newUser.UserName = tbMemberInsertUsername.Text;
                newUser.UserPassword = tbMemberInsertPassword.Text;
                newUser.UserType = "Member";

                newUser.Member = new Assignment4WoodsVoglewede.Member();
                newUser.Member.Member_UserID = newUser.UserID;
                newUser.Member.MemberFirstName = tbMemberInsertFN.Text;
                newUser.Member.MemberLastName = tbMemberInsertLN.Text;
                newUser.Member.MemberPhoneNumber = tbMemberInsertPhone.Text;
                newUser.Member.MemberEmail = tbMemberInsertEmail.Text;
                newUser.Member.MemberDateJoined = DateTime.Parse(tbMemberInsertDate.Text);

                dbcon.NetUsers.InsertOnSubmit(newUser);
                dbcon.SubmitChanges();


            }
            catch (Exception ex)
            {

            }

            RefreshPage();
        }

        protected void btnMemberDelete_Click(object sender, EventArgs e)
        {
            dbcon = new KarateDataContext(connString);

            try
            {
                Assignment4WoodsVoglewede.Section sectionMember =
                 dbcon.Sections.FirstOrDefault(item => item.Member_ID == Convert.ToInt32(tbMemberDeleteID.Text)); // identify section by id returned

                if (sectionMember != null)
                {
                    dbcon.Sections.DeleteOnSubmit(sectionMember);
                    dbcon.SubmitChanges();
                }

                Assignment4WoodsVoglewede.Member member =
                    dbcon.Members.FirstOrDefault(item => item.Member_UserID == Convert.ToInt32(tbMemberDeleteID.Text)); // identify member by id returned

                if (member != null)
                {
                    dbcon.Members.DeleteOnSubmit(member);
                    dbcon.SubmitChanges();
                }



                NetUser user = dbcon.NetUsers.FirstOrDefault(item => item.UserID == Convert.ToInt32(tbMemberDeleteID.Text)); // identify user by id returned

                if (user != null)
                {
                    dbcon.NetUsers.DeleteOnSubmit(user);
                    dbcon.SubmitChanges();
                }
            }
            catch (Exception ex)
            {

            }

            RefreshPage();

        }

        protected void btnAssignSection_Click(object sender, EventArgs e)
        {
            dbcon = new KarateDataContext(connString);

            try
            {
                var newSectionAssignment = new Assignment4WoodsVoglewede.Section();
                newSectionAssignment.SectionID = dbcon.Sections.Max(x => x.SectionID); // find new id that is usable for new section
                newSectionAssignment.SectionName = rbKarateType.SelectedValue;
                newSectionAssignment.SectionStartDate = DateTime.Now.AddDays(7);
                newSectionAssignment.Member_ID = Convert.ToInt32(tbSectionMemberID.Text);
                newSectionAssignment.Instructor_ID = Convert.ToInt32(tbSectionInstructorID.Text);
                newSectionAssignment.SectionFee = 500.0M;

                dbcon.Sections.InsertOnSubmit(newSectionAssignment);
                dbcon.SubmitChanges();
            }
            catch (Exception ex)
            {

            }

            RefreshPage();

        }

        protected void btnInstructorInsert_Click(object sender, EventArgs e)
        {
            dbcon = new KarateDataContext(connString);

            try
            {
                var newUser = new NetUser();
                newUser.UserID = dbcon.NetUsers.Max(x => x.UserID); // finds id that is usable for new user
                newUser.UserName = tbInstructorInsertUsername.Text;
                newUser.UserPassword = tbInstructorInsertPassword.Text;
                newUser.UserType = "Instructor";

                newUser.Instructor = new Assignment4WoodsVoglewede.Instructor();
                newUser.Instructor.InstructorID = newUser.UserID;
                newUser.Instructor.InstructorFirstName = tbInstructorInsertFN.Text;
                newUser.Instructor.InstructorLastName = tbInstructorInsertLN.Text;
                newUser.Instructor.InstructorPhoneNumber = tbInstructorInsertPhone.Text;

                dbcon.NetUsers.InsertOnSubmit(newUser);
                dbcon.SubmitChanges();
            }
            catch (Exception ex)
            {
                
            }

            RefreshPage();

        }

        protected void btnInstructorDelete_Click(object sender, EventArgs e)
        {
            dbcon = new KarateDataContext(connString);

            try
            {
                // loop to keep deleting sections that will have a null instructor
                while ((from section in dbcon.Sections where section.Instructor_ID == Convert.ToInt32(tbInstructorDeleteID.Text) select section).Count() > 0)
                {
                    Assignment4WoodsVoglewede.Section sectionInstructor =
                     dbcon.Sections.FirstOrDefault(item => item.Instructor_ID == Convert.ToInt32(tbInstructorDeleteID.Text)); // identify section by id returned

                    if (sectionInstructor != null)
                    {
                        dbcon.Sections.DeleteOnSubmit(sectionInstructor);
                        dbcon.SubmitChanges();
                    }

                }

                Assignment4WoodsVoglewede.Instructor instructor =
                        dbcon.Instructors.FirstOrDefault(item => item.InstructorID == Convert.ToInt32(tbInstructorDeleteID.Text)); // identify instructor by id returned

                if (instructor != null)
                {
                    dbcon.Instructors.DeleteOnSubmit(instructor);
                    dbcon.SubmitChanges();
                }



                NetUser user = dbcon.NetUsers.FirstOrDefault(item => item.UserID == Convert.ToInt32(tbInstructorDeleteID.Text)); // identify user by id returned

                if (user != null)
                {
                    dbcon.NetUsers.DeleteOnSubmit(user);
                    dbcon.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                
            }

            RefreshPage();

        }
    }
}