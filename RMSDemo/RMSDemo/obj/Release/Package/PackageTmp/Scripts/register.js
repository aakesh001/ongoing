$(document).ready(function () {
    $('#UserTableContainer').jtable({
        title: 'Company List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: false, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/Account/GetUsers',
            deleteAction: '/Account/DeleteUser',
            updateAction: '/Account/UpdateUser'
        },
        fields: {
            Id: {
                title: 'ID',
                key: true,
                create: false,
                edit: false,
                list: false
            },
            FirstName: {
                title: 'FirstName',
                edit: true
            },
            UserName: {
                title: 'UserName',
                edit: true
            },

            Password: {
                title: 'Password',
                edit: true
            },
            Email: {
                title: 'Email',
                edit: true
            },
            PhoneNumber: {
                title: 'PhoneNumber',
                edit: true
            }
        }
    });
    $('#UserTableContainer').jtable('load');
})