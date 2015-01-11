$(function () {

    // FactoryDropDownList datasource
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

    // Grid datasource
    var pageSize = 20;
    var currPage = 1;
    var keyWord;
    var factoryId;
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                type: "POST",
                url: "/PeopleGroups/PeopleGroupList",
                dataType: "json",
                contentType: "application/json"
            },
            parameterMap: function (data, operation) {
                if (operation === "read") {
                    currPage = data.page;
                    keyWord = $("#searchBar").val();
                    factoryId = $("#factoryList").val();
                    return JSON.stringify({ skip: data.skip, take: data.take, keyWord: keyWord, factoryId: factoryId });
                }
                return data;
            }
        },
        //batch: true,
        schema: {
            //取出資料陣列
            data: function (d) {
                //var data = d.PeopleGroupDataList;
                //var len = data.length;
                //var dataForDS = [];

                //for (var i = 0; i < len; i++) {
                //    var obj = {
                //        GroupID: data[i].GroupID,
                //        FactoryName: data[i].FactoryName,
                //        GroupName: data[i].GroupName,
                //        GroupOrder: data[i].GroupOrder,
                //        GroupPeopleList: data[i].GroupPeopleList[0] + data[i].GroupPeopleList[1]
                //    }
                //    dataForDS.push(obj);
                //}
                //return dataForDS;
                return d.PeopleGroupDataList;
            },
            //取出資料總筆數(計算頁數用)
            total: function (d) { return d.Count; }//,
            //model: {
            //    id: "GroupID",
            //    fields: {
            //        GroupID: { editable: false, nullable: true },
            //        FactoryName: { validation: { required: true } },
            //        GroupName: { validation: { required: true } },
            //        GroupOrder: { validation: { required: true } },
            //    }
            //}
        },
        pageSize: pageSize,
        serverPaging: true,
        serverSorting: true
    });

    $("#grid").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        height: 550,
        toolbar: [{
            name: "Add",
            text: "Add new record",
        }],
        columns: [
            { field: "FactoryName", title: "廠區名稱" },
            { field: "GroupName", title: "群組名稱" },
            {
                field: "GroupPeopleList", title: "群組成員",
                template: '#= "<span style=\\"color:\\#86B404\\">" + GroupPeopleList[0] + "</span><br />" +' +
                          ' "<span style=\\"color:\\#A4A4A4\\">" + GroupPeopleList[1] + "</span>" #'
            },
            { command: [{ name: "Edit", text: "Edit", click: redirectEdit }], title: "&nbsp;", width: "100px" }]
    });

    $(".k-grid-Add", "#grid").bind("click", function (ev) {
        location.href = "/PeopleGroups/Create";
    });

    function redirectEdit(e) {
        e.preventDefault();

        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        location.href = "/PeopleGroups/Edit?Id=" + dataItem.GroupID;
    }

    // 搜尋button
    $("#btnSearch").click(function () {
        dataSource.read({ skip: 0, take: pageSize });
        sortable();
    });

    // Jquery UI 拖曳排序
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
                    newIndex: newIndex + 1,
                    page: currPage,
                    pageSize: pageSize
                }
            });
        }
    });

    // 只有全部資料顯示時才能拖曳排序，不然排序會有問題
    function sortable() {
        if (keyWord == "" && factoryId == 0) {
            $('#grid tbody').sortable("enable");
        } else {
            $('#grid tbody').sortable("disable");
        }
    }
});