﻿"use strict";

const eventList = document.getElementById("convertationTableBody");

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs")
    .build();

connection.serverTimeoutInMilliseconds = 1000 * 60 * 2 * 15;

const form = document.getElementById( "convertersForm" );
// form.addEventListener( "submit", function ( event ) {
//     event.preventDefault();
//     var formData = new FormData(form);
//
//     postFile(formData);
// } );

//var sendButton = document.getElementById("sendFile");

// sendButton.addEventListener("click", event => {
//     var formData = new FormData();
//
//     var file = document.getElementById("fileInput");
//
//     formData.append("file", file.files[0]);
//    
//     let dto = {
//       file: formData  
//     };
//
//     postFile(formData);
// });

const addEventToList = convertationResult => {
    let table = document.getElementById("convertationTable");

    if (table.style.display === "none") {
        table.style.display = "block";
    }

    let tablePagination = document.getElementById("tablePagination");

    if (tablePagination.style.display === "none") {
        tablePagination.style.display = "block";
    }

    let trow = document.createElement("tr");

    let name = document.createElement("td");
    let created = document.createElement("td");
    let jsonLink = document.createElement("td");
    let xmlLink = document.createElement("td");

    name.innerText = convertationResult.name;
    created.innerText = convertationResult.created;
    
    let host = window.location.host;
    let protocol = window.location.protocol;
    let jsonUrl = protocol + "//" + host + convertationResult.jsonDownloadPath;
    let xmlUrl = protocol + "//" + host + convertationResult.xmlDownloadPath;
    
    let jsonA = document.createElement('a');
    let xmlA = document.createElement('a');

    let jsonNode = document.createTextNode("Загрузить");
    let xmlNode = document.createTextNode("Загрузить");
    
    jsonA.appendChild(jsonNode);
    xmlA.appendChild(xmlNode);

    jsonA.title = "download";
    xmlA.title = "download";

    jsonA.href = jsonUrl;
    xmlA.href = xmlUrl;

    jsonLink.appendChild(jsonA);
    xmlLink.appendChild(xmlA);
    // let link = document.createTextNode("Загрузить");
    // jsonA.appendChild(link);
    // xmlA.appendChild(link);
    
    
    // jsonLink.innerText = convertationResult.jsonDownloadPath;
    // xmlLink.innerText = convertationResult.xmlDownloadPath;

    trow.appendChild(created);
    trow.appendChild(name);
    trow.appendChild(jsonLink);
    trow.appendChild(xmlLink);

    eventList.prepend(trow);
};

connection.on("fileConverted", result => {
    addEventToList(result);
});

// const postFile = formData => {
//     let request = new XMLHttpRequest();
//     request.onreadystatechange = () => {
//         if (request.readyState === 4) {
//             if (request.status !== 200) {
//                 addEventToList("Error on converting file");
//             }
//         }
//     };
//
//     request.open("POST", "/send");
//
//     request.setRequestHeader("Content-Type", "multipart/form-data");
//
//     request.send(formData);
// };

connection.start().catch(err => console.error(err.toString()));