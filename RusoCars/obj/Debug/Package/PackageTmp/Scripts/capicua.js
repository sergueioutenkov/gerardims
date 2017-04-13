/// <reference path="jquery-1.7.1-vsdoc.js" />

function test(){
    alert("asd");
}

function openWindow (url,height,width,alignment){ 
	if (alignment == "center"){
		screenHeight = parseInt(screen.availHeight);
		screenWidth = parseInt(screen.availWidth);
		//Calculo en punto medio
		centerWidth = parseInt((screenWidth/2))
		centerHeight = parseInt((screenHeight/2))
		// ubico la venta segun sus dimensiones
		laXPopup = centerWidth - parseInt((width/2))
		laYPopup = centerHeight - parseInt((height/2))
	}else if (alignment == "corner"){
		laXPopup = 0
		laYPopup = 0
	}
	window.open(url,'','location=no, left ='+ laXPopup +',top='+ laYPopup +', menubar=no, scrollbars=yes, status=no, toolbar=no, resizable=yes, height='+height+', width='+width+'') ;
}


function slideDiv(absoluteId, pixeles, numero, idCirculo){
    //clearInterval(intervalo);
    var target = document.getElementById(absoluteId);
    target.style.left = pixeles;
    //var targetContador = document.getElementById('contadorSlider');
    //targetContador.innerHTML =  numero + '/4';
    $(".instanciaNoticia").fadeTo(100, 1, function(){		
            //var targetCirculo = document.getElementById(idCirculo);
            //targetCirculo.style.opacity = 1;
            });
    //autoSlide(absoluteId);
}       

function ocultarImagenes(className){
    $(".image").fadeOut(0, function(){
        $("." + className).fadeIn(0);
    });        
}        

function cambiarImagen(target, description, idImage, source, posicion, total){
    var imgTarget = document.getElementById('imageFull');
    $(imgTarget).fadeOut(500, function(){
        imgTarget.src = target;
    });
    $(imgTarget).fadeIn(500);
    var targetId = document.getElementById("IDImage");
    targetId.value = idImage;
    $(".imageCat");
    source.style.border = 'solid 2px #fff';
    source.style.opacity = 0.6;
    var targetPosicion = document.getElementById('posicion');
    targetPosicion.innerHTML = (posicion+1) + ' / ' + (total);
    var targetDescripcion = document.getElementById("descripcionImagen");
    targetDescripcion.innerHTML = description;
}

function cambiarImagenSimple(idTarget, source, nombre){
    var target = document.getElementById(idTarget);
    target.src = source;
    var targetHidden = document.getElementById('nombreImagen');
    targetHidden.value = nombre;
}

function cambiarTexto(idTarget, texto){
    var target = document.getElementById(idTarget);
    target.innerHTML = texto;
    
}

var flag = true;

function showDiv(targetId){
    var target = document.getElementById(targetId);
    $('.flotante').hide(500);    
    if(target.style.display == 'none'){
        $(target).show(500, function(){
        });
    }
}

function ocultarDiv(){
    $('.flotante').hide(500);
}

function autoSlide(absoluteId) {
     var target = document.getElementById(absoluteId);
     //targetContador = document.getElementById('contadorSlider');
     intervalo = setInterval(function(){         
     //$(".instanciaNoticia").fadeTo(100, 0.5);
     switch(target.style.left){
         case '-1000px':
            //$("#selector_3").fadeTo(100, 1);
            target.style.left = '-2000px';
            //targetContador.innerHTML = '3/4';
            break;
        case '-2000px':
            //$("#selector_1").fadeTo(100, 1);
            target.style.left = '-3000px';
            //targetContador.innerHTML = '4/4';
            break;
        case '-3000px':
            //$("#selector_1").fadeTo(100, 1);
            target.style.left = '-4000px';
            //targetContador.innerHTML = '4/4';
            break;
        case '-4000px':
            //$("#selector_1").fadeTo(100, 1);
            target.style.left = '0px';
            //targetContador.innerHTML = '4/4';
            break;
        default:
            //$("#selector_2").fadeTo(100, 1);
            target.style.left = '-1000px';
            //targetContador.innerHTML = '2/4';
            break;
        }
    }, 5000);
}

/*CAPICUA*/

function animacionMenuTop(color){
    var slider = document.getElementById("sliderIndex");    
    var logo = document.getElementById("logo");    
    logo.style.backgroundColor = color;
    slider.style.backgroundColor = color;
}

function ajaxOnBegin(idTarget) {
    $("#" + idTarget).slideDown(500);
    $("#" + idTarget + " *").fadeOut(500);
    setTimeout(function () {
        $("#" + idTarget).html('<div class="preload"><img src="../../Images/preloader.gif" /><br>Cargando...</div>');
    }, 500);
}

function ajaxOnComplete(idTarget) {

}

function ajaxOnSuccess(idTarget) {

}

function cerrarDiv(idTarget) {
    $("#" + idTarget).slideUp(500);
}