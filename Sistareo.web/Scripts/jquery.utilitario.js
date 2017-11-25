/* ************************************************************************************* 
    Archivo     :   jquery.utilitario.js
    Versión     :   v1.0
    Tipo        :   JS - JavaScript Ajax
    Objetivo    :   Javascript con funciones utilitarias    
**************************************************************************************** */





//****************************************************************
// Funcion		:: 	fn_util_MuestraMensaje
// Descripción	::	Muestra Mensaje

//****************************************************************
function fn_util_MuestraMensaje(pTitulo, pMensaje, pTipo) {
    if (pTipo == "E") {
        $("#dvAlertaERROR_titulo").html(pTitulo);
        $("#dvAlertaERROR_msg").html(pMensaje);
        $("#dvAlertaERROR").show();
        $("#dvAlertaERROR").delay(6000).fadeOut(300);
    }
    if (pTipo == "W") {
        $("#dvAlertaWARN_titulo").html(pTitulo);
        $("#dvAlertaWARN_msg").html(pMensaje);
        $("#dvAlertaWARN").show();
        $("#dvAlertaWARN").delay(6000).fadeOut(300);
    }
    if (pTipo == "OK") {
        $("#dvAlertaOK_titulo").html(pTitulo);
        $("#dvAlertaOK_msg").html(pMensaje);
        $("#dvAlertaOK").show();
        $("#dvAlertaOK").delay(6000).fadeOut(300);
    }
}


var vg_sTodosCaracteres = ' !#$%&\()*+,./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyzáéíóú{|}~-';
var vg_sSoloLetras = 'abcdefghijklmnñopqrstuvwxyzáéíóú ';
var vg_sAlfaNumerico = '-1234567890abcdefghijklmnñopqrstuvwxyzáéíóú ';
var vg_sSoloNumerosPos = '1234567890';
var vg_sSoloNumerosNeg = '-1234567890';
var vg_sSoloNumerosDecimalesPos = '1234567890.';
var vg_sSoloNumerosDecimalesNeg = '-1234567890.';
/***********************************************************************
 * jQuery fn_util_validarLetras function
 * Version 2
 * Validar Letras
 **********************************************************************/
(function (a) {
    a.fn.fn_util_validarLetras = function () {
        var id = jQuery(this).attr("id");
        a(this).on({
            keypress: function (a) {
                var c = a.which,
                    d = a.keyCode,
                    e = String.fromCharCode(c).toLowerCase(),
                    f = vg_sSoloLetras;
                (-1 != f.indexOf(e) || 9 == d || 37 != c && 37 == d || 39 == d && 39 != c || 8 == d || 46 == d && 46 != c) && 161 != c || a.preventDefault()
            },
            focusout: function (a) {
                var pDiferente = new RegExp("[" + vg_sSoloLetras + "]", 'gi');
                var sDiferente = vg_sTodosCaracteres.replace(pDiferente, '');
                var pFinal = new RegExp("[" + sDiferente + "]", 'gi');
                var sFinal = jQuery(this).val();
                sFinal = sFinal.replace(pFinal, '');
                jQuery(this).val(sFinal);
            }
        })
    }
})(jQuery);
/***********************************************************************
 * jQuery fn_util_validarAlfaNumerico function
 * Version 2
 * Validar Letras y Numeros
 **********************************************************************/
(function (a) {
    a.fn.fn_util_validarAlfaNumerico = function () {
        var id = jQuery(this).attr("id");
        a(this).on({
            keypress: function (a) {
                var c = a.which,
                    d = a.keyCode,
                    e = String.fromCharCode(c).toLowerCase(),
                    f = vg_sAlfaNumerico;
                (-1 != f.indexOf(e) || 9 == d || 37 != c && 37 == d || 39 == d && 39 != c || 8 == d || 46 == d && 46 != c) && 161 != c || a.preventDefault()
            },
            focusout: function (a) {
                var pDiferente = new RegExp("[" + vg_sAlfaNumerico + "]", 'gi');
                var sDiferente = vg_sTodosCaracteres.replace(pDiferente, '');
                var pFinal = new RegExp("[" + sDiferente + "]", 'gi');
                var sFinal = jQuery(this).val();
                sFinal = sFinal.replace(pFinal, '');
                jQuery(this).val(sFinal);
            }
        })
    }
})(jQuery);
/***********************************************************************
 * jQuery fn_util_validarNumeros function
 * Version 2
 * Validar Números => Parametro: POS => Positivos | NEG => NEGATIVOS
 **********************************************************************/
