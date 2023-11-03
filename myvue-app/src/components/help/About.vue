<template>
    <div class="about-author">
        <h1>关于作者</h1>
        <div class="author-info">
            <img src="static/img/logo.jpg" alt="Author Avatar" class="avatar">
            <p>
                欢迎来到我的个人空间！我是 delete217 ,本站的创始人和作者。
                在这里，我分享了我的一些想法、技术文章和项目。感谢你的访问！
            </p>
        </div>
        <div class="contact-info">
            <h2>联系我</h2>
            <p>Email: duan314100@gmail.com</p>
            <p>GitHub: <a href="https://github.com/delete217" target="_blank">github.com/delete217</a></p>
            <!-- 添加其他联系方式，比如社交媒体等 -->
        </div>
    </div>
</template>
  
<script>
export default {
    name: 'AboutAuthor',
    // 添加其他需要的配置
};
</script>
  
<style scoped>
.about-author {
    max-width: 800px;
    margin: auto;
    padding: 20px;
}

.author-info {
    display: flex;
    align-items: center;
    margin-bottom: 20px;
}

.avatar {
    width: 80px;
    height: 80px;
    border-radius: 50%;
    margin-right: 20px;
}

.contact-info {
    margin-top: 20px;
}

/* 添加其他样式根据需要 */
</style>
  