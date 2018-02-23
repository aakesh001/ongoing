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
            FarmerName: {
                title: 'Name',
                edit: true,
            },
            DistributorFirstLastName: {
                title: 'Distributor',
                edit: true,
            },
            FarmerCompanyName: {
                title: 'CompanyName',
                edit: true,
            },

            Pono: {
                title: 'Pono',
                list: false
            },

            DistrictId: {
                title: 'DistrictId'
            },
            PhoneNumber: {
                title: 'PhoneNumber'
            },
            StateId: {
                title: 'StateId'
            },
            VillageId: {
                title: 'VillageId'
            },
            MadnalId: {
                title: 'MadnalId'
            },
            Department: {
                title: 'Department'
            },
            Lattitude: {
                title: 'Lattitude'
            },
            Longitude: {
                title: 'Longitude'
            },
            Name: {
                title: 'Device'
            },
            IMEI: {
                title: 'IMEI'
            }
        }
    });
    $('#UserTableContainer').jtable('load');
})