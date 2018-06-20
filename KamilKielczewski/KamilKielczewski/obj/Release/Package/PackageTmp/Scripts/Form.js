function ValidateForm() {
    ValidateFirstName();
    ValidateLastName();
    ValidatePassword();

    var email = document.getElementById('Email');

    if (ValidateEmail(email.value)) {
        email.style.border = '1px solid #ced4da';
    } else {
        email.style.border = '1px solid red';
    }
}

function ValidateFirstName() {
    var name = document.getElementById('firstName');
    if (name.value != "") {
        name.style.border = '1px solid #ced4da';
        return;
    }
    name.style.border = '1px solid red';
}

function ValidateLastName() {
    var name = document.getElementById('lastName');
    if (name.value != "") {
        name.style.border = '1px solid #ced4da';
        return;
    }
    name.style.border = '1px solid red';
}

function ValidateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function ValidatePassword() {
    var password = document.getElementById('Password');
    var confirmPassword = document.getElementById('confirmPassword');

    if (password.value != confirmPassword.value || password.value == "" || confirmPassword == "") {
        password.style.border = '1px solid red';
        confirmPassword.style.border = '1px solid red';
    } else {
        if (password.value.match(/^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/)) {
            password.style.border = '1px solid #ced4da';
            confirmPassword.style.border = '1px solid #ced4da';
        }
    }
}

function ClosePopup() {
    popup = document.getElementById('popupx');
    popup.parentNode.removeChild(popup);
}