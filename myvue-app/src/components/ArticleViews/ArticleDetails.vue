<template>
    <div class="article-details">
        <h2 class="title">{{ item.title }}</h2>
        <div class="metadata">
            <span class="author">Author: {{ item.author }}</span>
            <span class="create-time">Create Time: {{ formatCreateTime(item.createTime) }}</span>
            <span class="view-counts">Views: {{ item.viewCounts }}</span>
        </div>
        <div class="content">
            <p class="summary">{{ item.summary }}</p>
            <div class="full-content" v-html="item.content"></div>
        </div>

        <!-- 分割线 -->
        <hr class="divider">

        <!-- 评论部分 -->
        <div class="comment-section">
            <div class="comment-intro">友好的评论区～</div>
            <div class="comment-form">
                <textarea v-model="ftComment" placeholder="在这里发表你的评论..." class="comment-box"></textarea>
                <button class="post-comment-button" @click="sumbitComment">发表评论</button>
            </div>

            <div v-for="(comment, index) in displayedComments" :key="index" class="comment-container">
                <!-- 评论作者和内容 -->
                <div class="comment" @click="displayReplyBox(comment)" @mouseenter="comment.showReplyText = true"
                    @mouseleave="comment.showReplyText = false">
                    <span class="author">{{ comment.authorName }}：</span>
                    <span class="content">{{ comment.content }}</span>
                    <div v-if="comment.showReplyText" class="reply-text">回复</div>
                </div>

                <div v-if="comment.showReplyBox" class="reply-box">
                    <!-- 这里是回复框的内容 -->
                    <textarea v-model="replyContent" placeholder="在这里回复..."></textarea>
                    <button @click="submitReply(comment)">提交回复</button>
                </div>
                <!-- 子评论 -->
                <div v-for="cdComment in comment.childComments" class="child-comment">
                    <span class="author">{{ cdComment.authorName }}：</span>
                    <span class="content">{{ cdComment.content }}</span>
                </div>
            </div>
            <!-- 展开按钮 -->
            <button class="expand-button" @click="showAllComments">展开 or 折叠</button>

        </div>
    </div>
</template>

<script>
import axios from 'axios';
import CommentList from '../comments/CommentList.vue';
import CommentForm from '../comments/CommentForm.vue';
import ReplyBox from '../comments/ReplyBox.vue';
import createAxiosInstance from '@/Api/Api.js';

export default {
    name: "ArticleDetails",
    data() {
        return {
            item: null,
            articleId: null,
            backendComments: [],
            shortComments: [],
            totalComments: [],
            displayedComments: [],
            comments: [],
            showAll: false,
            showReplyText: false,
            showReplyBox: false,
            replyContent: '',
            ftComment: ''
        }
    },
    created() {
        this.articleId = this.$route.query.articleId;
        var url = `http://localhost:5077/api/Article/View/?id=${this.articleId}`;
        axios.get(url).then(res => {
            this.item = res.data.data;
            //console.log(this.item)
            this.backendComments = this.item.comments;
            this.comments = this.backendComments.map(comment => {
                return {
                    ...comment,
                    // 添加新属性，比如 showReplyText
                    showReplyText: false,
                    showReplyBox: false
                }
            });

            this.shortComments = this.comments.slice(0, 3);
            this.totalComments = this.comments;
            this.displayedComments = this.shortComments;
            var token = localStorage.getItem("Token");
            console.log(this.totalComments);
            //console.log(this.item);
            //console.log(this.backendComments)
        })
    },
    components: {
        CommentList,
        CommentForm,
        ReplyBox
    },
    methods: {
        showAllComments() {
            // 切换 showAll 的值，触发 watch 更新 displayedComments
            this.showAll = !this.showAll;
            if (this.showAll) {
                this.displayedComments = this.totalComments;
            } else {
                this.displayedComments = this.shortComments;
            }
        },
        formatCreateTime(createTime) {
            const date = new Date(Number(createTime));
            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, '0');
            const day = String(date.getDate()).padStart(2, '0');
            const hours = String(date.getHours()).padStart(2, '0');
            const minutes = String(date.getMinutes()).padStart(2, '0');
            return `${year}-${month}-${day} ${hours}:${minutes}`;
        },
        displayReplyBox(comment) {
            comment.showReplyBox = !comment.showReplyBox;
        },
        submitReply(comment) {
            // 处理回复提交逻辑，例如发送到服务器
            const contentStr = this.replyContent;
            console.log(comment.id)
            const api = createAxiosInstance();
            var url = "http://localhost:5077/api/Comment/AddComments";
            api.post(url, {
                "content": contentStr,
                "articleId": this.articleId,
                "parentId": comment.id,
                "lever": "1"
            }).then(res => {
                if (res.data.code != 200) {
                    // console.log()
                    alert("评论前请先登录！");
                } else {
                    // 数据绑定 实时更新
                    comment.childComments.push(res.data.data)
                }
                //console.log(this.totalComments)
            })
            // 清空回复框内容
            this.replyContent = '';
            // 隐藏回复框
            comment.showReplyBox = false;
        },
        sumbitComment() {
            const contentStr = this.ftComment;
            const api = createAxiosInstance();
            var url = "http://localhost:5077/api/Comment/AddComments";
            api.post(url, {
                "content": contentStr,
                "articleId": this.articleId,
                "parentId": 0,
                "lever": "1"
            }).then(res => {
                if (res.data.code != 200) {
                    // console.log()
                    alert("评论前请先登录！");
                } else {
                    this.totalComments.push(res.data.data)
                }
                console.log(res.data)

                // console.log("===============");
                // console.log(this.displayedComments)
            })

            this.ftComment = '';

            // alert("回复成功");
        }
    },
}
</script>
<style>
.article-details {
    max-width: 800px;
    margin: 0 auto;
    padding-bottom: 80px;
    /* 根据你的固定页脚的高度调整值 */
    position: relative;
    /* 添加这一行样式 */
    z-index: 0;
    /* 添加这一行样式 */
}

