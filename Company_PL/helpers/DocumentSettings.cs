namespace Company_PL.helpers
{
    public static class DocumentSettings
    {
        //upload //string==>imgname
        public static string UploadFile(IFormFile file, string folderName)
        {
            //file path

            //1.get folder location
            //string FolderPath = "D:\\fullstack\\backend dot net\\CompanyProject\\Company_PL\\Company_PL\\wwwroot\\files\\images\\"+folderName;
            //var FolderPath = Directory.GetCurrentDirectory()+ "\\wwwroot\\files"+folderName;
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            //2.file name and make it unique
            var FileName = $"{Guid.NewGuid()}{file.FileName}";

            //3.copy
            var filePath = Path.Combine(FolderPath, FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return FileName;

        }


        //delete

        public static void DeleteFile(string filename,string foldername)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername, filename);


            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
}
}