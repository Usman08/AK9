using Microsoft.AspNetCore.Http;
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
