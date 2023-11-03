<template>
    <div class="category-list">
        <div v-for="item in items" class="category-item">
            <img :src="item.avatar" alt="Category Avatar" class="category-avatar" />
            <div class="category-details">
                <router-link :to="{ name: 'ViewCategory', query: { categoryId: item.id } }">
                    <h3>{{ item.categoryName }}</h3>
                </router-link>
                <p>{{ item.description }}</p>
            </div>
            <router-view class="router-content"></router-view>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import { RouterLink } from 'vue-router';
export default {
    name: 'AllCategory',
    components: { RouterLink },
    data() {
        return {
            items: [],
        }
    },
    created() {
        var url = "http://localhost:5077/api/Category/GetAllCategory";
        axios.get(url).then(res => {
            this.items = res.data.data;
            console.log(this.items);
        })
    }
}
</script>

<style>
.category-list {
    position: relative;
    display: flex;
    flex-wrap: wrap;
    justify-content: space-around;
}

.category-item {
    border: 1px solid #ccc;
    border-radius: 8px;
    margin: 10px;
    padding: 15px;
    display: flex;
    align-items: center;
}

.category-item:hover {
    background-color: #a1deb4;
}

.category-avatar {
    width: 100px;
    /* 调整图像大小 */
    height: 80px;
    margin-right: 15px;
    border-radius: 50%;
    /* 使图像呈圆形 */
}


.router-content {
    position: absolute;
    top: 100%;
    left: 50%;
    transform: translateX(-50%);
    width: 800px;
    max-height: 500px;
    padding: 30px;
    background-color: #f2f1f1;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}
</style>