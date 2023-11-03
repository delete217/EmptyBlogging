<template>
    <div>
        <nav class="navbar">

            <router-link to="/Home" class="router-link">主页</router-link>
            <router-link to="/AllTags" class="router-link">标签</router-link>
            <router-link to="/Navi" class="router-link">Nav</router-link>
            <router-link to="/Home" class="router-link">主页</router-link>
            <router-link to="/Home" class="router-link">主页</router-link>
            <router-link to="/Home" class="router-link">主页</router-link>
            <router-link to="/Home" class="router-link">主页</router-link>
            <!-- 其他导航链接 -->

            <!-- <div class="user-info">
                <img src="static/img/touxiang.jpg">
                <span>{{ username }}</span>
            </div> -->

            <div class="user-info-container">
                <div class="user-info">
                    <img src="static/img/touxiang.jpg" alt="User Avatar">
                    <span>{{ username }}</span>
                    <div class="buttons">
                        <a href="#" class="button" @click="logout">退出</a>
                    </div>
                </div>
            </div>


        </nav>
        <div class="content">
            <div v-for="item of items">
                <router-link :to="{ name: 'ArticleHeader', query: { articleId: item.articleId } }">
                    <div style="color: #0000cc">{{ item.title }}</div>
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
                        item.createTime
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
        <copyRight></copyRight>

    </div>
</template>

<script>
import axios from 'axios';
import router from '../router';
import { RouterLink } from 'vue-router';
import CopyRight from './CopyRight.vue';

export default {
    name: 'Home',
    data() {
        return {
            showInfo: false,
            items: [],
            isshow: false,
            countInfo: 0,
            pageSize: 5,
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
        logout() {
            this.$router.push({ name: 'Index' });
        },
    },
    components: { router, RouterLink, CopyRight }
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

.navbar {
    display: flex;
    justify-content: space-around;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    padding: 5px;
}

.router-link {
    text-decoration: none;
    color: #333;
    font-size: 16px;
    padding: 2px 4px;
    border-radius: 10px;
    transition: background-color 0.3s ease;

    &:hover {
        background-color: #cf1111;
    }
}

.user-info-container {
    position: relative;
}

.user-info {
    position: relative;
    display: flex;
    align-items: center;
}

.user-info img {
    width: 30px;
    height: 30px;
    border-radius: 50%;
    margin-left: 10px;
    cursor: pointer;
    /* 添加鼠标指针样式 */
}

.user-info span {
    margin-left: 5px;
}
</style>