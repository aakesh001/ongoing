$(document).ready(function () {
    $('#UserTableContainer').jtable({
        title: 'Company List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: false, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/Farmer/GetFarmers',
            deleteAction: '/Account/DeleteUser'
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
                edit: true,
            },
            UserName: {
                title: 'UserName'
            },

            Password: {
                title: 'Password'
            },
            Email: {
                title: 'Email'
            },
            PhoneNumber: {
                title: 'PhoneNumber'
            }
        }
    });
    $('#UserTableContainer').jtable('load');
})