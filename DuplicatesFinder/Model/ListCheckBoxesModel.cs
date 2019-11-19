using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicatesFinder.Model
{
    public class CheckBoxesModel
    {
        public string Data { get; set; }
        public bool IsCheckedCheckBox { get; set; }

        public CheckBoxesModel() { }
        public CheckBoxesModel(string data, bool isChecked)
        {
            Data = data;
            IsCheckedCheckBox = isChecked;
        }
    }
}
