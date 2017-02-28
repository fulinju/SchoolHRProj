var a = 0;
jQuery.validRules = {
    required: {
        message: '该项为必填项'
    },
    email: {
        validator: function (self, param, callBack) {
            callBack(/^\w+(\.\w*)*@\w+\.\w+$/.test(self.val()));
        },
        message: '请输入正确的邮箱'
    },
    url: {
        validator: function (self, param, callBack) {
            var r = /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(self.val());
            callBack(r);
        },
        message: '请输入正确的网络地址'
    },
    length: {
        validator: function (self, param, callBack) {
            var value = self.val();
            callBack(value.length >= param[0] && value.length <= param[1]);
        },
        message: '长度必须介于{0}到{1}个字'
    },
    equalTo: {
        validator: function (self, param, callBack) {
            var targetValue = $('#' + param[0]).val();
            callBack(self.val() == targetValue);
        },
        message: '两次输入的字符不一至'
    },
    notEqualTo: {
        validator: function (self, param, callBack) {
            var targetValue = $('#' + param[0]).val();
            callBack(self.val() != targetValue);
        },
        message: '输入的内容不能重复'
    },
    match: {
        validator: function (self, param, callBack) {
            var reg = new RegExp(param[0]);
            callBack(reg.test(self.val()));
        },
        message: '请输入正确的值'
    },
    notMatch: {
        validator: function (self, param, callBack) {
            var reg = new RegExp(param[0]);
            callBack(!reg.test(self.val()));
        },
        message: '请输入正确的值'
    },
    minLength: {
        validator: function (self, param, callBack) {
            var value = self.val();
            callBack(value.length >= param[0]);
        },
        message: '长度不能小于{0}个字'
    },
    maxLength: {
        validator: function (self, param, callBack) {
            var value = self.val();
            callBack(value.length <= param[0]);
        },
        message: '长度不能超过{0}个字'
    },
    range: {
        validator: function (self, param, callBack) {
            var value = self.val();
            if (/^-?\d+$/.test(value)) {
                var val = Number(value);
                callBack(val >= param[0] && val <= param[1]);
            } else {
                callBack(false);
            }
        },
        message: '值要在区间[{0},{1}]内的整数'
    },
    remote: {
        validator: function (self, param, callBack) {
            clearTimeout(a);
            a = setTimeout(function () {
                var array = [];
                $.each(param, function (k, v) { if (k > 0) array.push(v + '=' + $('#' + v).val()); });
                $.post(param[0], array.join('&'), function (data) {
                    if (data.state) {
                        callBack(data.state);
                    } else {
                        $.validRules.remote.message = data.message;
                        callBack(data.state);
                    }
                }, 'json');
            }, "300");
        },
        ajax: undefined,
        message: '请输入正确的值'
    },
    dropDown: {
        validator: function (self, param, callBack) {
            var value = self.val();
            callBack(value != undefined && value != "" && value != "0" && value != "-1");
        },
        message: '请选择一个下拉项'
    },
    dateTime: {
        validator: function (self, param, callBack) {
            var value = self.val();
            var ereg = /^(\d{1,4})(-|\/)(\d{1,2})(-|\/)(\d{1,2})$/;
            var r = value.match(ereg);
            if (r == null || r == "") {
                callBack(false);
            } else {
                var d = new Date(r[1], r[3] - 1, r[5]);
                callBack((d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[5]));
            }
        },
        message: '请输入正确的日期'
    },
    digits: {
        validator: function (self, param, callBack) {
            callBack(/^\d+$/.test(self.val()));
        },
        message: '请输入正整数'
    },
    number: {
        validator: function (self, param, callBack) {
            callBack(/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(self.val()));
        },
        message: '请输入正确的数字'
    },
    cellPhone: {
        validator: function (self, param, callBack) {
            callBack(/^1(?:[38]\d|4[57]|5[01256789])\d{8}$/.test(self.val()));
        },
        message: '手机号码格式不正确'
    }
};


