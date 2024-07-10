<template>
    <div class="container mt-5">
    <div class="row">
      <div class="col-md-6">
        <img :src="product?.image" class="img-fluid" alt="Product Image">
      </div>
      <div class="col-md-6">
        <h1 class="display-5">{{ product?.name }}</h1>
        <h3 class="text-primary">{{ new Intl.NumberFormat("vi-vn").format(product.unitPrice) }}</h3>
        <p>Description: {{product?.description}}</p>
        <p>Quantity: <strong>Product Quantity</strong></p>
        <p>Created Date: <strong>Created Date</strong></p>
        <button class="btn btn-primary">Add to Cart</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRoute } from "vue-router";
import productDApi from "@/api/product-detail.api";


const route = useRoute();
const product = ref<any>();
const products = ref<any>([]);
const quantity = ref<number>(1);

const fetchData = async () => {
  if (route.query.id) {
    const res = await productDApi.getProductById(route.query.id?.toString());
    if (res && res.data.result.isSuccess) {
      product.value = res.data.result.data;

      const response = await productDApi.getAllProduct();
      if (response && response.data.result.isSuccess) {
        products.value = response.data.result.data?.filter(
          (x: any) => x.categoryId == product.value?.categoryId
        );
      } else {
        products.value = [];
      }
    } else {
      alert(res.data.result.message);
    }
  }
};



  fetchData();
</script>