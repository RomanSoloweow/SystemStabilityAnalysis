$('.menu .item')
 .tab()
;

$('.ui.dropdown')
 .dropdown()
;

// $('.ui.form')
//   .form({
//     fields: {
//       value: {
//         identifier  : 'value',
//         rules: [
//           {
//             type   : 'empty'
//           }
//         ]
//       }
//     }
//   })
// ;

$('.message .close').on('click', function() {
  $(this).closest('.message').transition('fade');
});

$(".ui.icon.button.plus").click(addFilter);
$(".ui.icon.button.minus").click(deleteFilter);
$(".ui.button.next").click(nextPage);
$(".ui.button.next-tab").click(nextTab);
$(".ui.button.delete-all").click(deleteAll);
$(".ui.dropdown.names").click(getNames);
$(".ui.dropdown.conditions").click(getConditions);
$(".ui.button.save-system").click({url: "GetParametersWithEnter", param: "filename"},saveFile);
$(".ui.button.save-system1").click({url: "GetParametersWithEnter", param: "filename"},saveFile1);
$(".ui.button.save-restrictions").click({url: "SaveRestrictionsToFile", param: "parametersWithEnter"},saveFile);
$(".ui.button.upload-csv").click(uploadCsv);
$(".ui.button.generate-report").click(generateReport);
$(".ui.button.validate").click(validateSystem);
$(".ui.button.create-linear-chart").click(createLinearChart);
$(".ui.button.create-diagram").click(createDiagram);
$(".ui.button.new-system").click(newSystem);
$(".ui.button.delete1").click(deleteSystem);
$(".ui.button.upload-csv2").click(downloadSystem2);
$(".rezet-zoom-chart").click(()=> window.myLine.resetZoom());
$(".rezet-zoom-diag").click();
$(".download-system-1").click(downloadSystem1);
$(".ui.button.upload-csv4").click(uploadCsv4);

$('.ui.dropdown.names').change(function(){
  setTimeout(()=>{
    currentElement = $(".ui.dropdown.names").find(".item.active");
    $('.disField').find(".name").val(currentElement.attr("data-name"));
    $('.disField').find(".unit").val(currentElement.attr("data-unit"));
  }, 1);
});

function nextPage(event){
  let currentSegment = $(event.target).closest('.segment')
  let attr = $(event.target).closest('.segment').attr("data-tab");
  let currentTab = $(event.target).closest('.segment').parent().find(".menu").find(`[data-tab='${attr}']`)
  currentTab = currentTab.removeClass('active');
  let nextTab = $(currentTab.next()[0])
  
  currentTab.next().tab('change tab', nextTab.attr('data-tab'))
}

function nextTab(event){
  $(".item[data-tab='second']").removeClass('active');
  $(".item[data-tab='second']").next().tab('change tab', 'third')
}

function newSystem(event){
  $(".item[data-tab='third']").removeClass('active');
  $(".item[data-tab='second']").tab('change tab', 'second')
  $(".item[data-tab='second/a']").addClass('active');
  $(".item[data-tab='second/b']").removeClass('active');
  $(".item[data-tab='second/c']").removeClass('active');
}

function addFilter(){
  let parameter = $(".ui.names").find(".item.active").attr("data-value");
  let condition = $(".ui.conditions ").find(".item.active").attr("data-value");
  let value = $(".input.value ").val();
  $.ajax({
    method: "GET",
    url: `Restrictions/AddRestriction?parameter=${parameter == undefined ? "" : parameter}&condition=${condition == undefined ? "" : condition}&value=${value == undefined ? "" : value}`,
  }).done(function(msg){
    if (msg.status == "Success") {
      if ($(".ui.celled.table.restructions").length == 0 ) {
        $('.ui.form.form1').append(`<table class="ui celled blue table center aligned restructions">
        <thead>
          <tr>
            <th>Наименование показателя</th>
            <th>Обозначение</th>
            <th>Единица измерения</th>
            <th>Условие</th>
            <th>Значение</th>
            <th class="minus one wide center aligned"></th>
          </tr>
        </thead>
        <tbody>
        </tbody>
        </table>`)
      }
      $(".ui.celled.table.restructions tbody").after(`<tr>
      <td data-label="description" data-value=${msg.restrictionName}>${msg.description}</td>
      <td data-label="name">${msg.name}</td>
      <td data-label="unit">${msg.unit}</td>
      <td data-label="condition">${msg.condition}</td>
      <td data-label="value">${msg.value}</td>
      <td data-label="button" class="center aligned" >
        <button class="ui icon button minus">
          <i class="minus icon"></i>
        </button>
      </td>
      </tr>`)
      $(".ui.icon.button.minus").unbind();
      $(".ui.icon.button.minus").click(deleteFilter);
      clearFilters();
    }
    else
      notification(msg.status,msg.message,"first");
    getNames();
  });
}

