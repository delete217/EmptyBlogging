<template>
    <div>
        <div class="content">
            <div v-for="item of items">
                <router-link :to="{ name: 'ArticleDetails', query: { articleId: item.articleId } }">
                    <div style="color: #13a437; font-size: 20px;">{{ item.title }}</div>
                </router-link>

                <!-- 这一行显示文章作者 -->
                <div style="color: #000000">文章作者：<span style="color: #000000; margin-left: 10px">{{ item.authorName
                }}</span>
                </div>
                <!-- 这一行显示分类 -->
                <div style="color: #000000">文章标签：<span style="color: #000000; margin-left: 10px">{{ item.categoryName
                }}</span>
                </div>
                <!-- 下面一行显示发表时间，阅读数和收藏数 -->
                <div style="display: flex; justify-content: center;">
                    <div style="color: #000000;float: left">发表时间：<span style="color: #000000;margin-left: 10px">{{
                        formatCreateTime(item.createTime)
                    }}</span></div>

                    <div style="color: #000000;float: left; margin-left: 40px;">阅读量：<span
                            style="color: #000000;margin-left: 40px">{{
                                item.viewCounts }}</span></div>
                </div>
                <div style="color: #000000;max-width: 700px;margin: 0 auto;">摘要：<span style="color: #000000;">{{
                    item.summary
                }}</span></div>
                <hr style="width: 800px;">
            </div>

            <!-- 分页按钮布局 -->
            <div v-if="isshow" style="max-width: 400px;margin: 0 auto;">
                <div style="float:left; margin-right: 20px;">
                    <button type="button" class="btn btn-primary" v-on:click="getPrePage">上一页</button>
                </div>
                <div style="float:left; margin-right: 20px;">
                    <button type="button" class="btn btn-primary" v-on:click="getNextPage">下一页</button>
                </div>
                <div style="float:left; margin-right: 20px; margin-top: 5px;">
                    <span>第{{ currentPage }}/{{ countAll }}页</span>
                </div>
                <div style="float:left; margin-right: 20px; margin-top: 5px;">
                    <span>共有{{ countInfo }}条数据</span>
                </div>

            </div>
        </div>

    </div>
</template>
<script>
import axios from 'axios';
import { RouterLink } from 'vue-router';

export default {
    name: 'AllArticles',
    data() {
        return {
            showInfo: false,
            items: [],
            isshow: false,
            countInfo: 0,
            pageSize: 4,
            currentPage: 1,
            countAll: 1,
        };
    },
    created() {
        var url = "http://localhost:5077/api/Article/GetArticleVos";
        axios.get(url).then(res => {
            // 使用 console.log 输出到控制台
            console.log(res.data);
            this.items = res.data.data.slice(0, this.pageSize);
            // 一共有多少条数据(list集合 => length)
            this.countInfo = res.data.data.length;
            // 一共有需要展示几页数据
            this.countAll = Math.ceil(this.countInfo / this.pageSize);
            console.log(this.countInfo + " " + this.countAll);
            this.isshow = true;
        }).catch(error => {
            // 处理错误
            console.error(error);
        });
    },
    methods: {
        getNextPage() {
            //console.log("getNextPage");
            if (this.currentPage == this.countAll) {
                this.currentPage = this.currentPage;
            }
            else {
                this.currentPage += 1;
                //this.items = [];
                var url = "http://localhost:5077/api/Article/GetArticleVos";
                axios.get(url).then(res => {
                    this.items = res.data.data.slice(this.pageSize * (this.currentPage - 1), this.pageSize * (this.currentPage - 1) + this.pageSize);
                    //console.log(this.items);
                });
            }
        },
        getPrePage() {
            if (this.currentPage == 1) {
                this.currentPage = this.currentPage;
            }
            else {
                this.currentPage -= 1;
                var url = "http://localhost:5077/api/Article/GetArticleVos";
                axios.get(url).then(res => {
                    this.items = res.data.data.slice(this.pageSize * (this.currentPage - 1), this.pageSize * (this.currentPage - 1) + this.pageSize);
                    //console.log(this.items);
                });
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

    },
    components: { RouterLink }
}
</script>
<style>
.content {
    padding-bottom: 80px;
    /* 根据你的固定页脚的高度调整值 */
    position: relative;
    /* 添加这一行样式 */
    z-index: 0;
    /* 添加这一行样式 */
}
</style>