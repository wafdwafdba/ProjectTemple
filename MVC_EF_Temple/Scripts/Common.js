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