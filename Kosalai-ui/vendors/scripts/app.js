// var API_URL = "/api";
var API_URL = "http://localhost:5003";

var MasterModel = class {
    SponserList = [];

}
const msgType = [
    "alert-warning",
    "alert-success",
    "alert-danger"
];
Object.freeze(msgType);
var currentSelected;
var showAlert = (title, message, messageType) => {
    $('.main-container .alert').show();
    $('#toast-title').val(title);
    $('#toast-title').val(message);
    $('.main-container .alert').addClass(msgType[messageType]);
    currentSelected = messageType;
}

$(document).ready(() => {
    $('.main-container .alert .close').on('click', (e) => {
        
        $('#toast-title').val('');
        $('#toast-title').val('');
        currentSelected = undefined;
    })
})