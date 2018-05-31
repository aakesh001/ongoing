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
        async: false,
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
                list: true
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
            },
            LogoPath: {
                title: 'LogoPath',
                type: "upload",
                display: function (data) {
                    if (data.record.LogoPath === '') {
                        return ' <label>Logo Not Set</label>';
                    }
                    else {
                        return '<img style="width:50px" src=' + '/Uploads/' + data.record.LogoPath + ' />';
                    }
                },
                input: function (data) {
                    html = '<input type ="file" id="input-image" name="userfile" accept="image/*" />' +
                        '<input type="hidden" name="LogoPath" value="LogoPath">';
                    return html;
                }
            },
            DistributorID: {
                title: 'Distributor',
                list: false
            }
        },
        formCreated: function (event, data) {
            data.form.find('input[name="CompanyName"]').addClass('validate[required]');
            data.form.find('input[name="OrganizationName"]').addClass('validate[required]');
            data.form.find('input[name="Email"]').addClass('validate[required,custom[email]]');
            data.form.find('input[name="Address"]').addClass('validate[required]');
            data.form.find('input[name="PhoneNumber"]').addClass('validate[required]');

            data.form.validationEngine();
            data.form.attr('enctype', 'multipart/form-data');
            $("#input-image").on('change', function () {
                var file = $('#input-image')[0].files[0];
                console.log(file);
                var fd = new FormData();
                fd.append('theFile', file);
                $.ajax
                    ({
                        url: '/Company/Uploadfile',
                        type: 'POST',
                        data: fd,
                        cache: false,
                        contentType: false,
                        processData: false,
                        method: 'POST',
                        type: 'POST',
                        success: function (result) {
                            data.form.find('input[name="LogoPath"]').val(file.name);
                        }
                    });
            });
        },

        formSubmitting: function (event, data) {
            LogoPath = $('input[type=file]').val().split('\\').pop();
            ($("#" + data.form.attr("id")).find('input[name="image"]').val(LogoPath));
            return data.form.validationEngine('validate');
        },
        formClosed: function (event, data) {
            data.form.validationEngine('hide');
            data.form.validationEngine('detach');
        }
    });
    $('#PersonTableContainer').jtable('load');
})