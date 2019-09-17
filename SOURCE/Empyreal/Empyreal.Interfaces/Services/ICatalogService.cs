using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Interfaces.Services
{
    public interface ICatalogService
    {

        List<Catalog> AllCatalog();

        /// <summary>
        /// Lấy danh sách Danh mục theo trạng thái
        /// </summary>
        /// <param name="state">Trạng thái : 1= Tồn tại || 0= Đã xóa</param>
        /// <returns></returns>
        List<Catalog> GetAll(int state);

        Catalog Get(int id);
    }
}
