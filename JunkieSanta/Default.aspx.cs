using System;
using System.Collections.Generic;
using System.IO;
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
            _allNamesTaken = _dataLogicModel.LoggedNames(Server).Count(_ => !string.IsNullOrEmpty(_)) == _dataLogicModel.Names(Server).Count();
            if (_allNamesTaken)
            {
                Label1.Text = "Ok guys, we're done.";
                Button1.Enabled = false;
                Button1.Text = "С наступающим Вас 2018 годом!";
                ListBox1.Visible = false;
            }
            else
            {
                ListBox1.Items.AddRange(_dataLogicModel.Names(Server).Except(_dataLogicModel.LoggedNames(Server)).Select(_=>new ListItem(_)).ToArray());
            }
            UpdateImage();
        }

        private bool _allNamesTaken;

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
            if(_allNamesTaken) return;

            _dataLogicModel.NameBoxText = ListBox1.SelectedValue;
            UpdateImage();

            var name = _dataLogicModel.NameBoxText.ToLower();
            if (string.IsNullOrEmpty(name))
            {
                Label1.Text = "I told to enter fucking name. Can you fucking here me!?";
                return;
            }
            if (!_dataLogicModel.Names(Server).Contains(name))
            {
                Label1.Text = "Who the fuck YOU are? Didn't Andrey told you that this is a private party?";
                return;
            }
            if (_dataLogicModel.LoggedNames(Server).Contains(name))
            {
                Label1.Text = $"Namaste, {name}! For purpose of Art(and because of my skill limitations) everyone can enter his name only once. But you can donait our project. Om shanti!";
                return;
            }

            Label1.Text = $"ok, {name}, let's play...";

            Button1.Visible = false;
            Label2.Visible = true;
           _dataLogicModel.FindPresentReciever(name, Server);
            Response.Redirect("~/Result");
        }

        protected void ImageButton1_Init(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            _dataLogicModel.NameBoxText = Server.HtmlEncode(ListBox1.SelectedValue);
        }
    }
}