function deleteFilter(){
  $.ajax({
    method: "GET",
    url: `Restrictions/DeleteRestriction?restrictionName=${$( this ).parent().parent().find("[data-label='description']").attr("data-value")}`,
  })
  $( this ).parent().parent().remove();
  if ($(".ui.celled.table tr").length == 1) {
    $(".ui.celled.table").remove();
  }
  $(".ui.dropdown.names").find(".menu").empty();
}


function deleteAll(){
  $.ajax({
    method: "GET",
    url: `Restrictions/DeleteAllRestriction`,
  })
  $(".ui.dropdown.names").find(".menu").empty();
  $(".ui.celled.table").remove()
}

function getNames(){
  let currentCombobox = $(".ui.dropdown.names");
  if (currentCombobox.find(".menu").children().length == 1 ) {
    $.ajax({
      method: "GET",
      url: "Restrictions/GetParameters",
    }).done(function(msg){
      
      if (msg.status == "Success") {
        currentCombobox.find(".menu").empty();
        currentCombobox.dropdown('clear')
        $.each( msg.properties, function( key, value ) {
          currentCombobox.find(".menu").append(`<div class="item" 
            data-value="${value.value}"
            data-text="${value.description}"
            data-unit="${value.unit}"
            data-name="${value.name}"
            > 
            ${value.description} </div>
          `)
        });
        currentCombobox.dropdown('refresh')
      }
    });
  }
}

function getConditions(){
  let currentCombobox = $(".ui.dropdown.conditions");
  if (currentCombobox.find(".menu").children().length == 1 ) {
    $.ajax({
      method: "GET",
      url: "Restrictions/GetConditions",
    }).done(function(msg){
      if (msg.status == "Success") {
        currentCombobox.find(".menu").empty();
        currentCombobox.dropdown('refresh')
        $.each( msg.conditions, function( key, value ) {
          currentCombobox.find(".menu").append(`<div class="item" 
            data-value="${value.value}"
            data-text="${value.name}"
            > 
            ${value.name} </div>
          `)
        });
        currentCombobox.dropdown('refresh')
      }
    });
  }
}

function notification(type, message, tabNum){
  messages = []
  $.each(message, function( index, value ) {
    messages.push(value.replace(/^/,'&#8226 '));
  });
  message = messages.join("<br>");
  information = type == "Success" ? ["positive","Успешно!"] : ["negative", "Ошибка!"];
  messageElement =
  `
    <div class="ui ${information[0]} message row">
      <i class="close icon"></i>
      <div class="header">
        ${information[1]}
      </div>
      <p>${message}
    </p></div>
  `
  $(`.ui.tab.segment[data-tab='${tabNum}']`).prepend(messageElement);
  $('.message .close').unbind();
  $('.message .close')
    .on('click', function() {
      $(this)
        .closest('.message')
        .transition('fade')
      ;
    })
  ;
  if (type == "Success") {
    setTimeout(() => {
      $(".message .close").trigger("click");
    }, 5000);
  }
  
}

function clearFilters(){
  $(".ui.dropdown.names").find(".menu").empty();
  $(".ui.dropdown.names").dropdown('clear');
  $(".ui.dropdown.conditions").dropdown('clear');
  $('.disField').find(".name").val('');
  $('.disField').find(".unit").val('');
  $('.input.value').val('');
}

