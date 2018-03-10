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
            deleteAction: '/Farmer/DeleteFarmer'
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
            Name: {
                title: 'Device'
            },
            IMEI: {
                title: 'IMEI'
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
            StateName: {
                title: 'State'
            },
            VillageName: {
                title: 'Village'
            },
            MandalName: {
                title: 'Mandal'
            },
            Department: {
                title: 'Department'
            },
            Lattitude: {
                title: 'Lattitude'
            },
            Longitude: {
                title: 'Longitude'
            }
        }
    });
    $('#UserTableContainer').jtable('load');
})