<template>
    <div>
        <ul>
            <comment-item v-for="comment in comments" :key="comment.commentId" :comment="comment"
                @reply="startReply(comment)">
            </comment-item>
        </ul>
    </div>
</template>

<script>
import CommentItem from './CommentItem.vue';

export default {
    props: {
        comments: {
            type: Array,
            required: true,
        },
    },
    components: {
        CommentItem,
    },
    methods: {
        startReply(comment) {
            // 发起回复，可以在父组件中处理回复逻辑
            this.$emit('reply', comment);

        },
    },
};
</script>

