<template>
    <div class="login-container">
        <h2>Login</h2>
        <form @submit.prevent="login">
            <div class="form-group">
                <label for="username">Username:</label>
                <input type="text" id="username" v-model="username" required>
            </div>
            <div class="form-group">
                <label for="password">Password:</label>
                <input type="password" id="password" v-model="password" required>
            </div>
            <button type="submit">Login</button>

            <div v-if="errorMessage" class="error-message">
                {{ errorMessage }}
            </div>
        </form>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name: 'Index',
    data() {
        return {
            userId: '',
            username: '',
            password: '',
            errorMessage: '',
            token: '',
            avatar: ''
        };
    },
    methods: {
        login() {
            console.log("=====");
            var url = 'http://localhost:5077/api/Login';
            axios
                .post(url, {
                    username: this.username,
                    password: this.password,
                })
                .then((res) => {
                    console.log(res.data.code);
                    if (res.data.code == '200') {

                        let arrays = res.data.data.split(" ");
                        this.userId = arrays[0];
                        this.avatar = arrays[1];
                        this.token = arrays[2];

                        localStorage.setItem("UserId", this.userId)
                        localStorage.setItem("Token", this.token)
                        localStorage.setItem("Username", this.username)
                        localStorage.setItem("Avatar", this.avatar)
                        this.$router.push({ name: 'Nav' });
                    } else {
                        // Login failed, display error message
                        this.errorMessage =
                            'Login failed. Please check your username and password.';
                    }
                })
                .catch((error) => {
                    // Handle network errors
                    this.errorMessage = 'An error occurred. Please try again.';
                });
        },
    },
};
</script>

<style scoped>
.login-container {
    max-width: 400px;
    margin: auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

h2 {
    text-align: center;
    color: #3498db;
}

form {
    display: flex;
    flex-direction: column;
}

.form-group {
    margin-bottom: 15px;
}

label {
    font-weight: bold;
}

input {
    padding: 10px;
    width: 100%;
    box-sizing: border-box;
    margin-top: 5px;
    border: 1px solid #ccc;
    border-radius: 3px;
}

button {
    padding: 10px;
    background-color: #3498db;
    color: #fff;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s;
}

button:hover {
    background-color: #2980b9;
}

/* Add styles for the error message */
.error-message {
    color: red;
    margin-top: 10px;
    text-align: center;
}
</style>
