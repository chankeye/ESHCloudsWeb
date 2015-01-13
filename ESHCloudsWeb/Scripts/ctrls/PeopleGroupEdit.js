$(function () {

    // 因為要傳到後端的資料比較複雜，所以需自己組post
    (function ($) {
        $.extend($, {
            postJson: function (url, data, succ) {
                return $.ajax({
                    url: url,
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify(data),
                    dataType: "json",
                    success: succ
                });
            }
        });
    })(jQuery);

    // 解析url
    function urlParams(data) {
        var urlParams = {};
        var e,
            a = /\+/g,  // Regex for replacing addition symbol with a space
            r = /([^&=]+)=?([^&]*)/g,
            d = function (s) { return decodeURIComponent(s.replace(a, " ")); },
            q = window.location.search.substring(1);
        while (e = r.exec(q)) {
            urlParams[d(e[1])] = d(e[2]);
        }

        return urlParams[data];
    }

    var pageId = urlParams("Id");
    if (pageId == null)
        window.location = '/PeopleGroups';

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

    $.ajax({
        type: 'post',
        url: '/PeopleGroups/EditInit',
        data: {
            id: pageId
        },
        success: function (data) {
            if (data.FactoryID != 0) {
                $("#txtGroupName").val(data.GroupName);
                $("#factoryList").data("kendoDropDownList").select(function (item) {
                    return item.FactoryID === data.FactoryID;
                });
            } else {
                alert("找不到此頁");
                window.location = '/PeopleGroups';
            }
        }
    });

    // Grid datasource
    var dataInit = new kendo.data.DataSource({
        transport: {
            read: {
                type: "POST",
                url: "/PeopleGroups/EditPeoplesInit",
                dataType: "json",
                contentType: "application/json"
            },
            parameterMap: function (data, operation) {
                if (operation === "read") {
                    return JSON.stringify({ id: pageId });
                }
                return data;
            },
            schema: {
                //取出資料陣列
                data: function (d) { return d; },
                model: {
                    id: "PeopleID",
                    fields: {
                        PeopleID: { editable: false, nullable: true },
                        DepartName: { editable: false },
                        PeopleName: { editable: false },
                        MailType: { defaultValue: { Name: "TO" } }
                    }
                }
            }
        }
    });

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                type: "POST",
                url: "/PeopleGroups/GetPeopleGroupPeopleList",
                dataType: "json",
                contentType: "application/json"
            },
            parameterMap: function (data, operation) {
                if (operation === "read") {
                    return JSON.stringify({ peopleIDs: data.peopleIDs, selectedList: data.selectedList });
                }
                return data;
            },
            schema: {
                //取出資料陣列
                data: function (d) { return d; },
                model: {
                    id: "PeopleID",
                    fields: {
                        PeopleID: { editable: false, nullable: true },
                        DepartName: { editable: false },
                        PeopleName: { editable: false },
                        MailType: { defaultValue: { Name: "TO" } }
                    }
                }
            }
        }
    });

    $("#grid").kendoGrid({
        dataSource: dataInit,
        height: 300,
        columns: [
            { command: ["destroy"], title: "&nbsp;", width: "100px" },
            { field: "DepartName", title: "部門" },
            { field: "PeopleName", title: "姓名" },
            { field: "MailType", title: "寄件類型", editor: mailTypeDropDownEditor, template: "#=MailType#" }],//template: '#= "<select id=\\"MailType\\" class=\\".check-mailType\\"><option>TO</option><option>CC</option></select>" #' }]
        editable: true
    });

    function mailTypeDropDownEditor(container, options) {
        $('<input required data-text-field="Name" data-value-field="Name" data-bind="value:' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: false,
                dataSource: {
                    type: "json",
                    transport: {
                        read: {
                            type: "POST",
                            url: "/PeopleGroups/GetMailTypeList",
                            dataType: "json",
                            contentType: "application/json"
                        },
                    }
                }
            });
    }

    // 儲存群組
    $("#btnSave").click(function () {
        var factoryId = $("#factoryList").val();
        var groupName = $("#txtGroupName").val();
        var displayedData = $("#grid").data().kendoGrid.dataSource.view();

        $.postJson(
            "/PeopleGroups/EditPeopleGroup",
            {
                id: pageId,
                factoryId: factoryId,
                groupName: groupName,
                peopleList: displayedData,
            },
            function (data) {
                if (data == false) {
                    alert("修改群組失敗");
                } else {
                    alert("修改群組完成");
                    location.href = "/PeopleGroups";
                }
            },
            "json"
        );
    });

    // 取消
    $("#btnCancel").click(function () {
        if (history.length > 1)
            history.back();
        else
            window.location = '/PeopleGroups';
    });

    // 刪除
    $("#btnDelete").click(function () {
        if (confirm('確定要刪除？')) {
            $.ajax({
                type: 'post',
                url: '/PeopleGroups/Delete',
                data: {
                    id: pageId,
                },
                success: function (data) {
                    if (data == false) {
                        alert("此群組已在使用中，無法刪除");
                    } else {
                        alert("刪除完畢");
                        location.href = "/PeopleGroups";
                    }
                }
            });
        }
    });

    // 加入更多
    $("#btnAdd").button().on("click", function () {
        var array = [];
        var displayedData = $("#grid").data().kendoGrid.dataSource.view();
        for (var i = 0; i < displayedData.length; i++) {
            array.push(displayedData[i].PeopleID);
        }
        peopleGridDataSource.read({ peopleIDs: array });
        dialog.dialog("open");
    });

    // ------------------------------------------------------------------

    // PeopleGrid datasource
    var peopleGridDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                type: "POST",
                url: "/PeopleGroups/GetPeopleSelectList",
                dataType: "json",
                contentType: "application/json"
            },
            parameterMap: function (data, operation) {
                if (operation === "read") {
                    var keyWord = $("#searchBar").val();
                    return JSON.stringify({ peopleIDs: data.peopleIDs, keyWord: keyWord });
                }
                return data;
            },
            schema: {
                //取出資料陣列
                data: function (d) { return d; }
            }
        }
    });

    $("#peopleGrid").kendoGrid({
        dataSource: peopleGridDataSource,
        height: 300,
        columns: [
        { field: "Checked", template: "<input name='Checked' class='ob-paid' type='checkbox' data-bind='checked: Checked' #= Checked ? checked='checked' : '' #/>" },
        { field: "Name" },
        { field: "Id" }]
    });

    var peopleGrid = $("#peopleGrid").data().kendoGrid;
    peopleGrid.tbody.on("change", ".ob-paid", function (e) {
        var row = $(e.target).closest("tr");
        var item = peopleGrid.dataItem(row);
        item.set("Checked", $(e.target).is(":checked") ? 1 : 0);
    });

    // 搜尋peopleGrid
    $("#btnSearch").click(function () {
        peopleGridDataSource.read();
    });

    // 以下為dialog的script
    var dialog;

    function addPeople() {
        var array = [];
        var displayedData = $("#peopleGrid").data().kendoGrid.dataSource.view();
        var selectedList = $("#grid").data().kendoGrid.dataSource.view();
        for (var i = 0; i < displayedData.length; i++) {
            if (displayedData[i].Checked == 1) {
                //var PeopleID = displayedData[i].Id;
                //var DepartName = displayedData[i].Name.split(' ')[0];
                //var PeopleName = displayedData[i].Name.split(' ')[1];

                //var data = { PeopleID: PeopleID, DepartName: DepartName, PeopleName: PeopleName };
                //dataSource.push(data);
                array.push(displayedData[i].Id);
            }
        }
        dialog.dialog("close");
        $("#grid").data("kendoGrid").setDataSource(dataSource);
        dataSource.read({ peopleIDs: array, selectedList: selectedList });
        //$("#grid").data("kendoGrid").refresh();
    }

    // 確定
    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        height: 300,
        width: 350,
        modal: false,
        buttons: {
            "加入": addPeople,
            //"取消": function () {
            //    dialog.dialog("close");
            //}
        },
        close: function () {
        }
    });
});