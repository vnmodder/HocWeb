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
import Cookies from 'js-cookie'
import {useRouter} from 'vue-router'

const userName = ref('');
const password = ref('');
const router = useRouter();

const handleLogin = async ()=> {
  if(userName.value && password.value){
    const loginModel = {
      userName : userName.value,
      password : password.value,
    }
    const response = await auth.login(loginModel);
    console.log(response);
    if(response && response.result?.isSuccess){
      console.log(response);
      Cookies.set("token", response.result.data.token);
      router.push('/');
    }
    else{
      alert("Sai Tài Khoản hoặc mật khẩu");
    }
  }
  else{
    alert("Vui lòng nhập đủ!");
  }
  
};

</script>