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
                    return JSON.stringify({ peopleIDs: data.peopleIDs });
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
        dataSource: dataSource,
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

    // 建立群組
    $("#btnSave").click(function () {
        var factoryId = $("#factoryList").val();
        var groupName = $("#txtGroupName").val();
        var displayedData = $("#grid").data().kendoGrid.dataSource.view();

        $.postJson(
            "/PeopleGroups/CreatePeopleGroup",
            {
                factoryId: factoryId,
                groupName: groupName,
                peopleList: displayedData,
            },
            function (data) {
                if (data == false) {
                    alert("新增群組失敗");
                } else {
                    alert("新增群組完成");
                    location.href = "/PeopleGroups";
                }
            },
            "json"
        );
    });

    // 加入更多
    $("#btnAdd").button().on("click", function () {
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
                    return JSON.stringify({ keyWord: keyWord });
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
        dataSource.read({ peopleIDs: array });
        //$("#grid").data("kendoGrid").refresh();
    }

    // 確定
    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        height: 300,
        width: 350,
        modal: true,
        buttons: {
            "加入": addPeople,
            //"取消": function () {
            //    dialog.dialog("close");
            //}
        }
        //close: function () {
        //}
    });
});