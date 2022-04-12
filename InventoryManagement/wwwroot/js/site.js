// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Datatable for Role
$(document).ready(function () {
    dataTable = $("#roleDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Roles/ListRole",
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "roleId", "visible": false },
            { "data": "roleName", "name": "Role Name", "autoWidth": true },
            {
                "data": "roleId", "orderable": false, "render": function (data, type, row) {

                    var deleteUrl = "/Roles/Delete/" + row.roleId;
                    if (row.roleName != "SuperAdmin" && row.roleName != "User") {
                        return "<a href='/Roles/Edit/" + row.roleId + "'  class='btn btn-primary btn-sm' style='margin-left:5px' ><i class='far fa-edit'></i> Edit</a><a href='#' class='btn btn-danger btn-sm' style='margin-left:5px' onclick=deleteConfirm('" + deleteUrl + "'); ><i class='far fa-trash-alt'></i> Delete</a>";
                    } else {
                        return "<a href='#' style='display:none' class='btn btn-primary btn-sm'><i class='far fa-edit'></i> Edit</a>";
                    }
                    //} 

                }
            },

        ],
        "language": {

            "emptyTable": "No data found, Please click on <b>Add New</b> Button"
        }
    });
});

//Datatable for ListItem
$(document).ready(function () {
    dataTable = $("#itemDataTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/ListItem/GetItemData",
            "type": "POST",
            "datatype": "json",
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "listItemId", "visible": false },
            { "data": "listItemCategory.listItemCategoryName", "autoWidth": true },
            { "data": "listItemName", "name": "Items", "autoWidth": true },

            {
                "data": "listItemId", "orderable": false, "render": function (data, type, row) {
                    var deleteUrl = "/ListItem/Delete/" + row.listItemId;
                    return "<a href='/ListItem/Edit/" + row.listItemId + "'  class='btn btn-primary btn-sm' style='margin-left:5px' ><i class='far fa-edit'></i> Edit</a><a href='#' class='btn btn-danger btn-sm' style='margin-left:5px' onclick=deleteConfirm('" + deleteUrl + "'); ><i class='far fa-trash-alt'></i> Delete</a>";

                }
            },

        ],
        "language": {

            "emptyTable": "No data found, Please click on <b>Add New</b> Button"
        }
    });
});

//Datatable for Category
$(document).ready(function () {
    dataTable = $("#categoryDataTable").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Category/GetCategoryData",
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "listItemCategoryId", "visible": false },
            { "data": "listItemCategoryName", "autoWidth": true },

            {
                "data": "listItemId", "orderable": false, "render": function (data, type, row) {
                    var deleteUrl = "/Category/Delete/" + row.listItemCategoryId;

                    return "<a href='/Category/Edit/" + row.listItemCategoryId + "'  class='btn btn-primary btn-sm' style='margin-left:5px' ><i class='far fa-edit'></i> Edit</a><a href='#' class='btn btn-danger btn-sm' style='margin-left:5px' onclick=deleteConfirm('" + deleteUrl + "'); ><i class='far fa-trash-alt'></i> Delete</a>";

                    //} 

                }
            },

        ],
        "language": {

            "emptyTable": "No data found, Please click on <b>Add New</b> Button"
        }
    });
});

//Datatable for Employee
$(document).ready(function () {
    dataTable = $("#employeeDataTable").DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Employee/GetEmployeeData",
            "type": "POST",
            "datatype": "json",
        },
        "columns": [
            { "data": "employeeId", "visible": false },
            { "data": "fullName", "autoWidth": true },
            { "data": "email", "autoWidth": true },
            { "data": "person.contactNo", "autoWidth": true },
            { "data": "person.address", "autoWidth": true },
            { "data": "listItem.listItemName", "autoWidth": true },
            { "data": "password", "visible": false },
            {
                "data": "role.roleName", "autoWidth": true, 
            },
            {
                "data": "listItemId", "orderable": false, "render": function (data, type, row) {
                    var deleteUrl = "/Employee/DeleteEmployee/" + row.employeeId;
                    if (row.role.roleName != "SuperAdmin") {
                        return "<a href='/Employee/EditEmployee/" + row.employeeId + "'  class='btn btn-primary btn-sm' style='margin-left:5px' ><i class='far fa-edit'></i> Edit</a><a href='#' class='btn btn-danger btn-sm' style='margin-left:5px' onclick=deleteConfirm('" + deleteUrl + "'); ><i class='far fa-trash-alt'></i> Delete</a>";
                    } else {
                        return "<a href='/Employee/EditEmployee/" + row.employeeId + "'  class='btn btn-primary btn-sm' style='margin-left:5px' ><i class='far fa-edit'></i> Edit</a>";
                    }
                    //} 

                }
            },

        ],
        "language": {

            "emptyTable": "No data found, Please click on <b>Add New</b> Button"
        }
    });
});


var deleteConfirm = function (val) {
    $('#idToDelete').text(val);
    $('#delete-conformation').modal('show');
    console.log(val);
}


function deleteData() {
    var url = $('#idToDelete').text();
    console.log(url);
    $.ajax({
        type: "POST",
        url: url,
        success: function (data) {
            if (data.success === true) {
                $("#delete-conformation").modal('hide');
                $('#categoryDataTable').DataTable().ajax.reload();
                $('#roleDatatable').DataTable().ajax.reload();
                $('#itemDataTable').DataTable().ajax.reload();
                $('#employeeDataTable').DataTable().ajax.reload();

                toastr.success(data.message, 'Success Alert', { timeout: 300 });
            } else {
                $("#delete-conformation").modal('hide');
                toastr.error(data.message, 'Warning Alert', { timeout: 300 });
            }
        },
        error: function () {

            toastr.error(data.message, 'Error Alert', { timeout: 300 });
        }
    });
}