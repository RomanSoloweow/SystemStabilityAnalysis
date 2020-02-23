$('.menu .item')
 .tab()
;
$('.menu .item')
 .tab()
;

$('.ui.dropdown')
 .dropdown()
;

$(".ui.icon.button.plus").click(addFilter)
$(".ui.icon.button.minus").click(deleteFilter)
$(".ui.button.next").click(nextPage)

function nextPage(){
  currentTab = $('.active.item');
  currentTab = currentTab.removeClass('active');
  nextTab = currentTab.next()
  currentTab = currentTab.next().tab('change tab', nextTab.attr('data-tab'))
}

function addFilter(){
  if ($(".ui.celled.table").length > 0) {
    $(".ui.celled.table tr:last").after(`<tr>
    <td data-label="Name">Elyse</td>
    <td data-label="Age">24</td>
    <td data-label="Job">Designer</td>
    <td data-label="Age">24</td>
    <td data-label="Job">Engineer</td>
    <td data-label="Job" class="center aligned" >
      <button class="ui icon button filter">
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
          <button class="ui icon button filter">
            <i class="minus icon"></i>
          </button>
        </td>
      </tr>
      <tr>
        <td data-label="Name">Jill</td>
        <td data-label="Age">26</td>
        <td data-label="Job">Engineer</td>
        <td data-label="Age">24</td>
        <td data-label="Job">Engineer</td>
        <td data-label="Job" class="center aligned" >
          <button class="ui icon button filter">
            <i class="minus icon"></i>
          </button>
        </td>
      </tr>
      <tr>
        <td data-label="Name">Elyse</td>
        <td data-label="Age">24</td>
        <td data-label="Job">Designer</td>
        <td data-label="Age">24</td>
        <td data-label="Job">Engineer</td>
        <td data-label="Job" class="center aligned" >
          <button class="ui icon button filter">
            <i class="minus icon"></i>
          </button>
        </td>
      </tr>
    </tbody>
    </table>`)

  }
  
  
}

// .click(function(){
//   //$('.ui.form').addClass("loading");
//   

// });