

$.ajax({
    url: "https://localhost:44348/API/Universities/GetStudents",
    success: function (result) {
        console.log(result);

        var univName = []
        var univStudents = []

        for (var i = 0; i < result.result.key.length; i++) {

            univName.push(result.result.key[i])

        }

        for (var i = 0; i < result.result.value.length; i++) {

            univStudents.push(result.result.value[i])

        }

        console.log("UniversityName")
        console.log(univName);
        console.log("Students")
        console.log(univStudents);
      

        var options = {
            chart: {
                type: 'bar',
                    toolbar: {
                        show: true,
                        offsetX: 0,
                        offsetY: 0,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true,
                            reset: true | '<img src="/static/icons/reset.png" width="20">',
                                customIcons: []
    },
    export: {
        csv: {
            filename: undefined,
            columnDelimiter: ',',
            headerCategory: 'category',
            headerValue: 'value',
            dateFormatter(timestamp) {
                return new Date(timestamp).toDateString()
            }
        },
        svg: {
            filename: undefined,
        },
        png: {
            filename: undefined,
        }
    },
    autoSelected: 'zoom'
},

            },
            series: [{
                name: 'sales',
                data: [univStudents[0],
                    univStudents[1],
                    univStudents[2],
                    univStudents[3],
                    univStudents[4]
            
                ]
            }],
            xaxis: {
                categories: [
                    univName[0],
                    univName[1],
                    univName[2],
                    univName[3],
                    univName[4]
                ]
            }
        }

        var chart = new ApexCharts(document.querySelector("#chart1"), options);

        chart.render();

    }
})


$.ajax({
    url: "https://localhost:44348/API/Employees",
    success: function (result) {
        console.log(result);
        var male = 0
        var female = 0

        for (var i = 0; i < result.length; i++) {
            if (result[i].gender == 0) {
                male++
            }

            if (result[i].gender == 1) {
                female++
            }
        }
        console.log("Male")
        console.log(male)
        console.log("Female")
        console.log(female)

        var options = {

            series: [male, female],
            chart: {
                width: 380,
                type: 'pie',
                toolbar: {
                    show: true,
                    offsetX: 0,
                    offsetY: 0,
                    tools: {
                        download: true,
                        selection: true,
                        zoom: true,
                        zoomin: true,
                        zoomout: true,
                        pan: true,
                        reset: true | '<img src="/static/icons/reset.png" width="20">',
                        customIcons: []
                    },
                    export: {
                        csv: {
                            filename: undefined,
                            columnDelimiter: ',',
                            headerCategory: 'category',
                            headerValue: 'value',
                            dateFormatter(timestamp) {
                                return new Date(timestamp).toDateString()
                            }
                        },
                        svg: {
                            filename: undefined,
                        },
                        png: {
                            filename: undefined,
                        }
                    },
                    autoSelected: 'zoom'
                },
            },
            labels: ['Male', 'Female'],
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };

        var chart = new ApexCharts(document.querySelector("#chart2"), options);
        chart.render();
    }
})




$.ajax({
    url: "https://localhost:44348/API/Employees",
    success: function (result) {
        console.log(result);
        var diatas20 = 0
        var dibawah20 = 0

        for (var i = 0; i < result.length; i++) {
            if (result[i].salary > 20) {
                diatas20++
            }
            else
            {
                dibawah20++
            }
        }
        console.log("Diatas20")
        console.log(diatas20)
        console.log("Dibawah20")
        console.log(dibawah20)

var options = {
    series: [diatas20,dibawah20],
    chart: {
        type: 'donut',
        toolbar: {
            show: true,
            offsetX: 0,
            offsetY: 0,
            tools: {
                download: true,
                selection: true,
                zoom: true,
                zoomin: true,
                zoomout: true,
                pan: true,
                reset: true | '<img src="/static/icons/reset.png" width="20">',
                customIcons: []
            },
            export: {
                csv: {
                    filename: undefined,
                    columnDelimiter: ',',
                    headerCategory: 'category',
                    headerValue: 'value',
                    dateFormatter(timestamp) {
                        return new Date(timestamp).toDateString()
                    }
                },
                svg: {
                    filename: undefined,
                },
                png: {
                    filename: undefined,
                }
            },
            autoSelected: 'zoom'
        },
    },
    labels: ['Diatas 20', 'Dibawah 20'],
    responsive: [{
        breakpoint: 480,
        options: {
            chart: {
                width: 200
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
};

var chart = new ApexCharts(document.querySelector("#chart3"), options);
        chart.render();
    }
})