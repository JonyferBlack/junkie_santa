using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JunkieSanta
{
    public partial class Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = $"Hey! It looks like your gift goes to a person with the name {GlobalStorage.Instance.PredictedName}";
        }
    }
}