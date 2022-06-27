var Insertbutton = document.getElementById("Insertbutton");
var Inputuserinserttext = document.getElementById("Inputuserinserttext");
var Inputupdatetduur = document.getElementById("Inputupdatetduur");
var Inputupdatetstatus = document.getElementById("Inputupdatetstatus");
var Inputupdatetsnaam = document.getElementById("Inputupdatetnaam");
var Inputupdatetbesch = document.getElementById("Inputupdatetbesch");
var confirmbutton = document.getElementById("confirmbutton");
var lijstdiv = document.getElementById("lijstdiv");
var Inserttask = document.getElementById("Inserttask");
var startbutton = document.getElementById('startbutton');
var updatetlijst = document.getElementById('updatetlijst');
var taskid = 0;
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
    Insertbutton.style.visibility = "hidden"
}

function Taskbuttonmaken() {
    var naam = document.getElementById("tb1").value;
    var lijst = document.getElementById("tb2").value;
    var besch = document.getElementById("tb3").value;
    var status = document.getElementById("tb4").value;
    var duur = document.getElementById("tb5").value
    var tasklist = JSON.stringify({
        'Lijst': lijst,
        'Naam': naam,
        'Status': status,
        'Duur': duur,
        'Besch': besch
    });
    $.ajax({
        type: "post",
        dataType: "json",
        traditional: true,
        url: '/Home/Tasksaddto',
        data: tasklist,
        contentType: "application/json",
        success: function () { console.log(Id); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
    
}

function lijstbutton() {



    var lijstnieuw = document.getElementById("tb").value;
    $.ajax({
        type: "post",
        dataType: "json",
        traditional: true,
        url: '/Home/Listadd',
        data: JSON.stringify(lijstnieuw),
        contentType: 'application/json; charset=UTF-8',
        success: function () { console.log(Id); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });

    lijstdiv.innerHTML = ""
};

function confirmupdate(Id) {

    let div = document.getElementById(Id)
    var naam = document.getElementById("Inputupdatetnaam").value;
    var status = document.getElementById("Inputupdatetstatus").value;
    var duur = document.getElementById("Inputupdatetduur").value;
    var besch = document.getElementById("Inputupdatetbesch").value;
    var lijst = document.getElementById("Inputupdatetlijst").value;
    var takenlijst = JSON.stringify(
        {
            'Id': taskid,
            'Naam': document.getElementById("Inputupdatetnaam").value,
            'Status': document.getElementById("Inputupdatetstatus").value,
            'Duur': document.getElementById("Inputupdatetduur").value,
            'Besch': document.getElementById("Inputupdatetbesch").value,
            'Lijst': document.getElementById("Inputupdatetlijst").value
        });
    $.ajax({
        type: "post",
        dataType: "json",
        traditional: true,
        url: '/Home/Updatetask',
        data: takenlijst,
        contentType: "application/json",
        success: function () { console.log(Id); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
    
}

function startDelete(Id)
{

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


function deletelijst(id)
{

    $.ajax({
        type: "post",
        dataType: "json",
        traditional: true,
        url: '/Home/Deletelist',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function () { console.log(Id); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });
}

function updateshow(id)
{
    taskid = id;
    const element = document.getElementById(id);
    const para = document.createElement("p");
    const br = document.createElement("br");
    const node1 = document.createTextNode("Naam");
    const node2 = document.createTextNode("Beschrijving");
    const node3 = document.createTextNode("Duur");
    const node4 = document.createTextNode("Status");
    const node5 = document.createTextNode("Lijst");

    const input1 = document.createElement("input");
    input1.type = "text";
    input1.id = 'Inputupdatetnaam';

    const input2 = document.createElement("input");
    input2.type = "text";
    input2.id = 'Inputupdatetbesch';

    const input3 = document.createElement("input");
    input3.type = "text";
    input3.id = 'Inputupdatetduur';

    const input4 = document.createElement("input");
    input4.type = "text";
    input4.id = 'Inputupdatetstatus';

    const input5 = document.createElement("input");
    input5.type = "text";
    input5.id = 'Inputupdatetlijst';


    para.innerHTML = '<input id= "confirmbutton" value="confirm" type="button" onclick="confirmupdate()">'
    para.appendChild(node1);
    para.appendChild(input1);
    para.appendChild(br)
    para.appendChild(node2);
    para.appendChild(input2);
    para.appendChild(br)
    para.appendChild(node3);
    para.appendChild(input3);
    para.appendChild(br)
    para.appendChild(node4);
    para.appendChild(input4);
    para.appendChild(br)
    para.appendChild(node5);
    para.appendChild(input5);
    para.appendChild(br)
 

    element.appendChild(para);
    confirmbutton.style.visibility = 'visible';
}
var test2 = 0;

function updateshowlijst(test)
{
    let div = document.getElementById(test)
    test2 = test

    div.innerHTML = '<input type = "text" id = "updatetlijst"> <input id = test value="Update" type = "button" onclick="updatelijst()" >'
}

function updatelijst()
{


    var naam = document.getElementById("updatetlijst").value
    var list = JSON.stringify(
        {
            'Id': test2,
            'Lijst': naam
        });


    $.ajax({
        type: "post",
        dataType: "json",
        traditional: true,
        url: '/Home/ListUpdate',
        data: list,
        contentType: "application/json",
        success: function () { console.log(Id); },
        Error: function (a, b, c) { console.log(a); console.log(b); console.log(c); }
    });

}