function saveFile(event){
  if ($(event.target).parent().parent().find(".ui.input.save-system").length == 0) {

    $.ajax({
      method: "GET",
      url: `Restrictions/ValidateRestrictionsBeforeSave`,
    }).done(function(msg){
      if (msg.status == "Success") {
        element = $(event.target).is( ":button" ) ? $(event.target) : $(event.target).parent()
        element.before(`
          <div class="ui input save-system">
            <input type="text" placeholder="Имя">
          </div>
        `);
      } 
      else {
        notification("Error", msg.message,"first")
      }

    });
  }
  else {

    filename = $(".ui.input.save-system").find("input").val();
    if (filename.length > 0) {
        const url = `Restrictions/SaveRestrictionsToFile?filename=${filename}`;
        const a = document.createElement('a');
        a.style.display = 'none';
        a.href = url;
        // the filename you want
        a.download = `${filename}.csv`;
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(url);
      // const url = ``;
      // const a = document.createElement('a');
      // a.style.display = 'none';
      // a.href = url;
      // // the filename you want
      // a.download = `${filename}.csv`;
      // document.body.appendChild(a);
      // a.click();
      
      // window.URL.revokeObjectURL(url);
    }
    else {
      notification("Error",["Введите имя файла"],"first")
    }
  }
}


function saveFile1(event){

  if ($(event.target).parent().parent().find(".ui.input.save-system1").length == 0) {
    
    $.ajax({
      method: "GET",
      url: `Systems/ValidateSystemBeforeSave`,
    }).done(function(msg){
      if (msg.status == "Success") {
        element = $(event.target).is( ":button" ) ? $(event.target) : $(event.target).parent()
        element.before(`
          <div class="ui input save-system1">
            <input type="text" placeholder="Имя">
          </div>
        `);
      } 
      else {
        notification("Error", msg.message,"second/c")
      }
    });
  }
  else {
    filename = $(".ui.input.save-system1").find("input").val();
    if (filename.length > 0) {
      const url = `Systems/SaveSystemToFile?filename=${filename}`;
        const a = document.createElement('a');
        a.style.display = 'none';
        a.href = url;
        // the filename you want
        a.download = `${filename}.csv`;
        document.body.appendChild(a);
        a.click();
        
        window.URL.revokeObjectURL(url);
      
    }
    else {
      notification("Error",["Введите имя файла"],"first")
    }
  }
}

$('#FileUpload_FormFile').on('change', function(e) {
  $('#upload-csv').click();
});

function uploadCsv(){
  $('#FileUpload_FormFile').click();
}

$(".item[data-tab='second'").tab({'onVisible':function(){

  $(".tab.segment[data-tab='second/a']").find('table').remove()
  $(".tab.segment[data-tab='second/a']").append(`
      <table class="ui celled blue table center aligned analys">
            <thead>
              <tr>
                <th>Наименование показателя</th>
                <th>Обозначение</th>
                <th>Единица измерения</th>
                <th>Значение показателя</th>
              </tr>
            </thead>
          <tbody>
        </tbody>
      </table>
    `)
  $.ajax({
    method: "GET",
    url: "Systems/GetParametersWithEnter",
  }).done(function(msg){
    if (msg.status == "Success") {
      $.each( msg.parametersWithEnter, function( key, value ) {
        $(".tab.segment[data-tab='second/a']").find('tbody').append(`<tr>
          <td data-label="description" data-value=${value.name}>${value.description}</td>
          <td data-label="name">${value.designation}</td>
          <td data-label="unit">${value.unit}</td>
          <td data-label="button" class="center aligned" >
          <div class="ui input validate-div">
            <input type="number" placeholder="" class="system-validate" value="${value.value}">
          </div>
          </td>
          </tr>`
        )
      });
    }
    else {
      notification("Error",msg.message,"second/a")
    }
  });  
}});

