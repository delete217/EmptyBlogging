<template>
    <div>
        <mavon-editor v-model="mdContext" :toolbars="toolbars"></mavon-editor>
        <button @click="openDialog">发布文章</button>

        <!-- 对话框 -->
        <div class="dialog" v-show="showDialog">
            <div class="dialog-content">
                <h2>填写文章信息</h2>
                <label for="title">文章题目:</label>
                <input type="text" id="title" v-model="articleTitle">

                <label for="summary">文字摘要:</label>
                <textarea id="summary" v-model="articleSummary"></textarea>

                <label for="tags">文章标签:</label>
                <div class="tags-container">
                    <div v-for="tag in tags" :key="tag.id" @click="toggleTag(tag)"
                        :class="{ 'selected': selectedTags.includes(tag.id) }">
                        {{ tag.tagName }}
                    </div>
                </div>

                <label for="category">文章分类:</label>
                <div class="categories-container">
                    <div v-for="category in categorys" :key="category.id" @click="toggleCategory(category)"
                        :class="{ 'selected': selectedCategories.includes(category.id) }">
                        {{ category.categoryName }}
                    </div>
                </div>

                <div class="button-group">
                    <button @click="publishArticle">发布</button>
                    <button @click="closeDialog">取消</button>
                </div>
            </div>
        </div>

        <!-- SweetAlert2 弹出框 -->
        <!-- <sweet-modal ref="sweetModal"></sweet-modal> -->
    </div>
</template>
<script>
import SweetModal from 'vue-sweetalert2';
import axios from 'axios';
import createAxiosInstance from '@/Api/Api.js';
export default {
    name: 'Test',
    // components: {
    //     SweetModal
    // },
    data() {
        return {
            authorId: localStorage.getItem("UserId"),
            mdContext: '',
            htmlContext: '',
            toolbars: {
                bold: true, // 粗体
                italic: true, // 斜体
                header: true, // 标题
                underline: true, // 下划线
                mark: true, // 标记
                superscript: true, // 上角标
                quote: true, // 引用
                ol: true, // 有序列表
                link: true, // 链接
                imagelink: true, // 图片链接
                help: true, // 帮助
                code: true, // code
                subfield: true, // 是否需要分栏
                fullscreen: true, // 全屏编辑
                readmodel: true, // 沉浸式阅读
                undo: true, // 上一步
                trash: true, // 清空
                save: true, // 保存（触发events中的save事件）
                navigation: true // 导航目录
            },
            showDialog: false,
            articleTitle: '',
            articleSummary: '',
            categorys: [],
            tags: [],
            selectedTags: [],
            selectedCategories: [],
            // 标签有多个
            selectedTagsId: [],
            // 分类只有一个
            selectedCategoryId: ''
        }
    },
    created() {
        var url = "http://localhost:5077/api/User/NeedToken";
        // axios.post(url).then(res => {
        //     console.log(res.data)
        // })

        const api = createAxiosInstance();
        api.post(url).then(res => {
            console.log(res.data)
        }
        )
    },
    methods: {
        openDialog() {
            this.showDialog = true;
            this.htmlContext = this.converter.makeHtml(this.mdContext);
            //console.log(this.htmlContext);
            // 获取所有分类名称以及标签 并绑定数据
            var url1 = "http://localhost:5077/api/Category/GetAllCategory";
            var url2 = "http://localhost:5077/api/Tag/GetAllTags";
            const api = createAxiosInstance();
            api.get(url1).then(res => {
                this.categorys = res.data.data;
                //console.log(this.categorys)
            }
            );
            api.get(url2).then(res => {
                this.tags = res.data.data;
                //console.log(this.tags);
            })
        },
        closeDialog() {
            this.showDialog = false;
        },
        publishArticle() {
            // // 在这里处理发布文章的逻辑，使用 this.articleTitle, this.articleSummary, this.articleTags, this.articleCategory
            // console.log('文章标题:', this.articleTitle);
            // console.log('文字摘要:', this.articleSummary);
            // // console.log('文章标签:', this.articleTags);
            // // console.log('文章分类:', this.articleCategory);

            // 将选择的分类Id和标签Id映射到列表中
            this.selectedCategoryId = this.selectedCategories[0];
            this.selectedTagsId = this.selectedTags;

            // console.log(this.authorId);
            // console.log(this.selectedTagsId);
            // console.log(this.selectedCategoryId);

            const api = createAxiosInstance();
            var url = "http://localhost:5077/api/Article/ReleaseArticle";
            api.post(url, {
                summary: this.articleSummary,
                title: this.articleTitle,
                //authorId: this.authorId,
                categoryId: this.selectedCategoryId,
                tagIds: this.selectedTagsId,
                mdContent: this.mdContext,
                htmlContent: this.htmlContext,
                createTime: Date.now(),
                contentHtml: this.htmlContext
            }).then(res => {
                if (res.data.code == '200') {
                    this.closeDialog();
                    alert("发布成功！")
                    // 跳转回主页
                    this.$router.push({ name: 'Nav' });
                }
                else {
                    alert("出错了！")
                }

            })
        },
        toggleTag(tag) {
            // 切换选中状态
            if (this.selectedTags.includes(tag.id)) {
                this.selectedTags = this.selectedTags.filter(id => id !== tag.id);
            } else {
                this.selectedTags.push(tag.id);
                // this.selectedTagsId.push(tag.id);
                // console.log(this.selectedTagsId);
            }
        },
        toggleCategory(category) {
            // 切换选中状态
            if (this.selectedCategories.includes(category.id)) {
                this.selectedCategories = [];
            } else {
                // 选择新的分类，清除之前选中的分类
                this.selectedCategories = [category.id];
                // // 保存选中的分类Id
                // this.selectedCategoryId = category.id;
                // console.log(this.selectedCategoryId);
            }
        }
    }
}
</script>

<style>
/* 添加一些样式来美化对话框 */
.dialog {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 2000;
}

.dialog-content {
    background-color: #cdc8c8;
    padding: 20px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    border-radius: 5px;
    width: 400px;

}

.dialog h2 {
    margin-bottom: 20px;
    font-size: 1.5em;
    color: #333;
}

.dialog label {
    display: block;
    margin-bottom: 5px;
    font-size: 0.9em;
    color: #555;
}

.dialog input,
.dialog textarea {
    width: 100%;
    padding: 4px;
    margin-bottom: 15px;
    border: 1px solid #ccc;
    border-radius: 3px;
    font-size: 1em;
}

.button-group {
    text-align: right;
}

.dialog button {
    background-color: #007BFF;
    color: #fff;
    padding: 10px;
    border: none;
    cursor: pointer;
    margin-right: 10px;
    border-radius: 3px;
    font-size: 1em;
}

.dialog button:hover {
    background-color: #0056b3;
}

/* 样式用于显示选中效果 */
.selected {
    background-color: #007BFF;
    color: #fff;
}

.tags-container,
.categories-container {
    display: flex;
    flex-wrap: wrap;
}

.tags-container div,
.categories-container div {
    cursor: pointer;
    margin: 5px;
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 5px;
}

.tags-container div:hover,
.categories-container div:hover {
    background-color: #f0f0f0;
}
</style>