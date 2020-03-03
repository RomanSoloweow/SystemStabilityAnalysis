$('.menu .item')
 .tab()
;

$('.ui.dropdown')
 .dropdown()
;

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
$(".export-diag").click(exportDiag);
$(".export-chart").click(exportChart);

$('.ui.dropdown.names').change(function(){
  setTimeout(()=>{
    currentElement = $(".ui.dropdown.names").find(".item.active");
    $('.disField').find(".name").val(currentElement.attr("data-name"));
    $('.disField').find(".unit").val(currentElement.attr("data-unit"));
  }, 1);
});

function nextPage(event){
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

function addFilter(event){
  let parameter = $(".ui.names").find(".item.active").attr("data-value");
  let condition = $(".ui.conditions ").find(".item.active").attr("data-value");
  let value = $(".input.value ").val();
  $.ajax({
    method: "GET",
    url: `Restrictions/AddRestriction?parameter=${parameter == undefined ? "" : parameter}&condition=${condition == undefined ? "" : condition}&value=${value == undefined ? "" : value}`,
  }).done(function(msg){
    if (msg.status != "negative") {
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
    if (msg.message.length > 0)
      notification(msg.status, msg.header, msg.message,event.target);
    getNames();
  });
}

function deleteFilter(event){
  $.ajax({
    method: "GET",
    url: `Restrictions/DeleteRestriction?restrictionName=${$( this ).parent().parent().find("[data-label='description']").attr("data-value")}`,
  }).done(function(msg){
    if (msg.status != "negative") {
      $( this ).parent().parent().remove();
      if ($(".ui.celled.table tr").length == 1) {
        $(".ui.celled.table").remove();
      }
      $(".ui.dropdown.names").find(".menu").empty();
    }
    if (msg.message.length > 0)
      notification(msg.status, msg.header, msg.message,event.target);
  });
  
}


function deleteAll(event){
  $.ajax({
    method: "GET",
    url: `Restrictions/DeleteAllRestriction`,
  }).done(function(msg){
    if (msg.status != "negative") {
      $(".ui.dropdown.names").find(".menu").empty();
      $(".ui.celled.table").remove()
    }
    if (msg.message.length > 0)
      notification(msg.status, msg.header, msg.message,event.target);
  });

}

function getNames(event){
  let currentCombobox = $(".ui.dropdown.names");
  if (currentCombobox.find(".menu").children().length == 1 ) {
    $.ajax({
      method: "GET",
      url: "Restrictions/GetParameters",
    }).done(function(msg){
      if (msg.status != "negative") {
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
      if (msg.message.length > 0)
        notification(msg.status, msg.header, msg.message,event.target);
    });
  }
}

function getConditions(event){
  let currentCombobox = $(".ui.dropdown.conditions");
  if (currentCombobox.find(".menu").children().length == 1 ) {
    $.ajax({
      method: "GET",
      url: "Restrictions/GetConditions",
    }).done(function(msg){
      if (msg.status != "negative") {
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
      if (msg.message.length > 0)
        notification(msg.status, msg.header, msg.message,event.target);
    });
  }
}

function notification(type, header, message, place){
  tabNum = typeof place == "string" ? place : $(place).closest(".tab.segment").siblings().filter(".menu").find(".active").attr("data-tab")
  messages = []
  $.each(message, function( index, value ) {
    messages.push(value.replace(/^/,'&#8226 '));
  });
  message = messages.join("<br>");
  messageElement =
  `
    <div class="ui ${type} message row">
      <i class="close icon"></i>
      <div class="header">
        ${header}
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
  if (type != "negative" && type != "error") {
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
      if (msg.status != "negative") {
        element = $(event.target).is( ":button" ) ? $(event.target) : $(event.target).parent()
        element.before(`
          <div class="ui input save-system">
            <input type="text" placeholder="Имя">
          </div>
        `);
      } 
      if (msg.message.length > 0) {
        notification(msg.status,  msg.header, msg.message,event.target)
      }
    });
  }
  else {
    filename = $(".ui.input.save-system").find("input").val();
    if (filename.length > 0) {
      $(".ui.input.save-system").removeClass("error")
      const url = `Restrictions/SaveRestrictionsToFile?filename=${filename}`;
      const a = document.createElement('a');
      a.style.display = 'none';
      a.href = url;
      a.download = `${filename}.csv`;
      document.body.appendChild(a);
      a.click();
      window.URL.revokeObjectURL(url);
    }
    else {
      notification("error","Ошибка",["Введите имя файла"],event.target)
      $(".ui.input.save-system").addClass("error")
    }
  }
}


function saveFile1(event){
  if ($(event.target).parent().parent().find(".ui.input.save-system1").length == 0) {
    $.ajax({
      method: "GET",
      url: `Systems/ValidateSystemBeforeSave`,
    }).done(function(msg){
      if (msg.status != "negative") {
        element = $(event.target).is( ":button" ) ? $(event.target) : $(event.target).parent()
        element.before(`
          <div class="ui input save-system1">
            <input type="text" placeholder="Имя">
          </div>
        `);
      } 
      if (msg.message.length > 0) {
        notification(msg.status,  msg.header, msg.message,event.target)
      }
    });
  }
  else {
    filename = $(".ui.input.save-system1").find("input").val();
    if (filename.length > 0) {
      $(".save-system1").removeClass("error");
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
      notification("error","Ошибка",["Введите имя файла"],event.target)
      $(".save-system1").addClass("error");
    }
  }
}

$('#FileUpload_FormFile').on('change', function(e) {
  $('#upload-csv').click();
});

function uploadCsv(){
  $('#FileUpload_FormFile').click();
}

$(".item[data-tab='second']").tab({'onVisible':function(event){
  activeTab = $("[data-tab='second']").find(".item.active").attr("data-tab");
  switch (activeTab) {
    case "second/a":
      secondATab(event);
      break;
    case "second/b":
      secondBTab(event);
      break;
    case "second/c":
      secondCTab(event);
      break;
  }
}});

$(".item[data-tab='second/a']").tab({'onVisible':function(event){
  secondATab(event);
}});

$(".item[data-tab='second/b']").tab({'onVisible':function(event){
  secondBTab(event);
}});


$(".item[data-tab='second/c']").tab({'onVisible':function(event){
 secondCTab(event); 
}});

$(".item[data-tab='third/b']").tab({'onVisible':function(event){
  getSystems(event);
}});

$(".item[data-tab='third/c']").tab({'onVisible':function(event){
  getSystems(event);
}});

$(".item[data-tab='third/a']").tab({'onVisible':function(event){
  $.ajax({
    method: "GET",
    url: "Analysis/GetSystems",
  }).done(function(msg){
    if (msg.status != "negative") {
      // let list = $(".tab.segment[data-tab='third/a']").find(".system-list");
      // list.empty();
      // $.each( msg.systems, function( key, value ) {
      //   list.append(`
      //     <button class="ui button system-item">${value}</button>
      //   `
      //   )
      // });
      if ($(".list.system-list1").length == 0 && msg.systems.length > 0)
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
    if (msg.message.length > 0) {
      notification(msg.status,  msg.header, msg.message,event)
    }
  });  
}});