$(".item[data-tab='second/b'").tab({'onVisible':function(){
  $(".tab.segment[data-tab='second/b']").find('table').remove()
  $(".tab.segment[data-tab='second/b']").append(`
      <table class="ui celled blue table center aligned analys">
            <thead>
              <tr>
                <th>Наименование показателя</th>
                <th>Обозначение</th>
                <th>Единица измерения</th>
                <th>Значение показателя</th>
              </tr>
            </thead>
          <tbody>
        </tbody>
      </table>
    `)
  $.ajax({
    method: "GET",
    url: "Systems/GetParametersWithCalculate",
  }).done(function(msg){
    if (msg.status == "Success") {
      $.each( msg.parametersWithCalculate, function( key, value ) {
        $(".tab.segment[data-tab='second/b']").find('tbody').append(`<tr class="${value.correct == true ? "" : "error"}">
          <td data-label="description">${value.description}</td>
          <td data-label="name">${value.designation}</td>
          <td data-label="unit">${value.unit}</td>
          <td data-label="value">${value.value}</td>
          </tr>`
        )
      });
    }
    $(".message").remove()
    notification("Error",msg.message,"second/b")
  });  
}});


$(".item[data-tab='second/c'").tab({'onVisible':function(){
  $(".tab.segment[data-tab='second/c']").find('table').remove();
  $(".tab.segment[data-tab='second/c']").append(`
      <table class="ui celled blue table center aligned analys">
            <thead>
              <tr>
                <th>Наименование показателя</th>
                <th>Обозначение</th>
                <th>Единица измерения</th>
                <th>Значение показателя</th>
              </tr>
            </thead>
          <tbody>
        </tbody>
      </table>
    `)
  
  $.ajax({
    method: "GET",
    url: "Systems/GetParametersForAnalysis",
  }).done(function(msg){
    if (msg.status == "Success") {
      if ($(".tab.segment[data-tab='second/c']").find(".header.result").length == 0) {
        $(".tab.segment[data-tab='second/c']").find("table").before(`<div class='ui large message'>
          <div class="result header">
            U = ${msg.u}
          </div>
          ${msg.result}
          </div>`
        )
      }
      $.each( msg.parametersForAnalysis, function( key, value ) {
        $(".tab.segment[data-tab='second/c']").find('tbody').append(`<tr class="${value.correct == true ? "" : "error"}">
          <td data-label="description">${value.description}</td>
          <td data-label="name">${value.designation}</td>
          <td data-label="unit">${value.unit}</td>
          <td data-label="value">${value.value}</td>
          </tr>`
        )
      });
      $(".negative.message").remove()
    }
    notification("Error",msg.message,"second/c")
  });  
}});

$(".item[data-tab='third/b']").tab({'onVisible':function(){
  getSystems();
  getParamDiagram(); 
}});

$(".item[data-tab='third/c']").tab({'onVisible':function(){
  getSystems();
  getParamChart(); 
}});

$(".item[data-tab='third']").tab({'onVisible':function(){
  $.ajax({
    method: "GET",
    url: "Analysis/GetSystems",
  }).done(function(msg){
    if (msg.status == "Success") {
      // let list = $(".tab.segment[data-tab='third/a']").find(".system-list");
      // list.empty();
      // $.each( msg.systems, function( key, value ) {
      //   list.append(`
      //     <button class="ui button system-item">${value}</button>
      //   `
      //   )
      // });
      if ($(".list.system-list1").length == 0)
      $(".segment.active[data-tab='third/a']").append(
        `<div class="ui segment system-segment">
        <div class="ui divided list system-list1">
    
        </div>
      </div>`
      )
      let list = $(".tab.segment[data-tab='third/a']").find(".system-list1");
      list.empty();
      $.each( msg.systems, function( key, value ) {
        list.append(`

        <div class="item">
          <div class="right floated content">
            <button class="circular ui icon button mini delete1">
              <i class="icon delete"></i>
            </button>
          </div>
          <div class="content system-list-item">
          
            ${value}
          </div>
        </div>
        `
        )
        $(".delete1").unbind();
        $(".delete1").click(deleteSystem)
      });
    }
  });  
}});

