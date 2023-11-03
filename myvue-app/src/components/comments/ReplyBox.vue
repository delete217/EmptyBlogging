<template>
    <div>
        <comment-list :comments="comments" @reply="startReply" />
        <comment-form @submit-comment="submitComment" />

        <!-- 简单回复框 -->
        <div v-if="showReplyBox" class="reply-box">
            <textarea v-model="replyContent" placeholder="输入回复内容"></textarea>
            <button @click="submitReply">提交回复</button>
            <button @click="cancelReply">取消</button>
        </div>
    </div>
</template>
  
<script>
import CommentList from '@/components/comments/CommentList.vue';
import CommentForm from '@/components//comments/CommentForm.vue';

export default {
    data() {
        return {
            comments: [], // 从后端获取的评论数据
            showReplyBox: false, // 控制是否显示回复框
            replyToComment: null, // 存储要回复的评论对象
            replyContent: '', // 存储回复的内容
        };
    },
    methods: {
        startReply(comment) {
            // 设置要回复的评论对象，显示回复框
            this.replyToComment = comment;
            this.showReplyBox = true;
        },
        submitReply() {
            // 处理回复内容，可以发送到后端保存，并更新评论列表
            // this.replyContent 包含回复的内容
            if (this.replyContent.trim() !== '') {
                this.comments.push({
                    authorName: '当前用户', // 替换为实际的当前用户信息
                    content: this.replyContent,
                    createTime: Date.now(),
                });
            }

            // 关闭回复框
            this.showReplyBox = false;
            this.replyToComment = null;
            this.replyContent = ''; // 清空回复内容
        },
        cancelReply() {
            // 取消回复，关闭回复框
            this.showReplyBox = false;
            this.replyToComment = null;
            this.replyContent = ''; // 清空回复内容
        },
        submitComment(newComment) {
            // 处理提交的新评论
            // 可以发送到后端保存，并更新评论列表
            this.comments.push(newComment);
        },
    },
    components: {
        CommentList,
        CommentForm,
    },
};
</script>
  
<style>
.reply-box {
    margin-top: 10px;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
}

.reply-box textarea {
    width: 100%;
    margin-bottom: 10px;
}
</style>
  