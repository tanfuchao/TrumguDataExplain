using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace TrumguDataExplain.Core.Domain
{
    [SugarTable("table_info")]
    public class TableInfo:BaseEntity
    {
        [SugarColumn(ColumnName = "name_ch")]
        public string NameCh { get; set; }

        [SugarColumn(ColumnName = "parent_id")]
        public int ParentId { get; set; }

        [SugarColumn(ColumnName = "depth")]
        public int Depth { get; set; }

        [SugarColumn(ColumnName = "tree_state")]
        public int TreeState { get; set; }

        [SugarColumn(ColumnName = "tree_priority")]
        public int TreePriority { get; set; }

        [SugarColumn(ColumnName = "name_en")]
        public string NameEn { get; set; }

        [SugarColumn(ColumnName = "table_type")]
        public string TableType { get; set; }

        [SugarColumn(ColumnName = "table_key")]
        public string TableKey { get; set; }

        [SugarColumn(ColumnName = "table_state")]
        public int TableState { get; set; }

        [SugarColumn(ColumnName = "table_index")]
        public string TableIndex { get; set; }

        [SugarColumn(ColumnName = "table_explain")]
        public string TableExplain { get; set; }

        [SugarColumn(ColumnName = "tree_icon")]
        public string TreeIcon { get; set; }
    }
}
