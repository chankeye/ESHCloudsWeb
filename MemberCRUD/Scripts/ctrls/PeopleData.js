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
                    CN: { validation: { required: true } },
                    Depart: { defaultValue: { DepartID: 0, DepartName: "Please wait" } },
                    Pasd: { validation: { required: true } },
                    Name: { validation: { required: true } },
                    Mail: { type: "email", validation: { required: true } },
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
            "Name",
            { field: "CN", title: "CN" },
            { field: "Depart", title: "Depart", width: "180px", editor: departDropDownEditor, template: "#=Depart.DepartName#" },
            { field: "Pasd", title: "Pasd" },
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