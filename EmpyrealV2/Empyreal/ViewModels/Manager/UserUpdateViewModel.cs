using Empyreal.ViewModels.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Empyreal.ViewModels.Manager
{
    public class UserUpdateViewModel : ManagerBaseViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên"), MaxLength(100, ErrorMessage = "Họ tên không quá 100 ký tự")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email tối đa 100 ký tự"), MaxLength(100, ErrorMessage = "Email tối đa 100 ký tự")]
        public string Email { get; set; }

        public bool isChangePass { get; set; }

        [Required(ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự"), DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tháng sinh")]
        public string MonthOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn năm sinh")]
        public string YearOfBirth { get; set; }

        public bool IsSuccess { get; set; }

        public string UserRole { get; set; }

        public IEnumerable<SelectListItem> UserRoles { get; set; }

        #region Data of Dropdownlist
        public List<SelectListItem> Dates { get; } = new List<SelectListItem>
        {
            new SelectListItem() { Text = "Ngày", Value = string.Empty },
            new SelectListItem() { Text = "01", Value = "01" },
            new SelectListItem() { Text = "02", Value = "02" },
            new SelectListItem() { Text = "03", Value = "03" },
            new SelectListItem() { Text = "04", Value = "04" },
            new SelectListItem() { Text = "05", Value = "05" },
            new SelectListItem() { Text = "06", Value = "06" },
            new SelectListItem() { Text = "07", Value = "07" },
            new SelectListItem() { Text = "08", Value = "08" },
            new SelectListItem() { Text = "09", Value = "09" },
            new SelectListItem() { Text = "10", Value = "10" },
            new SelectListItem() { Text = "11", Value = "11" },
            new SelectListItem() { Text = "12", Value = "12" },
            new SelectListItem() { Text = "13", Value = "13" },
            new SelectListItem() { Text = "14", Value = "14" },
            new SelectListItem() { Text = "15", Value = "15" },
            new SelectListItem() { Text = "16", Value = "16" },
            new SelectListItem() { Text = "17", Value = "17" },
            new SelectListItem() { Text = "18", Value = "18" },
            new SelectListItem() { Text = "19", Value = "19" },
            new SelectListItem() { Text = "20", Value = "20" },
            new SelectListItem() { Text = "21", Value = "21" },
            new SelectListItem() { Text = "22", Value = "22" },
            new SelectListItem() { Text = "23", Value = "23" },
            new SelectListItem() { Text = "24", Value = "24" },
            new SelectListItem() { Text = "25", Value = "25" },
            new SelectListItem() { Text = "26", Value = "26" },
            new SelectListItem() { Text = "27", Value = "27" },
            new SelectListItem() { Text = "28", Value = "28" },
            new SelectListItem() { Text = "29", Value = "29" },
            new SelectListItem() { Text = "30", Value = "30" },
            new SelectListItem() { Text = "31", Value = "31" }
        };

        public List<SelectListItem> Months { get; } = new List<SelectListItem>
        {
            new SelectListItem() { Text = "Tháng", Value = string.Empty },
            new SelectListItem() { Text = "01", Value = "01" },
            new SelectListItem() { Text = "02", Value = "02" },
            new SelectListItem() { Text = "03", Value = "03" },
            new SelectListItem() { Text = "04", Value = "04" },
            new SelectListItem() { Text = "05", Value = "05" },
            new SelectListItem() { Text = "06", Value = "06" },
            new SelectListItem() { Text = "07", Value = "07" },
            new SelectListItem() { Text = "08", Value = "08" },
            new SelectListItem() { Text = "09", Value = "09" },
            new SelectListItem() { Text = "10", Value = "10" },
            new SelectListItem() { Text = "11", Value = "11" },
            new SelectListItem() { Text = "12", Value = "12" }
        };

        public List<SelectListItem> Years { get; } = new List<SelectListItem>
        {
            new SelectListItem() { Text = "Năm", Value = string.Empty },
            new SelectListItem() { Text = "2019", Value = "2019" },
            new SelectListItem() { Text = "2018", Value = "2018" },
            new SelectListItem() { Text = "2017", Value = "2017" },
            new SelectListItem() { Text = "2016", Value = "2016" },
            new SelectListItem() { Text = "2015", Value = "2015" },
            new SelectListItem() { Text = "2014", Value = "2014" },
            new SelectListItem() { Text = "2013", Value = "2013" },
            new SelectListItem() { Text = "2012", Value = "2012" },
            new SelectListItem() { Text = "2011", Value = "2011" },
            new SelectListItem() { Text = "2010", Value = "2010" },
            new SelectListItem() { Text = "2009", Value = "2009" },
            new SelectListItem() { Text = "2008", Value = "2008" },
            new SelectListItem() { Text = "2007", Value = "2007" },
            new SelectListItem() { Text = "2006", Value = "2006" },
            new SelectListItem() { Text = "2005", Value = "2005" },
            new SelectListItem() { Text = "2004", Value = "2004" },
            new SelectListItem() { Text = "2003", Value = "2003" },
            new SelectListItem() { Text = "2002", Value = "2002" },
            new SelectListItem() { Text = "2001", Value = "2001" },
            new SelectListItem() { Text = "2000", Value = "2000" },
            new SelectListItem() { Text = "1999", Value = "1999" },
            new SelectListItem() { Text = "1998", Value = "1998" },
            new SelectListItem() { Text = "1997", Value = "1997" },
            new SelectListItem() { Text = "1996", Value = "1996" },
            new SelectListItem() { Text = "1995", Value = "1995" },
            new SelectListItem() { Text = "1994", Value = "1994" },
            new SelectListItem() { Text = "1993", Value = "1993" },
            new SelectListItem() { Text = "1992", Value = "1992" },
            new SelectListItem() { Text = "1991", Value = "1991" },
            new SelectListItem() { Text = "1990", Value = "1990" },
            new SelectListItem() { Text = "1989", Value = "1989" },
            new SelectListItem() { Text = "1988", Value = "1988" },
            new SelectListItem() { Text = "1987", Value = "1987" },
            new SelectListItem() { Text = "1986", Value = "1986" },
            new SelectListItem() { Text = "1985", Value = "1985" },
            new SelectListItem() { Text = "1984", Value = "1984" },
            new SelectListItem() { Text = "1983", Value = "1983" },
            new SelectListItem() { Text = "1982", Value = "1982" },
            new SelectListItem() { Text = "1981", Value = "1981" },
            new SelectListItem() { Text = "1980", Value = "1980" },
            new SelectListItem() { Text = "1979", Value = "1979" },
            new SelectListItem() { Text = "1978", Value = "1978" },
            new SelectListItem() { Text = "1977", Value = "1977" },
            new SelectListItem() { Text = "1976", Value = "1976" },
            new SelectListItem() { Text = "1975", Value = "1975" },
            new SelectListItem() { Text = "1974", Value = "1974" },
            new SelectListItem() { Text = "1973", Value = "1973" },
            new SelectListItem() { Text = "1972", Value = "1972" },
            new SelectListItem() { Text = "1971", Value = "1971" },
            new SelectListItem() { Text = "1970", Value = "1970" },
            new SelectListItem() { Text = "1969", Value = "1969" },
            new SelectListItem() { Text = "1968", Value = "1968" },
            new SelectListItem() { Text = "1967", Value = "1967" },
            new SelectListItem() { Text = "1966", Value = "1966" },
            new SelectListItem() { Text = "1965", Value = "1965" },
            new SelectListItem() { Text = "1964", Value = "1964" },
            new SelectListItem() { Text = "1963", Value = "1963" },
            new SelectListItem() { Text = "1962", Value = "1962" },
            new SelectListItem() { Text = "1961", Value = "1961" },
            new SelectListItem() { Text = "1960", Value = "1960" },
            new SelectListItem() { Text = "1959", Value = "1959" },
            new SelectListItem() { Text = "1958", Value = "1958" },
            new SelectListItem() { Text = "1957", Value = "1957" },
            new SelectListItem() { Text = "1956", Value = "1956" },
            new SelectListItem() { Text = "1955", Value = "1955" },
            new SelectListItem() { Text = "1954", Value = "1954" },
            new SelectListItem() { Text = "1953", Value = "1953" },
            new SelectListItem() { Text = "1952", Value = "1952" },
            new SelectListItem() { Text = "1951", Value = "1951" },
            new SelectListItem() { Text = "1950", Value = "1950" }
        };

        public string Sex { get; set; }

        public List<Sex> SexList { get; } = new List<Sex>
        {
            new Sex { Text = "Nam", Value = "Nam" },
            new Sex { Text = "Nữ", Value = "Nữ" }
        };
        #endregion
    }

    public class Sex
    {
        public string Value { get; set; }

        public string Text { get; set; }
    }
}
