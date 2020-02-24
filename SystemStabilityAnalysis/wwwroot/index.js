$('.menu .item')
 .tab()
;

$('.ui.dropdown')
 .dropdown()
;

$('.message .close').on('click', function() {
  $(this).closest('.message').transition('fade');
});

$(".ui.icon.button.plus").click(addFilter)
$(".ui.icon.button.minus").click(deleteFilter)
$(".ui.button.next").click(nextPage)
$(".ui.button.delete-all").click(()=>{$(".ui.celled.table").remove()})
$(".ui.dropdown.names").click(getNames);
$(".ui.dropdown.conditions").click(getConditions);
$('.ui.dropdown.names').change(function(){
  setTimeout(()=>{
    currentElement = $(".ui.dropdown.names").find(".item.active");
    $('.disField').find(".name").val(currentElement.attr("data-name"));
    $('.disField').find(".unit").val(currentElement.attr("data-unit"));
  }, 1);
});


function nextPage(){
  currentTab = $('.active.item');
  currentTab = currentTab.removeClass('active');
  nextTab = currentTab.next()
  currentTab = currentTab.next().tab('change tab', nextTab.attr('data-tab'))
}

function addFilter(){
  let notificationMessage = ""
  $.ajax({
    method: "GET",
    url: "Restrictions/AddRestirction",
  }).done(function(msg){
    if (msg.Status == "Success") {
      if ($(".ui.celled.table").length > 0) {
        $(".ui.celled.table tr:last").after(`<tr>
        <td data-label="Name">Elyse</td>
        <td data-label="Age">24</td>
        <td data-label="Job">Designer</td>
        <td data-label="Age">24</td>
        <td data-label="Job">Engineer</td>
        <td data-label="Job" class="center aligned" >
          <button class="ui icon button minus">
            <i class="minus icon"></i>
          </button>
        </td>
        </tr>`)
        
      }
      else {
        $('.ui.form').append(`<table class="ui celled blue table center aligned">
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
          <tr>
            <td data-label="Name">James</td>
            <td data-label="Age">24</td>
            <td data-label="Job">Engineer</td>
            <td data-label="Age">24</td>
            <td data-label="Job">Engineer</td>
            <td data-label="Job" class="center aligned" >
              <button class="ui icon button minus">
                <i class="minus icon"></i>
              </button>
            </td>
          </tr>
        </tbody>
        </table>`)
      }
      $(".ui.icon.button.minus").unbind();
      $(".ui.icon.button.minus").click(deleteFilter);
    }
    notification(msg.Status,msg.Message,"first");
  });
}

function deleteFilter(){
  $( this ).parent().parent().remove();
  if ($(".ui.celled.table tr").length == 1) {
    $(".ui.celled.table").remove();
  }
}

function getNames(){
  let currentCombobox = $(".ui.dropdown.names");
  if (currentCombobox.find(".menu").children().length == 1 ) {
    $.ajax({
      method: "GET",
      url: "Restrictions/GetParameters",
    }).done(function(msg){
      
      if (msg.Status == "Success") {
        currentCombobox.find(".menu").empty();
        currentCombobox.dropdown('refresh')
        $.each( msg.Properties, function( key, value ) {
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
  console.log(currentCombobox)
  if (currentCombobox.find(".menu").children().length == 1 ) {
    $.ajax({
      method: "GET",
      url: "Restrictions/GetConditions",
    }).done(function(msg){
      if (msg.Status == "Success") {
        currentCombobox.find(".menu").empty();
        currentCombobox.dropdown('refresh')
        $.each( msg.Conditions, function( key, value ) {
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
    console.log(3)
    setTimeout(() => {
      $(".message .close").trigger("click");
    }, 5000);
  }
  
}