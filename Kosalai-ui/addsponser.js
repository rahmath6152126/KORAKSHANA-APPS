var requestURL = `${API_URL}/Sponser`;

var SponserModel = class model {
  constructor() {
    this.id = localStorage.getItem("id");
    localStorage.removeItem("id");
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
  reminder = []

  init() {
    var self = this;
    if (this.id !== undefined && this.id != '' && this.id != null) {
      this.ajaxGet(`${requestURL}/GetbyId/${self.id}`, res => {
        //Personal info
        $('#txtfirstName').val(res.firstName);
        $('#txtlastName').val(res.lastName);
        $('#txtContactno').val(res.pri_contact_no);
        $('#txtSecondaryContactno').val(res.contact_no);
        $('#sponserType').val(res.sponser_Type).change();
        $('#txtsponserName').val(res.toSponsername);
        $('#txtsponserContact').val(res.toSponserContactno);
        $('#regDate').val(moment(new Date(res.regDate)).format('DD MMM YYYY'));
        //Event details
        $('#eventType').val(res.evt_type).change();
        $('#eventDate').val(moment(new Date(res.event_date)).format('DD MMM YYYY'));
        $('#tamil_yr').val(res.tamil_yr).change();
        $('#tamil_month').val(res.tamil_month).change();
        $('#startname').val(res.starname).change();
        $('#patcamname').val(res.patcamname).change();
        $('#tithiname').val(res.tithiname).change();
        $('#gotraname').val(res.gotraname);
        //Payment details
        $('#paymentMode').val(res.paymentMode).change();
        $('#txttransactionno').val(res.transactionno);
        $('#txttransactionname').val(res.transactionname);
        $('#txtamount').val(res.amount);
        //Reference details
        $('#txtreftype').val(res.refType).change();
        $('#txtrefname').val(res.refname);
        $('#txtemail').val(res.email);
        $('#txtaddress').val(res.address);
        $('#img-profile').attr('src', res.photo_path);
        vmSponserModel.reminder = res.reminder.map(x => { x.tamilDate = new Date(x.tamilDate); return x; });
        vmSponserModel.bindTbodyTemplate();
        $('#txtRegNumber').val(res.code);
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
    debugger;
    var tamilDate = $('#tamil_date').val();

    if (tamilDate === undefined || tamilDate == "") {

      return false;
    }

    var tdate = moment(new Date(tamilDate));
    var data = { 'engDate': tdate.format('YYYY'), 'tamilDate': tdate.format('DD MMM YYYY') };
    this.reminder.push(data);
    this.bindTbodyTemplate();
    $('#eng_date').val('');
    $('#tamil_date').val('');
  }

  row_click = (e) => {
    vmSponserModel.reminder = vmSponserModel.reminder.filter(x => x.Slno != $(e).attr('data-value'));
    vmSponserModel.bindTbodyTemplate();
  }
  bindTbodyTemplate() {
    var self = this;
    $('#reminder tbody').html('');
    var i = 1;
    this.reminder.forEach(element => {
      element.Slno = i++;
      var html = `<tr><td>${element.Slno}</td><td>${element.engDate}</td><td>${element.tamilDate}</td><td></a><a onclick="vmSponserModel.row_click(this)"  data-value="${element.Slno}" class="remove"><i class="fa fa-trash"></i>Remove</a></td></tr>`
      $('#reminder tbody').append(html);
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
function DateChange() {
  if ($('#eventDate').val().length > 0) {
    var result = moment(new Date($('#eventDate').val())).format('DD/MM/YYYY')
    $('#display_eventdate').val(result);
  }
}
function clearSearch() {
  $('form').find('input,select').each((e, a) => { $(a).val('').change() });
  // $('form').find('select').each((e, a) => { $(a).val('').change() });
  $('#img-profile').attr('src', 'src/images/default-image.jpg');
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
    "code": $('#txtRegNumber').val(),
    "firstName": $('#txtfirstName').val(),
    "lastName": $('#txtlastName').val(),
    "pri_contact_no": $('#txtContactno').val(),
    "contact_no": $('#txtSecondaryContactno').val(),
    "sponser_Type": $('#sponserType').val(),
    "toSponsername": $('#txtsponserName').val(),
    "toSponserContactno": $('#txtsponserContact').val(),
    "photo_path": "string",
    "evt_type": $('#eventType').val(),
    "event_date": new Date($('#eventDate').val()),
    "tamil_yr": $('#tamil_yr').val(),
    "tamil_month": $('#tamil_month').val(),
    "starname": $('#startname').val(),
    "patcamname": $('#patcamname').val(),
    "tithiname": $('#tithiname').val(),
    "gotraname": $('#gotraname').val(),
    "regDate": new Date($('#regDate').val()),
    "photo_path": $('#img-profile').attr('src'),
    "relation": $('#txtrelation').val(),
    "paymentMode": $('#paymentMode').val(),
    "transactionno": $('#txttransactionno').val(),
    "transactionname": $('#txttransactionname').val(),
    "amount": $('#txtamount').val(),
    "address": $('#txtaddress').val(),
    "email": $('#txtemail').val(),
    "refname": $('#txtrefname').val(),
    "reftype": $('#txtreftype').val(),
    "reminder": vmSponserModel.reminder.map(x => {
      x.tamilDate = new Date(x.tamilDate);
      return x;
    })
  };
  vmSponserModel.ajaxPost(`${requestURL}/Add`, params, (res, status) => {
    if (res === true) {
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
  if ($('#search_reminder').val() !== undefined) {
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
}
$(document).ready(function () {

  var isMasterloaded = false;
  $("#imgInp").change(function () {
    debugger;
    var fd = new FormData();


    // Check file selected or not
    if (this.files.length > 0) {
      fd.append('model', this.files[0]);

      $.ajax({
        url: `${requestURL}/AddFile`,
        data: fd,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (response) {
          if (response != 0) {
            $('#img-profile').attr("src", response);
          } else {
            alert('file not uploaded');
          }
        },
      });
    }
  });

  $('#img-profile').attr('src', 'src/images/default-image.jpg');

  $('#img-profile').attr('src', 'src/images/default-image.jpg');

  vmSponserModel.ajaxGet(`${requestURL}/AllMaster`, (result) => {

    var res = result.list;
    $('#txtRegNumber').val(result.code);
    if (res !== undefined) {
      isMasterloaded = true;
      var List = [];
      // Sponser type loaded.
      if (res.sponserType !== undefined) {

        List = res.sponserType.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#search_sponserType').append(List);
        $('#sponserType').append(List);

      }

      // Event type loaded.
      if (res.eventType !== undefined) {

        List = res.eventType.map(x => {
          return `<option value="${x}">${x}</option>`;
        });
        $('#search_eventType').append(List);
        $('#eventType').append(List);

      }
      // Tamil list
      if (res.tamilYr !== undefined) {

        List = res.tamilYr.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#tamil_yr').append(List);

      }
      // Tamil month
      if (res.tamilMon !== undefined) {

        List = res.tamilMon.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#tamil_month').append(List);

      }
      //Star list 
      if (res.star !== undefined) {

        List = res.star.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#startname').append(List);

      }
      if (res.thithi !== undefined) {

        List = res.thithi.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#tithi').append(List);

      }

      //Star list 
      if (res.Patcam !== undefined) {

        List = res.Patcam.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#patcamname').append(List);

      }

      //Star list 
      if (res.paymentMode !== undefined) {

        List = res.paymentMode.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#paymentMode').append(List);

      }

      //Star list 
      if (res.referenceType !== undefined) {

        List = res.referenceType.map(x => {
          return `<option value="${x}">${x}</option>`;
        });

        $('#txtreftype').append(List);

      }
    }

  });



  $('#btnAdd').on('click', (e) => {
    console.log(e);

    vmSponserModel.pushReminder();
  });

  $('#sponserType').on('change', (e) => {

    $('#txtrelation').attr('disabled', $(e.currentTarget).val() === 'myself');
    $('#txtsponserName').attr('disabled', $(e.currentTarget).val() === 'myself');
    $('#txtsponserContact').attr('disabled', $(e.currentTarget).val() === 'myself');
  });


  $('#eventType').on('change', (e) => {
    DateChange();
    $('#patcamname').attr('disabled', $(e.currentTarget).val() === 'Birthday');
    $('#startname').attr('disabled', $(e.currentTarget).val() === 'Remembrance');
    $('#tithi').attr('disabled', $(e.currentTarget).val() === 'Birthday');
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
