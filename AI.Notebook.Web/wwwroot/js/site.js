function IsNullOrEmpty(value) {
    return value == null || value === "";
}

function BreakToNewline(str) {
    return str.replace(/<br\s*\/?>/mg, "\r");
}

function NewlineToBreak(str) {
    return str.replace(/\n/g, "<br/>");
}

function CarriageReturnNewlineToBreak(str) {
    return str.replace(/\r\n/g, "<br/>");
}

function siteAlert(alertTitle, alertMessage, callback) {

    alertMessage = NewlineToBreak(alertMessage);
    bootbox.confirm({
        title: alertTitle,
        message: alertMessage,
        callback: function (result) {
            if (callback && typeof (callback === "function")) {
                callback(result);
            }
        }
    });
}

function siteConfirm(confirmTitle, confirmMessage, cancelButtonLabel, proceedButtonLabel, callback) {
    var cancelButtonText = "No";
    var proceedButtonText = "Yes"

    if (!IsNullOrEmpty($.trim(cancelButtonLabel)))
        cancelButtonText = cancelButtonLabel;
    if (!IsNullOrEmpty($.trim(proceedButtonLabel)))
        proceedButtonText = proceedButtonLabel;

    confirmMessage = NewlineToBreak(confirmMessage);
    bootbox.confirm({
        title: confirmTitle,
        message: confirmMessage,
        buttons: {
            confirm: {
                label: proceedButtonText,
                className: 'btn-success'
            },
            cancel: {
                label: cancelButtonText,
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (callback && typeof (callback === "function")) {
                callback(result);
            }
        }
    });
}