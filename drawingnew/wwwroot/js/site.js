
$(function () {
    $('#btn_DrawShapeSave').on('click', function () {
        var name = $("#txt_shapeName").val();
        if (ValidateControl(true)) {
            $.ajax({
                url: '/Drawmethods/DrawShapes?handler=SaveDrawShapes',
                //url:'https://localhost:44387/API/DrawShapes/SaveDrawShapes',
                //data: $('#frmdrawshapes').serialize(),
                //data: {
                //    'name': name
                //},
                dataType: 'json',
                type: 'POST'
               // headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() }
            }).done(function (result) {
                if (result.status) {
                    if (result.message == 'Success') {
                       getDimensions();
                        showStatusNotification(true, 'Dimensions saved successfully');

                    }
                } else {
                   // displayProfileValidationSummary(result.errors);
                }
            });
        }
    });
    function ValidateControl() {

        if ($("#txt_shapeName").val() == "") {
            showModal('Person name is required');
           
            return false;
        }
        if ($("#txt_shapeemail").val() == "") {
            showModal('Person email is required');
            return false;
        }
        return true;
    }
    function getDimensions() {

        const option = 1;

        $.ajax({
            url: '/API/DrawShapesController?handler=GetDrawShapes&dimensionId=' + option,
            type: 'GET',
            dataType: 'json'
        }).done(function (data) {

            for (var i = 0; i < data.length; i++) {
                $('#txt_dimensionId').val(data[i].id);
                $('#txt_shapeName').val(data[i].name);
                $("#txt_shapeemail").val(data[i].email);
                $("#canvas_circle").val(data[i].dimensionfield);
                
            }


            data.map(function (clientType) {
                $('#sel_client_type').append('<option value="' + clientType.id + '">' + clientType.name + '</option>');
            });

        });
    }
    $('#canvas_circle').on('MouseDrag',function(e) {
        
        var path = new Path.Circle({
            center: e.downPoint,

            radius: (e.downPoint - e.point).length,

            strokeColor: 'red'
        });
    });

})