function validateSystem(event) {
  let description = $(".tab.segment[data-tab='second/a']").find('tr [data-label="description"]');
  let td_value = $(".tab.segment[data-tab='second/a']").find('.system-validate');
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
    $(".message").remove()
    if (msg.message.length > 0)
      notification(msg.status,  msg.header, msg.message,event.target)
  });
}

function getSystems(event){
  let currentCombobox = $(".ui.dropdown.systems-cb");
  currentCombobox.find(".menu").empty();
  $.ajax({
    method: "GET",
    url: "Analysis/GetSystems",
  }).done(function(msg){
    if (msg.status != "negative") {
      $.each( msg.systems, function( key, value ) {
        currentCombobox.hasClass("multiple") ? "" : currentCombobox.addClass("multiple");
        currentCombobox.find(".menu").append(`<div class="item" 
          data-text="${value}"
          > 
          ${value} 
          </div>
        `)
      });
      if (msg.systems.length == 0) {
        currentCombobox.hasClass("multiple") ? currentCombobox.removeClass("multiple") : "";
        currentCombobox.find(".menu").append(`<div class="disabled item"> 
          Нет добавленных систем
          </div>
        `)
      }
      currentCombobox.dropdown('refresh')
    }
    if (msg.message.length > 0)
        notification(msg.status, msg.header, msg.message,event);
  });
  
}

