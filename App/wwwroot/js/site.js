

const url = 'https://localhost:7160/api/';
var options = [];
var startAngle = 0;
var arc = Math.PI / (options.length / 2);
var spinTimeout = null;
var roulette = {};
var spinArcStart = 10;
var spinTime = 0;
var spinTimeTotal = 0;

var ctx;
document.getElementById("spin").addEventListener("click", ValidBet);
const addRulette = () => {
    $("#modal-Rulette").modal("show");

}

const addRuletteSubmit = () => {

    data = {
        numMin: $("#minVal").val(),
        numMax: $("#maxVal").val()
    }

    $.ajax({
        type: "POST",                                              
        url: `${url}Bet`,                    
        data: JSON.stringify(data),                                                
        contentType: "application/json; charset=utf-8",            
        dataType: "json",                                                                                   
        success: function (resultado) {                            
            
            Swal.fire({
                icon: "success",
                title: "Id de Ruleta: " + resultado,

            });
           
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { 
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: XMLHttpRequest.responseText,
               
            });
        }
    });
};

const GetRoulette = (id = null) => {
    if(id==null) id = $("#routteId").val();
    let data = {
        id:id
    }
    $.ajax({
        type: "GET",
        url: `${url}Bet/Roulette/${id}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resultado) {
            roulette = resultado;
            printRollete(resultado)
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: XMLHttpRequest.responseText,

            });
        }
    });
}

const printRollete = (data) => {
    
    options=[]
    for (var i = data.numMin; i < data.numMax+1; i++) {
        options.push(i);
    }
    arc = Math.PI / (options.length / 2);
    drawRouletteWheel();
}





function byte2Hex(n) {
    var nybHexString = "0123456789ABCDEF";
    return String(nybHexString.substr((n >> 4) & 0x0F, 1)) + nybHexString.substr(n & 0x0F, 1);
}

function RGB2Color(r, g, b) {
    return '#' + byte2Hex(r) + byte2Hex(g) + byte2Hex(b);
}

function getColor(item) {
    if (item % 2 == 0) return "#FD390F";
    return "#000000"
}

function drawRouletteWheel() {
    var canvas = document.getElementById("canvas");
    if (canvas.getContext) {
        var outsideRadius = 200;
        var textRadius = 160;
        var insideRadius = 125;

        ctx = canvas.getContext("2d");
        ctx.clearRect(0, 0, 500, 500);

        ctx.strokeStyle = "black";
        ctx.lineWidth = 2;

        ctx.font = 'bold 12px Helvetica, Arial';

        for (var i = 0; i < options.length; i++) {
            var angle = startAngle + i * arc;
            let item = options[i];
            //ctx.fillStyle = colors[i];
            ctx.fillStyle = getColor(item);
            
            ctx.beginPath();
            ctx.arc(250, 250, outsideRadius, angle, angle + arc, false);
            ctx.arc(250, 250, insideRadius, angle + arc, angle, true);
            ctx.stroke();
            ctx.fill();

            ctx.save();
            ctx.shadowOffsetX = -1;
            ctx.shadowOffsetY = -1;
            ctx.shadowBlur = 0;
            ctx.shadowColor = "rgb(220,220,220)";
            ctx.fillStyle = "black";
            ctx.translate(250 + Math.cos(angle + arc / 2) * textRadius,
                250 + Math.sin(angle + arc / 2) * textRadius);
            ctx.rotate(angle + arc / 2 + Math.PI / 2);
            var text = options[i];
            ctx.fillText(text, -ctx.measureText(text).width / 2, 0);
            ctx.restore();
        }

        //Arrow
        ctx.fillStyle = "black";
        ctx.beginPath();
        ctx.moveTo(250 - 4, 250 - (outsideRadius + 5));
        ctx.lineTo(250 + 4, 250 - (outsideRadius + 5));
        ctx.lineTo(250 + 4, 250 - (outsideRadius - 5));
        ctx.lineTo(250 + 9, 250 - (outsideRadius - 5));
        ctx.lineTo(250 + 0, 250 - (outsideRadius - 13));
        ctx.lineTo(250 - 9, 250 - (outsideRadius - 5));
        ctx.lineTo(250 - 4, 250 - (outsideRadius - 5));
        ctx.lineTo(250 - 4, 250 - (outsideRadius + 5));
        ctx.fill();
    }
}

function spin() {
    spinAngleStart = Math.random() * 10 + 10;
    spinTime = 0;
    spinTimeTotal = Math.random() * 3 + 4 * 1000;
    rotateWheel();
}

function rotateWheel() {
    spinTime += 30;
    if (spinTime >= spinTimeTotal) {
        stopRotateWheel();
        return;
    }
    var spinAngle = spinAngleStart - easeOut(spinTime, 0, spinAngleStart, spinTimeTotal);
    startAngle += (spinAngle * Math.PI / 180);
    drawRouletteWheel();
    spinTimeout = setTimeout('rotateWheel()', 30);
}

function stopRotateWheel() {
    clearTimeout(spinTimeout);
    var degrees = startAngle * 180 / Math.PI + 90;
    var arcd = arc * 180 / Math.PI;
    var index = Math.floor((360 - degrees % 360) / arcd);
    ctx.save();
    ctx.font = 'bold 30px Helvetica, Arial';
    var text = options[index]

    Apostar(options[index]);

    ctx.fillText(text, 250 - ctx.measureText(text).width / 2, 250 + 10);
    ctx.restore();
}

function easeOut(t, b, c, d) {
    var ts = (t /= d) * t;
    var tc = ts * t;
    return b + c * (tc + -3 * ts + 3 * t);
}
GetRoulette(2)

const Apostar = (num) => {

    let won = false;
    if (parseInt($("#numbet").val()) == parseInt(num)) {
        Swal.fire({
            icon: "success",
            title: "Ganaste.....",

        });
        won = true;
    }
        
    data = {
        roulette_Id: roulette.id,
        betNumber: parseInt($("#numbet").val()),
        numberRandon: parseInt(num),
        won: won,
        betValue: parseInt($("#betval").val())
    }

   
    $.ajax({
        type: "POST",
        url: `${url}Bet/NewBet`,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resultado) {
            $("#numbet").val('');
            $("#betval").val('');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            Swal.fire({
                icon: "error",
                title: "Oops... Error al gaurdar apuesta",
                text: XMLHttpRequest.responseText,

            });
        }
    });

}

function ValidBet() {
    var data = {
        roulette_Id: roulette.id,
        betNumber: $("#numbet").val(),
        betValue: $("#betval").val()
    };
    if (data.betNumber == null || data.betNumber == "" || data.betNumber > roulette.numMax || data.betNumber < roulette.numMin) {
        Swal.fire({
            icon: "error",
            title: "Ingrese un numero dentro de ruleta",
            text: XMLHttpRequest.responseText,

        });
        return false;
    }
    if (data.betValue == null || data.betValue == "" || data.betValue > roulette.maxValue || data.betValue < roulette.minValue) {
        Swal.fire({
            icon: "error",
            title: `Ingrese un valor mayor a: ${roulette.minValue} y menor a: ${roulette.maxValue} `,
            text: XMLHttpRequest.responseText,

        });
        return false;
    }
    spin()

}
     