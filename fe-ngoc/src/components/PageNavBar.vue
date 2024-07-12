<template>
 <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid">
        <a class="navbar-brand" href="/">Web</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                <li class="nav-item">
                    <a class="nav-link" href="#">Liên hệ</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Giới thiệu</a>
                </li>
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button"
                        data-bs-toggle="dropdown" aria-expanded="false">
                        Dropdown
                    </a>
                    <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <li v-for="item in categories" :key="item.id">
                            <a :href="'/product?categoryId=' + item.id" class="dropdown-item">{{ item.nameVN }}</a>
                        </li>
                    </ul>
                </li>
            </ul>
            <form class="d-flex me-3">
                <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                <button class="btn btn-outline-success" type="submit">Search</button>
            </form>
            <div class="d-flex align-items-center">
                <div class="position-relative me-3">
                    <img src="https://cdn-icons-png.flaticon.com/512/1170/1170678.png" alt="Cart Icon" class="img-fluid"
                        style="width: 30px;">
                    <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
                        0
                    </span>
                </div>
                <ul class="navbar-nav">
                    <li v-if="!user" class="nav-item">
                        <a href="/login" class="btn btn-primary">Đăng Nhập</a>
                    </li>
                    <li v-else class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button"
                            data-bs-toggle="dropdown" aria-expanded="false">
                            Chào bạn <strong>{{ user?.userName }}</strong>
                        </a>
                        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                            <li><a class="dropdown-item" href="/user-info">Thông tin</a></li>
                            <li><a class="dropdown-item" href="/order">Lịch sử đặt hàng</a></li>
                            <li><a class="dropdown-item" @click="logout" href="/">Đăng xuất</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</nav>

</template>

<script setup lang='ts'>
import { ref } from 'vue';
import homeApi from '@/api/home.api';
import { storeToRefs } from 'pinia';
import { userStore } from '@/stores/auth';
import Cookies from 'js-cookie';
import router from '@/router';

const categories = ref<any>([]);
const authStore = userStore();
const {user} = storeToRefs(authStore);//hoi thay thang nay nua

const fetchData = async () => {
  const response = await homeApi.getAllCategory();
  // console.log(response);
  if(response && response.data.result.isSuccess){
    categories.value = response.data.result.data;
  }
  else{
    alert(response?.data.result.message ?? "Lỗi");
  }
};// hoi thay dong 21
const logout = async () =>{
  Cookies.remove("token");
  authStore.logout();
}
console.log(user.value);
fetchData(); 
</script>