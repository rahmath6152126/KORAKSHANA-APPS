var requestURL = '/api/Sponser';

var SponserModel = class model {
  constructor() {
    this.id = localStorage.getItem("id");
  }
  id = ''
  code = ''
  firstName = ''
  lastName = ''
  pri_contact_no = ''
  contact_no = ''
  sponser_Type = 0
  toSponsername = ''
  toSponserContactno = ''
  photo_path = ''
  event_data = {}
  reminder = []

  init() {
    var self = this;
    if (this.id !== undefined) {
      this.ajaxGet(`${requestURL}/GetbyId/${self.id}`, res => {
        $('#txtfirstName').val(res.firstName);
        $('#txtlastName').val(res.lastName);
        $('#txtContactno').val(res.pri_contact_no);
        $('#txtSecondaryContactno').val(res.contact_no);
        $('#sponserType').val(res.sponser_Type).change();
        $('#txtsponserName').val(res.toSponsername);
        $('#txtsponserContact').val(res.toSponserContactno);
        // "event_data": {
        $('#eventType').val(res.event_data.evt_type).change();
        $('#eventDate').val(moment(new Date(res.event_data.event_date)).format('DD MMM YYYY'));
        $('#tamil_yr').val(res.event_data.tamil_yr).change();
        $('#tamil_month').val(res.event_data.tamil_month).change();
        $('#startname').val(res.event_data.starname).change();
        $('#patcamname').val(res.event_data.patcamname).change();
        $('#tithiname').val(res.event_data.tithiname);
        $('#gotraname').val(res.event_data.gotraname);
        vmSponserModel.reminder = res.reminder.map(x => { x.eng_date = new Date(x.eng_date); return x; });
        vmSponserModel.bindTbodyTemplate();
        $('#pageid').html(`Edit Sponser ( ${res.code} )`);
      });
    }
    else { $('#pageid').html(`Add Sponser`); }
  }
  set(name, value) {
    this[name] = value
    $(`#` + name).val(value)
  }
  get(name) {
    return this[name]
  }
  pushReminder() {
    var englishDate = $('#eng_date').val();
    var tamilDate = $('#tamil_date').val();
    if (englishDate === undefined && englishDate == "") {

      return false;
    }
    else if (tamilDate === undefined && tamilDate == "")
      return false;
    var data = { 'eng_date': new Date(englishDate), 'tamil_date': tamilDate };
    this.reminder.push(data);
    this.bindTbodyTemplate();
    $('#eng_date').val('');
    $('#tamil_date').val('');
  }
  bindTbodyTemplate() {
    var self = this;
    $('#reminder tbody').html('');
    var i = 1;
    this.reminder.forEach(element => {
      element.Slno = i++;
      var html = `<tr data-value="${element.Slno}"><td>${element.Slno}</td><td>${moment(element.eng_date).format('DD MMM YYYY')}</td><td>${element.tamil_date}</td><td></a><a  class="remove"><i class="fa fa-trash"></i>Remove</a></td></tr>`
      $('#reminder tbody').append(html);
    });
    $('#reminder tbody tr').on('click', (e) => {
      var index = vmSponserModel.reminder.find(x => x.Slno == $(e.currentTarget).attr('data-value'));
      if (index !== undefined) {
        vmSponserModel.remove(index);
        vmSponserModel.bindTbodyTemplate();
      }
    });
  }

  ajaxGet(URL, callback) {
    $.get(`${URL}`, function (data, status) {
      callback(data);
    });

  }

  ajaxPost(URL, data, callback) {
    $.ajax({
      url: URL,
      type: "POST",
      data: JSON.stringify(data),
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (res) {
        callback(res);
      },
      error: (res) => {
        callback(res);
      }
    });
  }
  ajaxPut(URL, data, callback) {
    $.ajax(URL, {
      type: 'POST',  // http method
      data: filter(),
      "Content-Type": 'application/json',  // data to submit
      success: function (data, status, xhr) {
        callback(data);
      },
      error: function (jqXhr, textStatus, errorMessage) {
        $('p').append('Error' + errorMessage);
      }
    });
  }
  filter() {
    return {
      "name": $('#search_firstName').val(),
      "contactno": null,
      "sponserType": $('#search_sponserType').val(),
      "toSponser": null,
      "evetDate": new Date(),
      "event_Type": $('#search_eventType').val()
    }
  }
}

function clearSearch() {
  $('#search_firstName').val('');
  $('#search_sponserType').val('').change();
  $('#search_eventType').val('').change();
  datableBind();

}

var vmSponserModel = new SponserModel();


var columns = [
  { data: 'slNo' },
  { data: 'name' },
  { data: 'contactno' },
  { data: 'sponserType' },
  { data: 'toSponser' },
  { data: 'event_Type' },
  { data: 'evetDate' },
  { data: 'action' }
];


