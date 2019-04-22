using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace TrumguDataExplain.Core.Domain
{
    [SugarTable("field_info")]
    public class FieldInfo: BaseEntity
    {
        [SugarColumn(ColumnName = "name_en")]
        public string NameEn { get; set; }

        [SugarColumn(ColumnName = "name_ch")]
        public string NameCh { get; set; }

        [SugarColumn(ColumnName = "field_type")]
        public string FieldType { get; set; }

        [SugarColumn(ColumnName = "field_length")]
        public string FieldLength { get; set; }

        [SugarColumn(ColumnName = "field_note")]
        public string FieldNote { get; set; }

        [SugarColumn(ColumnName = "field_explain")]
        public string FieldExplain { get; set; }

        [SugarColumn(ColumnName = "table_id")]
        public int TableId { get; set; }

    }
}
