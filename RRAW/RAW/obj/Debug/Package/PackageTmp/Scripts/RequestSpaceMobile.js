$('div[data-role=page]').live('pageshow', function () {
	$.mobile.showPageLoadingMsg();

	$("#lblUserNameRequestSpaceMobile").html(GetCookie("mCurrentUserName"));
	$("#lblAppVersionRequestSpaceMobile").html(GetCookie("mCurrentAppVersion"));

	GetRequests(GetCookie("mCurrentUserID"));
});

function GetRequests(CurrentUserID) {
	RRAW.WebServices.Mobile.GetRequests(CurrentUserID, onSuccessOfGetRequests, onFailureOfGetRequests);
}

function onFailureOfGetRequests(res) {
	//window.location = "Errors.aspx?Operation=GetAllOceanMasters&ExceptionType=" + res._exceptionType + "&Msg=" + res._message + "&StackTrace=" + res._stackTrace + "&TimedOut=" + res._timedOut;
	$.mobile.hidePageLoadingMsg();
}

function onSuccessOfGetRequests(res) {
	var i = 0;

	$("#listRequests").children().remove();

	$.each(res, function (key, val) {
		//Create TITLE and add COUNT
		var division = "<li data-role='list-divider' role='heading' class='ui-li ui-li-divider ui-btn ui-bar-b ui-btn-up-undefined'><span id='lblDivisionTitle" + i + "'></span><span class='ui-li-count ui-btn-up-c ui-btn-corner-all' id='lblDivisionCount" + i + "'></span></li>";
		$("#listRequests").append(division);

		$("#lblDivisionTitle" + i).html(val.Metadata.TitleCount.Title);
		$("#lblDivisionCount" + i).html(val.Metadata.TitleCount.Count);

		//Create LIST ITEMS
		$.each(val.Requests, function (key, val) {
			var item = "<li data-theme='c' class='ui-btn ui-btn-icon-right ui-li ui-btn-up-c'><div class='ui-btn-inner ui-li'><div class='ui-btn-text'><a data-ajax='false' href='"
			switch (val.RateRequestType) {
				case "A":
					item += "NewRateRequest.aspx";
					break;
				case "O":
					item += "OceanRateRequest.aspx";
					break;
			}
			item += "?RateRequestID=" + key + "' class='ui-link-inherit'><h3 class='ui-li-heading'>" + key + " - " + val.OriginPort + " to " + val.DestinationPort + "</h3><p class='ui-li-desc'><strong>" + val.RequestDate + "</strong></p><p class='ui-li-desc'>" + val.OriginComment + "</p></a><span class='ui-icon ui-icon-arrow-r ui-icon-shadow'></span></div></div></li>";
			$("#listRequests").append(item);
		});


		i++;
	});


	$.mobile.hidePageLoadingMsg();
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