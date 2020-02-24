$('.menu .item')
 .tab()
;

$('.ui.dropdown')
 .dropdown()
;

$('.ui.form')
  .form({
    fields: {
      value: {
        identifier  : 'value',
        rules: [
          {
            type   : 'empty'
          }
        ]
      }
    }
  })
;

$('.message .close').on('click', function() {
  $(this).closest('.message').transition('fade');
});

$(".ui.icon.button.plus").click(function(eventData, handler) {
  $( ".ui.form" ).submit();
  if ($(".ui.form.success").length === 1)
    addFilter();
});
$(".ui.icon.button.minus").click(deleteFilter);
$(".ui.button.next").click(nextPage);
$(".ui.button.delete-all").click(deleteAll);
$(".ui.dropdown.names").click(getNames);
$(".ui.dropdown.conditions").click(getConditions);
$(".ui.button.save-system").click(saveSystem);
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
  currentTab = currentTab.next().tab('change tab', nextTab.attr('data-tab'))
}

function addFilter(){
  //let currentCombobox = $(".ui.dropdown.names");
  let message = "Ограничение не добавлено.";
  let parameter = $(".ui.names").find(".item.active").attr("data-value");
  let condition = $(".ui.conditions ").find(".item.active").attr("data-value");
  let value = $(".input.value ").val();
  $.ajax({
    method: "GET",
    url: `Restrictions/AddRestriction?parameter=${parameter}&condition=${condition}&value=${value}`,
  }).done(function(msg){
    if (msg.status == "Success") {
      if ($(".ui.celled.table").length == 0 ) {
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
      console.log(msg)
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
    }
    else
      notification(msg.status,msg.message,"first");
    clearFilters();
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

function saveSystem(event){
  console.log(8)
  if ($(event.target).parent().find(".ui.input.save-system").length == 0) {
    console.log($(event.target).parent())
    $(event.target).before(`
      <div class="ui input save-system">
        <input type="text" placeholder="Имя">
      </div>
    `);
    $(".ui.button.save-system").text("Сохранить");
  }
  else {
    //сохранить
  }
}

$(".item[data-tab='second'").tab({'onVisible':function(){
  if ($(".tab.segment[data-tab='second/a']").find('table').length == 0) {
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
  }
  $.ajax({
    method: "GET",
    url: "Systems/GetParameters",
  }).done(function(msg){
    if (msg.status == "Success") {
      $.each( msg.properties, function( key, value ) {
        $(".tab.segment[data-tab='second/a']").find('tbody').after(`<tr>
          <td data-label="description" data-value=${value.value}>${value.description}</td>
          <td data-label="name">${value.name}</td>
          <td data-label="unit">${value.unit}</td>
          <td data-label="button" class="center aligned" >
          <div class="ui input">
            <input type="number" placeholder="">
          </div
          </td>
          </tr>`
        )
      });
    }
  });
  
  
}});