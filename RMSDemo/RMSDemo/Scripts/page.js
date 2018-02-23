// {
//     "Result": "OK",
//         "Records": [
//             { "PersonId": 1, "Name": "Benjamin Button", "Age": 17, "RecordDate": "\/Date(1320259705710)\/" },
//             { "PersonId": 2, "Name": "Douglas Adams", "Age": 42, "RecordDate": "\/Date(1320259705710)\/" },
//             { "PersonId": 3, "Name": "Isaac Asimov", "Age": 26, "RecordDate": "\/Date(1320259705710)\/" },
//             { "PersonId": 4, "Name": "Thomas More", "Age": 65, "RecordDate": "\/Date(1320259705710)\/" }
//         ]
// }

$(document).ready(function () {
    
    
    $('#PersonTableContainer').jtable({
        title: 'Company List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: false, //Enable sorting
        async:false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/Company/GetCompanies',
            deleteAction: '/Company/DeleteCompany',
            updateAction: '/Company/UpdateCompany'
            
           
        },
        fields: {
            Id: {
                title: 'ID',
                key: true,
                create: false,
                edit: false,
                list: false
            },
            CompanyName: {
                title: 'CompanyName',
                edit: true,
            },
            OrganizationName: {
                title: 'OrganizationName'
            },
           
            Address: {
                title: 'Address'
            },
            Email: {
                title: 'Email'
            },
            PhoneNumber: {
                title: 'PhoneNumber'
            }, LogoPath: {
                title: 'LogoPath',
                display: function (data) {
                    if (data.record.LogoPath === '')
                    {
                        return ' <label>Logo Not Set</label>';
                    }
                    else {
                        return '<img style="width:50px" src=' + '/Uploads/' + data.record.LogoPath + ' />';
                    }

                }
            },
            DistributorID: {
                title: 'Distributor',
                list: false
            }
        },
        formCreated: function (event, data) {
            data.form.find('input[name="CompanyName"]').addClass( 'validate[required]');
            data.form.find('input[name="OrganizationName"]').addClass( 'validate[required]');
            data.form.find('input[name="Email"]').addClass( 'validate[required,custom[email]]');
            data.form.find('input[name="Address"]').addClass('validate[required]');
            data.form.find('input[name="PhoneNumber"]').addClass( 'validate[required]');
            
            data.form.validationEngine();
        },
        formSubmitting: function (event, data) {
            return data.form.validationEngine('validate');
        },
        formClosed: function (event, data) {
            data.form.validationEngine('hide');
            data.form.validationEngine('detach');
        }
    });
    $('#PersonTableContainer').jtable('load');
})