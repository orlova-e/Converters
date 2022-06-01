const eventList = document.getElementById("convertationTableBody");

const connection = new signalR.HubConnectionBuilder().withUrl("/hubs").build();

document.getElementById("sendFile").addEventListener("click", event => {
    var formData = new FormData();

    var file = document.getElementById("fileInput");

    formData.append("file", file.files[0]);

    postFile(formData);
});

connection.on("fileConverted", result => {
    addEventToList(result);
});

connection.start().catch(err => console.error(err.toString()));

const postFile = file => {
    let request = new XMLHttpRequest();
    request.onreadystatechange = () => {
        if (request.readyState === 4) {
            if (request.status !== 200) {
                addEventToList("Error on converting file");
            }
        }
    };

    request.open("POST", "/converters");

    request.setRequestHeader("Content-Type", "application/octet-stream");

    request.send(file);
};

const addEventToList = convertationResult => {
    let table = document.getElementById("convertationTable");

    if (table.style.display === "none") {
        table.style.display = "block";
    }

    let trow = document.createElement("tr");

    let name = document.createElement("td");
    let created = document.createElement("td");
    let jsonLink = document.createElement("td");
    let xmlLink = document.createElement("td");

    name.innerText = convertationResult.Name;
    created.innerText = convertationResult.Created;
    jsonLink.innerText = convertationResult.Json;
    xmlLink.innerText = convertationResult.Xml;

    trow.appendChild(name);
    trow.appendChild(created);
    trow.appendChild(jsonLink);
    trow.appendChild(xmlLink);

    eventList.prependChild(trow);
};
