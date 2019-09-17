using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Empyreal.ViewModels.Display
{
    public class UserBasicViewModel
    {
        public string ID { get; set; }

        public string HoTen { get; set; }

        //[DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Sex { get; set; }

        public int? State { get; set; }

        public List<string> Roles { get; set; }

        public string Filter { get; set; }

        public List<SelectListItem> Filters { get; } = new List<SelectListItem>
        {
            new SelectListItem() { Text = "Tất cả", Value = "all" },
            new SelectListItem() { Text = "Hoạt động", Value = "1" },
            new SelectListItem() { Text = "Ngưng hoạt động", Value = "0" }
        };
    }
}
