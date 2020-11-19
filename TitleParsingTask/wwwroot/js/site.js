(function main() {
    document.getElementById('get-data-button').addEventListener('click', eventHandler);

})();

function eventHandler(event) {
    var textarea = document.getElementById('urls');
    let tableBody = document.getElementById('sites-response');
    let urls = textarea.value.split('\n');

    tableBody.innerHTML = "";

    urls.forEach((url) => {
        postData('/api/title', { url: url })
            .then((response) => {
                tableBody.innerHTML += `
                    <tr>
                        <td>${response.url}</td>
                        <td>${response.title}</td>
                        <td>${response.responseCode}</td>
                    </tr>
                `;
            });
    });
}


async function postData(url = '', data = {}) {
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });
    return await response.json();
}

