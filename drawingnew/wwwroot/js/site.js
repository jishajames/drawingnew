
//$(function () {

   

//})
$('#btn_DrawShapeSave').on('click', function (e) {
    e.preventDefault();
    var id = $("#txt_dimensionId").val();
    if (id != '') {
        id = parseInt(id);
    }
    else {
        id = 0;
    }
    var name = $("#txt_shapeName").val();
    var email = $("#txt_shapeemail").val();
    var imageData = ctx.getImageData(0, 0, canvas.width, canvas.height);
    var output = '';
    for (var i = 0; i < imageData.data.length; i += 4) {
        var red = imageData.data[i];
        var green = imageData.data[i + 1];
        var blue = imageData.data[i + 2];
        var alpha = imageData.data[i + 3];
        output += '(' + red + ',' + green + ',' + blue + ',' + alpha + ') ';
    }
    var object = {
        id:id,
        'name': name,
        'email': email,
        'dimensionalfield': output
    }
    if (ValidateControl(true)) {
        $.ajax({
            url: '/Drawmethods/DrawShapes?handler=SaveDrawShapes',
            //url:'http://localhost:44387/API/DrawShapes/SaveDrawShapes',
            /*data: $('#frmdrawshapes').serialize(),*/
            data: {
                'id':id,
                'name': name,
                'email': email,
                'dimensionalfield': output
            },
            dataType: 'json',
            type: 'POST',
            headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() }
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
const showStatusNotification = function (success, message) {
    if (success) {
        $('.toast .toast-header strong').removeClass('text-danger').addClass('text-success').html('Success');
    } else {
        $('.toast .toast-header strong').removeClass('text-success').addClass('text-danger').html('Error');
    }
    $('.toast .toast-body').html(message);
    $('.toast').toast('show');
}
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
getDimensions();
function getDimensions() {

    const option = 1;

    $.ajax({
        url: '/Drawmethods/DrawShapes?handler=DrawShapes',
        type: 'GET',
        dataType: 'json'
    }).done(function (data) {

        for (var i = 0; i < data.length; i++) {
            $('#txt_dimensionId').val(data[i].id);
            $('#txt_shapeName').val(data[i].name);
            $("#txt_shapeemail").val(data[i].email);
            $("#canvas_circle").val(data[i].dimensionfield);
            for (var i = 0; i < data[i].dimensionfield.length; i++) {
                var barHeight = data[i].dimensionfield;
                var x = i * barWidth;
                var y = canvas.height - barHeight;

                // Draw the bar
                ctx.fillStyle = 'blue';
                ctx.fillRect(x, y, barWidth, barHeight);
            }

        }


        data.map(function (clientType) {
            $('#sel_client_type').append('<option value="' + clientType.id + '">' + clientType.name + '</option>');
        });

    });
}

var canvas = document.getElementById('canvas_circle');
var ctx = canvas.getContext('2d');
var isDrawing = false;
var lastX, lastY;
var shapeType;
// Button click handlers
$('#drawCircleBtn').click(function (e) {
    e.preventDefault();
    shapeType = 'circle';
    $('#drawCircleBtn').addClass('.btn-color');
    $('#drawLineBtn').removeClass('.btn-color')
});

$('#drawLineBtn').click(function (e) {
    e.preventDefault();
    shapeType = 'line';
    $('#drawCircleBtn').removeClass('.btn-color')
    $('#drawLineBtn').addClass('.btn-color')
});
$('#drawClearBtn').click(function (e) {
    e.preventDefault();
    clearCanvas();
});
// Event handlers
$(canvas).mousedown(function (e) {
    isDrawing = true;
    [lastX, lastY] = [e.offsetX, e.offsetY];
    ctx.beginPath();
}).mousemove(function (e) {
    if (!isDrawing) return;
    var x = e.offsetX;
    var y = e.offsetY;
    if (shapeType === 'line') {
        drawLine(x, y);
    } else if (shapeType === 'circle') {
        drawCircle(x, y);
    }
}).mouseup(function () {
    isDrawing = false;
    ctx.closePath();
}).mouseleave(function () {
    isDrawing = false;
    ctx.closePath();
});



// Draw line between last position and current position
function drawLine(x, y) {
    ctx.moveTo(lastX, lastY);
    ctx.lineTo(x, y);
    ctx.stroke();
    [lastX, lastY] = [x, y];
}

// Draw circle centered at last position with current position as radius
function drawCircle(x, y) {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    var radius = Math.sqrt(Math.pow(x - lastX, 2) + Math.pow(y - lastY, 2));
    ctx.beginPath();
    ctx.arc(lastX, lastY, radius, 0, 2 * Math.PI);
    ctx.stroke();
}
function drawShape(x, y) {
    ctx.beginPath();
    ctx.moveTo(lastX, lastY);
    ctx.lineTo(x, y);
    ctx.stroke();
    ctx.closePath();
}
function clearCanvas() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
}