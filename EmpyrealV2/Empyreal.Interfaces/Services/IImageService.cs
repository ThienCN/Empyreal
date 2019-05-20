using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Interfaces.Services
{
    public interface IImageService
    {
        List<Image> GetList(int ProductID);
        Image Get(int ProductID);

    }
}
