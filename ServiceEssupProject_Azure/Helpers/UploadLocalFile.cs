using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Helpers
{
    public class UploadLocalFile
    {
        PathService Paths;
        public UploadLocalFile(PathService Paths)
        {
            this.Paths = Paths;
        }
        public async Task<String> UploadFileAsync( Folder folder, IFormFile file)
        {
            String pathfile = this.Paths.MapPath(file.FileName, Folder.Temporal);
            using (var stream = new FileStream(pathfile, FileMode.Create))
            {
                await file.CopyToAsync(stream);    
            }
                return pathfile;
        }

        public String GetExtensionImagen(string imagen)
        {
           string img= this.Paths.MapPath(imagen, Folder.Temporal);

           int punto= img.LastIndexOf(".") - 1;
           string extension=imagen.Substring(imagen.LastIndexOf("."));
          
            return extension;
        }
    }
}
