$(document).ready(function () {
    var lineNo;
    $('#UserTableContainer').jtable({
        title: 'Dashboard',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: true, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/Company/GetDashboardData'
        },
        fields:
        {
            SrNo: {
                title: 'SNo.',
                list: true,
                create: false,
                edit: false,
                sorting: false,
            },
            Name: {
                title: 'Name'
            },
            IMEI: {
                title: 'IMEI'
            },
            Frquency: {
                title: 'Fr'
            },
            DcBusVoltage: {
                title: 'DcB'
            },
            Current: {
                title: 'Cur'
            },
            InPower: {
                title: 'IP'
            },
            OutVolt: {
                title: 'OV'
            },
            LastUpdate: {
                title: 'LU'
            },
            TotalEnergy: {
                title: 'TE'
            },
            TotalWaterFLOw: {
                title: 'TW'
            },
            FarmerName: {
                title: 'FarmerName'
            },
            District: {
                title: 'Dis'
            },
            PhoneNumber: {
                title: 'PhNo.'
            },
            StateName: {
                title: 'State'
            },
            VillageName: {
                title: 'Village'
            },
            MandalName: {
                title: 'Mandal'
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
        lineNo = 0;
    });
})