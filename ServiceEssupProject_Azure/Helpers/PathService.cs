using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Helpers
{
    public enum Folder
    {
        Images=0,Documents=1, Temporal=2
    }
    public class PathService
    {
        IWebHostEnvironment environment;
        public PathService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public String MapPath(String filename, Folder folder)
        {
            String carpeta="";

            if(folder == Folder.Documents)
            {
                carpeta = "documents";
            }else if(folder == Folder.Images)
            {
                carpeta = "images";
            }else if (folder == Folder.Temporal)
            {
                carpeta = "temporal";
            }
            String path = Path.Combine(this.environment.WebRootPath, carpeta, filename);

            return path;
        }

    }
}