function validateSystem() {
  let description = $(".tab.segment[data-tab='second/a']").find('tr [data-label="description"]');
  let td_value = $(".tab.segment[data-tab='second/a']").find('.system-validate');
  let blocked = 0
  validationArr = []
  $.each(description, function( index, value ) {
    validationArr.push({parameterName: $(value).attr("data-value"), value: $(td_value[index]).val()})
  });
  $.ajax({
    method: "GET",
    url: `Systems/Validate`,
    data: {validateArr: JSON.stringify(validationArr)}
  }).done(function(msg){
    $.each(msg.parametersCorrect, function(index, value){
      if (value.correct == false)
        $("[data-tab='second/a']").find("table").find(`td[data-value='${value.parameterName}']`).parent().find(".validate-div").addClass("error")
      else
      $("[data-tab='second/a']").find("table").find(`td[data-value='${value.parameterName}']`).parent().find(".validate-div").removeClass("error")
    });
    $(".message").remove()
    notification("Error",msg.message,"second/a")

  });
}

function getSystems(){
  let currentCombobox = $(".ui.dropdown.systems-cb");
  currentCombobox.find(".menu").empty();
  //currentCombobox.dropdown('clear');
  $.ajax({
    method: "GET",
    url: "Analysis/GetSystems",
  }).done(function(msg){
    if (msg.status == "Success") {
      $.each( msg.systems, function( key, value ) {
        currentCombobox.find(".menu").append(`<div class="item" 
          data-text="${value}"
          > 
          ${value} </div>
        `)
      });
      currentCombobox.dropdown('refresh')
    }
  });
  
}

function getParamDiagram(){
  let currentCombobox = $(".ui.dropdown.param-diag");
  currentCombobox.find(".menu").empty();
  //currentCombobox.dropdown('clear');
  $.ajax({
    method: "GET",
    url: "Analysis/GetParametersForDiagram",
  }).done(function(msg){
    if (msg.status == "Success") {
      $.each( msg.parametersForDiagram, function( key, value ) {
        currentCombobox.find(".menu").append(`<div class="item" 
          data-name="${value.name}"
          data-description="${value.description}"
          data-value="${value.value}"
          > 
          ${value.description} </div>
        `)
      });
      currentCombobox.dropdown('refresh')
    }
  });
}

function getParamChart(){
  let currentCombobox = $(".ui.dropdown.param-chart");
  currentCombobox.find(".menu").empty();
  //currentCombobox.dropdown('clear');
  $.ajax({
    method: "GET",
    url: "Analysis/GetParametersForChart",
  }).done(function(msg){
    if (msg.status == "Success") {
      $.each( msg.parametersForChart, function( key, value ) {
        currentCombobox.find(".menu").append(`<div class="item" 
          data-name="${value.name}"
          data-description="${value.description}"
          data-value="${value.value}"
          > 
          ${value.description} </div>
        `)
      });
      currentCombobox.dropdown('refresh')
    }
  });
  
}

function createLinearChart(){
  let params = {}
  params.namesSystems = []
  $(".ui.systems-cb.system-chart").find(".item.active").each(function( index, element  ) {
    params.namesSystems.push($(element).attr("data-text"))
  });
  params.from = $(".linear-chart-from").val();
  params.to = $(".linear-chart-to").val();
  params.countDote = $(".linear-chart-dots-count").val();
  params.parameterName = $(".ui.param-chart").find(".item.active").attr("data-value");
  $.ajax({
    method: "GET",
    url: "Analysis/GetCalculationForChart",
    data: {queryString: JSON.stringify(params)}
  }).done(function(msg){
      showChart4(msg)
      if (msg.hasOwnProperty('message')) {
        notification("Error",msg.message,'third/c')
      }
  });
  Result = JSON.parse("{\u0022ParameterSelect\u0022:\u0022Tn\u0022,\u0022Tn\u0022:[20.0,20.11111111111111,20.22222222222222,20.333333333333332,20.444444444444443,20.555555555555554,20.666666666666664,20.777777777777775,20.888888888888886],\u0022P\u0022:[2.434181973074066E-07,2.1785025870934773E-07,1.9496790177364581E-07,1.744890408809124E-07,1.5616121992637724E-07,1.3975849976991407E-07,1.2507867355899056E-07,1.11940773583199E-07,1.0018284036333434E-07]}")

}

