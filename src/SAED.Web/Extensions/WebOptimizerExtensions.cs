using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebOptimizer;

namespace SAED.Web.Extensions
{
    public static class WebOptimizerExtensions
    {
        public static IAssetPipeline TryGetRelativeScssFiles(this IAssetPipeline pipeline, out IEnumerable<string> realtiveScssFiles, string webRootPath, string stylesFolderRootLocation)
        {
            List<string> allFolderFiles;
            try
            {
                allFolderFiles = Directory.GetFiles(Path.Join(webRootPath, stylesFolderRootLocation)).ToList();
            }
            catch (DirectoryNotFoundException)
            {
                allFolderFiles = Directory.GetFiles(Path.Join(webRootPath, "src", "SAED.Web", "wwwroot", stylesFolderRootLocation)).ToList();
            }
            
            var absoluteScssFiles = allFolderFiles.Where(file => file.EndsWith(".scss"));
            var myRealtiveScssFiles = new List<string>();
            
            foreach (var scssFile in absoluteScssFiles)
            {
                var scssFileLocation = scssFile.LastIndexOf(stylesFolderRootLocation, StringComparison.Ordinal);
                myRealtiveScssFiles.Add(scssFile.Substring(scssFileLocation));
            }

            realtiveScssFiles = myRealtiveScssFiles.AsEnumerable();
            
            return pipeline;
        }
    }
}