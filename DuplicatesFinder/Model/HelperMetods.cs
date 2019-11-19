using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DuplicatesFinder.ViewModel;

namespace DuplicatesFinder.Model
{
    public class HelperMetods
    {
        /// <summary>
        /// checking the user path existence
        /// </summary>
        public static bool CheckDirectoryExists(string path)
        {
            if (Directory.Exists(path))
                return true;
            else
                return false;
        }


        /// <summary>
        /// getting all files in one list from user path without duplicates
        /// </summary>
        public static List<DuplicatesModel> GetAllFiles(string path)
        {
            List<DuplicatesModel> allFilesAsObjects;
            string[] zeroFile = null;
            string[] innerFolders = null;

            try
            {
                zeroFile = Directory.GetFiles(path);
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine("First time" + " " + ex);
            }

            allFilesAsObjects = new List<DuplicatesModel>();
            if (zeroFile != null)
            {
                foreach (string item in zeroFile)
                {
                    DuplicatesModel newFile = new DuplicatesModel();
                    newFile.FileName = Path.GetFileName(item);
                    newFile.FilePath = Path.GetFullPath(path);
                    allFilesAsObjects.Add(newFile);
                }
            }

            try
            {
                innerFolders = Directory.GetDirectories(path);
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine("Second time" + " " + ex);
            }

            if (innerFolders != null)
            {
                foreach (string item in innerFolders)
                {
                    allFilesAsObjects.AddRange(GetAllFiles(Path.GetFullPath(item)));
                }
            }

            return allFilesAsObjects;
        }



        /// <summary>
        /// getting the list all lists of duplicates
        /// </summary>
        public static List<List<DuplicatesModel>> GetDublicate(List<DuplicatesModel> generalList)
        {
            List<List<DuplicatesModel>> allDublicates = new List<List<DuplicatesModel>>();

            foreach (DuplicatesModel file in generalList)
            {
                if (file.isFinded == false)
                {
                    List<DuplicatesModel> currentListDublicates = new List<DuplicatesModel>();
                    currentListDublicates.Add(file);
                    file.isFinded = true;
                    foreach (DuplicatesModel eachFile in generalList)
                    {
                        if (eachFile.isFinded == false)
                        {
                            if (file.FileName == eachFile.FileName)
                            {
                                currentListDublicates.Add(eachFile);
                                eachFile.isFinded = true;
                            }
                        }
                    }
                    if (currentListDublicates.Count() > 1)
                    {
                        allDublicates.Add(currentListDublicates);
                    }
                }
            }

            return allDublicates;
        }


        /// <summary>
        /// convert list To ObservListWithNameForTemplate
        /// </summary>
        public static ObservListWithNameForTemplate ConvertToCosilyDisplay(List<DuplicatesModel> ListAllFilesAsClass)
        {
            ObservListWithNameForTemplate resultParseTo = new ObservListWithNameForTemplate();
            resultParseTo.NameOfList = ListAllFilesAsClass[0].FileName;
            foreach (DuplicatesModel item in ListAllFilesAsClass)
            {
                resultParseTo.DuplicatesOfList.Add(item);
            }

            return resultParseTo;
        }

    }
}