function createDiagram(){
  let params = {}
  params.namesSystems = []
  $(".ui.systems-cb.system-diagram").find(".item.active").each(function( index, element  ) {
    params.namesSystems.push($(element).attr("data-text"))
  });
  params.parameterName = $(".ui.param-diag").find(".item.active").attr("data-value");
  $.ajax({
    method: "GET",
    url: "Analysis/GetCalculationForDiagram",
    data: {queryString: JSON.stringify(params)}
  }).done(function(msg){
    showChart3(msg)
    if (msg.hasOwnProperty('message')) {
      notification("Error",msg.message,'third/b')
    }
    
  });
}

async function AJAXSubmit (oFormElement) {

  const formData = new FormData(oFormElement);
  try {
  const response = await fetch(oFormElement.action, {
    method: 'POST',
    body: formData
  })
  .then(response => response.json())
  .then(msg => {
    if (msg.status == "Success") {
      $(".ui.celled.table.restructions").remove()
      $('.ui.form.form1').append(`<table class="ui celled blue table center aligned restructions">
      <thead>
        <tr>
          <th>Наименование показателя</th>
          <th>Обозначение</th>
          <th>Единица измерения</th>
          <th>Условие</th>
          <th>Значение</th>
          <th class="minus one wide center aligned"></th>
        </tr>
      </thead>
      <tbody>
      </tbody>
      </table>`)
      $.each( msg.restrictions, function( key, value ) {
        $(".ui.celled.table.restructions tbody").append(`<tr>
        <td data-label="description" data-value=${value.description}>${value.description}</td>
        <td data-label="name">${value.name}</td>
        <td data-label="unit">${value.unit}</td>
        <td data-label="condition">${value.condition}</td>
        <td data-label="value">${value.value}</td>
        <td data-label="button" class="center aligned" >
          <button class="ui icon button minus">
            <i class="minus icon"></i>
          </button>
        </td>
        </tr>`)
      });
      $(".ui.icon.button.minus").unbind();
      $(".ui.icon.button.minus").click(deleteFilter);
    }
    else {
      notification("Error", msg.message,"first")
    }
    $("#FormAJAX")[0].reset();
  });
  
  } catch (error) {
    notification("Error", error,"first")
    console.error('Error:', error);
  }
  }


function showChart3(Result)
{
  Chart.plugins.register({
    beforeDraw: function(c) {
        var legends = c.legend.legendItems;
        legends.forEach(function (e, i) {
        e.fillStyle = 'rgba(255,255,255,1)'
        e.strokeStyle = 'rgba(255, 255, 255, 1)'
      });
    }
  });

  let labels = [];
  let diagData = [];
  let colors = [];
  $.each(Result.calculations, function(index, element){
    labels.push(element.nameSystem);
    diagData.push(element.value);
    colors.push(`rgba(${Math.floor(Math.random() * Math.floor(255))}, ${Math.floor(Math.random() * Math.floor(255))}, ${Math.floor(Math.random() * Math.floor(255))}, 0.5)`)
  });
  color = "27,110,194"
  config1.data = {
      labels: labels,
      datasets: [{
          label: [Result.parameterName],
          data: diagData,
          borderColor: color,
          backgroundColor:  colors,
          pointBorderColor: "rgb( " + color + ")",
          pointBackgroundColor: "rgb( " + color + ")",
          pointBorderWidth: 1,

      }]
  }
  config1.options.legend.display = true
  window.myLine1.update();
};

