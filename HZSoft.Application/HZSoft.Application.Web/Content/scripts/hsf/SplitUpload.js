layui.use(['form', 'upload', 'element'], function () {
    var form = layui.form;
    var upload = layui.upload;
    var element = layui.element;
    var $ = layui.$;
    upload.render({
        elem: '#fileUpload',
        url: '/CustomerManage/DZ_Order/RecoveryKPJD?folderId=d569d19f-b9d6-46e5-a803-24364262be43', //处理上传文件接口
        accept: 'file',
        auto: false,
        acceptMime: '.exe,.zip,.rar,.gz',//允许上传的文件类型
        choose: function (obj) {
            element.progress('uploadProgress', '0%');
            $('.mask').show();
            $('.loading').show();
            var data = this.data;
            var files = obj.pushFile();
            var LENGTH = 500 * 1024; //每片文件大小
            obj.preview(function (index, file, result) {
                var totalSize = file.size;
                var totalPage = Math.ceil(totalSize / LENGTH);
                $('#totalPage').val(totalPage);
                $('#page').val('1');
                $('#status').val('1');
                var fileName = file.name;
                $("#fileUpload").parent().next().text(fileName);
                //$("#CreatePath_a").text(fileName);
                var fileExt = fileName.substr(fileName.lastIndexOf('.') + 1);
                fileName = fileName.substr(0, fileName.lastIndexOf('.'));
                var progressTimer = setInterval(function () {
                    var totalPage = parseInt($('#totalPage').val());
                    var page = parseInt($('#page').val());
                    var status = $('#status').val();
                    if (parseInt(totalPage) == parseInt(page) && (parseInt(status) == 2 || parseInt(status) == -1)) {
                        clearInterval(progressTimer);
                    } else {
                        if (status == 1) {
                            $('#status').val('0');
                            data.fileName = fileName;
                            data.page = page;
                            data.totalPage = totalPage;
                            data.fileExt = fileExt;
                            obj.upload(index, file.slice((page - 1) * LENGTH, page * LENGTH));
                        }
                    }
                }, 100);
            });
        },
        done: function (res) {
            if (res.status == 1) { //分片上传
                var page = parseInt($('#page').val());
                var totalPage = parseInt($('#totalPage').val());
                element.progress('uploadProgress', Math.ceil(page * 100 / totalPage) + '%');
                page = page + 1;
                console.log(page);
                $('#page').val(page);
                $('#status').val('1');
            } else if (res.status == 2) { //上传完成
                element.progress('uploadProgress', '100%');
                $('#status').val('2');

                $("#fileUpload").parent().next().next().val(res.downUrl);
                $("#fileUpload").parent().next().attr("href", res.downUrl);
                //$('#CreatePath').val(res.downUrl);
                //$("#CreatePath_a").attr("href", res.downUrl);

                layer.msg('上传成功', { time: 1000, anim: 0 }, function () {
                    $('.mask').hide();
                    $('.loading').hide();
                });
            } else { //上传错误
                $('#status').val('-1');
                element.progress('uploadProgress', '0%');
                console.log(!typeof (res.downUrl) == "undefined");
                if (typeof (res.downUrl) == "undefined") {
                } else {
                    //$('#downUrl').val(res.downUrl);
                }
                layer.msg("上传失败，请重试", { time: 3000, anim: 0 }, function () {
                    $('.mask').hide();
                    $('.loading').hide();
                });
            }
        },
        error: function () {
            $('.mask').hide();
            $('.loading').hide();
        }
    });
});