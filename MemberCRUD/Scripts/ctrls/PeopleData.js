$(function () {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                type: "POST",
                url: "/PeopleData/PeopleList",
                dataType: "json",
                contentType: "application/json"
            },
            update: {
                type: "POST",
                url: "/PeopleData/Edit",
                dataType: "json",
                contentType: "application/json"
            },
            destroy: {
                type: "POST",
                url: "/PeopleData/Delete",
                dataType: "json",
                contentType: "application/json"
            },
            create: {
                type: "POST",
                url: "/PeopleData/Create",
                dataType: "json",
                contentType: "application/json"
            },
            parameterMap: function (data, operation) {
                if (operation === "read") {
                    return JSON.stringify({ skip: data.skip, take: data.take });
                }
                else if ((operation === "update" || operation === "create") && data) {
                    return JSON.stringify({ peopleData: data });
                }
                else if (operation === "destroy" && data.PeopleID) {
                    return JSON.stringify({ personId: data.PeopleID });
                }
                return data;
            }
        },
        //batch: true,
        schema: {
            //取出資料陣列
            data: function (d) { return d.PeopleDataList; },
            //取出資料總筆數(計算頁數用)
            total: function (d) { return d.Count; },
            model: {
                id: "PeopleID",
                fields: {
                    PeopleID: { editable: false, nullable: true },
                    Depart: { defaultValue: { DepartID: 0, DepartName: "請選擇..." } },
                    UserID: { validation: { required: true } },
                    UserPasd: { validation: { required: true } },
                    Name: { validation: { required: true } },
                    Mail: { type: "email", validation: { required: true } }
                }
            }
        },
        requestEnd: function (e) {
            if (e.response == false) {
                alert('had something error');
                $('#grid').data("kendoGrid").cancelChanges();
                //location.reload();
            }
        },
        pageSize: 10,
        serverPaging: true,
        serverSorting: true,
    });

    $("#grid").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        height: 550,
        toolbar: ["create"],
        columns: [
            { field: "Name", title: "Name" },
            { field: "UserID", title: "UserID" },
            { field: "UserPasd", title: "UserPasd" },
            { field: "Depart", title: "Depart", width: "180px", editor: departDropDownEditor, template: "#=Depart.DepartName#" },
            { field: "Mail", title: "Mail" },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "inline"
    });

    function departDropDownEditor(container, options) {
        $('<input required data-text-field="DepartName" data-value-field="DepartID" data-bind="value:' + options.field + '"/>')
            .appendTo(container)
            .kendoDropDownList({
                autoBind: false,
                dataSource: {
                    type: "json",
                    transport: {
                        read: {
                            type: "POST",
                            url: "/PeopleData/GetDepartList",
                            dataType: "json",
                            contentType: "application/json"
                        },
                    }
                }
            });
    }
});