$('.ui.dropdown.param-diag').dropdown({
  onShow: function() {
    getParamDiagram();
  }
 });

 $('.ui.dropdown.param-chart').dropdown({
  onShow: function() {
    getParamChart();
  }
 });



function getParamDiagram(){
  let currentCombobox = $(".ui.dropdown.param-diag");
  currentCombobox.find(".menu").empty();
   //currentCombobox.dropdown('clear');
  $.ajax({
    method: "GET",
    url: "Analysis/GetParametersForDiagram",
  }).done(function(msg){
    if (msg.status != "negative") {
      console.log(msg.parametersForDiagram)
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
    if (msg.message.length > 0) {
      notification(msg.status,  msg.header, msg.message,'third/b')
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
    if (msg.status != "negative") {
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
    if (msg.message.length > 0) {
      notification(msg.status,  msg.header, msg.message,'third/c')
    }
  });
  
}

function createLinearChart(event){
  let params = {}
  params.namesSystems = []
  $("[data-tab='third/c']").find(".ui.label.transition.visible").each(function( index, element  ) {
    params.namesSystems.push($(element).text())
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
    if (msg.status != "negative")
      showChart4(msg)
    if (msg.message.length > 0) {
      notification(msg.status,  msg.header, msg.message,event.target)
    }
  });
}

function createDiagram(event){
  let params = {}
  params.namesSystems = []
  $("[data-tab='third/b']").find(".ui.label.transition.visible").each(function( index, element  ) {
    params.namesSystems.push($(element).text())
  });
  params.parameterName = $(".ui.param-diag").find(".item.active").attr("data-value");
  $.ajax({
    method: "GET",
    url: "Analysis/GetCalculationForDiagram",
    data: {queryString: JSON.stringify(params)}
  }).done(function(msg){
    if (msg.status != "negative")
      showChart3(msg)
    if (msg.message.length > 0) {
      notification(msg.status,  msg.header, msg.message,event.target)
    }
  });
}

async function AJAXSubmit (oFormElement) {
  let currentCombobox = $(".ui.dropdown.names");
  currentCombobox.find(".menu").empty();
  currentCombobox.dropdown('clear')
  const formData = new FormData(oFormElement);
  try {
  const response = await fetch(oFormElement.action, {
    method: 'POST',
    body: formData
  })
  .then(response => response.json())
  .then(msg => {
    if (msg.status != "negative") {

      $.ajax({
        method: "GET",
        url: "Restrictions/GetRestrictions",
      }).done(function(msg){
        if (msg.status != "negative") {
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
        if (msg.message.length > 0)
          notification(msg.status,  msg.header, msg.message,"first")
      }); 
    }
    if (msg.message.length > 0) {
      notification(msg.status,  msg.header, msg.message,"first")
    }
    $("#FormAJAX")[0].reset();
  });
  
  } catch (error) {
    notification("error", error,"first")
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
  let datasets = [];
  let xValues = [];
  let yValues = [];
  $.each(Result.calculations, function(index, element){
    // labels.push(element.nameSystem);
    // diagData.push(element.value);
    xValues = [];
    yValues = [];
    let color = `rgba(${Math.floor(Math.random() * Math.floor(255))}, ${Math.floor(Math.random() * Math.floor(255))}, ${Math.floor(Math.random() * Math.floor(255))}, 0.5)`

    $.each(element.values, function(index, val){
     xValues.push(val.x);
     yValues.push(val.y);
    });
    let dataset = {
      label: [element.nameSystem],
      data: yValues,
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
      labels: xValues,
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
    if (msg.status != "negative") {
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
        if (msg.status != "negative") {
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
    if (msg.message.length > 0) {
      notification(msg.status,  msg.header, msg.message,"second/a")
    }
    $("#FormAJAX1")[0].reset();
  });
  
  } catch (error) {
    notification("error", msg.message,"second")
    console.error('Error:', error);
  }
  }


function generateReport(event){

  if ($(event.target).parent().parent().find(".ui.input.save-system2").length == 0) {
    $.ajax({
      method: "GET",
      url: `Systems/ValidateSystemBeforeSave`,
    }).done(function(msg){
      if (msg.status != "negative") {
        element = $(event.target).is( ":button" ) ? $(event.target) : $(event.target).parent()
        element.before(`
          <div class="ui input save-system2">
            <input type="text" placeholder="Имя">
          </div>
        `);
      } 
      if (msg.message.length > 0) {
        notification(msg.status,  msg.header, msg.message,event.target)
      }
    });
  }
  else {
    filename = $(".ui.input.save-system2").find("input").val();
    if (filename.length > 0) {
      $(".ui.input.save-system2").removeClass("error")
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
      notification("error","Ошибка",["Введите имя файла"],event.target)
      $(".ui.input.save-system2").addClass("error")
    }
  }
}

function deleteSystem(event){
  let currentButton = $( event.target ).is( ":button" ) ? event.target :  event.target.closest("button");
  let curElement = $(currentButton).parent().parent();
  let textElement = curElement.find(".system-list-item").html().trim();
  // <div class="ui divided list system-list1">

  //   </div>

  $.ajax({
    method: "GET",
    url: `Analysis/DeleteSystem`,
    data: {nameSystem: textElement}
  }).done(function(msg){
    if (msg.status != "negative") {
        $(curElement).remove();
        if ($(".list.system-list1").children().length == 0)
          $(".system-segment").remove()
    }
    if (msg.message.length > 0)
      notification(msg.status,  msg.header, msg.message,event.target)
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
    if (msg.status != "negative") {
      $.ajax({
        method: "GET",
        url: "Analysis/GetSystems",
      }).done(function(msg){
        if (msg.status != "negative") {
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
        if (msg.message.length > 0) {
          notification(msg.status,  msg.header, msg.message,"third/a")
        }
      });  
    }
    if (msg.message.length > 0) {
      notification(msg.status,  msg.header, msg.message,"third/a")
    }
    $("#FormAJAX2")[0].reset();
  });
  
  } catch (error) {
    notification("error", msg.message,"third/a")
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
    let currentCombobox = $(".ui.dropdown.names");
    currentCombobox.find(".menu").empty();
    currentCombobox.dropdown('clear')
    const formData = new FormData(oFormElement);
    try {
    const response = await fetch(oFormElement.action, {
      method: 'POST',
      body: formData
    })
    .then(response => response.json())
    .then(msg => {
      if (msg.status != "negative") {
        $.ajax({
          method: "GET",
          url: "Restrictions/GetRestrictions",
        }).done(function(msg){
          if (msg.status != "negative") {
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
          if (msg.message.length > 0) {
            notification(msg.status,  msg.header, msg.message,"first")
          }
        }); 
        }
      if (msg.message.length > 0) {
        notification(msg.status,  msg.header, msg.message,"first")
      }
      
      $("#FormAJAX4")[0].reset();
    });
    
    } catch (error) {
      notification("error", msg.message,"first")
      console.error('Error:', error);
    }
    }


function secondATab(event) {
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
    $(".message").remove()
    if (msg.message.length > 0) {
      notification(msg.status,  msg.header, msg.message,event)
    }
  }); 
};

function secondBTab(event) {
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
    $.each( msg.parametersWithCalculate, function( key, value ) {
      $(".tab.segment[data-tab='second/b']").find('tbody').append(`<tr class="${value.correct == true ? "" : "error"}">
        <td data-label="description">${value.description}</td>
        <td data-label="name">${value.designation}</td>
        <td data-label="unit">${value.unit}</td>
        <td data-label="value">${value.value}</td>
        </tr>`
      )
    });
    $(".message").remove()
    if (msg.message.length > 0)
      notification(msg.status,  msg.header, msg.message,event)
  });  
};

function secondCTab(event) {
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
 
    if (msg.message.length > 0)
      notification(msg.status,  msg.header, msg.message,event)
  }); 
};


function exportChart(event) {

  let params = {}
  params.namesSystems = []
  $("[data-tab='third/c']").find(".ui.label.transition.visible").each(function( index, element  ) {
    params.namesSystems.push($(element).text())
  });
  params.from = $(".linear-chart-from").val();
  params.to = $(".linear-chart-to").val();
  params.countDote = $(".linear-chart-dots-count").val();
  params.parameterName = $(".ui.param-chart").find(".item.active").attr("data-value");
  if ($(event.target).parent().parent().find(".ui.input.save-chart1").length == 0) {
    $.ajax({
      method: "GET",
      url: "Analysis/ValidateChartBeforeSave",
      data: {queryString: JSON.stringify(params)}
    }).done(function(msg){
      if (msg.status != "negative") {
        element = $(event.target).is( ":button" ) ? $(event.target).parent() : $(event.target).parent().parent
        element.before(`
        <div class="field">
        <label>&ensp;<br></label>
          <div class="ui input save-chart1 ">
            <input type="text" placeholder="Имя">
          </div>
          </div>
        `);
      } 
      if (msg.message.length > 0) {
        notification(msg.status,  msg.header, msg.message,event.target)
      }
    });
  }
  else {
    filename = $(".ui.input.save-chart1").find("input").val();
    if (filename.length > 0) {
      $(".ui.input.save-chart1").removeClass("error")
      var url_base64jp = $(`#chart`)[0].toDataURL({format: 'jpg', quality: 1})
      $.ajax({
        method: "POST",
        url: `Analysis/SaveChartToFile`,
        data: {chart: url_base64jp.replace(/^data:image\/(png|jpg);base64,/, ""),
        filename: filename}
      }).done(function(msg){
        var blob = new Blob([msg.fileData]);
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.style.display = 'none';
        a.href = url //url_base64jp;
        a.download = `График.csv`;
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(a.href);
      });
    }
    else {
      notification("error","Ошибка",["Введите имя файла"],event.target)
      $(".ui.input.save-system").addClass("error")
    }
  }
}


function exportDiag(event) {
  let params = {}
  params.namesSystems = []
  $("[data-tab='third/b']").find(".ui.label.transition.visible").each(function( index, element  ) {
    params.namesSystems.push($(element).text())
  });
  params.parameterName = $(".ui.param-diag").find(".item.active").attr("data-value");
  if ($(event.target).parent().parent().find(".ui.input.save-diagram1").length == 0) {
    $.ajax({
      method: "GET",
      url: "Analysis/ValidateDiagramBeforeSave",
      data: {queryString: JSON.stringify(params)}
    }).done(function(msg){
      if (msg.status != "negative") {
        element = $(event.target).is( ":button" ) ? $(event.target).parent() : $(event.target).parent().parent
        console.log(element)
        element.before(`
        <div class="field">
        <label>&ensp;<br></label>
          <div class="ui input save-diagram1 ">
            <input type="text" placeholder="Имя">
          </div>
          </div>
        `);
      } 
      if (msg.message.length > 0) {
        notification(msg.status,  msg.header, msg.message,event.target)
      }
    });
  }
  else {
    filename = $(".ui.input.save-diagram1").find("input").val();
    if (filename.length > 0) {
      $(".ui.input.save-diagram1").removeClass("error")
      var url_base64jp = $(`#diagram`)[0].toDataURL({format: 'jpg', quality: 1})
      $.ajax({
        method: "POST",
        url: `Analysis/SaveDiagramToFile`,
        data: {chart: url_base64jp.replace(/^data:image\/(png|jpg);base64,/, ""),
        filename: filename
      }
      }).done(function(msg){
        var blob = new Blob([msg.fileData]);
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.style.display = 'none';
        a.href = url //url_base64jp;
        a.download = `Диаграмма.csv`;
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(a.href);
      });
    }
    else {
      notification("error","Ошибка",["Введите имя файла"],event.target)
      $(".ui.input.save-system").addClass("error")
    }
  }
}