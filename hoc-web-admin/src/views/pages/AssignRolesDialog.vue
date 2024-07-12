<template>
    <div>
      <form @submit.prevent="onSubmit">
        <div class="mb-3">
          <label for="roles" class="form-label">Quyền</label>
          <select v-model="selectedRoles" class="form-control" multiple>
            <option v-for="role in allRoles" :key="role" :value="role">{{ role }}</option>
          </select>
        </div>
        <button type="submit" class="btn btn-primary">Lưu</button>
      </form>
    </div>
  </template>
  
  <script setup lang="ts">
  import { ref, defineProps } from 'vue';
  import accountApi from "@/api/account.api";
  
  const props = defineProps({
    onClose: Function,
    modelValue: Object
  });
  
  const allRoles = ref<string[]>(['Admin', 'User']);
  const selectedRoles = ref<string[]>([]);
  
  const onSubmit = async () => {
  try {
    if (typeof props.onClose !== 'function') {
      return;
    }
    if (!props.modelValue || !props.modelValue.id) {
      alert('Không có dữ liệu tài khoản để phân quyền.');
      return;
    }
    const response = await accountApi.assignRoles(props.modelValue.id, selectedRoles.value);  
    if (response && response.data.isSuccess) {
      alert('Phân quyền thành công');
      props.onClose();
    }
  } catch (error) {
    alert('lỗi');
  }
};

  </script>
  
  