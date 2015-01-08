$(function () {

    var factoryDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                type: "POST",
                url: "/PeopleGroups/GetFactoryList",
                dataType: "json",
                contentType: "application/json"
            }
        }
    });

    $("#factoryList").kendoDropDownList({
        dataSource: factoryDataSource,
        dataTextField: "FactoryName",
        dataValueField: "FactoryID",
    });


    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                type: "POST",
                url: "/PeopleGroups/PeopleGroupList",
                dataType: "json",
                contentType: "application/json"
            },
            create: {
                type: "POST",
                url: "/PeopleGroups/Create",
                dataType: "json",
                contentType: "application/json"
            },
            parameterMap: function (data, operation) {
                if (operation === "read") {
                    var keyWord = $("#searchBar").val();
                    var factoryId = $("#factoryList").val();//$("#factoryList").data("kendoDropDownList").value();
                    return JSON.stringify({ skip: data.skip, take: data.take, factoryId:factoryId, keyWord: keyWord });
                }
                else if (operation === "create" && data) {
                    return JSON.stringify({ peopleData: data });
                }
                return data;
            }
        },
        //batch: true,
        schema: {
            //取出資料陣列
            data: function (d) { return d.PeopleGroupDataList; },
            //取出資料總筆數(計算頁數用)
            total: function (d) { return d.Count; },
            model: {
                id: "GroupID",
                fields: {
                    GroupID: { editable: false, nullable: true },
                    FactoryName: { validation: { required: true } },
                    GroupName: { validation: { required: true } },
                    GroupOrder: { validation: { required: true } },
                }
            }
        },
        pageSize: 10,
        serverPaging: true,
        serverSorting: true
    });

    $("#grid").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        height: 550,
        toolbar: ["create"],
        columns: [
            { field: "GroupOrder", title: "排序" },
            { field: "FactoryName", title: "廠區名稱" },
            { field: "GroupName", title: "群組名稱" },
            { command: ["edit"], title: "&nbsp;", width: "250px" }]
    });

    $('#grid tbody').sortable({
        opacity: 0.6,
        cursor: 'move',
        axis: 'y',
        start: function (event, ui) {
            var start_pos = ui.item.index();
            ui.item.data('start_pos', start_pos);
        },
        update: function (event, ui) {
            //var productOrder = $(this).sortable('toArray').toString();
            var oldIndex = ui.item.data('start_pos');
            var newIndex = ui.item.context.rowIndex;
            $.ajax({
                type: 'post',
                url: '/PeopleGroups/DropOrderItem',
                data: {
                    oldIndex: oldIndex + 1,
                    newIndex: newIndex + 1
                },
                success: function (data) {
                    //location.reload();
                }
            });
        }
    });

    $("#btnSearch").click(function () {
        //要求資料來源重新讀取(並指定切至第一頁)
        dataSource.read();
        //Grid重新顯示資料 2013-07-19更正，以下可省略
        //$("#dvGrid").data("kendoGrid").refresh();
    });
});