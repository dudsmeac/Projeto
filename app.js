function loadData() {
    const periodSelect = document.getElementById('periodo');
    const horas = periodSelect.value;

    fetch(`http://localhost:7072/api/Dados/average/${horas}`)
        .then(response => response.json())
        .then(data => {
            const labels = data.map(item => item.EquipmentId);
            const values = data.map(item => item.AverageValue);
            renderChart(labels, values);
        })
        .catch(error => console.error('Erdataror fetching :', error));
}

function renderChart(labels, data) {
    const ctx = document.getElementById('sensorChart').getContext('2d');
    const chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Valor Médio',
                data: data,
                backgroundColor: 'rgba(54, 162, 235, 0.5)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
