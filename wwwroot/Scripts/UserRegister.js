
function RegisterData(data) {
    var self = this;
    if (data != undefined) {
        self.Id = ko.observable(data.Id);
        self.UserName = ko.observable(data.UserName);
        self.Email = ko.observable(data.Email);
        self.Password = ko.observable(data.Password);
        self.PhoneNo = ko.observable(data.PhoneNo);
        self.Gender = ko.observable(data.Gender);
        self.Age = ko.observable(data.Age);
       

    }
}


var UserRegisterViewModel = function () {
    var self = this;
    self.UserName = ko.observable('');
    self.Email = ko.observable('');
    self.Password = ko.observable('');
    self.ConfirmPassword = ko.observable('');
    self.PhoneNo = ko.observable('');
    self.Gender = ko.observable('');
    self.Age = ko.observable('');


    self.Validation = function () {
        var errMsg = "";
        if (ko.toJS(self.UserName()) == undefined || self.UserName() == '') {
            errMsg += "Enter Username !!!<br/>";
        }
        if (ko.toJS(self.Email()) == undefined || self.Email() == '') {
            errMsg += "Enter Email !!!<br/>";
        }
        if (ko.toJS(self.PhoneNo()) == undefined || self.PhoneNo() == '') {
            errMsg += "Enter Phone Number !!!<br/>";
        }

        if (ko.toJS(self.Gender()) == undefined || self.Gender() == '') {
            errMsg += "Choose Gender !!!<br/>";
        }
        if (ko.toJS(self.Age()) == undefined || self.Age() == '') {
            errMsg += "Choose Age !!!<br/>";
        }

        if (ko.toJS(self.Password()) == undefined || self.Password() == '') {
            errMsg += "Enter Password<br/>";
        }

        if (ko.toJS(self.ConfirmPassword() == undefined || self.ConfirmPassword() == '')) {
            errMsg += "Please Confirm Password !! <br/>";
        }
        if (ko.toJS(self.Password()) !== ko.toJS(self.ConfirmPassword())) {
            errMsg += "Passwords Donot Match. <br/>";
        }

        if (errMsg !== "") {

            toastr.error(errMsg);

            return false;
        }
        else {
            return true;
        }

    }

    self.Register = function () {
        if (!self.Validation()) return;
        var RegisterDetail = {
            UserName: self.UserName(),
            Email: self.Email(),
            Password: self.Password(),
            PhoneNo: self.PhoneNo(),
            Gender: self.Gender(),
            Age: self.Age(),
        }
        console.log(RegisterDetail);
        var url = '/User/SaveRegister';
        var data = { userRegister: RegisterDetail };
        $.post(url, data, function (res, status, xhr) {

            console.log(res);
            var obj = res;
            console.log(obj);
            if (obj.isSuccess) {
                toastr.success(obj.message);
                self.ClearControls();
                window.location.href = '/Login/Index';

            }
            else {
                self.ClearControls();
                alert('error', "Not Responding Please Try Later");
                self.ClearControls();
            }
        }).fail((xhr, status, message) => {
            jAlert(status, message);
        });
    }

    self.ClearControls = function () {
        self.UserName('');
        self.Email('');
        self.Password('');
        self.ConfirmPassword('');
        self.Gender('');
        self.PhoneNo('');
        self.Age('');
    }
}


$(document).ready(function () {
    ko.applyBindings(new UserRegisterViewModel());
});