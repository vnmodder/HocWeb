<template>
    <div class="container mt-4">
        <div class="row">
            <div v-for="item in products" :key="item.id" class="col-3">
                <div class="card">
                    <img :src="item.image" class="card-img-top" :alt="`Product ${item.name}`">
                    <div class="card-body">
                        <h5 class="card-title">{{ item.name }}</h5>
                        <p class="card-text">{{ item.description }}</p>
                        <div class="d-flex justify-content-between">
                            <a href="#" class="btn btn-primary">View</a>
                            <a href="#" class="btn btn-secondary">Add to Cart</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script setup lang='ts'>
import { useRoute } from 'vue-router';
import axios from 'axios';
import { ref } from 'vue';
import productApi from '@/api/product.api';

const route = useRoute();
const products = ref<any>([]);
const categoryName = ref<any>();

const fetchData = async () => {
  if (route.query.categoryId) {
    const cateRes = await productApi.getCategoryById(
      route.query.categoryId?.toString()
    );
    if (cateRes && cateRes.data.result.isSuccess) {
      categoryName.value = cateRes.data.result.data?.nameVN;
    }
  }

  const response = await productApi.getAllProduct();
  if (response && response.data.result.isSuccess) {
    if (route.query.categoryId) {
      products.value = response.data.result.data?.filter(
        (x: any) => x.categoryId == route.query.categoryId
      );
    } else {
      products.value = response.data.result.data;
    }
  } else {
    products.value = [];
    alert(response.data.result.message);
  }
};
fetchData();

</script>