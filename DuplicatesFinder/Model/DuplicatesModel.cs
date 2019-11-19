using DuplicatesFinder.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DuplicatesFinder.Model
{
    public class DuplicatesModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }


        /// <summary>
        /// the parameter which helps for searching algorithm
        /// </summary>
        public bool isFinded { get; set; }

        public DuplicatesModel()
        {
            isFinded = false;
        }
    }
}
