$(document).ready(function(){
$('[data-toggle="collapse"]').on('click', function() {
    console.log("du mâmy");
    var $this = $(this),
            $parent = typeof $this.data('parent')!== 'undefined' ? $($this.data('parent')) : undefined;
    if($parent === undefined) { /* Just toggle my  */
        $this.find('.glyphicon').toggleClass('glyphicon-plus glyphicon-minus');
        return true;
    }

    /* Open element will be close if parent !== undefined */
    var currentIcon = $this.find('.glyphicon');
    currentIcon.toggleClass('glyphicon-plus glyphicon-minus');
    $parent.find('.glyphicon').not(currentIcon).removeClass('glyphicon-minus').addClass('glyphicon-plus');

    });


    const id_movie = $('#id_movie').val();
    load_comment();
    function load_comment() {
        $.ajax({
            url: "/Home/_load_comment",
            method: "POST",
            data: { id: id_movie },
            success: function (respone) {
                    let soluong_comment = 0;
               // console.log(respone)
                const comment = $.map(respone, function (item) {

                    //console.log(item);
                    if (item.fatherComment == null && item != null) {
                        var replyToggle = "reply" + "father" + item.id;
                        
                        soluong_comment = respone.length
                      //  console.log(soluong_comment)
                        let _fatherComment = `<div class="comment_item_father">
                                                    
                                                        <div class="left_comment_item">
                                                            <div class="avatar">
                                                                <img src="~/Assets/img/5746.jpg" />
                                                            </div>
                                                        </div>
                                                        <div class="right_comment_item">
                                                            <div class="comment_username">Dũng Trần</div>
                                                            <div class="comment_user_content">${item.content}</div>
                                                            <div class="comment_rep-time">
                                                                <div class="comment_rep">
                                                                    <span>
                                                                        <a class="" role="button" data-toggle="collapse" href="#${replyToggle}"
                                                                           aria-expanded="false" aria-controls="collapseExample">Trả lời</a>
                                                                    </span>
                                                                </div>
                                                            
                                                                <div class="comment_time">${item.commentDate}</div>
                                                            </div>
                                                            <div class="collapse" id="${replyToggle}">
                                                                            <input type="hidden" name="id_commentFather" id="id_commentFather" value="${item.id}" />
                                                                <form action="/Home" class="form_rep" method="post">
                                                                    <div class="form-group">
                                                                        <label for="comment">Bình luận</label>
                                                                        <textarea name="comment" class="form-control"id="textarea_commentChild" rows="3"></textarea>
                                                                    </div>
                                                                    <input type="button" class="btn btn-default submit_commentChild" value="Gửi" />
                                                                    
                                                                </form>
                                                            </div>

                                                        </div>
                                                 </div>`
                        let child_comment = $.map(respone, function (item2) {

                            if (item2.fatherComment == item.id) {
                              //  console.log(item2);

                                let _child_comment = `<div class="comment_item_child">
                            <div class="left_comment_item">
                                <div class="avatar">
                                    <img src="5746.jpg" />

                                </div>
                            </div>
                            <div class="right_comment_item">
                                <div class="comment_username">Dũng Trần</div>
                                <div class="comment_user_content">${item2.content}</div>
                                <div class="comment_rep-time">
                                    <div class="comment_rep">
                                    </div>
                                    <div class="comment_time">${item2.commentDate}</div>
                                </div>
                            </div>
                        </div>`

                                _fatherComment = _fatherComment + _child_comment
                            }
                        })
                        return `
                           
                                <div class="comment_item"">
                
                                    ${_fatherComment}
                                    
                                  </div>
                            `
                    }
                });
                const htmls = comment.join("");
                $('.comment_list').html(htmls);
                console.log(soluong_comment)
                $('.comment_count').html(`Bình luận (${soluong_comment})`);
                
            }
        })
    }
    $('.comment_list').load('/templates.html');
   
    $('.submit_commentFather').click(function () {
            $.ajax({
                url: "/Home/Add_commentFather",
                method: "POST",
                data: { id: id_movie, comment: $('#textarea_commentFather').val() },
                success: function (respone) {
                    console.log(respone)
                    load_comment();
                }
        })
        $('#textarea_commentFather').val("");
        $('#textarea_commentChild').val("");
    })
    const id_commentFather = $('#id_commentFather').val();

    $('.submit_commentChild').click(function () {
        
        
        $.ajax({
            url: "/Home/Add_commentChild",
            method: "POST",
            data: { id: id_movie, id_commentFather: id_commentFather, comment: $('#textarea_commentChild').val() },
            success: function (respone) {
                console.log(respone)
                load_comment();
            }
        })
        $('#textarea_commentFather').val("");
        $('#textarea_commentChild').val("");
    })
});

