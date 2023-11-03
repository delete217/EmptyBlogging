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
                <p>分类：{{ item.categoryName }}</p>
                <!-- 其他文章内容展示 -->
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name: 'ViewCategory',
    props: ['categoryId'],
    data() {
        return {
            items: [],
        };
    },
    watch: {
        categoryId(newCategoryId, oldCategoryId) {
            this.fetchData(newCategoryId);
        }
    },
    created() {
        this.categoryId = this.$route.query.categoryId;
    },
    mounted() {
        this.fetchData(this.categoryId);
    },
    methods: {
        fetchData(categoryId) {
            const url = `http://localhost:5077/api/Article/GetArticlesByCategoryId?categoryId=${categoryId}`;
            axios.get(url).then(res => {
                this.items = res.data.data;
            })
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
    }
}
</script>

