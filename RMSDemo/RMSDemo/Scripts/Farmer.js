$(document).ready(function () {
    $('#UserTableContainer').jtable({
        title: 'Company List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: true, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/Farmer/GetFarmers',
            deleteAction: '/Farmer/DeleteFarmer',
            updateAction: '/Farmer/UpdateFarmer'
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
                title: 'IMEI',
                edit: false
            },
            DistributorFirstLastName: {
                title: 'Distributor',
                edit: false
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
    //Re-load records when user click 'load records' button.
    $('#LoadRecordsButton').click(function (e) {
        e.preventDefault();
        $('#UserTableContainer').jtable('load', {
            name: $('#name').val()
        });
    });
    //Load all records when page is first shown
})