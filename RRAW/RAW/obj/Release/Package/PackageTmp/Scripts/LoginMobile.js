$(document).bind("mobileinit", function () {
	$("#txtUserID").focus();
});

$("#btnLogin").click(function () {
	$.mobile.showPageLoadingMsg();

	VerifyLogin($("#txtUserID").val(), $("#txtPassword").val());
});

function VerifyLogin(userName, password) {
	RRAW.WebServices.Mobile.VerifyLogin(userName, password, onSuccessOfVerifyLogin, onFailureOfVerifyLogin);
}

function onFailureOfVerifyLogin(res) {
	//window.location = "Errors.aspx?Operation=GetAllOceanMasters&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
}

function onSuccessOfVerifyLogin(res) {
	if (res["CurrentUserID"] > 0) {
		setCookie("mAuthenticationToken", res["AuthenticationToken"], 1);
		setCookie("mCurrentAppVersion", res["CurrentAppVersion"], 1);
		setCookie("mCurrentUserID", res["CurrentUserID"], 1);
		setCookie("mCurrentUserName", res["CurrentUserName"], 1);
		setCookie("mCurrentUserType", res["CurrentUserType"], 1);

		$.mobile.changePage("Navigation.htm", { transition: "slide" });
	}
	else {
		$.mobile.hidePageLoadingMsg();
		$("#lblError").show();
	}
}

function GetCurrentDate() {
	var t = new Date();
	return ((t.getMonth() + 1) + '/' + t.getDate() + '/' + t.getFullYear() + ' ' + t.getHours() + ':' + t.getMinutes() + ':' + t.getSeconds());
}

function GetDate(dt) {
	dt = new Date(dt);
	var m = dt.getMonth() + 1;
	var d = dt.getDate();
	var y = dt.getFullYear();

	return (((m < 10) ? ("0" + m) : m) + '/' + ((d < 10) ? ("0" + d) : d) + '/' + y);
}

function GetQueryString(key) {
	var re = new RegExp('(?:\\?|&)' + key + '=(.*?)(?=&|$)', 'gi');
	var r = [], m;
	while ((m = re.exec(document.location.search)) != null) r[r.length] = m[1];
	if (r.length > 0) {
		return r;
	}
	else {
		return undefined;
	}
}

function GetCookie(c_name) {
	var i, x, y, ARRcookies = document.cookie.split(";");
	for (i = 0; i < ARRcookies.length; i++) {
		x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
		y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
		x = x.replace(/^\s+|\s+$/g, "");
		if (x == c_name) {
			return unescape(y);
		}
	}
}

function setCookie(c_name, value, exdays) {
	var exdate = new Date();
	exdate.setDate(exdate.getDate() + exdays);
	var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
	document.cookie = c_name + "=" + c_value;
}

$("input[type='text'], input[type='password']").keypress(function (e) {
	if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
		$("#btnLogin").click();
		return true;
	}
});