using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JunkieSanta
{
    public partial class _Default : Page
    {
        private readonly DataLogicModel _dataLogicModel = GlobalStorage.Instance;

        protected void Page_Load(object sender, EventArgs e)
        {
          //  if (Page.IsPostBack) return;
            UpdateImage();
        }

        private void UpdateImage()
        {
            _dataLogicModel.UpdateImage(ImageButton1, "~/Content/santa");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            UpdateImage();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            _dataLogicModel.NameBoxText = TextBox1.Text;
            UpdateImage();

            var name = _dataLogicModel.NameBoxText.ToLower();
            if (string.IsNullOrEmpty(name))
            {
                Label1.Text = "I told to enter fucking name. Can you fucking here me!?";
                return;
            }
            if (!_dataLogicModel.Names.Contains(name))
            {
                Label1.Text = "Who the fuck YOU are? Didn't Andrey told you that this is a private party?";
                return;
            }

            Label1.Text = $"ok, {name}, let's play...";

            Button1.Visible = false;
            Label2.Visible = true;
           _dataLogicModel.FindPresentReciever(name);
            Response.Redirect("~/Result");
        }

        protected void ImageButton1_Init(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            _dataLogicModel.NameBoxText = Server.HtmlEncode(TextBox1.Text);
        }
    }
}