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
        #region properties
        protected string DestinationDirectoryName
        {
            get { return WebConfigurationManager.AppSettings["DestinationDirectory"]; }
        }
        protected string DestinationDirectory
        {
            get { return Server.MapPath(DestinationDirectoryName); }
        }
        protected string DownloadUrl
        {
            get { return WebConfigurationManager.AppSettings["SourceUrl"]; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void ImageItemDataSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            var id = (e.InputParameters["item"] as ImageItem).ID;
            var img = new ImageDAO().GetImageItem(id);
            var filepath = Path.Combine(DestinationDirectory, img.FileName);
            if (File.Exists(filepath)) File.Delete(filepath);
        }

        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            new ImageDAO().DownloadAndSaveAllImages(DownloadUrl, DestinationDirectory);
        }

        protected void RemoveAllButton_Click(object sender, EventArgs e)
        {
            new ImageDAO().DeleteAllImageItemsAndFiles(DestinationDirectory);
        }
    }
}