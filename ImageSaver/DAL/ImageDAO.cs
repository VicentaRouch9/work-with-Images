using HtmlAgilityPack;
using ImageSaver.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;

namespace ImageSaver.DAL
{
    public class ImageDAO
    {
        private string connectionString;
        private List<string> _extensions = new List<string>() { ".gif", ".jpg", ".jpeg", ".png" };
        public string ServerPathForDownloads { get; set; }

        public ImageDAO()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
        }
        public ImageDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int InsertImageItem(ImageItem item)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("InsertImageItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@FileName", SqlDbType.NVarChar));
                    cmd.Parameters["@FileName"].Value = item.FileName;
                    cmd.Parameters.Add(new SqlParameter("@Url", SqlDbType.NVarChar));
                    cmd.Parameters["@Url"].Value = item.Url;
                    cmd.Parameters.Add(new SqlParameter("@ImageData", SqlDbType.Image));
                    cmd.Parameters["@ImageData"].Value = item.ImageData;
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                    cmd.Parameters["@ID"].Direction = ParameterDirection.Output;

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return (int)cmd.Parameters["@ID"].Value;
                    }
                    catch (SqlException)
                    {
                        throw new ApplicationException("Data access error");
                    }
                }
            }
        }

        public void DeleteImageItem(ImageItem item)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("DeleteImageItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                    cmd.Parameters["@ID"].Value = item.ID;

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        throw new ApplicationException("Data access error");
                    }
                }
            }
        }

        public ImageItem GetImageItem(int id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("GetImageItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 4));
                    cmd.Parameters["@ID"].Value = id;

                    try
                    {
                        con.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            reader.Read();

                            ImageItem image = new ImageItem
                            {
                                ID = (int)reader["ID"],
                                Url = (string)reader["Url"],
                                FileName = (string)reader["FileName"],
                                ImageData = (byte[])reader["ImageData"]
                            };

                            return image;
                        }
                    }
                    catch (SqlException)
                    {
                        throw new ApplicationException("Data access error");
                    }
                }
            }
        }

        public List<ImageItem> GetAllImageItems()
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("GetAllImageItems", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    List<ImageItem> items = new List<ImageItem>();

                    try
                    {
                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ImageItem taskItem = new ImageItem
                                {
                                    ID = (int)reader["ID"],
                                    Url = (string)reader["Url"],
                                    FileName = (string)reader["FileName"],
                                    ImageData = (byte[])reader["ImageData"]
                                };
                                items.Add(taskItem);
                            }
                            return items;
                        }
                    }
                    catch (SqlException)
                    {
                        throw new ApplicationException("Data access error");
                    }
                }
            }
        }

        public int CountImageItems()
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("CountImageItems", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        con.Open();
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException)
                    {
                        throw new ApplicationException("Data access error");
                    }
                }
            }
        }

        public void DownloadAndSaveAllImages(string from_url, string to_path)
        {
            var url = new Uri(from_url);

            using (var webClient = new WebClient())
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(webClient.DownloadString(url));

                var nodes = doc.DocumentNode.SelectNodes("//img[@src]");

                foreach (var node in nodes)
                {
                    var src_attr = node.Attributes["src"].Value;
                    string extention = Path.GetExtension(src_attr);
                    if (_extensions.Contains(extention))
                    {
                        var fileUrl = new Uri(url, src_attr);
                        string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUrl.ToString());
                        string fullFilePath = Path.Combine(to_path, newFileName);

                        webClient.DownloadFile(fileUrl, fullFilePath);

                        var imageData = File.ReadAllBytes(fullFilePath);

                        InsertImageItem(new ImageItem { FileName = newFileName, Url = fileUrl.ToString(), ImageData = imageData });
                    }
                }
            }
        }

        public void DeleteAllImageItems()
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("DeleteAllImageItems", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        throw new ApplicationException("Data access error");
                    }
                }
            }
        }
        public void DeleteAllImageItemsAndFiles(string path)
        {
            DeleteAllImageItems();

            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                    if (File.Exists(file)) File.Delete(file);
            }
        }
    }
}