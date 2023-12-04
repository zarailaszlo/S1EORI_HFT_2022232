function backtomain() {
    window.location.assign("index.html");
}

let noncrud = [];
//document.getElementById('statisticsDone').style.display = 'block';
//document.getElementById('statisticsResults').style.display = 'block';

async function ReadUsersOlderThan(age) {
    document.getElementById('statisticsResults').style.display = 'block';
    document.getElementById('statTitle').innerHTML =
        '<h2>Users Older Than ' + age + '</h2>';
    await fetch('http://localhost:58986/Stat/ReadUsersOlderThan?age=' + age)
        .then(x => x.json())
        .then(users => {
            displayUsers(users, 'statisticsDone');
            console.log(users) 

        });
}
async function GetCommitCountForRepository(repositoryId) {
    document.getElementById('statisticsResults').style.display = 'block';
    document.getElementById('statTitle').innerHTML =
        '<h2>Commit Count For Repository (id: ' + repositoryId + ')</h2>';
    await fetch('http://localhost:58986/Stat/GetCommitCountForRepository?repositoryId=' + repositoryId)
        .then(x => x.json())
        .then(commitCount => {
            displayCommitCount(commitCount, 'statisticsDone');
        });
}
async function ReadRepositoryStats() {
    document.getElementById('statisticsResults').style.display = 'block';
    document.getElementById('statTitle').innerHTML =
        '<h2>Repository Stats</h2>';
    await fetch('http://localhost:58986/Stat/ReadRepositoryStats')
        .then(x => x.json())
        .then(stats => {
            displayRepositoryStats(stats, 'statisticsDone');
        });
}

async function GroupRepositoriesByVisibility() {
    document.getElementById('statisticsResults').style.display = 'block';
    document.getElementById('statTitle').innerHTML =
        '<h2>Repositories By Visibility</h2>';
    await fetch('http://localhost:58986/Stat/GroupRepositoriesByVisibility')
        .then(x => x.json())
        .then(groupedStats => {
            displayGroupedRepos(groupedStats, 'statisticsDone');
        });
}

async function ReadUsersWithZeroRepositories() {
    document.getElementById('statisticsResults').style.display = 'block';
    document.getElementById('statTitle').innerHTML =
        '<h2>Users With Zero Repositories</h2>';
    await fetch('http://localhost:58986/Stat/ReadUsersWithZeroRepositories')
        .then(x => x.json())
        .then(users => {
            displayUsersWithZeroRepos(users, 'statisticsDone');
        });
}


function displayUsers(users, containerId) {
    const container = document.getElementById(containerId);
    let htmlContent = '<ul>';
    users.forEach(user => {
        htmlContent += `<li>${user.username}, Age: ${user.age}</li>`;
    });
    htmlContent += '</ul>';
    container.innerHTML = htmlContent;
}
function displayCommitCount(commitCount, containerId) {
    const container = document.getElementById(containerId);
    container.innerHTML = `<p>Commit Count: ${commitCount}</p>`;
}
function displayRepositoryStats(stats, containerId) {
    const container = document.getElementById(containerId);
    let htmlContent = '<ul>';
    stats.forEach(stat => {
        htmlContent += `<li>Repository: ${stat.repositoryName}, User ID: ${stat.userId}, Commit Count: ${stat.commitCount}, Average Commit Length: ${stat.averageCommitLength}</li>`;
    });
    htmlContent += '</ul>';
    container.innerHTML = htmlContent;
}
function displayGroupedRepos(groupedStats, containerId) {
    const container = document.getElementById(containerId);
    let htmlContent = '<ul>';
    groupedStats.forEach(stat => {
        htmlContent += `<li>Visibility: ${stat.visibility}, Repository Count: ${stat.repositoryCount}</li>`;
    });
    htmlContent += '</ul>';
    container.innerHTML = htmlContent;
}
function displayUsersWithZeroRepos(users, containerId) {
    const container = document.getElementById(containerId);
    let htmlContent = '<ul>';
    users.forEach(user => {
        htmlContent += `<li>Username: ${user.username}, Full Name: ${user.fullName}, Email: ${user.email}</li>`;
    });
    htmlContent += '</ul>';
    container.innerHTML = htmlContent;
}

