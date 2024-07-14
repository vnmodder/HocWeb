<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid justify-content-around">
      <a class="navbar-brand">Web</a>
      <a class="nav-link" href="/">HOME</a>
      <a class="nav-link" href="/about">ABOUT</a>
      <a class="nav-link" href="#">CONTRACT</a>
      <a class="nav-link" href="#">Link 3</a>

      <form class="d-flex input-group w-auto">
        <input
          type="search"
          class="form-control rounded"
          placeholder="Search"
          aria-label="Search"
          aria-describedby="search-addon"
        />
        <span class="input-group-text border-0" id="search-addon">
          <i class="fas fa-search"></i>
        </span>
      </form>
      <form class="d-flex" v-if="user">
        <a class="btn btn-outline-dark" href="/cart">
          <i class="bi-cart-fill me-1"></i>
          Giỏ hàng
          <span class="badge bg-dark text-white ms-1 rounded-pill">{{
            items.length
          }}</span>
        </a>
      </form>
      <ul class="navbar-nav">
        <li v-if="!user" class="justify-content-around">
          <a class="btn btn-primary" href="/login">Login</a>
          <a class="btn btn" href="/login">Sign up</a>
        </li>
        <li v-else class="nav-item dropdown">
          <a
            class="nav-link dropdown-toggle"
            id="navbarDropdown"
            role="button"
            data-bs-toggle="dropdown"
            aria-expanded="false"
            >Chào bạn <strong>{{ user?.fullName }}</strong></a
          >
          <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="/user-info">Thông tin</a></li>
            <li>
              <a class="dropdown-item" href="/order">Lịch sử đặt hàng</a>
            </li>
            <li>
              <a class="dropdown-item" @click="logout" href="/login"
                >Đăng xuất</a
              >
            </li>
          </ul>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { onMounted } from "@vue/runtime-core";
import { ref } from "vue";
import { userStore } from "../stores/auth";
import { useCartStore } from "../stores/cart";
import { storeToRefs } from "pinia";
import Cookies from "js-cookie";

const authStore = userStore();
const { user } = storeToRefs(authStore);

const useCart = useCartStore();
const { items } = storeToRefs(useCart);

const logout = async () => {
  Cookies.remove("token");
  authStore.logout();
};

onMounted(() => {
  useCart.loadCart();
});
</script>