(function ($) {
    // 隐藏提示信息
    function hideError(self, hideImage) {
        $('.validbox-tip').remove();
        if (hideImage) self.removeClass("valid-error");
        return self;
    };

    // 显示错误信息
    function showError(self, message) {
        $(self)[0].focus();
        hideError(self, true).addClass("valid-error");
        var left = self.offset().left + self.width() + Number(self.css("padding-right").match(/-?\d+/)[0]) + Number(self.css("padding-left").match(/-?\d+/)[0]);
        var top = self.offset().top;
        var html = '<div class="validbox-tip"><span class="m">' + message + '</span><span class="p"></span></div>';
        $(html).css('left', left).css("top", top).appendTo("body");
        var offsetTop = top - $(document).scrollTop();
        if (offsetTop <= 0 || offsetTop >= $(window).height()) {
            $("body").animate({ scrollTop: top - $(window).height() / 2 - self.height() / 2 }, 400);
        }
        // 把信息缓存，在mouseEnter时使用
        self.data('message', message);
        return self;
    };

    // 合成信息
    function getMessage(message, param) {
        for (var i = 0; i < param.length; i++) {
            var reg = new RegExp('\\{' + i + '\\}', 'gm');
            message = message.replace(reg, param[i]);
        }
        return message;
    }

    // 获取是否为必须输入 
    function getRequired(self) {
        var key = 'required';
        var rule = self.data(key);

        if (rule == undefined) {
            rule = { required: self.attr(key) != undefined, message: self.attr("required-message") };
            self.data(key, rule);
        }
        return rule;
    };

    // 获取相关的验证规则
    function getRules(self) {
        var key = 'rule';
        var rule = self.data(key);
        if (rule) {
            return rule;
        }

        var validTypeArray = $.trim(self.attr("validType")).split(';');
        var messageArray = eval(self.attr("message")) || [];

        var rules = [];
        $.each(validTypeArray, function (k, v) {
            if (v.length > 0) {
                var ruleName = v.replace(/\[.*\]/, '');
                var param = eval(v.replace(ruleName, '')) || [];
                var message = messageArray.length > k ? messageArray[k] : undefined;
                rules.push({ ruleName: ruleName, param: param, message: message });
            }
        });
        self.data(key, rules);
        return rules;
    };

    // 对规则进行验证
    function validRules(self, callBack) {
        var required = getRequired(self);
        // 不必输入且没有输入
        var value = self.val().replace(/(^\s*)|(\s*$)/g, "");
        if (required.required == false && value.length == 0) {
            hideError(self, true);
            if ($.isFunction(callBack)) callBack(true);
            return;
        }
        // 必须输入而未输入
        if (required.required && value.length == 0) {
            showError(self, required.message || $.validRules.required.message);
            if ($.isFunction(callBack)) callBack(false);
            return;
        }

        var index = 0;
        var rules = getRules(self);

        var _validRules = function () {
            if (index < rules.length) {
                var rule = rules[index++];
                var validRule = $.validRules[rule.ruleName];
                validRule.validator(self, rule.param, function (r) {
                    if (r == false) {
                        var message = getMessage(rule.message || validRule.message, rule.param);
                        showError(self, message);
                        if ($.isFunction(callBack))
                            callBack(r);
                        return;
                    } else {
                        _validRules();
                    }
                });
            } else {
                if ($.isFunction(callBack)) callBack(true);
                hideError(self, true);
            }
        };
        _validRules();
    };

    // 初始化
    function _init() {
        // 自动加载css
        var selfPath = $("script:last").attr("src");
        var cssPath = selfPath.substring(0, selfPath.lastIndexOf("/") + 1) + 'images/validBox.css';
        var likecss = document.createElement("link");
        likecss.setAttribute("rel", "stylesheet");
        likecss.setAttribute("type", "text/css");
        likecss.setAttribute("href", cssPath);
        var heads = document.getElementsByTagName("head");
        if (heads.length)
            heads[0].appendChild(likecss);
        else
            document.documentElement.appendChild(likecss);
        // 绑定事件验证
        var seletor = ".validBox";
        $(document).on("keyup", seletor, function () {
            var self = $(this);
            validRules(self);
        }).on("mouseenter", seletor, function () {
            var self = $(this);
            if (self.hasClass("valid-error")) showError(self, self.data('message'));
        }).on("mouseleave blur", seletor, function () {
            var self = $(this);
            if (self.hasClass("valid-error")) hideError(self, false);
        });
    };
    // 验证元素的输入
    $.fn.validInput = function (callBack) {
        var self = $(this);
        var boxs = self.filter(function () { return $(this).hasClass("validBox"); });
        if (boxs.length == 0) boxs = self.find(".validBox");

        var index = 0;
        var _validInput = function () {
            if (index < boxs.length) {
                var box = $(boxs[index++]);
                validRules(box, function (r) {
                    if (r == false) {
                        if ($.isFunction(callBack)) callBack(r);
                        return;
                    } else {
                        _validInput();
                    }
                });
            } else {
                if ($.isFunction(callBack)) callBack(true);
            }
        };
        _validInput();
    };

    _init();

})(jQuery);


