function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
};
function getJxid() {
    return getCookie("loginflag");
}
function jsonDate(strTime) {
    var obj = new Date(parseInt(strTime.replace("/Date(", "").replace(")/", ""), 10));
    return obj.getFullYear() + "-" + (obj.getMonth() + 1) + "-" + obj.getDate();
}
function jsonTime(strTime) {
    var obj = new Date(parseInt(strTime.replace("/Date(", "").replace(")/", ""), 10));
    return  obj.getHours() + ":" + obj.getMinutes() + ":" + obj.getSeconds();
}
function jsonDateTime(strTime) {
    var obj = new Date(parseInt(strTime.replace("/Date(", "").replace(")/", ""), 10));
    return obj.getFullYear() + "-" + (obj.getMonth() + 1) + "-" + obj.getDate() + " " + obj.getHours() + ":" + obj.getMinutes() + ":" + obj.getSeconds();
}
/** 
 * param 将要转为URL参数字符串的对象,并把json对象里的日期时间格式化成 yyyy-MM-dd hh:mm:ss
 * key URL参数字符串的前缀 
 * encode true/false 是否进行URL编码,默认为true 
 *  
 * return URL参数字符串 
 */
var urlEncode = function (param, key, encode) {
    if (param == null) return '';
    var paramStr = '';
    var t = typeof (param);
    if (t == 'string' || t == 'number' || t == 'boolean') {
        paramStr += '&' + key + '=' + ((encode == null || encode) ? encodeURIComponent(param) : param);
    } else {
        for (var j in param) {
            var str = param[j];
            var t = typeof (str);
            if (t == 'string') {
                if (str.indexOf('/Date') != -1 ) {
                    var tmp = jsonDateTime(param[j]);
                    param[j] = tmp;
                    //alert(tmp);
                }
            }
        }
        for (var i in param) {
            var k = key == null ? i : key + (param instanceof Array ? '[' + i + ']' : '.' + i);
            paramStr += urlEncode(param[i], k, encode);
        }
    }
    return paramStr;
}
/**
 * 把json对象里的日期时间格式化成 yyyy-MM-dd
 * @param {any} param
 */
var jsonObjFormatDate = function (param) {
    if (param == null) return param;
    for (var j in param) {
        var str = param[j];
        var t = typeof (str);
        if (t == 'string') {
            if (str.indexOf('/Date') != -1) {
                var tmp = jsonDate(param[j]);
                param[j] = tmp;
            }
        }
    }
    return param;
}

var getIDname = function (id, obj) {
    for (var i in obj) {
        if (obj[i].ID === id) {
            return obj[i].Name;
        }
    }
    return "暂无数据";
}