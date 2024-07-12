<template>
    <div>
      <form @submit.prevent="onSubmit">
        <div class="mb-3">
          <label for="userName" class="form-label">Tên tài khoản</label>
          <input type="text" class="form-control" v-model="user.userName" id="userName" required />
        </div>
        <div class="mb-3">
          <label for="email" class="form-label">Email</label>
          <input type="email" class="form-control" v-model="user.email" id="email" required />
        </div>
        <div class="mb-3">
          <label for="fullName" class="form-label">Họ và tên</label>
          <input type="text" class="form-control" v-model="user.fullName" id="fullName" required />
        </div>
        <div class="mb-3" v-if="mode === 'create'">
          <label for="password" class="form-label">Mật khẩu</label>
          <input type="password" class="form-control" v-model="user.password" id="password" required />
        </div>
        <button type="submit" class="btn btn-primary">Lưu</button>
      </form>
    </div>
  </template>
  
  <script setup lang="ts">
  import { ref, watch, defineProps } from 'vue';
  import accountApi from "@/api/account.api";
  
  const props = defineProps({
    onClose: Function,
    mode: String,
    modelValue: Object as () => User | undefined
  });
  
  interface User {
    id?: number;
    userName: string;
    email: string;
    fullName: string;
    password: string;
  }
  
  const user = ref<User>({
    userName: '',
    email: '',
    fullName: '',
    password: ''
  });
  
  watch(
    () => props.modelValue,
    (newValue) => {
      if (newValue) {
        user.value = { ...newValue };
      }
    },
    { immediate: true }
  );
  
  const onSubmit = async () => {
    try {
      if (!user.value) {
        alert('Không có dữ liệu tài khoản để lưu.');
        return;
      }
  
      const formData = new FormData();
      formData.append('id', (user.value.id ?? '').toString());
      formData.append('userName', user.value.userName);
      formData.append('email', user.value.email);
      formData.append('fullName', user.value.fullName);
      formData.append('password', user.value.password);
  
      let response;
      if (props.mode === 'create') {
        response = await accountApi.addUser(formData);
      } else {
        response = await accountApi.updateAccount(formData);
      }
  
      if (response && response.data.isSuccess) {
        alert('Thành công');
        if (props.onClose) props.onClose();
      }
    } catch (error) {
      alert('lỗi');
    }
  };
  </script>
  