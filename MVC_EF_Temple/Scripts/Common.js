/***
通用js
****/
function JsonDateFormate(val) {
    if (val != null) {
        var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
        //月份为0-11，所以+1，月份小于10时补个0
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate;
    }

    return "";
}

/**
* easyui Datagrid百分比显示宽度
*/
function fixWidth(percent) {
    return document.body.clientWidth * percent;//根据自身情况更改
}

//url：窗口调用地址，title：窗口标题，width：宽度，height：高度，shadow：是否显示背景阴影罩层
function showMessageDialog(url, title, width, height, shadow) {
    var content = '<iframe id="ifram1" src="' + url + '" width="100%" height="99%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="msgwindow" title="' + title + '"></div>'//style="overflow:hidden;"可以去掉滚动条
    $(document.body).append(boarddiv);
    var win = $('#msgwindow').dialog({
        content: content,
        width: width,
        height: height,
        modal: shadow,
        closed: false,
        closable: false,
        title: title,
        buttons: [{
            text: '保存',
            iconCls: 'icon-ok',
            handler: function (e) {
                 Edit(document.getElementById("ifram1").contentWindow.Edit(e));
            }},{
            text: '关闭',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#msgwindow').dialog('destroy');
            }}]
    });
    win.dialog('open');
}


//url：窗口调用地址，title：窗口标题，width：宽度，height：高度，shadow：是否显示背景阴影罩层
function showMessageDialog(url, title, width, height) {
    var content = '<iframe id="ifram1" src="' + url + '" width="100%" height="99%" frameborder="0" scrolling="no"></iframe>';
    var boarddiv = '<div id="msgwindow" title="' + title + '"></div>'//style="overflow:hidden;"可以去掉滚动条
    $(document.body).append(boarddiv);
    var win = $('#msgwindow').dialog({
        content: content,
        width: width,
        height: height,
        modal: false,
        closed: false,
        closable: false,
        title: title,
        buttons: [{
            text: '关闭',
            iconCls: 'icon-cancel',
            handler: function () {
                $('#msgwindow').dialog('destroy');
            }
        }]
    });
    win.dialog('open');
}

function Edit(callback) {
    if (callback[0] != undefined)
    {
        if (callback[0].flag == true)
        {
            $('#msgwindow').dialog('destroy');
        }
        ShowWaringMsg(callback[0].Message);
    }
    $('#dg').datagrid('reload');
}



//显示提示
function ShowMsgClose(message) {
    $.messager.alert('Success', message, "error");
}

//$.messager.alert({
//    title: title,//标题
//    msg: msg,//信息
//    icon: icon,//图标类型：error-错误；warning-警告; 或其他
//    left: 400,
//    top: 100
//});


//警告提示
function ShowWaringMsg(message) {
    $.messager.alert('Warning', message, "warning");
}

///是否删除提示框
function IsDelMsg(message, callBack) {
    $.messager.confirm('Confirm', message, function (r) {
        if (r) {
            callBack();
        };
    });
}

//成功提示
function ShowMsgSuccess(message) {
    $.messager.alert('Success', message);
}




