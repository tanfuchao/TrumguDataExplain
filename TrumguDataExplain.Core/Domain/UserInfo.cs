using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace TrumguDataExplain.Core.Domain
{
    [SugarTable("user_info")]
    public class UserInfo:BaseEntity
    {
        [SugarColumn(ColumnName = "login_name")]
        public string LoginName { get; set; }

        public string Password { get; set; }

        [SugarColumn(ColumnName = "real_name")]
        public string RealName { get; set; }

        public int Flag { get; set; }
    }
}
