<template>
  <main>
    <div class="row row-cols-1 row-cols-md-3 mb-3 text-center">
      <div v-for="item in items" class="col">
        <div class="card mb-4 rounded-3 shadow-sm">
          <div class="card-header py-3">
            <h4 class="my-0 fw-normal">{{ item.nameVN }}</h4>
          </div>
          <div class="card-body">
            <h1 class="card-title pricing-card-title">{{ item.name }}</h1>
            <ul class="list-unstyled mt-3 mb-4">
              <li>
                <img :src="item.image" /></li>
              <li>{{ item.icon }}</li>
            </ul>
            <!-- <button type="button" class="w-100 btn btn-lg btn-outline-primary">Sign up for free</button> -->
          </div>
        </div>
      </div>
    </div>
  </main>
</template>

<script setup lang="ts">
import homeApi from '@/api/home.api'
import { ref } from 'vue'

const items = ref([]);

const fetchData = async () => {
  const response = await homeApi.getAllCategory();
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