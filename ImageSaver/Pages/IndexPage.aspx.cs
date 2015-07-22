using HtmlAgilityPack;
using ImageSaver.DAL;
using ImageSaver.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImageSaver.Pages
{
    public partial class IndexPage : System.Web.UI.Page
    {
        protected string DestinationDirectory
        {
            get { return WebConfigurationManager.AppSettings["DestinationDirectory"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            //var from_url = WebConfigurationManager.AppSettings["SourceUrl"];
            //var to_path = Server.MapPath(WebConfigurationManager.AppSettings["DestinationDirectory"]);
            //var dao = new ImageDAO();
            //dao.DownloadAndSaveAllImages(from_url, to_path);
        }
    }
}