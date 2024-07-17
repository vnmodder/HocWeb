<template>
  <div class="container-xxl py-5">
    <div class="container">
      <h3 class="text-center">ĐĂNG KÝ</h3>
      <form>
        <div class="form-group">
          <label for="UserName">User Name</label>
          <input type="text" v-model="userName" class="form-control" />
        </div>
        <div class="form-group">
          <label for="UserName">Full Name</label>
          <input type="text" v-model="fullName" class="form-control" />
        </div>
        <div class="form-group">
          <label for="Email">Email</label>
          <input
            type="email"
            class="form-control"
            placeholder="Enter email"
            v-model="email"
          />
        </div>
        <div class="form-group">
          <label for="Password">Password</label>
          <input
            type="password"
            class="form-control"
            v-model="password"
            placeholder="Password"
          />
        </div>
        <div class="form-group">
          <label for="CheckPassword">Confirm Password</label>
          <input
            type="password"
            class="form-control"
            v-model="checkPassword"
            placeholder="Password"
          />
        </div>
        <div>
          <button type="button" class="btn btn-primary" @click="handleRegister">
            Đăng ký
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import authApi from "@/api/authenticate.api";
import { useRouter } from "vue-router";

const userName = ref("");
const password = ref("");
const email = ref("");
const fullName = ref("");
const checkPassword = ref("");
const router = useRouter();

const handleRegister = async () => {
  if (userName.value && password.value && fullName.value && email.value) {
    if (password.value == checkPassword.value) {
      const response = await authApi.register(
        userName.value,
        password.value,
        fullName.value,
        email.value
      );
      if (response.status === 200) {
        alert("Đăng ký thành công!");
        router.push("/login");
      } else {
        console.log(response);
        alert(response.data.title);
      }
    } else {
      alert("2 mật khẩu không trùng nhau");
    }
  } else {
    alert("Hãy nhập đủ thông tin.");
  }
};
</script>
