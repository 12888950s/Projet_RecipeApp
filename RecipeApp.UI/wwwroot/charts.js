window.renderCharts = function (
    catLabels, catValues,
    cuisineLabels, cuisineValues,
    topLabels, topValues,
    avgLabels, avgValues
) {
    const colors = ['#4e73df', '#1cc88a', '#36b9cc', '#f6c23e', '#e74a3b', '#858796'];

    new Chart(document.getElementById('categoryChart'), {
        type: 'doughnut',
        data: { labels: catLabels, datasets: [{ data: catValues, backgroundColor: colors }] }
    });

    new Chart(document.getElementById('cuisineChart'), {
        type: 'bar',
        data: { labels: cuisineLabels, datasets: [{ label: 'Recettes', data: cuisineValues, backgroundColor: colors }] },
        options: { plugins: { legend: { display: false } } }
    });

    // ✅ Corrigé : indexAxis: 'y' remplace horizontalBar
    new Chart(document.getElementById('topCaloriesChart'), {
        type: 'bar',
        data: { labels: topLabels, datasets: [{ label: 'Calories', data: topValues, backgroundColor: '#e74a3b' }] },
        options: {
            indexAxis: 'y',
            plugins: { legend: { display: false } }
        }
    });

    new Chart(document.getElementById('avgCaloriesChart'), {
        type: 'bar',
        data: { labels: avgLabels, datasets: [{ label: 'Moy. Calories', data: avgValues, backgroundColor: '#f6c23e' }] },
        options: { plugins: { legend: { display: false } } }
    });
};s