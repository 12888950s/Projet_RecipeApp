let categoryChartInstance = null;
let cuisineChartInstance = null;
let topCaloriesChartInstance = null;
let avgCaloriesChartInstance = null;

window.renderCharts = function (
    catLabels, catValues,
    cuisineLabels, cuisineValues,
    topLabels, topValues,
    avgLabels, avgValues
) {
    const colors = [
        '#4e73df',
        '#1cc88a',
        '#36b9cc',
        '#f6c23e',
        '#e74a3b',
        '#858796'
    ];

    const commonOptions = {
        responsive: true,
        maintainAspectRatio: false,
        animation: {
            duration: 800
        },
        plugins: {
            tooltip: {
                backgroundColor: '#111827',
                titleColor: '#ffffff',
                bodyColor: '#ffffff',
                padding: 12,
                cornerRadius: 10
            },
            legend: {
                labels: {
                    usePointStyle: true,
                    boxWidth: 8,
                    font: {
                        size: 12
                    }
                }
            }
        }
    };

    destroyChart(categoryChartInstance);
    destroyChart(cuisineChartInstance);
    destroyChart(topCaloriesChartInstance);
    destroyChart(avgCaloriesChartInstance);

    const categoryCanvas = document.getElementById('categoryChart');
    const cuisineCanvas = document.getElementById('cuisineChart');
    const topCaloriesCanvas = document.getElementById('topCaloriesChart');
    const avgCaloriesCanvas = document.getElementById('avgCaloriesChart');

    if (!categoryCanvas || !cuisineCanvas || !topCaloriesCanvas || !avgCaloriesCanvas) {
        return;
    }

    categoryChartInstance = new Chart(categoryCanvas, {
        type: 'doughnut',
        data: {
            labels: catLabels,
            datasets: [{
                data: catValues,
                backgroundColor: colors,
                borderColor: '#ffffff',
                borderWidth: 2,
                hoverOffset: 8
            }]
        },
        options: {
            ...commonOptions,
            cutout: '62%',
            plugins: {
                ...commonOptions.plugins,
                legend: {
                    position: 'top',
                    labels: {
                        usePointStyle: true,
                        boxWidth: 8,
                        font: {
                            size: 12
                        }
                    }
                }
            }
        }
    });

    cuisineChartInstance = new Chart(cuisineCanvas, {
        type: 'bar',
        data: {
            labels: cuisineLabels,
            datasets: [{
                label: 'Recettes',
                data: cuisineValues,
                backgroundColor: colors,
                borderRadius: 10,
                maxBarThickness: 70
            }]
        },
        options: {
            ...commonOptions,
            plugins: {
                ...commonOptions.plugins,
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        precision: 0
                    },
                    grid: {
                        color: '#e5e7eb'
                    }
                },
                x: {
                    grid: {
                        display: false
                    }
                }
            }
        }
    });

    topCaloriesChartInstance = new Chart(topCaloriesCanvas, {
        type: 'bar',
        data: {
            labels: topLabels,
            datasets: [{
                label: 'Calories',
                data: topValues,
                backgroundColor: '#e74a3b',
                borderRadius: 10,
                maxBarThickness: 36
            }]
        },
        options: {
            ...commonOptions,
            indexAxis: 'y',
            plugins: {
                ...commonOptions.plugins,
                legend: {
                    display: false
                }
            },
            scales: {
                x: {
                    beginAtZero: true,
                    grid: {
                        color: '#e5e7eb'
                    }
                },
                y: {
                    grid: {
                        display: false
                    },
                    ticks: {
                        font: {
                            size: 11
                        }
                    }
                }
            }
        }
    });

    avgCaloriesChartInstance = new Chart(avgCaloriesCanvas, {
        type: 'bar',
        data: {
            labels: avgLabels,
            datasets: [{
                label: 'Moy. Calories',
                data: avgValues,
                backgroundColor: '#f6c23e',
                borderRadius: 10,
                maxBarThickness: 70
            }]
        },
        options: {
            ...commonOptions,
            plugins: {
                ...commonOptions.plugins,
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    grid: {
                        color: '#e5e7eb'
                    }
                },
                x: {
                    grid: {
                        display: false
                    }
                }
            }
        }
    });
};

function destroyChart(chartInstance) {
    if (chartInstance !== null) {
        chartInstance.destroy();
    }
}