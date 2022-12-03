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

function sortTable2(table_id, sortColumn, btnName, btnNameDesc, tipo = 0) {
    var btn = document.getElementById(btnName);
    var btnDesc = document.getElementById(btnNameDesc);

    if (btn != null && btn.style != null)
        btn.style.color = "red";

    if (btnDesc != null && btnDesc.style != null)
        btnDesc.style.color = "black";

    var tableData = document.getElementById(table_id).getElementsByTagName('tbody').item(0);
    var rowData = tableData.getElementsByTagName('tr');
    for (var i = 0; i < rowData.length - 1; i++) {
        for (var j = 0; j < rowData.length - (i + 1); j++) {
            var item1 = rowData.item(j).getElementsByTagName('td').item(sortColumn).innerHTML.replace("%", "").replace(",", ".");
            var item2 = rowData.item(j + 1).getElementsByTagName('td').item(sortColumn).innerHTML.replace("%", "").replace(",", ".")
            if (tipo == 0) {
                //inteiro ou float
                if (parseFloat(item1) > parseFloat(item2)) {
                    tableData.insertBefore(rowData.item(j + 1), rowData.item(j));
                }
            }
            else if (tipo == 2) {
                //volume 51.222.111,00
                item1 = item1.replace(/\./g, '');
                item2 = item2.replace(/\./g, '');
                if (parseFloat(item1) > parseFloat(item2)) {
                    tableData.insertBefore(rowData.item(j + 1), rowData.item(j));
                }
            }else {
                //string
                if (item1.toLowerCase() > item2.toLowerCase()) {
                    tableData.insertBefore(rowData.item(j + 1), rowData.item(j));
                }
            }
        }
    }
}

function sortTableDesc2(table_id, sortColumn, btnName, btnNameDesc, tipo = 0) {
    var btn = document.getElementById(btnName);
    var btnDesc = document.getElementById(btnNameDesc);

    if (btn != null && btn.style != null)
        btn.style.color = "red";

    if (btnDesc != null && btnDesc.style != null)
        btnDesc.style.color = "black";

    var tableData = document.getElementById(table_id).getElementsByTagName('tbody').item(0);
    var rowData = tableData.getElementsByTagName('tr');
    for (var i = 0; i < rowData.length - 1; i++) {
        for (var j = 0; j < rowData.length - (i + 1); j++) {
            var item1 = rowData.item(j).getElementsByTagName('td').item(sortColumn).innerHTML.replace("%", "").replace(",", ".");
            var item2 = rowData.item(j + 1).getElementsByTagName('td').item(sortColumn).innerHTML.replace("%", "").replace(",", ".")
            if (tipo == 0) {
                //inteiro ou float
                if (parseFloat(item1) < parseFloat(item2)) {
                    tableData.insertBefore(rowData.item(j + 1), rowData.item(j));
                }
            } else if (tipo == 2) {
                //volume 51.222.111,00
                item1 = item1.replace(/\./g, '');
                item2 = item2.replace(/\./g, '');
                if (parseFloat(item1) < parseFloat(item2)) {
                    tableData.insertBefore(rowData.item(j + 1), rowData.item(j));
                }
            } else {
                //string
                if (item1.toLowerCase() < item2.toLowerCase()) {
                    tableData.insertBefore(rowData.item(j + 1), rowData.item(j));
                }
            }
        }
    }
}

function sortTable(index, tipo, btnName, btnNameDesc) {
    var btn = document.getElementById(btnName);
    var btnDesc = document.getElementById(btnNameDesc);

    if (btn != null && btn.style != null)
        btn.style.color = "red";

    if (btnDesc != null && btnDesc.style != null)
        btnDesc.style.color = "black";

    //document.getElementById(btnNameDesc).style.color = "black";

    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("tbConsolidacao");
    switching = true;
    /*Make a loop that will continue until
    no switching has been done:*/
    while (switching) {
        //start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        /*Loop through all table rows (except the
        first, which contains table headers):*/
        for (i = 1; i < (rows.length - 1); i++) {
            //start by saying there should be no switching:
            shouldSwitch = false;
            /*Get the two elements you want to compare,
            one from current row and one from the next:*/
            x = rows[i].getElementsByTagName("TD")[index];
            y = rows[i + 1].getElementsByTagName("TD")[index];
            if (tipo == 0) {
                //inteiro
                if (parseInt(x.innerHTML) < parseInt(y.innerHTML)) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else if (tipo == 1) {
                //float
                if (parseFloat(x.innerHTML) < parseFloat(y.innerHTML)) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else {
                //string
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }            
        }
        if (shouldSwitch) {
            /*If a switch has been marked, make the switch
            and mark that a switch has been done:*/
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

function sortTableDesc(index, tipo, btnName, btnNameDesc) {
    var btn = document.getElementById(btnName);
    var btnDesc = document.getElementById(btnNameDesc);

    if (btn != null && btn.style != null)
        btn.style.color = "red";

    if (btnDesc != null && btnDesc.style != null)
        btnDesc.style.color = "black";

    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("tbConsolidacao");
    switching = true;
    /*Make a loop that will continue until
    no switching has been done:*/
    while (switching) {
        //start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        /*Loop through all table rows (except the
        first, which contains table headers):*/
        for (i = 1; i < (rows.length - 1); i++) {
            //start by saying there should be no switching:
            shouldSwitch = false;
            /*Get the two elements you want to compare,
            one from current row and one from the next:*/
            x = rows[i].getElementsByTagName("TD")[index];
            y = rows[i + 1].getElementsByTagName("TD")[index];
            if (tipo == 0) {
                //inteiro
                if (parseInt(x.innerHTML) > parseInt(y.innerHTML)) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else if (tipo == 1) {
                //float
                if (parseFloat(x.innerHTML) > parseFloat(y.innerHTML)) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else {
                //string
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            /*If a switch has been marked, make the switch
            and mark that a switch has been done:*/
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }
}

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
