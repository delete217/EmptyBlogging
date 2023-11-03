<template>
    <div class="tag">
        <div v-for="item in items" class="tag-item">
            <router-link :to="{ name: 'ViewTag', query: { tagId: item.id } }">
                <div class="tag-name">{{ item.tagName }}</div>
            </router-link>
        </div>

        <router-view class="router-content"></router-view>
    </div>
</template>

<script>
import axios from 'axios';
import { RouterLink } from 'vue-router';
export default {
    name: "AllTags",
    components: { RouterLink },
    data() {
        return {
            items: []
        }
    },
    created() {
        var url = "http://localhost:5077/api/Tag/GetAllTags";
        axios.get(url).then(res => {
            this.items = res.data.data;
            console.log(this.items[0].tagName)
        })
    },
}
</script>
<style>
.tag {
    position: relative;
    display: flex;
    flex-wrap: wrap;
    justify-content: space-around;
}

.tag-item {
    margin: 10px;
    padding: 15px;
    border: 1px solid #ccc;
    border-radius: 8px;
    background-color: #f9f9f9;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    text-align: center;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.tag-item:hover {
    background-color: #a1deb4;
}

.tag-name {
    color: #333;
    font-size: 18px;
    font-weight: bold;
}

.router-content {
    position: absolute;
    top: 100%;
    left: 50%;
    transform: translateX(-50%);
    width: 800px;
    max-height: 500px;
    padding: 20px;
    background-color: #f2f1f1;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}
</style>