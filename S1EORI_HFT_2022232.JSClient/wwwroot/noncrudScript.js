function backtomain() {
    window.location.assign("index.html");
}

let noncrud = [];
function hideElements() {
    const elementIdsToHide = ['usersOlderThanResults', 'commitCountResults', 'repositoryStatsResults', 'groupedReposResults', 'usersWithZeroReposResults'];
    document.getElementById('statisticsResults').style.display = 'block';
    elementIdsToHide.forEach(id => {
        const element = document.getElementById(id);
        if (element) {
            element.style.display = 'none';
        }
    });
}

async function ReadUsersOlderThan(age) {
    hideElements();
    document.getElementById('statTitle').innerHTML =
        '<h2>Users Older Than ' + age + '</h2>';
    document.getElementById('usersOlderThanResults').style.display = 'block';
    await fetch('http://localhost:58986/Stat/ReadUsersOlderThan?age=' + age)
        .then(x => x.json())
        .then(users => {
            displayUsers(users, 'usersOlderThanResults');
            console.log(users) 

        });
}
async function GetCommitCountForRepository(repositoryId) {
    hideElements();
    document.getElementById('statTitle').innerHTML =
        '<h2>Commit Count For Repository (id: ' + repositoryId + ')</h2>';
    document.getElementById('commitCountResults').style.display = 'block';
    await fetch('http://localhost:58986/Stat/GetCommitCountForRepository?repositoryId=' + repositoryId)
        .then(x => x.json())
        .then(commitCount => {
            displayCommitCount(commitCount, 'commitCountResults');
        });
}
async function ReadRepositoryStats() {
    hideElements();
    document.getElementById('statTitle').innerHTML =
        '<h2>Repository Stats</h2>';
    document.getElementById('repositoryStatsResults').style.display = 'block';
    await fetch('http://localhost:58986/Stat/ReadRepositoryStats')
        .then(x => x.json())
        .then(stats => {
            displayRepositoryStats(stats, 'repositoryStatsResults');
        });
}

async function GroupRepositoriesByVisibility() {
    hideElements();
    document.getElementById('statTitle').innerHTML =
        '<h2>Repositories By Visibility</h2>';
    document.getElementById('groupedReposResults').style.display = 'block';
    await fetch('http://localhost:58986/Stat/GroupRepositoriesByVisibility')
        .then(x => x.json())
        .then(groupedStats => {
            displayGroupedRepos(groupedStats, 'groupedReposResults');
        });
}

async function ReadUsersWithZeroRepositories() {
    hideElements();
    document.getElementById('statTitle').innerHTML =
        '<h2>Users With Zero Repositories</h2>';
    document.getElementById('usersWithZeroReposResults').style.display = 'block';
    await fetch('http://localhost:58986/Stat/ReadUsersWithZeroRepositories')
        .then(x => x.json())
        .then(users => {
            displayUsersWithZeroRepos(users, 'usersWithZeroReposResults');
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

