<template>
    <div>
        <nav class="navbar">
            <!-- Logo 和名字 -->
            <div class="brand">
                <!-- <img src="static/img/logo.jpeg" alt="Logo"> -->
                <span>𝓔mpty空空
                </span>
            </div>
            <router-link to="/Navi" class="router-link">主页</router-link>
            <router-link to="/Navi/AllTags" class="router-link">标签</router-link>
            <router-link to="/Navi/AllCategory" class="router-link">分类</router-link>
            <router-link to="/Navi/Test" class="router-link">写文章</router-link>
            <router-link to="/Navi/AboutAuthor" class="router-link">关于作者</router-link>
            <!-- 搜索框 -->
            <div class="search-box">
                <input type="text" v-model="articleName" placeholder="输入文章题目...">
                <router-link :to="searchLink" class="router-link">搜索</router-link>
            </div>
            <div class="user-info-container">
                <div class="user-info">
                    <img :src="avatar" alt="User Avatar">
                    <!-- <span class="username" @click="userInfo">{{ username }}</span> -->
                    <router-link to="/Navi/UserInfo" class="router-link">{{ username }}</router-link>

                    <!-- 在你的组件模板中 -->
                    <!-- <router-link @click="navigateToUserInfo" class="router-link">{{ username }}</router-link> -->

                    <div class="buttons">
                        <a href="#" class="button" @click="logout">退出</a>
                    </div>
                </div>
            </div>

        </nav>
        <router-view></router-view>
        <div class="copyright">
            <p>&copy; 2023 Empty空空. All rights reserved. | Designed with ❤️ by delete217</p>
        </div>
    </div>
</template>
<script>

export default {
    name: 'Nav',
    data() {
        return {
            username: localStorage.getItem("Username"),
            avatar: localStorage.getItem("Avatar"),
            articleName: '',
        }
    },
    computed: {
        searchLink() {
            return {
                name: 'SearchArticle',
                query: { articleName: this.articleName }
            };
        },
    },
    methods: {
        logout() {
            localStorage.setItem("Token", null);
            localStorage.setItem("Username", "未登录");
            localStorage.setItem("Avatar", "/static/img/notlogin.jpg");
            this.$router.push({ name: 'Index' });
        },

    }
};
</script>
<style>
.navbar {
    display: flex;
    justify-content: space-evenly;
    align-items: center;
    /* 垂直居中 */
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    padding: 2px;
    /* 调整上下内边距 */
}

.router-link {
    text-decoration: none;
    color: #333;
    font-size: 16px;
    padding: 8px 12px;
    /* 调整上下内边距，左右内边距 */
    border-radius: 10px;
    transition: background-color 0.3s ease;


    &:hover {
        background-color: #cf1111;
    }
}

/* 添加样式以对齐 logo 和名字 */
.brand {
    display: flex;
    align-items: center;
    margin-right: 20px;
    /* 调整 logo 和名字 与其他导航链接的间距 */
}

.brand img {
    width: 40px;
    /* 根据需要调整 logo 的大小 */
    height: auto;
    margin-right: 10px;
    /* 调整 logo 和名字 之间的间距 */
}

.brand span {
    font-size: 18px;
    font-family: 'cursive';
    /* 选择一个手写或草书风格的字体 */
    color: #333;
    /* 字体颜色 */
    transition: color 0.3s ease;
    /* 添加颜色变化的过渡效果 */
}

.brand span:hover {
    background-color: #cf1111;
    /* 鼠标悬停时的背景颜色 */
    color: #fff;
    /* 鼠标悬停时的文字颜色 */
    border-radius: 5px;
    /* 鼠标悬停时的圆角效果 */

    /* 在上面的样式基础上添加下面的样式 */
}

.search-box {
    display: flex;
    align-items: center;
}

.search-box input {
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 4px;
    margin-right: 10px;
    outline: none;
    /* 去除输入框的轮廓边框 */
}

.search-box button {
    padding: 8px 12px;
    border: none;
    border-radius: 4px;
    background-color: #0056b3;
    color: #fff;
    cursor: pointer;
}

.search-box button:hover {
    background-color: #cf1111;
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

.username {
    color: #321bc9;
    text-decoration: underline;
    cursor: pointer;
    font-size: 18px;
    margin-right: 10px;
    /* 添加右边距，调整和退出按钮之间的间隔 */
}

.buttons {
    display: flex;
    align-items: center;
    font-size: 14px;
}

.button {
    color: #79d87d;
    border: none;
    padding: 2px 2px;
    border-radius: 5px;
    cursor: pointer;
    margin-left: 10px;

    &:hover {
        background-color: #57bb5f;
    }
}


.copyright {
    background-color: #333;
    color: #ffffff;
    text-align: center;
    padding: 0px;
    position: fixed;
    bottom: 0;
    left: 0;
    width: 100%;
}
</style>