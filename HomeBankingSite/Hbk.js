function Toggle(Obj)
    {Obj = document.getElementById(Obj);
    if (Obj.style.display=="block")
        Obj.style.display="none";
    else
        Obj.style.display="block";}
function FormatCurrency(num)
    {num = num.toString().replace(/\$|\,/g,'');
    if(isNaN(num))
        num = "0";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num*100+0.50000000001);
        cents = num%100;
        num = Math.floor(num/100).toString();
    if(cents<10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
        num = num.substring(0,num.length-(4*i+3))+','+
        num.substring(num.length-(4*i+3));
    return (((sign) ? '' : '-') + '$' + num + '.' + cents);
}

function sizeFrame2(ifrm) {
    var F = document.getElementById(ifrm);
    if (F.contentDocument) {
        F.height = F.contentDocument.documentElement.scrollHeight + 30; //FF 3.0.11, Opera 9.63, and Chrome
    } else {



        F.height = F.contentWindow.document.body.scrollHeight + 30; //IE6, IE7 and Chrome

    }

}

function sizeFrame() {
    var F = document.getElementById("hbkFrame");
    if (F.contentDocument) {
        F.height = F.contentDocument.documentElement.scrollHeight + 30; //FF 3.0.11, Opera 9.63, and Chrome
    } else {



        F.height = F.contentWindow.document.body.scrollHeight + 30; //IE6, IE7 and Chrome

    }

}
 
(function () {
    var init = function () {
        var updateOrientation = function () {
            var orientation = window.orientation;

            switch (orientation) {
                case 90: case -90:
                    orientation = 'landscape';
                    break;
                default:
                    orientation = 'portrait';
            }

            // set the class on the HTML element (i.e. )  
            document.body.parentNode.setAttribute('class', orientation);
        };

        // event triggered every 90 degrees of rotation  
        window.addEventListener('orientationchange', updateOrientation, false);

        // initialize the orientation  
        updateOrientation();
    }

    window.addEventListener('DOMContentLoaded', init, false);

})();