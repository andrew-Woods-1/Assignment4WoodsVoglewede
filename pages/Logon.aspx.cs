using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4WoodsVoglewede.pages
{
    public partial class Logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dbcon = new KarateDataContext(connString);
            int id = dbcon.Instructors.First().InstructorID;
            Label1.Text = id.ToString();
        }

        String connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        KarateDataContext dbcon;

        //On a successful login, set this string to the username used
        //This will be passed to the Member and Instructor forms
        //in their page load events to help access info from database
        string userName;

        public string GetUserName()
        {
            return userName;
        }
    }
}