var table;
$(document).ready(function () {
    init_nav_height();      //初始化nav高度
    init_jstree();          //初始化jstree
    init_data("0");         //初始化dataTable插件
    Init_Index();           //重绘dataTable 自定义序号
    init_click();           //注入details-control click事件
});

//初始化nav高度
function init_nav_height() {
    var height = $(window).height() - $("#nav").height() - 30;
    $(".row").height(height);
    window.onresize = function () {
        var height = $(window).height() - $("#nav").height() - 10;
        $(".row").height(height);
    };
};

//初始化jstree控件
function init_jstree() {
    $("#jstree").jstree({
        "core": {
            'data': {
                'url': '/Home/GetTreeData',
                'dataType': 'json',
                success: function () {
                    //alert('ok');
                }
            },
            'themes': {
                'name': 'proton',
                'responsive': true,
                "dots": true,
                "icons": true
            }

        }
    });
};

//初始化dataTable插件
function init_data(tableId) {
    table = $('#fieldTable').DataTable({
        "paging": false,
        "ordering": false,
        "bAutoWidth": false,
        "responsive": false,//关闭响应式效果,否则以上设置无效
       // "scrollX": true,//水平滚动
        "info": false,
        "searching": false,
        "ajax": {
            'url': '/Home/GetTableData',
            'dataType': 'json',
            'data': { TableId: tableId }
        },
        "columns": [
            { "data": null, "width":"50px" },
            { "data": "NameEn" },
            { "data": "NameCh"},
            { "data": "FieldType", "width": "100px" },
            { "data": "FieldLength", "width": "50px" },
            { "data": "FieldNote" },
            {
                "data": null,
                "defaultContent": ''
            },
            { "data": "FieldExplain"}
        ],
        "columnDefs": [
            {
                "targets": -1,
                "visible": false
            },
            {
                "targets": -2,
                "width": "50px",
                "createdCell": function (td, cellData, rowData, row, col) {
                    if (cellData.FieldExplain!=null && cellData.FieldExplain.length > 0) {
                        $(td).addClass("details-control");
                    }

                }
            }
        ],
        "order": [[1, 'asc']],
        "language": {
            "processing": "加载中...",
            "lengthMenu": "每页显示 _MENU_ 条数据",
            "zeroRecords": "没有匹配结果",
            "info": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
            "infoEmpty": "显示第 0 至 0 项结果，共 0 项",
            "infoFiltered": "(由 _MAX_ 项结果过滤)",
            "infoPostFix": "",
            "search": "搜索:",
            "url": "",
            "emptyTable": "没有匹配结果",
            "loadingRecords": "载入中..."
        }
    });

}

//重绘dataTable 自定义序号
function Init_Index() {
    table.on('draw.dt',
        function () {
            table.column(0,
                {
                    search: 'applied',
                    order: 'applied'
                }).nodes().each(function (cell, i) {
                    //i 从0开始，所以这里先加1

                    i = i + 1;
                    //服务器模式下获取分页信息，使用 DT 提供的 API 直接获取分页信息

                    var page = table.page.info();
                    //当前第几页，从0开始

                    var pageno = page.page;
                    //每页数据

                    var length = page.length;
                    //行号等于 页数*每页数据长度+行号

                    var columnIndex = (i + pageno * length);
                    cell.innerHTML = columnIndex;
                });
        });
}

//注入details-control click事件
function init_click() {
    $('#fieldTable tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);
        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });
};

//附加信息样式
function format(d) {
    return '<table border="0" style="padding-left:50px;">' +
        '<tr>' +
        '<td style="width:65px;vertical-align:top">说明:</td>' +
        '<td>' + d.FieldExplain + '</td>' +
        '</tr>' +
        '</table>';
}

// jstree点击事件
$('#jstree').on("changed.jstree", function (e, data) {
    init_TableInfo(data.node.id);
    var param = { TableId: data.node.id }
    table.settings()[0].ajax.data = param;
    table.ajax.reload();
    $(".introWrap").css("display", "none");
});

// 所有节点都加载完后
$("#jstree").on("ready.jstree", function (event, data) {
    data.instance.open_node(1); // 展开root节点
    //// 隐藏根节点 http://stackoverflow.com/questions/10429876/how-to-hide-root-node-from-jstree
    $("#1_anchor").css("visibility", "hidden");
    $("li#1").css("position", "relative");
    $("li#1").css("top", "-20px");
    $("li#1").css("left", "-20px");
    $(".jstree-last .jstree-icon").first().hide();
});

//为表格基础项赋值
function init_TableInfo(id) {
    $.ajax({
        url: '/Home/GetTableInfo/',
        type: 'post',
        dataType: 'json',
        data: {
            id: id
        },
        success: function (data) {
            $("#table_name_cn").text(data.table_name_cn);
            $("#table_name").text(data.table_name);
            $("#table_type").text(data.table_type);
            $("#key_name").text(data.key_name);
            $("#table_status").text(data.table_status);
            $("#unique_index").text(data.unique_index);
            $("#description").text(data.description);
        },
        error: function () {
            console.log("服务器错误");
        }
    });
}