var Submit = (e) => {
  var params = {
    "id": vmSponserModel.id,
    "firstName": $('#txtfirstName').val(),
    "lastName": $('#txtlastName').val(),
    "pri_contact_no": $('#txtContactno').val(),
    "contact_no": $('#txtSecondaryContactno').val(),
    "sponser_Type": $('#sponserType').val(),
    "toSponsername": $('#txtsponserName').val(),
    "toSponserContactno": $('#txtsponserContact').val(),
    "photo_path": "string",
    "event_data": {
      "evt_type": $('#eventType').val(),
      "event_date": new Date(),//$('#eventDate').val()
      "tamil_yr": $('#tamil_yr').val(),
      "tamil_month": $('#tamil_month').val(),
      "starname": $('#startname').val(),
      "patcamname": $('#patcamname').val(),
      "tithiname": $('#tithiname').val(),
      "gotraname": $('#gotraname').val()
    },
    "reminder": vmSponserModel.reminder
  };
  vmSponserModel.ajaxPost(`${requestURL}/Add`, params, (res, status) => {

    if (res == 'true') {
      alert("Successfully saved");
      localStorage.clear();
      location.reload();
    }
    else {
      alert("Error on page please check admin");
    }

  });

}

function datableBind() {
  $.ajax({
    url: `${requestURL}/Search/${$('#search_reminder').val()}`,
    type: "POST",
    dataType: "json",
    contentType: 'application/json',
    data: JSON.stringify(vmSponserModel.filter()),
    success: (res) => {
      if ($.fn.DataTable.isDataTable('#example')) {
        $('#example').DataTable().destroy();
      }
      var i = 0;
      var List = res.map(x => { x.evetDate = moment(new Date(x.evetDate)).format('DD MMM YYYY'); x.slNo = ++i; x.action = `<a class="datatable-edit p-2" data-value="${x.id}"><i class="fa fa-edit text-primary"></i></a><a data-value="${x.id}" class="datatable-remove p-2"><i class="fa text-danger fa-trash"></i></a>`; return x; })
      $('#example tbody').empty();

      $('#example').DataTable({
        data: List,
        scrollCollapse: true,
        autoWidth: false,
        responsive: true,
        columnDefs: [{
          targets: "datatable-nosort",
          orderable: false,
        }],
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "language": {
          "info": "_START_-_END_ of _TOTAL_ entries",
          searchPlaceholder: "Search",
          paginate: {
            next: '<i class="ion-chevron-right"></i>',
            previous: '<i class="ion-chevron-left"></i>'
          }
        },
        columns: columns,
        dom: 'Bfrtp',
        buttons: [
          'copy', 'csv', 'pdf', 'print'
        ]
      });

      $('#example tbody td a.datatable-edit').on('click', (e) => {
        debugger;
        localStorage.setItem("id", $(e.currentTarget).attr('data-value'));
        window.location.href = 'addsponser.html';

      });
      $('#example tbody td a.datatable-remove').on('click', (e) => {
        $.ajaxGet(`${requestURL}/Delete?id=${$(e.currentTarget).attr('data-value')}`, (res) => {
          location.reload();
        });
      });
    }
  });
}
$(document).ready(function () {

  var isMasterloaded = false;

  vmSponserModel.ajaxGet(`${requestURL}/GetMaster`, (res) => {

    debugger;
    if (res !== undefined) {
      isMasterloaded = true;
      var List = [];
      // Sponser type loaded.
      if (res.sponserList !== undefined) {

        List = res.sponserList.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#search_sponserType').append(List);
        $('#sponserType').append(List);

      }

      // Event type loaded.
      if (res.eventList !== undefined) {

        List = res.eventList.map(x => {
          return `<option value="${x}">${x}</option>`;
        });
        $('#search_eventType').append(List);
        $('#eventType').append(List);

      }
      // Tamil list
      if (res.tamilYearList !== undefined) {

        List = res.tamilYearList.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#tamil_yr').append(List);

      }
      // Tamil month
      if (res.tamilMonthList !== undefined) {

        List = res.tamilMonthList.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#tamil_month').append(List);

      }
      //Star list 
      if (res.starList !== undefined) {

        List = res.starList.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#startname').append(List);

      }

      //Star list 
      if (res.patcamList !== undefined) {

        List = res.patcamList.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#patcamname').append(List);

      }
    }

  });

  $('#btnAdd').on('click', (e) => {
    console.log(e);

    vmSponserModel.pushReminder();
  });

  $('#sponserType').on('change', (e) => {
    $('#txtsponserName').attr('disabled', $(e.currentTarget).val() === 'myself');
    $('#txtsponserContact').attr('disabled', $(e.currentTarget).val() === 'myself');
  });


  $('#eventType').on('change', (e) => {
    $('#patcamname').attr('disabled', $(e.currentTarget).val() === 'Birthday');
    $('#startname').attr('disabled', $(e.currentTarget).val() !== 'Birthday');
    $('#tithiname').attr('disabled', $(e.currentTarget).val() === 'Birthday');
  });


  setTimeout(repeatAction, 1000);
  var repeatAction = () => {
    if (isMasterloaded == true) {

    }
    else {
      setTimeout(repeatAction, 1000);
    }

  }

  datableBind();

  vmSponserModel.init();

});