var vg_TipoNumero;
(function (a) {    
    a.fn.fn_util_validarNumeros = function (pTipo) {
        var id = jQuery(this).attr("id");
        a(this).on({
            blur: function () {
                var valor = $(this).val();

                if (valor == '') {
                    $(this).val('0');
                }

                //Valida Tipo NEG o POS                
                if ($.trim(pTipo) == "POS") {
                    vg_TipoNumero = vg_sSoloNumerosPos;
                } else {
                    vg_TipoNumero = vg_sSoloNumerosNeg;
                }
                var pDiferente = new RegExp("[" + vg_TipoNumero + "]", 'gi');
                var sDiferente = vg_sTodosCaracteres.replace(pDiferente, '');
                var pFinal = new RegExp("[" + sDiferente + "]", 'gi');
                var sFinal = jQuery(this).val();
                sFinal = sFinal.replace(pFinal, '');
                jQuery(this).val(sFinal);
                
                //Valida si es numero valido
                if ($.isNumeric(valor)) {
                    $(this).val(valor);
                } else {
                    $(this).val("0");
                }
                
            },
            keypress: function (a) {

                //Valida Tipo NEG o POS                
                if ($.trim(pTipo) == "POS") {
                    vg_TipoNumero = vg_sSoloNumerosPos;
                } else {
                    vg_TipoNumero = vg_sSoloNumerosNeg;
                }

                var c = a.which,//c dfgdfgdfgdfg
                    d = a.keyCode,
                    e = String.fromCharCode(c).toLowerCase(),
                    f = vg_TipoNumero;
                (-1 != f.indexOf(e) || 9 == d || 37 != c && 37 == d || 39 == d && 39 != c || 8 == d || 46 == d && 46 != c) && 161 != c || a.preventDefault()

                //Validar el negativo
                var valor = $(this).val();
                if ((valor.indexOf('-') != -1) && e == '-') {
                    a.preventDefault();
                }
            }
        })
    }
})(jQuery);
/***********************************************************************
 * jQuery fn_util_validarPersonalizado function
 * Version 2
 * Validar Personalizado
 **********************************************************************/
(function (a) {
    a.fn.fn_util_validaPersonalizado = function (sValidar) {
        var id = jQuery(this).attr("id");
        a(this).on({
            keypress: function (a) {
                var c = a.which,
                    d = a.keyCode,
                    e = String.fromCharCode(c).toLowerCase(),
                    f = sValidar;
                (-1 != f.indexOf(e) || 9 == d || 37 != c && 37 == d || 39 == d && 39 != c || 8 == d || 46 == d && 46 != c) && 161 != c || a.preventDefault()
            },
            focusout: function (a) {
                var pDiferente = new RegExp("[" + sValidar + "]", 'gi');
                var sDiferente = vg_sTodosCaracteres.replace(pDiferente, '');
                var pFinal = new RegExp("[" + sDiferente + "]", 'gi');
                var sFinal = jQuery(this).val();
                sFinal = sFinal.replace(pFinal, '');
                jQuery(this).val(sFinal);
            }
        })
    }
})(jQuery);
/***********************************************************************
 * jQuery fn_util_validarCampoDecimal function
 * Version 2
 * Validar Decimales: cantidad decimal, signo de puntuacion (',' - '.' ), cantidad de enteros
 **********************************************************************/
(function (a) {
    var vg_TipoNumeroDecimal;
    a.fn.fn_util_validaDecimal = function (pLongitud, pTipo) {
        var id = jQuery(this).attr("id");
        var caracter = ".";
        jQuery(this).addClass("css_textoDecimal");
        a(this).on({
            blur: function () {
                var valor = $(this).val();
                $(this).val(fn_util_ValidaMonto(valor, pLongitud));
            },
            keypress: function (event) {
                var valor = $(this).val();

                //TIPO => POS o NEG                
                if ($.trim(pTipo)=="POS") {
                    vg_TipoNumeroDecimal = vg_sSoloNumerosDecimalesPos;
                }else{
                    vg_TipoNumeroDecimal = vg_sSoloNumerosDecimalesNeg;
                }

                var c = event.which,
                    d = event.keyCode,
                    e = String.fromCharCode(c).toLowerCase(),
                    f = vg_TipoNumeroDecimal + caracter;

                (-1 != f.indexOf(e) || 9 == d || 37 != c && 37 == d || 39 == d && 39 != c || 8 == d || 46 == d && 46 != c) && 161 != c || event.preventDefault();

                var key = String.fromCharCode(event.which);
                var position = $(this).fn_util_obtenerPosicionCursor() - 1;

                //alert(position + "|" + (valor.indexOf(caracter)));
                if (position < (valor.indexOf(caracter))) {

                } else if ((valor.indexOf(caracter) != -1) && (valor.substring(valor.indexOf(caracter), valor.indexOf(caracter).length).length > pLongitud)) {
                    event.preventDefault();
                }

                //Validar el punto / coma
                if ((valor.indexOf(caracter) != -1) && e == caracter) {
                    event.preventDefault();
                }

                //Validar el negativo
                if ((valor.indexOf('-') != -1) && e == '-') {
                    event.preventDefault();
                }
            }
        });
    }
})(jQuery);
/***********************************************************************
 * funciones Utilitarias
 * Version 2
 * Obtener Posicion de Cursor, Obtener Trama JSON, 
 * Activar Linea de Tabla
 **********************************************************************/
