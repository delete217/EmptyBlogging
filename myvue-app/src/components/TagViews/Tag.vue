<template>
    <div>
        <div v-for="item in items">
            <div>
                <router-link :to="{ name: 'ArticleDetails', query: { articleId: item.articleId } }">
                    <div style="color: #13a437; font-size: 20px;">
                        <h2>{{ item.title }}</h2>
                    </div>
                </router-link>
                <p>作者：{{ item.authorName }}</p>
                <p>创建时间：{{ formatCreateTime(item.createTime) }}</p>
                <!-- 其他文章内容展示 -->
            </div>
        </div>
        <router-view></router-view>
    </div>
</template>
<script>
import axios from 'axios';
import router from '../../router';
export default {
    name: 'ViewTag',
    props: ['tagId'],
    data() {
        return {
            items: [],
        };
    },
    watch: {
        tagId(newTagId, oldTagId) {
            // 在 tagId 变化时执行的操作
            this.fetchData(newTagId);
        },
    },
    created() {
        this.tagId = this.$route.query.tagId;
    },
    mounted() {
        // 在 mounted 钩子中调用接口，此时 $route 已经初始化完成
        this.fetchData(this.tagId);
    },
    methods: {
        fetchData(tagId) {
            const url = `http://localhost:5077/api/Article/GetArticlesByTagId?tagId=${tagId}`;
            axios.get(url).then((res) => {
                this.items = res.data.data;
                //console.log(this.items);
            });
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
    components: { router }
}
</script>

<style></style>