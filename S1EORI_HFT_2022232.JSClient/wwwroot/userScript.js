function backtomain() {
    window.location.assign("index.html");
}

let users = []; 
let connection = null;

getData(); // Call the function to get data from the server
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:58986/hub')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("UserCreated", (user, message) => {
        getData();
    });

    connection.on("UserDeleted", (user, message) => {
        getData();
    });
    connection.on("UserUpdated", (user, message) => {
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
    // Fetch user data
    await fetch('http://localhost:58986/user')
        .then(response => response.json())
        .then(data => {
            users = data; // Store data in the array
            display('userTable1'); // Call display function for the first table
        });
}

// Function to create table rows
function createRow(userData) {
    return `
        <tr>
            <td>${userData.idUser}</td>
            <td>${userData.username}</td>
            <td>${userData.password}</td>
            <td>${userData.fullName}</td>
            <td>${userData.email}</td>
            <td>${userData.age}</td>
            <td><button onclick="remove(${userData.idUser})">Delete</button>  <button onclick="update(${userData.idUser})">Update</button></td>
        </tr>
    `;
}

// Function to display data in a specified table
function display(tableId) {
    const tableBody = document.getElementById(tableId);
    tableBody.innerHTML = ''; // Clear existing data
    users.forEach(user => {
        tableBody.innerHTML += createRow(user);
    });
}



function togglePassword() {
    var passwordInput = document.getElementById('Password');
    var type = passwordInput.type === 'password' ? 'text' : 'password';
    passwordInput.type = type;
    // Change button text accordingly
    this.textContent = type === 'password' ? 'Show Password' : 'Hide Password';
}

function create() {
    let usernamesub = document.getElementById('Username').value;
    let passwordsub = document.getElementById('Password').value;
    let fullNamesub = document.getElementById('FullName').value;
    let emailsub = document.getElementById('Email').value;
    let agesub = document.getElementById('Age').value;
    fetch('http://localhost:58986/user', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                username: usernamesub,
                password: passwordsub,
                fullName: fullNamesub,
                email: emailsub,
                age: agesub,
            }),
    })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => {
            console.error('Error:', error);
            getData();
        });
}

function updateUser() {
    let idsub = document.getElementById('UserID').value;
    let usernamesub = document.getElementById('UsernameUpdate').value;
    let passwordsub = document.getElementById('PasswordUpdate').value;
    let fullNamesub = document.getElementById('FullNameUpdate').value;
    let emailsub = document.getElementById('EmailUpdate').value;
    let agesub = document.getElementById('AgeUpdate').value;
    fetch('http://localhost:58986/user', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                idUser: idsub,
                username: usernamesub,
                password: passwordsub,
                fullName: fullNamesub,
                email: emailsub,
                age: agesub,
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
function showUpdateForm(userId) {
    const user = users.find(u => u.idUser === userId);
    if (user) {
        document.getElementById('UserID').value = user.idUser;
        document.getElementById('UsernameUpdate').value = user.username;
        document.getElementById('PasswordUpdate').value = user.password;
        document.getElementById('FullNameUpdate').value = user.fullName;
        document.getElementById('EmailUpdate').value = user.email;
        document.getElementById('AgeUpdate').value = user.age;
        document.getElementById('updateFormDiv').style.display = 'block';
    }
}
function update(userId) {
    showUpdateForm(userId);
}

function remove(id) {
    fetch('http://localhost:58986/user/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) =>
        {
            console.error('Error:', error);
        });
}