<template>
  <div>
    <h4>Danh sách tài khoản</h4>
    <table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Tên tài khoản</th>
          <th scope="col">Email</th>
          <th scope="col">Ngày tạo</th>
          <th scope="col">Trạng thái</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(account, index) in accounts" :key="account.id">
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ account.userName }}</td>
          <td>{{ account.email }}</td>
          <td>{{ formatDateTime(account.createdDate) }}</td>
          <td>{{ getAccountStatus(account) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import accountApi from '@/api/account.api';

const accounts = ref<any[]>([]);

onMounted(async () => {
  await loadAccounts();
});

const loadAccounts = async () => {
  try {
    const response = await accountApi.getAllAccounts();
    console.log('API Response:', response);
    
    if (response && response.data.isSuccess) {
      accounts.value = response.data.data;
    } else {
      accounts.value = [];
      alert(response.data.message);
    }
  } catch (error) {
    console.error('Error loading accounts:', error);
    alert('Failed to load accounts. Please try again.');
  }
};

const formatDateTime = (dateTimeStr: string) => {
  const date = new Date(dateTimeStr);
  return `${date.toLocaleDateString()} ${date.toLocaleTimeString()}`;
};

const getAccountStatus = (account: any) => {
  return account.isActive ? "Hoạt động" : "Không hoạt động";
};
</script>

<style scoped>

</style>
