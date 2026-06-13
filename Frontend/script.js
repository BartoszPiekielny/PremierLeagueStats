const API_URL = "http://localhost:5050";

function renderPlayers(players)
{
    const table =
        document.getElementById("playersTable");

    table.innerHTML = "";

    players.forEach(player =>
    {
        table.innerHTML += `
            <tr>
                <td>${player.firstName}</td>
                <td>${player.lastName}</td>
                <td>${player.nationality}</td>
                <td>${player.position}</td>
            </tr>
        `;
    });
}

async function loadPlayers()
{
    const response =
        await fetch(`${API_URL}/api/players`);

    const data =
        await response.json();

    renderPlayers(data);
}

async function searchNationality()
{
    const nationality =
        document.getElementById(
            "nationalityInput")
            .value;

    const response =
        await fetch(
            `${API_URL}/api/players/nationality/${nationality}`);

    const data =
        await response.json();

    renderPlayers(data);
}

async function searchClub()
{
    const club =
        document.getElementById(
            "clubInput")
            .value;

    const response =
        await fetch(
            `${API_URL}/api/players/clubname/${club}`);

    const data =
        await response.json();

    renderPlayers(data);
}

async function importPlayers()
{
    await fetch(
        `${API_URL}/api/import/players`,
        {
            method: "POST"
        });

    alert("Players imported");
}

async function importCoaches()
{
    await fetch(
        `${API_URL}/api/import/coaches`,
        {
            method: "POST"
        });

    alert("Coaches imported");
}
async function searchPosition()
{
    const position =
        document.getElementById("positionInput").value;

    const response =
        await fetch(
            `${API_URL}/api/players/position/${position}`);

    const data =
        await response.json();

    renderPlayers(data);
}
async function searchPlayer()
{
    const name =
        document.getElementById("nameInput").value;

    const response =
        await fetch(
            `${API_URL}/api/players/search?name=${encodeURIComponent(name)}`);

    if (!response.ok)
    {
        alert("Player not found");
        return;
    }

    const data = await response.json();

    renderPlayers(data);
}