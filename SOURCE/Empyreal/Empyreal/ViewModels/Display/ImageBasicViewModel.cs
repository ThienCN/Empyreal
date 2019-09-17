using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empyreal.ViewModels.Display
{
    public class ImageBasicViewModel
    {

        #region --- Variables ---
        public string ID { get; set; }
        public string Url
        {
            get
            {
                string newUrl = "/images/ImageAvailable.png";
                if (string.IsNullOrEmpty(SetURL))
                    return newUrl;
                return SetURL.ToString();
            }
        }
        public string SetURL { get; set; }
        #endregion --- Variables ---

        #region --- Constructor ---

        public ImageBasicViewModel(Image img)
        {
            this.ID = img.Id.ToString();
            this.SetURL = img.Url;
        }
        public ImageBasicViewModel()
        {
        }

        #endregion --- Constructor ---

    }
}
