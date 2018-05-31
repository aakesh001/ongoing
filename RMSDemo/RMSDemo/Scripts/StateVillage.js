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
            deleteAction: '/StateVillage/DeleteState',
            updateAction: '/StateVillage/UpdateState'
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
                title: 'Name',
                edit: true
            }
        }
    });
    $('#StateTableContainer').jtable('load');

    $('#DistrictTableContainer').jtable({
        title: 'District List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: false, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/StateVillage/GetDistrict',
            deleteAction: '/StateVillage/DeleteDistrict',
            updateAction: '/StateVillage/UpdateDistrict'
        },
        fields: {
            Id: {
                title: 'ID',
                key: true,
                create: false,
                edit: false,
                list: true
            },
            DistrictName: {
                title: 'Name',
                edit: true
            },
            StateID: {
                title: 'StateId',
                edit: true
            }
        }
    });
    $('#DistrictTableContainer').jtable('load');

    $('#VillageTableContainer').jtable({
        title: 'Village List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: true, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/StateVillage/GetVillage',
            deleteAction: '/StateVillage/DeleteVillage',
            updateAction: '/StateVillage/UpdateVillage'
        },
        fields: {
            Id: {
                title: 'ID',
                key: true,
                create: false,
                edit: false,
                list: true
            },
            VillageName: {
                title: 'VillageName',
                edit: true
            },
            StateId: {
                title: 'StateId',
                edit: true
            },

            DistrictID: {
                title: 'DistId',
                edit: true
            }
        }
    });
    $('#VillageTableContainer').jtable('load');

    $('#MandalTableContainer').jtable({
        title: 'Mandal List',
        paging: true, //Enable paging
        pageSize: 10,
        sorting: true, //Enable sorting
        async: false,
        defaultSorting: 'ID ASC',
        actions: {
            listAction: '/StateVillage/GetMandal',
            deleteAction: '/StateVillage/DeleteMandal',
            updateAction: '/StateVillage/UpdateMandal'
        },
        fields: {
            Id: {
                title: 'ID',
                key: true,
                create: false,
                edit: false,
                list: true
            },
            MandalName: {
                title: 'MandalName',
                edit: true
            },
            VillageId: {
                title: 'VillageId',
                edit: true
            },
            DistrictID: {
                title: 'DistrictID',
                edit: true
            },
            StateId: {
                title: 'StateId',
                edit: true
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
                    $("#StateIDDistrict").append
                        ($('<option></option>').val('0').html('Select State'))
                    $("#StateIDMandal").append
                        ($('<option></option>').val('0').html('Select State'))

                    $.each(result.Records, function (i, city) {
                        $("#StateID").append
                            ($('<option></option>').val(city.Id).html(city.StateName))
                        $("#StateIDDistrict").append
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

    $("#StateID").change(function () {
        var stateId = $("#StateID").val();
        getDistrictList(stateId);
    });

    function getDistrictList(stateId) {
        $.ajax
            ({
                url: '/StateVillage/GetDistrictByStateID',
                type: 'POST',
                datatype: 'application/json',
                contentType: 'application/json',
                data: JSON.stringify({
                    stateID: stateId
                }),
                success: function (result) {
                    $("#DistID").html("");

                    console.log(result);
                    $("#DistID").append
                        ($('<option></option>').val('0').html('Select District'))

                    $.each(result.Records, function (i, city) {
                        $("#DistID").append
                            ($('<option></option>').val(city.Id).html(city.DistrictName))
                    })
                },
                error: function () {
                    alert("Whooaaa! Something went wrong..")
                },
            });
    }

    $("#StateIDMandal").change(function () {
        var stateId = $("#StateIDMandal").val();
        getDistrictListforMandal(stateId)
        //getVillageList();
    });
    function getDistrictListforMandal(stateId) {
        $.ajax
            ({
                url: '/StateVillage/GetDistrictByStateID',
                type: 'POST',
                datatype: 'application/json',
                contentType: 'application/json',
                data: JSON.stringify({
                    stateID: stateId
                }),
                success: function (result) {
                    $("#DistIdMandal").html("");

                    console.log(result);
                    $("#DistIdMandal").append
                        ($('<option></option>').val('0').html('Select District'))

                    $.each(result.Records, function (i, city) {
                        $("#DistIdMandal").append
                            ($('<option></option>').val(city.Id).html(city.DistrictName))
                    })
                },
                error: function () {
                    alert("Whooaaa! Something went wrong..")
                },
            });
    }

    $("#DistIdMandal").change(function () {
        getVillageList();
    });

    function getVillageList() {
        var stateIdForMandal = $("#StateIDMandal").val();
        var distIdForMandal = $("#DistIdMandal").val();
        $.ajax
            ({
                url: '/StateVillage/GetVillagefofrMandal',
                type: 'POST',
                datatype: 'application/json',
                contentType: 'application/json',
                data: JSON.stringify({
                    id: stateIdForMandal,
                    distID: distIdForMandal
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