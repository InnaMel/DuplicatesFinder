using System;
using System.Collections.ObjectModel;
using DuplicatesFinder.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicatesFinder.ViewModel
{
    /// <summary>
    /// class to display at the form
    /// </summary>
    public class ObservListWithNameForTemplate
    {
        public string NameOfList { get; set; }

        public ObservableCollection<DuplicatesModel> DuplicatesOfList { get; set; } = new ObservableCollection<DuplicatesModel>();
    }
}
