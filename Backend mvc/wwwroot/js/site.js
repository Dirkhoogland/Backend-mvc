var Insertbutton = document.getElementById("Insertbutton");
var Inputuserinserttext = document.getElementById("Inputuserinserttext");
var Inputupdatetduur = document.getElementById("Inputupdatetduur");
var Inputupdatetstatus = document.getElementById("Inputupdatetstatus");
var Inputupdatetsnaam = document.getElementById("Inputupdatetnaam");
var Inputupdatetbesch = document.getElementById("Inputupdatetbesch");
var confirmbutton = document.getElementById("confirmbutton");
var lijstdiv = document.getElementById("lijstdiv");
var Inserttask = document.getElementById("Inserttask");

function Insertshow() {
    Inputuserinserttext.style.visibility = "visible"
    lijstdiv.style.visibilisty = 'Visible'

}
function Insertlijst() {
    lijstdiv.style.visibility = "Visible"
    let para = document.createElement("p");

    let  parent = document.createElement("p");

    let node = document.createTextNode("Lijst Naam");

    let element = document.getElementById("lijstdiv");

    let div = document.createElement('div');

    div.innerHTML = '<button onclick="lijstbutton()">confirm</button> <input type = "Textbox" id ="tb" ></input>';

    para.appendChild(node);
    para.appendChild(div);
    element.appendChild(para);
}

function taskinput()
{
    let para = document.createElement("p");

    let parent = document.createElement("p");



    let element = document.getElementById("Inputuserinserttext");

    let div = document.createElement('div');

    div.innerHTML = ' <p>Taaknaam</p> <input type = "Textbox" id ="tb1" ></input>  <br> <p>Lijst naam</p> <input type = "Textbox" id = "tb2" ></input >  <br> <p>Beschrijving</p> <input type = "Textbox" id ="tb3" ></input>  <br> <p>Status</p> <input type = "Textbox" id ="tb4" ></input> <br><p>Duur in minuten</p> <input type = "Textbox" id ="tb5" ></input> <br> <button onclick="Taskbuttonmaken()">maak task</button>  ';
;


    para.appendChild(div);
    element.appendChild(para);
    Inputuserinserttext.style.visibility = "visible"
}


function updatestart() {
    Inputupdatetsnaam.style.visibility = "visible"
    Inputupdatetbesch.style.visibility = "visible"
    Inputupdatetstatus.style.visibility = "visible"
    Inputupdatetduur.style.visibility = "visible"
    confirmbutton.style.visibility = "Visible"
}
function Taskbuttonmaken() {
    var taaknieuw = document.getElementById("tb1").value;
    var lijst = document.getElementById("tb2").value;
    var besch = document.getElementById("tb3").value;
    var status = document.getElementById("tb4").value;
    var duur = document.getElementById("tb5").value
    var newtaak =
    {
        'Naam': taaknieuw,
        'lijst':lijst,
        'Besch': besch,
        'Status': status,
        'Duur': duur
    }
    $.ajax({
        type:"post",
        dataType:"json",
        traditional: true,
        url:'/Home/Tasksaddto',
        data: JSON.stringify(newtaak),
        contentType: "application/json",
        success: function () { console.log(Id); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
    Inputuserinserttext.innerHTML = ""
}

function lijstbutton() {



    var lijstnieuw = document.getElementById("tb").value;
    $.ajax({
        type: "post",
        dataType: "json",
        traditional: true,
        url: '/Home/Listadd',
        data: JSON.stringify(lijstnieuw),
        contentType: "application/json",
        success: function () { console.log(Id); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });

    lijstdiv.innerHTML = ""
};
function confirmupdate(Id, Naam, Status, Duur, Besch) {
    var tasklist =
    {
        'Id': Id,
        'Naam': Naam,
        'Status': Status,
        'Duur': Duur,
        'Besch': Besch
    }
    $.ajax({
        type: "post",
        dataType: "json",
        traditional: true,
        url: '/Home/Updatetask',
        data: JSON.stringify(tasklist),
        contentType: "application/json",
        success: function () { console.log(Id); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
}

function startDelete(Id) {

    $.ajax({
        type: "post",
        dataType: "json",
        traditional: true,
        url: '/Home/Deletetask',
        data: JSON.stringify(Id),
        contentType: "application/json",
        success: function () { console.log(Id); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
}