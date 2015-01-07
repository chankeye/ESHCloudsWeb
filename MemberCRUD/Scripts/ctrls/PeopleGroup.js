$(function () {
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
                    return JSON.stringify({ skip: data.skip, take: data.take });
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
                    GroupName: { validation: { required: true } },
                    GroupOrder: { validation: { required: true } },
                    CN: { validation: { required: true } },
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
            { field: "GroupName", title: "廠區名稱" },
            { field: "CN", title: "群組名稱" },
            { field: "GroupOrder", title: "Order" },
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
        change: function(event, ui) {
            var start_pos = ui.item.data('start_pos');
            var index = ui.placeholder.index();
            if (start_pos < index) {
                $('#sortable li:nth-child(' + index + ')').addClass('highlights');
            } else {
                $('#sortable li:eq(' + (index + 1) + ')').addClass('highlights');
            }
        },
        update: function (event, ui) {
            var productOrder = $(this).sortable('toArray').toString();
            $.ajax({
                type: 'post',
                url: '/PeopleGroups/DropOrderItem',
                data: {
                    orders: productOrder
                },
                success: function (data) {
                    //location.reload();
                }
            });
        }
    });
});