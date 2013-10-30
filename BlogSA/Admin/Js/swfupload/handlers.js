var progressbarContainer = $('<div class="progress-container ui-corner-all"></div>');
var progressbarWrapper = $('<div class="progress-wrapper"></div>');
var progressCancel = $('<a href="#" class="progress-cancel ui-icon ui-icon-circle-close"></a>');

var progressText = $('<div class="progress-title"></div>');
var progressStatus = $('<div class="progress-status"></div>');

var progressBar = $('<div id="progressbar" class="ui-corner-all"></div>');
var clear = $('<div style="clear:both;height:1px;display:block;"></div>');

progressbarWrapper.append(progressCancel);
progressbarWrapper.append(progressText);
progressbarWrapper.append(progressStatus);
progressbarWrapper.append(clear);
progressbarWrapper.append(progressBar);

progressbarContainer.append(progressbarWrapper);

$(document).ready(function () {
    $("#divFileProgressContainer").append(progressbarContainer);
    //progressCancel.hide();
    progressBar.progressbar({ value: 0 });
    progressBar.height(14);
    progressText.text("File");
    progressStatus.text("Preparing");
    progressbarContainer.hide();
});

function fileQueueError(file, errorCode, message) {
    try {
        var imageName = "error.gif";
        var errorName = "";
        if (errorCode === SWFUpload.errorCode_QUEUE_LIMIT_EXCEEDED) {
            errorName = "You have attempted to queue too many files.";
        }

        if (errorName !== "") {
            alert(errorName);
            return;
        }

        switch (errorCode) {
            case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
                imageName = "zerobyte.gif";
                break;
            case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
                imageName = "toobig.gif";
                break;
            case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
            default:
                alert(message);
                break;
        }
    } catch (ex) {
        this.debug(ex);
    }

}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
    try {
        if (numFilesQueued > 0) {
            this.startUpload();
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadProgress(file, bytesLoaded) {

    try {
        progressbarContainer.show();

        var percent = Math.ceil((bytesLoaded / file.size) * 100);

        progressText.text(file.name);
        progressBar.progressbar("value", percent);

        if (percent === 100) {
            progressStatus.text(Blogsa.Language.FileLoading);
            progressCancel.hide();
        } else {
            progressStatus.text(Blogsa.Language.FileUploading);
            progressCancel.show();
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadSuccess(file, serverData) {
    try {
        var data = eval(serverData);

        var elLi = $("<li></li>");

        var elSpan = $('<span class="file-name">' + file.name + '</span>');
        var elA = $('<div class="file-link-holder"><a class="sbtn" href="' + data[0].url + '"><span>' + Blogsa.Language.Show + '</span></a></div>');

        if (data[0].thumbnailUrl != "") {
            var elImg = $('<div class="file-image-holder"><img class="file-image" src="' + data[0].thumbnailUrl + '" alt=""/></div>');
            elLi.append(elImg);
        }

        elLi.append(elA);
        elLi.append(elSpan);
        var fileLiClear = $('<div style="clear:both;height:1px;display:block;"></div>');
        elLi.append(fileLiClear);

        $("#files ul").append(elLi);
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadComplete(file) {
    try {
        /*  I want the next upload to continue automatically so I'll call startUpload here */
        if (this.getStats().files_queued > 0) {
            this.startUpload();
        } else {
            progressBar.hide();
            progressStatus.hide();
            progressCancel.hide();
            progressText.text(Blogsa.Language.AllFilesReceived);
        }
    } catch (ex) {
        this.debug(ex);
    }
}

function uploadError(file, errorCode, message) {
    var imageName = "error.gif";
    var progress;
    try {
        switch (errorCode) {
            case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
                try {
                    //progress = new FileProgress(file, this.customSettings.upload_target);
                    //progress.setCancelled();
                    //progress.setStatus("Cancelled");
                    //progress.toggleCancel(false);
                }
                catch (ex1) {
                    this.debug(ex1);
                }
                break;
            case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
                try {
                    //progress = new FileProgress(file, this.customSettings.upload_target);
                    //progress.setCancelled();
                    //progress.setStatus("Stopped");
                    //progress.toggleCancel(true);
                }
                catch (ex2) {
                    this.debug(ex2);
                }
            case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
                imageName = "uploadlimit.gif";
                break;
            default:
                alert(message);
                break;
        }

        //addImage("images/" + imageName);

    } catch (ex3) {
        this.debug(ex3);
    }

}
