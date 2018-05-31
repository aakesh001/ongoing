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
    $('#StateTableContainer').jtable({
        title: 'State List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: false, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/StateVillage/GetStates',
            deleteAction: '/StateVillage/DeleteState'
        },
        fields: {
            Id: {
                title: 'ID',
                key: true,
                create: false,
                edit: false,
                list: true
            },
            StateName: {
                title: 'Name'
            }
        }
    });
    $('#StateTableContainer').jtable('load');

    $('#VillageTableContainer').jtable({
        title: 'Village List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: false, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/StateVillage/GetVillage',
            deleteAction: '/StateVillage/DeleteVillage'
        },
        fields: {
            Id: {
                title: 'ID',
                key: true,
                create: false,
                edit: false,
                list: true
            }, VillageName: {
                title: 'VillageName'
            },
            StateId: {
                title: 'StateId'
            }
        }
    });
    $('#VillageTableContainer').jtable('load');

    $('#MandalTableContainer').jtable({
        title: 'Mandal List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: false, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/StateVillage/GetMandal',
            deleteAction: '/StateVillage/DeleteMandal'
        },
        fields: {
            Id: {
                title: 'ID',
                key: true,
                create: false,
                edit: false,
                list: false
            },
            MandalName: {
                title: 'MandalName'
            },
            VillageId: {
                title: 'VillageId'
            },
            StateId: {
                title: 'StateId'
            }
        }
    });
    $('#MandalTableContainer').jtable('load');
    function getStateList() {
        $.ajax
            ({
                url: '/StateVillage/GetStates',
                type: 'POST',
                datatype: 'application/json',
                success: function (result) {
                    $("#StateID").html("");
                    $("#StateIDMandal").html("");

                    console.log(result);
                    $("#StateID").append
                        ($('<option></option>').val('0').html('Select State'))

                    $("#StateIDMandal").append
                        ($('<option></option>').val('0').html('Select State'))

                    $.each(result.Records, function (i, city) {
                        $("#StateID").append
                            ($('<option></option>').val(city.Id).html(city.StateName))
                        $("#StateIDMandal").append
                            ($('<option></option>').val(city.Id).html(city.StateName))
                    })
                },
                error: function () {
                    alert("Whooaaa! Something went wrong..")
                },
            });
    }
    getStateList();
    $("#StateIDMandal").change(function () {
        getVillageList();
    });

    function getVillageList () {
        var stateIdForMandal = $("#StateIDMandal").val();
        $.ajax
            ({
                url: '/StateVillage/GetVillagefofrMandal',
                type: 'POST',
                datatype: 'application/json',
                contentType: 'application/json',
                data: JSON.stringify({
                    id: stateIdForMandal
                }),
                success: function (result) {
                    $("#VillageIdMandal").html("");

                    console.log(result);
                    $("#VillageIdMandal").append
                        ($('<option></option>').val('0').html('Select Village'))

                    $.each(result.Records, function (i, city) {
                        $("#VillageIdMandal").append
                            ($('<option></option>').val(city.Id).html(city.VillageName))
                    })
                },
                error: function () {
                    alert("Whooaaa! Something went wrong..")
                },
            });
    }
})