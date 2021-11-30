using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.ViewModels
{
    public class EditImageViewModel : UploadImageViewModel
    {
        public int ID { get; set; }
        public string ExistingImage { get; set; }
    }
}
