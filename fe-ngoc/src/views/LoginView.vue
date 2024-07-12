<template>
    <div class="container vh-100 d-flex justify-content-center align-items-center">
      <div class="card p-4 shadow-sm" style="max-width: 400px; width: 100%;">
        <h2 class="text-center mb-4">Đăng Nhập</h2>
        <form>
          <div class="mb-3">
            <label for="text" class="form-label">UserName</label>
            <input type="text" class="form-control" id="UserName" 
              placeholder="Tên người dùng" v-model = "userName">
          </div>
          <div class="mb-3">
            <label for="password" class="form-label">Mật Khẩu</label>
            <input type="password" class="form-control" id="password"
              placeholder="Mật khẩu" v-model="password">
          </div>
          <div class="d-grid">
            <button type="button" class="btn btn-primary" @click="handleLogin">Đăng Nhập</button> 
          </div>
          <div class="d-flex justify-content-between mt-3">
            <a href="#" class="btn btn-link">Quên Mật Khẩu?</a>
            <a href="#" class="btn btn-link">Đăng Ký</a> 
          </div>
        </form>
      </div>
    </div>
</template>

<script setup lang = "ts">
import auth from '@/api/authenticate.api'
import { ref } from 'vue';
import Cookies from 'js-cookie';
import router from '@/router';
import {userStore} from "../stores/auth"

const userName = ref<any>("");
const password = ref<any>("");
const user = userStore(); // hoi thay thang nay.

const handleLogin = async () => {
  if(userName.value && password.value){
    const LoginModel = {
      userName: userName.value,
      password: password.value,
    }
    
    const response = await auth.login(LoginModel);
    console.log(response);
    if(response && response.result?.isSuccess){
      user.login({
        userId: response.result.data.userId,
        username: response.result.data.userName,
        email: response.result.data.email,
        fullName: response.result.data.fullname,
      })
      Cookies.set("token", response.result.data.token);
      router.push("/");
    }
    else{
      alert("Sai tài khoản hoặc mật khẩu!");
    }
  }
  else{
    alert("Vui lòng nhập đầy đủ thông tin!");
  }
}

</script>