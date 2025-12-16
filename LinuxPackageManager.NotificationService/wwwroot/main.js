document.addEventListener("DOMContentLoaded", function() {

    const statusDiv = document.getElementById('status');
    const messagesDiv = document.getElementById('messages');

    function log(text, type = 'log') {
        const div = document.createElement('div');
        div.className = type;
        div.textContent = `[${new Date().toLocaleTimeString()}] ${text}`;
        messagesDiv.appendChild(div);
        messagesDiv.scrollTop = messagesDiv.scrollHeight;
    }

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7125/ws/notifications") // Путь теперь совпадает с Program.cs
        .withAutomaticReconnect()
        .withKeepAliveInterval(15000)
        .build();
    
    connection.on("ReceiveMessage", (message) => {
        log(`Получено: ${message}`, 'msg');
    });
    
    connection.on("ReceiveRatingNotification", (data) => {
        console.log("Получено событие рейтинга:", data);
        showNotification(data);
    });

    connection.onreconnecting(error => {
        statusDiv.textContent = 'Status: Reconnecting...';
        statusDiv.style.color = 'orange';
    });

    connection.onreconnected(connectionId => {
        statusDiv.textContent = 'Status: Connected';
        statusDiv.style.color = 'green';
    });

    connection.onclose(error => {
        statusDiv.textContent = 'Status: Disconnected';
        statusDiv.style.color = 'red';
    });

    async function start() {
        try {
            await connection.start();
            statusDiv.textContent = 'Status: Connected';
            statusDiv.style.color = 'green';
            log('Соединение установлено');
        } catch (err) {
            log(`Ошибка подключения: ${err}`);
        }
    }
    
    function showNotification(data) {
        const div = document.createElement('div');
        div.className = `notification verdict-${data.verdict}`;

        div.innerHTML = `
            <strong>Пользователь ID: ${data.userId}</strong><br>
            Рейтинг рассчитан: <b>${data.score}</b><br>
            Вердикт: ${data.verdict}
        `;
        
        messagesDiv.prepend(div);
    }

    start();
});