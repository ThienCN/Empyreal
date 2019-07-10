using Empyreal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empyreal.Interfaces.Services
{
    public interface IHistoryService
    {

        List<History> GetByTable(string tableName);

        List<History> GetByDetail(string table, int detail);

        int SaveHistory(History history);

    }
}