(function (a) {
    $.fn.fn_util_obtenerPosicionCursor = function () {
        var el = $(this).get(0);
        var pos = 0;
        if ('selectionStart' in el) {
            pos = el.selectionStart;
        } else if ('selection' in document) {
            el.focus();
            var Sel = document.selection.createRange();
            var SelLength = document.selection.createRange().text.length;
            Sel.moveStart('character', -el.value.length);
            pos = Sel.text.length - SelLength;
        }
        return pos;
    }
})(jQuery);


//**********************************************************************
// Nombre: fn_util_ValidaMonto
//**********************************************************************
function fn_util_ValidaMonto(pstrMonto, pintDecimales) {
    var strMonto = String(pstrMonto);
    if ($.trim(strMonto) == "") strMonto = "0";

    $('<input>').attr({
        type: 'hidden',
        id: 'hddUtilMonto',
        name: 'hddUtilMonto'
    }).appendTo('body');

    $("#hddUtilMonto").val($.number(pstrMonto, pintDecimales));
    return $("#hddUtilMonto").val();
}


/***********************************************************************
 * jQuery fn_util_validaCantidadTexto function
 * Version 2
 * Muestra cantidad de caracteres de un input o TextArea
 * Copyright (c) 2014
 **********************************************************************/
(function (a) {
    a.fn.fn_util_validaCantidadTexto = function (sTexto) {
        var id = jQuery(this).attr("id");

        if ($("#spCont_" + id).length == 0) {

            var nMaxLenght = jQuery(this).attr("maxlength");
            $("#" + id).parent().append("<span id='spCont_" + id + "' style='color:#AAA;font-size:10px !important;font-weight:normal !important;'>( 0 / " + nMaxLenght + " )</span>");

            var vValue = jQuery(this).val();
            var nCantidadTexto = vValue.length;
            a(this).on({
                keyup: function (a) {
                    vValue = jQuery(this).val();
                    nCantidadTexto = vValue.length;
                    $("#spCont_" + id).html("( " + nCantidadTexto + " / " + nMaxLenght + " )");
                },
                focusout: function (a) {

                }
            })

        }
    }
})(jQuery);


//****************************************************************
// Funcion		:: 	fn_util_bloquearFormulario 
// Descripción	::	Bloquea todo el Formulario
//****************************************************************
function fn_util_bloquearPantalla(pMensaje) {
    var strMensaje = pMensaje;
    if (pMensaje == null) strMensaje = "Procesando";
    if ($.trim(strMensaje) == "") strMensaje = "Procesando";

    vRutaServidor = "/";
    try {
        vRutaServidor = VG_RUTA_SERVIDOR;
    } catch (ex) {
        vRutaServidor = "/";
    }

    $.blockUI({
        message: '<table align="center"><tr><td style="padding-top:15px;"><img src="' + vRutaServidor + 'Content/Images/gif-loading.gif" width="50" /></td></tr><tr><td style="padding:11px;font-size:13px;">' + strMensaje + '</td></tr></table>',
        centerY: 0,
        css: {
            width: '150px',
            top: ($(window).height() - 150) / 2 + 'px',
            left: ($(window).width() - 150) / 2 + 'px',
            border: '0px solid #ff8304',
            padding: '3px',
            backgroundColor: '#333333',
            opacity: .99,
            color: '#FFFFFF'
        }
    });
}
function fn_util_desbloquearPantalla() {
    $.unblockUI();
}