function showChart4(Result)
{
  Chart.plugins.register({
    beforeDraw: function(c) {
      //   var legends = c.legend.legendItems;
      //   legends.forEach(function (e, i) {
      //   e.fillStyle = 'rgba(255,255,255,1)'
      //   e.strokeStyle = 'rgba(255, 255, 255, 1)'
      // });
    }
  });

  let labels = [];
  let diagData = [];
  let colors = [];
  let datasets = []
  $.each(Result.calculations, function(index, element){
    // labels.push(element.nameSystem);
    // diagData.push(element.value);
    let color = `rgba(${Math.floor(Math.random() * Math.floor(255))}, ${Math.floor(Math.random() * Math.floor(255))}, ${Math.floor(Math.random() * Math.floor(255))}, 0.5)`
    let dataset = {
      label: element.nameSystem,
      data: element.values,
      borderColor: color,
      backgroundColor: "transparent",
      pointBorderColor: color,
      pointBackgroundColor: color,
      pointBorderWidth: 1,
    };
    datasets.push(dataset)
    //colors.push(`rgba(${Math.floor(Math.random() * Math.floor(255))}, ${Math.floor(Math.random() * Math.floor(255))}, ${Math.floor(Math.random() * Math.floor(255))}, 0.5)`)
  });
  color = "27,110,194"
  config.data = {
      labels: Result.parameterNameX,
      datasets: datasets
  }
  config.options.legend.display = true
  config.options.scales.yAxes[0].scaleLabel.labelString = Result.parameterNameY;
  config.options.scales.xAxes[0].scaleLabel.labelString = Result.parameterNameX;
  window.myLine.update();



};

function downloadSystem1(){
  $('#FileUpload_FormFile1').click();
};

$('#FileUpload_FormFile1').on('change', function(e) {
  $('#upload-csv1').click();
});

async function AJAXSubmit1 (oFormElement) {
  const formData = new FormData(oFormElement);
  try {
  const response = await fetch(oFormElement.action, {
    method: 'POST',
    body: formData
  })
  .then(response => response.json())
  .then(msg => {
    if (msg.status == "Success") {
      $(".tab.segment[data-tab='second/a']").find('table').remove()
      $(".tab.segment[data-tab='second/a']").append(`
          <table class="ui celled blue table center aligned analys">
                <thead>
                  <tr>
                    <th>Наименование показателя</th>
                    <th>Обозначение</th>
                    <th>Единица измерения</th>
                    <th>Значение показателя</th>
                  </tr>
                </thead>
              <tbody>
            </tbody>
          </table>
        `)
      $.ajax({
        method: "GET",
        url: "Systems/GetParametersWithEnter",
      }).done(function(msg){
        if (msg.status == "Success") {
          $.each( msg.parametersWithEnter, function( key, value ) {
            $(".tab.segment[data-tab='second/a']").find('tbody').append(`<tr>
              <td data-label="description" data-value=${value.name}>${value.description}</td>
              <td data-label="name">${value.designation}</td>
              <td data-label="unit">${value.unit}</td>
              <td data-label="button" class="center aligned" >
              <div class="ui input validate-div">
                <input type="number" placeholder="" class="system-validate" value="${value.value}">
              </div>
              </td>
              </tr>`
            )
          });
        }
      }); 
    }
    else {
      notification("Error", msg.message,"second")
    }
    $("#FormAJAX1")[0].reset();
  });
  
  } catch (error) {
    notification("Error", msg.message,"second")
    console.error('Error:', error);
  }
  }


function generateReport(){

  if ($(event.target).parent().parent().find(".ui.input.save-system2").length == 0) {


    $.ajax({
      method: "GET",
      url: `Systems/ValidateSystemBeforeSave`,
    }).done(function(msg){
      if (msg.status == "Success") {
        element = $(event.target).is( ":button" ) ? $(event.target) : $(event.target).parent()
        element.before(`
          <div class="ui input save-system2">
            <input type="text" placeholder="Имя">
          </div>
        `);
      } 
      else {
        notification("Error", msg.message,"second/c")
      }
    });
  }
  else {
    filename = $(".ui.input.save-system2").find("input").val();
    if (filename.length > 0) {
      const url = `Systems/GenerateReport?filename=${filename}`;
      const a = document.createElement('a');
      a.style.display = 'none';
      a.href = url;
      // the filename you want
      a.download = `${filename}.csv`;
      document.body.appendChild(a);
      a.click();
      window.URL.revokeObjectURL(url);
    }
    else {
      notification("Error",["Введите имя файла"],"first")
    }
  }
}

