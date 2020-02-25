//функция для сброса масштабирования графика
function resetZoom()
{
    window.myLine.resetZoom();
}
//настройки для графика
let config =
{
    type: 'line',
    datasets: [{
    }],

    options: {
        maintainAspectRatio: false,
        legend: {
            display: false,
            position: 'top',
            labels: {
                boxWidth: 80,
                fontColor: 'black'
            }
        },
        scales: {
            xAxes:
                [{
                    scaleLabel:
                    {
                        display: true
                    }
                 }],
            yAxes:
                [{
                    scaleLabel:
                    {
                        display: true
                    }
                 }]
        },
        pan:
        {
            enabled: true,
            mode: 'x',
            speed: 1
        },
        zoom: {
            enabled: true,
            drag: false,
            mode: 'x',
            speed: 0.01
        }
    }

};

let config1 =
{
    type: 'bar',
    data: {
      labels: [],
      datasets: [{
        label: ['График'],
        data: [],
        backgroundColor: [
          'rgba(255, 99, 132, 0.2)',
          'rgba(54, 162, 235, 0.2)',
          'rgba(255, 206, 86, 0.2)',
          'rgba(75, 192, 192, 0.2)',
          'rgba(153, 102, 255, 0.2)',
          'rgba(255, 159, 64, 0.2)',
          'rgba(255, 99, 132, 0.2)',
          'rgba(54, 162, 235, 0.2)',
          'rgba(255, 206, 86, 0.2)',
          'rgba(75, 192, 192, 0.2)',
          'rgba(153, 102, 255, 0.2)',
          'rgba(255, 159, 64, 0.2)'
        ],
        borderColor: [
          'rgba(255,99,132,1)',
          'rgba(54, 162, 235, 1)',
          'rgba(255, 206, 86, 1)',
          'rgba(75, 192, 192, 1)',
          'rgba(153, 102, 255, 1)',
          'rgba(255, 159, 64, 1)',
          'rgba(255,99,132,1)',
          'rgba(54, 162, 235, 1)',
          'rgba(255, 206, 86, 1)',
          'rgba(75, 192, 192, 1)',
          'rgba(153, 102, 255, 1)',
          'rgba(255, 159, 64, 1)'
        ],
        borderWidth: 1
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        xAxes: [{
          ticks: {
            maxRotation: 90,
            minRotation: 80
          }
        }],
        yAxes: [{
          ticks: {
            beginAtZero: true
          }
        }]
      },
      legend: {
        display: true
      },  
      pan:
      {
          enabled: true,
          mode: 'x',
          speed: 1
      },
      zoom: {
          enabled: true,
          drag: false,
          mode: 'x',
          speed: 0.01
      }
    }
  };

window.onload = function ()
{
    var ctx = document.getElementById("chart");
    window.myLine = new Chart(ctx, config);
    var ctx1 = document.getElementById("diagram");
    window.myLine1 = new Chart(ctx1, config1);
    // canvas.width = parent.offsetWidth;
    // canvas.height = parent.offsetHeight;
    // var result = this.GetObjFromStorage();
    // if (result == null)
    //     return;

    // result = this.ObjFromJson(result);
    //showChart2(result);
};
//Функция показать график
function showChart2(Result)
{

    color = "27,110,194"
    config.data = {
        labels: Result[Result.ParameterSelect],
        datasets: [{
            data: Result.P,
            borderColor: "rgb( " + color + ")",
            backgroundColor: "transparent",
            pointBorderColor: "rgb( " + color + ")",
            pointBackgroundColor: "rgb( " + color + ")",
            pointBorderWidth: 1,

        }]
    }
    config.options.scales.yAxes[0].scaleLabel.labelString = "P";
    config.options.scales.xAxes[0].scaleLabel.labelString = Result.ParameterSelect;
    window.myLine.update();
};


function showChart2(Result)
{

    color = "27,110,194"
    config.data = {
        labels: Result[Result.ParameterSelect],
        datasets: [{
            data: Result.P,
            borderColor: "rgb( " + color + ")",
            backgroundColor: "transparent",
            pointBorderColor: "rgb( " + color + ")",
            pointBackgroundColor: "rgb( " + color + ")",
            pointBorderWidth: 1,

        }]
    }
    config.options.scales.yAxes[0].scaleLabel.labelString = "P";
    config.options.scales.xAxes[0].scaleLabel.labelString = Result.ParameterSelect;
    window.myLine.update();
};
