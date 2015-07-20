using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using WebApp.Helpers;

namespace WebApp.Controls
{
    /// <summary>
    /// Ref: http://geekswithblogs.net/omtalsania7/archive/2012/12/27/how-to-create-an-image-grid-with-asp.net-44.5-using.aspx
    /// </summary>
    public partial class ImageGrid : System.Web.UI.UserControl
    {
        private string virtualPath;
        private string physicalPath;

        /// <summary>
        /// Relative path to the Images Folder
        /// </summary>
        public string ImageFolderPath { get; set; }

        /// <summary>
        /// Title to be displayed on top of Images
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Get or Set the Admin Mode 
        /// </summary>
        public bool AdminMode { get; set; }

        /// <summary>
        /// Page load operations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Update the path
            UpdatePath();

            //Show AdminMode specific controls
            if (AdminMode)
            {
                ImageListView.InsertItemPosition = InsertItemPosition.FirstItem;
            }
        }

        /// <summary>
        /// Pre render operations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //Binds the Data Before Rendering
            BindData();
        }

        /// <summary>
        /// Sets Title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void titleLabel_Load(object sender, EventArgs e)
        {
            var titleLabel = sender as Label;
            if (titleLabel == null) return;

            titleLabel.Text = Title;
        }

        /// <summary>
        /// Enables delete functionality for Admin Mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void deleteLinkButton_Load(object sender, EventArgs e)
        {
            //In case of AdminMode, we would want to show the delete button 
            //which is not visible by iteself for Non-Admin users
            if (AdminMode)
            {
                var deleteButton = sender as LinkButton;
                if (deleteButton == null) return;

                deleteButton.Visible = true;
            }

        }

        /// <summary>
        /// Redirects to the full image when the image is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void itemImageButton_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(e.CommandArgument as string);
        }

        /// <summary>
        /// Performs commands for bound buttons in the ImageListView. In this case 
        /// 'Remove (Delete)'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            /* We have not bound the control to any DataSource derived controls, 
            nor do we use any key to identify the image. Hence, it makes more sense not to 
            use 'delete' but to use a custom command 'Remove' which can be fired as a 
            generic ItemCommand, and the ListViewCommandEventArgs e will have 
            the CommandArgument passed by the 'Remove' button In this case, it is the bound 
            ImageUrl that we are passing, and making use it of to delete the image.*/
            switch (e.CommandName)
            {
                case "Remove":
                    var path = e.CommandArgument as string;
                    if (path != null)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(Server.MapPath(path));
                            fi.Delete();

                            //Display message
                            Parent.Controls.Add(new Label()
                            {
                                Text = GetFileName(path) + " deleted successfully!"
                            });

                        }
                        catch (Exception ex)
                        {
                            Logger.Log(ex.Message);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Saves the Posted File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imageUpload_Load(object sender, EventArgs e)
        {
            //Get the required controls
            var imageUpload = sender as FileUpload;
            if (imageUpload == null) return;

            var parent = imageUpload.Parent;
            if (parent == null) return;

            var imageUploadStatus = parent.FindControl("imageUploadStatusLabel") as Label;
            if (imageUploadStatus == null) return;


            //If a file is posted, save it
            if (this.IsPostBack)
            {
                if (imageUpload.PostedFile != null && imageUpload.PostedFile.ContentLength > 0)
                {
                    try
                    {
                        imageUpload.PostedFile.SaveAs(string.Format("{0}\\{1}",
                            physicalPath, GetFileName(imageUpload.PostedFile.FileName)));
                        imageUploadStatus.Text = string.Format(
                            "Image {0} successfully uploaded!",
                            imageUpload.PostedFile.FileName);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message);
                        imageUploadStatus.Text = string.Format("Error uploading {0}!",
                            imageUpload.PostedFile.FileName);
                    }
                }
                else
                {
                    imageUploadStatus.Text = string.Empty;
                }

            }

        }

        /// <summary>
        /// Get File Name
        /// </summary>
        /// <param name="path">full path</param>
        /// <returns>string containing the file name</returns>
        private string GetFileName(string path)
        {
            DateTime timestamp = DateTime.Now;
            string fileName = string.Empty;
            try
            {
                if (path.Contains('\\')) fileName = path.Split('\\').Last();
                if (path.Contains('/')) fileName = path.Split('/').Last();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
            return fileName;
        }

        /// <summary>
        /// Updates the path variables
        /// </summary>
        private void UpdatePath()
        {
            //use a default path
            virtualPath = "~/Images";
            physicalPath = Server.MapPath(virtualPath);

            //If ImageFolderPath is specified then use that path
            if (!string.IsNullOrEmpty(ImageFolderPath))
            {
                physicalPath = Server.MapPath(ImageFolderPath);
                virtualPath = ImageFolderPath;
            }

        }

        /// <summary>
        /// Binds the ImageListView to current DataSource
        /// </summary>
        private void BindData()
        {
            ImageListView.DataSource = GetListOfImages();
            ImageListView.DataBind();

        }

        /// <summary>
        /// Gets list of images
        /// </summary>
        /// <returns></returns>
        private List<string> GetListOfImages()
        {
            var images = new List<string>();

            try
            {
                var imagesFolder = new DirectoryInfo(physicalPath);
                foreach (var item in imagesFolder.EnumerateFiles())
                {
                    if (item is FileInfo)
                    {
                        //add virtual path of the image to the images list
                        images.Add(string.Format("{0}/{1}", virtualPath, item.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                //log exception
                Logger.Log(ex.Message);
            }

            return images;

        }
    }
}