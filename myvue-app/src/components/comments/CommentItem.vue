<template>
    <li>
        <div>
            <span>{{ comment.authorName }}</span>：{{ comment.content }}
            <span>{{ formatCreateTime(comment.createDate) }}</span>
        </div>
        <button @click="startReply">回复</button>
        <comment-list v-if="comment.replies" :comments="comment.replies" @reply="startReply" />

    </li>
</template>

<script>
import CommentList from './CommentList.vue';
import ReplyBox from './ReplyBox.vue';
export default {
    props: {
        comment: {
            type: Object,
            required: true,
        },
    },
    components: {
        CommentList,
        ReplyBox
    },
    methods: {
        startReply() {
            // 发起回复
            this.$emit('reply', this.comment);
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
    },
}
</script>