function deleteSystem(event){
  let currentButton;
  if ( $( event.target ).is( ":button" ) ) {
    currentButton =  event.target 
  }
  else {
    currentButton =  event.target.closest("button");
  }
  let curElement = $(currentButton).parent().parent();
  let textElement = curElement.find(".system-list-item").html().replace(/\s/g, '');
  // <div class="ui divided list system-list1">

  //   </div>

  $.ajax({
    method: "GET",
    url: `Analysis/DeleteSystem`,
    data: {nameSystem: textElement}
  }).done(function(msg){
    if (msg.status == "Success") {
        $(curElement).remove();
        if ($(".list.system-list1").children().length == 0)
          $(".system-segment").remove()
    }
    else
      notification("Error",msg.message,"third/a")
  });
  
}



function downloadSystem2(){
  $('#FileUpload_FormFile2').click();
};

$('#FileUpload_FormFile2').on('change', function(e) {
  $('#upload-csv2').click();
});

async function AJAXSubmit2 (oFormElement) {
  const formData = new FormData(oFormElement);
  try {
  const response = await fetch(oFormElement.action, {
    method: 'POST',
    body: formData
  })
  .then(response => response.json())
  .then(msg => {
    if (msg.status == "Success") {
      $.ajax({
        method: "GET",
        url: "Analysis/GetSystems",
      }).done(function(msg){
        if (msg.status == "Success") {
          // let list = $(".tab.segment[data-tab='third/a']").find(".system-list");
          // list.empty();
          // $.each( msg.systems, function( key, value ) {
          //   list.append(`
          //     <button class="ui button system-item">${value}</button>
          //   `
          //   )
          // });
          if ($(".list.system-list1").length == 0)
          $(".segment.active[data-tab='third/a']").append(
            `<div class="ui segment system-segment">
            <div class="ui divided list system-list1">
        
            </div>
          </div>`
          )
          let list = $(".tab.segment[data-tab='third/a']").find(".system-list1");
          list.empty();
          $.each( msg.systems, function( key, value ) {
            list.append(`
    
            <div class="item">
              <div class="right floated content">
                <button class="circular ui icon button mini delete1">
                  <i class="icon delete"></i>
                </button>
              </div>
              <div class="content system-list-item">
              
                ${value}
              </div>
            </div>
            `
            )
            $(".delete1").unbind();
            $(".delete1").click(deleteSystem)
          });
        }
      });  
    }
    else {
      notification("Error", msg.message,"second")
    }
    $("#FormAJAX2")[0].reset();
  });
  
  } catch (error) {
    notification("Error", msg.message,"second")
    console.error('Error:', error);
  }
  }



  $('#FileUpload_FormFile4').on('change', function(e) {
    $('#upload-csv4').click();
  });
  
  function uploadCsv4(){
    $('#FileUpload_FormFile4').click();
  }


  async function AJAXSubmit4 (oFormElement) {
    const formData = new FormData(oFormElement);
    try {
    const response = await fetch(oFormElement.action, {
      method: 'POST',
      body: formData
    })
    .then(response => response.json())
    .then(msg => {
      if (msg.status == "Success") {
        if ($(".ui.celled.table.restructions").length == 0)
          $('.ui.form.form1').append(`<table class="ui celled blue table center aligned restructions">
          <thead>
            <tr>
              <th>Наименование показателя</th>
              <th>Обозначение</th>
              <th>Единица измерения</th>
              <th>Условие</th>
              <th>Значение</th>
              <th class="minus one wide center aligned"></th>
            </tr>
          </thead>
          <tbody>
          </tbody>
          </table>`)
        $.each( msg.restrictions, function( key, value ) {
          $(".ui.celled.table.restructions tbody").append(`<tr>
          <td data-label="description" data-value=${value.description}>${value.description}</td>
          <td data-label="name">${value.name}</td>
          <td data-label="unit">${value.unit}</td>
          <td data-label="condition">${value.condition}</td>
          <td data-label="value">${value.value}</td>
          <td data-label="button" class="center aligned" >
            <button class="ui icon button minus">
              <i class="minus icon"></i>
            </button>
          </td>
          </tr>`)
        });
        $(".ui.icon.button.minus").unbind();
        $(".ui.icon.button.minus").click(deleteFilter);
      }
      notification("Error", msg.message,"first")
      $("#FormAJAX4")[0].reset();
    });
    
    } catch (error) {
      notification("Error", msg.message,"second")
      console.error('Error:', error);
    }
    }