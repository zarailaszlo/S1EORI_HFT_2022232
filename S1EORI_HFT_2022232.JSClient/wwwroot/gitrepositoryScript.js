function backtomain() {
    window.location.assign("index.html");
}

let gitRepositories = [];
let connection = null;

getData();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:58986/hub')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("GitRepositoryCreated", (user, message) => {
        getData();
    });

    connection.on("GitRepositoryDeleted", (user, message) => {
        getData();
    });

    connection.on("GitRepositoryDeleted", (user, message) => {
        getData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getData() {
    await fetch('http://localhost:58986/gitRepository')
        .then(response => response.json())
        .then(data => {
            gitRepositories = data; 
            display('repositoryTable'); 
        });
}

function createRow(gitRepositoryData) {
    return `
        <tr>
            <td>${gitRepositoryData.idGitRepository}</td>
            <td>${gitRepositoryData.name}</td>
            <td>${gitRepositoryData.visibility}</td>
            <td>${gitRepositoryData.createdDate}</td>
            <td>${gitRepositoryData.userId}</td>
            <td><button onclick="remove(${gitRepositoryData.idGitRepository})">Delete</button>  <button onclick="update(${gitRepositoryData.idGitRepository})">Update</button></td>
        </tr>
    `;
}

function display(tableId) {
    const tableBody = document.getElementById(tableId);
    tableBody.innerHTML = '';
    gitRepositories.forEach(user => {
        tableBody.innerHTML += createRow(user);
    });
}

function create() {
    let namesub = document.getElementById('Name').value;
    let visibilitysub = document.getElementById('Visibility').value;
    let createdDatesub = document.getElementById('CreatedDate').value;
    let userIdsub = document.getElementById('UserId').value;

    fetch('http://localhost:58986/gitRepository', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: namesub,
                visibility: visibilitysub,
                createdDate: createdDatesub,
                userId: userIdsub, 
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
            getData();
        });
}

function updateGitRepository() {
    let idsub = document.getElementById('GitRepositoryID').value;
    let namesub = document.getElementById('NameUpdate').value;
    let visibilitysub = document.getElementById('VisibilityUpdate').value;
    let createdDatesub = document.getElementById('CreatedDateUpdate').value;
    let userIdsub = document.getElementById('UserIdUpdate').value;
    fetch('http://localhost:58986/gitRepository', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                idGitRepository: idsub,
                name: namesub,
                visibility: visibilitysub,
                createdDate: createdDatesub,
                userId: userIdsub,
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
            getData();
        });
}
function showUpdateForm(gitRepositoryID) {
    const gitRepo = gitRepositories.find(u => u.idGitRepository === gitRepositoryID);
    if (gitRepo) {
        var createdDateTime = gitRepo.createdDate;
        var formattedDateTime = createdDateTime.slice(0, 16);
        document.getElementById('GitRepositoryID').value = gitRepo.idGitRepository;
        document.getElementById('NameUpdate').value = gitRepo.name;
        document.getElementById('VisibilityUpdate').value = gitRepo.visibility;
        document.getElementById('CreatedDateUpdate').value = formattedDateTime;
        document.getElementById('UserIdUpdate').value = gitRepo.userId;
        document.getElementById('updateFormDiv').style.display = 'block';
    }
}
function update(gitRepositoryID) {
    showUpdateForm(gitRepositoryID);
}

function remove(id) {
    fetch('http://localhost:58986/gitRepository/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}