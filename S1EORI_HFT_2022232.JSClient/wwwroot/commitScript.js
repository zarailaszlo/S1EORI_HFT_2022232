function backtomain() {
    window.location.assign("index.html");
}

let commits = [];
let connection = null;

getData(); 
setupSignalR();

function fillData() {
    document.getElementById('Hash').value = 'qzhf812';
    document.getElementById('Message').value = 'Ez egy teszt üzenet';
    document.getElementById('CommittedDate').value = '2023-11-22T10:30';
    document.getElementById('GitRepositoryId').value = 2;
    document.getElementById('UserId').value = 3;
}
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:58986/hub')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CommitCreated", (user, message) => {
        getData();
    });

    connection.on("CommitDeleted", (user, message) => {
        getData();
    });

    connection.on("CommitDeleted", (user, message) => {
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
    await fetch('http://localhost:58986/commit')
        .then(response => response.json())
        .then(data => {
            commits = data;
            display('commitTable'); 
        });
}

function createRow(commitData) {
    return `
        <tr>
            <td>${commitData.idCommit}</td>
            <td>${commitData.hash}</td>
            <td>${commitData.message}</td>
            <td>${commitData.committedDate}</td>
            <td>${commitData.gitRepositoryId}</td>
            <td>${commitData.userId}</td>
            <td><button onclick="remove(${commitData.idCommit})">Delete</button>  <button onclick="update(${commitData.idCommit})">Update</button></td>
        </tr>
    `;
}

function display(tableId) {
    const tableBody = document.getElementById(tableId);
    tableBody.innerHTML = '';
    commits.forEach(user => {
        tableBody.innerHTML += createRow(user);
    });
}

function create() {
    let hashsub = document.getElementById('Hash').value;
    let messagesub = document.getElementById('Message').value;
    let committedDatesub = document.getElementById('CommittedDate').value;
    let gitRepositoryIdsub = document.getElementById('GitRepositoryId').value;
    let userIdsub = document.getElementById('UserId').value;

    fetch('http://localhost:58986/commit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                hash: hashsub,
                message: messagesub,
                committedDate: committedDatesub,
                gitRepositoryId: gitRepositoryIdsub,
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

function updateCommit() {
    let idsub = document.getElementById('CommitId').value;
    let hashsub = document.getElementById('HashUpdate').value;
    let messagesub = document.getElementById('MessageUpdate').value;
    let committedDatesub = document.getElementById('CommittedDateUpdate').value;
    let gitRepositoryIdsub = document.getElementById('GitRepositoryIdUpdate').value;
    let userIdsub = document.getElementById('UserIdUpdate').value;
    fetch('http://localhost:58986/commit', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                idCommit: idsub,
                hash: hashsub,
                message: messagesub,
                committedDate: committedDatesub,
                gitRepositoryId: gitRepositoryIdsub,
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
function showUpdateForm(commitId) {
    const comm = commits.find(u => u.idCommit === commitId);
    if (comm) {
        var commitDateTime = comm.committedDate;
        var formattedDateTime = commitDateTime.slice(0, 16); 
        document.getElementById('CommittedDateUpdate').value = formattedDateTime;
        document.getElementById('CommitId').value = comm.idCommit;
        document.getElementById('HashUpdate').value = comm.hash;
        document.getElementById('MessageUpdate').value = comm.message;
        document.getElementById('GitRepositoryIdUpdate').value = comm.gitRepositoryId;
        document.getElementById('UserIdUpdate').value = comm.userId;
        document.getElementById('updateFormDiv').style.display = 'block';
    }
}
function update(commitId) {
    showUpdateForm(commitId);
}

function remove(id) {
    fetch('http://localhost:58986/commit/' + id, {
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
