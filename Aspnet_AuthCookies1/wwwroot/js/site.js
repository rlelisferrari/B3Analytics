// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    $body = $("body");

    $(document).on({
        ajaxStart: function () {$body.addClass("loading"); },
        });

    //Initiates an AJAX request on click
    $(document).on("submit", function () {
        $.get("/mockjax");
        }); 

    function meuFiltro() {
        var inputAtivo, inputEntradas, inputLucro, inputPLucro, lucroInput, PercentlucroInput,ativo,entrada, table, tr, tdLucro, tdPLucro, i;
        inputAtivo = document.getElementById("filterByAtivo");
        inputEntradas = document.getElementById("filterByEntradas");
        inputLucro = document.getElementById("filterByLucro");
        inputPLucro = document.getElementById("filterByPercentLucro");
        ativo = inputAtivo.value.toUpperCase();
        entrada = parseInt(inputEntradas.value);
        lucroInput = parseFloat(inputLucro.value);
        PercentlucroInput = parseFloat(inputPLucro.value);
        table = document.getElementById("tbConsolidacao");
        tr = table.getElementsByTagName("tr");

        if (isNaN(entrada))
            entrada = 0;

        if (isNaN(lucroInput))
            lucroInput = 0;

        if (isNaN(PercentlucroInput))
            PercentlucroInput = 0;
    
        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[1];
            tdEntrada = tr[i].getElementsByTagName("td")[2];
            tdLucro = tr[i].getElementsByTagName("td")[3];
            tdPLucro = tr[i].getElementsByTagName("td")[4];                  

            if (td && tdEntrada && tdLucro && tdPLucro) {
                if (td.innerHTML.toUpperCase().indexOf(ativo) > -1 && parseFloat(tdEntrada.innerHTML) >= entrada && parseFloat(tdLucro.innerHTML) >= lucroInput && parseFloat(tdPLucro.innerHTML) >= PercentlucroInput) {
                    tr[i].style.display = "";
                }
                else {
                    tr[i].style.display = "none";
                }
            } else {
                tr[i].style.display = "";
            }
        }
    }
