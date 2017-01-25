using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebServerFileDownloader
{
    public partial class Sample : System.Web.UI.Page
    {
        /*
         * this program is a demo for an uploading a file to a server's folder,
         * and downloading of files under specified server's folder.
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Populates the files that is under server files folder
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Server Files/"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
                //view to Gridview
                GridView1.DataSource = files;
                GridView1.DataBind();
            }
        }
        protected void UploadFile(object sender, EventArgs e)
        {
            /*
             * upload a file to Server Files folder.
             * for mock up only
            */
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Server Files/") + fileName);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            /*
             * When a download link is clicked it downloads the file from server to its client
             */
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            /*
             * Deletes a file from server
             */
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}