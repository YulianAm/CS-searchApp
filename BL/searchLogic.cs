using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Threading;
using DAL;

namespace BL
{

  



    public static class Search
    {


        public static void addPathDB(string resultFilePath, string searchTermUser, string searchPathUser, DateTime timeStamp, string searchType)
        {

            var ent = SearchDB.ent;


            searchLog t = new searchLog()
            {
                searchTerm = searchTermUser,
                searchPath = searchPathUser,
                resultFilePath = resultFilePath,
                DateSearched = timeStamp,
                SearchType = searchType
            };

            try
            {
                ent.searchLogs.Add(t);
                ent.SaveChanges();
            }
            catch (Exception)
            {

                throw ;
            }
            




        }

        public static IEnumerable<string> SearchFiles(string root, string searchTerm)
        {

            var files = new List<string>();
            var messages = new List<string>();
            var results = files.Concat(messages);

            try
            {
                IEnumerable<string> listfiles = Directory.EnumerateFiles(root).Select(e => e.ToLower()).
                    Where(m => m.Contains(searchTerm.ToLower()));
                //add paths conatining the file to lsit 'files' - per run 

                foreach (var path in listfiles)
                {
                    var directory = Path.GetDirectoryName(path);

                    

                    //test to see if the file found is folder or not - we do not want folders to display and logged ONLY files!
                    if (Path.GetFileName(path).Contains(searchTerm))
                    {
                        
                        files.Add(path);
                    }


                    // including folders

                    //if (directory.Contains(searchTerm))
                    //{
                    //    Boolean isDirAlreadyInList = false;



                    //    foreach (var file in files)
                    //    {
                    //        if (file.Contains(directory))
                    //        {
                    //            isDirAlreadyInList = true;
                    //            break;
                    //        }
                    //        else { continue; }


                    //    }

                    //    if (!isDirAlreadyInList) { files.Add(directory);  }

                    //}


                }


            }
            catch (Exception)
            {
                messages.Add("no files found!");



            }


            try
            {
                //next run to search inside existing path folder (go one step deeper)
                foreach (var subDir in Directory.EnumerateDirectories(root))
                {
                    try
                    {
                        files.AddRange(SearchFiles(subDir, searchTerm));
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        //if not access to folder by admin
                        messages.Add("no access to required folder:" + ex.Message + " manage permissions and make sure you're admin");
                        addPathDB("N/A", searchTerm, root, DateTime.Now, "search term + path - no access");
                        
                        
                    }
                }
            }
            catch (DirectoryNotFoundException dirEx)
            {
                messages.Add("Directory not found: " + dirEx.Message);
                addPathDB("N/A", searchTerm, root, DateTime.Now, "search term + path - broken path");
            }


            return results;

        }
    }
}

