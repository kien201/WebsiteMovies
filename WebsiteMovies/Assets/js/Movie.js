$(document).ready(function () {

    $('.rating').hide();
    $('.show-rating').click(function () {
        $('.rating').show();
        $('.comment').hide();
        $('.comment_content_list').hide();
    });
    $('.show-comment').click(function () {
        $('.rating').hide();
        $('.comment').show();
        $('.comment_content_list').show();
    })


    const id_movie = $('#id_movie').val();
    load_comment();
    function load_comment() {
        $.ajax({
            url: "/Movies/_load_comment",
            method: "POST",
            data: { id: id_movie },
            success: function (respone) {
                let soluong_comment = 0;
                const comment = $.map(respone, function (item) {

                    if (item.fatherComment == null && item != null) {
                        var replyToggle = "reply" + "father" + item.id;
                        soluong_comment = respone.length
                        let datecomment;
                        if (item.commentDate >= 0 && item.commentDate<60) {
                            datecomment = "Vừa xong"                           
                        }
                        if (item.commentDate >= 60 && item.commentDate < 3600) {
                            datecomment = parseInt(item.commentDate/60) + " " + "phút";
                        }
                        else if (item.commentDate >= 3600 && item.commentDate < 86400) {
                            datecomment = parseInt(item.commentDate/3600) + " " + "giờ";
                        }
                        else if (item.commentDate >= 86400 && item.commentDate < 2592000) {
                            datecomment = parseInt(item.commentDate/86400) +" "+ "ngày";
                        }
                        else if (item.commentDate >= 2592000 && item.commentDate < 31536000) {
                            datecomment = parseInt(item.commentDate/2592000)+" " + "tháng";
                        }
                        else if (item.commentDate >= 31536000) {
                            datecomment = parseInt(item.commentDate/31536000) + " " + "năm";
                        }  

                        let _fatherComment = `<div class="comment_item_father">
                                                    
                                                        <div class="left_comment_item">
                                                            <div class="avatar">
                                                                <img src="/Assets/Images/AccountImages/${item.imageAccount != null ? item.imageAccount : "default-account-image.jpg"}"alt="Ảnh đại diện" />
                                                            </div>
                                                        </div>
                                                        <div class="right_comment_item">
                                                            <div class="comment_username">${item.displayName}</div>
                                                            <div class="comment_user_content">${item.content}</div>                                                                                                                  
                                                                <div class="comment_time">${datecomment} </div>                                               
                                                        </div>
                                                 </div>`

                        return `
                           
                                <div class="comment_item"">
                
                                    ${_fatherComment}
                                    
                                  </div>
                            `
                    }
                });
                const htmls = comment.join("");
                $('.comment_list').html(htmls);
                $('.comment_count').html(`Bình luận (${soluong_comment})`);

            }
        })
    }
    $('.comment_list').load('/templates.html');

    $('.submit_commentFather').click(function () {

        if ($('#textarea_commentFather').val() == "") {
            alert("Phải nhập bình luận");
        }
        else {
            $.ajax({
                url: "/Movies/Add_commentFather",
                method: "POST",
                data: { id: id_movie, comment: $('#textarea_commentFather').val() },
                success: function (respone) {
                    load_comment();
                }
            })
            $('#textarea_commentFather').val("");
        }
        })
});

