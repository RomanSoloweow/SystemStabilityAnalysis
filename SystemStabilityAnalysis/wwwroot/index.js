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
$(".ui.button.save-restrictions").click({url: "SaveRestrictionsToFile", param: "parametersWithEnter"},saveFile);
$(".ui.button.upload-csv").click(uploadCsv);
$(".ui.button.validate").click(validateSystem);
$(".ui.button.create-linear-chart").click(linearChart);
$('.ui.dropdown.names').change(function(){
  setTimeout(()=>{
    currentElement = $(".ui.dropdown.names").find(".item.active");
    $('.disField').find(".name").val(currentElement.attr("data-name"));
    $('.disField').find(".unit").val(currentElement.attr("data-unit"));
  }, 1);
});


function nextPage(event){
  currentTab = $(event.target).closest('.segment').parent().find(".item.active");
  currentTab = currentTab.removeClass('active');
  nextTab = currentTab.next()
  currentTab.next().tab('change tab', nextTab.attr('data-tab'))
}

function nextTab(event){
  $(".item[data-tab='second']").removeClass('active');
  $(".item[data-tab='second']").next().tab('change tab', 'third')
}

function addFilter(){
  let parameter = $(".ui.names").find(".item.active").attr("data-value");
  let condition = $(".ui.conditions ").find(".item.active").attr("data-value");
  let value = $(".input.value ").val();
  // if (parameter == undefined || condition == undefined || value == undefined)
  // return
  $.ajax({
    method: "GET",
    //добавить тернарный
    url: `Restrictions/AddRestriction?parameter=${parameter == undefined ? "" : parameter}&condition=${condition == undefined ? "" : condition}&value=${value == undefined ? "" : value}`,
  }).done(function(msg){
    if (msg.status == "Success") {
      if ($(".ui.celled.table.restructions").length == 0 ) {
        $('.ui.form').append(`<table class="ui celled blue table center aligned restructions">
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
    element = $(event.target).is( ":button" ) ? $(event.target) : $(event.target).parent()
    element.before(`
      <div class="ui input save-system">
        <input type="text" placeholder="Имя">
      </div>
    `);

  }
  else {
    filename = $(".ui.input.save-system").find("input").val();
    if (filename.length > 0) {
        const url = `Restrictions/${event.url}?filename=${filename}`;
        const a = document.createElement('a');
        a.style.display = 'none';
        a.href = url;
        // the filename you want
        a.download = 'todo-1.json';
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(url);
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
          <td data-label="description" data-value=${value.value}>${value.description}</td>
          <td data-label="name">${value.name}</td>
          <td data-label="unit">${value.unit}</td>
          <td data-label="button" class="center aligned" >
          <div class="ui input">
            <input type="number" placeholder="" class="system-validate">
          </div>
          </td>
          </tr>`
        )
      });
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
          <td data-label="name">${value.name}</td>
          <td data-label="unit">${value.unit}</td>
          <td data-label="value">${value.value}</td>
          </tr>`
        )
      });
    }
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
      if ($(".tab.segment[data-tab='second/c']").find(".header").length == 0)
        $(".tab.segment[data-tab='second/c']").find("table").before(`<div class='ui large message'>
          <div class="header">
            U = ${msg.u}
          </div>
          ${msg.result}
          </div>`
        )
      $.each( msg.parametersForAnalysis, function( key, value ) {
        $(".tab.segment[data-tab='second/c']").find('tbody').append(`<tr class="${value.correct == true ? "" : "error"}">
          <td data-label="description">${value.description}</td>
          <td data-label="name">${value.name}</td>
          <td data-label="unit">${value.unit}</td>
          <td data-label="value">${value.value}</td>
          </tr>`
        )
      });
    }
  });  
}});


function validateSystem() {
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
    $.each(msg.something, function(index, value){
      
    })
  });
}

function linearChart() {
  
}


async function AJAXSubmit (oFormElement) {
  var resultElement = oFormElement.elements.namedItem("result");
  const formData = new FormData(oFormElement);
  try {

  const response = await fetch(oFormElement.action, {
    method: 'POST',
    body: formData
  })
  .then(response => response.text())
  .then(msg => {
    $(".ui.celled.table.restructions").remove()
    $('.ui.form').append(`<table class="ui celled blue table center aligned restructions">
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
    $.each( JSON.parse(msg).restrictions, function( key, value ) {

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
  });
  
  } catch (error) {
    console.error('Error:', error);
  }
  }
