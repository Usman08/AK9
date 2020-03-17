using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace AK9.AppHelper.Utils
{
    public static class Helper
    {
        public static DateTime GetDate()
        {
            return DateTime.Now;
        }

        public static string UploadImage(IFormFile file, string path, string fileName)
        {
            string fileNameWithExtension = fileName + Path.GetExtension(file.FileName);
            string pathWithFileName = Path.Combine(path, fileNameWithExtension);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] files = Directory.GetFiles(path, fileName + ".*");
            foreach (string item in files)
            {
                File.Delete(item);
            }

            if (file.Length > 0)
            {
                using (var stream = new FileStream(pathWithFileName, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return fileNameWithExtension;
        }

        public static string UploadImage(IFormFile file, string path, string fileName, int width, int height, bool maintainAspectRatio = false)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string fileNameWithExtension = fileName + fileExtension;
            string pathWithFileName = Path.Combine(path, fileNameWithExtension);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] files = Directory.GetFiles(path, fileName + ".*");
            foreach (string item in files)
            {
                File.Delete(item);
            }

            if (file.Length > 0)
            {
                using (var stream = new FileStream(pathWithFileName, FileMode.Create))
                {
                    using (Image<Rgba32> image = Image.Load(file.OpenReadStream()))
                    {
                        int destinationWidth = width;
                        int destinationHeight = height;

                        int originalWidth = image.Width;
                        int originalHeight = image.Height;

                        if (maintainAspectRatio && destinationWidth < originalWidth)
                        {
                            double ratio = 0;

                            ratio = (double)destinationWidth / (double)originalWidth;
                            destinationWidth = Convert.ToInt32(originalWidth * ratio);
                            destinationHeight = Convert.ToInt32(originalHeight * ratio);
                        }

                        image.Mutate(x => x.Resize(destinationWidth, destinationHeight));

                        ImageFormatManager imageFormatManager = new ImageFormatManager();
                        imageFormatManager.AddImageFormat(ImageFormats.Jpeg);
                        imageFormatManager.AddImageFormat(ImageFormats.Png);
                        image.Save(stream, imageFormatManager.FindFormatByFileExtension(fileExtension));
                    }
                }
            }

            return fileNameWithExtension;
        }

        public static void DeleteFile(string path, string fileName)
        {
            string pathWithFileName = Path.Combine(path, fileName);

            if (File.Exists(pathWithFileName))
            {
                File.Delete(pathWithFileName);
            }
        }
    }
}
