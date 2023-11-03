<template>
    <div>
        <div class="user-profile">
            <img v-if="user.avatar" :src="user.avatar" alt="User Avatar" class="avatar">
            <div class="user-details">
                <div class="user-info">
                    <span class="label">昵称：</span>
                    <span class="value">{{ user.nickname || '未设置' }}</span>
                </div>
                <div class="user-info">
                    <span class="label">账号：</span>
                    <span class="value">{{ user.account || '未设置' }}</span>
                </div>
                <div class="user-info">
                    <span class="label">邮箱：</span>
                    <span class="value">{{ user.email || '未设置' }}</span>
                </div>
                <div class="user-info">
                    <span class="label">手机号：</span>
                    <span class="value">{{ user.mobilePhoneNumber || '未设置' }}</span>
                </div>
                <div class="user-info">
                    <span class="label">注册时间：</span>
                    <span class="value">{{ formatTimestamp(user.createDate) }}</span>
                </div>
                <div class="user-info">
                    <span class="label">最后登录时间：</span>
                    <span class="value">{{ formatTimestamp(user.lastLogin) }}</span>
                </div>
                <div class="user-info">
                    <span class="label">管理员：</span>
                    <span class="value">{{ user.admin ? '是' : '否' }}</span>
                </div>
                <div class="user-info">
                    <span class="label">状态：</span>
                    <span class="value">{{ user.status || '未设置' }}</span>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import createAxiosInstance from '@/Api/Api.js';
export default {
    data() {
        return {
            user: [],
        }
    },
    created() {
        const api = createAxiosInstance();
        var url = "http://localhost:5077/api/User/GetUserInfo";
        api.get(url).then(res => {
            if (res.data.code != 200) {
                this.$router.push({ name: 'Index' })
            } else {
                console.log(res.data);
                this.user = res.data.data
            }

        })
    },
    methods: {
        formatTimestamp(timestamp) {
            if (!timestamp) return '未设置';
            const date = new Date(timestamp);
            return `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date
                .getDate()
                .toString()
                .padStart(2, '0')} ${date.getHours()}:${date.getMinutes()}`;
        },
    }
}
</script>
<style>
.user-profile {
    display: flex;
    align-items: center;
    margin-top: 40px;
}

.avatar {
    width: 100px;
    height: 100px;
    border-radius: 50%;
    margin-right: 20px;
}

.user-details {
    font-size: 16px;
}

.user-info {
    margin-bottom: 10px;
}

.label {
    font-weight: bold;
    margin-right: 5px;
}

.value {
    color: #333;
}
</style>