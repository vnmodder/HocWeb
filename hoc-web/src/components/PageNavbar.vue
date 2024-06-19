<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container px-4 px-lg-5">
                <a class="navbar-brand" href="/">
                  <img src="/src/assets/logo.png">
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation"><span class="navbar-toggler-icon"></span></button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0 ms-lg-4">
                        <li class="nav-item"><a class="nav-link active" aria-current="page" href="/">Trang chủ</a></li>
                        <li class="nav-item"><a class="nav-link" href="/about">Giới thiệu</a></li>
                        <li class="nav-item"><a class="nav-link" href="/contact">Liên hệ</a></li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" id="navbarDropdown" role="button"  data-bs-toggle="dropdown" aria-expanded="false">Danh Mục</a>
                            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <li v-for="item in items" :key="item.id"><a class="dropdown-item" :href="'/product?categoryId='+ item.id">{{ item.nameVN }}</a></li>
                            </ul>
                        </li>
                    </ul>
                    <form class="d-flex">
                        <button class="btn btn-outline-dark" type="submit">
                            <i class="bi-cart-fill me-1"></i>
                            Giỏ hàng
                            <span class="badge bg-dark text-white ms-1 rounded-pill">0</span>
                        </button>
                    </form>
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0 ms-lg-4">
                        <li class="nav-item"><a class="nav-link" href="/login">Đăng Nhập</a></li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">Chào bạn</a>
                            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <li ><a class="dropdown-item" href="#!">Thông tin</a></li>
                                <li ><a class="dropdown-item" href="#!">Đăng xuất</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
</template>
<script setup lang='ts'>
import navApi from '@/api/nav.api'
import { ref } from 'vue'

const items = ref<any>([]);

const fetchData = async () => {
  const response = await navApi.getAllCategory();
  if (response && response.data.result.isSuccess) {
    items.value = response.data.result.data
  }
  else {
    items.value = []
    alert(response.data.result.message)
  }
}


fetchData();

</script>
