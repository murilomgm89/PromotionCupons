/*global console*/
(function() {
    'use strict';
    window.addEventListener('load', function() {
        var s = document.querySelectorAll('script[data-src]'),
            i = 0,
            fn = function() {
                var str = this;
                str = (str && (str.getAttribute('data-name') || str.src)) || 'script';
                if (window._satellite) {
                    console.log("SHIFT: " + str + ' end load');
                    if (window._satellite.pageBottom) {
                        console.log("SHIFT: _satellite.pageBottom()");
                        window._satellite.pageBottom();
                    } else
                        console.log('SHIFT: _satellite.pageBottom === undefined');
                } else
                    console.log('SHIFT: _satellite === undefined');
            },
            src;
        for (; i < s.length; i++) {
            s[i].onload = fn;
            src = s[i].getAttribute('data-src');
            s[i].src = src;
            src = src.replace(/(?:^.*\/|^)(\w+).*?\.js.*/i, '$1') || src;
            s[i].setAttribute('data-name', src);
            console.log('SHIFT: ' + src + ' init load');
        }
    }, false);
    var form = document.querySelector('form'),
        input = document.getElementsByTagName('input'),
        select = document.getElementsByTagName('select'),
        cep = document.querySelector('input[name="person.Address.cep"]'),
        endereco = document.querySelector('input[name="person.Address.address"]'),
        bairro = document.querySelector('input[name="person.Address.district"]'),
        lubs = document.querySelectorAll('.enviados > div > ul.lub > li > span'),
        uf = document.querySelector('select[name="uf"]');
    var addCities = function(uf, cb) {
            var xhr = new XMLHttpRequest();
            xhr.open('GET', '/state/' + uf + '/cities', true);
            xhr.onreadystatechange = function() {
                if (this.readyState === 4) {
                    if (this.status >= 200 && this.status < 400) {
                        var data = JSON.parse(this.responseText),
                            el = document.querySelector('select[name="person.Address.idCity"]'),
                            str = '<option value="" disabled=""></option>',
                            i;
                        for (i = 0; i < data.length; i++)
                            if (data[i].idCity)
                                str += '<option value="' + data[i].idCity + '">' + data[i].name + '</option>';
                        if (el)
                            el.innerHTML = str;
                        if (cb)
                            cb();
                    }
                }
            };
            xhr.send();
            xhr = null;
        },
        addAddress = function() {
            var val = this.value;
            if (val) {
                val = val.replace(/\D+/g, '');
                if (val.length !== 8)
                    return;
            } else
                return;
            var xhr = new XMLHttpRequest();
            xhr.open('GET', '/address/' + val, true);
            xhr.onreadystatechange = function() {
                if (this.readyState === 4) {
                    if (this.status >= 200 && this.status < 400) {
                        var data = JSON.parse(this.responseText),
                            idCity = data.city,
                            opt = data.state && uf ? uf.querySelector('[value="' + data.state + '"]') : null;
                        if (data.address && endereco)
                            endereco.value = data.address;
                        if (data.district && bairro)
                            bairro.value = data.district;
                        if (opt) {
                            opt.selected = true;
                            addCities(data.state, function() {
                                var el = document.querySelector('select[name="person.Address.idCity"] option[value="' + idCity + '"]');
                                if (el)
                                    el.selected = true;
                            });
                        }
                    }
                }
            };
            xhr.send();
            xhr = null;
        },
        imgUpload = function() {
            var self = this;
            if (!self || !self.value)
                return;
            var el = document.getElementById('image-name');
            el.value = self.value.replace(/^.*(\\|\/)/, '');
            el.parentNode.removeAttribute('data-valid');
            self.parentNode.removeAttribute('data-valid');
            if (self.files && self.files.length && self.files[0] && self.files[0].size > 4e6) {
                if (window.alert)
                    window.alert('Arquivo maior que 4MB');
                el.parentNode.setAttribute('data-valid', 'false');
                self.parentNode.setAttribute('data-valid', 'false');
                self.value = '';
            }
        },
        noForm = function() {
            var el = document.querySelectorAll('.winners > ul > li > a'),
                i;
            for (i = 0; i < el.length; i++)
                el[i].addEventListener('click', ev.winners, false);
        },
        loadScript = function(src, cb) {
            var head = document.getElementsByTagName('body')[0] || document.documentElement,
                script = document.createElement('script');
            var done = false;
            script.onload = script.onreadystatechange = function() {
                if (!done && (!this.readyState || this.readyState === 'loaded' || this.readyState === 'complete')) {
                    done = true;
                    script.onload = script.onreadystatechange = null;
                    if (head && script.parentNode)
                        head.removeChild(script);
                    cb();
                }
            };
            script.src = src;
            head.insertBefore(script, head.firstChild);
        },
        ev = {},
        mask = {},
        validate = {};

    function fn() {
        var newwin = document.querySelectorAll('[target="newwin"]'),
            abas = document.querySelectorAll('.enviados ul.abas span'),
            arr = ['change', 'keyup', 'textInput', 'input', 'click'],
            fns = {
                'person.Address.cep': addAddress,
                ReceiptFile: imgUpload,
                aceitoTermos: ev.termos
            },
            i, j;
        if (!form) {
            noForm();
            return;
        }
        if (uf) {
            uf.addEventListener('change', function() {
                addCities(this.value);
                this.blur();
            }, false);
        }
        for (i = 0; i < lubs.length; i++)
            lubs[i].addEventListener('click', ev.lubs, false);
        if (lubs && lubs[0])
            ev.lubs.call(lubs[0]);
        for (i = 0; i < select.length; i++)
            select[i].addEventListener('change', ev.select, false);
        for (i = 0; i < newwin.length; i++)
            newwin[i].addEventListener('click', ev.newwin, false);
        for (i = 0; i < abas.length; i++)
            abas[i].addEventListener('click', ev.abas, false);
        if (abas.length)
            ev.abas.call(abas[0]);
        if (cep)
            cep.addEventListener('keyup', addAddress, false);
        for (i = 0; i < input.length; i++) {
            if (/text|tel|number/i.test(input[i].type)) {
                for (j = 0; j < arr.length; j++) {
                    input[i].addEventListener(arr[j], ev.input, false);
                    if (fns[input[i].name])
                        input[i].addEventListener(arr[j], fns[input[i].name], false);
                    if (input[i].value.length && input[i].parentNode.getAttribute('data-valid') !== 'false')
                        ev.input.call(input[i]);
                }
            } else if (fns[input[i].name])
                for (j = 0; j < arr.length; j++)
                    input[i].addEventListener(arr[j], fns[input[i].name], false);
        }

        form.setAttribute('novalidate', 'novalidate');
        form.addEventListener('submit', ev.submit, false);
    }
    ev.termos = function() {
        var self = this;
        if (!self)
            return;
        self.parentNode.setAttribute('data-valid', self.checked.toString);
    };
    ev.select = function() {
        if (this)
            this.parentNode.setAttribute('data-valid', (this.selectedIndex > 0).toString());
    };
    ev.lubs = function() {
        var self = this,
            i;
        for (i = 0; self && i < lubs.length; i++) {
            lubs[i].className = lubs[i].className.replace(/(^|\s)active($|\s)/gi, '');
        }
        self.className = ' active';
        self.className = self.className.replace(/^\s+/g, '');
    };
    ev.winners = function(e) {
        if (e)
            e.preventDefault();
        var el = this,
            active = document.querySelectorAll('.winners > ul > li > a'),
            val = (el && el.innerHTML && el.innerHTML.replace(/[^a-zA-Z]/i, '').toLowerCase()) || null,
            i;
        for (i = 0; i < active.length; i++)
            active[i].className = '';
        if (el && val) {
            el.className = 'active';
            el = document.querySelectorAll('.winners table');
            for (i = 0; i < el.length; i++) {
                if (el[i].getAttribute('data-index') === val.toUpperCase())
                    el[i].className = 'active';
                else
                    el[i].className = '';
            }
        }
    };
    ev.submit = function(e) {
        var el = document.querySelectorAll('form input, form select'),
            imgName = document.querySelector('#image-name'),
            i, b, valid;
        for (i = 0; i < el.length; i++) {
            if (el[i].hasAttribute('data-required') && (!el[i].hasAttribute('data-ignore'))) {
                if (!/file/i.test(el[i].type) &&
                    (((/text|tel|number/i.test(el[i].type) && !el[i].value) ||
                        (/select/i.test(el[i].tagName) && el[i].selectedIndex < 1)) || !validate.init(el[i]))) {
                    el[i].parentNode.setAttribute('data-valid', 'false');
                    if (e)
                        e.preventDefault();
                    b = true;
                } else if (/file/i.test(el[i].type)) {
                    valid = el[i].files && el[i].files.length && el[i].files[0] && el[i].files[0].size < 4e6;
                    valid = valid ? 'true' : 'false';
                    el[i].parentNode.setAttribute('data-valid', valid);
                    if (imgName)
                        imgName.parentNode.setAttribute('data-valid', valid.toString());
                } else {
                    el[i].parentNode.setAttribute('data-valid', 'true');
                }
            } else if (/text|tel|number/i.test(el[i].type))
                el[i].setAttribute('data-empty', 'true');
        }
        for (i = 0; !b && i < el.length; i++) {
            if (/(^(cpf|phone)$)|cnpj|cep/i.test(el[i].name.replace(/.*\./, '')))
                el[i].value = el[i].value.replace(/\D+/g, '');
            else if (el[i].hasAttribute('data-empty') && !el[i].value)
                el[i].value = 'x';
        }
        if (e)
            e.preventDefault();
        if (!b) {
            if (document.querySelector('#image-name')) {
                el = document.body || document.querySelector('body');
                el.className = el.className.replace(/(^|\s)aguarde(\s|$)/g, '') + ' aguarde';
                el.className = el.className.replace(/^\s+/g, '');
            }
            form.submit();
        }
    };
    ev.newwin = function(e) {
        if (e)
            e.preventDefault();
        var wh;
        wh = this.getAttribute('data-size');
        if (wh)
            wh = wh.split('x');
        window.open(this.href, '', 'status=no,height=' + wh[1] + ',width=' + wh[0] + ',resizable=yes,toolbar=no,menubar=no,scrollbars=no,location=no,directories=no');
    };
    ev.abas = function() {
        var abas = document.querySelectorAll('.enviados ul.abas span'),
            ul = document.querySelectorAll('.enviados ul.abas + div ul'),
            i;
        for (i = 0; i < abas.length; i++)
            abas[i].className = '';
        if (this)
            this.className = 'active';
        for (i = 0; i < ul.length; i++) {
            if (/vpower/i.test(ul[i].className))
                ul[i].className = (/power/i.test(this.innerHTML) ? 'vpower active' : 'vpower');
            else if (/lub/i.test(ul[i].className))
                ul[i].className = (/lubrificantes/i.test(this.innerHTML) ? 'lub active' : 'lub');
        }
    };
    ev.input = function() {
        var self = this;
        if (!self || !('selectionStart' in self))
            return;
        var dataValid = self.parentNode.getAttribute('data-valid'),
            oldest = self.getAttribute('data-old');
        if (oldest === self.value && (!dataValid || dataValid === 'true')) {
            oldest = (new RegExp(self.getAttribute('data-pattern'))).exec(self.value);
            if (oldest && oldest.length && oldest[0] !== self.value && oldest[0].length > self.getAttribute('data-maxlength'))
                self.value = oldest[0];
            return;
        }
        if (self.select && !self.selectionStart && !self.selectionEnd && /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream)
            self.select();
        var ret = [],
            filter = self.hasAttribute('data-filter'),
            caret = [Math.min(self.selectionStart, self.selectionEnd), Math.max(self.selectionStart, self.selectionEnd)],
            maxlength = self.getAttribute('data-maxlength'),
            pattern = self.getAttribute('data-pattern'),
            val, tmp;
        caret[2] = self.value.substring(caret[1], self.value.length);
        if (!self.value && !dataValid) {
            self.parentNode.removeAttribute('data-valid');
            self.parentNode.parentNode.removeAttribute('data-valid');
        } else if (!self.parentNode.hasAttribute('data-valid')) {
            self.parentNode.setAttribute('data-valid', '');
            self.parentNode.parentNode.setAttribute('data-valid', '');
        } else if (dataValid === 'true') {
            if (!self.value) {
                self.parentNode.setAttribute('data-valid', 'false');
                self.parentNode.parentNode.setAttribute('data-valid', 'false');
            }
        }
        if (self.hasAttribute('data-mask')) {
            ret[0] = mask.init(self);
            ret[1] = mask.clean(ret[0].value, ret[0].arrMask, ret[0].dataRe);
            self.value = mask.filter(ret[1].value, ret[1].mask, ret[0].arrVal, ret[0].dataRe);
        } else if (filter) {
            val = self.value.match((new RegExp(pattern, 'gi')));
            self.value = val && val.length > 0 ? val.join('') : '';
        }
        if (maxlength && (self.name.replace(/.*\./, '') === 'phone' ? self.value.length + 1 : self.value.length) >= (maxlength | 0)) {
            self.value = self.value.substring(0, window.parseInt(maxlength));
            self.parentNode.setAttribute('data-valid', (validate.init(self)).toString());
        }
        self.setAttribute('data-old', self.value);
        tmp = self.value;
        if (oldest && tmp) {
            if (oldest !== tmp) {
                self.setSelectionRange(tmp.length - caret[2].length, tmp.length - caret[2].length);
            } else
                self.setSelectionRange(caret[0], caret[1]);
        }
        if (self.parentNode.getAttribute('data-valid') && self.parentNode.getAttribute('data-valid') !== validate.init(self))
            self.parentNode.setAttribute('data-valid', (validate.init(self)).toString());
    };
    validate.init = function(el) {
        if (function() {
                if (!el)
                    return false;
                var pattern = el.getAttribute('data-pattern');
                if (!(el.name.replace(/.*\./, '') in validate) && pattern)
                    return (new RegExp(pattern, 'i')).test(el.value);
                if (el.name.replace(/.*\./, '') in validate)
                    return validate[el.name.replace(/.*\./, '')](el, pattern);
                if (/radio|checkbox/i.test(el.type))
                    return el.checked;
                if (/select/i.test(el.tagName))
                    return el.selectedIndex > 0;
                return el.value.length > 0;
            }()) {
            el.parentNode.removeAttribute('data-custom');
            return true;
        }
        return false;
    };
    validate.init.custom = function(el, index) {
        if (!el || !el.tagName) {
            if (el && el.length && el[0].tagName)
                el = el[0];
            else
                return false;
        }
        if (index >= 0) {
            el.setAttribute('data-custom', index);
        } else
            el.removeAttribute('data-custom');
        return false;
    };
    validate.ReceiptFile = function(el) {
        var self = el;
        return (self && self.files && self.files.length && self.files[0] && self.files[0].size <= 4e6);
    };
    validate.cpf = function(el) {
        var i, j, s, r, str;
        str = el.value;
        str = str.replace(/\D+/g, '');
        if (str.length !== 11 || str.match(/^(\d)\1+$/))
            return false;
        for (i = 0; i < 2; i++) {
            s = 0;
            for (j = 0; j <= 9 + i; j++)
                s += window.parseInt(str.substring(j - 1, j) || 0) * (11 + i - j);
            r = (s * 10) % 11;
            r = (r === 10) || (r === 11) ? 0 : r;
            if (r !== window.parseInt(str.substring(9 + i, 10 + i)))
                return false;
        }
        return true;
    };
    validate.vendorCNPJ = function(el) {
        var i, sum, len, beg, end, pos, str, ret;
        str = el.value;
        str = str.replace(/\D+/g, '');
        if (str.length !== 14 || str.match(/^(\d)\1+$/))
            return false;
        // Valida DVs
        len = str.length - 2;
        beg = str.substring(0, len);
        end = str.substring(len);
        sum = 0;
        pos = len - 7;
        for (i = len; i >= 1; i--) {
            sum += beg.charAt(len - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        ret = sum % 11 < 2 ? 0 : 11 - sum % 11;
        if (ret != end.charAt(0))
            return false;

        len = len + 1;
        beg = str.substring(0, len);
        sum = 0;
        pos = len - 7;
        for (i = len; i >= 1; i--) {
            sum += beg.charAt(len - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        ret = sum % 11 < 2 ? 0 : 11 - sum % 11;
        if (ret != end.charAt(1))
            return false;
        return true;
    };
    validate.phone = function(el) {
        var str = el.value.replace(/\D+/g, '');
        str = /^([1-9]{2})(\d{8,9}$)/.exec(str);
        return !!(str &&
                str[1] &&
                !/^(2[3569]|3[69]|5[26789]|7[268])$/.test(str[1]) &&
                str[2] &&
                //, /^([9]\d{8}|[^9]\d{7})$/.test(str[2]) &&
                //algumas regiões não tem o nono digito
                !/^(\d)\1+$/.test(str[2])) &&
            !/^[01]/.test(str[2]);
    };
    validate.birth = function(el) {
        var str = el.value;
        str = str.replace(/\D+/g, '');
        if (str.length !== 8 || str.match(/^(\d)\1+$/))
            return false;

        function getDay(str, beg, end) {
            var ret = str.substring(beg, end);
            return ret && !isNaN(ret) ? window.parseInt(ret, 10) : 0;
        }
        var d = getDay(str, 0, 2),
            m = getDay(str, 2, 4),
            y = getDay(str, 4, 8),
            date = (new Date(y, m - 1, d)).getTime(),
            now = (new Date()).getTime(),
            dMax = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
        if (!d || !m || !y)
            return false;
        if (y % 400 === 0 || (y % 100 !== 0 && y % 4 === 0))
            dMax[1] = 29;
        if (m > 0 && m <= 12) {
            if (d > 0 && d <= dMax[m - 1]) {
                if (y >= 1900 && (now - date) > 567648e6)
                    return true;
                else
                    return validate.init.custom(el.parentNode, 2);
            } else
                return validate.init.custom(el.parentNode, 0);
        } else
            return validate.init.custom(el.parentNode, 1);
        return false;
    };
    validate.cep = function(el) {
        var str = el.value;
        str = str.replace(/\D+/g, '');
        if (str.length !== 8)
            return false;
        return true;
    };
    mask.init = function(el, val) {
        var pattern = el.getAttribute('data-pattern');
        if (pattern && pattern.match(/\(.*?\)/)) {
            pattern = pattern.replace(/[^\\](\+|\*)/g, function(str) {
                var obj = {
                    '*': '{0,}',
                    '+': '{1,}'
                };
                return str.slice(0, -1) + obj[str.slice(-1)] || '';
            });
        } else
            return null;
        var arrMask = pattern.match(/\(.*?\)(\{.*?\}|$)/g) || [''],
            arrVal = [],
            dataRe,
            i = 0;
        for (; i < arrMask.length; i++)
            arrVal[i] = arrMask[i].replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, '\\$&');
        arrVal = pattern.split((new RegExp(arrVal.join('|') || '^', 'g')));
        dataRe = el.getAttribute('data-re');
        if (!dataRe) {
            dataRe = mask.match(arrVal, arrMask);
            el.setAttribute('data-re', dataRe);
        }
        return {
            value: val || el.value,
            arrVal: arrVal,
            arrMask: arrMask,
            dataRe: dataRe
        };
    };
    mask.match = function(arrVal, arrMask) {
        var reBeg = '',
            reEnd = '',
            reErr = '.+',
            i, j, node, rng;
        if (!arrVal || !arrVal.length || !arrMask || !arrMask.length)
            return reErr;
        for (i = 0; i < arrVal.length; i++) {
            node = arrVal[i].match(/^.*?(\{|$)/);
            node = node && node.length ? node[0].replace(/\{$/, '') : '';
            if (node) {
                rng = /\{(\d+),{0,1}(\d*)\}/.exec(arrVal[i]);
                if (!rng || (rng.splice(0, 1), !rng) || !rng.length)
                    rng = ['1', ''];
                for (j = 0; j < rng[0]; j++) {
                    reBeg += node + '(';
                    if (window.parseInt(rng[0]) < j + 2)
                        reBeg += node + '{0,' + (rng[1] ? window.parseInt(rng[1]) - window.parseInt(rng[0]) : '') + '}';
                    reEnd += '|)';
                }
            }
            reBeg += arrMask[i] || '';
        }
        return reBeg === '^' ? reErr : reBeg + reEnd;
    };
    mask.clean = function(val, arrMask, dataRe) {
        var reMask = '(',
            mask = '',
            i = 0,
            tmp;
        for (; i < arrMask.length; i++)
            if (arrMask[i] && arrMask[i].match(/^[^)]*?\)/)) {
                tmp = /^\((.*)\).*$/g.exec(arrMask[i]);
                tmp = tmp.length > 1 ? tmp[1] : '';
                reMask += tmp + '|';
                mask += '(' + tmp.replace(/\\(.)/g, '$1') + ')';
            }
        reMask += ')';
        mask = mask ? mask.replace(/^\(|\)$/g, '').split(/\)\(/g) : [];
        val = val.match((new RegExp(dataRe, 'g')));
        val = val && val.length ? val.join('') : '';
        val = val.replace((new RegExp(reMask, 'g')), '');
        val = val.match((new RegExp(dataRe, 'g')));
        val = val && val.length ? val : [''];
        val = val.sort(function(a, b) {
            return b.length - a.length;
        })[0];
        return {
            value: val,
            mask: mask
        };
    };
    mask.filter = function(val, mask, arrVal, dataRe) {
        var masked = '',
            tmp,
            i;
        tmp = '(' + arrVal.join(')((') + ')';
        tmp += '|' + (arrVal.length - 2 ? '$' : '') + ')' + (new Array(arrVal.length - 1)).join('|)');
        tmp = (new RegExp(tmp)).exec(val);
        tmp = tmp ? tmp : [];
        for (i = 0; i < tmp.length; i++)
            tmp.splice(i, 1);
        for (i = 0; i < tmp.length; i++)
            if (tmp[i] === undefined || (i > 0 && tmp[i] === ''))
                tmp.splice(i--, 1);
        for (i = 0; i < tmp.length; i++)
            masked += (tmp[i] || '') + (mask[i] || '');
        masked += val.replace(tmp.join(''), '');
        val = masked.match((new RegExp(dataRe)));
        val = val && val.length ? val[0] : '';
        return val;
    };
    document.addEventListener('DOMContentLoaded', fn, false);
    if (function() {
            try {
                return window.self !== window.top;
            } catch (e) {
                return true;
            }
        }()) {
        loadScript(window.location.protocol + '//s00.static-shell.com/apps/shell-common/components/components/iframe/clientlib/external.min.js', function() {
            console.log('SHIFT: iFrameLib end load');
            document.body.style.overflow = 'hidden';
        });
        console.log("SHIFT: iFrameLib init load");
    }
    (function() {
        var fn = function() {},
            obj = {},
            arr = [
                'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error', 'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log', 'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd', 'timeline', 'timelineEnd', 'timeStamp', 'trace', 'warn'
            ];
        for (var i = 0; i < arr.length; i++)
            if (!obj[arr[i]])
                obj[arr[i]] = fn;
        window.console = window.console || obj;
    }());
}());
