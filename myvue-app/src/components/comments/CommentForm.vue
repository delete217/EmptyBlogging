<template>
    <div>
        <h1>发表评论</h1>
        <form @submit.prevent="submitForm">
            <label for="authorName">作者名：</label>
            <input v-model="authorName" type="text" id="authorName" required />

            <label for="content">评论内容：</label>
            <textarea v-model="content" id="content" required></textarea>

            <button type="submit">提交评论</button>
        </form>
    </div>
</template>

<script>
export default {
    data() {
        return {
            authorName: '',
            content: '',
        };
    },
    methods: {
        submitForm() {
            // 提交评论，可以在父组件中处理提交逻辑
            this.$emit('submit-comment', {
                authorName: this.authorName,
                content: this.content,
                createTime: Date.now()
            });

            // 清空表单
            this.authorName = '';
            this.content = '';
        },
    },
};
</script>