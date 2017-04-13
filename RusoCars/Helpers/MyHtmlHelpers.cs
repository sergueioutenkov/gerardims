using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RusoCars;
using RusoCars.Models;
using System.Web.Routing;
using System.IO;
using System.Web.Helpers;
using RusoCars.DAL;

namespace RusoCars.Helpers
{
    public static class MyHtmlHelpers
    {
        public static HtmlString File(this HtmlHelper helper,
                                                 string type,
                                                 string name)
        {

            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(name))
            {
                TagBuilder fileBuilder = new TagBuilder("input");
                fileBuilder.MergeAttribute("type", type);
                Random r = new Random();
                string s = r.Next(0, 99999999).ToString();
                name = name + s;
                fileBuilder.MergeAttribute("name", name);
                fileBuilder.MergeAttribute("data-val", "true");
                fileBuilder.MergeAttribute("data-val-required", "Debe seleccionar una imagen");
                var ret = new MvcHtmlString(fileBuilder.ToString(TagRenderMode.SelfClosing));
                return ret;
            }
            return null;
        }

        public static HtmlString FileNoValidation(this HtmlHelper helper,
                                             string type,
                                             string name)
        {

            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(name))
            {
                TagBuilder fileBuilder = new TagBuilder("input");
                fileBuilder.MergeAttribute("type", type);
                Random r = new Random();
                string s = r.Next(0, 99999999).ToString();
                name = name + s;
                fileBuilder.MergeAttribute("name", name);
                var ret = new MvcHtmlString(fileBuilder.ToString(TagRenderMode.SelfClosing));
                return ret;
            }
            return null;
        }

        public static HtmlString FilePlusTextBox(this HtmlHelper helper,
                                                 string type,
                                                 string name)
        {

            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(name))
            {
                TagBuilder fileBuilder = new TagBuilder("input");
                fileBuilder.MergeAttribute("type", type);
                Random r = new Random();
                string s = r.Next(0, 99999999).ToString();
                name = name + s;
                fileBuilder.MergeAttribute("name", name);


                // <input type="text" name="lname"><br>
                TagBuilder textBoxBuilder = new TagBuilder("input");
                textBoxBuilder.MergeAttribute("type", "text");
                string name2 = "TextBox" + s;
                textBoxBuilder.MergeAttribute("name", name2);

                var ret = new MvcHtmlString(fileBuilder.ToString(TagRenderMode.SelfClosing) + "<br />" + textBoxBuilder.ToString(TagRenderMode.SelfClosing));
                return ret;
            }
            return null;
        }

    }

    public static class FileHelpers
    {

        public static HttpPostedFileBase UploadFile(HttpPostedFileBase file, string serverPath, string randomString)
        {
            if (file.ContentLength > 0)
            {
                string filePath = Path.Combine(serverPath, randomString + file.FileName);
                file.SaveAs(filePath);
                var webImage = new WebImage(filePath);
                webImage.Resize(800, 600, preserveAspectRatio: true, preventEnlarge: false);
                webImage.Save(filePath);
                return file;
            }
            return null;

        }
        public static void RemoveFile(int? imageId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            if (!imageId.HasValue)
                return;
            CarsRusoEntities db = new CarsRusoEntities();
            Image image = unitOfWork.ImageRepository.GetByID(imageId);
            string realPath = HttpContext.Current.Server.MapPath(image.ImagePath);
            File.Delete(realPath);
        }

        public static void RemoveFile(string imagePath)
        {
            string realPath = HttpContext.Current.Server.MapPath(imagePath);
            File.Delete(realPath);
        }
        public static int SaveImage(HttpPostedFileBase file, string randomString)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            //Save image in DB
            Image image = new Image();
            image.ImageTitle = file.FileName;
            image.ImagePath = "../../Images/" + randomString + file.FileName;
            unitOfWork.ImageRepository.Insert(image);
            unitOfWork.SaveChanges();
            int imageId = unitOfWork.ImageRepository.dbSet.Max<Image>(i => i.ImageId);
            return imageId;
        }

        public static HttpPostedFileBase UploadNewsImage(HttpPostedFileBase file, string serverPath, string randomString)
        {
            if (file.ContentLength > 0)
            {
                string filePath = Path.Combine(serverPath, randomString + file.FileName);
                file.SaveAs(filePath);
                var webImage = new WebImage(filePath);
                webImage.Resize(680,460, preserveAspectRatio: true, preventEnlarge: false);
                webImage.Save(filePath);
                return file;
            }
            return null;

        }
    }
}