.title {
    font-size: 24px;
    margin-bottom: 10px;
}

.metadata {
    font-size: 14px;
    color: #555;
    margin-bottom: 20px;
}

.metadata>span {
    margin-right: 10px;
}

.content {
    line-height: 1.6;
}

.summary {
    font-weight: bold;
    margin-bottom: 15px;
    color: #333;
    font-size: 16px;
}

.full-content {
    margin-top: 20px;
}

.full-content p {
    margin-bottom: 15px;
}

.full-content img {
    max-width: 100%;
}

.divider {
    height: 2px;
    background-color: #ddd;
    margin: 20px 0;
    /* 调整分割线上下的间距 */
}

.comment {
    position: relative;
    /* 为了定位回复文字 */
}

/* .reply-text {
    position: absolute;
    top: 0;
    right: 0;
    background-color: #e0e0e0;
    padding: 2px 6px;
    border-radius: 5px;
}

.reply-box {
    margin-top: 10px;
} */

.reply-box {
    margin-top: 5px;
    padding: 5px;
    border: 1px solid #ccc;
    border-radius: 8px;
    background-color: #f9f9f9;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    /* 添加阴影效果 */
}

.reply-box textarea {
    width: 100%;
    padding: 5px;
    margin-bottom: 0px;
    border: 1px solid #ddd;
    border-radius: 5px;
}

.reply-box button {
    background-color: #4caf50;
    color: white;
    padding: 5px 5px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

.reply-box button:hover {
    background-color: #45a049;
}

.comment-section {
    padding: 20px;
    background-color: #f8f8f8;
    border-radius: 8px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.comment-intro {
    font-size: 18px;
    margin-bottom: 15px;
    color: #333;
    text-align: left;
}

/* 评论表单容器 */
.comment-form {
    display: flex;
    align-items: flex-start;
    /* 垂直居上对齐 */
}

/* 评论框样式 */
.comment-box {
    flex: 1;
    /* 填充剩余空间 */
    padding: 10px;
    margin-right: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
}

/* 发表评论按钮样式 */
.post-comment-button {
    background-color: #007bff;
    color: #fff;
    border: none;
    border-radius: 5px;
    padding: 10px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.post-comment-button:hover {
    background-color: #0056b3;
}

.comment-container {
    margin-top: 5px;
    margin-bottom: 15px;
}

.comment {
    color: #000000;
    margin-left: 10px;
    background-color: #d2acac;
    /* 父评论背景色，根据需要调整 */
    padding: 5px;
    /* 添加一些内边距，使评论更清晰 */
    border-radius: 5px;
    text-align: left;
    /* 文字左对齐 */
    /* 圆角，使评论更美观 */
}

.child-comment {
    color: #000000;
    margin-left: 40px;
    /* 缩进子评论，根据需要调整 */
    background-color: #b1a6a6;
    /* 子评论背景色，根据需要调整 */
    padding: 5px;
    /* 添加一些内边距，使子评论更清晰 */
    border-radius: 5px;
    text-align: left;
    /* 文字左对齐 */
    /* 圆角，使子评论更美观 */
}

.expand-button {
    margin-top: 10px;
    padding: 8px 12px;
    font-size: 14px;
    background-color: #007bff;
    color: #fff;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.expand-button:hover {
    background-color: #0056b3;
}
</style>